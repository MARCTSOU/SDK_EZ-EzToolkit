using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public class Syncword
    {
        // Tx Syncword
        //[0xA034]SyncwordLen-1 //byte (固定為4bytes)
        //[0xA038] Syncword //0x12345678, {78,56,34,12}
        //---------------------------------------------------
        // Rx Syncword
        // [0xB100]Syncword
        // Error bit
        // [0xB104](u32Data & ~0x03)|allowed_bit_err
        // allowed_bit_err = 0~7
        //---------------------------------------------------
        // [0xa0a8] 1 or 0 //For MSB enable or disable
        public static void Set(StreamWriter sw, string txSyncword, string rxSyncword, uint bitError)
        {
            uint tmp = 0x0007CC01;
            bitError = bitError & 0x0F;
            uint clrBit = 0x03;
            string hexString = "";
            sw.WriteLine("    //====== " + RegCategories.Get(0xA034) + " ======");
            CodeOutHelper.CodeOut(sw, ("0xA034, 0x00000003,"));  // (固定為4bytes -1)
            hexString = "0x" + txSyncword + ",";
            CodeOutHelper.CodeOut(sw, ("0xA038, " + hexString));  // tx Syncword
            sw.WriteLine("    //====== " + RegCategories.Get(0xB100) + " ======");
            hexString = "0x" + rxSyncword + ",";
            CodeOutHelper.CodeOut(sw, ("0xB100, " + hexString));   // rx Syncword
            //sw.WriteLine("    //====== Rx Syncword Error bit ======");
            tmp = (tmp & (~clrBit)) | bitError;
            hexString = "0x" + tmp.ToString("X8") + ",";            // allow bit error
            CodeOutHelper.CodeOut(sw, ("0xB104, " + hexString));
        }
    }
}
