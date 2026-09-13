using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ER41xxTestTool
{
    public class PCRMU
    {
        public static void Set(StreamWriter sw, bool en, bool shdDisable)
        {
            //Enable - [0x0020]0x00000004(或依shdDisable算), [0x0028]0x00000000, [0x1A00]0xFFFFFFF, [0x1A08]0xFFFFFFF
            //Disable - [0x0020]0xFFFFFFFF, [0x1A00]0x00000000, [0x1A08]0x00000000
            sw.WriteLine("    //====== " + RegCategories.Get(0x0020) + " ======");
            if (en)
            {
                // PMU_CTL_REG(0x0020) bit2 = PMU_DIS_SHUTDOWN: 1=禁用shutdown mode, 0=允許。
                // 呼叫端傳入shdDisable = !paramPcrShdEnableRadioBtn.Checked, 也就是選"Enable + Shutdown"
                // 才會是shdDisable=false(bit2=0, 允許shutdown), 其餘兩個選項都是shdDisable=true(bit2=1, 禁用)。
                uint reg0020 = shdDisable ? 0x00000004u : 0x00000000u;
                CodeOutHelper.CodeOut(sw, ("0x0020, 0x" + reg0020.ToString("X8") + ","));
                CodeOutHelper.CodeOut(sw, ("0x0028, 0x00000000,"));
                CodeOutHelper.CodeOut(sw, ("0x1A00, 0xFFFFFFFF,"));
                CodeOutHelper.CodeOut(sw, ("0x1A08, 0xFFFFFFFF,"));
            }
            else
            {
                CodeOutHelper.CodeOut(sw, ("0x0020, 0xFFFFFFFF,"));
                //CodeOutHelper.CodeOut(sw, ("0x0028, 0x00000000,"));
                CodeOutHelper.CodeOut(sw, ("0x1A00, 0x00000000,"));
                CodeOutHelper.CodeOut(sw, ("0x1A08, 0x00000000,"));
            }

        }
    }
}
