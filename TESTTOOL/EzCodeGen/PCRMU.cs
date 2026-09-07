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
        public static void Set(StreamWriter sw, bool en)
        {
            //Enable - [0x0020]0x00000004, [0x0028]0x00000000, [0x1A00]0xFFFFFFF, [0x1A08]0xFFFFFFF
            //Disable - [0x0020]0xFFFFFFFF, [0x1A00]0x00000000, [0x1A08]0x00000000
            sw.WriteLine("    //====== " + RegCategories.Get(0x0020) + " ======");
            if (en)
            {
                CodeOutHelper.CodeOut(sw, ("0x0020, 0x00000004,"));
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
