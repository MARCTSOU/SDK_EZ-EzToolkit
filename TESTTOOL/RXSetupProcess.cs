using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ER41xxTestTool
{
    public partial class MainForm : Form
    {
        /*
        double rxdataRate = 0.0;
        double dataRate_offset = 0.0;
        double rxfdev = 0.0;
        double xtal = 0.0;
        uint crystal_HZ = 0;
        double Frequency_MHz = 0.0;
        */
        public string[] DR_xtal40m = new string[]{"2000000","1000000","500000", "250000", "200000", "125000", "100000", "62500", "50000", "40000", "31250", "25000", "20000", "15625",
                                                  "12500", "10000", "7812.5", "6250", "5000", "3125","2500", "1250", "1000", "625"};

        public string[] DR_xtal384m = new string[]{"1920000","960000","480000", "240000", "192000", "120000", "96000", "60000", "48000", "38400", "30000", "24000", "19200","15000",
                                                  "12000", "9600", "7500", "6000", "4800", "3000", "2400", "1200", "960", "600"};

        public string[] DR_xtal384m_SHORT1 = new string[] { "96000", "38400", "9600", "4800" };


        //# ================================== #
        //#          LPF  information          #  // excel file 用小寫: rxlpf_h0p75_OSR8
        //# ================================== #
        // for 2M 
        public string[] rxlpf_h0p5_OSR8_2M = { "0xb114, 0xbff00400,", "0xb118, 0xd5185ff7,", "0xb11c, 0x0000019c," };
        public string[] rxlpf_h0p75_OSR8_2M = { "0xb114, 0xc0000000,", "0xb118, 0x72881fff,", "0xb11c, 0x00000190," };
        public string[] rxlpf_h1_OSR8_2M = { "0xb114, 0x02000000,", "0xb118, 0x8287fef8,", "0xb11c, 0x00000192," };

        public string[] rxlpf_h0p5_OSR8 = { "0xb108, 0xbff00400,", "0xb10c, 0xd5185ff7,", "0xb110, 0x0000019c," };
        //public string[] rxlpf_h0p5_OSR16 = { "0xb108, 0x3ffffc00,", "0xb10C, 0x9418a308,", "0xb110, 0x00000192," }; // before 2024/12/09
        public string[] rxlpf_h0p5_OSR16 = { "0xb108, 0x7feef7de,", "0xb10C, 0xf7bd6820,", "0xb110, 0x0000019e," };  // after 2024/12/09
        public string[] rxlpf_h0p75_OSR8 = { "0xb108, 0xc0000000,", "0xb10C, 0x72881fff,", "0xb110, 0x00000190," };
        public string[] rxlpf_h1_OSR8 = { "0xb108, 0x02000000,", "0xb10C, 0x8287fef8,", "0xb110, 0x00000192," };
        public string[] rxlpf_h1_OSR16 = { "0xb108, 0x3fff7fff,", "0xb10c, 0x941ca308,", "0xb110, 0x00000194," };
        public string[] rxlpf_h1p25_OSR8 = { "0xb108, 0x42000000,", "0xb10C, 0x8287def8,", "0xb110, 0x00000194," };

        public string[] rxlpf_h1p25_OSR16 = { "0xb108, 0xfdef7fe2,", "0xb10C, 0xa49c8207,", "0xb110, 0x00000196," };
        public string[] rxlpf_h1p5_OSR8 = { "0xb108, 0x420f8000,", "0xb10C, 0x927fdf00,", "0xb110, 0x00000196," };
        public string[] rxlpf_h1p5_OSR16 = { "0xb108, 0xbff00000,", "0xb10C, 0xc49860ff,", "0xb110, 0x0000019a," };
        public string[] rxlpf_h1p75_OSR8 = { "0xb108, 0x41f00000,", "0xb10C, 0x927fdf08,", "0xb110, 0x00000198," };

        public string[] rxlpf_h2_OSR16 = { "0xb108, 0xbe000000,", "0xb10c, 0xd5145ff7,", "0xb110, 0x0000019c," };
        //public string[] rxlpf_h2p5_OSR16  = { "0xb108, 0xc010841f,", "0xb10C, 0x72881fff,", "0xb110, 0x00000190," };  // before 2024/12/24; V2.2.4, V2.2.5
        //public string[] rxlpf_h4_OSR16    = { "0xb108, 0x420ffc01,", "0xb10C, 0x9203de00,", "0xb110, 0x00000196," };  // before 2024/12/24; V2.2.4, V2.2.5
        //public string[] rxlpf_h4p5_OSR16  = { "0xb108, 0x41ff8000,", "0xb10C, 0x927fdf08,", "0xb110, 0x00000198," };  // before 2024/12/24; V2.2.4, V2.2.5
        //public string[] rxlpf_h5_OSR16    = { "0xb108, 0x7ff00420,", "0xb10C, 0xa1fbc008,", "0xb110, 0x0000019a," };  // before 2024/12/24; V2.2.4, V2.2.5
        //public string[] rxlpf_h6_OSR16    = { "0xb108, 0xfe0083e0,", "0xb10C, 0xa0f7e20f,", "0xb110, 0x0000019e," };  // before 2024/12/24; V2.2.4, V2.2.5

        public string[] rxlpf_h2p5_OSR16 = { "0xb108, 0xc010841f,", "0xb10C, 0x72881fff,", "0xb110, 0x00000150," };  // V2.2.6
        public string[] rxlpf_h4_OSR16 = { "0xb108, 0x420ffc01,", "0xb10C, 0x9203de00,", "0xb110, 0x00000156," };  // V2.2.6
        public string[] rxlpf_h4p5_OSR16 = { "0xb108, 0x41ff8000,", "0xb10C, 0x927fdf08,", "0xb110, 0x00000158," };  // V2.2.6
        public string[] rxlpf_h5_OSR16 = { "0xb108, 0x7ff00420,", "0xb10C, 0xa1fbc008,", "0xb110, 0x0000015a," };  // V2.2.6
        public string[] rxlpf_h6_OSR16 = { "0xb108, 0xfe0083e0,", "0xb10C, 0xa0f7e20f,", "0xb110, 0x0000015e," };  // V2.2.6

        // For Data
        public string[] RX_2MBPS_Array = {  "0x0004, 0x00010101,","0x1604, 0x1fc01b7d,","0xb200, 0x0008001a ","0xb1b4, 0x0000ffb0,",
                                            "0xb304, 0x00000010,","0xb1a8, 0x00000050,","0xb0d0, 0x0000083f,","0xb19c, 0x00015f04,",
                                            "0xb208, 0x06057433,","0xb114, 0xbff00400,","0xb118, 0xd5185ff7,","0xb11c, 0x0000019c,",
                                            "0xb400, 0x0000002a,","0xb404, 0xfff6001c,","0xb408, 0xff07ff9d,","0xb40c, 0xfe5dfe74,",
                                            "0xb410, 0x0145ff40,","0xb414, 0x05eb03e9,","0xb418, 0x021405b1,","0xb41c, 0xf373fb49,",
                                            "0xb420, 0xf065ee62,","0xb424, 0x12e4fc87,","0xb428, 0x4cfe2fe8,","0xb42c, 0x6a796287,",};
        // 從 0xb400 後 = fir_h0p5_OSR8

        public string[] RX_1MBPSh05_Array = {"0x0004, 0x00020101,","0xb1b4, 0x0000ff60,","0xb304, 0x00000010,","0xb1a8, 0x00000050,",
                                            "0xb0d0, 0x0000083f,","0xb19c, 0x00015f04,","0x1988, 0x010f4240,","0x1604, 0x1fc01b7d,",
                                            "0xb200, 0x00100018,","0xb108, 0xbff00400,","0xb10c, 0xd5185ff7,","0xb110, 0x0000019c,",
                                            "0xb400, 0x0000002a,","0xb404, 0xfff6001c,","0xb408, 0xff07ff9d,","0xb40c, 0xfe5dfe74,",
                                            "0xb410, 0x0145ff40,","0xb414, 0x05eb03e9,","0xb418, 0x021405b1,","0xb41c, 0xf373fb49,",
                                            "0xb420, 0xf065ee62,","0xb424, 0x12e4fc87,","0xb428, 0x4cfe2fe8,","0xb42c, 0x6a796287,",};

        public string[] RX_1MBPSh1_Array = {
            "0x0004, 0x00020101,","0xb1b4, 0x0000ff60,",
            "0xb304, 0x00000010,","0xb1a8, 0x00000050,",
            "0xb0d0, 0x0000083f,","0xb19c, 0x00015f04,",
            "0x1988, 0x010f4240,","0xb108, 0x02000000,",
            "0xb10C, 0x8287fef8,","0xb110, 0x00000192,",
            "0xb400, 0x0000002a,","0xb404, 0xff92ffb0,",
            "0xb408, 0x0077ffc4,","0xb40c, 0x01b7015b,",
            "0xb410, 0xfeea00de,","0xb414, 0xfcfdfd0e,",
            "0xb418, 0x03ccff9b,","0xb41c, 0x04ca068d,",
            "0xb420, 0xf59cfdf7,","0xb424, 0xf9d5f243,",
            "0xb428, 0x27930d76,","0xb42c, 0x46b73ded,",};

        //# ================================== #
        //#          CHFILT  information       #   // = fir_h0p5_OSR8
        //# ================================== #
        // VERSION2 at 2024/11/21
        public string[] fir_h0p5_OSR8 = { "0xb400, 0x0000002a,", "0xb404, 0xfff6001c,", "0xb408, 0xff07ff9d,", "0xb40c, 0xfe5dfe74,", "0xb410, 0x0145ff40,", "0xb414, 0x05eb03e9,", "0xb418, 0x021405b1,", "0xb41c, 0xf373fb49,", "0xb420, 0xf065ee62,", "0xb424, 0x12e4fc87,", "0xb428, 0x4cfe2fe8,", "0xb42c, 0x6a796287," };
        public string[] fir_h0p75_OSR8 = { "0xb400, 0x0000002a,", "0xb404, 0xffe5ffb8,", "0xb408, 0x00b5003d,", "0xb40c, 0x006300e4,", "0xb410, 0xfdf4ff31,", "0xb414, 0xff4cfdbd,", "0xb418, 0x04eb0243,", "0xb41c, 0x010e04f5,", "0xb420, 0xf49dfa56,", "0xb424, 0xfeadf4e6,", "0xb428, 0x27f7116c,", "0xb42c, 0x416d3a59," };
        public string[] fir_h1_OSR8 = { "0xb400, 0x0000002a,", "0xb404, 0xff92ffb0,", "0xb408, 0x0077ffc4,", "0xb40c, 0x01b7015b,", "0xb410, 0xfeea00de,", "0xb414, 0xfcfdfd0e,", "0xb418, 0x03ccff9b,", "0xb41c, 0x04ca068d,", "0xb420, 0xf59cfdf7,", "0xb424, 0xf9d5f243,", "0xb428, 0x27930d76,", "0xb42c, 0x46b73ded," };
        public string[] fir_h1p25_OSR8 = { "0xb400, 0x0000002a,", "0xb404, 0x007c0058,", "0xb408, 0xff370022,", "0xb40c, 0xfee7fe74,", "0xb410, 0x027300b9,", "0xb414, 0xfed101f7,", "0xb418, 0xfc0afb8f,", "0xb41c, 0x07760163,", "0xb420, 0xfe5b076a,", "0xb424, 0xf059f26b,", "0xb428, 0x239c01c7,", "0xb42c, 0x537f455d," };
        public string[] fir_h1p5_OSR8 = { "0xb400, 0x0000002a,", "0xb404, 0x005b0008,", "0xb408, 0x0061009e,", "0xb40c, 0xfeafff7a,", "0xb410, 0x012eff39,", "0xb414, 0x015802ab,", "0xb418, 0xfb0efd80,", "0xb41c, 0x0521fe07,", "0xb420, 0x028f08e9,", "0xb424, 0xee64f508,", "0xb428, 0x2102fd08,", "0xb42c, 0x587447be," };
        public string[] fir_h1p75_OSR8 = { "0xb400, 0x0000002a,", "0xb404, 0x00900006,", "0xb408, 0x027501a1,", "0xb40c, 0xff6201c1,", "0xb410, 0xfe5efd66,", "0xb414, 0x042101f9,", "0xb418, 0xfb640133,", "0xb41c, 0x00bef9ef,", "0xb420, 0x07fb0967,", "0xb424, 0xecb0f993,", "0xb428, 0x1cf5f69a,", "0xb42c, 0x5f424ac0," };
        public string[] fir_h2_OSR8 = { "0xb400, 0x0000002a,", "0xb404, 0xff73ffe2,", "0xb408, 0xff53ff02,", "0xb40c, 0x0178009c,", "0xb410, 0xfdf10043,", "0xb414, 0x018efdfb,", "0xb418, 0x00d30433,", "0xb41c, 0xfab0fa65,", "0xb420, 0x0b56047c,", "0xb424, 0xee770167,", "0xb428, 0x1636ef6d,", "0xb42c, 0x680c4da6," };
        public string[] fir_h4_OSR16 = { "0xb400, 0x0000002a,", "0xb404, 0xffdefffd,", "0xb408, 0x003effdd,", "0xb40c, 0xff900063,", "0xb410, 0x00b1ff23,", "0xb414, 0xfeff01ae,", "0xb418, 0x015afd06,", "0xb41c, 0xfe4c0500,", "0xb420, 0x0207f7d1,", "0xb424, 0xfdb50da6,", "0xb428, 0x0277e681,", "0xb42c, 0x7d7a50eb," };
        public string[] fir_h5_OSR16 = { "0xb400, 0x0000002a,", "0xb404, 0xffee0001,", "0xb408, 0x002bfff4,", "0xb40c, 0xffa5fffd,", "0xb410, 0x007d0045,", "0xb414, 0xffb9ff2b,", "0xb418, 0xff58019b,", "0xb41c, 0x028ffdd6,", "0xb420, 0xfab001ae,", "0xb424, 0x0852010d,", "0xb428, 0xf551f791,", "0xb42c, 0x4b9426db," };

        public void CodeOutTitle(double txdataRate, double rxdataRate, double xtal, double txfdev, double rxfdev, double txH, double rxH)
        {
            ShowInsLog("//Crystal = " + xtal + "MHz\r\n");
            ShowInsLog("//Frequency = " + paramDvbBandCombo.Text + "Hz\r\n");

            ShowInsLog("//RX_DateRate = " + rxdataRate + "bps\r\n");
            ShowInsLog("//RX_FreqDev = " + rxfdev + "Hz\r\n");
            ShowInsLog("//RX_H = " + Math.Round(rxH, 4)+ "\r\n");
            ShowInsLog("\r\n");
        }

        public void CodeOut(string input)
        {
            //ShowInsLog(str+"\r\n");
            //作法
            //先去掉結尾多餘的逗號,。
            //用 Split(',') 分割字串。
            //Trim() 掉可能的空白。
            //用 "rr " + string.Join(" ", parts) + "\r" 組合
            string cmd = "";
            input = input.Trim().TrimEnd(',');
            string[] parts = input.Split(',');
            // 去掉空白
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim();
            }
            cmd = "wr " + string.Join(" ", parts) + "\r";
            //ShowInsLog(cmd+"\n");
            if (serialPort1.IsOpen)
            {
                serialPort1.Write(cmd);
                Thread.Sleep(50);
            }
            else
                ShowInsLog("COM PORT ERROR!\r\n");
        }
        public void ShowMesgBox(string str)
        {
            str = "H=2*Freq.Derv/ Datarate\r\n" + str;
            MessageBox.Show(str, "WARNING");
        }
        public double CheckDataRate(double txdataRate, double dataRate, double dataRate_offset, double xtal, double txfdev, double fdev, double txH, double H)
        {
            double newH = 0.0;
            string str = "";

            switch (dataRate_offset)
            {
                case 2000000:    //2M
                    if ((H > 0) && (H <= 0.5))  // 202601/16前是 H<=1
                        newH = H;
                    else
                    {
                        newH = 0.5;    // 202601/16前是 H=1
                        logResultText.Text += "RX Frequency Derivation: " + fdev + "->";
                        fdev = (newH * dataRate / 2)/1000;
                        paramDeviationText.Text = fdev.ToString();
                        logResultText.Text += fdev + "kbps\r\n";
                        str = "Now H=" + H + "\r\nBut H need to be 0<H<="+ newH+"\r\nHere the setting is H="+ newH+"\r\nwhen FreqDev=" + fdev+";\r\nPress START again";
                        ShowMesgBox(str);
                    }
                    break;

                case 1000000:    //1M
                    if (H > 0 && H <= 1)     // 202601/16前是 H<=2
                        newH = H;
                    else
                    {                         
                        newH = 1;      // 202601/16前是 H=2
                        logResultText.Text += "RX Frequency Derivation: " + fdev + "->";
                        fdev = (newH * dataRate / 2) / 1000;
                        paramDeviationText.Text = fdev.ToString();
                        logResultText.Text += fdev + "kbps\r\n";
                        str = "Now H=" + H + "\r\nBut H need to be 0<H<=" + newH + "\r\nHere the setting is H=" + newH + "\r\nwhen FreqDev=" + fdev + ";\r\nPress START again";
                        ShowMesgBox(str);
                    }
                    break;

                case 500000:
                    if (H > 0 && H <= 3)
                        newH = H;
                    else
                    {
                        newH = 3;
                        logResultText.Text += "RX Frequency Derivation: " + fdev + "->";
                        fdev = (newH * dataRate / 2) / 1000;
                        paramDeviationText.Text = fdev.ToString();
                        logResultText.Text += fdev + "kbps\r\n";
                        str = "Now H=" + H + "\r\nBut H need to be 0<H<=3\r\nHere the setting is H=3\r\nwhen FreqDev=" + fdev;
                        ShowMesgBox(str);
                    }
                    break;

                case 250000:
                    if (H > 0 && H <= 4)
                        newH = H;
                    else
                    {
                        newH = 4;
                        logResultText.Text += "RX Frequency Derivation: " + fdev + "->";
                        fdev = (newH * dataRate / 2) / 1000;
                        paramDeviationText.Text = fdev.ToString();
                        logResultText.Text += fdev + "kbps\r\n";
                        str = "Now H=" + H + "\r\nBut H need to be 0<H<=4\r\nHere the setting is H=4\r\nwhen FreqDev=" + fdev;
                        ShowMesgBox(str);
                    }
                    break;

                case 200000:
                    if (H > 0 && H <= 4)
                        newH = H;
                    else
                    {
                        newH = 4;
                        logResultText.Text += "RX Frequency Derivation: " + fdev + "->";
                        fdev = (newH * dataRate / 2) / 1000;
                        paramDeviationText.Text = fdev.ToString();
                        logResultText.Text += fdev + "kbps\r\n";
                        str = "Now H=" + H + "\r\nBut H need to be 0<H<=4\r\nHere the setting is H=4\r\nwhen FreqDev=" + fdev;
                        ShowMesgBox(str);
                    }
                    break;

                default:
                    if (H > 0 && H <= 6)
                        newH = H;
                    else
                    {
                        newH = 6;
                        logResultText.Text += "RX Frequency Derivation: " + fdev + "->";
                        fdev = (newH * dataRate / 2) / 1000;
                        paramDeviationText.Text = fdev.ToString();
                        logResultText.Text += fdev + "kbps\r\n";
                        str = "Now H=" + H + "\r\nBut H need to be 0<H<=6\r\nHere the setting is H=6\r\nwhen FreqDev=" + fdev;
                        ShowMesgBox(str);
                    }
                    break;
            }

            //CodeOutTitle(txdataRate, dataRate, xtal, txfdev, fdev, txH, newH);
            return newH;

        }
        public bool RxTableSetup()
        {
            bool res = false;
            int generateReturn = 0;
            double new_rxH = 0.0;
            double xtal = Convert.ToDouble(paramXtalCombo.Text);    // 40 or 38.4, 依 XTAL 下拉選單
            double dataRate_offset = 0.0;
            double BPSOffset = xtal * 1000000 / 40000000;
            double rxdataRate = (double.Parse(paramDataRateCombo.Text)) * 1000;
            double rxfdev = (Convert.ToDouble(paramDeviationText.Text)) * 1000;
            double rxH = 2 * rxfdev / rxdataRate;
            dataRate_offset = rxdataRate / BPSOffset;  // datarate用Rx的
            new_rxH = CheckDataRate(rxdataRate, rxdataRate, dataRate_offset, xtal, rxfdev, rxfdev, rxH, rxH);    // datarate用Rx的
            if (new_rxH != rxH)
            {
                return false;
            }
            else
            {
                generateReturn = RxRegisterGenerate(dataRate_offset, new_rxH);    // Version_2
                if (generateReturn == 1)
                    res = false;
                else
                    res = true;
            }
            return res;
        }
    }
}
