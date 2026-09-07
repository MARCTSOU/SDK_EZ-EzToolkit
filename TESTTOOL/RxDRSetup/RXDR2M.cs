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
        public int DR2MSet(double H)
        {
            int i = 0;
            int res = 0;

            CodeOut("0x0004, 0x00010101,");
            CodeOut("0xb0f0, 0x17342000,");  // OWen add 2025/06/12 改
            CodeOut("0xb304, 0x00000010,");
            CodeOut("0xb1b4, 0x0000ffd8,");
            CodeOut("0x1604, 0x1fc01b7d,");
            if ((H <= 0.5) && (H > 0))
            {
                CodeOut("0xb200, 0x0010001b,");
                for (i = 0; i < fir_h0p5_OSR8.Length; i++)
                    CodeOut(fir_h0p5_OSR8[i]);  // 0xb400~0xb42c
                // 0xb108~0xb110 - ignored                              
                for (i = 0; i < rxlpf_h0p5_OSR8_2M.Length; i++)
                    CodeOut(rxlpf_h0p5_OSR8_2M[i]);  // 0xb114~0xb11c
            }
            else if ((H <= 0.75) && (H > 0.5))
            {
                CodeOut("0xb200, 0x0028001b,");
                for (i = 0; i < fir_h0p75_OSR8.Length; i++)
                    CodeOut(fir_h0p75_OSR8[i]);// 0xb400~0xb42c
                // 0xb108~0xb110 - ignored
                for (i = 0; i < rxlpf_h0p75_OSR8_2M.Length; i++)
                    CodeOut(rxlpf_h0p75_OSR8_2M[i]); //0xb114~0xb11c
            }
            else if ((H <= 1) && (H > 0.75)) //0.75<H<=1
            {
                CodeOut("0xb200, 0x0028001b,");
                for (i = 0; i < fir_h1_OSR8.Length; i++)
                    CodeOut(fir_h1_OSR8[i]);// 0xb400~0xb42c
                // 0xb108~0xb110 - ignored
                for (i = 0; i < rxlpf_h1_OSR8_2M.Length; i++)
                    CodeOut(rxlpf_h1_OSR8_2M[i]);  //0xb114~0xb11c
            }
            else{
                ShowInsLog("WRONG H");
                res = 1;
                return res;
            }
            
            return res;
        }
    }
}
