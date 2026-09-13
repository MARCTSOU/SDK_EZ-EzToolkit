using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public static class WriteXoConfig // Xtal Gain and Cap IO
    {
        public static void Set(StreamWriter sw, uint cap, uint xtal_gain)
        {
            // 1. 確保 cap/xtal_gain 只取各自的合法 bit 數，預防傳入值超出範圍
            uint cap7bit = (uint)cap & 0x7F;
            uint gain4bit = (uint)xtal_gain & 0xF;   // XO_40M_GAIN_CTRL 只有 bit[7:4] 共4 bits (0~15)
            uint tmp = 0x00E04088;
            // 2. 清除 tmp 的 bit4 到 bit21 (共 18 個 bits): bit[7:4]=GAIN_CTRL, bit[14:8]=CAP_I_CTRL, bit[21:15]=CAP_O_CTRL
            // 遮罩值 0xFFC0000F 剛好會保留 bit0-3 與 bit22-31，將 bit4-21 設為 0
            tmp &= 0xFFC0000F;

            // 3. 將 gain4bit/cap7bit 分別寫入對應欄位
            // bit4-7:   XO_40M_GAIN_CTRL, gain4bit 左移 4 位
            // bit8-14:  XO_40M_CAP_I_CTRL, cap7bit 左移 8 位
            // bit15-21: XO_40M_CAP_O_CTRL, cap7bit 左移 15 位
            tmp |= (gain4bit << 4);
            tmp |= (cap7bit << 8);
            tmp |= (cap7bit << 15);
            string hexString = "0x" + tmp.ToString("X8") + ",";
            sw.WriteLine("    //====== " + RegCategories.Get(0x1580) + " ======");
            CodeOutHelper.CodeOut(sw, ("0x1580, "+hexString));
        }
    }
}
