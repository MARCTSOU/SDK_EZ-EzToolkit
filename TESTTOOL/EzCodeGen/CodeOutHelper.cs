using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public static class CodeOutHelper
    {
        public static void CodeOut(StreamWriter sw, string str)
        {
            if (str.Equals("WRONG H"))
            {
                sw.WriteLine(str);
                return;
            }

            // Generate filter result 模式 (MainForm.GenerateAllowedAddrs 非 null 時): 這顆位址目前
            // 沒通過 REG TABLE 的 Topic/DependsOn 過濾就整行跳過不寫; null 代表 Generate all result, 不過濾。
            if (MainForm.GenerateAllowedAddrs != null && !PassesGenerateFilter(str))
                return;

            // Generate 現在以 REG TABLE 目前顯示的值為資料來源: 這顆位址如果在 REG TABLE 76(+)顆
            // 追蹤範圍內, 用 REG TABLE 目前的值蓋掉這裡原本算出來的值; 不在範圍內 (Initial_Reg_Array、
            // Hopping table、以及呼叫端主動把 GenerateValueOverrides 存空的那段) 就維持原樣不動。
            str = ApplyValueOverride(str);

            if (MainForm.is8BitsFormat)   // 8bits
            {
                str = str.Trim().TrimEnd(',');
                string[] parts = str.Split(',');
                string first = parts[0].Trim();
                string second = parts[1].Trim();
                string hex32 = second.Substring(2);

                List<string> bytes = new List<string>();
                for (int i = hex32.Length - 2; i >= 0; i -= 2)
                {
                    bytes.Add("0x" + hex32.Substring(i, 2));
                }
                string str2 = $"{first}, {{{string.Join(", ", bytes)}}},";
                sw.WriteLine("    " + str2);
            }
            else          // 32bits
            {
                sw.WriteLine("    " + str);
            }
        }

        /// <summary>從 "0xADDR, 0xVALUE" 這種字串抓出位址, 檢查是否在 MainForm.GenerateAllowedAddrs 裡; 解析不出位址就不擋 (保守起見不誤殺)</summary>
        private static bool PassesGenerateFilter(string str)
        {
            string trimmed = str.Trim().TrimEnd(',');
            int comma = trimmed.IndexOf(',');
            string addrText = (comma >= 0 ? trimmed.Substring(0, comma) : trimmed).Trim();
            ushort addr;
            if (!TryParseAddr(addrText, out addr))
                return true;
            return MainForm.GenerateAllowedAddrs.Contains(addr);
        }

        /// <summary>MainForm.GenerateValueOverrides 非 null 時, 把這行的值換成 REG TABLE 目前顯示的值; 解析不出位址、或位址不在 override 表裡就原樣不動</summary>
        private static string ApplyValueOverride(string str)
        {
            if (MainForm.GenerateValueOverrides == null)
                return str;
            string trimmed = str.Trim().TrimEnd(',');
            int comma = trimmed.IndexOf(',');
            if (comma < 0)
                return str;
            string addrText = trimmed.Substring(0, comma).Trim();
            ushort addr;
            uint overrideValue;
            if (!TryParseAddr(addrText, out addr) || !MainForm.GenerateValueOverrides.TryGetValue(addr, out overrideValue))
                return str;
            return addrText + ", 0x" + overrideValue.ToString("X8") + ",";
        }

        private static bool TryParseAddr(string addrText, out ushort addr)
        {
            addr = 0;
            if (!addrText.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                return false;
            return ushort.TryParse(addrText.Substring(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out addr);
        }
    }
}
