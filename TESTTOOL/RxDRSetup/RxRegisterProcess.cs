using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ER41xxTestTool
{
    public partial class MainForm : Form
    {
        public int RxRegisterGenerate(double dataRate, double H)
        {
            int res = 0;
            // dataRate 已 * BPSOffset
            switch (dataRate)
            {
                case 2000000: // 2M
                    res = DR2MSet(H);                                       
                    break;

                case 1000000: //1M
                    res = DR1MSet(H);
                    break;

                case 500000:  // 500K
                    res = DR500KSet(H);
                    break;

                case 250000:  // 250K
                    res = DR250KSet(H);
                    break;

                case 200000: // 200K
                    res = DR200KSet(H);
                    break;

                case 125000:  // 125K
                    res = DR125KSet(H);
                    break;

                case 100000:  // 100K
                    res = DR100KSet(H);
                    break;

                case 62500: // 62.5K
                    res = DR62K5Set(H);
                    break;

                case 50000: // 50K
                    res = DR50KSet(H);
                    break;

                case 40000: // 40K
                    res = DR40KSet(H);
                    break;

                case 31250: // 31.25K
                    res = DR31K25Set(H);
                    break;

                case 25000: // 25K
                    res = DR25KSet(H);
                    break;

                case 20000: // 20K
                    res = DR20KSet(H);
                    break;

                case 15625: // 15.625K
                    res = DR15K625Set(H);
                    break;

                case 12500:    //12.5K
                    res = DR12K5Set(H);
                    break;

                case 10000: // 10K
                    res = DR10KSet(H);
                    break;

                case 7812.5:  //7.8125K
                    res = DR7K8125Set(H);
                    break;

                case 6250: //6.25K
                    res = DR6K25Set(H);
                    break;

                case 5000: //5K
                    res = DR5KSet(H);
                    break;

                case 3125: //3.125K
                    res = DR3K125Set(H);
                    break;

                case 2500:  // 2.5K
                    res = DR2K5Set(H);
                    break;

                case 1250:  // 1.25K
                    res = DR1K25Set(H);
                    break;

                case 1000:  // 1K
                    res = DR1KSet(H);
                    break;

                case 625:  // 0.625K
                    res = DR0K625Set(H);
                    break;
                default:
                    ShowInsLog("WRONG DataRate\r\n");
                    res = 1;
                    break;
            }
            return res;
        }
       
        
    }


}
