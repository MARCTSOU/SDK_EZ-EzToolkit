using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public class PayloadOrder
    {
        public static void Set(StreamWriter sw, bool msbEnable)
        {
            if (msbEnable)
            {
                sw.WriteLine("    //====== " + RegCategories.Get(0xA0A8) + " ======");
                CodeOutHelper.CodeOut(sw, ("0xA0A8, 0x00000001,"));
            }
            else
            {
                sw.WriteLine("    //====== " + RegCategories.Get(0xA0A8) + " ======");
                CodeOutHelper.CodeOut(sw, ("0xA0A8, 0x00000000,"));
            }
        }
    }
}
