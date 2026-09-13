
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
using System.Reflection;
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

        private string initTitle = "ER41xx EZ TOOLKIT";

        // 產品名稱/版本標籤: 視窗標題、About頁、存檔檔頭(ToolTitle)三個地方共用同一個來源,
        // 避免像之前那樣各自維護版本號跟日期、結果三邊互相對不上。日期不手動維護,
        // 直接取這次執行的exe/dll實際編譯連結(link)時間。
        public const string ToolName = "ER41xx EzToolkit";
        public const string ToolVersionLabel = "V1.20L";

        private static string BuildToolTitle()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            DateTime buildDate = File.GetLastWriteTime(exePath);
            return ToolName + " " + ToolVersionLabel + "     " + buildDate.ToString("yyyyMMdd") ;
        }

        public static readonly string ToolTitle = BuildToolTitle();

        private Controller txSerial;
        private Controller2 rxSerial;
        private Controller3 pcbSerial;
        RegIDs          regIDs;
        RegNames        regNames;
        RegContents     regContents;
        FsPos           fsPos;
        FSMask          fsMask;
        BitRange        bitRange;
        
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
        public string[] xtalGainCtrl = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" };
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
            InitialXtalGainList();
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

        /*-------------------------------------------------------------
         *  InitialFreqBandList
         * [物件控制] - 刷新paramDvbBandCombo
         * 把dvbBandTable[idx]這组頻段選項填進DVB Band下拉選單(paramDvbBandCombo)，
         * 填之前先清空舊選項，預設選第0項。idx=0為Low Power({"433","868","915"})，
         * idx=1為High Power({"433","915"})，實際傳哪個值由連線後讀到的PA/IC類型決定。
         * Param :
         *   idx - dvbBandTable的索引 (0=Low Power, 1=High Power)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        public void InitialFreqBandList(int idx)
        {
            paramDvbBandCombo.Items.Clear();
            paramDvbBandCombo.Items.AddRange(dvbBandTable[idx].ToArray());
            paramDvbBandCombo.SelectedIndex = 0;
        }

        /*-------------------------------------------------------------
         *  SetDVBBandComboDisconnected
         * [物件控制] - 刷新paramDvbBandCombo
         * DVBBandCombo純粹反映DVB板子(EEPROM/IC類型)的狀態，跟PARAMETER/REG TABLE的
         * register計算完全無關(GenerateTxt()不會讀它)。沒連線時這顆板子相關的資訊
         * 根本無意義，鎖住並顯示"Don't care"，避免使用者誤以為選它會影響任何計算結果。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void SetDVBBandComboDisconnected()
        {
            paramDvbBandCombo.Items.Clear();
            paramDvbBandCombo.Items.Add("Don't care");
            paramDvbBandCombo.SelectedIndex = 0;
            paramDvbBandCombo.Enabled = false;
        }

        /*-------------------------------------------------------------
         *  UpdateDvbConnStateLabel
         * [物件控制] - 刷新dvbTstConnStateLabel
         * 依isDVBConnected目前的狀態更新DVB Tester分頁標題旁的連線狀態標籤:
         * 已連線顯示"Connected"(深綠), 未連線顯示"[Disconnected]"(深紅)。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void UpdateDvbConnStateLabel()
        {
            if (isDVBConnected)
            {
                dvbTstConnStateLabel.Text = "[Connected]";
                dvbTstConnStateLabel.ForeColor = Color.DarkGreen;
            }
            else
            {
                dvbTstConnStateLabel.Text = "[Disconnected]";
                dvbTstConnStateLabel.ForeColor = Color.DarkRed;
            }
        }

        /*-------------------------------------------------------------
         *  InitialPAConfigList
         * [物件控制] - 刷新paramPAConfigCombo
         * 把PAConfigTable[idx]這组PA Config選項填進PA CONFIG下拉選單(paramPAConfigCombo)，
         * 填之前先清空舊選項，預設選第0項。idx對應PA_TYPE列舉
         * (0=PA_915433L→{"10"}, 1=PA_868L→{"14"}, 2=PA_915433H→{"20","23"})，
         * 實際傳哪個值由連線後讀到的PA/IC類型決定。
         * Param :
         *   idx - PAConfigTable的索引 (對應PA_TYPE列舉值)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        public void InitialPAConfigList(int idx)
        {
            paramPAConfigCombo.Items.Clear();
            paramPAConfigCombo.Items.AddRange(PAConfigTable[idx].ToArray());
            paramPAConfigCombo.SelectedIndex = 0;
        }
        /*-------------------------------------------------------------
         *  InitialXtalList
         * [物件控制] - 刷新paramXtalCombo
         * 把固定清單xtalArray({"40","38.4"}, 單位MHz)填進PARAMETER分頁的
         * Xtal下拉選單(paramXtalCombo)，填之前先清空舊選項，預設選第0項(40MHz)。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        public void InitialXtalList()
        {
            paramXtalCombo.Items.Clear();
            paramXtalCombo.Items.AddRange(xtalArray.ToArray());
            paramXtalCombo.SelectedIndex = 0;
        }

        /*-------------------------------------------------------------
         *  InitialXtalList
         * [物件控制] - paramCapIoGainText
         * xtalGainCtrl 填進PARAMETER分頁的
         * Xtal下拉選單(paramCapIoGainText)，填之前先清空舊選項，預設選 4。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        public void InitialXtalGainList()
        {
            paramXtalGainCombo.Items.Clear();
            paramXtalGainCombo.Items.AddRange(xtalGainCtrl.ToArray());
            paramXtalGainCombo.SelectedItem = "4";
        }

        /*-------------------------------------------------------------
         *  GetDataRightNow
         * 舊版1580流程用的資料回呼: 依mode把讀回的字串存到readBackData, 並清對應的
         * 等待旗標(isWaitingData/isGetBackData/isShowIOData)。非UI thread呼叫時用
         * Invoke切回UI thread再執行一次。
         * Param :
         *   str - 讀回的資料字串
         *   mode - 0=自動頻率校正 / 1=1580讀回 / 2=只顯示IO值
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  SetController / SetController2 / SetController3
         * 注入對應COM連線用的Controller/Controller2/Controller3物件, 供後續收發使用。
         * Param :
         *   txSerial/rxSerial/pcbSerial - 要注入的Controller物件
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  RcvDataToReg
         * 從rcvRegArray(起始flag=64即'@')裡切出接續的8個byte當作讀到的register值字串,
         * 依目前是哪個等待旗標(isWaitingData/isGetBackData/isShowIOData/isAutoFreqCal2)
         * 轉呼叫GetDataRightNow()對應的mode。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  rcvRSSIProcess2
         * 把收到的byte逐一塞進rcvRegArray, 遇到連續兩個'&'(38)判定為結尾, 記下rcvEnd。
         * 目前呼叫端(RcvDataToArray)已刪除, 保留原始邏輯未變動。
         * Param :
         *   dataR - 這次收到的原始byte
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  rcvRSSIProcess
         * [物件控制] - 刷新dvbTstPktRxOkCntText/dvbTstPktRxRssiText
         * 解析Packet RX收到的"$xxx#&"格式資料: 每收到一組"#&"就算一包, 累加收包計數
         * (dvbTstPktRxOkCntText), 並從"&"前4個byte取出RSSI值(取負號後)顯示到
         * dvbTstPktRxRssiText。
         * Param :
         *   dataR - 這次收到的原始byte
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  ReadRegisterProcess
         * 舊版讀register流程: isReadRegister為true期間, 把收到的byte塞進rcvRegArray,
         * 遇到連續兩個'&'(38)判定為結尾, 呼叫RcvDataToReg()解析並關掉isReadRegister。
         * Param :
         *   dataR - 這次收到的原始byte
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  OpenComEvent
         * [物件控制] - 刷新autoSendcbx/autoReplyCbx
         * txSerial (COM1)開啟結果的回呼: 開成功就啟用auto send/auto reply checkbox,
         * 失敗則停用。非UI thread呼叫時用Invoke切回UI thread再執行一次。
         * Param :
         *   sender - 未使用
         *   e - 內含isOpend (是否開啟成功)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  OpenComEvent2
         * rxSerial (COM2)開啟結果的回呼, 目前兩個分支都是空的(舊版3-COM架構的殘留), 只保留
         * InvokeRequired轉UI thread的骨架。
         * Param :
         *   sender - 未使用
         *   e - 內含isOpend (是否開啟成功)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  OpenComEvent3
         * [物件控制] - 刷新autoSendcbx/autoReplyCbx
         * pcbSerial (COM3)開啟結果的回呼: 開成功就啟用auto send/auto reply checkbox,
         * 失敗則不處理。
         * Param :
         *   sender - 未使用
         *   e - 內含isOpend (是否開啟成功)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  CloseComEvent
         * [物件控制] - 刷新autoSendcbx (停用+停止autoSendtimer)
         * txSerial (COM1)關閉結果的回呼: 關成功就停用auto send checkbox並停掉自動送出計時器。
         * Param :
         *   sender - 未使用
         *   e - 內含isOpend (關閉後應為false)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  CloseComEvent2
         * rxSerial (COM2)關閉結果的回呼, 目前分支是空的(舊版3-COM架構的殘留), 只保留
         * InvokeRequired轉UI thread的骨架。
         * Param :
         *   sender - 未使用
         *   e - 內含isOpend (關閉後應為false)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  CloseComEvent3
         * pcbSerial (COM3)關閉結果的回呼, 分支內容已全部註解掉(舊版3-COM架構的殘留),
         * 只保留InvokeRequired轉UI thread的骨架。
         * Param :
         *   sender - 未使用
         *   e - 內含isOpend (關閉後應為false)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  ComReceiveDataEvent
         * txSerial (COM1)收到資料的回呼: 字串顯示模式(recStrRadiobtn勾選)下, isRSSIAnalysis
         * 開啟時轉呼叫rcvRSSIProcess()解析RSSI封包; hex顯示模式的分支目前已註解掉。
         * Param :
         *   sender - 未使用
         *   e - 內含receivedBytes (收到的原始資料)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  ComReceiveDataEvent2
         * rxSerial (COM2)收到資料的回呼, 目前body是空的(舊版3-COM架構的殘留), 只保留
         * InvokeRequired轉UI thread的骨架。
         * Param :
         *   sender - 未使用
         *   e - 內含receivedBytes (收到的原始資料)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  ComReceiveDataEvent3
         * pcbSerial (COM3)收到資料的回呼, 兩個顯示模式分支內容都已註解掉(舊版3-COM架構的
         * 殘留), 只保留InvokeRequired轉UI thread的骨架。
         * Param :
         *   sender - 未使用
         *   e - 內含receivedBytes (收到的原始資料)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  statustimer_Tick
         * [物件控制] - 刷新statusTimeLabel
         * 狀態列時鐘計時器: 每次Tick把statusTimeLabel更新成目前系統時間。
         * Param :
         *   sender, e - Tick事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void statustimer_Tick(object sender, EventArgs e)
        {
            this.statusTimeLabel.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        /*-------------------------------------------------------------
         *  autoSendtimer_Tick
         * 舊版自動送出計時器的Tick處理, 內容已註解掉, 目前是空函式。
         * Param :
         *   sender, e - Tick事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void autoSendtimer_Tick(object sender, EventArgs e)
        {
            //txsendbtn_Click(sender, e);
        }

        /*-------------------------------------------------------------
         *  sendIntervalTimetbx_KeyPress
         * 過濾自動送出間隔輸入框的按鍵: 只放行數字跟Backspace, 其餘按鍵吃掉。
         * Param :
         *   sender - 未使用
         *   e - KeyPress事件參數, 取其KeyChar判斷/設定Handled
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  addCRCcbx_CheckedChanged
         * Add CRC checkbox變動的處理, 目前是空函式(尚未實作)。
         * Param :
         *   sender, e - CheckedChanged事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void addCRCcbx_CheckedChanged(object sender, EventArgs e)
        {

        }

        /*-------------------------------------------------------------
         *  SaveInitValue
         * 跳出存檔對話框, 把regList/regName/regValue(舊版REG WR子系統的資料)整理成
         * 對齊欄位的文字檔存檔。由SaveInitValueToolStripMenuItem_Click呼叫, 但其父選單
         * saveToolStripMenuItem("Save")的Visible=false, 目前UI上點不到。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
                

        /*-------------------------------------------------------------
         *  MainForm_Load
         * [物件控制] - 刷新this.Text(視窗標題)/PARAMETER分頁/REG TABLE/dvbTstPktTxDgm等
         * 封包格式圖
         * 開機第二階段: 用ToolTitle蓋掉Designer.cs寫死的視窗標題、套上官方預設參數
         * (ApplyDefaultParameters)、用畫面參數重算REG TABLE(ApplyUiValuesToRegisterList)、
         * 掛上參數變動自動重算(HookParameterChangeEvents)、刷新封包格式圖
         * (RefreshPacketFormatDiagram)。細節見各行內註解。
         * Param :
         *   sender, e - Load事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = ToolTitle;   // 蓋掉Designer.cs裡寫死的舊標題文字, 改用ToolTitle這個唯一來源
            FitToScreen();   // v1.19L: 螢幕夠大就維持原始大小(無捲軸), 螢幕不夠大才啟用捲軸
            dvbTstContiTxPatternGroup.Enabled = false;

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
        /*-------------------------------------------------------------
         *  FullInSearchRegName
         * 把regList的每個索引(0~長度-1)填進searchRegName, 供舊版REG WR子系統的搜尋功能用。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  ShowInsLog
         * [物件控制] - 刷新logResultText
         * 把str追加到底部Log(logResultText), 超過MAX_LENGTH字元會從前面截掉舊內容並自動
         * 捲到最後。bQuietLog開啟時(開機REG TABLE初始化期間)整個跳過, 避免洗版。非UI
         * thread呼叫時用BeginInvoke切回UI thread再執行一次。
         * Param :
         *   str - 要追加顯示的文字
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        
        /*-------------------------------------------------------------
         *  btnTxClear_Click
         * [物件控制] - 清空logResultText
         * 清空底部Log顯示框。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void btnTxClear_Click(object sender, EventArgs e)
        {
            logResultText.Clear();
        }

        System.Threading.Thread CloseDown2;

        /*-------------------------------------------------------------
         *  btnSetER4100_Click
         * Set Tx Parameter鈕: 實際寫入呼叫(ER4100_SetParameter)已於2026-09-03暫時關閉,
         * 改成使用者自己先按Write DVB Register再操作 (見dvbTstPktTxStartBtn_Click同樣說明),
         * 目前只做COM port是否開啟的檢查。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  CloseSerial2OnExit
         * 關閉serialPort2 (rxSerial所用的COM口): 先拆掉RxReceiveData事件、清收發緩衝區,
         * 再Close, 例外時記錄到Log。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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


        /*-------------------------------------------------------------
         *  ER4100_SendCmdThread
         * 透過txSerial(Controller)送出命令字串。
         * Param :
         *   cmd - 要送出的命令
         * Return:
         *   true - 送出成功 / false - 失敗
         *-------------------------------------------------------------*/
        public bool ER4100_SendCmdThread(string cmd)
        {
            bool res = false;
            res = txSerial.SendDataToCom(cmd);
            return res;
        }

        /*-------------------------------------------------------------
         *  ER4100_RxSendCmd
         * 若serialPort2已開啟, 直接用WriteLine送出命令字串。
         * Param :
         *   cmd - 要送出的命令
         * Return:
         *   true - serialPort2已開啟且已送出 / false - 未開啟
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  ER4100_SendCmd
         * 若serialPort1已開啟, 直接用Write送出命令字串。
         * Param :
         *   cmd - 要送出的命令
         * Return:
         *   true - serialPort1已開啟且已送出 / false - 未開啟
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  ER4100_SendModeCmd
         * 若serialPort3已開啟, 直接用WriteLine送出命令字串。
         * Param :
         *   cmd - 要送出的命令
         * Return:
         *   true - serialPort3已開啟且已送出 / false - 未開啟
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  SetRxTestMode
         * 送"settestst <rOn>"命令切換RX Test Mode, 依isRxOldMethod選擇走
         * ER4100_SendCmdThread(舊方式)或ER4100_RxSendCmd(新方式)。
         * Param :
         *   rOn - 1=開啟test mode / 0=關閉
         * Return:
         *   true - 送出成功 / false - 失敗
         *-------------------------------------------------------------*/
        public bool SetRxTestMode(int rOn)
        {
            bool res = false;
            if (isRxOldMethod)
                res = ER4100_SendCmdThread("settestst "+rOn+"\r");
            else
                res = ER4100_RxSendCmd("settestst " + rOn + "\r");
            return res;
        }

        /*-------------------------------------------------------------
         *  SetRxPerMode
         * 送"setperst <rOn>"命令切換PER統計模式, 依isRxOldMethod選擇送出方式(同SetRxTestMode)。
         * Param :
         *   rOn - 1=開啟PER統計 / 0=關閉
         * Return:
         *   true - 送出成功 / false - 失敗
         *-------------------------------------------------------------*/
        public bool SetRxPerMode(int rOn)
        {
            bool res = false;
            if (isRxOldMethod)
                res = ER4100_SendCmdThread("setperst " + rOn + "\r");
            else
                res = ER4100_RxSendCmd("setperst " + rOn + "\r");
            return res;
        }

        /*-------------------------------------------------------------
         *  ClrRxResult
         * 送"perclr"命令清除PER統計結果, 依isRxOldMethod選擇送出方式(同SetRxTestMode)。
         * Param :
         *   (無)
         * Return:
         *   true - 送出成功 / false - 失敗
         *-------------------------------------------------------------*/
        public bool ClrRxResult()
        {
            bool res = false;
            if (isRxOldMethod)
                res = ER4100_SendCmdThread("perclr\r");
            else
                res = ER4100_RxSendCmd("perclr\r");
            return res;
        }

        /*-------------------------------------------------------------
         *  btnRxResultClr_Click
         * 清除PER結果鈕: 呼叫ClrRxResult()。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void btnRxResultClr_Click(object sender, EventArgs e)
        {
            ClrRxResult();
        }

        /*-------------------------------------------------------------
         *  GetRxResult
         * 送"pernum"命令查詢PER結果, 等100ms後讀回serialPort1的內容並顯示到Log。
         * Param :
         *   (無)
         * Return:
         *   true - 送出成功 / false - serialPort1未開啟
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  btnRxGetResult_Click
         * 取得PER結果鈕: 呼叫GetRxResult()。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void btnRxGetResult_Click(object sender, EventArgs e)
        {
            GetRxResult();
        }

        /*-------------------------------------------------------------
         *  btnRxStart_Click
         * [物件控制] - 刷新dvbTstPktRxStartBtn
         * RX Start/Stop鈕: 依目前按鈕文字判斷Start或Stop。Start時送出喚醒/測試命令,
         * 開啟RX Test Mode跟PER統計, 按鈕文字切成"Stop"; Stop分支見後續程式碼。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  RefreshDvbComPortList
         * [物件控制] - 刷新comPortToolStripComboBox / connectComToolStripMenuItem
         * 重新列出系統目前的COM port到comPortToolStripComboBox。沒有任何COM port時停用
         * "CONNECT"選單項; 有的話依名稱排序、預設選第一個, 並啟用選單項。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  refreshComToolStripMenuItem_Click
         * COM PORT選單的Refresh項: 呼叫RefreshDvbComPortList()重新列出COM port。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void refreshComToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshDvbComPortList();
        }

        /*-------------------------------------------------------------
         *  comPortToolStripMenuItem_Click
         * [物件控制] - 開啟ComPortSelectDialog / 刷新comPortToolStripComboBox
         * 呼叫Menu上的DVB Connection小視窗，讓使用者選擇DVB Port相關資訊。
         * v1.21L: 點「COM PORT」-> 已連線時直接斷線, 未連線時彈出ComPortSelectDialog選port
         * 後直接連線(dvbConnectionProcess), 並重算REG TABLE。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  RxSenTextClr_Click_1
         * [物件控制] - 清空dvbTstPktRxLogText
         * 清空DVB Tester RX分頁的Log顯示框。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void RxSenTextClr_Click_1(object sender, EventArgs e)
        {
            dvbTstPktRxLogText.Text = "";
        }

        /*-------------------------------------------------------------
         *  SaveInitValueToolStripMenuItem_Click
         * "Save > Initial Data"選單項: 呼叫SaveInitValue()。父選單saveToolStripMenuItem
         * ("Save")的Visible=false, 目前UI上點不到這個選單項。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void SaveInitValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveInitValue();
        }

        System.Threading.Thread CloseDown1;
        //private void connectComToolStripMenuItem_Click(object sender, EventArgs e)
        /*-------------------------------------------------------------
         *  dvbConnectionProcess
         * [物件控制] - 刷新isDVBConnected/paramDvbBandCombo/paramPAConfigCombo/
         * paramPwrLvText/paramPwrLvLabel等連線後才確定的PARAMETER控制項
         * COM PORT選單「CONNECT/DISCONNECT」的實際動作。未連線時: 開serialPort1
         * (ConnectWithIRQ)、送測試命令確認通、呼叫CheckICPowerType()判斷Low/High Power IC
         * 並依結果套用對應的頻段/PA Config/Power Level預設值; 讀不到IC類型則不斷線,
         * 提示使用者手動選PA CONFIG。已連線時走斷線分支(見後續程式碼)。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
                    string stmp;
                    /* 開始處理DVB的前期作業 */
                    ShowInsLog($"{comPortToolStripComboBox.Text} connected\r\n");
                    refreshComToolStripMenuItem.Enabled = false;
                    comPortToolStripComboBox.Enabled = false;
                    Thread.Sleep(50);
                    serialPort1.Write("\r");
                    Thread.Sleep(50);
                    stmp = serialPort1.ReadExisting();
                    //if (stmp.IndexOf("CMD>") > 0)
                    //    ShowInsLog("ERR: DVB is not ready?\r\n");
                    Thread.Sleep(50);
                    serialPort1.Write("settestst 1\r"); // Owen tmp Closed
                    Thread.Sleep(50);
                    stmp = serialPort1.ReadExisting();

                    /* 先讀ER4100 BBP ID確認它存不存在 */
                    Thread.Sleep(50);
                    serialPort1.Write("rr 0xf100\r"); // Owen tmp Closed
                    Thread.Sleep(50);
                    stmp = serialPort1.ReadExisting();
                    if (stmp.IndexOf("9000000") >= 0)
                        ShowInsLog("Chip ID :" + stmp + "\r");
                    else
                    {
                        isDVBConnected = false;
                        ShowInsLog("ERR: Chip is No Exist : " + stmp + "\r");
                        CloseComPortProcess();
                        return;
                    }
                    // connectComToolStripMenuItem.Text = "CLOSE";
                    isDVBConnected = true;
                    UpdateDvbConnStateLabel();

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

                    PacketRxBtnStopProcess();
                    ShowInsLog("RX Reception Stop\r\n");
                }
               
            }
            else  // 原本已 CONNECT
            {
                CloseComPortProcess();
                ShowInsLog("COM Closed\r\n");
            }
        }
       
        /*-------------------------------------------------------------
         *  CloseComPortProcess
         * [物件控制] - 刷新paramDvbBandCombo(鎖"Don't care")/paramPAConfigCombo
         * 斷開DVB連線: 送出stopblink/rfg/setperst/settestst等收尾命令, 另起執行緒
         * (CloseSerial1OnExit)真正Close serialPort1, 並把isDVBConnected設回false、
         * 還原COM選單/下拉選單的Enabled狀態。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

            UpdateDvbConnStateLabel();
        }
        
        /*-------------------------------------------------------------
         *  CloseSerial1OnExit
         * 真正關閉serialPort1: 先拆掉Packet RX的接收處理常式(DetachUartRxHandler)再Close,
         * 例外時靜默吞掉(結尾時不需要再跳訊息)。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  SetSyncword
         * 把paramTxSyncText目前的十六進位值送"rfg 7 <值>"命令寫到DVB。跟
         * btnTxSetSyncword_Click內容完全相同, 目前找不到其他呼叫端。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  btnTxSetSyncword_Click
         * Set Syncword鈕: 把paramTxSyncText目前的十六進位值送"rfg 7 <值>"命令寫到DVB
         * (跟SetSyncword()內容完全相同, 是各自獨立維護的兩份copy)。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  ShowRxRcvNum
         * [物件控制] - 刷新dvbTstPktRxOkCntText/dvbTstPktRxRssiText
         * 顯示收包數/RSSI到畫面。非UI thread呼叫時用Invoke切回UI thread再執行一次。
         * Param :
         *   sNum - 收包數文字
         *   sRssi - RSSI文字
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  RxReceiveData
         * [物件控制] - 刷新dvbTstPktRxOkCntText/dvbTstPktRxRssiText (經由ShowRxRcvNum)
         * serialPort2的DataReceived處理常式(舊版RX流程用): 逐行讀取, 含'#'視為收到一包
         * (累加rxReceivedNum), 若同時含'$'和'&'則從'$'後3個字元解析RSSI(取負號)。
         * isRxSenStart開啟時才更新畫面顯示。
         * Param :
         *   sender - 未使用
         *   e - DataReceived事件參數 (未使用, 直接從serialPort2讀)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  aboutToolStripMenuItem1_Click_1
         * [物件控制] - 開啟AboutForm
         * Help > About選單項: 開啟AboutForm並置中顯示於主視窗之上。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  QuitToolStripMenuItem_Click_1
         * File > Quit選單項: 若serialPort1/serialPort2開著, 各自另起執行緒關閉
         * (CloseSerial1OnExit/CloseSerial2OnExit), 再關閉主視窗。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  crc16_ibm
         * 計算CRC16-IBM (bit-reverse版本): 每個byte先做bit reverse再跟crc做XOR/移位運算,
         * 最後把16-bit結果整體再bit-reverse一次才回傳。供ShowPktData()使用。
         * Param :
         *   data - 要算CRC的資料
         *   length - 資料長度
         * Return:
         *   16-bit CRC值
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  ComputeCrc16Ibm
         * 標準CRC16-IBM(不做bit-reverse)。唯一呼叫處在ShowPktData()裡被註解掉,
         * 目前沒有真正在用。
         * Param :
         *   data - 要算CRC的資料
         * Return:
         *   16-bit CRC值
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  ShowPktData
         * [物件控制] - 附加顯示到logResultText
         * 除錯用: 組一組假封包資料(len bytes, 前3 byte固定填len/0x46/0x53, 其餘遞增),
         * 逐byte印到Log, 再算CRC16-IBM(crc16_ibm)印在最後。
         * Param :
         *   len - 要產生的假封包長度
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        
        /*-------------------------------------------------------------
         *  dvbTstPktRxSetPayloadBtn_Click
         * Set Payload鈕: 呼叫SetPayloadProcess()。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void dvbTstPktRxSetPayloadBtn_Click(object sender, EventArgs e)
        {
            SetPayloadProcess();

        }

        //private const int MaxHexChars = 125 * 2;
        private bool isEditing = false;
        private List<byte> tmpArray = new List<byte>();
        
        /*-------------------------------------------------------------
         *  dvbTstPktTxPayloadText_TextChanged
         * [物件控制] - 刷新dvbTstPktTxPayloadText/dvbTstPktTxFilledLenText
         * Packet TX Payload輸入框即時格式化: 依CRC選項(dvbTstPktTxCrcCombo)/是否
         * PrependSeq(dvbTstPktTxPrependSeqCheck)算出上限字元數, 移除非十六進位字元、
         * 超長截斷並嗶一聲提示, 每2位加空格重新格式化寫回輸入框, 同步更新tmpArray
         * (byte陣列)跟已填長度顯示(dvbTstPktTxFilledLenText)。游標位置會依編輯前"游標
         * 前面有幾個十六進位字元"反推回正確位置, 而不是每次都強制跳到最後面(舊版那樣寫
         * 會導致在字串中間反白多字元編輯時, 第二個字元以後全部跑到最尾端)。
         * dvbTstPktTxRandomPlCheck勾選(Random Payload模式)時整段跳過。isEditing旗標
         * 防止TextChanged重入。
         * Param :
         *   sender, e - TextChanged事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

                // Step 0: 記錄目前游標前面有幾個十六進位字元, 格式化後用這個數字反推正確
                // 的游標位置, 不要每次都直接跳到最後面
                int caretPos = dvbTstPktTxPayloadText.SelectionStart;
                string beforeCaret = dvbTstPktTxPayloadText.Text.Substring(0, Math.Min(caretPos, dvbTstPktTxPayloadText.Text.Length));
                int hexCharsBeforeCaret = Regex.Replace(beforeCaret.ToUpper(), @"[^0-9A-F]", "").Length;

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
                hexCharsBeforeCaret = Math.Min(hexCharsBeforeCaret, raw.Length);

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

                // Step 4: 將格式化後的字串寫回 TextBoxInput, 游標還原到編輯的位置
                // (依Step 0算出來的hexCharsBeforeCaret反推, 每2個十六進位字元前面會多插入
                // 1個空格, 不是每次都跳到最後面)
                dvbTstPktTxPayloadText.Text = formatted.ToString();
                int newCaretPos = hexCharsBeforeCaret == 0 ? 0 : hexCharsBeforeCaret + (hexCharsBeforeCaret - 1) / 2;
                dvbTstPktTxPayloadText.SelectionStart = Math.Min(newCaretPos, dvbTstPktTxPayloadText.Text.Length);

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
        /*-------------------------------------------------------------
         *  ReverseBits
         * 把32-bit值的位元順序整個反轉(bit 0變bit 31, 依此類推)。
         * Param :
         *   value - 要反轉的32-bit值
         * Return:
         *   位元順序反轉後的值
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  hexReverse
         * 把十六進位字串解析成32-bit值、整個位元順序反轉(ReverseBits)、再轉回8位
         * 十六進位字串。
         * Param :
         *   hexInput - 十六進位字串 (不含"0x"前綴)
         * Return:
         *   位元反轉後的8位十六進位字串
         *-------------------------------------------------------------*/
        public string hexReverse(string hexInput)
        {
            string hexResult = "";
            uint value = Convert.ToUInt32(hexInput, 16);
            uint reversed = ReverseBits(value);
            hexResult = reversed.ToString("X8");
            return hexResult;
        }
        /*-------------------------------------------------------------
         *  dvbTstPktTxTestBtn_Click
         * [物件控制] - 刷新logResultText
         * 除錯用測試鈕(原btnCalCrc): 把paramTxSyncText目前值位元反轉(hexReverse)後印到
         * Log對照, 底下另一段CRC測試程式碼已註解掉不執行。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        private void paramXtalHelpBtn_Click(object sender, EventArgs e)
        {
            string msg =
                "(1). Xtal: Must match the crystal actually fitted on the module. It determines which Data Rates are available.\r\n\r\n" +
                "(2). Xtal Gain: Affects the crystal's startup time on wakeup / power-up. It also affects the current draw while NOT sleeping." +
                "      e.g. approximately 1.8mA at Gain=4 vs 2.4mA at Gain=8.\r\n\r\n" +
                "(3). Cap IO: Trims the crystal's internal load capacitance, used to fine-tune the actual Base Frequency offset.";
            MessageBox.Show(this, msg, "Xtal / Cap IO Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void paramFreqHelpBtn_Click(object sender, EventArgs e)
        {
            string msg =
                "(1). DVB Band: Only relevant to the DVB Tester. Determined by the ER4100 daughter-board's hardware and cannot be selected from software," +
                "   it reflects the frequency band the daughter-board was built for. This setting does not limit the Base Frequency; " +
                "   it only narrows down the PA Config options.\r\n\r\n" +
                "(2). Frequency: The Base Frequency. ER4100 supports the following 5 bands:\r\n" +
                "      99 - 125 MHz\r\n" +
                "      196 - 253 MHz\r\n" +
                "      391 - 508 MHz\r\n" +
                "      780 - 1018 MHz";
            MessageBox.Show(this, msg, "Frequency Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void paramPwrHelpBtn_Click(object sender, EventArgs e)
        {
            string msg =
                "(1). PA Config: Selects the output power of the chip's internal High-Power PA (H-PA) or Low-Power PA (L-PA), and must match the external matching network design.\r\n" +
                "      e.g. if the external matching is designed for 915MHz / 23dBm, select \"23\" here. " +
                "           H-PA/L-PA itself is fixed at the factory and cannot be changed here - this only selects the corresponding dBm value.\r\n\r\n" +
                "(2). Power LV: The range depends on PA Config - H-PA (23dBm) is 0~127, L-PA (10/14dBm) is 0~27. " +
                "     e.g. with PA Config = 23dBm (H-PA), 127 is the theoretical value for full 23dBm output.";
            MessageBox.Show(this, msg, "Power Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /*-------------------------------------------------------------
         *  btnDevComment_Click
         * DEVIATION欄位旁的"?"按鈕: 建議使用h=1(即Deviation = DataRate/2, 見H=2*Dev/DataRate
         * 的定義), 依目前DATA RATE算出建議值提示給使用者, 不會自動改欄位內容。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void paramDeviationHelpBtn_Click(object sender, EventArgs e)
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

        /*-------------------------------------------------------------
         *  CRC_Calculation
         * 對tmpArray(目前Packet TX Payload解析出來的byte陣列)算CRC16-IBM(bit-reverse版本,
         * 邏輯跟crc16_ibm相同但用int運算)。
         * Param :
         *   (無)
         * Return:
         *   16-bit CRC值 (以int回傳)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  DisableSetting
         * [物件控制] - 停用Packet TX相關的PARAMETER與Packet TX控制項 (paramBaseFreqText/
         * paramDataRateCombo/paramDeviationText/paramModulationCombo/paramPwrLvText/
         * paramCapIoText/paramTxSyncText/paramTxSyncSoftInvertCheck/
         * dvbTstPktTxInfiniteCheck/dvbTstPktTxPrependSeqCheck/dvbTstPktTxRandomPlCheck/
         * dvbTstPktTxRandomPlLenText/dvbTstPktTxCrcCombo)
         * 送包期間鎖住這些控制項避免使用者中途修改。dvbTstPktTxPayloadText例外, 用
         * ReadOnly=true而不是Enabled=false, 這樣還是保留設計時的黃字黑底樣式, 不會被
         * WinForms停用狀態的預設灰階外觀蓋掉。非UI thread呼叫時用Invoke切回UI thread
         * 再執行一次。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
                paramXtalGainCombo.Enabled = false;
                paramXtalGainCombo.Update();
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
                dvbTstPktTxPayloadText.ReadOnly = true;
                dvbTstPktTxPayloadText.Update();
                dvbTstPktTxCrcCombo.Enabled = false;
                dvbTstPktTxCrcCombo.Update();
            }
            

        }

        /*-------------------------------------------------------------
         *  EnableSetting
         * [物件控制] - 還原DisableSetting()鎖住的控制項的Enabled狀態
         * 送包結束後解鎖控制項。dvbTstPktTxRandomPlLenText/dvbTstPktTxPayloadText的
         * Enabled/ReadOnly會依dvbTstPktTxRandomPlCheck目前是否勾選決定。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        public void EnableSetting()
        {
            paramBaseFreqText.Enabled = true;
            paramDataRateCombo.Enabled = true;
            paramDeviationText.Enabled = true;
            paramModulationCombo.Enabled = true;
            paramPwrLvText.Enabled = true;
            paramCapIoText.Enabled=true;
            paramXtalGainCombo.Enabled = true;
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
        /*-------------------------------------------------------------
         *  InfiniteStop
         * 送單一字元"t"命令停止Infinite模式的連續送包。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        public void InfiniteStop()
        {
            if (serialPort1.IsOpen)
                serialPort1.Write("t");
        }

        /*-------------------------------------------------------------
         *  btnStop_Click
         * Stop鈕: COM port開著就呼叫InfiniteStop(), 否則印錯誤訊息到Log。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
                InfiniteStop();
            else
                logResultText.Text += "COMPORT ERROR!\r\n";
        }

        /*-------------------------------------------------------------
         *  InfiniteStart
         * 送"rfg 21"命令開始Infinite模式的連續送包, 送完等待100ms讓DVB處理。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        public void InfiniteStart()
        {
            serialPort1.Write("rfg 21\r");
            Thread.Sleep(100);
        }

        public bool isSending = false;

        /*-------------------------------------------------------------
         *  GetFifoLen
         * 算目前這次要送的封包在FIFO裡的實際長度: Length欄位(1) + tmpArray的Payload
         * byte數 + seqLen(PrependSeq開啟則+2) + crcLen(依dvbTstPktTxCrcCombo, NO_CRC=0
         * 否則2)。
         * Param :
         *   (無)
         * Return:
         *   FIFO總長度 (byte數)
         *-------------------------------------------------------------*/
        private int GetFifoLen()
        {
            int seqLen = dvbTstPktTxPrependSeqCheck.Checked ? 2 : 0;
            int crcLen = (dvbTstPktTxCrcCombo.SelectedIndex == 2) ? 0 : 2;  // NO_CRC = 0 bytes
            return 1 + tmpArray.Count + seqLen + crcLen;  // 1 = Length field
        }

        /*-------------------------------------------------------------
         *  PacketSend
         * [物件控制] - 刷新dvbTstPktTxStartBtn/dvbTstPktTxStopBtn (Enabled狀態)
         * 連續送num次"cfg 14 1"觸發送包命令。依目前DATA RATE算送包間隔delayTime
         * (低速率時間隔拉長, 避免FIFO來不及清空); 迴圈中isSending變false(按了STOP)就
         * 立刻中止。送完/中止後還原按鈕狀態並呼叫EnableSetting()解鎖控制項。
         * 這個方法本身在背景執行緒跑, UI操作都包在Invoke裡切回UI thread。
         * Param :
         *   num - 要送的封包數
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  IsAllNumber
         * 判斷字串是否整串都是數字(regex "^\d+$")。
         * Param :
         *   s - 要檢查的字串
         * Return:
         *   true - 整串都是數字 / false - 含非數字字元或空字串
         *-------------------------------------------------------------*/
        bool IsAllNumber(string s)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(s, @"^\d+$");
        }

        /*-------------------------------------------------------------
         *  dvbTstPktTxStartBtn_Click
         * [物件控制] - 刷新dvbTstPktTxStartBtn等Packet TX控制項 (經由DisableSetting/PacketSend)
         * Packet TX Start鈕: 檢查COM port已連線、REG TABLE無未寫回的變更
         * (ConfirmRewriteIfRegTableDirty)、封包數(1~200)、Payload長度是否超過目前
         * CRC/PrependSeq組合允許的上限, Random Payload模式會先產生隨機資料
         * (GenerateRandomPayload)。檢查都過了才呼叫DisableSetting()鎖控制項並轉呼叫
         * PacketSend()實際送包 (詳細分支見內文)。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
                    //SetPayloadOrder();      // 0xA0A8: LSB=0 / MSB=1, 要在 cfg 之後才不會被蓋掉
                    //Thread.Sleep(50);
                    //SetAllowBitError();     // 0xB104: RX syncword 允許的 bit error 數
                    //Thread.Sleep(50);
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

        /*-------------------------------------------------------------
         *  dvbTstPktTxInfiniteCheck_CheckedChanged
         * [物件控制] - 刷新dvbTstPktTxCntText
         * 勾選Infinite模式時停用封包數輸入框(Infinite不需要指定次數), 取消勾選則還原。
         * Param :
         *   sender, e - CheckedChanged事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void dvbTstPktTxInfiniteCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (dvbTstPktTxInfiniteCheck.Checked)
            {
                dvbTstPktTxCntText.Enabled = false;
            }
            else
                dvbTstPktTxCntText.Enabled = true;
        }

        /*-------------------------------------------------------------
         *  HexGenerate
         * [物件控制] - 刷新dvbTstPktTxPayloadText
         * 依dvbTstPktTxRandomPlLenText指定的長度產生亂數byte, 存進tmpArray並以
         * 十六進位格式顯示到dvbTstPktTxPayloadText。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  dvbTstPktTxRandomPlCheck_CheckedChanged
         * [物件控制] - 刷新dvbTstPktTxPayloadText/dvbTstPktTxRandomPlLenText/
         * dvbTstPktTxFilledLenText/dvbTstPktTxFilledLenDescLabel
         * 勾選Random Payload: Payload輸入框清空並改唯讀(灰底), 長度輸入框啟用預設填5,
         * 隱藏FilledLen顯示改立刻產生一次隨機payload(GenerateRandomPayload)。
         * 取消勾選: 還原Payload輸入框可編輯、顯示FilledLen, 清空tmpArray。
         * Param :
         *   sender, e - CheckedChanged事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void dvbTstPktTxRandomPlCheck_CheckedChanged(object sender, EventArgs e)
        {
            dvbTstPktTxRandomPlLenText.Enabled = dvbTstPktTxRandomPlCheck.Checked;
            if (dvbTstPktTxRandomPlCheck.Checked) {
                dvbTstPktTxPayloadText.Clear();
                dvbTstPktTxPayloadText.ReadOnly = true;
                dvbTstPktTxPayloadText.BackColor = Color.Black;
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
                dvbTstPktTxPayloadText.BackColor = Color.Black;
                dvbTstPktTxPayloadText.ReadOnly = false;
                dvbTstPktTxPayloadText.Update();
                dvbTstPktTxPayloadText.Clear();
                tmpArray.Clear();
            }
        }

        /*-------------------------------------------------------------
         *  dvbTstPktTxRandomPlLenText_Leave
         * 離開長度輸入框時, 若Random Payload模式已勾選, 依新長度重新產生一次隨機payload。
         * Param :
         *   sender, e - Leave事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void dvbTstPktTxRandomPlLenText_Leave(object sender, EventArgs e)
        {
            if (dvbTstPktTxRandomPlCheck.Checked)
                GenerateRandomPayload();
        }

        // 焦點還在 dvbTstPktTxRandomPlLenText 裡直接按 Enter 也重新產生一次, 不用先點別的地方讓焦點離開觸發 Leave。
        // SuppressKeyPress 擋掉 Enter 在單行 TextBox 上預設的警示音。
        /*-------------------------------------------------------------
         *  dvbTstPktTxRandomPlLenText_KeyDown
         * 在長度輸入框裡直接按Enter等同Leave的效果, 立即重新產生隨機payload(若已勾選),
         * 並吃掉Enter避免單行TextBox預設的警示音。
         * Param :
         *   sender - 未使用
         *   e - KeyDown事件參數, 取其KeyCode判斷是否為Enter
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void dvbTstPktTxRandomPlLenText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (dvbTstPktTxRandomPlCheck.Checked)
                    GenerateRandomPayload();
            }
        }

        /*-------------------------------------------------------------
         *  GenerateRandomPayload
         * [物件控制] - 刷新dvbTstPktTxPayloadText (經由HexGenerate)
         * 依dvbTstPktTxRandomPlLenText目前的長度重新產生一次隨機payload, 存進tmpArray;
         * dvbTstPktTxRandomPlCheck沒勾時不做事。長度(含PrependSeq +2後)超出目前
         * CRC選項允許的上限會跳警告訊息。原本是獨立的"Apply"按鈕(btnRandomGenerate)才會
         * 觸發, 現在勾選/改長度時直接呼叫。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  dvbTstPktTxStopBtn_Click
         * [物件控制] - 刷新dvbTstPktTxStopBtn/dvbTstPktTxStartBtn (經由EnableSetting)
         * Packet TX Stop鈕: Infinite模式開著就先送停止命令(InfiniteStop), 關閉isSending
         * 旗標讓PacketSend()迴圈中止, 還原按鈕Enabled狀態並解鎖控制項(EnableSetting)。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  cutTitle
         * 取字串前len個字元。
         * Param :
         *   t - 原始字串
         *   len - 要取的長度
         * Return:
         *   t的前len個字元
         *-------------------------------------------------------------*/
        public static string cutTitle(string t, int len)
        {
            return t.Substring(0, len);
        }

        /*-------------------------------------------------------------
         *  ApplyPASetting
         * [物件控制] - 刷新paramPAConfigCombo
         * 檢查setPA是否在mode對應的PAConfigTable合法清單裡, 合法就套到paramPAConfigCombo,
         * 不合法跳錯誤訊息。
         * Param :
         *   mode - PA_TYPE (決定要用PAConfigTable的哪一組清單)
         *   setPA - 要套用的PA Config字串
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  getOneByteData
         * 把字串解析成整數, 失敗時記Log並回傳-1。
         * Param :
         *   stmp - 要解析的字串
         * Return:
         *   解析出的整數; 失敗回傳-1
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  CheckEepromExist
         * 送"romexist"命令查詢DVB板子是否有EEPROM (ER81Wx沒有), 讀回結果去掉換行後解析成整數。
         * Param :
         *   (無)
         * Return:
         *   1 - 有EEPROM / 0 - 沒有 / -1 - 解析失敗
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  ReadMaxPwrFromEeprom
         * 送"user 6 27"命令從EEPROM讀回PA Power索引(0=10dBm,1=14dBm,2=16dBm,3=20dBm,4=23dBm)。
         * Param :
         *   (無)
         * Return:
         *   讀到的PA Power索引; 解析失敗回傳-1
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  ReadFreqBandFromEeprom
         * 送"user 6 15"/"user 6 16"命令分別讀回RF-band的高低byte, 組成16-bit rfBand值。
         * Param :
         *   (無)
         * Return:
         *   組合後的RF Band值 (band_H<<8 | band_L)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  btnClearInsLog_Click
         * [物件控制] - 清空logResultText
         * 清空底部Log顯示框。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void btnClearInsLog_Click(object sender, EventArgs e)
        {
            logResultText.Clear();
        }

        /*-------------------------------------------------------------
         *  cwOnProcess
         * 依目前Continuous TX的Pattern選項(dvbTstContiTxSingleToneRadioBtn/
         * dvbTstContiTxBit0101RadioBtn/dvbTstContiTxPrbs9RadioBtn)送出對應的CW命令
         * ("txst 1"/"txpr55"/"txprbs")。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  cwOffProcess
         * 送"txst 0"命令關閉CW輸出。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        public void cwOffProcess()
        {
            string cmd = "txst 0\r";
            serialPort1.Write(cmd);
            ShowInsLog("CW OFF\r\n");
        }

        /*-------------------------------------------------------------
         *  dvbTstContiTxStartBtn_Click
         * [物件控制] - 停用dvbTstContiTxSingleToneRadioBtn等Pattern選項並刷新按鈕狀態
         * Continuous TX Start鈕: 檢查COM port已連線、REG TABLE無未寫回的變更
         * (ConfirmRewriteIfRegTableDirty), 送出"tx 1 30 0"進TX模式後呼叫cwOnProcess()
         * 依Pattern選項送CW命令, 並鎖住Pattern選項避免送出中途切換。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  dvbTstContiTxStopBtn_Click
         * [物件控制] - 刷新dvbTstContiTxStartBtn/dvbTstContiTxStopBtn/
         * dvbTstContiTxSingleToneRadioBtn/dvbTstContiTxPatternRadioBtn
         * Continuous TX Stop鈕: 呼叫cwOffProcess()關閉CW輸出, 還原按鈕/Pattern選項的
         * Enabled狀態。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  DVBBandCombo_SelectedIndexChanged
         * [物件控制] - 刷新paramPAConfigCombo
         * highlowICType未知時不動作。Low Power IC下, 依paramDvbBandCombo選的頻段套用
         * 對應的PA Config清單(433/915→PA_915433L, 868→PA_868L)。
         * Param :
         *   sender, e - SelectedIndexChanged事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  PacketRxBtnStopProcess
         * [物件控制] - 刷新Packet RX相關控制項 (經由EnableRxControlsAfterStop)
         * 停止Packet RX: 送"rx 0"命令、拆掉Packet RX的位元組接收處理常式
         * (DetachUartRxHandler), 再解鎖RX相關控制項(EnableRxControlsAfterStop)。
         * Param :
         *   (無)
         * Return:
         *   固定回傳false (保留欄位, 目前呼叫端未使用回傳值)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  EnableRxControlsAfterStop
         * [物件控制] - 刷新paramRxSyncSoftInvertCheck/dvbTstPktRxScanBtn/
         * dvbTstPktRxStartBtn/dvbTstPktRxCrcCombo等PARAMETER/Packet RX控制項
         * Packet Rx沒在開啟中(尚未開始或已停止/失敗)時, 把所有相關參數控制項恢復成可設定
         * 狀態。供PacketRxBtnStopProcess()及PacketRxStartProcess()的失敗路徑共用, 避免
         * 兩邊各自維護一份清單而漏改。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  DisconnectUartProcess
         * [物件控制] - 刷新dvbTstPktRxStartBtn
         * 送"setperst 0"/"settestst 0"關閉PER統計跟test mode, 重新啟用Start RX鈕。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  SetStateButton
         * [物件控制] - 依enable改變btn的視覺樣式(FlatStyle)
         * 把按鈕改成扁平樣式並依enable切換配色: 可點狀態用淺藍, 鎖定/執行中用灰色。
         * Param :
         *   btn - 要設定樣式的按鈕
         *   enable - true=可點(淺藍) / false=鎖定(灰色)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  dvbTstPktRxStartBtn_Click
         * [物件控制] - 刷新dvbTstPktRxLogText/dvbTstPktRxOkCntText/dvbTstPktRxErrCntText/
         * dvbTstPktRxRssiText/dvbTstPktRxScanBtn/dvbTstPktRxStartBtn
         * Start RX/Stop RX鈕(同一顆依文字判斷): Start時歸零統計、清Log、鎖住Scan RSSI跟
         * PARAMETER控制項(DisableParameterSetting)、檢查REG TABLE未寫回的變更
         * (ConfirmRewriteIfRegTableDirty), 再呼叫PacketRxStartProcess()真正開始收包,
         * 成功則按鈕文字切成"Stop RX"。Stop時呼叫PacketRxBtnStopProcess()並把文字
         * 切回"Start RX"。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  packetRxCountMode_CheckedChanged
         * [物件控制] - 刷新dvbTstPktRxCntText
         * dvbTstPktRxCntLimitRadioBtn("Max Packet Count")/dvbTstPktRxCntNoLimitRadioBtn
         * ("No limitation")共用同一個handler, 兩顆RadioButton的CheckedChanged都會掛這個
         * ——選了Max Packet Count才讓dvbTstPktRxCntText可編輯。
         * Param :
         *   sender, e - CheckedChanged事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void packetRxCountMode_CheckedChanged(object sender, EventArgs e)
        {
            dvbTstPktRxCntText.Enabled = dvbTstPktRxCntLimitRadioBtn.Checked;
        }

        /*-------------------------------------------------------------
         *  btnRxResultClr_Click_1
         * [物件控制] - 清空dvbTstPktRxLogText
         * 清空Packet RX的Log顯示框。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void btnRxResultClr_Click_1(object sender, EventArgs e)
        {
            dvbTstPktRxLogText.Clear();
        }

        /*-------------------------------------------------------------
         *  dvbTstContiTxPatternRadioBtn_CheckedChanged
         * [物件控制] - 刷新dvbTstContiTxPatternGroup
         * 選到Pattern模式時顯示Pattern選項群組(0101/PRBS9)。
         * Param :
         *   sender, e - CheckedChanged事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void dvbTstContiTxPatternRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            dvbTstContiTxPatternGroup.Enabled = true;
        }

        /*-------------------------------------------------------------
         *  dvbTstContiTxSingleToneRadioBtn_CheckedChanged
         * [物件控制] - 刷新dvbTstContiTxPatternGroup
         * 選到Single Tone模式時隱藏Pattern選項群組(Single Tone不需要選Pattern)。
         * Param :
         *   sender, e - CheckedChanged事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void dvbTstContiTxSingleToneRadioBtn_CheckedChanged(object sender, EventArgs e)
        {
            dvbTstContiTxPatternGroup.Enabled = false;
        }

        /*-------------------------------------------------------------
         *  dvbTstPktTxPrependSeqCheck_CheckedChanged
         * [物件控制] - 刷新dvbTstPktTxDgm系列封包格式圖 (經由RefreshPacketFormatDiagram)
         * PrependSeq勾選變動時重畫封包格式示意圖 (是否含SeqNum欄位)。
         * Param :
         *   sender, e - CheckedChanged事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void dvbTstPktTxPrependSeqCheck_CheckedChanged(object sender, EventArgs e)
        {
            //addSeqLevel.Visible = dvbTstPktTxPrependSeqCheck.Checked;
            RefreshPacketFormatDiagram();
        }

        /*-------------------------------------------------------------
         *  dvbTstPktTxCrcCombo_SelectedIndexChanged
         * [物件控制] - 刷新dvbTstPktTxDgm系列封包格式圖 (經由RefreshPacketFormatDiagram)
         * CRC選項變動時重畫封包格式示意圖(是否含CRC欄位)。
         * Param :
         *   sender, e - SelectedIndexChanged事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void dvbTstPktTxCrcCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshPacketFormatDiagram();
        }

        /*-------------------------------------------------------------
         *  preambleLen_TextChanged
         * [物件控制] - 刷新dvbTstPktTxDgm系列封包格式圖 (經由RefreshPacketFormatDiagram)
         * Preamble長度變動時重畫封包格式示意圖。
         * Param :
         *   sender, e - TextChanged事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void preambleLen_TextChanged(object sender, EventArgs e)
        {
            RefreshPacketFormatDiagram();
        }

        /*-------------------------------------------------------------
         *  SetFifoSegmentState
         * [物件控制] - 刷新box(Label)/lenLabel(Label)
         * 封包格式圖裡單一欄位色塊的顯示狀態: active時淺藍底黑字並顯示activeLenText,
         * 否則白底灰字顯示"0Bytes"。
         * Param :
         *   box - 該欄位的色塊Label
         *   lenLabel - 該欄位的長度說明Label
         *   active - 這個欄位目前是否啟用(存在)
         *   activeLenText - active時要顯示的長度文字
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void SetFifoSegmentState(Label box, Label lenLabel, bool active, string activeLenText)
        {
            box.BackColor = active ? Color.LightBlue : Color.White;
            box.ForeColor = active ? Color.Black : Color.Gray;
            lenLabel.Text = active ? activeLenText : "0Bytes";
        }

        /*-------------------------------------------------------------
         *  RefreshPacketFormatDiagram
         * [物件控制] - 刷新dvbTstPktTxDgmPbLenLabel/dvbTstPktTxDgmSyncLenLabel/
         * dvbTstPktTxDgmSeqNumLabel/dvbTstPktTxDgmSeqNumLenLabel/dvbTstPktTxDgmCrcLabel/
         * dvbTstPktTxDgmCrcLenLabel/dvbTstPktTxDgmPayloadLenLabel
         * 依目前Preamble長度/Syncword實際位元數/PrependSeq/CRC選項重畫Packet TX的封包
         * 格式示意圖各欄位長度, 並依FIFO_MAX_LEN(128)反推目前組合下Payload的最大可用長度
         * (Length欄位1 byte + SeqNum(0/2) + Payload + CRC(0/2) = 128)。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  dvbTstStateSetBtn_Click
         * Power Mode State Set鈕: 檢查COM port已連線、REG TABLE無未寫回的變更
         * (ConfirmRewriteIfRegTableDirty), 依選取的狀態送對應命令: Idle("pm 0"接著
         * "rx 0")、Deep Sleep("pm 1")、Shutdown("pm 2")。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  GetRSSI
         * 把韌體回傳的3位數字串轉成RSSI值(取負號)。
         * Param :
         *   str - 韌體回傳的字串
         * Return:
         *   解析出的RSSI(dBm); 長度不是3或解析失敗則回傳333(無效值)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  DoScanProcess
         * [物件控制] - 刷新dvbTstPktRxRssiText
         * Scan RSSI一次取樣: 送"getchrssi <getrssiTimes>"命令, 讀回結果解析RSSI
         * (GetRSSI)並顯示。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  OnTimedScanFreqProcess
         * scanTimer的Elapsed callback: COM port開著就呼叫DoScanProcess()做一次RSSI取樣,
         * 否則停掉計時器(StopScanTimer)。
         * Param :
         *   source, e - Elapsed事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  InitScanTimer
         * 建立並啟動scanTimer(500ms週期, AutoReset), Elapsed掛OnTimedScanFreqProcess。
         * 已存在就不重建(避免重複new)。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  StopScanTimer
         * 停止並釋放scanTimer。已是null就不做事。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void StopScanTimer()
        {
            if (scanTimer == null) return;
            //scanTimer.Enabled = false;
            scanTimer.Stop();
            scanTimer.Dispose();
            scanTimer = null;
        }
        /*-------------------------------------------------------------
         *  ScanStop
         * [物件控制] - 刷新dvbTstPktRxScanBtn/dvbTstPktRxStartBtn/dvbTstPktRxOkCntText/
         * dvbTstPktRxErrCntText
         * 停止Scan RSSI: 送"setregb19c 0"、停計時器(StopScanTimer)、解鎖PARAMETER控制項
         * (EnableParameterSetting)、按鈕文字還原"Scan RSSI"、重新啟用Start RX等控制項。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  ScanStart
         * [物件控制] - 刷新dvbTstPktRxScanBtn/dvbTstPktRxStartBtn/dvbTstPktRxOkCntText/
         * dvbTstPktRxErrCntText/dvbTstPktRxLogText
         * 開始Scan RSSI: 送"settestst 1"/"rx 0"/"setregb19c 3"進入掃描模式, 按鈕文字改
         * "Scan Stop", 鎖住Start RX等控制項, 啟動計時器(InitScanTimer)定期取樣。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  ScanProcess
         * [物件控制] - 刷新dvbTstPktRxScanBtn (經由ScanStart/ScanStop)
         * Scan RSSI鈕的統一進入點: 依dvbTstPktRxScanBtn目前文字判斷要Start還是Stop,
         * Start前先確認REG TABLE無未寫回的變更(ConfirmRewriteIfRegTableDirty)。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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
        /*-------------------------------------------------------------
         *  btnScanRssi_Click
         * Scan RSSI鈕: 轉呼叫ScanProcess()。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void btnScanRssi_Click(object sender, EventArgs e)
        {
                // Set Parameter
                // Set REG_B19C_3
                // SetScanRssi
                // init timer
                // Read RSSI
                ScanProcess();
        }

        /*-------------------------------------------------------------
         *  dvbTstPktRxClearLogBtn_Click
         * [物件控制] - 清空dvbTstPktRxLogText
         * 清空Packet RX的Log顯示框。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void dvbTstPktRxClearLogBtn_Click(object sender, EventArgs e)
        {
            dvbTstPktRxLogText.Clear();
        }

        private ContextMenuStrip genMenu;

        /*-------------------------------------------------------------
         *  btnGen_Click
         * 按下GENERATE先跳出選單: 要照REG TABLE目前的Topic/DependsOn過濾結果只產生
         * 通過的register, 還是產生全部。選單物件(genMenu)延遲建立、重複使用。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  RunGenerate
         * filterOnly=true時只有目前通過REG TABLE Topic/DependsOn過濾的位址會被
         * CodeOutHelper寫出(見GenerateAllowedAddrs); false就是原本的「全部產生」。
         * 值一律來自REG TABLE目前顯示的現值(GenerateValueOverrides, 而不是重新用參數
         * 算一次), 產生成功則跳出CodeGenPreviewForm預覽/存檔。
         * Param :
         *   filterOnly - true=只產生通過過濾的位址 / false=產生全部
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
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

        /*-------------------------------------------------------------
         *  BuildGenerateAllowedAddrs
         * 依REG TABLE目前的Topic/DependsOn勾選狀態(PassesFilter, 定義在
         * RegDumpProcess.cs), 收集通過的register位址。
         * Param :
         *   (無)
         * Return:
         *   通過目前過濾條件的位址集合
         *-------------------------------------------------------------*/
        private HashSet<ushort> BuildGenerateAllowedAddrs()
        {
            HashSet<ushort> allowed = new HashSet<ushort>();
            foreach (RegTableItem it in RegTableItems.Items)
                if (PassesFilter(it))
                    allowed.Add(it.Addr);
            return allowed;
        }

        /*-------------------------------------------------------------
         *  BuildGenerateValueOverrides
         * REG TABLE目前顯示的每一顆register值(GetCurrentRegTableValue, 定義在
         * RegDumpProcess.cs), 不受Topic/DependsOn過濾影響——過濾只決定輸出裡
         * 「有沒有這顆」, 這裡決定「有的話值是多少」。
         * Param :
         *   (無)
         * Return:
         *   位址->目前應輸出值的對照表 (涵蓋RegTableItems全部項目)
         *-------------------------------------------------------------*/
        private Dictionary<ushort, uint> BuildGenerateValueOverrides()
        {
            Dictionary<ushort, uint> overrides = new Dictionary<ushort, uint>();
            foreach (RegTableItem it in RegTableItems.Items)
                overrides[it.Addr] = GetCurrentRegTableValue(it);
            return overrides;
        }


        private void dvbTstPktFifoDgmHelpBtn_Click(object sender, EventArgs e)
        {
            string msg =
                "1. Preamble (4 Bytes) and Syncword (4 Bytes) are generated by the MAC layer automatically - they are NOT part of the FIFO data you fill in.\r\n\r\n" +
                "2. FIFO Data (Max 128 Bytes total) is what you actually write into the FIFO:\r\n" +
                ">>Length      (1 Byte)\r\n" +
                ">>Seq Number  (2 Bytes)  - optional, only present when \"Prepend Seq Num\" is enabled.\r\n" +
                ">>Payload     (Max 125 Bytes)\r\n" +
                ">>CRC         (2 Bytes)  - optional, depends on the selected CRC mode.\r\n\r\n" +
                "3. Length = byte count of (Seq Number + Payload + CRC) combined. It does NOT include itself, Preamble, or Syncword.\r\n\r\n" +
                "4. CRC is calculated over (Seq Number + Payload) only. It does NOT cover the Length byte, Preamble, or Syncword.";
            MessageBox.Show(this, msg, "FIFO Data Format Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
