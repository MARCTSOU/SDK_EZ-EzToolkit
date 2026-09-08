using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ER41xxTestTool
{
    /// <summary>
    /// 把 GenerateTxt() 目前組出來的 8bits 裸陣列格式 (0xADDR, {b0,b1,b2,b3}, 直接塞進
    /// Initial_Reg_Array[]/TRx_Config_Array[]/TRx_ConfigEx_Array[]) 轉成 Marc 要求的新格式:
    /// 每個 register 先變成具名 #define (INIT_ 前綴用於 Initial_Reg_Array),
    /// 陣列改成用 backslash 接續、只列 macro 名稱的 RF_REG_INIT_ARRAY / RF_REG_CONFIG_ARRAY /
    /// RF_REG_CONFIG_EX_ARRAY，結尾用 END_REG_DATA。register 產生的數值/順序完全不變，
    /// 只改表示方式，比對基準: 20260803_Marc要求\SPI_ER41xx_config_38C_output.h
    /// </summary>
    public static class NamedMacroFormatter
    {
        private const string Indent = "          "; // 10 spaces, 對齊範例

        private static readonly Regex EntryLineRe = new Regex(
            @"^0x([0-9A-Fa-f]{1,4})\s*,\s*\{\s*(0x[0-9A-Fa-f]{1,2})\s*,\s*(0x[0-9A-Fa-f]{1,2})\s*,\s*(0x[0-9A-Fa-f]{1,2})\s*,\s*(0x[0-9A-Fa-f]{1,2})\s*\}\s*,?\s*$");

        private static readonly Regex EntryAnyRe = new Regex(
            @"0x([0-9A-Fa-f]{1,4})\s*,\s*\{\s*(0x[0-9A-Fa-f]{1,2})\s*,\s*(0x[0-9A-Fa-f]{1,2})\s*,\s*(0x[0-9A-Fa-f]{1,2})\s*,\s*(0x[0-9A-Fa-f]{1,2})\s*\}");

        private static readonly Regex ChannelByteGroupRe = new Regex(
            @"((?:0x[0-9A-Fa-f]{1,2}\s*,\s*){3}0x[0-9A-Fa-f]{1,2})\s*,?[ \t]*(?://\s*([^\r\n]*))?");

        private static readonly string[] ChannelFieldNames_other = { "FREQ", "1998", "199C", "19A0", "1994" };
        private static readonly string[] ChannelFieldNames_40m = { "FREQ" };
        private static string[] ChannelFieldNames = { "FREQ", "1998", "199C", "19A0", "1994" };
        
        public static string Convert(string rawText)
        {
            // Generate filter result 模式 (MainForm.GenerateAllowedAddrs 非 null): 只要通過過濾的
            // register, 依 Topic 分組的 "//----- Topic -----" + #define, 檔頭/CRYSTAL_HZ 等 Predefined
            // Parameter/RF_REG_xxx_ARRAY 巨集/Hopping table/#endif 這些跟 filter 無關的內容全部不要。
            if (MainForm.GenerateAllowedAddrs != null)
                return ConvertFilteredOnly(rawText);

            var sb = new StringBuilder();

            // ---- Title + Comment 區塊 (不變, 原樣沿用) ----
            int defineIdx = rawText.IndexOf("/* Define to prevent recursive inclusion", StringComparison.Ordinal);
            string titleAndComment = defineIdx >= 0 ? rawText.Substring(0, defineIdx).TrimEnd() : "";
            sb.Append(titleAndComment).Append("\r\n");

            // ---- Header ----
            string crystalLine = ExtractLine(rawText, @"^#define CRYSTAL_HZ.*$");
            string rxSzLine = ExtractLine(rawText, @"^#define PREDEFINED_RX_SZ.*$");
            string rxSyncLine = ExtractLine(rawText, @"^#define PREDEFINED_RX_SYNCWORD.*$");
            string drLine = ExtractLine(rawText, @"^#define GLB_DATA_RATE.*$");
            string devLine = ExtractLine(rawText, @"^#define GLB_DEVIATION.*$");

            sb.Append("/* Define to prevent recursive inclusion -------------------------------------*/\r\n");
            sb.Append("#ifndef __SPI_ER41XX_CONFIG_H__\r\n");
            sb.Append("#define __SPI_ER41XX_CONFIG_H__\r\n");
            sb.Append("/* Includes ------------------------------------------------------------------*/\r\n");
            sb.Append("/* --------------------------------------------------------------------------*/\r\n");
            sb.Append("/* Predefined Parameter                                                      */\r\n");
            sb.Append("/* --------------------------------------------------------------------------*/\r\n");
            sb.Append(crystalLine).Append("\r\n");
            sb.Append(rxSzLine).Append("\r\n");
            sb.Append(rxSyncLine).Append("\r\n");
            sb.Append(drLine).Append("\r\n");
            sb.Append(devLine).Append("\r\n");
            sb.Append("\r\n");
            sb.Append("/* --------------------------------------------------------------------------*/\r\n");
            sb.Append("/* Configuration Register Data                                               */\r\n");
            sb.Append("/* --------------------------------------------------------------------------*/\r\n");

            // ---- 三個陣列: 各自建立具名 #define + 記錄陣列成員順序 ----
            string initBody = ExtractArrayBody(rawText, "Initial_Reg_Array");
            string configBody = ExtractArrayBody(rawText, "TRx_Config_Array");
            string configExBody = ExtractArrayBody(rawText, "TRx_ConfigEx_Array");

            // IncludeInitArrayCheckBox 沒勾時, CodeGenProcess 完全不會寫 Initial_Reg_Array[] 到 rawText,
            // initBody 就會是空的 —— 這裡連標題行、INIT_ 具名 #define、底下的 RF_REG_INIT_ARRAY 巨集
            // 都整段跳過, 不留下一個空殼的 macro。
            bool includeInit = !string.IsNullOrWhiteSpace(initBody);
            List<string> initNames = includeInit ? BuildInitSection(sb, "Initial_Reg_Array", initBody, "INIT_") : new List<string>();
            if (includeInit)
                sb.Append("\r\n");
            List<string> configNames = BuildLineSection(sb, "TRx_Config_Array", configBody, "");
            sb.Append("\r\n");
            List<string> configExNames = BuildLineSection(sb, "TRx_ConfigEx_Array", configExBody, "");
            sb.Append("\r\n");

            // ---- Configuration Register Array ----
            sb.Append("/* --------------------------------------------------------------------------*/\r\n");
            sb.Append("/* Configuration Register Array                                              */\r\n");
            sb.Append("/* --------------------------------------------------------------------------*/\r\n");
            sb.Append("#define END_REG_INT 0xFFFF\r\n");
            sb.Append("#define END_REG_CHAR 0xFF\r\n");
            sb.Append("#define END_REG_DATA END_REG_INT, {END_REG_CHAR, END_REG_CHAR, END_REG_CHAR, END_REG_CHAR}\r\n");
            sb.Append("\r\n");
            if (includeInit)
            {
                sb.Append(FormatFlatArrayMacro("RF_REG_INIT_ARRAY", initNames, "END_REG_DATA")).Append("\r\n");
                sb.Append("\r\n");
            }
            sb.Append(FormatFlatArrayMacro("RF_REG_CONFIG_ARRAY", configNames, "END_REG_DATA")).Append("\r\n");
            sb.Append("\r\n");
            sb.Append(FormatFlatArrayMacro("RF_REG_CONFIG_EX_ARRAY", configExNames, "END_REG_DATA")).Append("\r\n");

            // ---- Hopping Register Data / Array ----
            int numChTable = 0;
            Match numChMatch = Regex.Match(rawText, @"#define NUM_OF_CH_TABLE\s+(\d+)");
            if (numChMatch.Success)
                numChTable = int.Parse(numChMatch.Groups[1].Value);

            sb.Append("/* --------------------------------------------------------------------------*/\r\n");
            sb.Append("/* Hopping Register Data                                                     */\r\n");
            sb.Append("/* --------------------------------------------------------------------------*/\r\n");
            sb.Append("#define NUM_OF_CH_TABLE     ").Append(numChTable).Append("\r\n");

            if (numChTable > 0)
            {
                Match chMatch = Regex.Match(rawText, @"channel_pll_table\[\]\s*=\s*\{(.*?)\n\s*\};", RegexOptions.Singleline);
                if(crystalLine.Contains("40000000"))
                    ChannelFieldNames = ChannelFieldNames_40m;
                else
                    ChannelFieldNames = ChannelFieldNames_other; //for none 40MHz crystal

                if (chMatch.Success)
                {
                    List<string> channelNames = new List<string>();
                    sb.Append("//====== Register data of channel_pll_table ======\r\n");
                    int i = 0;
                    foreach (Match m in ChannelByteGroupRe.Matches(chMatch.Groups[1].Value))
                    {
                        int channel = i / ChannelFieldNames.Length;
                        string field = ChannelFieldNames[i % ChannelFieldNames.Length];
                        string dataStr = FormatByteGroup(m.Groups[1].Value);
                        string name = $"CHANNEL_PLL_TABLE_CH{channel}_{field}";
                        sb.Append("#define ").Append(name).Append(" ").Append(dataStr);
                        if (m.Groups[2].Success && m.Groups[2].Value.Trim().Length > 0)
                            sb.Append("  // ").Append(m.Groups[2].Value.Trim());
                        sb.Append("\r\n");
                        channelNames.Add(name);
                        i++;
                    }
                    sb.Append("\r\n");
                    sb.Append("/* --------------------------------------------------------------------------*/\r\n");
                    sb.Append("/* Hopping Register Array                                                    */\r\n");
                    sb.Append("/* --------------------------------------------------------------------------*/\r\n");
                    sb.Append("//====== Array of channel_pll_table ======\r\n");
                    sb.Append(FormatFlatArrayMacro("CHANNEL_PLL_TABLE_ARRAY", channelNames, null)).Append("\r\n");
                }
            }

            sb.Append("\r\n");
            sb.Append("#endif /*__SPI_ER41XX_CONFIG_H__*/\r\n");
            sb.Append("/*** (C) COPYRIGHT 2020 ESMT Technology Corp. ***/\r\n");

            return sb.ToString();
        }

        /// <summary>Generate filter result 專用: 只印 TRx_Config_Array / TRx_ConfigEx_Array 依 Topic 分組的區塊, 其餘什麼都不加</summary>
        private static string ConvertFilteredOnly(string rawText)
        {
            var sb = new StringBuilder();
            string configBody = ExtractArrayBody(rawText, "TRx_Config_Array");
            string configExBody = ExtractArrayBody(rawText, "TRx_ConfigEx_Array");

            BuildLineSectionBody(sb, configBody, "");
            BuildLineSectionBody(sb, configExBody, "");

            return sb.ToString();
        }

        private static string ExtractLine(string text, string pattern)
        {
            Match m = Regex.Match(text, pattern, RegexOptions.Multiline);
            return m.Success ? m.Value.TrimEnd() : "";
        }

        private static string ExtractArrayBody(string rawText, string arrayName)
        {
            Match m = Regex.Match(rawText, Regex.Escape(arrayName) + @"\[\]\s*=\s*\{(.*?)TABLE_END_ADDR", RegexOptions.Singleline);
            return m.Success ? m.Groups[1].Value : "";
        }

        /// <summary>Initial_Reg_Array: 4 個 entry 擠在同一行, 沒有註解, 直接整段 regex findall。</summary>
        private static List<string> BuildInitSection(StringBuilder sb, string arrayName, string body, string prefix)
        {
            sb.Append("//====== ").Append(arrayName).Append(" ======\r\n");
            var entries = new List<(int addr, byte[] data)>();
            foreach (Match m in EntryAnyRe.Matches(body))
                entries.Add(ParseEntry(m));

            List<(string name, int addr, byte[] data)> defines = BuildDefines(entries, prefix);
            foreach (var d in defines)
                sb.Append(FormatDefine(d.name, d.addr, d.data)).Append("\r\n");
            return defines.Select(d => d.name).ToList();
        }

        /// <summary>
        /// TRx_Config_Array / TRx_ConfigEx_Array: 一行一筆 entry, entry 依照 (addr,data) 去重、
        /// 同址不同值才加序數尾碼。原始 Set() 函式自己寫的 "//====== ... ======" 註解行完全不採用 ——
        /// 改成依每顆 register 的位址查 RegCategories 算出 Topic, 相鄰同 Topic 的 entry 合併成一個區塊,
        /// 區塊標題格式 "// {Topic} - {Tag1}(count1), {Tag2}(count2), ..." 列出該區塊內每個 DependsOn
        /// 原子標籤出現幾次 (查不到 DependsOn 的, 例如不在 REG TABLE 76 顆裡的位址, 不列入breakdown;
        /// 整個區塊都查不到 DependsOn 就只印 "// {Topic}")。
        /// </summary>
        private static List<string> BuildLineSection(StringBuilder sb, string arrayName, string body, string prefix)
        {
            sb.Append("//====== ").Append(arrayName).Append(" ======\r\n");
            return BuildLineSectionBody(sb, body, prefix);
        }

        /// <summary>BuildLineSection 拿掉 "//====== arrayName ======" 那行陣列標題後的核心邏輯, 供 ConvertFilteredOnly 直接重用 (filter 模式不需要陣列名稱標題)</summary>
        private static List<string> BuildLineSectionBody(StringBuilder sb, string body, string prefix)
        {
            string[] rawLines = body.Replace("\r\n", "\n").Split('\n');
            var entries = new List<(int addr, byte[] data)>();
            foreach (string rawLine in rawLines)
            {
                string line = rawLine.Trim();
                Match m = EntryLineRe.Match(line);
                if (m.Success)
                    entries.Add(ParseEntry(m));
            }

            List<(string name, int addr, byte[] data)> defines = BuildDefines(entries, prefix);
            var keyToDefine = new Dictionary<string, (string name, int addr, byte[] data)>();
            foreach (var d in defines)
                keyToDefine[Key(d.addr, d.data)] = d;

            // 依原始出現順序取出去重後的 define 清單, 保留順序才能正確算出「相鄰同 Topic」的區塊邊界
            var printed = new HashSet<string>();
            var orderedDefines = new List<(string name, int addr, byte[] data)>();
            foreach (string rawLine in rawLines)
            {
                string line = rawLine.Trim();
                if (line.Length == 0)
                    continue;
                Match m = EntryLineRe.Match(line);
                if (!m.Success)
                    continue;
                var entry = ParseEntry(m);
                string key = Key(entry.addr, entry.data);
                if (printed.Contains(key))
                    continue;
                printed.Add(key);
                orderedDefines.Add(keyToDefine[key]);
            }

            string currentTopic = null;
            var pending = new List<(string name, int addr, byte[] data)>();
            foreach (var d in orderedDefines)
            {
                string topic = RegCategories.Get((ushort)d.addr);
                if (currentTopic != null && topic != currentTopic)
                    FlushTopicBlock(sb, currentTopic, pending);
                currentTopic = topic;
                pending.Add(d);
            }
            FlushTopicBlock(sb, currentTopic, pending);

            return orderedDefines.Select(d => d.name).ToList();
        }

        /// <summary>印出一個 Topic 區塊: 標題 + 區塊內每個 #define, 印完清空 pending</summary>
        private static void FlushTopicBlock(StringBuilder sb, string topic, List<(string name, int addr, byte[] data)> pending)
        {
            if (pending.Count == 0)
                return;
            sb.Append("//----- ").Append(topic).Append(" -----\r\n");
            foreach (var d in pending)
            {
                // 0x0004 (MODEM_CLK_REG): 40MHz下硬體會在Synthesizer trigger(0x1980~0x1990)後自動
                // 依40MHz的時序填入這顆, 38.4MHz時硬體填的還是40MHz版本的錯誤值, 韌體必須在trigger
                // 之後重新寫入正確值才生效 —— 提醒使用產生出的.h檔的人不要調換寫入順序。
                if (d.name == "MODEM_CLK_REG")
                    sb.Append("// NOTE: must be written after the Synthesizer trigger (0x1980~0x1990), or the 40MHz-based hardware auto-fill will overwrite it.\r\n" +
                              "// Also remember to move MODEM_CLK_REG's entry in RF_REG_CONFIG_ARRAY (below) to after the Synthesizer trigger registers' entries (0x1980/0x1984/0x1988/0x1990).\r\n");
                sb.Append(FormatDefine(d.name, d.addr, d.data)).Append("\r\n");
            }
            pending.Clear();
        }

        private static (int addr, byte[] data) ParseEntry(Match m)
        {
            int addr = System.Convert.ToInt32(m.Groups[1].Value, 16);
            byte[] data = new byte[4];
            for (int i = 0; i < 4; i++)
                data[i] = (byte)System.Convert.ToInt32(m.Groups[i + 2].Value, 16);
            return (addr, data);
        }

        private static string Key(int addr, byte[] data)
        {
            return addr + ":" + BitConverter.ToString(data);
        }

        private static List<(string name, int addr, byte[] data)> BuildDefines(List<(int addr, byte[] data)> entries, string prefix)
        {
            var distinctByAddr = new Dictionary<int, List<byte[]>>();
            foreach (var e in entries)
            {
                if (!distinctByAddr.TryGetValue(e.addr, out List<byte[]> list))
                {
                    list = new List<byte[]>();
                    distinctByAddr[e.addr] = list;
                }
                if (!list.Any(d => d.SequenceEqual(e.data)))
                    list.Add(e.data);
            }

            var seen = new HashSet<string>();
            var nameIndex = new Dictionary<int, int>();
            var defines = new List<(string name, int addr, byte[] data)>();
            foreach (var e in entries)
            {
                string key = Key(e.addr, e.data);
                if (seen.Contains(key))
                    continue;
                seen.Add(key);

                string baseName = RegAddrNameMap.Lookup(e.addr);
                int totalDistinct = distinctByAddr[e.addr].Count;
                string name = baseName;
                if (totalDistinct > 1)
                {
                    int idx = nameIndex.TryGetValue(e.addr, out int cur) ? cur + 1 : 1;
                    nameIndex[e.addr] = idx;
                    name = baseName + "_" + Ordinal(idx);
                }
                name = prefix + name;
                defines.Add((name, e.addr, e.data));
            }
            return defines;
        }

        private static string Ordinal(int n)
        {
            if (n % 100 >= 10 && n % 100 <= 20)
                return n + "TH";
            switch (n % 10)
            {
                case 1: return n + "ST";
                case 2: return n + "ND";
                case 3: return n + "RD";
                default: return n + "TH";
            }
        }

        private static string FormatDefine(string name, int addr, byte[] data)
        {
            string dataStr = "{" + string.Join(", ", data.Select(b => $"0x{b:X2}")) + "}";
            return $"#define {name}  0x{addr:X4}, {dataStr}";
        }

        private static string FormatByteGroup(string commaSeparatedBytes)
        {
            var bytes = commaSeparatedBytes.Split(',')
                .Select(s => System.Convert.ToInt32(s.Trim(), 16));
            return "{" + string.Join(", ", bytes.Select(b => $"0x{b:X2}")) + "}";
        }

        private static string FormatFlatArrayMacro(string macroName, List<string> names, string terminator)
        {
            var sb = new StringBuilder();
            sb.Append("#define ").Append(macroName).Append(" { \\\r\n");
            foreach (string name in names)
                sb.Append(Indent).Append(name).Append(", \\\r\n");
            if (terminator != null)
                sb.Append(Indent).Append(terminator).Append(" \\\r\n");
            sb.Append("}");
            return sb.ToString();
        }
    }
}
