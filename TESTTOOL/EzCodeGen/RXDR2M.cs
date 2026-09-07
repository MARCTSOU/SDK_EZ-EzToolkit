using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public class RXDR2M
    {
        public static int Set(StreamWriter sw, double H, out string modemClkReg)
        {
            int i = 0;
            int res = 0;

            modemClkReg = "0x0004, 0x00010101,";
            CodeOutHelper.CodeOut(sw, "0xb0f0, 0x17342000,");  // OWen add 2025/06/12 改
            CodeOutHelper.CodeOut(sw, "0xb304, 0x00000010,");
            CodeOutHelper.CodeOut(sw, "0xb1b4, 0x0000ffd8,");
            CodeOutHelper.CodeOut(sw, "0x1604, 0x1fc01b7d,");
            if ((H >= 0) && (H < 0.5))
            {
                CodeOutHelper.CodeOut(sw, "0xb200, 0x0010001b,");
                for (i = 0; i < FirTable.fir_h0p5_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h0p5_OSR8[i]);  // 0xb400~0xb42c
                // 0xb108~0xb110 - ignored                              
                for (i = 0; i < FirTable.rxlpf_h0p5_OSR8_2M.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h0p5_OSR8_2M[i]);  // 0xb114~0xb11c
            }
            else if ((H >= 0.5) && (H < 0.75))
            {
                CodeOutHelper.CodeOut(sw, "0xb200, 0x0028001b,");
                for (i = 0; i < FirTable.fir_h0p75_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h0p75_OSR8[i]);// 0xb400~0xb42c
                // 0xb108~0xb110 - ignored
                for (i = 0; i < FirTable.rxlpf_h0p75_OSR8_2M.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h0p75_OSR8_2M[i]); //0xb114~0xb11c
            }
            else if ((H >= 0.75) && (H < 1)) //0.75<=H<1
            {
                CodeOutHelper.CodeOut(sw, "0xb200, 0x0028001b,");
                for (i = 0; i < FirTable.fir_h1_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h1_OSR8[i]);// 0xb400~0xb42c
                // 0xb108~0xb110 - ignored
                for (i = 0; i < FirTable.rxlpf_h1_OSR8_2M.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h1_OSR8_2M[i]);  //0xb114~0xb11c
            }
            else{
                //CodeOutHelper.CodeOut(sw, "WRONG H");
                //res = 1;
                return res;
            }
            
            return res;
        }
    }
}
