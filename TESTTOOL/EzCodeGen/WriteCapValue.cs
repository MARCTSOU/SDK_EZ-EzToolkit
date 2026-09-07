using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public static class WriteCapValue
    {
        public static void Set(StreamWriter sw, uint cap)
        {
            // 1. 確保 cap 只取 7 bits (0~127)，預防傳入值超出範圍
            uint cap7bit = (uint)cap & 0x7F;
            uint tmp = 0x00E04088;
            // 2. 清除 tmp 的 bit8 到 bit21 (共 14 個 bits)
            // 遮罩值 0xFFC000FF 剛好會保留 bit0-7 與 bit22-31，將 bit8-21 設為 0
            tmp &= 0xFFC000FF;

            // 3. 將 cap7bit 寫入 bit8-14 與 bit15-21
            // bit8-14: cap7bit 左移 8 位
            // bit15-21: cap7bit 左移 15 位
            tmp |= (cap7bit << 8);
            tmp |= (cap7bit << 15);
            string hexString = "0x" + tmp.ToString("X8") + ",";
            sw.WriteLine("    //====== " + RegCategories.Get(0x1580) + " ======");
            CodeOutHelper.CodeOut(sw, ("0x1580, "+hexString));
        }
    }
}
