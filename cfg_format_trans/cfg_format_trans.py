"""
Translate SPI_ER41xx_config.h's raw {addr, {d0,d1,d2,d3}} register-write
entries into named #define macros, e.g.:
    0x0010, {0xFF, 0xFF, 0xFF, 0xFF}
->  #define SW_RST_REG_1ST  {0x10, 0x00}, {0xFF, 0xFF, 0xFF, 0xFF}

The macro name is the register name (the part before the first '-')
looked up from Register/imf_2/**/*.imf, which already carries
"REGISTER_NAME-FIELD_NAME regADDR[...] ..." lines produced by
table_to_list.py. When the same address is written more than once with
different data, each distinct value gets an ordinal suffix (_1ST, _2ND, ...);
repeats of the exact same value reuse the same macro name.
"""
import re
from pathlib import Path

ROOT = Path(__file__).resolve().parent
CONFIG_H = ROOT / "SPI_ER41xx_config_NU384.h"
IMF2_DIR = ROOT / "Register" / "imf_2_fixed"
OUT_H = ROOT / f"{CONFIG_H.stem}_output.h"

ARRAY_NAMES = ["Initial_Reg_Array", "TRx_Config_Array", "TRx_ConfigEx_Array"]
IMF_LINE_RE = re.compile(r"^([A-Za-z0-9_]+)-\S+\s+reg(\d+)")
ENTRY_RE = re.compile(
    r"0x([0-9A-Fa-f]{1,4})\s*,\s*\{\s*"
    r"(0x[0-9A-Fa-f]{1,2})\s*,\s*(0x[0-9A-Fa-f]{1,2})\s*,\s*"
    r"(0x[0-9A-Fa-f]{1,2})\s*,\s*(0x[0-9A-Fa-f]{1,2})\s*\}"
)
# ch_pll_item_t has 5 uint8_t[4] fields: freq, reg_1998, reg_199C, reg_19A0, reg_1994
CHANNEL_FIELD_NAMES = ["FREQ", "1998", "199C", "19A0", "1994"]
CHANNEL_FIELDS_PER_ENTRY = len(CHANNEL_FIELD_NAMES)
CHANNEL_BYTE_GROUP_RE = re.compile(
    r"((?:0x[0-9A-Fa-f]{1,2}\s*,\s*){3}0x[0-9A-Fa-f]{1,2})"
    r"\s*,?[ \t]*(?://\s*([^\n]*))?"
)

# Static preamble for the generated file: same top comment block as
# SPI_ER41xx_config.h, but GLB_DATA_RATE/GLB_DEVIATION are moved down to
# just above the generated #define sections, and the struct typedefs /
# real array declarations are dropped entirely (this file is macros only).
HEADER_TEXT = """/* **********************************************************************
 *
 * @8bits setting
 * @file     SPI_ER41xx_config.h
 * @version  V4.04 for other crystal
 * @brief    ER41xx series SPI driver
 * config file
 *
 * @copyright (C) COPYRIGHT 2020 ESMT
 * Technology Corp. All rights reserved.
 * ********************************************************************** */
/* Comments ------------------------------------------------------------------*/
// Crystal              : 38.4MHz
// CAP IO               : 33
/* RF Properties */
// Base frequency       : 435MHz
// Data Rate            : 96000bps
// Deviation            : 48000Hz
// Modulation           : GFSK
// TX Power - PA Type   : 23dBm
// TX Power - Level     : 127
/* Packet Format */
// TX Preamble          : 0xAA, 8byte(s)
// Tx Syncword          : 0x5A0FBE66
// Rx Syncword          : 0x5A0FBE66
// Syncword bit error   : 1bit(s)
/* Interrupt */
// INT_ST_RX
// INT_ST_TX
/* Define to prevent recursive inclusion -------------------------------------*/
#ifndef __SPI_ER41XX_CONFIG_H__
#define __SPI_ER41XX_CONFIG_H__
/* Includes ------------------------------------------------------------------*/
#include "SPI_ER41xx.h"
#include "compiler_compat.h"
/* Definition & Macro -------------------------------------------------------*/
#define CRYSTAL_HZ          38400000
#define NUM_OF_CH_TABLE     1

/* Predefined resources */
#define PREDEFINED_RX_SZ                128
#define PREDEFINED_RX_SYNCWORD          0x5A0FBE66
#define TABLE_END_ADDR      0xFFFF
#define TABLE_END_DATA_A    0xFF
#define TABLE_END_DATA_B    0xFF
#define TABLE_END_DATA_C    0xFF
#define TABLE_END_DATA_D    0xFF
#define IS_TABLE_END(e) \\
     ((e).addr == TABLE_END_ADDR && \\
     (e).reg_data[0] == TABLE_END_DATA_A && \\
     (e).reg_data[1] == TABLE_END_DATA_B && \\
     (e).reg_data[2] == TABLE_END_DATA_C && \\
     (e).reg_data[3] == TABLE_END_DATA_D)
     
#define GLB_DATA_RATE {0x00,0x77,0x01,0x00}  //0x00017700 = 96000
#define GLB_DEVIATION {0x80,0xBB,0x00,0x00}  //0x0000BB80 = 48000"""

FOOTER_TEXT = """/* Struct --------------------------------------------------------*/
/* Exported constants --------------------------------------------------------*/

#endif /*__SPI_ER41XX_CONFIG_H__*/
/*** (C) COPYRIGHT 2020 ESMT Technology Corp. ***/"""


def ordinal(n: int) -> str:
    if 10 <= n % 100 <= 20:
        suffix = "TH"
    else:
        suffix = {1: "ST", 2: "ND", 3: "RD"}.get(n % 10, "TH")
    return f"{n}{suffix}"


def load_addr_to_regname() -> dict:
    addr_to_regname = {}
    for imf_path in sorted(IMF2_DIR.rglob("*.imf")):
        for line in imf_path.read_text(encoding="utf-8").splitlines():
            m = IMF_LINE_RE.match(line)
            if not m:
                continue
            regname, addr = m.group(1), int(m.group(2))
            existing = addr_to_regname.get(addr)
            if existing is not None and existing != regname:
                print(f"  [WARN] address 0x{addr:04X} maps to both '{existing}' and "
                      f"'{regname}' ({imf_path.relative_to(ROOT)}) - keeping '{existing}'")
                continue
            addr_to_regname[addr] = regname
    return addr_to_regname


def extract_entries(text: str, array_name: str) -> list:
    m = re.search(array_name + r"\[\]\s*=\s*\{(.*?)\n\s*TABLE_END_ADDR", text, re.S)
    if not m:
        raise ValueError(f"could not find array body for {array_name}")
    entries = []
    for addr_hex, d0, d1, d2, d3 in ENTRY_RE.findall(m.group(1)):
        addr = int(addr_hex, 16)
        data = tuple(int(d, 16) for d in (d0, d1, d2, d3))
        entries.append((addr, data))
    return entries


def build_defines(entries: list, addr_to_regname: dict) -> list:
    # Pass 1: distinct data values per address, in order of first appearance.
    distinct_by_addr = {}
    for addr, data in entries:
        values = distinct_by_addr.setdefault(addr, [])
        if data not in values:
            values.append(data)

    unresolved = sorted({addr for addr in distinct_by_addr if addr not in addr_to_regname})
    if unresolved:
        print("  [WARN] addresses with no register name in imf_2 (using REG_XXXX fallback):")
        for addr in unresolved:
            print(f"    - 0x{addr:04X}")

    def base_name(addr):
        return addr_to_regname.get(addr, f"REG_{addr:04X}")

    # Pass 2: assign macro names in order of first appearance, preserving
    # source order; only emit one #define per distinct (addr, data) pair.
    defines = []  # list of (name, addr, data)
    seen = {}  # (addr, data) -> name
    name_index = {}  # addr -> how many distinct values assigned so far
    for addr, data in entries:
        key = (addr, data)
        if key in seen:
            continue
        total_distinct = len(distinct_by_addr[addr])
        name = base_name(addr)
        if total_distinct > 1:
            idx = name_index.get(addr, 0) + 1
            name_index[addr] = idx
            name = f"{name}_{ordinal(idx)}"
        seen[key] = name
        defines.append((name, addr, data))
    return defines


def format_define(name: str, addr: int, data: tuple) -> str:
    addr_lo, addr_hi = addr & 0xFF, (addr >> 8) & 0xFF
    addr_str = f"{{0x{addr_lo:02X}, 0x{addr_hi:02X}}}"
    data_str = "{" + ", ".join(f"0x{b:02X}" for b in data) + "}"
    return f"#define {name}  {addr_str}, {data_str}"


def extract_channel_defines(text: str) -> list:
    """CHANNEL_PLL_TABLE_CH{n}_{field} for each 4-byte group of each
    channel_pll_table[] entry, e.g. CHANNEL_PLL_TABLE_CH0_FREQ {...},
    CHANNEL_PLL_TABLE_CH0_1998 {...} // 1998, following the ch_pll_item_t
    member order (freq, reg_1998, reg_199C, reg_19A0, reg_1994)."""
    m = re.search(r"channel_pll_table\[\]\s*=\s*\{(.*?)\n\};", text, re.S)
    if not m:
        raise ValueError("could not find channel_pll_table body")

    lines = []
    for i, (blob, comment) in enumerate(CHANNEL_BYTE_GROUP_RE.findall(m.group(1))):
        channel = i // CHANNEL_FIELDS_PER_ENTRY
        field = CHANNEL_FIELD_NAMES[i % CHANNEL_FIELDS_PER_ENTRY]
        data_str = "{" + ", ".join(f"0x{int(b, 16):02X}" for b in blob.split(",")) + "}"
        line = f"#define CHANNEL_PLL_TABLE_CH{channel}_{field} {data_str}"
        if comment.strip():
            line += f"  // {comment.strip()}"
        lines.append(line)
    return lines


def format_flat_array_macro(macro_name: str, names: list) -> str:
    """A backslash-continued macro listing each name, e.g.:
    #define RF_REG_INIT_ARRAY { \\
              SW_RST_REG_1ST, \\
              ...
    }"""
    lines = [f"#define {macro_name} {{ \\"]
    for name in names:
        lines.append(f"          {name}, \\")
    lines.append("}")
    return "\n".join(lines)


def main():
    addr_to_regname = load_addr_to_regname()
    print(f"Loaded {len(addr_to_regname)} address->register-name mappings from imf_2\n")

    text = CONFIG_H.read_text(encoding="utf-8")

    channel_defines = extract_channel_defines(text)
    channel_names = [line.split()[1] for line in channel_defines]
    channel_defines = channel_defines + ["", format_flat_array_macro("CHANNEL_PLL_TABLE_ARRAY", channel_names)]
    print(f"channel_pll_table -> {len(channel_names)} defines\n")
    sections = [("channel_pll_table", channel_defines)]

    reg_names_all = []
    for array_name in ARRAY_NAMES:
        print(f"--- {array_name} ---")
        entries = extract_entries(text, array_name)
        defines = build_defines(entries, addr_to_regname)
        reg_names_all.extend(name for name, _, _ in defines)
        sections.append((array_name, [format_define(name, addr, data) for name, addr, data in defines]))
        print(f"  {len(entries)} entries -> {len(defines)} defines\n")

    last_title, last_defines = sections[-1]
    sections[-1] = (last_title, last_defines + ["", format_flat_array_macro("RF_REG_INIT_ARRAY", reg_names_all)])

    body = "\n\n".join(
        "\n".join([f"//====== {title} ======"] + defines)
        for title, defines in sections
    )

    OUT_H.write_text(HEADER_TEXT + "\n" + body + "\n" + FOOTER_TEXT + "\n", encoding="utf-8")
    print(f"Wrote {OUT_H.relative_to(ROOT)}")


if __name__ == "__main__":
    main()
