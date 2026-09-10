
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Windows;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Timers;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;
using System.Security;
using System.Threading.Tasks;
using Microsoft.Vbe.Interop;
using ER41xxTestTool.Properties;
//using Microsoft.Office.Interop.Excel;

//using Agilent.CommandExpert.ScpiNet.Ag90x0_VMA_A_30_00;
//    uint8_t idx_band1; //sub-RF Band_idx_H:0= 230, 1=315, 2=433, 3=490, 4=815, 5= 915
//    uint8_t idx_band2; //sub-RF Band_idx_L:0= 230, 1=315, 2=433, 3=490, 4=815, 5= 915
//    uint8_t idx_txPower;//TX Power: 0=10dBm, 1=14dBm, 2=16dBm, 3=20dBm, 4=23dBm
//    TX_BPSK_MODE = 0,	                    //!<		XT9000 Tx BPSK mode			    0
// 		TX_QPSK_MODE,                       //!<		XT9000 Tx QPSK mode			    1
// 		TX_oQPSK_MODE,          			//!<		XT9000 Tx oQPSK mode	        2
// 		TX_FSK_MODE,            			//!<		XT9000 Tx FSK mode	            3
// 		TX_GFSK_MODE,           			//!<		XT9000 Tx GFSK mode	            4
//    PA Config
//    915: 10/20/23dBm
//    868: 14dBm
//    433: 10 / 23dBm
//    string s = s.Trim();   去頭尾空白
//------------- start: MainForm() ----------- Owen ------//
namespace ER41xxTestTool
{
    public partial class MainForm : Form, IView
    {
        //public event EventHandler SelectedIndexChanged; // Owen Listview Test1
        private System.Timers.Timer scanTimer;
        private System.Timers.Timer aTimer;
        private System.Timers.Timer readRegTimer;
        private System.Timers.Timer checkSCPITimer;
        private delegate void DelShowMessage(string sMessage, Color sColor, float cur);
        //private delegate void DelShowInsLog(string str);
        private delegate void DelGetDataRightNow(string str, int mode);
   
        private delegate void DelShowRxRcvNum(string sNum, string sRssi);
        private delegate void DelDisableSetting();

        private Controller txSerial;
        private Controller2 rxSerial;
        private Controller3 pcbSerial;
        private string initTitle = "ER41xx TEST TOOL";
        RegIDs          regIDs;
        RegNames        regNames;
        RegContents     regContents;
        FsPos           fsPos;
        FSMask          fsMask;
        BitRange        bitRange;
        public string ToolTitle = "ER4100 RF TEST TOOL 20260826 V1.20L";  // delete ER81Wx
        public bool isIRQ = false;
        public bool isRxOldMethod = false;
        public int TX_BPSK_MODE     = 0;
        public int TX_QPSK_MODE     = 1;
        public int TX_oQPSK_MODE    = 2;
        public int TX_FSK_MODE      = 3;
        public int TX_GFSK_MODE     = 4;

        public int maxPwrIdx = 0;
        public int frequencyBand = 0;

        List<string>[] dvbBandTable =
        {
            new List<string>{ "433", "868", "915" },   // idx = 0 (Low Power)
            new List<string>{ "433", "915" }           // idx = 1 (High Power)
        };
        public enum PA_TYPE
        {
            PA_915433L = 0,
            PA_868L = 1,
            PA_915433H = 2
        }
        List<string>[] PAConfigTable =
        {
            new List<string>{ "10" },       
            new List<string>{ "14" },       
            new List<string>{ "20", "23" }  
        };
        // Max Power:  0=10dBm, 1=14dBm, 2=16dBm, 3=20dBm, 4=23dBm
        public int baudRateNumber = 57600;    //9600;//115200;
        //public bool comConnect = false;
        public bool isDVBConnected  = false;	//DVB連線旗標 - 與DVB的連線狀態綁定
        public bool isTableReadReg  = false;
        public bool isWaitingData   = false;
        public bool isGetBackData   = false;
        public bool isShowIOData    = false;
        public bool isAutoFreqCal2  = false;
        public string readBackData;
        public bool bSearchID;
        public bool bSearchReg;

        public bool rxTestMode = false;
        public int rxReceivedNum = 0;
        public int cwProcess;
        public float freqTxt;
        public int powerTxt;
        public bool isRxSenStart;
        public int txPacketSetNum = 0;
        public int txPacketSent = 0;

        public int nowRssi = 0;
        public int ckRxSenResult = 0;
        public int formWidth = 1820;
        public int formHeight = 900;

        public int txMeter = 0;
        //private bool meterGetPower = false;

        //private int ckTxTimes = 0;
        public int isRomExist = 0;
        public bool isReadAllRegister;
        public bool isReadRegister;
        public bool isRSSIAnalysis;
        public bool hScanInit = false;
        public int getrssiTimes = 20;
        public string[] regName = new string[345];
        public string[] regList = new string[345];
        public string[] regValue = new string[345];
        public uint[] regValueInt = new uint[345];
        public string[][] regCont;
        public string[][] bitRangeMatrix;
        public int[][] fsPosMatrix;
        public int[][] fsMaskMatrix;
        public int regTableSize;
        public ListViewItem viewItem;
        public int rcvIdx;
        public int rcvEnd;
        public byte[] rcvRegArray = new byte[450];  // ORG: [4500]
        public int selIdx;
        public int[] searchIdx;
        public List<int> searchRegName = new List<int>();

        public string[] drList = {"2000.000","1000.000","500.000","250.000","200.000","125.000","100.000",
                                "62.500","50.000","40.000","31.250","25.000","20.000","15.625","12.500",
                                "10.000","7.8125","6.250", "5", "3.125","2.5", "1.25", "1", "0.625"};

        public string[] MaxPwrSelList = { "10", "14", "16", "20", "23" };
        //                             0      1       2      3      4     5
        public string[] RFBandSel = { "230", "315", "433", "490", "868","915"};
        public string[] modList = { "GFSK" };
        public string[] crcList = { "CCIT", "CRC16_IBM", "No CRC"};
        
        public string[] xtalArray = { "40", "38.4" };   // v1.19L: 與 EzCodeGen 相同, 支援 38.4MHz
        public const int NO_CRC_LEN_NO_SEQ = 127;
        public const int CRC_LEN_NO_SEQ = 125;
        public const int FIFO_MAX_LEN = 128;   // Length(1) + SeqNum(0/2) + Payload + CRC(0/2)
        public int highlowICType = -1;
        //TreeNode myTreeNode;
        public MainForm()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Dpi; // 或 Font
            RefreshDvbComPortList();     //刷新DVB Connection頁面中的com port list
            InitParamZoneUI();   // PARAMETER 分頁的動態控制項生成(INTERRUPT下拉勾選清單)+元件間即時連動邏輯
            InitRegDumpUI();   // REG WR 分頁的 REGISTER DUMP 功能初始化
            SetDVBBandComboDisconnected();   // 開機時還沒連線, DVBBandCombo 先鎖住顯示 "Don't care"
            InitialCRCList();
            InitialTxDatarate();
            InitialTxMod();
            //InitialMaxPowerSelectList(5);          
            InitialXtalList();
            //InitialTxPwr();
            //this.dvbTstPktRxPage.Parent = null;//隱藏
            //this.dvbTstContiTxPage.Parent = null;//隱藏

            dvbTstContiTxStartBtn.Enabled = true;
            dvbTstContiTxStopBtn.Enabled = false;
            powerTxt = 0;
            freqTxt = 0;
            cwProcess = 0;
            selIdx = 0;
            rcvIdx = 0;
            rcvEnd = 0;
            regTableSize = 0;
            isRxSenStart = false;
            bSearchID = false;
            bSearchReg = false;
            FullInSearchRegName();
            isReadAllRegister   = false;
            isReadRegister      = false;
            isRSSIAnalysis = false;
            viewItem    = new ListViewItem();
            regContents = new RegContents();
            fsPos       = new FsPos();
            fsMask      = new FSMask();
            bitRange    = new BitRange();
           
            this.statusTimeLabel.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            //ShowInsLog(PAConfigCombo.Text+"\r\n");
        }

        /*-------------------------------------------------------------
         *  CmdReadDVB
         * 透過Serial port對DVB下命令，並且讀取回字串訊息，最多重試MaxRetry(3)次，
         * 每次讀回後會執行以下作業:
         *  (1) 過濾第一個CRLF符號及其後的所有內容 (只取第一行)
         *  (2) 過濾頭尾空格
         *  (3) 驗證長度為8個字元、且為合法16進位字串 (表示字串為32bit資料的ascii)
         *  驗證通過就轉成byte[4] (大端序, MSB在前) 回傳true；重試MaxRetry次都失敗則回傳false。
         * Param :
         *   cmd    - 要送給DVB的serial指令字串 (需含結尾的換行符號)
         *   rdData - 輸出參數；成功時為轉換後的4-byte大端序資料，失敗時為null
         * Return:
         *   true  - 成功讀到合法的32bit hex字串並轉換成功
         *   false - 重試MaxRetry次後仍未讀到合法回應
         *-------------------------------------------------------------*/
        public bool CmdReadDVB(string cmd, out byte[] rdData)
        {
            rdData = null;
            const int MaxRetry = 3;
            string stmp = "";
            for (int i = 0; i < MaxRetry; i++)
            {
                try
                {
                    stmp = serialPort1.ReadExisting();
                    // 送 command
                    serialPort1.Write(cmd);
                    Thread.Sleep(100);

                    // 讀回字串   // Owen: try Read() no ReadLine()
                    stmp = serialPort1.ReadExisting();  //org: ReadLine()
                    //stmp = serialPort1.ReadExisting();
                    // 去掉 CRLF
                    //stmp = Regex.Replace(stmp, "[\r\n]", "").Trim();
                    //------------------------------------------------
                    // 用正則去除 \r\n 及其後的內容，再移除空格
                    stmp = Regex.Replace(stmp, @"[\r\n].*$", "");
                    stmp = stmp.Trim();
                    // 驗證長度 8 且是合法 hex
                    if (stmp.Length == 8 && uint.TryParse(stmp,
                            System.Globalization.NumberStyles.HexNumber,
                            null, out uint value))
                    {
                        // 轉成 byte[4]
                        rdData = BitConverter.GetBytes(value);
                        Array.Reverse(rdData); // 保證大端序 (00 00 00 02)
                        return true; 
                    }
                }
                catch
                {
                    // 忽略例外，直接重試
                }
            }
            return false;
        }       

        /*-------------------------------------------------------------
         *  CheckICPowerType
         * 呼叫CmdReadDVB()讀取暫存器0xF104，從讀回的4-byte(大端序)資料中，
         * 取最低位元組(rdData[3], 即暫存器bits[7:0])的bit1，
         * 判斷目前連接的DVB子板是High Power還是Low Power的IC。
         * (依呼叫端用法對照: 0=Low Power, 1=High Power)
         * 讀取失敗(CmdReadDVB回傳false)時會印出錯誤訊息、回傳-1當作"讀取失敗"的旗標值，
         * 呼叫端要另外處理-1這個特殊情況 (不能直接當成0或1用)。
         * Param :
         *   (無)
         * Return:
         *   0/1 - 讀取成功，IC為Low Power(0)或High Power(1)
         *   -1  - 讀取失敗 (CmdReadDVB重試多次仍讀不到合法回應)
         *-------------------------------------------------------------*/
        public int CheckICPowerType()
        {
            int type = -1;
            byte[] rdData = new byte[4];
            string scmd = "rr 0xf104\r";
            if (CmdReadDVB(scmd, out rdData))
            {
                //ShowInsLog(BitConverter.ToString(rdData)+"\r\n"); // 00-00-00-02
                //ShowInsLog("[0]" + rdData[0] + " [1]" + rdData[1] + " [2]" + rdData[2] + "- Type[3]: " + rdData[3] + "\r\n");
                type = (rdData[3] & 0x02) >> 1;
            }
            else
            {
                ShowInsLog("Read IC Type Fail, check the device\r\n");
            }           
            return type;
        }

        /*-------------------------------------------------------------
         *  InitialCRCList
         * [物件控制] - 刷新dvbTstPktTxCrcCombo, dvbTstPktRxCrcCombo
         * 把固定清單crcList({"CCIT","CRC16_IBM","No CRC"})填進PACKET TX/RX
         * 兩個分頁各自的CRC Polynomial下拉選單(dvbTstPktTxCrcCombo/dvbTstPktRxCrcCombo)，
         * 預設都選第0項("CCIT")。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        public void InitialCRCList()
        {
            int i = 0;
            for (i = 0; i < crcList.Length; i++)
            {
                dvbTstPktTxCrcCombo.Items.Add(crcList[i]);
                dvbTstPktRxCrcCombo.Items.Add(crcList[i]);
            }
            dvbTstPktTxCrcCombo.SelectedIndex = 0;
            dvbTstPktRxCrcCombo.SelectedIndex = 0;
        }

        /*-------------------------------------------------------------
         *  InitialTxDatarate
         * [物件控制] - 刷新paramDataRateCombo
         * 把固定清單drList(40MHz Xtal用的Data Rate選項, "2000.000"~"0.625"共24項)
         * 填進PARAMETER分頁的Data Rate下拉選單(paramDataRateCombo)，
         * 預設選第6項("100.000", 100kbps)。
         * (38.4MHz Xtal用的清單是drList384，另外在xtalCombo切換時才重新填。)
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        public void InitialTxDatarate()
        {
            int i = 0;
            for (i = 0; i < drList.Length; i++)
            {
                paramDataRateCombo.Items.Add(drList[i]);
            }
            paramDataRateCombo.SelectedIndex = 6;
        }

        /*-------------------------------------------------------------
         *  InitialMaxPowerSelectList
         * [物件控制] - 刷新paramPAConfigCombo
         * 把固定清單MaxPwrSelList({"10","14","16","20","23"}, 單位dBm)的前len項
         * 填進PA CONFIG下拉選單(paramPAConfigCombo)，填之前先清空舊選項，
         * 預設選第0項。len依連線後讀到的PA/IC類型而定，不同IC支援的dBm選項數量不同。
         * Param :
         *   len - 要填入的選項數量 (從MaxPwrSelList開頭取len項)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        public void InitialMaxPowerSelectList(int len)
        {
            int i = 0;
            paramPAConfigCombo.Items.Clear();
            for (i = 0; i < len; i++)
            {
                paramPAConfigCombo.Items.Add(MaxPwrSelList[i]);
            }
            paramPAConfigCombo.SelectedIndex = 0;
        }
        /*-------------------------------------------------------------
         *  InitialTxMod
         * [物件控制] - 刷新paramModulationCombo
         * 把固定清單modList(目前只有"GFSK"一項, 這個工具目前只支援GFSK調變)
         * 填進PARAMETER分頁的Modulation下拉選單(paramModulationCombo)，
         * 填之前先清空舊選項，預設選第0項。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        public void InitialTxMod()
        {
            paramModulationCombo.Items.Clear();
            paramModulationCombo.Items.AddRange(modList.ToArray());
            paramModulationCombo.SelectedIndex = 0;
        }

        public void InitialFreqBandList(int idx)
        {
            paramDvbBandCombo.Items.Clear();
            paramDvbBandCombo.Items.AddRange(dvbBandTable[idx].ToArray());
            paramDvbBandCombo.SelectedIndex = 0;
        }

        /// <summary>
        /// DVBBandCombo 純粹反映 DVB 板子(EEPROM/IC類型)的狀態, 跟 PARAMETER/REG TABLE 的 register
        /// 計算完全無關 (GenerateTxt() 不會讀它)。沒連線時這顆板子相關的資訊根本無意義, 鎖住並顯示
        /// "Don't care", 避免使用者誤以為選它會影響任何計算結果。
        /// </summary>
        private void SetDVBBandComboDisconnected()
        {
            paramDvbBandCombo.Items.Clear();
            paramDvbBandCombo.Items.Add("Don't care");
            paramDvbBandCombo.SelectedIndex = 0;
            paramDvbBandCombo.Enabled = false;
        }
        public void InitialPAConfigList(int idx)
        {
            paramPAConfigCombo.Items.Clear();
            paramPAConfigCombo.Items.AddRange(PAConfigTable[idx].ToArray());
            paramPAConfigCombo.SelectedIndex = 0;
        }
        public void InitialXtalList()
        {
            paramXtalCombo.Items.Clear();
            paramXtalCombo.Items.AddRange(xtalArray.ToArray());
            paramXtalCombo.SelectedIndex = 0;
        }                          
       
        public void GetDataRightNow(string str, int mode)
        {
            if (this.InvokeRequired)
            {
                DelGetDataRightNow del = new DelGetDataRightNow(GetDataRightNow);
                this.Invoke(del, str, mode);
            }
            else
            {
                readBackData = str;
                if (mode==0)
                {
                    isWaitingData = false;
                    //DoAutoAdjustFreqAfter(str);
                }
                else if (mode == 1)
                {
                    isGetBackData = false;
                    //WriteBack1580(str, true);
                }
                else if (mode == 2)     // only show IO_Value on TextBox
                {
                    isShowIOData = false;
                    //WriteBack1580(str, false);
                }
                
                //ShowLogOnTxtBox(1, (isWaitingData+"_"+ readBackData), true);
            }
        }

        /// <summary>
        /// Set txSerial
        /// </summary>
        /// <param name="txSerial"></param>
        public void SetController(Controller txSerial)
        {
            this.txSerial = txSerial;
        }

        public void SetController2(Controller2 rxSerial)
        {
            this.rxSerial = rxSerial;
        }

        public void SetController3(Controller3 pcbSerial)
        {
            this.pcbSerial = pcbSerial;

        }

        public void RcvDataToReg()
        {
            int j = 0;
            string stmp = "";
            int startFlag = 64;  // 64=@"

            //txreceivetbx.Text += "rcvIdx=" + rcvIdx + "; end=" + rcvEnd + "\r\n";
            for (int i = 0; i < rcvIdx; i++)
            {
                if (rcvRegArray[i] == startFlag)
                {
                    j = i;
                    break;
                }
            }
            //txreceivetbx.Text += "St=" + j + " \r\n";

            stmp = System.Text.Encoding.UTF8.GetString(rcvRegArray, (j+1), 8);
      
            //if (wrCheckbox.Checked)
            //    txtValue.Text = stmp;
            //else
                
            if (isWaitingData)
            {               
                GetDataRightNow(stmp, 0); // 0 -- for auto freq calibration 
            }
            else if (isGetBackData)
            {
                GetDataRightNow(stmp, 1); // 1 -- for 1580 read back
            }
            else if (isShowIOData)
            {
                GetDataRightNow(stmp, 2); // 1 -- for 1580 show IO value on ioValue.Text
            }
            if (isAutoFreqCal2)
            {
                GetDataRightNow(stmp, 3); // 3 -- for auto freq calibration 2
            }
        }

        // 當第一次收到interrupt時,直接貼在rcvRegArray[0],
        // 之後在 rcvRegArrar 判斷有幾個 "$xxx#&"
        // 找到最後一個#&, Ex: &在[10],然後將[11]後的所有data往前貼在[0]
        // 
        public void rcvRSSIProcess2(byte[] dataR)
        {
            for (int i = 0; i < dataR.Length; i++)
            {
                if (rcvIdx < rcvRegArray.Length)
                {
                    rcvRegArray[rcvIdx] = dataR[i];
                    rcvIdx++;
                }

                if ((dataR[i] == 38) && (dataR[i - 1] == 38))   // @ = 38
                {
                    rcvEnd = (rcvIdx - 2);
                    //RcvDataToArray();
                }
            }
        }

        public void rcvRSSIProcess(byte[] dataR)
        {
            // "$xxx#&"
            try
            {
                for (int i = 0; i < dataR.Length; i++)
                {
                    if ((dataR[i] == '&') && (dataR[i - 1] == '#'))   // @ = 38
                    {
                        rxReceivedNum++;
                        dvbTstPktRxOkCntText.Text = rxReceivedNum.ToString();
                        dvbTstPktRxOkCntText.Update();
                        // * 可以用以下方法每8bytes剪一下, 再每一個bytes轉成str, 也可以只用一條command:
                        string stmp = "";
                        if ((i - 4) >= 0)
                        {
                            stmp = System.Text.Encoding.UTF8.GetString(dataR, (i - 4), 3);
                            nowRssi = (-1) * Convert.ToInt32(stmp);
                            dvbTstPktRxRssiText.Text = nowRssi.ToString();
                            dvbTstPktRxRssiText.Update();
                            //ShowInsLog("- " + nowRssi + "\r\n");
                        }
                        else
                            ShowInsLog("Rcv error:len=" + dataR.Length + "\r\n");
                    }
                }
            }
            catch (Exception e)
            {
                ;
            }
            
        }


/*
//        rdr 0x0004
//        [RDR]:0x0004:0x0000011A
//        OK
//        CMD> 

// 0   1    2   3  4  5  6  7  8  9  10 11 12 13 14 15 16 17 18 19 20 21  22 23 24 25 26 27 28  29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50
// --------------------------------------------------------------------------------------------------------------------------------------------------------------
//                                                                                    :  0  x   0  0  0  0  0  1  1  A
// 114 100 114 32 48 120 48 48 48 52 13 13 10 13 91 82 68 82 93 58 48 120 48 48 48 52 58 48 120 48 48 48 48 48 49 49 65 10 13 79 75 10 13 13 10 13 67 77 68 62 32 
            */
        public void ReadRegisterProcess(byte[] dataR)
        {
            if ((isReadRegister) && ((rcvIdx+ dataR.Length)< rcvRegArray.Length))
            {
                for (int i = 0; i < dataR.Length; i++)
                {
                    rcvRegArray[rcvIdx] = dataR[i];
                    rcvIdx++;                   
                    if ((i > 0) && (dataR[i] == 38) && (dataR[i - 1] == 38))   // @ = 38
                    {
                        rcvEnd = (rcvIdx - 2);
                        RcvDataToReg();
                        //ShowAllRegisterData();
                        //ShowRegisterData();
                        isReadRegister = false;                        
                    }
                }
            }

        }

        /// <summary>
        /// update status bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// Rx COM 接受 data
        public void OpenComEvent(Object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Object, SerialPortEventArgs>(OpenComEvent), sender, e);
                return;
            }

            if (e.isOpend)  //Open successfully
            {
                //statuslabel.Text = txcomListCbx.Text + "- Tx Opened";
                //btnRxComConnect.Text = "Close";
                //rxTestStart.Enabled = true;

                autoSendcbx.Enabled = true;
                autoReplyCbx.Enabled = true;
                //rxComboBox.Enabled = false;
                //dataBitsCbx.Enabled = false;
                //stopBitsCbx.Enabled = false;
                //parityCbx.Enabled = false;
                //btnRxComRefresh.Enabled = false;

               
            }
            else    //Open failed
            {
       
                autoSendcbx.Enabled = false;
                autoReplyCbx.Enabled = false;
            }
        }

        public void OpenComEvent2(Object sender, SerialPortEventArgs2 e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Object, SerialPortEventArgs2>(OpenComEvent2), sender, e);
                return;
            }

            if (e.isOpend)  //Open successfully
            {

            }
            else    //Open failed
            {
            }
        }

        public void OpenComEvent3(Object sender, SerialPortEventArgs3 e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Object, SerialPortEventArgs3>(OpenComEvent3), sender, e);
                return;
            }

            if (e.isOpend)  //Open successfully
            {
                //statuslabel.Text = rxcomListCbx.Text + " Opend";
                //pcbopenCloseSpbtn.Text = "Disconnect";
                //pcbsendbtn.Enabled = true;
                autoSendcbx.Enabled = true;
                autoReplyCbx.Enabled = true;

            }
            else    //Open failed
            {

            }
        }

        /// <summary>
        /// update status bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CloseComEvent(Object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Object, SerialPortEventArgs>(CloseComEvent), sender, e);
                return;
            }

            if (!e.isOpend) //close successfully
            {
                //statuslabel.Text = txcomListCbx.Text + " Closed";
                //btnRxComConnect.Text = "Connect";
                autoSendcbx.Enabled = false;
                autoSendtimer.Stop();
                //rxComboBox.Enabled = true;
                //btnRxComRefresh.Enabled = true;
            }
        }

        public void CloseComEvent2(Object sender, SerialPortEventArgs2 e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Object, SerialPortEventArgs2>(CloseComEvent2), sender, e);
                return;
            }

            if (!e.isOpend) //close successfully
            {

            }
        }

        public void CloseComEvent3(Object sender, SerialPortEventArgs3 e)
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action<Object, SerialPortEventArgs3>(CloseComEvent3), sender, e);
                return;
            }

            if (!e.isOpend) //close successfully
            {
                //statuslabel.Text = pcbcomListCbx.Text + " Closed";
                //pcbopenCloseSpbtn.Text = "Connect";
                //txPCBPower.Text = "";
                //pcbsendbtn.Enabled = false;
            }
        }

        /// <summary>
        /// Display received data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// Rx COM 接受 data
        public void ComReceiveDataEvent(Object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    Invoke(new Action<Object, SerialPortEventArgs>(ComReceiveDataEvent), sender, e);
                }
                catch (System.Exception)
                {
                    //disable form destroy exception
                }
                return;
            }
            /// Owen: Rx COM 接受 data
            if (recStrRadiobtn.Checked) //display as string // Owen: RX COM RECEIVE
            {
                //ShowInsLog(Encoding.Default.GetString(e.receivedBytes)+" ");
                //rxSenResultText.AppendText(Encoding.Default.GetString(e.receivedBytes));
                if (isRSSIAnalysis)
                    rcvRSSIProcess(e.receivedBytes);
                //if (isReadRegister)
                //    ReadRegisterProcess(e.receivedBytes);
                //if (isReadAllRegister)
                //    ReadAllRegisterProcess(e.receivedBytes);

            }
            else //display as hex
            {
                //if (txreceivetbx.Text.Length > 0){
                //    txreceivetbx.AppendText("-");}
                //txreceivetbx.AppendText(Controller.Bytes2Hex(e.receivedBytes));
            }           
        }
        public void ComReceiveDataEvent2(Object sender, SerialPortEventArgs2 e)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    Invoke(new Action<Object, SerialPortEventArgs2>(ComReceiveDataEvent2), sender, e);
                }
                catch (System.Exception)
                {
                    //disable form destroy exception
                }
                return;
            }           
            
        }

        public void ComReceiveDataEvent3(Object sender, SerialPortEventArgs3 e)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    Invoke(new Action<Object, SerialPortEventArgs3>(ComReceiveDataEvent3), sender, e);
                }
                catch (System.Exception)
                {
                    //disable form destroy exception
                }
                return;
            }

            if (recStrRadiobtn.Checked) //display as string
            {
                //if (meterGetPower == true)
                //   PCBTxPower(e.receivedBytes);
                //pcbreceivetbx.AppendText(Encoding.Default.GetString(e.receivedBytes));

            }
            else //display as hex
            {
                //if (pcbreceivetbx.Text.Length > 0){
                //    pcbreceivetbx.AppendText("-");
                //pcbreceivetbx.AppendText(Controller.Bytes2Hex(e.receivedBytes));
            }
            
        }        

        /// <summary>
        /// update time in status bar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statustimer_Tick(object sender, EventArgs e)
        {
            this.statusTimeLabel.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }


        private void autoSendtimer_Tick(object sender, EventArgs e)
        {
            //txsendbtn_Click(sender, e);
        }

        /// <summary>
        /// filter illegal input of auto send interval time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sendIntervalTimetbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Add CRC checkbox changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addCRCcbx_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void SaveInitValue()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "txt file|*.txt";
            saveFileDialog.DefaultExt = ".txt";
            //saveFileDialog.FileName = "InitialValue.txt";

            //if (saveFileDialog.ShowDialog() == DialogResult.OK){
            //    
            //    System.IO.File.WriteAllText(fName, txreceivetbx.Text);
            //}
            String fName = saveFileDialog.FileName;
            int space = 0;
            
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {                
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName))
                {
                    sw.WriteLine(initTitle);
                    for (int i = 0; i < regName.Length; i++){
                        sw.Write(regList[i]);
                        sw.Write("    ");
                        sw.Write(regName[i]);
                        space = regName[i].Length;
                        for (int j=0; j<(30-space); j++)
                            sw.Write(" ");
                        sw.WriteLine(regValue[i]);
                    }
                }
                //sw.Write("};");
            }
        }
     
        public byte[] tRpt = { 58, 67, 65, 76, 67, 58, 77, 65, 82, 75, 49, 58, 89, 63,
                                13, 10, 13,10, 50, 46, 50, 54, 54, 69, 43, 48, 48, 13,
                                10, 83, 67, 80, 73, 62, 32 };
                

        private void MainForm_Load(object sender, EventArgs e)
        {
            FitToScreen();   // v1.19L: 螢幕夠大就維持原始大小(無捲軸), 螢幕不夠大才啟用捲軸
            dvbTstContiTxPatternGroup.Visible = false;

            // PARAMETER 等分頁先套上官方預設值 (38.4MHz/435MHz/96kbps/48kHz/PA23dBm, 見
            // DefaultParameters.cs), 這組值已驗證跟 REG TABLE 內建預設值 (RegTableItems.cs)
            // 逐位元組相符, 所以下一步用畫面參數重算出來的 register 才會跟 REG TABLE 對得上。
            // 一樣要放在 Load 而不是建構子, 理由同下面 ApplyUiValuesToRegisterList()。
            ApplyDefaultParameters();

            // REG TABLE 第二步: 用畫面上的參數蓋掉預設值。
            // 放在 Load 而不是建構子, 因為 InitialTxDatarate()/InitialXtalList() 等要先把下拉選單填好。
            ApplyUiValuesToRegisterList();
            // 之後參數一有變動就自動重算 (掛完才會把 bRegAutoRefreshReady 打開)
            HookParameterChangeEvents();

            RefreshPacketFormatDiagram();

            //SetStateButton(dvbTstPktRxStartBtn, true);
        }

        /*  //因為ListView compile 時出error, 暫時 closed
        private void SetHeight(ListView listView, int height)
        {
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, height);
            listView.SmallImageList = imgList;
        }*/
        private void FullInSearchRegName()
        {
            for (int i=0; i< regList.Length; i++)
            {
                searchRegName.Add(i);
            }
        }

         /*
        public void ShowInsLog(string str)
        {
            insertValue.Text = insertValue.Text.Insert(0, str);
            insertValue.Update();
        }*/

        public void ShowInsLog(string str)
        {
            if (bQuietLog)      // 開機時的 REG TABLE 初始化不要洗版 (見 RegDumpProcess.cs)
                return;
            if (logResultText.InvokeRequired)
            {
                // 如果在非 UI Thread 呼叫 (例如 DataReceived)，要用 Invoke 切回 UI Thread
                logResultText.BeginInvoke((MethodInvoker)(() => ShowInsLog(str)));
            }
            else
            {
                // 限制文字大小，避免 TextBox 文字太多卡住
                const int MAX_LENGTH = 100000; // 例如只保留最近 100K 字
                if (logResultText.TextLength > MAX_LENGTH)
                {
                    logResultText.Text = logResultText.Text.Substring(logResultText.TextLength - MAX_LENGTH);
                }

                // 追加文字
                logResultText.AppendText(str); // AppendText 效率比 += 好

                // 自動捲到最後
                logResultText.SelectionStart = logResultText.Text.Length;
                logResultText.ScrollToCaret();
            }
        }       
        
        private void btnTxClear_Click(object sender, EventArgs e)
        {
            logResultText.Clear();
        }       
                
        System.Threading.Thread CloseDown2;

        // Set Tx Parameter
        private void btnSetER4100_Click(object sender, EventArgs e)
        {
            bool res = false;
            if (!serialPort1.IsOpen)
            {
                ShowInsLog("COMPORT FAIL\r\n");
            }
            else
            {
                // 2026-09-03 暫時關閉: 見 dvbTstPktTxStartBtn_Click 同樣的說明, 改成使用者自己先按
                // Write DVB Register 再操作。
                // res = ER4100_SetParameter(0); // Set Tx Parameter
            }

        }

        private void CloseSerial2OnExit()
        {
            try
            {
                serialPort2.DataReceived -= RxReceiveData;
                serialPort2.DiscardInBuffer();
                serialPort2.DiscardOutBuffer();
                serialPort2.Close();
            }
            catch (Exception ex)
            {
                ShowInsLog(ex.Message + "\r\n");
            }
        }

        // Owen: SG 操作
        // Preset --> mode --> Dual ARB --> Select Waveform --> explorer --> int_storage
        // ER4_2000K_0P5.WFM --> Load Segment from INt_Media --> return
        // Select waveform
        // trig --> Single --> buffered trigger
        // Trig source --> Trig souurce (Ext) --> Trigger key
        // Dual ARB --> ON
        // RF ON


        public bool ER4100_SendCmdThread(string cmd)
        {
            bool res = false;
            res = txSerial.SendDataToCom(cmd);
            return res;
        }

        public bool ER4100_RxSendCmd(string cmd)
        {
            bool res = false;
            res = serialPort2.IsOpen;
            if (res)
            {
                serialPort2.WriteLine(cmd);
                res = true;
            }
            return res;
        }

        public bool ER4100_SendCmd(string cmd)
        {
            bool res = false;
            res = serialPort1.IsOpen;
            if (res)
            {
                serialPort1.Write(cmd);
                res = true;
            }            
            return res;
        }

        public bool ER4100_SendModeCmd(string cmd)
        {
            bool res = false;
            res = serialPort3.IsOpen;
            if (res)
            {
                serialPort3.WriteLine(cmd);
                res = true;
            }
            return res;
        }

        public bool SetRxTestMode(int rOn)
        {
            bool res = false;
            if (isRxOldMethod)
                res = ER4100_SendCmdThread("settestst "+rOn+"\r");
            else
                res = ER4100_RxSendCmd("settestst " + rOn + "\r");
            return res;
        }

        public bool SetRxPerMode(int rOn)
        {
            bool res = false;
            if (isRxOldMethod) 
                res = ER4100_SendCmdThread("setperst " + rOn + "\r");
            else
                res = ER4100_RxSendCmd("setperst " + rOn + "\r");
            return res;
        }

        public bool ClrRxResult()
        {
            bool res = false;
            if (isRxOldMethod) 
                res = ER4100_SendCmdThread("perclr\r");
            else
                res = ER4100_RxSendCmd("perclr\r");
            return res;
        }        

        private void btnRxResultClr_Click(object sender, EventArgs e)
        {
            ClrRxResult();
        }
        
        public bool GetRxResult()
        {
            bool res = false;
            res = ER4100_SendCmd("pernum\r");
            string str = "";
            if (res)
            {
                Thread.Sleep(100);
                str = serialPort1.ReadExisting();
                ShowInsLog(str+"\r\n");
            }
            return res;
        }

        private void btnRxGetResult_Click(object sender, EventArgs e)
        {
            GetRxResult();
        }

        private void btnRxStart_Click(object sender, EventArgs e)
        {
            bool res = false;
            if (dvbTstPktRxStartBtn.Text == "Start")
            {
                if (isRxOldMethod)
                    res = ER4100_SendCmdThread("\r");
                else
                    res = ER4100_RxSendCmd("\r");
                if (!res)
                {
                    ShowInsLog("COM PORT ERRPR\r\n");
                }   
                else
                {
                    isRxSenStart = true;
                    lastRSSI = "";
                    res = SetRxTestMode(1);
                    Thread.Sleep(100);
                    res = SetRxPerMode(1);
                    rxTestMode = true;
                    dvbTstPktRxStartBtn.Text = "Stop";
                    dvbTstPktRxStartBtn.Update();
                    rxReceivedNum = 0;
                    nowRssi = 0;
                    //avgRssi = 0;
                    //rcvIdx = 0;
                    //rcvEnd = 0;
                    dvbTstPktRxOkCntText.Text = rxReceivedNum.ToString();
                    dvbTstPktRxRssiText.Text = nowRssi.ToString();
                    //avgrssitbx.Text = avgRssi.ToString();
                    isRSSIAnalysis = true;
                    serialPort2.DiscardOutBuffer();
                    serialPort2.DiscardInBuffer();

                    ShowInsLog("Set Rx Mode: ON\r\n");
                }               
            }
            else
            {
                
                res = SetRxTestMode(0);
                Thread.Sleep(100);
                res = SetRxPerMode(0);
                if (res)
                {
                    serialPort2.DiscardOutBuffer();
                    serialPort2.DiscardInBuffer();
                    rxTestMode = false;
                    isRSSIAnalysis = false;
                    lastRSSI = "";
                    isRxSenStart = false;
                    dvbTstPktRxStartBtn.Text = "Rx Start";
                    dvbTstPktRxStartBtn.Update();
                    ShowInsLog("Set Rx Mode: OFF\r\n");
                }
                else
                    ShowInsLog("COM PORT ERRPR\r\n");
            }                        
                          
        }

        public void RefreshDvbComPortList()
        {
            comPortToolStripComboBox.Items.Clear();
            string[] ArrayComPortsNames = SerialPort.GetPortNames().Distinct().ToArray();
            if (ArrayComPortsNames.Length == 0)
            {
                connectComToolStripMenuItem.Enabled = false;
            }
            else
            {
                Array.Sort(ArrayComPortsNames);
                for (int i = 0; i < ArrayComPortsNames.Length; i++)
                {
                    comPortToolStripComboBox.Items.Add(ArrayComPortsNames[i]);
                }
                comPortToolStripComboBox.Text = ArrayComPortsNames[0];
                connectComToolStripMenuItem.Enabled = true;
                //statuslabel.Text = "OK !";
            }
        }        

        // ===== v1.19L: COM PORT 選單 =====
        private void refreshComToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshDvbComPortList();
        }

        // 呼叫Menu上的DVB Connection小視窗，讓使用者選擇DVB Port相關資訊
        // v1.21L: 點「COM PORT」-> 已連線時直接斷線, 未連線時彈出 ComPortSelectDialog 選 port 後直接連線
        private void comPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool isConnected = isDVBConnected;// (connectComToolStripMenuItem.Text == "CLOSE");
            string[] portNames = SerialPort.GetPortNames().Distinct().OrderBy(p => p).ToArray();

            string baudRateText = "Baudrate: " + baudRateNumber.ToString() + " bps";
            using (ComPortSelectDialog dlg = new ComPortSelectDialog(portNames, comPortToolStripComboBox.Text, baudRateText, isConnected))
            {
                DialogResult result = dlg.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    comPortToolStripComboBox.Text = dlg.SelectedPort;
                    //connectComToolStripMenuItem_Click(sender, e); //透過com port讀取er4100模組設定
                    dvbConnectionProcess();
                    RefreshRegisterListFromUi();
                }
                else if (result == DialogResult.Abort)  // DISCONNECT
                {
                    //connectComToolStripMenuItem_Click(sender, e); //透過com port讀取er4100模組設定
                    dvbConnectionProcess();
                }
            }
        }

        private void RxSenTextClr_Click_1(object sender, EventArgs e)
        {
            dvbTstPktRxLogText.Text = "";
        }

        private void SaveInitValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveInitValue();
        }

        System.Threading.Thread CloseDown1;
        //private void connectComToolStripMenuItem_Click(object sender, EventArgs e)
        private void dvbConnectionProcess()
        {
            bool res = false;

            if (!isDVBConnected)//(connectComToolStripMenuItem.Text == "CONNECT")
            {
                if (!serialPort1.IsOpen)
                {
                    res = ConnectWithIRQ();  // no IRQ
                    //ConnectSerial3();
                }
                Thread.Sleep(100);
                
                if ((serialPort1.IsOpen) && (res))
                {
                    ShowInsLog($"{comPortToolStripComboBox.Text} connected\r\n");
                    refreshComToolStripMenuItem.Enabled = false;
                    comPortToolStripComboBox.Enabled = false;
                    Thread.Sleep(50);
                    serialPort1.Write("\r");
                    Thread.Sleep(50);
                    string stmp = serialPort1.ReadExisting();
                    Thread.Sleep(50);
                    serialPort1.Write("settestst 1\r"); // Owen tmp Closed
 
                    Thread.Sleep(50);
                    stmp = serialPort1.ReadExisting();
                   // connectComToolStripMenuItem.Text = "CLOSE";
                    isDVBConnected = true;

                    highlowICType = CheckICPowerType();

                    if (highlowICType == -1)
                    {
                        // v1.19L: 讀不到 IC 設定時不斷線, 顯示訊息並讓 user 手動選 PA CONFIG
                        ShowInsLog("Cannot read IC setting value.\r\n");
                        ShowInsLog("Please select PA CONFIG manually.\r\n");
                        paramPAConfigCombo.Enabled = true;
                        InitialFreqBandList(0);   // 已連線, 只是讀不到 IC 類型, 給真的 band 選項讓使用者手動選 (不是 "Don't care")
                        paramDvbBandCombo.Enabled = true;
                        return;
                    }
                    //public enum PA_TYPE{
                    //    PA_915433L = 0,
                    //    PA_868L = 1,
                    //    PA_915433H = 2
                    //}
                    else if (highlowICType == 0)
                    {
                        InitialFreqBandList(0);  // low power
                        paramPwrLvText.Text = "27";
                       // pwrlvlLable.Text = "Level (0-27)";
                        paramPwrLvLabel.Text = "Power LV(0~27)";
                        InitialPAConfigList((int)PA_TYPE.PA_915433L);
                        ShowInsLog("Low Power IC\r\n");
                    }                       
                    else if (highlowICType == 1)
                    {
                        InitialFreqBandList(1);    // high power
                        paramPwrLvText.Text = "127";
                        //pwrlvlLable.Text = "Level (0-127)";
                        paramPwrLvLabel.Text = "Power LV(0~127)";
                        InitialPAConfigList((int)PA_TYPE.PA_915433H);
                        paramPAConfigCombo.SelectedIndex = 1;
                        ShowInsLog("High Power IC\r\n");
                    }                       
                    else {
                        // v1.19L: 讀不到 IC 設定時不斷線, 顯示訊息並讓 user 手動選 PA CONFIG
                        ShowInsLog("Cannot read IC setting value.\r\n");
                        ShowInsLog("Please select PA CONFIG manually.\r\n");
                        paramPAConfigCombo.Enabled = true;
                        InitialFreqBandList(0);   // 已連線, 只是讀不到 IC 類型, 給真的 band 選項讓使用者手動選 (不是 "Don't care")
                        paramDvbBandCombo.Enabled = true;
                        return;
                    }
                    stmp = serialPort1.ReadExisting();
                    Thread.Sleep(50);
                    // ER81Wx 沒有 EEPROM
                     
                    isRomExist = CheckEepromExist();

                    if (isRomExist == 1) //有 EEPROM
                    {
                        capio = ReadTableCap();
                        if (capio == 0)
                        {
                            ShowInsLog("Read CAPIO From EEPROM FAIL! Let initial cap=33\r\n");
                            capio = 33;
                        }
                        paramCapIoText.Text = capio.ToString();
                        maxPwrIdx = ReadMaxPwrFromEeprom();                       
                        if ((maxPwrIdx >=0) && (maxPwrIdx <= 4))
                        {
                            paramPAConfigCombo.Items.Clear();
                            InitialMaxPowerSelectList(5);
                            paramPAConfigCombo.SelectedIndex = maxPwrIdx;
                            paramPAConfigCombo.Enabled = false;
                            //PAConfigCombo.Text = MaxPwrSelList[maxPwrIdx];
                        }
                        else
                        {
                            ShowInsLog("Cannot read PA CONFIG setting value (" + maxPwrIdx + "). Please select PA CONFIG manually.\r\n");
                            paramPAConfigCombo.Enabled = true;
                        }
                        frequencyBand = ReadFreqBandFromEeprom();
                        if (frequencyBand == 2)
                        {
                            paramDvbBandCombo.Text = RFBandSel[2];
                            paramBaseFreqText.Text = RFBandSel[2];
                            paramDvbBandCombo.Enabled = false;
                        }
                        else if (frequencyBand == 4)
                        {
                            paramDvbBandCombo.Text = RFBandSel[4];
                            paramBaseFreqText.Text = RFBandSel[4];
                            paramDvbBandCombo.Enabled = false;
                        }                            
                        else if (frequencyBand == 5)
                        {
                            paramDvbBandCombo.Text = RFBandSel[5];
                            paramBaseFreqText.Text = RFBandSel[5];
                            paramDvbBandCombo.Enabled = false;
                        }
                        else
                        {
                            ShowInsLog("Read RF Band Fail: " + frequencyBand + "\r\n");
                            paramDvbBandCombo.Enabled = true;
                        }
                    }                            
                }
               
            }
            else  // 原本已 CONNECT
            {
                CloseComPortProcess();
                ShowInsLog("COM Closed\r\n");
            }
        }
       
        public void CloseComPortProcess()
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("stopblink 0\r");
                Thread.Sleep(50);
                serialPort1.WriteLine("rfg 27 0\r");
                Thread.Sleep(50);
                serialPort1.WriteLine("setperst 0\r");
                Thread.Sleep(50);
                serialPort1.WriteLine("settestst 0\r");
                Thread.Sleep(50);
                //serialPort1.Close();
                CloseDown1 = new System.Threading.Thread(new System.Threading.ThreadStart(CloseSerial1OnExit));
                CloseDown1.Start();
            }
            //connectComToolStripMenuItem.Text = "CONNECT";
            isDVBConnected = false;
            refreshComToolStripMenuItem.Enabled = true;
            comPortToolStripComboBox.Enabled = true;
            SetDVBBandComboDisconnected();   // 斷線後 DVBBandCombo 跟 DVB 無關, 鎖住顯示 "Don't care"
            paramPAConfigCombo.Enabled = true;
        }
        
        private void CloseSerial1OnExit()
        {
            try
            {
                if (serialPort1 != null)
                {
                    if (serialPort1.IsOpen)
                    {
                        DetachUartRxHandler();
                       // serialPort1.DiscardInBuffer();
                        //serialPort1.DiscardOutBuffer();
                        serialPort1.Close();
                    }
                    //serialPort1.Dispose();
                }
            }
            catch (Exception ex)
            {
                //ShowInsLog($"Close COM error: {ex.Message}\r\n");
            }
        }
        public void SetSyncword()
        {
            string txSyncwordHex = paramTxSyncText.Text;
            uint intValue = (UInt32)Convert.ToInt32(txSyncwordHex, 16);
            string cmd = "rfg 7 " + intValue + "\r";
            bool res = ER4100_SendCmd(cmd);
            if (!res)
                ShowInsLog("COM PORT ERROR\r\n");
            else
                ShowInsLog("Set Syncword OK\r\n");
        }
        private void btnTxSetSyncword_Click(object sender, EventArgs e)
        {
            string txSyncwordHex = paramTxSyncText.Text;
            uint intValue = (UInt32)Convert.ToInt32(txSyncwordHex, 16);
            string cmd = "rfg 7 " + intValue + "\r";
            bool res = ER4100_SendCmd(cmd);
            if (!res)
                ShowInsLog("COM PORT ERROR\r\n");
            else
                ShowInsLog("Set Syncword OK\r\n");
        }       

        public void ShowRxRcvNum(string sNum, string sRssi)
        {
            if (this.InvokeRequired)
            {
                DelShowRxRcvNum del = new DelShowRxRcvNum(ShowRxRcvNum);
                this.Invoke(del, sNum, sRssi);
            }
            else
            {
                //txPCBPower.Text = str;
                dvbTstPktRxOkCntText.Text = sNum;
                dvbTstPktRxOkCntText.Update();
                dvbTstPktRxRssiText.Text = sRssi;
                dvbTstPktRxRssiText.Update();
            }
        }
        //public int rnum = 0;
        public string lastRSSI = "";
        public void RxReceiveData(object sender, SerialDataReceivedEventArgs e)
        {
            //string str = serialPort2.ReadExisting();
            try
            {
                string str = "";
                str = serialPort2.ReadLine();
                string sRssi = "";
                string tmp = "";
                int nowRssi = 0;
                int start = 0;
                //int stop = 0;
                if (str.Contains('#'))
                {
                    rxReceivedNum++;
                    if (str.Contains('$') && str.Contains('&'))
                    {                  
                        start = str.IndexOf("$");
                        //stop = str.IndexOf("#");
                        tmp = str.Substring((start+1), 3);
                        nowRssi = (-1) * Convert.ToInt32(tmp);
                        sRssi = nowRssi.ToString();
                        //lastRSSI = sRssi;
                        //ShowInsLog(start+" "+stop + " "+ tmp + "\r\n");
                    }                    
                }
                //else
                //    sRssi = lastRSSI;

                if (isRxSenStart)
                    ShowRxRcvNum(rxReceivedNum.ToString(), sRssi);
            }
            catch (Exception ex)
            {
                //sRssi = lastRSSI;
                //ShowInsLog(str + "\r\n");
            }
            serialPort2.DiscardInBuffer();
            //serialPort2.DiscardOutBuffer();
        }

        private void aboutToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.StartPosition = FormStartPosition.CenterParent;
            about.Show();

            if (about.StartPosition == FormStartPosition.CenterParent)
            {
                var x = Location.X + (Width - about.Width) / 2;
                var y = Location.Y + (Height - about.Height) / 2;
                about.Location = new Point(Math.Max(x, 0), Math.Max(y, 0));
            }
        }
        private void QuitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (serialPort2.IsOpen)
            {
                CloseDown2 = new System.Threading.Thread(new System.Threading.ThreadStart(CloseSerial2OnExit));
                CloseDown2.Start();
            }
            if (serialPort1.IsOpen)
            {
                //serialPort1.Close();
                CloseDown1 = new System.Threading.Thread(new System.Threading.ThreadStart(CloseSerial1OnExit));
                CloseDown1.Start();
            }

            this.Close();
        }

        ushort crc16_ibm(byte[] data, byte length)
        {
            ushort crc = 0xFFFF;
            byte i = 0;
            byte j = 0;
            byte temp = 0; 
            byte oper = 0;
            ushort temp_short;


            for (i = 0; i < length; i++)
            {
                oper = data[i];
                if (true)
                {
                    temp = oper;
                    oper = 0;
                    for (j = 0; j < 8; j++)
                    {
                         
                        if ((temp & 0x01)>=1)
                            oper |= (byte) (0x01 << (7 - j));
                        temp >>= 1;
                    }
                }
                crc ^= oper;
                for (j = 0; j < 8; j++)
                {
                    if ((crc & 0x0001)>=1)
                    {
                        crc = (ushort) ((crc >> 1) ^ 0xA001); // A001=8005 bit inverse
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            if (true)
            {
                temp_short = crc;
                crc = 0;
                for (i = 0; i < 16; i++)
                {
                    if ((temp_short & 0x01)>=1)
                        crc |= (ushort) (0x01 << (15 - i));
                    temp_short >>= 1;
                }
            }
            return crc;// ^ CRC_XOROUT;
        }

        static ushort ComputeCrc16Ibm(byte[] data)
        {
            ushort crc = 0x0000;
            ushort CRC16_IBM_POLY = 0xA001;
            foreach (byte b in data)
            {
                crc ^= b;
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc = (ushort)((crc >> 1) ^ CRC16_IBM_POLY);
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;

        }
        public void ShowPktData(int len)
        {
            byte[] datas = new byte[len];
            for (uint i = 0; i < len; i++)
                datas[i] = (byte) (i & 0xFF);

            datas[0] = (byte)len;
            datas[1] = 0x46;
            datas[2] = 0x53;
            //txResult.Text = (len+2).ToString("X2")+" ";
            
            for (int j=0;j<len; j++)
            {
                logResultText.Text += datas[j].ToString("X2") + " ";
            }
            //txResult.Text += "\r\n";
            //int totalLen = len + 3;
            //byte[] data = { 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 }; // "123456789"
            ushort crc = crc16_ibm(datas, (byte)len);
            //ushort crc = ComputeCrc16Ibm(datas);
            //txResult.Text += "CRC="+crc.ToString("X4") + "\r\n";
            logResultText.Text += (crc & 0xFF).ToString("X2") + " " + (crc >> 8).ToString("X2")+"\r\n";            
        }

        
        private void dvbTstPktRxSetPayloadBtn_Click(object sender, EventArgs e)
        {
            SetPayloadProcess();
            
        }

        //private const int MaxHexChars = 125 * 2;
        private bool isEditing = false;
        private List<byte> tmpArray = new List<byte>();
        
        private void dvbTstPktTxPayloadText_TextChanged(object sender, EventArgs e)
        {
            int MaxHexChars = 123;
            int crcChoose = dvbTstPktTxCrcCombo.SelectedIndex;
            int maxLen = CRC_LEN_NO_SEQ;
            switch (crcChoose)
            {
                case 0:   //CCITT
                    maxLen = CRC_LEN_NO_SEQ;
                    break;
                case 1:    //CRC16_IBM
                    maxLen = CRC_LEN_NO_SEQ;
                    break;
                case 2:  // NO_CRC
                    maxLen = NO_CRC_LEN_NO_SEQ;
                    break;
            }
            if (!dvbTstPktTxRandomPlCheck.Checked)
            {
                if (isEditing) return;
                isEditing = true;

                // Step 1: 移除非十六進位字元並轉大寫
                string raw = Regex.Replace(dvbTstPktTxPayloadText.Text.ToUpper(), @"[^0-9A-F]", "");
                if (dvbTstPktTxPrependSeqCheck.Checked)
                    MaxHexChars = (maxLen-2) * 2;
                else
                    MaxHexChars = maxLen * 2;
                // Step 2: 限制最多 127 個 hex 字元
                if (raw.Length > MaxHexChars)
                {
                    raw = raw.Substring(0, MaxHexChars);
                    System.Media.SystemSounds.Beep.Play(); // 警告音
                }

                // Step 3: 重新格式化為每2位一組加空格
                StringBuilder formatted = new StringBuilder();
                tmpArray.Clear();
                int fullByteCount = raw.Length / 2;

                for (int i = 0; i < raw.Length; i++)
                {
                    formatted.Append(raw[i]);
                    if (i % 2 == 1 && i != raw.Length - 1)
                        formatted.Append(" ");
                }

                // Step 4: 將格式化後的字串寫回 TextBoxInput
                dvbTstPktTxPayloadText.Text = formatted.ToString();
                dvbTstPktTxPayloadText.SelectionStart = dvbTstPktTxPayloadText.Text.Length;

                // Step 5: 將完整兩位一組的 hex 字串轉成 byte
                try
                {
                    //tmpArray.Add(0); // 這是準備放tmpArray[0] 
                    for (int i = 0; i < fullByteCount * 2; i += 2)
                    {
                        string byteStr = raw.Substring(i, 2);
                        byte b = Convert.ToByte(byteStr, 16);
                        tmpArray.Add(b);
                    }
                }
                catch
                {
                    // 忽略格式錯誤
                }
                //tmpArray[0] = (byte)(tmpArray.Count - 1);
                // Step 6: 更新長度與 HEX 顯示
                dvbTstPktTxFilledLenText.Text = tmpArray.Count.ToString(); // tmpArray[0].ToString();   //tmpArray.Count.ToString();

                StringBuilder hexOutput = new StringBuilder();
                foreach (byte b in tmpArray)
                {
                    hexOutput.AppendFormat("{0:X2} ", b);
                }
                //txResult.Text = hexOutput.ToString().Trim();
                isEditing = false;
            }
            
        }
        static uint ReverseBits(uint value)
        {
            uint result = 0;

            for (int i = 0; i < 32; i++)
            {
                result <<= 1;
                result |= (value & 1);
                value >>= 1;
            }
            return result;
        }

        public string hexReverse(string hexInput)
        {
            string hexResult = "";
            uint value = Convert.ToUInt32(hexInput, 16);
            uint reversed = ReverseBits(value);
            hexResult = reversed.ToString("X8");
            return hexResult;
        }
        private void dvbTstPktTxTestBtn_Click(object sender, EventArgs e)
        {
            
            string hexInput = paramTxSyncText.Text;
            string hexResult = hexReverse(paramTxSyncText.Text);
            logResultText.Text = "0x"+hexInput + "=>" + hexResult;
            
            /*
            int crcValue = 0;
            byte[] bytes = new byte[2];
            crcValue = CRC_Calculation();
            bytes[0] = (byte)((crcValue >> 8) & 0xFF);
            bytes[1] = (byte)(crcValue & 0xFF);            
            string hexString = $"{bytes[0]:X2} {bytes[1]:X2}";
            txResult.Text += "\r\nCRC=" + hexString+"\r\n";
            */

        }

        /// <summary>
        /// DEVIATION 欄位旁的 "?" 按鈕: 建議使用 h=1 (即 Deviation = DataRate/2, 見 H=2*Dev/DataRate 的定義),
        /// 依目前 DATA RATE 算出建議值提示給使用者, 不會自動改欄位內容。
        /// </summary>
        private void btnDevComment_Click(object sender, EventArgs e)
        {
            double dataRateKbps;
            if (!double.TryParse(paramDataRateCombo.Text, out dataRateKbps))
            {
                GenMsg.Show("Please set DATA RATE first.", "DEVIATION");
                return;
            }

            double suggestedDevKHz = dataRateKbps / 2;
            GenMsg.Show(
                "Suggest using h = 1, i.e. Deviation = (DataRate*H) / 2.\r\n" +
                "Currently suitable value to fill in: " + suggestedDevKHz.ToString("0.###") + " KHz",
                "DEVIATION");
        }

        public int CRC_Calculation()
        {
            int crc = 0xFFFF;
            int i = 0;
            int j = 0;
            int temp = 0, oper;
            int temp_short;

            for (i = 0; i < tmpArray.Count; i++)
            {
                oper = tmpArray[i];
                if (true)
                {
                    temp = oper;
                    oper = 0;
                    for (j = 0; j < 8; j++)
                    {
                        if ((temp & 0x01)>0)
                            oper |= 0x01 << (7 - j);
                        temp >>= 1;
                    }
                }
                crc ^= oper;
                for (j = 0; j < 8; j++)
                {
                    if ((crc & 0x0001)>0)
                    {
                        crc = (crc >> 1) ^ 0xA001; // A001=8005 bit inverse
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            if (true)
            {
                temp_short = crc;
                crc = 0;
                for (i = 0; i < 16; i++)
                {
                    if ((temp_short & 0x01)>0)
                        crc |= 0x01 << (15 - i);
                    temp_short >>= 1;
                }
            }
            return crc;
        }

        public void DisableSetting()
        {
            if (this.InvokeRequired)
            {
                DelDisableSetting del = new DelDisableSetting(DisableSetting);
                this.Invoke(del);
            }
            else
            {
                paramBaseFreqText.Enabled = false;
                paramBaseFreqText.Update();
                paramDataRateCombo.Enabled = false;
                paramDataRateCombo.Update();
                paramDeviationText.Enabled = false;
                paramDeviationText.Update();
                paramModulationCombo.Enabled = false;
                paramModulationCombo.Update();
                paramPwrLvText.Enabled = false;
                paramPwrLvText.Update();
                paramCapIoText.Enabled = false;
                paramCapIoText.Update();
                paramTxSyncText.Enabled = false;
                paramTxSyncText.Update();
                paramTxSyncSoftInvertCheck.Enabled = false;
                paramTxSyncSoftInvertCheck.Update();
                dvbTstPktTxInfiniteCheck.Enabled = false;
                dvbTstPktTxInfiniteCheck.Update();
                dvbTstPktTxPrependSeqCheck.Enabled = false;
                dvbTstPktTxPrependSeqCheck.Update(); 
                dvbTstPktTxRandomPlCheck.Enabled = false;
                dvbTstPktTxRandomPlCheck.Update();
                //dvbTstPktTxCntText.Enabled = false;
                dvbTstPktTxRandomPlLenText.Enabled = false;
                dvbTstPktTxRandomPlLenText.Update();
                dvbTstPktTxPayloadText.Enabled = false;
                dvbTstPktTxPayloadText.Update();
                dvbTstPktTxCrcCombo.Enabled = false;
                dvbTstPktTxCrcCombo.Update();
            }
            

        }

        public void EnableSetting()
        {
            paramBaseFreqText.Enabled = true;
            paramDataRateCombo.Enabled = true;
            paramDeviationText.Enabled = true;
            paramModulationCombo.Enabled = true;
            paramPwrLvText.Enabled = true;
            paramCapIoText.Enabled=true;
            paramTxSyncText.Enabled = true;
            paramTxSyncSoftInvertCheck.Enabled = true;
            dvbTstPktTxInfiniteCheck.Enabled = true;
            dvbTstPktTxPrependSeqCheck.Enabled = true;
            dvbTstPktTxRandomPlCheck.Enabled = true;
            dvbTstPktTxCntText.Enabled = true;
            dvbTstPktTxRandomPlLenText.Enabled = dvbTstPktTxRandomPlCheck.Checked;
            dvbTstPktTxCrcCombo.Enabled = true;
            dvbTstPktTxCrcCombo.Update();
            dvbTstPktTxPayloadText.Enabled = true;
            dvbTstPktTxPayloadText.ReadOnly = dvbTstPktTxRandomPlCheck.Checked;
            dvbTstPktTxPayloadText.Update();
        }
        public void InfiniteStop()
        {
            if (serialPort1.IsOpen)
                serialPort1.Write("t");
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
                InfiniteStop();
            else
                logResultText.Text += "COMPORT ERROR!\r\n";
        }
        public void InfiniteStart()
        {            
            serialPort1.Write("rfg 21\r");
            Thread.Sleep(100);
        }

        public bool isSending = false;

        private int GetFifoLen()
        {
            int seqLen = dvbTstPktTxPrependSeqCheck.Checked ? 2 : 0;
            int crcLen = (dvbTstPktTxCrcCombo.SelectedIndex == 2) ? 0 : 2;  // NO_CRC = 0 bytes
            return 1 + tmpArray.Count + seqLen + crcLen;  // 1 = Length field
        }

        public void PacketSend(int num)
        {
            int delayTime = 100;
            int drValue = 100000;
            double drTextValue = 0.0;
            isSending = true;
            string cmd = "";
            cmd = "cfg 14 " + 1 + "\r";
            //cmd = "cfg 14 " + num + "\r";
            //serialPort1.WriteLine(cmd);
            this.Invoke((MethodInvoker)(() =>
            {
                dvbTstPktTxStopBtn.Enabled = true;
                dvbTstPktTxStartBtn.Enabled = false;
                ShowInsLog("Packet Send: " + GetFifoLen() + " bytes\r\n");
                drTextValue = double.Parse(paramDataRateCombo.Text);
            }));
            
            drValue = (int)(1000 * drTextValue);
            if (drValue >= 100000)
                delayTime = 100;
            else
            {
                delayTime = (1024 * 1000) / drValue;
                //ShowInsLog("DelayTime:" + delayTime + "; "+(delayTime * 4) +"\r\n");
                delayTime = delayTime * 10;   // 10
                if (delayTime<100)
                    delayTime = 100;
            }
                       
            for (int i = 0; i < num; i++)
            {
                if (!isSending)  // 如果 STOP 按了，直接跳出
                    break;
                serialPort1.WriteLine(cmd);
                Thread.Sleep(delayTime);
            }
            this.Invoke((MethodInvoker)(() =>
            {
                ShowInsLog("Packet Send Finished\r\n");
                isSending = false;
                dvbTstPktTxStartBtn.Enabled = true;
                dvbTstPktTxStopBtn.Enabled = false;
                EnableSetting();
            }));
        }
        bool IsAllNumber(string s)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(s, @"^\d+$");
        }
        private void dvbTstPktTxStartBtn_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                MessageBox.Show("Com Port is not connected", "Error");
                return;
            }
            ConfirmRewriteIfRegTableDirty();
            int lenOfHexTxt = Convert.ToInt32(dvbTstPktTxFilledLenText.Text);
            int lenOfRandomTxt = Convert.ToInt32(dvbTstPktTxRandomPlLenText.Text);
            int crcChoose = dvbTstPktTxCrcCombo.SelectedIndex;
            int byteLength = 0;
            bool isEmpty = false;
            int num = 0;
            if (string.IsNullOrWhiteSpace(dvbTstPktTxCntText.Text))
            {
                MessageBox.Show("Packet Num need 0<num<=200", "Error");
                return;
            }
            if (!int.TryParse(dvbTstPktTxCntText.Text, out int value))
            {
                MessageBox.Show("Only input numeric 0-9", "Error");
                return;
            }
            else
            {
                num = Convert.ToInt32(dvbTstPktTxCntText.Text);
                if (num > 200 || num <= 0)
                {
                    MessageBox.Show("Packet Num need 0<num<=200", "Warning");
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(dvbTstPktTxPayloadText.Text))
            {
                isEmpty = true;
            }
            int maxLen = CRC_LEN_NO_SEQ;
            switch (crcChoose)
            {
                case 0:   //CCITT
                    maxLen = CRC_LEN_NO_SEQ;
                    break;
                case 1:    //CRC16_IBM
                    maxLen = CRC_LEN_NO_SEQ;
                    break;
                case 2:  // NO_CRC
                    maxLen = NO_CRC_LEN_NO_SEQ;
                    break;
            }
            
            if (((lenOfHexTxt <= 0) && (lenOfRandomTxt <=0)) || (isEmpty))
            {
                ShowInsLog("ERROR: The Length of payload is 0 or payload is empty!\r\n");
                //ShowInsLog("=================================\r\n");
                return;
            }
            if (dvbTstPktTxRandomPlCheck.Checked)
                byteLength = lenOfRandomTxt;
            else
                byteLength = lenOfHexTxt;
            //------------------------------------------------------------
            if (byteLength > maxLen) {
                string str = "";
                if (crcChoose == 2) // NO_CRC
                {
                    if (dvbTstPktTxPrependSeqCheck.Checked)
                        str = "0 < Max Len (no CRC)<= 125 (add seq)";
                    else
                        str = "0 < Max Len (no CRC)<= 127";
                }
                else
                {
                    if (dvbTstPktTxPrependSeqCheck.Checked)
                        str = "0 < Max Len <= 123(add seq)";
                    else
                        str = "0 < Max Len <= 125";
                }
                MessageBox.Show(str, "Warning");
            }
            bool res = false;
            string cmd = "";
            //btnInfiniteStart.Enabled = false;
            if (serialPort1.IsOpen)
            {
                //serialPort1.Write("rfg 31 0\r");  // disbale IRQ
                //Thread.Sleep(50);
                // 2026-09-03 暫時關閉: ER4100_SetParameter() 是呼叫DVB韌體自己的cfg API算RF組態,
                // 只支援Xtal=40MHz, 跟REG TABLE/Generate算出來的值可能對不起來。之後要改成先用
                // REG TABLE的「Write DVB Register」直接寫暫存器, 這裡先跳過, 使用者自己按過
                // Write DVB Register 才按 Start。
                // res = ER4100_SetParameter(0);
                res = true;
                if (res)
                {
                    SetPayloadOrder();      // 0xA0A8: LSB=0 / MSB=1, 要在 cfg 之後才不會被蓋掉
                    Thread.Sleep(50);
                    SetAllowBitError();     // 0xB104: RX syncword 允許的 bit error 數
                    Thread.Sleep(50);
                    SetPayloadProcess();
                    Thread.Sleep(50);
                    serialPort1.Write("rfg 31 1\r");  // enable IRQ
                    Thread.Sleep(50);

                    if (dvbTstPktTxInfiniteCheck.Checked)
                    {
                        serialPort1.WriteLine("rfg 29 1\r"); // infinite 要加delaytime
                        Thread.Sleep(50);
                        //btnInfiniteStart.Text = "Stop";                    
                        dvbTstPktTxStartBtn.Enabled = false;
                        dvbTstPktTxStopBtn.Enabled = true;
                        ShowInsLog("Packet Send: " + GetFifoLen() + " bytes\r\n");

                        DisableSetting();
                        InfiniteStart();
                    }
                    else
                    {                        
                        serialPort1.WriteLine("rfg 29 0\r"); // 有限包由ap控制timing 不用在F/W加delaytime
                        Thread.Sleep(50);
                        
                        if (num <= 200 && num > 0)
                        {
                            DisableSetting();
                            Task.Run(() => PacketSend(num));
                        }
                        else
                            ShowInsLog("num need to be 0<num<=200");
                    }                  
                }
                else
                    ShowInsLog("Wrong Parameter! STOP!\r\n");
            }
            else
            {
                logResultText.Text += "COMPORT ERROR!\r\n";
                EnableSetting();
            }
                
        }

        private void dvbTstPktTxInfiniteCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (dvbTstPktTxInfiniteCheck.Checked)
            {
                dvbTstPktTxCntText.Enabled = false;
            }
            else
                dvbTstPktTxCntText.Enabled = true;
        }

        public void HexGenerate()
        {
            int byteLength = 0;
            //dvbTstPktTxPayloadText.Clear();
            try{
                byteLength = Convert.ToInt32(dvbTstPktTxRandomPlLenText.Text);
            }
            catch (Exception ex)
            {
                ;
            }
            //if (byteLength > 0 && byteLength <= 125)
            {
                byte[] buffer = new byte[byteLength];
                Random rand = new Random();
                rand.NextBytes(buffer);
                tmpArray.Clear();
                //tmpArray.Add((byte)byteLength);  // 注意轉型為 byte  // Owen:不傳送byteLength
                tmpArray.AddRange(buffer);

                StringBuilder sb = new StringBuilder(byteLength * 3); // 每個 byte 占 "?? " 共3個字元

                foreach (byte b in buffer)
                {
                    sb.AppendFormat("{0:X2} ", b); // 轉成兩位大寫十六進位
                }

                dvbTstPktTxPayloadText.Text = sb.ToString().Trim(); // 移除最後一個空格
            }
            /*
            else
                ShowInsLog("Payload length: 0<Len<=125\r\n");  */
        }
        // dvbTstPktTxRandomPlCheck/dvbTstPktTxRandomPlLenText 兩個一起作用就好: 勾選當下直接產生一次隨機payload(不用再另外按按鈕確認),
        // dvbTstPktTxRandomPlLenText 改變長度時(Leave事件, 見下面 dvbTstPktTxRandomPlLenText_Leave)也會直接重新產生。
        // dvbTstPktTxRandomPlLenText/dvbTstPktTxRandomPlLenUnitLabel 一律保持可見, 只依checkbox狀態切換Enabled, 不再Visible顯示/隱藏來回切換。
        private void dvbTstPktTxRandomPlCheck_CheckedChanged(object sender, EventArgs e)
        {
            dvbTstPktTxRandomPlLenText.Enabled = dvbTstPktTxRandomPlCheck.Checked;
            if (dvbTstPktTxRandomPlCheck.Checked) {
                dvbTstPktTxPayloadText.Clear();
                dvbTstPktTxPayloadText.ReadOnly = true;
                dvbTstPktTxPayloadText.BackColor = Color.FromArgb(32, 32, 32);
                dvbTstPktTxPayloadText.Update();
                tmpArray.Clear();
                dvbTstPktTxFilledLenText.Text = "0";
                dvbTstPktTxFilledLenText.BackColor = Color.White;
                dvbTstPktTxFilledLenText.Update();
                dvbTstPktTxRandomPlLenText.Text = "5";
                dvbTstPktTxFilledLenText.Visible = false;
                dvbTstPktTxFilledLenDescLabel.Visible = false;
                GenerateRandomPayload();
            }
            else
            {
                dvbTstPktTxFilledLenText.Visible = true;
                dvbTstPktTxFilledLenText.Text = "0";
                dvbTstPktTxFilledLenDescLabel.Visible = true;
                dvbTstPktTxPayloadText.BackColor = Color.FromArgb(32, 32, 32);
                dvbTstPktTxPayloadText.ReadOnly = false;
                dvbTstPktTxPayloadText.Update();
                dvbTstPktTxPayloadText.Clear();
                tmpArray.Clear();
            }
        }

        private void dvbTstPktTxRandomPlLenText_Leave(object sender, EventArgs e)
        {
            if (dvbTstPktTxRandomPlCheck.Checked)
                GenerateRandomPayload();
        }

        // 焦點還在 dvbTstPktTxRandomPlLenText 裡直接按 Enter 也重新產生一次, 不用先點別的地方讓焦點離開觸發 Leave。
        // SuppressKeyPress 擋掉 Enter 在單行 TextBox 上預設的警示音。
        private void dvbTstPktTxRandomPlLenText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (dvbTstPktTxRandomPlCheck.Checked)
                    GenerateRandomPayload();
            }
        }

        /// <summary>依 dvbTstPktTxRandomPlLenText 目前的長度重新產生一次隨機payload, 存進 tmpArray; randomCheckbox沒勾時不做事。原本是獨立的"Apply"按鈕(btnRandomGenerate)才會觸發, 現在勾選/改長度時直接呼叫。</summary>
        private void GenerateRandomPayload()
        {
            if (!dvbTstPktTxRandomPlCheck.Checked)
                return;

            int byteLength = 0;
            int crcChoose = dvbTstPktTxCrcCombo.SelectedIndex;
            int maxLen = CRC_LEN_NO_SEQ;
            switch (crcChoose){
                case 0 :   //CCITT
                    maxLen = CRC_LEN_NO_SEQ;
                    break;
                case 1:    //CRC16_IBM
                    maxLen = CRC_LEN_NO_SEQ;
                    break;
                case 2:  // NO_CRC
                    maxLen = NO_CRC_LEN_NO_SEQ;
                    break;
            }
            dvbTstPktTxPayloadText.Clear();
            try
            {
                byteLength = Convert.ToInt32(dvbTstPktTxRandomPlLenText.Text);
            }
            catch (Exception ex){
                ShowInsLog("0 <= Len <= 127");
                return;
            }
            if (dvbTstPktTxPrependSeqCheck.Checked)
                byteLength = byteLength + 2;
            if (byteLength > 0 && byteLength <= maxLen)
                HexGenerate();
            else
            {
                string str = "";
                if (crcChoose == 2) // NO_CRC
                {
                    if (dvbTstPktTxPrependSeqCheck.Checked)
                        str = "0 < NO_CRC Len <= 125 (add seq)";
                    else
                        str = "0 < NO_CRC Len <= 127";
                }
                else
                {
                    if (dvbTstPktTxPrependSeqCheck.Checked)
                        str = "0 < CRC Len <= 123(add seq)";
                    else
                        str = "0 < CRC Len <= 125";
                }
                MessageBox.Show(str, "Warning");
            }
        }

        private void dvbTstPktTxStopBtn_Click(object sender, EventArgs e)
        {
            if (dvbTstPktTxInfiniteCheck.Checked)
            {
                InfiniteStop();
                //btnInfiniteStart.Text = "Start";
            }
            isSending = false;
            dvbTstPktTxStopBtn.Enabled = false;
            dvbTstPktTxStartBtn.Enabled = true;
            ShowInsLog("Packet Stop\r\n");
            EnableSetting();
        }
        public static string cutTitle(string t, int len)
        {
            return t.Substring(0, len);
        }
        public void ApplyPASetting(PA_TYPE mode, string setPA)
        {
            List<string> allowList = PAConfigTable[(int)mode];
            bool found = allowList.Contains(setPA);
            if (!found)
            {
                MessageBox.Show("Setting is wrong", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 合法 → 套用到 ComboBox
            paramPAConfigCombo.Text = setPA.ToString();
        }

        public int getOneByteData(string stmp)
        {
            int ans = -1;
            //stmp = Regex.Replace(stmp, "[\r\n]", "");
            try{
                ans = Convert.ToInt32(stmp);
            }
            catch (Exception ex)
            {
                ans = -1;
                ShowInsLog("Return Data FAIL\r\n");
            }
            return ans;
        }
        public int CheckEepromExist()
        {
            int res = 0;
            string stmp = "";
            serialPort1.Write("romexist\r");
            Thread.Sleep(50);
            stmp = serialPort1.ReadExisting(); // org: ReadLine()

            // 用正則去除 \r\n 及其後的內容，再移除空格
            stmp = Regex.Replace(stmp, @"[\r\n].*$", "");
            stmp = stmp.Trim();

            //stmp = Regex.Replace(stmp, "[\r\n]", "");
            res = getOneByteData(stmp);
            return res;
        }

        public int ReadMaxPwrFromEeprom()
        {
            int pwrIdx = -1;
            string stmp = "";
            
            //serialPort1.WriteLine("xtrom 12\r");  // read back the power idx
            serialPort1.Write("user 6 27\r");  // read back the power idx
                                                       //0=10dBm, 1=14dBm, 2=16dBm, 3=20dBm, 4=23dBm
            Thread.Sleep(30);
            stmp = serialPort1.ReadLine();
            pwrIdx = getOneByteData(stmp);
            //maxPwrIdx = pwrIdx;
            //ShowInsLog("pwr idx=" + pwrIdx + "\r\n");

            return pwrIdx;
        }

        public int ReadFreqBandFromEeprom()
        {
            int rfBand = -1;
            int band_H = 0;
            int band_L = 0;
            string stmp = "";

            serialPort1.WriteLine("user 6 15\r");  // RF-band-H: 15, 
            Thread.Sleep(30);
            stmp = serialPort1.ReadLine();
            band_H = getOneByteData(stmp);

            stmp = serialPort1.ReadExisting();

            serialPort1.WriteLine("user 6 16\r");  // RF-band-L: 16
            Thread.Sleep(30);
            stmp = serialPort1.ReadLine();
            band_L = getOneByteData(stmp);
            //maxPwrIdx = pwrIdx;
            rfBand = (band_H & 0xFF) << 8 | (band_L & 0xFF);
            //ShowInsLog("RF Band=" + rfBand + "\r\n");

            return rfBand;
        }

        private void btnClearInsLog_Click(object sender, EventArgs e)
        {
            logResultText.Clear();
        }

        public void cwOnProcess()
        {
            string cmd = "txst 1\r";
            if (dvbTstContiTxSingleToneRadioBtn.Checked)
            {
                cmd = "txst 1\r";
                ShowInsLog("SingleTone CW ON\r\n");
            }
            else
            {
                if (dvbTstContiTxBit0101RadioBtn.Checked){
                    cmd = "txpr55\r";
                    ShowInsLog("CW (Pattern 0101).. ON\r\n");
                }
                else if (dvbTstContiTxPrbs9RadioBtn.Checked) {
                    cmd = "txprbs\r";
                    ShowInsLog("CW (Pattern PRBS9) ON\r\n");
                }
            }
            serialPort1.Write(cmd);
            
        }
        public void cwOffProcess()
        {
            string cmd = "txst 0\r";
            serialPort1.Write(cmd);
            ShowInsLog("CW OFF\r\n");
        }
        private void dvbTstContiTxStartBtn_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                MessageBox.Show("Com Port no connected", "Error");
                return;
            }
            ConfirmRewriteIfRegTableDirty();
            if (serialPort1.IsOpen)
            {
                // 2026-09-03 暫時關閉: 見 dvbTstPktTxStartBtn_Click 同樣的說明, 改成使用者自己先按
                // Write DVB Register 再操作。
                // ER4100_SetParameter(1);
                Thread.Sleep(50);
                serialPort1.Write("tx 1 30 0\r");
                Thread.Sleep(200);
                cwOnProcess();
                this.BeginInvoke((MethodInvoker)(() =>
                {                   
                    dvbTstContiTxSingleToneRadioBtn.Enabled = false;
                    dvbTstContiTxPatternRadioBtn.Enabled = false;
                    dvbTstContiTxStartBtn.Enabled = false;
                    dvbTstContiTxStopBtn.Enabled = true;
                }));
            }
            else
            {
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    dvbTstContiTxSingleToneRadioBtn.Enabled = true;
                    dvbTstContiTxPatternRadioBtn.Enabled = true;
                    dvbTstContiTxStartBtn.Enabled = true;
                    dvbTstContiTxStopBtn.Enabled = false;
                    ShowInsLog("COM PORT FAIL!\r\n");
                }));
            }
        }

        private void dvbTstContiTxStopBtn_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                cwOffProcess();
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    dvbTstContiTxStartBtn.Enabled = true;
                    dvbTstContiTxStopBtn.Enabled = false;
                    dvbTstContiTxSingleToneRadioBtn.Enabled = true;
                    dvbTstContiTxPatternRadioBtn.Enabled = true;
                }));
            }
            else
            {
                ShowInsLog("COM PORT FAIL!\r\n");
                dvbTstContiTxStartBtn.Enabled = true;
                dvbTstContiTxStopBtn.Enabled = false;
            }
        }

        private void DVBBandCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (highlowICType < 0)
                return;
            
            if (highlowICType == 0)
            {  // LOW POWER
                switch (paramDvbBandCombo.Text)
                {
                    case "433":
                        InitialPAConfigList((int)PA_TYPE.PA_915433L);
                        break;
                    case "915":
                        InitialPAConfigList((int)PA_TYPE.PA_915433L);
                        break;
                    case "868":
                        InitialPAConfigList((int)PA_TYPE.PA_868L);
                        break;

                    default:
                        break;
                }
            }         
        }

        public bool PacketRxBtnStopProcess()
        {
            bool res = false;
            string cmd = "";
            /*
            serialPort1.DataReceived -= UartReceiveDataByte;
            if (serialPort1.IsOpen)
            {
                serialPort1.DiscardInBuffer();   // 清掉尚未讀的資料
                serialPort1.Close();
            }*/
            if (serialPort1.IsOpen)
            {
                cmd = "rx 0\r"; 
                serialPort1.Write(cmd);
            }

             if (serialPort1.IsOpen)
                DetachUartRxHandler();
            //CloseSerial1OnExit();

             /*
            Thread.Sleep(50);
            if (connectComToolStripMenuItem.Text=="CLOSE")
            {
                Thread.Sleep(100);
                res = ConnectWithIRQ();
                if (!res)
                {
                    ShowInsLog("COM PORT ERROR\r\n");
                    connectComToolStripMenuItem.Text = "CONNECT";
                    refreshComToolStripMenuItem.Enabled = true;
                    comPortToolStripComboBox.Enabled = true;
                }
            }
             */

            this.BeginInvoke((MethodInvoker)(() =>
            {
                EnableRxControlsAfterStop();
            }));

            return res;
        }

        // Packet Rx 沒在開啟中(尚未開始 或 已停止/失敗)時,把所有相關參數控制項恢復成可設定狀態。
        // 供 PacketRxBtnStopProcess() 及 PacketRxStartProcess() 的失敗路徑共用,避免兩邊各自維護一份清單而漏改。
        public void EnableRxControlsAfterStop()
        {
            paramRxSyncSoftInvertCheck.Enabled = true;
            dvbTstPktRxScanBtn.Enabled = true;
            EnableParameterSetting();
            dvbTstPktRxStartBtn.Enabled = true;
            dvbTstPktRxCrcCombo.Enabled = true;
            dvbTstPktRxParseSeqCheck.Enabled = true;
            dvbTstPktRxCntLimitRadioBtn.Enabled = true;
            dvbTstPktRxCntNoLimitRadioBtn.Enabled = true;
            dvbTstPktRxCntText.Enabled = dvbTstPktRxCntLimitRadioBtn.Checked;
        }

        public void DisconnectUartProcess()
        {
            string cmd = "";
            cmd = "setperst 0\r";     // per=5 即用
            serialPort1.WriteLine(cmd);
            Thread.Sleep(50);
            // ---------------------------
            cmd = "settestst 0\r";     // 不印一般 LOG
            serialPort1.WriteLine(cmd);
            Thread.Sleep(50);
            // ---------------------------

            dvbTstPktRxStartBtn.Enabled = true;
        }
        void SetStateButton(System.Windows.Forms.Button btn, bool enable)
        {
            btn.Enabled = enable;
            btn.UseVisualStyleBackColor = false;
            btn.FlatStyle = FlatStyle.Flat;

            if (enable)
            {
                // 淺藍（可點狀態）
                btn.BackColor = Color.FromArgb(0, 122, 204);
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = Color.FromArgb(0, 90, 158);
                btn.FlatAppearance.MouseOverBackColor = btn.BackColor;
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 90, 158);
            }
            else
            {
                // 灰色（已鎖定/執行中）
                btn.BackColor = Color.FromArgb(160, 160, 160);
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = Color.FromArgb(120, 120, 120);
                btn.FlatAppearance.MouseOverBackColor = btn.BackColor;
                btn.FlatAppearance.MouseDownBackColor = btn.BackColor;
            }
        }
        private void dvbTstPktRxStartBtn_Click(object sender, EventArgs e)
        {
            bool res = false;
            averageRssi = 0;
            totalRcv = 0;
            totalRssi = 0;
            rcvOK = 0;
            rcvErr = 0;

            if (!serialPort1.IsOpen)
            {
                MessageBox.Show("Com Port no connected","Error");
                return;
            }

            if (dvbTstPktRxStartBtn.Text == "Start RX")
            {
                ConfirmRewriteIfRegTableDirty();
                this.BeginInvoke((MethodInvoker)(() =>
                {
                    //paramRxSyncSoftInvertCheck.Enabled = false;
                    dvbTstPktRxLogText.Clear();
                    //btnRxResultClr.Enabled = false;
                    dvbTstPktRxOkCntText.Text = "";
                    dvbTstPktRxErrCntText.Text = "";
                    dvbTstPktRxRssiText.Text = "";
                    dvbTstPktRxScanBtn.Enabled = false;   // Scan RSSI 跟 Start RX 互斥, RX 開始時鎖住 Scan RSSI
                    //SetStateButton(dvbTstPktRxStartBtn, false);
                    DisableParameterSetting();
                }));

                res = PacketRxStartProcess();
                if (res)
                {
                    ShowInsLog("RX Reception Start\r\n");
                    dvbTstPktRxStartBtn.Text = "Stop RX";
                }
                //if (serialPort1.IsOpen)
                //    DisconnectUartProcess();

            }
            else if (dvbTstPktRxStartBtn.Text == "Stop RX")
            {
                PacketRxBtnStopProcess();
                ShowInsLog("RX Reception Stop\r\n");
                dvbTstPktRxStartBtn.Text = "Start RX";
            }
            
        }
        // dvbTstPktRxCntLimitRadioBtn("Max Packet Count")/dvbTstPktRxCntNoLimitRadioBtn("No limitation") 共用同一個handler,
        // 兩顆 RadioButton 的 CheckedChanged 都會掛這個 —— 選了 Max Packet Count 才讓 dvbTstPktRxCntText 可編輯。
        private void packetRxCountMode_CheckedChanged(object sender, EventArgs e)
        {
            dvbTstPktRxCntText.Enabled = dvbTstPktRxCntLimitRadioBtn.Checked;
        }

        private void btnRxResultClr_Click_1(object sender, EventArgs e)
        {
            dvbTstPktRxLogText.Clear();
        }

        private void dvbTstContiTxPatternRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            dvbTstContiTxPatternGroup.Visible = true;
        }

        private void dvbTstContiTxSingleToneRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            dvbTstContiTxPatternGroup.Visible = false;
        }

        private void dvbTstPktTxPrependSeqCheck_CheckedChanged(object sender, EventArgs e)
        {
            //addSeqLevel.Visible = dvbTstPktTxPrependSeqCheck.Checked;
            RefreshPacketFormatDiagram();
        }

        private void dvbTstPktTxCrcCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPacketFormatDiagram();
        }

        private void preambleLen_TextChanged(object sender, EventArgs e)
        {
            RefreshPacketFormatDiagram();
        }

        private void SetFifoSegmentState(Label box, Label lenLabel, bool active, string activeLenText)
        {
            box.BackColor = active ? Color.LightBlue : Color.White;
            box.ForeColor = active ? Color.Black : Color.Gray;
            lenLabel.Text = active ? activeLenText : "0Bytes";
        }

        private void RefreshPacketFormatDiagram()
        {
            int preambleBytes;
            if (!int.TryParse(paramPbLenText.Text.Trim(), out preambleBytes))
                preambleBytes = 0;
            dvbTstPktTxDgmPbLenLabel.Text = preambleBytes + "Bytes";

            // paramTxSyncActualLabel.Text 是 "Transfer :5A 0F BE 66" 這種格式 (前綴 + 冒號 + 空白分隔的hex byte),
            // 取冒號後面的hex字元數 /2 就是實際的syncword bytes數, 這樣invert切換/沒打完整都跟著真實狀態走
            string syncHexPart = Regex.Replace(paramTxSyncActualLabel.Text, @"^[^:]*:", "");
            string syncHexOnly = Regex.Replace(syncHexPart, "[^0-9A-Fa-f]", "");
            dvbTstPktTxDgmSyncLenLabel.Text = (syncHexOnly.Length / 2) + "Bytes";

            bool hasSeq = dvbTstPktTxPrependSeqCheck.Checked;
            SetFifoSegmentState(dvbTstPktTxDgmSeqNumLabel, dvbTstPktTxDgmSeqNumLenLabel, hasSeq, "2Bytes");

            bool hasCrc = dvbTstPktTxCrcCombo.SelectedIndex != 2;   // 2 = "No CRC"
            SetFifoSegmentState(dvbTstPktTxDgmCrcLabel, dvbTstPktTxDgmCrcLenLabel, hasCrc, "2Bytes");

            // Length(1) + SeqNum(0/2) + Payload + CRC(0/2) = FIFO_MAX_LEN(128)
            int maxPayload = FIFO_MAX_LEN - 1 - (hasSeq ? 2 : 0) - (hasCrc ? 2 : 0);
            dvbTstPktTxDgmPayloadLenLabel.Text = "Max " + maxPayload + " Bytes";
        }

        private void dvbTstStateSetBtn_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                MessageBox.Show("Com port not connected", "Warning");
                return;
            }
            ConfirmRewriteIfRegTableDirty();
            string cmd = "";
            if (dvbTstStateIdleRadioBtn.Checked)
            {
                cmd = "pm 0\r";
                serialPort1.Write(cmd);
                Thread.Sleep(300);
                cmd = "rx 0\r";
                serialPort1.Write(cmd);
                ShowInsLog("Set idle mode OK\r\n");
            }
            else if (dvbTstStateDeepSleepRadioBtn.Checked) {
                cmd = "pm 1\r";
                serialPort1.Write(cmd);
                ShowInsLog("Set deep sleep mode OK\r\n");
            }
            else if (dvbTstStateShutdownRadioBtn.Checked)
            {
                cmd = "pm 2\r";
                serialPort1.Write(cmd);
                ShowInsLog("Set shutdown mode OK\r\n");
            }
        }
        private int GetRSSI(string str)
        {
            int rssidBm = 333;
            int len = str.Length;
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            if (len == 3){
                try
                {
                    rssidBm = (-1) * Convert.ToInt32(str.Substring(0, 3));
                }
                catch (Exception ex)
                {
                    rssidBm = 333;
                }
            }
            return rssidBm;
        }
        public void DoScanProcess()
        {
            int rssi = -200;
            string cmd = "";
            string stmp = "";
            cmd = "getchrssi " + getrssiTimes + "\r";   // 20
            serialPort1.Write(cmd);
            Thread.Sleep(50);
            stmp = serialPort1.ReadExisting();
            rssi = GetRSSI(stmp);
            this.Invoke((MethodInvoker)(() =>
            {
                dvbTstPktRxRssiText.Text = rssi.ToString();
            }));
            string timestamp = DateTime.Now.ToString("HH:mm:ss:fff");
            ShowRxPacket(timestamp + " RSSI : " + rssi + "dBm\r\n");
        }
        private void OnTimedScanFreqProcess(Object source, ElapsedEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                DoScanProcess();
            }
            else
            {               
                StopScanTimer();
            }
        }
        private void InitScanTimer()
        {
            if (scanTimer != null) return;   // 避免重複new

            scanTimer = new System.Timers.Timer();
            scanTimer.Interval = 500;        // 依你需求
            scanTimer.AutoReset = true;
            scanTimer.Elapsed += new ElapsedEventHandler(OnTimedScanFreqProcess);
            //scanTimer.Enabled = true;
            scanTimer.Start();
        }

        private void StopScanTimer()
        {
            if (scanTimer == null) return;
            //scanTimer.Enabled = false;
            scanTimer.Stop();
            scanTimer.Dispose();
            scanTimer = null;
        }
        public void ScanStop()
        {
            string cmd = "";
            cmd = "setregb19c 0\r";
            serialPort1.Write(cmd);
            StopScanTimer();
            EnableParameterSetting();
            dvbTstPktRxScanBtn.Text = "Scan RSSI";
            hScanInit = false;
            dvbTstPktRxStartBtn.Enabled = true;
            dvbTstPktRxOkCntText.Enabled = true;
            dvbTstPktRxErrCntText.Enabled = true;
        }

        public void ScanStart()
        {
            bool res = false;
            string cmd = "";
            string stmp = "";
            int tmpRssi = -118;
            dvbTstPktRxScanBtn.Text = "Scan Off";
            cmd = "settestst 1\r";

            serialPort1.Write(cmd);
            Thread.Sleep(30);
            // 2026-09-03 暫時關閉: 見 dvbTstPktTxStartBtn_Click 同樣的說明, 改成使用者自己先按
            // Write DVB Register 再操作。
            // res = ER4100_SetParameter(2);
            res = true;
            if (!res)
            {
                ShowInsLog("Set Pararmeter Error!\r\n");
                return;
            }
            cmd = "rx 0\r";
            res = ER4100_SendCmd(cmd);
            Thread.Sleep(50);
            cmd = "setregb19c 3\r";
            res = ER4100_SendCmd(cmd);
            Thread.Sleep(50);
            stmp = serialPort1.ReadExisting();
            dvbTstPktRxScanBtn.Text = "Scan Stop";
            dvbTstPktRxStartBtn.Enabled = false;
            dvbTstPktRxOkCntText.Enabled = false;
            dvbTstPktRxErrCntText.Enabled = false;
            InitScanTimer();
            hScanInit = true;
            this.BeginInvoke((MethodInvoker)(() =>
            {
                dvbTstPktRxLogText.Clear();
                //btnRxResultClr.Enabled = false;
                dvbTstPktRxOkCntText.Text = "";
                dvbTstPktRxErrCntText.Text = "";
                dvbTstPktRxRssiText.Text = "";
                DisableParameterSetting();
            }));
        }
        public void ScanProcess()
        {
            if (!serialPort1.IsOpen)
            {
                MessageBox.Show("Com Port not connected", "Error");
                return;
            }
            if (dvbTstPktRxScanBtn != null && dvbTstPktRxScanBtn.Text == "Scan RSSI")
            {
                ConfirmRewriteIfRegTableDirty();
                ScanStart();
            }
            else
            {
                ScanStop();
            }
        }
        private void btnScanRssi_Click(object sender, EventArgs e)
        {
                // Set Parameter
                // Set REG_B19C_3
                // SetScanRssi
                // init timer
                // Read RSSI
                ScanProcess();
        }

        private void dvbTstPktRxClearLogBtn_Click(object sender, EventArgs e)
        {
            dvbTstPktRxLogText.Clear();
        }

        private void pageParameter_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void txModCombo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pageParameter_Click_1(object sender, EventArgs e)
        {

        }

        private ContextMenuStrip genMenu;

        /// <summary>按下 GENERATE 先跳出選單: 要照 REG TABLE 目前的 Topic/DependsOn 過濾結果只產生通過的 register, 還是產生全部</summary>
        private void btnGen_Click(object sender, EventArgs e)
        {
            if (genMenu == null)
            {
                genMenu = new ContextMenuStrip();
                genMenu.Items.Add("Generate filter result", null, (s, ev) => RunGenerate(true));
                genMenu.Items.Add("Generate all result", null, (s, ev) => RunGenerate(false));
            }
            genMenu.Show(regTableGenBtn, new Point(0, regTableGenBtn.Height));
        }

        /// <summary>filterOnly=true 時只有目前通過 REG TABLE Topic/DependsOn 過濾的位址會被 CodeOutHelper 寫出 (見 GenerateAllowedAddrs); false 就是原本的「全部產生」</summary>
        private void RunGenerate(bool filterOnly)
        {
            string apiVersion = "V4.04";
            if (paramDataFormat8BitsRadioBtn.Checked)
                is8BitsFormat = true;
            else
                is8BitsFormat = false;

            GenerateAllowedAddrs = filterOnly ? BuildGenerateAllowedAddrs() : null;
            GenerateValueOverrides = BuildGenerateValueOverrides();
            bool res;
            try
            {
                res = GenerateTxt(apiVersion);
            }
            finally
            {
                GenerateAllowedAddrs = null;
                GenerateValueOverrides = null;
            }

            if (res)
            {
                using (CodeGenPreviewForm previewForm = new CodeGenPreviewForm(generatedText))
                {
                    previewForm.SaveClicked += (s, ev) => SaveGeneratedTextToFile(previewForm.PreviewText);
                    previewForm.ShowDialog(this);
                }
            }
        }

        /// <summary>依 REG TABLE 目前的 Topic/DependsOn 勾選狀態 (PassesFilter, 定義在 RegDumpProcess.cs), 收集通過的 register 位址</summary>
        private HashSet<ushort> BuildGenerateAllowedAddrs()
        {
            HashSet<ushort> allowed = new HashSet<ushort>();
            foreach (RegTableItem it in RegTableItems.Items)
                if (PassesFilter(it))
                    allowed.Add(it.Addr);
            return allowed;
        }

        /// <summary>REG TABLE 目前顯示的每一顆 register 值 (GetCurrentRegTableValue, 定義在 RegDumpProcess.cs), 不受 Topic/DependsOn 過濾影響 —— 過濾只決定輸出裡「有沒有這顆」, 這裡決定「有的話值是多少」</summary>
        private Dictionary<ushort, uint> BuildGenerateValueOverrides()
        {
            Dictionary<ushort, uint> overrides = new Dictionary<ushort, uint>();
            foreach (RegTableItem it in RegTableItems.Items)
                overrides[it.Addr] = GetCurrentRegTableValue(it);
            return overrides;
        }

        //private void btnDefault_Click(object sender, EventArgs e)
        //{

        //}

        private void btnReadDVB_Click(object sender, EventArgs e)
        {
            btnRegDumpFromDVB();// sender, e);
        }

        private void btnWriteDVB_Click(object sender, EventArgs e)
        {
            btnRegWriteToDVB();// sender, e);
        }

        /*
private void btnICCheck_Click(object sender, EventArgs e)
{
   ReadEepromData();
} */

        /* Hold 1
        private void dvbTstPktTxPayloadText_TextChanged(object sender, EventArgs e)
        {
            string hexString = dvbTstPktTxPayloadText.Text.Replace(" ", ""); // 移除空格
            tmpArray.Clear();

            if (hexString.Length % 2 != 0)
            {
                // 如果是奇數長度，先暫時不要轉換，直到輸入完整兩個字元
                lenOfTrans.Text = tmpArray.Count.ToString();
                return;
            }

            try
            {
                for (int i = 0; i < hexString.Length; i += 2)
                {
                    string byteString = hexString.Substring(i, 2);
                    byte value = Convert.ToByte(byteString, 16);
                    tmpArray.Add(value);
                }

                // 更新長度顯示
                lenOfTrans.Text = tmpArray.Count.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid hex string (e.g. 0A1B2C)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        } */


        /* Hold 2
        private void dvbTstPktTxPayloadText_TextChanged(object sender, EventArgs e)
        {         
            if (isEditing) return;
            isEditing = true;

            // Step 1: 移除非十六進位字元並轉大寫
            string raw = Regex.Replace(dvbTstPktTxPayloadText.Text.ToUpper(), @"[^0-9A-F]", "");
            StringBuilder formatted = new StringBuilder();
            tmpArray.Clear();

            // Step 2: 將每兩位加空格，並同時填入 tmpArray
            int fullByteCount = raw.Length / 2;

            for (int i = 0; i < raw.Length; i++)
            {
                formatted.Append(raw[i]);
                if ((i % 2 == 1) && (i != raw.Length - 1))
                    formatted.Append(" ");
            }

            // Step 3: 將格式化後的字串寫回 TextBoxInput
            dvbTstPktTxPayloadText.Text = formatted.ToString();
            dvbTstPktTxPayloadText.SelectionStart = dvbTstPktTxPayloadText.Text.Length;

            // Step 4: 將完整的兩位一組轉成 byte 存入 tmpArray
            try
            {
                for (int i = 0; i < fullByteCount * 2; i += 2)
                {
                    string byteStr = raw.Substring(i, 2);
                    byte b = Convert.ToByte(byteStr, 16);
                    tmpArray.Add(b);
                }
            }
            catch
            {
                // 忽略錯誤（例如格式錯），但你也可以選擇提示
            }

            // Step 5: 更新顯示長度與 HEX 輸出
            lenOfTrans.Text = tmpArray.Count.ToString();
            
            StringBuilder hexOutput = new StringBuilder();
            foreach (byte b in tmpArray)
            {
                hexOutput.AppendFormat("{0:X2} ", b);
            }
            txResult.Text = hexOutput.ToString().Trim();

            isEditing = false;
        }
        */
        // string to hexstring
        //string prefixedHex = hexTxt.Text;
        //uint intValue = (UInt32)Convert.ToInt32(prefixedHex, 16);
        //txtValue.Text += "0x"+prefixedHex +"= " + intValue + "\r\n";
    }
}
