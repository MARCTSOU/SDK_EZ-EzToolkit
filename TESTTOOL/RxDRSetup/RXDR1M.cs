using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ER41xxTestTool
{
    public partial class MainForm : Form
    {
        public int DR1MSet(double H)
        {
            int i = 0;
            int res = 0;
            CodeOut("0x0004, 0x00010102,");
            CodeOut("0xb0f0, 0x17342000,");  // OWen add 2025/06/12 改 V2.2.8
            CodeOut("0xb304, 0x00000010,");
            CodeOut("0xb1b4, 0x0000ffb0,");
            CodeOut("0x1604, 0x1fc01b7d,");
            if ((H <= 0.5) && (H > 0))  //0<H<=0.5
            {
                CodeOut("0xb200, 0x0010001a,");
                for (i = 0; i < fir_h0p5_OSR8.Length; i++)
                    CodeOut(fir_h0p5_OSR8[i]);  // 0xb400~0xb42c                         
                for (i = 0; i < rxlpf_h0p5_OSR8.Length; i++)
                    CodeOut(rxlpf_h0p5_OSR8[i]);  // 0xb108~0xb110 
            }
            else if ((H <= 0.75) && (H > 0.5))  //0.5<H<=0.75
            {
                CodeOut("0xb200, 0x0028001a,");
                for (i = 0; i < fir_h0p75_OSR8.Length; i++)
                    CodeOut(fir_h0p75_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h0p75_OSR8.Length; i++)
                    CodeOut(rxlpf_h0p75_OSR8[i]); // 0xb108~0xb110 
            }
            else if ((H <= 1) && (H > 0.75)) //0.75<H<=1
            {
                CodeOut("0xb200, 0x0028001a,");
                for (i = 0; i < fir_h1_OSR8.Length; i++)
                    CodeOut(fir_h1_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h1_OSR8.Length; i++)
                    CodeOut(rxlpf_h1_OSR8[i]);  // 0xb108~0xb110 
            }
            else if ((H <= 1.25) && (H > 1)) // 1<H<=1.25
            {
                CodeOut("0xb200, 0x0028001a,");
                for (i = 0; i < fir_h1p25_OSR8.Length; i++)
                    CodeOut(fir_h1p25_OSR8[i]);// 0xb400~0xb42c                
                for (i = 0; i < rxlpf_h1p25_OSR8.Length; i++)
                    CodeOut(rxlpf_h1p25_OSR8[i]);  // 0xb108~0xb110 
            }
            else if ((H <= 1.5) && (H > 1.25)) // 1.25<H<=1.5
            {
                CodeOut("0xb200, 0x0028001a,");
                for (i = 0; i < fir_h1p5_OSR8.Length; i++)
                    CodeOut(fir_h1p5_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h1p5_OSR8.Length; i++)
                    CodeOut(rxlpf_h1p5_OSR8[i]);  // 0xb108~0xb110 
            }
            else if ((H <= 1.75) && (H > 1.5)) // 1.5<H<=1.75
            {
                CodeOut("0xb200, 0x0028001a,");
                for (i = 0; i < fir_h1p75_OSR8.Length; i++)
                    CodeOut(fir_h1p75_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h1p75_OSR8.Length; i++)
                    CodeOut(rxlpf_h1p75_OSR8[i]);  // 0xb108~0xb110 
            }
            else if ((H <= 2) && (H > 1.75)) // 1.75<H<=2
            {
                CodeOut("0xb200, 0x0028001a,");
                for (i = 0; i < fir_h2_OSR8.Length; i++)
                    CodeOut(fir_h2_OSR8[i]);// 0xb400~0xb42c
                for (i = 0; i < rxlpf_h1p75_OSR8.Length; i++)
                    CodeOut(rxlpf_h1p75_OSR8[i]);  // 0xb108~0xb110 
            }
            else
            {
                ShowInsLog("WRONG H");
                res = 1;
                return res;
            }
            
            return res;
        }
    }
}
