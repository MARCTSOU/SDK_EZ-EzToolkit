"""
Insert register names into .imf field lines, e.g.:
    ANCTL_TX_EN reg7684[0] 1 RW Word
->  ANCTL_CONFIG_1-ANCTL_TX_EN reg7684[0] 1 RW Word

Register names are looked up from the matching .xlsx file's "Reg Table"
sheet (column layout: Offset Address | Bit | Register | Field | ...).
Reads everything under imf/ and writes the converted files under imf_2/,
mirroring the same directory structure.
"""
import re
from pathlib import Path

import openpyxl

ROOT = Path(__file__).resolve().parent
IMF_DIR = ROOT / "imf"
XLSX_DIR = ROOT / "xlsx"
OUT_DIR = ROOT / "imf_2"

FIELD_SUFFIX_RE = re.compile(r"\[[^\[\]]*\]$")
LINE_RE = re.compile(r"^(\S+)(\s.*)$")


def strip_bit_suffix(token: str) -> str:
    return FIELD_SUFFIX_RE.sub("", token)


def load_field_register_map(xlsx_path: Path) -> dict:
    """Field name -> Register name, built from the 'Reg Table' sheet."""
    wb = openpyxl.load_workbook(xlsx_path, data_only=True, read_only=True)
    ws = wb["Reg Table"]

    field_to_register = {}
    current_register = None
    for row in ws.iter_rows(min_row=2, values_only=True):
        register, field = row[2], row[3]
        if register is not None:
            current_register = str(register).strip()
        if field is None:
            continue
        field = str(field).strip()
        if not field or field.upper() == "RESERVED":
            continue
        if field in field_to_register and field_to_register[field] != current_register:
            print(f"  [WARN] '{field}' maps to multiple registers in {xlsx_path.name}: "
                  f"'{field_to_register[field]}' vs '{current_register}' (keeping first)")
            continue
        field_to_register[field] = current_register
    wb.close()
    return field_to_register


def find_matching_xlsx(imf_rel: Path) -> Path | None:
    candidate = (XLSX_DIR / imf_rel).with_suffix(".xlsx")
    if candidate.exists():
        return candidate

    # Fallback for imf files with no exact xlsx counterpart (e.g. extra
    # "_eco2" suffix): progressively strip trailing "_token" segments.
    parts = imf_rel.stem.split("_")
    while len(parts) > 1:
        parts.pop()
        candidate = XLSX_DIR / imf_rel.parent / ("_".join(parts) + ".xlsx")
        if candidate.exists():
            return candidate
    return None


def find_by_suffix(base_name: str, field_to_register: dict) -> str | None:
    """Fallback for xlsx fields that carry a module-name prefix the imf
    field name drops, e.g. xlsx 'PCRMU_PERIPH_ID_LEN' vs imf 'PERIPH_ID_LEN'.
    Only applied when exactly one xlsx field matches on a '_'-boundary."""
    suffix = "_" + base_name
    candidates = [(f, r) for f, r in field_to_register.items() if f.endswith(suffix)]
    if len(candidates) == 1:
        return candidates[0][1]
    return None


def convert_file(imf_path: Path, xlsx_path: Path, out_path: Path) -> list:
    field_to_register = load_field_register_map(xlsx_path)

    unmatched = []
    out_lines = []
    for raw_line in imf_path.read_text(encoding="utf-8").splitlines():
        m = LINE_RE.match(raw_line)
        if not m:
            out_lines.append(raw_line)
            continue
        token, rest = m.group(1), m.group(2)
        base_name = strip_bit_suffix(token)
        register = field_to_register.get(base_name)
        if register is None:
            register = find_by_suffix(base_name, field_to_register)
        if register is None:
            unmatched.append(base_name)
            out_lines.append(raw_line)
        else:
            out_lines.append(f"{register}-{token}{rest}")

    out_path.parent.mkdir(parents=True, exist_ok=True)
    out_path.write_text("\n".join(out_lines) + "\n", encoding="utf-8")
    return unmatched


def main():
    imf_files = sorted(IMF_DIR.rglob("*.imf"))
    no_match_files = []
    files_with_unmatched = []

    for imf_path in imf_files:
        imf_rel = imf_path.relative_to(IMF_DIR)
        xlsx_path = find_matching_xlsx(imf_rel)
        out_path = OUT_DIR / imf_rel

        if xlsx_path is None:
            print(f"[NO XLSX MATCH] {imf_rel} -> copied unchanged")
            out_path.parent.mkdir(parents=True, exist_ok=True)
            out_path.write_text(imf_path.read_text(encoding="utf-8"), encoding="utf-8")
            no_match_files.append(str(imf_rel))
            continue

        unmatched = convert_file(imf_path, xlsx_path, out_path)
        tag = xlsx_path.relative_to(XLSX_DIR)
        print(f"[OK] {imf_rel}  (xlsx: {tag})"
              + (f"  -- {len(unmatched)} unmatched field(s)" if unmatched else ""))
        if unmatched:
            files_with_unmatched.append((str(imf_rel), unmatched))

    print("\n=== Summary ===")
    print(f"Total imf files: {len(imf_files)}")
    print(f"Converted with register names: {len(imf_files) - len(no_match_files)}")
    if no_match_files:
        print("Files with no xlsx match (copied unchanged):")
        for f in no_match_files:
            print(f"  - {f}")
    if files_with_unmatched:
        print("Files with unmatched fields (left un-prefixed, need manual check):")
        for f, fields in files_with_unmatched:
            print(f"  - {f}: {fields}")


if __name__ == "__main__":
    main()
