using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen
{
    public partial class Form1
    {
        public int DR0K625Set(double H)
        {
            int i = 0;
            int res = 0;
            CodeOut("0x0004, 0x000a0a10,");
            CodeOut("0xb304, 0x00000000,");
            if (H >= 2)
                CodeOut("0xb0d0, 0x00000805,");
            CodeOut("0xb1b4, 0x0000fee8,");
            CodeOut("0x1604, 0x1fc33b7d,");

            if (H <= 0.5)
                CodeOut("0xb200, 0x000c001d,");
            else if (H <= 4.5 && H > 0.5)
                CodeOut("0xb200, 0x0024001d,");
            else
                CodeOut("0xb200, 0x003c001d,");

            if ((H <= 0.5) && (H > 0))
            {
                for (i = 0; i < fir_h0p5_OSR8.Length; i++)
                    CodeOut(fir_h0p5_OSR8[i]);
                for (i = 0; i < rxlpf_h0p5_OSR16.Length; i++)
                    CodeOut(rxlpf_h0p5_OSR16[i]);
            }
            else if ((H <= 0.75) && (H > 0.5))
            {
                for (i = 0; i < fir_h0p75_OSR8.Length; i++)
                    CodeOut(fir_h0p75_OSR8[i]);
                for (i = 0; i < rxlpf_h0p5_OSR16.Length; i++)
                    CodeOut(rxlpf_h0p5_OSR16[i]);
            }
            else if ((H <= 1) && (H > 0.75))
            {
                for (i = 0; i < fir_h1_OSR8.Length; i++)
                    CodeOut(fir_h1_OSR8[i]);
                for (i = 0; i < rxlpf_h1_OSR16.Length; i++)
                    CodeOut(rxlpf_h1_OSR16[i]);
            }
            else if ((H <= 1.25) && (H > 1))
            {
                for (i = 0; i < fir_h1p25_OSR8.Length; i++)
                    CodeOut(fir_h1p25_OSR8[i]);
                for (i = 0; i < rxlpf_h1p25_OSR16.Length; i++)
                    CodeOut(rxlpf_h1p25_OSR16[i]);
            }
            else if ((H <= 1.5) && (H > 1.25))
            {
                for (i = 0; i < fir_h1p5_OSR8.Length; i++)
                    CodeOut(fir_h1p5_OSR8[i]);
                for (i = 0; i < rxlpf_h1p5_OSR16.Length; i++)
                    CodeOut(rxlpf_h1p5_OSR16[i]);
            }
            else if ((H <= 1.75) && (H > 1.5))
            {
                for (i = 0; i < fir_h1p75_OSR8.Length; i++)
                    CodeOut(fir_h1p75_OSR8[i]);
                for (i = 0; i < rxlpf_h1p5_OSR16.Length; i++)
                    CodeOut(rxlpf_h1p5_OSR16[i]);
            }
            else if ((H <= 2) && (H > 1.75))
            {
                for (i = 0; i < fir_h2_OSR8.Length; i++)
                    CodeOut(fir_h2_OSR8[i]);
                for (i = 0; i < rxlpf_h2_OSR16.Length; i++)
                    CodeOut(rxlpf_h2_OSR16[i]);
            }
            else if ((H <= 2.5) && (H > 2))
            {
                for (i = 0; i < fir_h2_OSR8.Length; i++)
                    CodeOut(fir_h2_OSR8[i]);
                for (i = 0; i < rxlpf_h2p5_OSR16.Length; i++)
                    CodeOut(rxlpf_h2p5_OSR16[i]);
            }
            else if ((H <= 3) && (H > 2.5))
            {
                for (i = 0; i < fir_h2_OSR8.Length; i++)
                    CodeOut(fir_h2_OSR8[i]);
                for (i = 0; i < rxlpf_h1_OSR8.Length; i++)
                    CodeOut(rxlpf_h1_OSR8[i]);
            }
            else if ((H <= 3.5) && (H > 3))
            {
                for (i = 0; i < fir_h2_OSR8.Length; i++)
                    CodeOut(fir_h2_OSR8[i]);
                for (i = 0; i < rxlpf_h1_OSR8.Length; i++)
                    CodeOut(rxlpf_h1_OSR8[i]);
            }
            else if ((H <= 4) && (H > 3.5))
            {
                for (i = 0; i < fir_h4_OSR16.Length; i++)
                    CodeOut(fir_h4_OSR16[i]);
                for (i = 0; i < rxlpf_h4_OSR16.Length; i++)
                    CodeOut(rxlpf_h4_OSR16[i]);
            }
            else if ((H <= 4.5) && (H > 4))
            {
                for (i = 0; i < fir_h4_OSR16.Length; i++)
                    CodeOut(fir_h4_OSR16[i]);
                for (i = 0; i < rxlpf_h4p5_OSR16.Length; i++)
                    CodeOut(rxlpf_h4p5_OSR16[i]);
            }
            else if ((H <= 5) && (H > 4.5))
            {
                for (i = 0; i < fir_h5_OSR16.Length; i++)
                    CodeOut(fir_h5_OSR16[i]);
                for (i = 0; i < rxlpf_h5_OSR16.Length; i++)
                    CodeOut(rxlpf_h5_OSR16[i]);
            }
            //else if ((H <= 6) && (H > 5))   // Owen: 2025/08/28 將H改成無上限
            else if (H > 5)  // Owen: 2025/08/28 將H改成無上限
            {
                for (i = 0; i < fir_h5_OSR16.Length; i++)
                    CodeOut(fir_h5_OSR16[i]);
                for (i = 0; i < rxlpf_h6_OSR16.Length; i++)
                    CodeOut(rxlpf_h6_OSR16[i]);
            }
            else
            {
                CodeOut("WRONG H");
                //res = 1;
                return res;
            }
            return res;
        }
        

    }
}
