using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen
{
    public partial class Form1
    {
        public int DR125KSet(double H)
        {
            int i = 0;
            int res = 0;
            CodeOut("0x0004, 0x00010108,");
            CodeOut("0xb304, 0x00000000,");
            if (H >= 2)
                CodeOut("0xb0d0, 0x00000805,");
            CodeOut("0xb1b4, 0x0000ff10,");
            CodeOut("0x1604, 0x1fc33b7d,");

            if (H <= 0.5)
                CodeOut("0xb200, 0x00100018,");
            else if (H <= 4.5 && H > 0.5)
                CodeOut("0xb200, 0x00280018,");
            else if (H <= 6 && H > 4.5)
                CodeOut("0xb200, 0x00400018,");
            else
                CodeOut("WRONG H");

            if ((H <= 0.5) && (H > 0))  //0<H<=0.5
            {
                for (i = 0; i < fir_h0p5_OSR8.Length; i++)
                    CodeOut(fir_h0p5_OSR8[i]);  // 0xb400~0xb42c                         
                for (i = 0; i < rxlpf_h0p5_OSR16.Length; i++)
                    CodeOut(rxlpf_h0p5_OSR16[i]);  // 0xb108~0xb110 
            }
            else if ((H <= 0.75) && (H > 0.5))  //0.5<H<=0.75
            {
                for (i = 0; i < fir_h0p75_OSR8.Length; i++)
                    CodeOut(fir_h0p75_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h0p5_OSR16.Length; i++)
                    CodeOut(rxlpf_h0p5_OSR16[i]); // 0xb108~0xb110 
            }
            else if ((H <= 1) && (H > 0.75)) //0.75<H<=1
            {
                for (i = 0; i < fir_h1_OSR8.Length; i++)
                    CodeOut(fir_h1_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h1_OSR16.Length; i++)
                    CodeOut(rxlpf_h1_OSR16[i]);  // 0xb108~0xb110 
            }
            else if ((H <= 1.25) && (H > 1)) // 1<H<=1.25
            {
                for (i = 0; i < fir_h1p25_OSR8.Length; i++)
                    CodeOut(fir_h1p25_OSR8[i]);// 0xb400~0xb42c                
                for (i = 0; i < rxlpf_h1p25_OSR16.Length; i++)
                    CodeOut(rxlpf_h1p25_OSR16[i]);  // 0xb108~0xb110 
            }
            else if ((H <= 1.5) && (H > 1.25)) // 1.25<H<=1.5
            {
                for (i = 0; i < fir_h1p5_OSR8.Length; i++)
                    CodeOut(fir_h1p5_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h1p5_OSR16.Length; i++)
                    CodeOut(rxlpf_h1p5_OSR16[i]);  // 0xb108~0xb110 
            }
            else if ((H <= 1.75) && (H > 1.5)) // 1.5<H<=1.75
            {
                for (i = 0; i < fir_h1p75_OSR8.Length; i++)
                    CodeOut(fir_h1p75_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h1p5_OSR16.Length; i++)
                    CodeOut(rxlpf_h1p5_OSR16[i]);  // 0xb108~0xb110 
            }
            else if ((H <= 2) && (H > 1.75)) // 1.75<H<=2
            {
                for (i = 0; i < fir_h2_OSR8.Length; i++)
                    CodeOut(fir_h2_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h2_OSR16.Length; i++)
                    CodeOut(rxlpf_h2_OSR16[i]);  // 0xb108~0xb110 
            }
            else if ((H <= 2.5) && (H > 2)) // 2<H<=2.5
            {
                for (i = 0; i < fir_h2_OSR8.Length; i++)
                    CodeOut(fir_h2_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h2p5_OSR16.Length; i++)
                    CodeOut(rxlpf_h2p5_OSR16[i]);  // 0xb108~0xb110 
            }
            else if ((H <= 3) && (H > 2.5)) // 2.5<H<=3
            {
                for (i = 0; i < fir_h2_OSR8.Length; i++)
                    CodeOut(fir_h2_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h1_OSR8.Length; i++)
                    CodeOut(rxlpf_h1_OSR8[i]);  // 0xb108~0xb110 
            }
            else if ((H <= 3.5) && (H > 3)) // 3<H<=3.5
            {
                for (i = 0; i < fir_h2_OSR8.Length; i++)
                    CodeOut(fir_h2_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h1_OSR8.Length; i++)
                    CodeOut(rxlpf_h1_OSR8[i]);  // 0xb108~0xb110 
            }
            else if ((H <= 4) && (H > 3.5)) // 3.5<H<=4
            {
                for (i = 0; i < fir_h4_OSR16.Length; i++)
                    CodeOut(fir_h4_OSR16[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h4_OSR16.Length; i++)
                    CodeOut(rxlpf_h4_OSR16[i]);  // 0xb108~0xb110 
            }

            else if ((H <= 4.5) && (H > 4)) // 4<H<=4.5
            {
                for (i = 0; i < fir_h4_OSR16.Length; i++)
                    CodeOut(fir_h4_OSR16[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h4p5_OSR16.Length; i++)
                    CodeOut(rxlpf_h4p5_OSR16[i]);  // 0xb108~0xb110 
            }
            else if ((H <= 5) && (H > 4.5)) // 4.5<H<=5
            {
                for (i = 0; i < fir_h5_OSR16.Length; i++)
                    CodeOut(fir_h5_OSR16[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h5_OSR16.Length; i++)
                    CodeOut(rxlpf_h5_OSR16[i]);  // 0xb108~0xb110 
            }
            else if ((H <= 6) && (H > 5)) // 4.5<H<=5
            {
                for (i = 0; i < fir_h5_OSR16.Length; i++)
                    CodeOut(fir_h5_OSR16[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h6_OSR16.Length; i++)
                    CodeOut(rxlpf_h6_OSR16[i]);  // 0xb108~0xb110 
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
