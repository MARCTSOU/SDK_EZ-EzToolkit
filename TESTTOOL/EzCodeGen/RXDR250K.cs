using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public class RXDR250K
    {
        public static int Set(StreamWriter sw, double H, out string modemClkReg)
        {
            int i = 0;
            int res = 0;
            modemClkReg = "0x0004, 0x00010104,";
            CodeOutHelper.CodeOut(sw, "0xb304, 0x00000000,");
            if (H > 2)
                CodeOutHelper.CodeOut(sw, "0xb0d0, 0x00000805,");
            CodeOutHelper.CodeOut(sw, "0xb1b4, 0x0000ff10,");
            //CodeOutHelper.CodeOut(sw, "0x1604, 0x1fc01b7d"); //分 H <=1.5 及 H>1.5
            if (H <= 2)
                CodeOutHelper.CodeOut(sw, "0x1604, 0x1fc33b7d,");
            else if ((H > 2) && (H <= 4))
                CodeOutHelper.CodeOut(sw, "0x1604, 0x1fc22b7d,");
            //else
            //    CodeOutHelper.CodeOut(sw, "WRONG H");

            if (H <= 0.5)
                CodeOutHelper.CodeOut(sw, "0xb200, 0x00100017,");
            else if((H > 0.5) && (H <= 4))
                CodeOutHelper.CodeOut(sw, "0xb200, 0x00280017,");
            //else
            //    CodeOutHelper.CodeOut(sw, "WRONG H");
            if ((H > 0) && (H <= 0.5))  //0<H<=0.5
            {
                for (i = 0; i < FirTable.fir_h0p5_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h0p5_OSR8[i]);  // 0xb400~0xb42c                         
                for (i = 0; i < FirTable.rxlpf_h0p5_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h0p5_OSR16[i]);  // 0xb108~0xb110 
            }
            else if ((H > 0.5) && (H <= 0.75))  //0.5<H<=0.75
            {
                for (i = 0; i < FirTable.fir_h0p75_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h0p75_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < FirTable.rxlpf_h0p5_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h0p5_OSR16[i]); // 0xb108~0xb110 
            }
            else if ((H > 0.75) && (H <= 1)) //0.75<H<=1
            {
                for (i = 0; i < FirTable.fir_h1_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h1_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < FirTable.rxlpf_h1_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h1_OSR16[i]);  // 0xb108~0xb110 
            }
            else if ((H > 1) && (H <= 1.25)) // 1<H<=1.25
            {
                for (i = 0; i < FirTable.fir_h1p25_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h1p25_OSR8[i]);// 0xb400~0xb42c                
                for (i = 0; i < FirTable.rxlpf_h1p25_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h1p25_OSR16[i]);  // 0xb108~0xb110 
            }
            else if ((H > 1.25) && (H <= 1.5)) // 1.25<H<=1.5
            {
                for (i = 0; i < FirTable.fir_h1p5_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h1p5_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < FirTable.rxlpf_h1p5_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h1p5_OSR16[i]);  // 0xb108~0xb110 
            }
            else if ((H > 1.5) && (H <= 1.75)) // 1.5<H<=1.75
            {
                for (i = 0; i < FirTable.fir_h1p75_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h1p75_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < FirTable.rxlpf_h1p5_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h1p5_OSR16[i]);  // 0xb108~0xb110 
            }
            else if ((H > 1.75) && (H <= 2)) // 1.75<H<=2
            {
                for (i = 0; i < FirTable.fir_h2_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h2_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < FirTable.rxlpf_h2_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h2_OSR16[i]);  // 0xb108~0xb110 
            }
            else if ((H > 2) && (H <= 2.5)) // 2<H<=2.5
            {
                for (i = 0; i < FirTable.fir_h2_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h2_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < FirTable.rxlpf_h2p5_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h2p5_OSR16[i]);  // 0xb108~0xb110 
            }
            else if ((H > 2.5) && (H <= 3)) // 2.5<H<=3
            {
                for (i = 0; i < FirTable.fir_h2_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h2_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < FirTable.rxlpf_h1_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h1_OSR8[i]);  // 0xb108~0xb110 
            }
            else if ((H > 3) && (H <= 3.5)) // 3<H<=3.5
            {
                for (i = 0; i < FirTable.fir_h2_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h2_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < FirTable.rxlpf_h1_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h1_OSR8[i]);  // 0xb108~0xb110 
            }
            else if ((H > 3.5) && (H <= 4)) // 3.5<H<=4
            {
                for (i = 0; i < FirTable.fir_h4_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h4_OSR16[i]);// 0xb400~0xb42c
                for (i = 0; i < FirTable.rxlpf_h4_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h4_OSR16[i]);  // 0xb108~0xb110 
            }
            else
            {
                //CodeOutHelper.CodeOut(sw, "WRONG H");
                //res = 1;
                return res;
            }
            return res;
        }

    }
}
