using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public class Preamble
    {
        public static void Set(StreamWriter sw, uint Content, uint len)
        {
            //	if(u8PreambleLen>16)
            //return TRx_STATUS_FAIL;
            //u32Data = ((uint32_t)(u8PreambleLen - 1) | ((uint32_t)u8Preamble << 8));
            //TRx_WRITEREG(trx, 0xa02c, &u32Data);
            //------------------------------------------------------------------------
            //[0xa02c]((uint32_t)(u8PreambleLen - 1) | ((uint32_t)u8Preamble << 8)) u8Preamble: 0xAA or 0x55,...
            sw.WriteLine("    //====== " + RegCategories.Get(0xA02C) + " ======");
            uint tmp = 0;
            tmp = (len - 1) | Content << 8;
            string hexString = "0x" + tmp.ToString("X8") + ",";            
            CodeOutHelper.CodeOut(sw, ("0xA02C, "+hexString));
        }
    }
}
