using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public class Initial_Reg_Array
    {
        /// <summary>
        /// 唯一的資料來源(73筆: 位址, 值)。WriteItem8Bits()/WriteItem32Bits()都從這裡算輸出格式,
        /// 不再各自手打一份文字——避免兩份拷貝改一邊忘了改另一邊, 悄悄長出不同步的差異。
        /// </summary>
        public static readonly (ushort Addr, uint Value)[] Entries = new (ushort, uint)[]
        {
            (0x0010, 0xFFFFFFFF), 
            (0x0010, 0x00000000), 
            (0x0000, 0x00000007), 
            (0x0014, 0x00000001),
            (0x1100, 0x00003821), 
            (0x1280, 0x00007890), 
            (0x1284, 0x00200015), 
            (0x1380, 0x0016A785),
            (0x1384, 0x3FCC02A8), 
            (0x1390, 0x00000708), 
            (0x1580, 0x00C28548), 
            (0x1620, 0x441405E8),
            (0x1624, 0x02920135), 
            (0x1700, 0x02000041), 
            (0x4004, 0x00160000), 
            (0x400C, 0x00060000),
            (0x1A14, 0x00000000), 
            (0xB0A8, 0x6F00370E), 
            (0xB19C, 0x00015F03), 
            (0xB208, 0x06068544),
            (0xB158, 0x00001200), 
            (0xB15C, 0x33222222), 
            (0xB160, 0x33333333), 
            (0xB164, 0x33333333),
            (0xB168, 0x33333333), 
            (0xB16C, 0x00000000), 
            (0xB170, 0x33222220), 
            (0xB174, 0x33333333),
            (0xB178, 0x33333333), 
            (0xB17C, 0x54987654), 
            (0xB180, 0x21432106), 
            (0xB184, 0x43210543),
            (0xB188, 0xCBA98765), 
            (0xB18C, 0x66555555), 
            (0xB190, 0x66666666),
            (0xB194, 0x77777666),
            (0xB198, 0x77777777), 
            (0xB1BC, 0x0032001A), 
            (0xB1C0, 0x0062004A), 
            (0xB1C4, 0x0092007A),
            (0xB1C8, 0x00C600A8), 
            (0xB1CC, 0x00F000DA), 
            (0xB1D0, 0x011C0104), 
            (0xB1D4, 0x014C0134),
            (0xB1D8, 0x017A0164), 
            (0xB1DC, 0x01AC0192), 
            (0xB1E0, 0x01D601C4), 
            (0xB1E4, 0x020201EA),
            (0xB1E8, 0x0236021A), 
            (0xB1EC, 0x02600248), 
            (0xB1F0, 0x0294027C), 
            (0xB1F4, 0x02C402AA),
            (0xB1F8, 0x02EC02DA), 
            (0xB250, 0x00000000), 
            (0xB254, 0x76543210), 
            (0xB258, 0x77777777),
            (0x1200, 0x8E1205A0), 
            (0x1204, 0x00000018), 
            (0x1E50, 0x253124ED), 
            (0x1E54, 0x24312431),
            (0x1E58, 0x122D122D), 
            (0x1E5C, 0x00000112), 
            (0x1E18, 0x04041400), 
            (0x1E00, 0x00010101),
            (0x1E3C, 0x0000000D), 
            (0x1E30, 0xFDFFFF3F), 
            (0x1E34, 0x00000001), 
            (0x1E64, 0x00000000),
            (0xB30C, 0x00000201), 
            (0x1488, 0x00000001), 
            (0x1600, 0x000A0200), 
            (0x1E60, 0x00000001),
            (0x1624, 0x13D20135),
        };

        public static void WriteItem8Bits(StreamWriter sw)
        {
            sw.WriteLine("/* Config Table*/");
            sw.WriteLine("code const t_reg_array Initial_Reg_Array[] = {");
#if false
            WriteEntriesGrouped(sw, 1,
                (addr, val) => $"0x{addr:X4}, {{0x{val & 0xFF:X2}, 0x{(val >> 8) & 0xFF:X2}, 0x{(val >> 16) & 0xFF:X2}, 0x{(val >> 24) & 0xFF:X2}}}");
                //(addr, val) => string.Format("0x{0:X4}, {{0x{1:X2}, 0x{2:X2}, 0x{3:X2}, 0x{4:X2}}}",
                   // addr, val & 0xFF, (val >> 8) & 0xFF, (val >> 16) & 0xFF, (val >> 24) & 0xFF));
#else
            WriteEntriesGrouped(sw, 1, (addr, val) =>
            {
                byte b0 = (byte)(val & 0xFF);
                byte b1 = (byte)((val >> 8) & 0xFF);
                byte b2 = (byte)((val >> 16) & 0xFF);
                byte b3 = (byte)((val >> 24) & 0xFF);
                return $"0x{addr:X4}," +"{" + $"0x{b0:X2}, 0x{b1:X2}, 0x{b2:X2}, 0x{b3:X2}"+"}";
            });
#endif
            sw.WriteLine("    TABLE_END_ADDR, {TABLE_END_DATA_A, TABLE_END_DATA_B, TABLE_END_DATA_C, TABLE_END_DATA_D}};");
            sw.WriteLine("//------------------------------------------------");
        }

        public static void WriteItem32Bits(StreamWriter sw)
        {
            sw.WriteLine("/* Config Table*/");
            sw.WriteLine("code const t_reg_array Initial_Reg_Array[] = {");
            WriteEntriesGrouped(sw, 1, (addr, val) =>
                string.Format("0x{0:X4}, 0x{1:X8}", addr, val));
            sw.WriteLine("    TABLE_END_ADDR,  TABLE_END_DATA};");
            sw.WriteLine("//------------------------------------------------");
        }

        /// <summary>把Entries每itemsPerLine筆組成一行(用formatItem格式化每一筆), 逐行輸出, 每行結尾補逗號。</summary>
        private static void WriteEntriesGrouped(StreamWriter sw,   int itemsPerLine, 
            Func<ushort, uint, string> formatItem)
        {
            for (int i = 0; i < Entries.Length; i += itemsPerLine)
            {
                var chunk = Entries.Skip(i).Take(itemsPerLine);
                string line = string.Join(", ", chunk.Select(e => formatItem(e.Addr, e.Value)));
                sw.WriteLine("    " + line + ",");
            }
        }
    }
}
