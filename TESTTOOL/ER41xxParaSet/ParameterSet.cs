using System;
using System.IO;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.Office.Interop.Excel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace ER41xxTestTool
{
    public partial class MainForm : Form, IView
    {
        int capio = 33;
        int capioFromIni = 0;
        public double freqLimit1_Start = 778;
        public double freqLimit1_Stop = 1020;
        //----------------------------------
        public double freqLimit2_Start = 389;
        public double freqLimit2_Stop = 510;
        //----------------------------------
        public double freqLimit3_Start = 194.5;
        public double freqLimit3_Stop = 255;
        //----------------------------------
        public double freqLimit4_Start = 97.25;
        public double freqLimit4_Stop = 127.5;
        //----------------------------------

        public int FindTableCap(string str)
        {
            int cap = 0;
            string[] t = str.Split(',');
            // xtrom 11 回傳 %d,\r\nOK\r\n
            // t[0] = orgCapioValue = Read eeprom addr:%d
            try
            {
                cap = Convert.ToInt32(t[0]);
            }
            catch (Exception ex)
            {
                cap = 0;
                ShowInsLog("Table CapIO is Wrong!\r\n");
            }
            //for (int i = 0; i < t.Length; i++)
            //    ShowInsLog(t[i] + "    ");

            return cap;
        }

        public int ReadTableCap() //Owen 讀取 EEprom 內的值
        {
            int cap = 0;
            string scmd = "xtrom 11 37 1\r";
            string strTmp = "";
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine(scmd);
                Thread.Sleep(100);
                strTmp = serialPort1.ReadExisting();
                //ShowInsLog(strTmp + "\r\n");
                if (!string.IsNullOrEmpty(strTmp))
                {
                    bool isContains = strTmp.Contains(',');
                    if (isContains)
                        cap = FindTableCap(strTmp);
                    else
                        cap = 0;
                }
            }
            else
                ShowInsLog("COM PORT ERROR!\r\n");
            return cap;
        }

        public bool CheckFrequencyLimit(double freqInput)
        {
            bool res = false;
            if ((freqInput>=(freqLimit1_Start) && freqInput <= (freqLimit1_Stop)) || 
                (freqInput >= (freqLimit2_Start) && freqInput <= (freqLimit2_Stop)) ||
                (freqInput >= (freqLimit3_Start) && freqInput <= (freqLimit3_Stop)) ||
                (freqInput >= (freqLimit4_Start) && freqInput <= (freqLimit4_Stop)))
            {
                res = true;
            }
            return res;
        }

        public bool setCapio(int src)
        {
            bool res = false;
            
            string input = "";
            string errLog = "";
            string cmd = "";
            try
            {
                input = this.capioStr.Text.Trim(); // 取出字串並去掉前後空白
                if (int.TryParse(input, out capio))
                {
                    // 成功轉換
                    errLog = ("Capio: " + capio.ToString() +"\r\n");
                    res = true;
                }
                else
                {
                    errLog = ("Capio Input Error\r\n");
                }
            }
            catch (Exception ex)
            {
                ; // MessageBox.Show(ex.Message);
            }
            if (res)
            {
                cmd = "setcap " + capio + "\r";
                serialPort1.WriteLine(cmd);
                Thread.Sleep(50);
                res = true;
            }
            ShowInsLog(errLog);
            return res;
        }
        public string HexToBitString(string hex)
        {
            hex = hex.Replace("0x", "").Replace("0X", "");
            StringBuilder sb = new StringBuilder(hex.Length * 4);
            foreach (char c in hex)
            {
                int value = Convert.ToInt32(c.ToString(), 16);
                sb.Append(Convert.ToString(value, 2).PadLeft(4, '0'));
            }
            return sb.ToString();
        }
        public bool ER4100_SetParameter(int src) // src: from 0-PacketTx, 1-ContinueTx, 2-PacketRx
        {
            bool res = false;
            string stmp = "";
            int drIdx = txDRCombo.SelectedIndex;
            int crcSelect = 0;
          
            if (src ==0)
                crcSelect = crcSelectCombo.SelectedIndex;
            else if (src == 2)
                crcSelect = packetRxCrcCombo.SelectedIndex;
            //txResult.Text += "Idx=" + idx + "   ";
            //txResult.Text += "PowerLevel=" + powerLevelList[idx] + "\r\n";

                int freqValue = 0;
                int drValue = 0;
                int moduValue = 4;
                int mod_idx_Value = 8;
                int pwrInteger = 127;
            double freqDevDouble = 0; 
            double freqDouble = 0; 
            bool freqInputIsOK = false;
            try
            {
                freqDevDouble = Convert.ToDouble(freqDev.Text);
                freqDouble = Convert.ToDouble(txFreqText.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Frequency or Deviation Setting Wrong");
                return res;
            }
             freqInputIsOK = CheckFrequencyLimit(freqDouble);
            if (freqInputIsOK)
                {                   
                    int freqDevValue = 0;
                    string pwrLevel = "0";
                    // src: 0-PacketTx / 1-ContinueTx 用 PARAMETER 內的 TX SYNCWORD;
                    //      2-PacketRx (含 SCAN) 用 RX SYNCWORD
                    bool isRx = (src == 2);
                    string syncwordInput = isRx ? rxSyncword.Text : txSyncword.Text;
                    bool syncwordInvert = isRx ? rxsyncInvertCheckbox.Checked : syncLSBCheckbox.Checked;
                    string syncwordString = "";
                    syncwordString = syncwordInput;

                    if (syncwordInvert)
                        syncwordString = hexReverse(syncwordInput);

                    string cmd = "";
                    freqValue = (int)(1000000 * freqDouble);
                    double drTextValue = double.Parse(txDRCombo.Text);
                    drValue = (int) (1000 * drTextValue);//Convert.ToInt32(drList2[drIdx]);
                    freqDevValue = (int)(2 * 1000 * freqDevDouble);

                    //mod_idx_Value = (int)(freqDevValue * 16 / drValue);
                    //if (mod_idx_Value < 8)
                    //    mod_idx_Value = 8;
                    //serialPort1.Write("rx 0\r");
                    //Thread.Sleep(50);

                    serialPort1.Write("stopblink 1\r");
                    Thread.Sleep(50);

                    stmp = serialPort1.ReadExisting();
                    Thread.Sleep(50);
                    string maxPwrValue = PAConfigCombo.Text;
                    serialPort1.Write("cfg 11 "+ maxPwrValue+"\r");
                    Thread.Sleep(50);
                    
                    stmp = serialPort1.ReadExisting();
                    try
                    {
                        pwrLevel = pwrlvl.Text.Trim();   // 抓取並去掉前後空白
                        pwrInteger = int.Parse(pwrLevel);          // 嘗試轉換
                        if (pwrInteger < 0 || pwrInteger > 127)
                        {
                            ShowInsLog("Power Level only (0~127)\r\n");
                            return false;
                        }
                    }
                    catch (FormatException)
                    {
                        ShowInsLog("Power Level Fail!\r\n");
                        return false;
                    }
                    catch (OverflowException)
                    {
                        ShowInsLog("Power Level Fail!\r\n");
                        return false;
                    }
                    cmd = "cfg 6 " + pwrInteger.ToString() + " " + freqValue + " " + drValue + " " + moduValue + " " + mod_idx_Value + "\r";                    
                    serialPort1.Write(cmd);
                    Thread.Sleep(250);
                    stmp = serialPort1.ReadExisting();
                    //ShowInsLog(stmp + "\r\n");
                    // PREAMBLE 的內容 (cfg 34) 與長度 (cfg 9) 都要在 cfg 7 之前送:
                    // 韌體收到 cfg 7 時會用「當下的」preamble 設定重新呼叫 xtApiSetTxFromat,
                    // 先送這兩個才會被 cfg 7 一起套進去。
                    // 注意 cfg 34 是不存 flash 的, 每次重開機/cfg 255 韌體都會回到 0xAA,
                    // 所以每次 SetParameter 都要重送一次。
                    string preambleContentStr = preambleContent.Text.Trim().PadLeft(2, '0');
                    cmd = "cfg 34 0x" + preambleContentStr + "\r";
                    serialPort1.Write(cmd);
                    Thread.Sleep(50);
                    stmp = serialPort1.ReadExisting();

                    string preambleLenStr = preambleLen.Text.Trim();
                    cmd = "cfg 9 " + preambleLenStr + "\r";
                    serialPort1.Write(cmd);
                    Thread.Sleep(50);
                    stmp = serialPort1.ReadExisting();

                    cmd = ("cfg 7 0x" + syncwordString + "\r");
                    serialPort1.Write(cmd);
                    Thread.Sleep(50);
                    stmp = serialPort1.ReadExisting();

                    cmd = "cfg 15 " + freqDevValue + "\r";   // reg 0x1984 (SYNTH_CAL_OFFSET_IN) = 2 * Deviation(Hz), 所以 freqDevValue 送出前已經乘 2

                    serialPort1.Write(cmd);
                    Thread.Sleep(50);
                    setCapio(src);

                    //ShowInsLog("SET TX OK\r\n");
                    stmp = serialPort1.ReadExisting();
                    //ShowInsLog(stmp+"\r\n");
                    txResult.Clear();
                    stmp = ("Set Parameter: " + txFreqText.Text + "MHz, " + txDRCombo.Text + "kbps, " + freqDev.Text + "kHz \r\n");
                    ShowInsLog(stmp);
                    stmp = (isRx ? "RX Syncword= (" : "TX Syncword= (") + HexToBitString(hexReverse(syncwordString)) + ")\r\n";
                    ShowInsLog(stmp);
                    stmp = "Preamble= 0x" + preambleContentStr + " x " + preambleLenStr + " byte(s)\r\n";
                    ShowInsLog(stmp);
                    res = true;
                }
                else
                {
                    res = false;
                    ShowInsLog("Frequency Not In Accept Range: \r\n");
                    ShowInsLog("778MHz - 1020MHz \r\n");
                    ShowInsLog("389MHz - 510MHz \r\n");
                    ShowInsLog("194.5MHz - 255MHz \r\n");
                    ShowInsLog("97.25MHz - 127.5MHz \r\n");   
                }
            return res;
        }

        /// <summary>
        /// 依 PARAMETER 分頁的 PAYLOAD ORDER 選擇, 直接寫 register 0xA0A8: LSB=0, MSB=1。
        /// (與 EzCodeGen/PayloadOrder.cs 產出的 .h 同一組值)
        /// 這個 register 沒有對應的 cfg 命令, 所以每次 TX/RX START 都補寫一次。
        /// </summary>
        public bool SetPayloadOrder()
        {
            if (!serialPort1.IsOpen)
            {
                ShowInsLog("COM PORT ERROR!\r\n");
                return false;
            }
            bool msbEnable = payloadMSB.Checked;
            string cmd = "wr 0xa0a8 0x" + (msbEnable ? "00000001" : "00000000") + "\r";
            serialPort1.Write(cmd);
            Thread.Sleep(50);
            serialPort1.ReadExisting();
            ShowInsLog("Payload Order: " + (msbEnable ? "MSB" : "LSB") + "\r\n");
            return true;
        }

        /// <summary>
        /// 依 PARAMETER 分頁的 ALLOW BIT ERROR (0~7) 寫 register 0xB104 的 INT_SYNC_ERR_BLE (bit[2:0])。
        /// 韌體的 xtApiSetRxSyncWordBitErr() 沒有對應的 cfg 命令可用, 只能直接 wr。
        /// 基底值 0x0007CC01 取自 .h 的初始設定 (= EzCodeGen/Syncword.cs 的算法),
        /// RxTableSetup() 不會動到 0xB104, 所以這樣寫不會蓋掉別的欄位。
        /// </summary>
        public bool SetAllowBitError()
        {
            if (!serialPort1.IsOpen)
            {
                ShowInsLog("COM PORT ERROR!\r\n");
                return false;
            }
            uint bitErr;
            if (!uint.TryParse(allowBitError.Text.Trim(), out bitErr) || bitErr > 7)
            {
                ShowInsLog("Allow Bit Error only (0~7)\r\n");
                return false;
            }
            uint regValue = (0x0007CC01u & ~0x07u) | bitErr;
            string cmd = "wr 0xb104 0x" + regValue.ToString("X8") + "\r";
            serialPort1.Write(cmd);
            Thread.Sleep(50);
            serialPort1.ReadExisting();
            ShowInsLog("Allow Bit Error: " + bitErr + " bit(s)\r\n");
            return true;
        }

        public void SetPayloadProcess()
        {
            // Owen:於 F/W內,用到crc16_ibm(uint8_t *data, uint8_t length) 
            // F/W: XTAPP_TransPbSwSend(void)
            // F/W: XTAPP_CustomerizePkt_Tx(uint32_t len)
            // 2025/08/14:加上CCIT及CRC16_IBM選擇
            // 2026/01/02:crcSelectCombo 加 NO_CRC 
            int crcChoose = 0;
            int len = 0;
            int num = 0;
            string cmd = "";
            bool addSeq = false;
            string stmp = "";
            cmd = "rfg 18 2\r";    // set isCustomerPayload =2 是從AP傳Payload下去FW
            serialPort1.Write(cmd);
            Thread.Sleep(50);

            if (addSeqCheckbox.Checked)
                addSeq = true;
            else
                addSeq = false;

            if (addSeq)
            {
                cmd = "rfg 23 1\r";    // addSeq = true (1)
                serialPort1.Write(cmd);
                Thread.Sleep(50);
                cmd = "rfg 25 0\r";    // seqNumber = 0 (reset)
                serialPort1.Write(cmd);
                Thread.Sleep(50);
            }
            else
            {
                cmd = "rfg 23 0\r";    // addSeq = false (0)
                serialPort1.Write(cmd);
                Thread.Sleep(50);
            }
            crcChoose = crcSelectCombo.SelectedIndex;
            cmd = "rfg 27 " + crcChoose+"\r";
            serialPort1.Write(cmd);
            Thread.Sleep(50);
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (byte b in tmpArray)
                {
                    sb.AppendFormat("{0:X2} ", b); // 轉成 2 位大寫 HEX，加空格
                }
            }
            catch (Exception ex)
            {
                //txResult.Text += "len <= 125";
                ShowInsLog("len is over range; len <= 125");
            }
            
            //int crcValue = CRC_Calculation();
            //byte[] bytes = new byte[2];
            //bytes[0] = (byte)((crcValue >> 8) & 0xFF);
            //bytes[1] = (byte)(crcValue & 0xFF);
            //string hexString = $"{bytes[0]:X2} {bytes[1]:X2}";
            //txResult.Text += "\r\nCRC=" + hexString + "\r\n";
            if (serialPort1.IsOpen)
            {
                cmd = "pay "+tmpArray.Count+"\r";    // Set payload: org: cfg 16
                serialPort1.Write(cmd);
                Thread.Sleep(100);
                // 如果傳送 byte array 要用 serialPort1.Write(arr, 0, arr.Length);
                serialPort1.Write(tmpArray.ToArray(), 0, tmpArray.Count);                
                Thread.Sleep(100);
                cmd = "\r";
                serialPort1.Write(cmd);
                Thread.Sleep(50);
                stmp = serialPort1.ReadExisting();
                //txResult.Text += stmp + "\r\n";
                ShowInsLog("Set Payload OK\r\n");
            }
            else
            {
                txResult.Text = "COM PORT NOT READY!\r\n";
            }


        }
    }
}
