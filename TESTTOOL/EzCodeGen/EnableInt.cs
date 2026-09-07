using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public  class EnableInt
    {
        public static void Set(StreamWriter sw, uint type)
        {
            sw.WriteLine("    //====== " + RegCategories.Get(0xF004) + " ======");
            string hexString = "0x" + type.ToString("X8") + ",";
            CodeOutHelper.CodeOut(sw, ("0xF004, "+hexString));
            //sw.WriteLine("    0xF004, " + hexString);
        }

        public static void SetTxRxThr(StreamWriter sw, uint txThr, uint rxThr)
        {
            sw.WriteLine("    //====== " + RegCategories.Get(0xA0A4) + " ======");
            uint tmp = ((128 - rxThr) << 16) | txThr;
            string hexString = "0x" + tmp.ToString("X8") + ",";
            CodeOutHelper.CodeOut(sw, ("0xA0A4, " + hexString));
            //sw.WriteLine("    0xA0A4, " + hexString);
        }
    }
}
