using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public class TxPayloadFormat
    {
        public static void Set(StreamWriter sw, uint txType)
        {
            uint tmp = 0x00000000;
            txType = txType & 0x0F;
            tmp = tmp | txType;
            sw.WriteLine("    //====== " + RegCategories.Get(0xA028) + " ======");
            string hexString = "0x" + tmp.ToString("X8") + ",";
            CodeOutHelper.CodeOut(sw, ("0xA028, "+hexString));  
        }
    }
}
