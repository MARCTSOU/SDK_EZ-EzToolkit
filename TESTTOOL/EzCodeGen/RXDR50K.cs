using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ER41xxTestTool
{
    public class RXDR50K
    {
        public static int Set(StreamWriter sw, double H, out string modemClkReg)
        {
            int i = 0;
            int res = 0;
            modemClkReg = "0x0004, 0x0001020a,";
            CodeOutHelper.CodeOut(sw, "0xb304, 0x00000000,");
            if (H > 2)
                CodeOutHelper.CodeOut(sw, "0xb0d0, 0x00000805,");
            CodeOutHelper.CodeOut(sw, "0xb1b4, 0x0000fee8,");
            CodeOutHelper.CodeOut(sw, "0x1604, 0x1fc33b7d,");

            if (H <= 0.5)
                CodeOutHelper.CodeOut(sw, "0xb200, 0x000c0018,");
            else if (H > 0.5 && H <= 4.5)
                CodeOutHelper.CodeOut(sw, "0xb200, 0x00240018,");
            else if (H > 4.5 && H <= 6)
                CodeOutHelper.CodeOut(sw, "0xb200, 0x003c0018,");
            //else
            //    CodeOutHelper.CodeOut(sw, "WRONG H");
            if ((H > 0) && (H <= 0.5))
            {
                for (i = 0; i < FirTable.fir_h0p5_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h0p5_OSR8[i]);
                for (i = 0; i < FirTable.rxlpf_h0p5_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h0p5_OSR16[i]);
            }
            else if ((H > 0.5) && (H <= 0.75))
            {
                for (i = 0; i < FirTable.fir_h0p75_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h0p75_OSR8[i]);
                for (i = 0; i < FirTable.rxlpf_h0p5_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h0p5_OSR16[i]);
            }
            else if ((H > 0.75) && (H <= 1))
            {
                for (i = 0; i < FirTable.fir_h1_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h1_OSR8[i]);
                for (i = 0; i < FirTable.rxlpf_h1_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h1_OSR16[i]);
            }
            else if ((H > 1) && (H <= 1.25))
            {
                for (i = 0; i < FirTable.fir_h1p25_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h1p25_OSR8[i]);
                for (i = 0; i < FirTable.rxlpf_h1p25_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h1p25_OSR16[i]);
            }
            else if ((H > 1.25) && (H <= 1.5))
            {
                for (i = 0; i < FirTable.fir_h1p5_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h1p5_OSR8[i]);
                for (i = 0; i < FirTable.rxlpf_h1p5_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h1p5_OSR16[i]);
            }
            else if ((H > 1.5) && (H <= 1.75))
            {
                for (i = 0; i < FirTable.fir_h1p75_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h1p75_OSR8[i]);
                for (i = 0; i < FirTable.rxlpf_h1p5_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h1p5_OSR16[i]);
            }
            else if ((H > 1.75) && (H <= 2))
            {
                for (i = 0; i < FirTable.fir_h2_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h2_OSR8[i]);
                for (i = 0; i < FirTable.rxlpf_h2_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h2_OSR16[i]);
            }
            else if ((H > 2) && (H <= 2.5))
            {
                for (i = 0; i < FirTable.fir_h2_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h2_OSR8[i]);
                for (i = 0; i < FirTable.rxlpf_h2p5_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h2p5_OSR16[i]);
            }
            else if ((H > 2.5) && (H <= 3))
            {
                for (i = 0; i < FirTable.fir_h2_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h2_OSR8[i]);
                for (i = 0; i < FirTable.rxlpf_h1_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h1_OSR8[i]);
            }
            else if ((H > 3) && (H <= 3.5))
            {
                for (i = 0; i < FirTable.fir_h2_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h2_OSR8[i]);
                for (i = 0; i < FirTable.rxlpf_h1_OSR8.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h1_OSR8[i]);
            }
            else if ((H > 3.5) && (H <= 4))
            {
                for (i = 0; i < FirTable.fir_h4_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h4_OSR16[i]);
                for (i = 0; i < FirTable.rxlpf_h4_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h4_OSR16[i]);
            }
            else if ((H > 4) && (H <= 4.5))
            {
                for (i = 0; i < FirTable.fir_h4_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h4_OSR16[i]);
                for (i = 0; i < FirTable.rxlpf_h4p5_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h4p5_OSR16[i]);
            }
            else if ((H > 4.5) && (H <= 5))
            {
                for (i = 0; i < FirTable.fir_h5_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h5_OSR16[i]);
                for (i = 0; i < FirTable.rxlpf_h5_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h5_OSR16[i]);
            }
            else if ((H > 5) && (H <= 6))
            {
                for (i = 0; i < FirTable.fir_h5_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.fir_h5_OSR16[i]);
                for (i = 0; i < FirTable.rxlpf_h6_OSR16.Length; i++)
                    CodeOutHelper.CodeOut(sw, FirTable.rxlpf_h6_OSR16[i]);
            }
            else
            {
             //   CodeOutHelper.CodeOut(sw, "WRONG H");
                //res = 1;
                return res;
            }
            return res;
        }

    }
}
