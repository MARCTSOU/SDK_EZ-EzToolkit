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
        public static void SetGpio0(StreamWriter sw, bool en, int type)
        {
            sw.WriteLine("    //====== " + RegCategories.Get(0x4000) + " ======");
            string hexString = "";
            uint tmp = 0x160000;
            if (en)
            {
                tmp = 0x160100;
                tmp |= (uint)type;
                hexString = "0x" + tmp.ToString("X8") + ",";
                CodeOutHelper.CodeOut(sw, ("0x4000, " + hexString));
            }
            else
            {
                hexString = "0x" + tmp.ToString("X8") + ",";
                CodeOutHelper.CodeOut(sw, ("0x4000, " + hexString));
            }
        }
        public static void SetGpio1(StreamWriter sw, bool en, int type)
        {
            sw.WriteLine("    //====== " + RegCategories.Get(0x4004) + " ======");
            string hexString = "";
            uint tmp = 0x160000;
            if (en)
            {
                tmp = 0x160100;
                tmp |= (uint)type;
                hexString = "0x" + tmp.ToString("X8") + ",";
                CodeOutHelper.CodeOut(sw, ("0x4004, " + hexString));
            }
            else
            {
                hexString = "0x" + tmp.ToString("X8") + ",";
                CodeOutHelper.CodeOut(sw, ("0x4004, " + hexString));
            }
        }
    }
}
