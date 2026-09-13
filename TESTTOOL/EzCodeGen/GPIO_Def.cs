using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public class GPIO_Def
    {
        // DS(bits[17:16])/SMT(bit18)/DBG_NUM(bits[27:24])目前沒有對應的UI控制項,
        // 固定沿用原本硬體預設值(DS=13.5mA, SMT=Enable, DBG_NUM=0)。
        private const uint FixedDsSmtDbgNumBits = (2u << 16) | (1u << 18);

        /// <summary>
        /// 組出GPIO_x_CFG_REG的完整32-bit值:
        /// bits[3:0]=GIO_SEL, bit[7]=GIO_INV, bits[10:8]=MODE, bits[20:19]=PULL_CTRL,
        /// 其餘(DS/SMT/DBG_NUM/RESERVED)沿用固定預設值。
        /// </summary>
        private static uint BuildGpioCfgValue(int mode, int gioSel, bool gioInv, int pullCtrl)
        {
            uint tmp = FixedDsSmtDbgNumBits;
            tmp |= (uint)(gioSel & 0xF);
            if (gioInv) tmp |= (1u << 7);
            tmp |= (uint)(mode & 0x7) << 8;
            tmp |= (uint)(pullCtrl & 0x3) << 19;
            return tmp;
        }

        public static void SetGpio0(StreamWriter sw, int mode, int gioSel, bool gioInv, int pullCtrl)
        {
            sw.WriteLine("    //====== " + RegCategories.Get(0x4000) + " ======");
            uint tmp = BuildGpioCfgValue(mode, gioSel, gioInv, pullCtrl);
            CodeOutHelper.CodeOut(sw, "0x4000, 0x" + tmp.ToString("X8") + ",");
        }

        public static void SetGpio1(StreamWriter sw, int mode, int gioSel, bool gioInv, int pullCtrl)
        {
            sw.WriteLine("    //====== " + RegCategories.Get(0x4004) + " ======");
            uint tmp = BuildGpioCfgValue(mode, gioSel, gioInv, pullCtrl);
            CodeOutHelper.CodeOut(sw, "0x4004, 0x" + tmp.ToString("X8") + ",");
        }
    }
}
