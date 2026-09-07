using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ER41xxTestTool
{
    /// <summary>
    /// v1.19L: 從 EzCodeGen2 (20260713 V2.2) 整合過來的 Code Generator 功能
    /// 共用參數 (CRYSTAL/CAPIO/FREQUENCY/DATA RATE/DEVIATION/MODULATION/PA CONFIG/POWER LEVEL/TX SYNCWORD)
    /// 直接讀取 RF TEST TOOL 原有欄位; 其餘設定在 Code Gen 分頁
    /// </summary>
    public partial class MainForm : Form
    {
        public enum TxPayloadMode : int
        {
            TX_TRANSPARENT = 0x0,
            TX_TRANSPARENTwithPRESYNC = 0x3,
            TX_802154withPRESYNC = 0xF
        }
        public enum TRx_GPIOMode
        {
            [Description("TX/RX in progress")]
            TR_BSY = 0,
            [Description("TX/RX completion")]
            TR_FSH,
            [Description("TX packet in progress")]
            TX_PKT,
            TX_DATA,
            TX_PKT_O,
            RX_CD,
            RX_SYNC,
            FIFO_EXH,
            TX_DIO_DATA,
            TX_DIO_CLK,
            RX_DIO_DATA,
            RX_DIO_CLK,
            EVT_WAKEUP,
            TX_EN,
            NFC_FIELD_OK,
            NFC_BUSY
        }
        public enum TRx_InterruptMode
        {
            INT_ST_RX = 0,
            INT_ST_RXERR = 1,
            INT_ST_TX = 2,
            INT_ST_TXERR = 3,
            INT_ST_TMRTHD = 4,
            INT_ST_WAKEUP = 8,
            INT_ST_NFC = 9,
            INT_ST_TX_FIFO_AEMPTY = 10,
            INT_ST_RX_FIFO_AFULL = 11,
            INT_ST_RX_SYNCW = 12
        }

        private System.Windows.Forms.Button btnIntMode;
        private ToolStripDropDown intModeDropDown;
        private CheckedListBox intModeCheckedList;
        private List<double> freqData = new List<double>();
        private const int MAX_FREQ_COUNT = 20;

        public static bool is8BitsFormat = true;   // 8bits/32bits 輸出格式 (EzCodeGen 引擎共用旗標)

        // GENERATE 下拉選單用: null = 不過濾 (Generate all result), 非 null 時只有位址在這個集合裡的
        // register 才會被 CodeOutHelper.CodeOut() 實際寫出 (Generate filter result, 依 REG TABLE
        // 目前的 Topic/DependsOn 過濾狀態決定要包含哪些位址)。
        public static HashSet<ushort> GenerateAllowedAddrs = null;

        // Generate 現在以 REG TABLE 目前顯示的值為資料來源: null = 不套用 (理論上不會發生, RunGenerate
        // 每次都會設); 非 null 時, CodeOutHelper.CodeOut() 對於位址在這個表裡的 register, 一律改用這裡
        // 存的值蓋掉原本算出來的值再寫出。由 MainForm.BuildGenerateValueOverrides() 依 RegTableItems.Items
        // 逐顆讀 REG TABLE 目前的 RawValue 建出來, 只在 RunGenerate() 執行期間有效。
        public static Dictionary<ushort, uint> GenerateValueOverrides = null;
        private string generatedText = "";         // GENERATE 產生的內容, 供顯示與存檔用
        public string Rxsyncword_Forward = "Dectect : ";
        public string Rxsyncword_Invert = "Dectect :";
        public string Txsyncword_Forward = "Transfer :";
        public string Txsyncword_Invert = "Transfer :";
        public int intNum = 0;
        public string lastDir = AppDomain.CurrentDomain.BaseDirectory;

        // 38.4MHz crystal 用的 DATA RATE 清單 (kbps, 格式與 drList 一致)
        public string[] drList384 = {"1920.000","960.000","480.000","240.000","192.000","120.000","96.000",
                                "60.000","48.000","38.400","30.000","24.000","19.200","15.000","12.000",
                                "9.600","7.500","6.000","4.800","3.000","2.400","1.200","0.960","0.600"};

        /// <summary>
        /// 檢查 0x1988 (SYNTH_CAL_CONFIG_2) bit[23:0] 換算出的 DataRate(kbps)
        /// 是否落在目前 CRYSTAL 設定對應的合法清單 (drList / drList384) 內。
        /// REG TABLE 分頁可以把這顆 register 改成清單外的任意值, PARAMETER 分頁的
        /// txDRCombo 因為是 DropDownList 樣式反而鎖死選不到清單外的值, 兩邊需要共用同一份判斷。
        /// </summary>
        private bool TryGetLegalDataRateKbps(uint regRawValue, out double kbps)
        {
            kbps = (regRawValue & 0xFFFFFF) / 1000.0;
            double xtalVal;
            if (!double.TryParse(xtalCombo.Text, out xtalVal))
                return true;   // CRYSTAL 還沒設定好, 交給 CheckGenParameterReady() 處理, 這裡不擋
            string[] legalList = (xtalVal == 38.4) ? drList384 : drList;
            foreach (string s in legalList)
            {
                double v;
                if (double.TryParse(s, out v) && Math.Abs(v - kbps) < 0.0005)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Code Gen 功能初始化 (由 MainForm 建構子呼叫)
        /// </summary>
        private void InitCodeGenUI()
        {
            InitInterruptModeSelector();

            // FREQUENCY TABLE ListView
            freqList.View = System.Windows.Forms.View.Details;
            freqList.FullRowSelect = true;
            freqList.CheckBoxes = true;
            freqList.Columns.Clear();
            freqList.Columns.Add("Frequency (MHz)", -2);

            EnhanceButton(btnGen);
            UpdateTxGroupBoxVisible();
            syncwordOrderList(true);
            syncwordOrderList(false);
            // 沒 invert 時 lblTxBitOrder/rxBitOrder 現在會顯示 "Actual send/verify stream", 打字改
            // txSyncword/rxSyncword 也要跟著即時重算, 不然只有切 invert checkbox 才會更新, 值會顯示過期
            txSyncword.TextChanged += (s, ev) => syncwordOrderList(true);
            rxSyncword.TextChanged += (s, ev) => syncwordOrderList(false);

            // Code Gen 的設定項目都已移到上方 PARAMETER 分頁區,
            // GENERATE 隨時可用, 不再跟著分頁切換; 按下後彈出預覽視窗 (CodeGenPreviewForm) 顯示內容並可存檔。

            // 未連線時給 PA CONFIG 預設清單 (連線後由元件讀回的設定覆蓋)
            if (double.TryParse(txFreqText.Text, out double f0))
                UpdatePAConfigCombo(f0);
            // FREQUENCY 改變時 (未連線) 跟著更新 PA CONFIG 清單
            txFreqText.TextChanged += (s, ev) =>
            {
                if (!serialPort1.IsOpen && double.TryParse(txFreqText.Text, out double f))
                    UpdatePAConfigCombo(f);
            };

            // PA CONFIG(dBm) 是 10/14 時走低功率函式, POWER LV 上限是 27; 其餘 dBm 走高功率函式, 上限 127
            // (對照 TxPowerLv.cs 裡各 TRx_PAxxxMHzYYdBm_FSK() 用的是 8bit 還是 32bit 滿刻度)。
            // 用 TextChanged 而非 SelectedIndexChanged: PAConfigCombo.Text 有好幾處是直接賦值改的
            // (讀取實機設定/載入 ini), 不一定會經過 SelectedIndexChanged, TextChanged 兩種情況都能接到。
            UpdatePwrLvLabel();
            PAConfigCombo.TextChanged += (s, ev) => UpdatePwrLvLabel();
        }

        /// <summary>
        /// 未連線時依 FREQUENCY 給 PA CONFIG 選項 (EzCodeGen 的規則):
        /// x>=900 或 x<=508 → 10/23 dBm; 780<=x<900 → 14 dBm
        /// </summary>
        private void UpdatePAConfigCombo(double x)
        {
            if (serialPort1.IsOpen)   // 連線中由元件決定, 不覆蓋
                return;
            PAConfigCombo.Items.Clear();
            if (x >= 900 || x <= 508)
            {
                PAConfigCombo.Items.Add("10");
                PAConfigCombo.Items.Add("23");
            }
            else if (x >= 780 && x < 900)
            {
                PAConfigCombo.Items.Add("14");
            }
            if (PAConfigCombo.Items.Count > 0)
                PAConfigCombo.SelectedIndex = 0;
        }

        /// <summary>PA CONFIG(dBm) = 10 或 14 時 POWER LV 只到 27, 其餘 dBm 到 127; 依 PAConfigCombo 目前的值更新 PwrLvLabel 上的範圍字樣</summary>
        private void UpdatePwrLvLabel()
        {
            string paConfig = PAConfigCombo.Text.Trim();
            if (paConfig == "10" || paConfig == "14")
                PwrLvLabel.Text = "Power LV(0~27)";
            else
                PwrLvLabel.Text = "Power LV(0~127)";
        }

        /// <summary>
        /// CRYSTAL 切換時同步更換 DATA RATE 清單 (40M ↔ 38.4M)
        /// </summary>
        private void xtalCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txDRCombo.Items.Clear();
            if (xtalCombo.SelectedIndex == 0)   // 40MHz
                txDRCombo.Items.AddRange(drList);
            else                                 // 38.4MHz
                txDRCombo.Items.AddRange(drList384);
            txDRCombo.SelectedIndex = 6;         // 100 / 96 kbps
        }

        /// <summary>
        /// 螢幕夠大時完全維持原本畫面 (原始尺寸, 無捲軸);
        /// 只有當螢幕比畫面小時, 才把視窗縮進螢幕可用範圍並顯示水平/垂直捲軸。(移植自 EzCodeGen)
        /// </summary>
        private void FitToScreen()
        {
            Size design = this.ClientSize;
            Rectangle wa = Screen.FromControl(this).WorkingArea;

            // 螢幕放得下整個畫面 → 不動, 維持原始大小
            if (this.Width <= wa.Width && this.Height <= wa.Height)
                return;

            // 螢幕比畫面小 → 開啟捲軸, 並把視窗縮到螢幕可用範圍內、靠左上角
            this.AutoScroll = true;
            this.AutoScrollMinSize = design;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = wa.Location;
            this.Size = new Size(Math.Min(this.Width, wa.Width),
                                 Math.Min(this.Height, wa.Height));
        }

        private void EnhanceButton(System.Windows.Forms.Button btn)
        {
            btn.UseVisualStyleBackColor = false;
            btn.BackColor = Color.DodgerBlue;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += (s, e) => btn.BackColor = Color.RoyalBlue;
            btn.MouseLeave += (s, e) => btn.BackColor = Color.DodgerBlue;
            btn.MouseDown += (s, e) => btn.BackColor = Color.Navy;
            btn.MouseUp += (s, e) => btn.BackColor = Color.RoyalBlue;
        }

        // =============== INTERRUPT MODE 下拉勾選選單 ===============
        private void InitInterruptModeSelector()
        {
            // 1. 按鈕 (外觀看起來像 ComboBox); 固定寬度貼齊群組內緣, 避免 DPI 縮放時超出被切
            btnIntMode = new System.Windows.Forms.Button();
            btnIntMode.Text = "Select Int Event ▼";
            btnIntMode.Font = new Font("Microsoft Sans Serif", 8f);
            btnIntMode.AutoSize = false;
            btnIntMode.Location = new Point(8, 38);
            btnIntMode.Size = new Size(intModeGrp.Width - 16, 26);
            btnIntMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnIntMode.Click += BtnIntMode_Click;

            intModeGrp.Controls.Add(btnIntMode);

            // 2. CheckedListBox
            intModeCheckedList = new CheckedListBox();
            intModeCheckedList.CheckOnClick = true;
            intModeCheckedList.BorderStyle = BorderStyle.None;
            intModeCheckedList.Width = 220;
            intModeCheckedList.Height = 160;

            foreach (TRx_InterruptMode mode in Enum.GetValues(typeof(TRx_InterruptMode)))
            {
                intModeCheckedList.Items.Add(mode, false);
            }

            // 3. 用 ToolStripControlHost 包起來
            ToolStripControlHost host = new ToolStripControlHost(intModeCheckedList);
            host.Margin = Padding.Empty;
            host.Padding = Padding.Empty;
            host.AutoSize = false;
            host.Size = intModeCheckedList.Size;

            // 4. ToolStripDropDown
            intModeDropDown = new ToolStripDropDown();
            intModeDropDown.Padding = Padding.Empty;
            intModeDropDown.Items.Add(host);
            intModeCheckedList.ItemCheck += intModeCheckedList_ItemCheck;
        }

        private void BtnIntMode_Click(object sender, EventArgs e)
        {
            if (intModeDropDown.Visible)
                intModeDropDown.Close();
            else
                intModeDropDown.Show(btnIntMode, new Point(0, btnIntMode.Height));
        }

        private void UpdateTxGroupBoxVisible()
        {
            bool txAEmptySelected = intModeCheckedList.CheckedItems
                .Cast<TRx_InterruptMode>()
                .Any(m => m == TRx_InterruptMode.INT_ST_TX_FIFO_AEMPTY);

            bool txFullSelected = intModeCheckedList.CheckedItems
                .Cast<TRx_InterruptMode>()
                .Any(m => m == TRx_InterruptMode.INT_ST_RX_FIFO_AFULL);

            TXAEMPTY_Grp.Visible = txAEmptySelected;
            RXFULL_Grp.Visible = txFullSelected;

            // 計算目前勾選數量
            intNum = intModeCheckedList.CheckedItems.Count;
            showIntNum.Text = intNum.ToString();
        }

        private void intModeCheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke((MethodInvoker)UpdateTxGroupBoxVisible);
        }

        private uint GetInterruptModeBitMask()
        {
            uint tmp = 0;
            foreach (var item in intModeCheckedList.CheckedItems)
            {
                TRx_InterruptMode mode = (TRx_InterruptMode)item;
                tmp |= (1u << (int)mode);
            }
            return tmp;
        }

        public List<string> GetInterruptItem()
        {
            List<string> list = new List<string>();
            foreach (var item in intModeCheckedList.CheckedItems)
            {
                list.Add(item.ToString());
            }
            return list;
        }

        private void SetDefaultInterruptMode(TRx_InterruptMode mode, bool isChecked)
        {
            for (int i = 0; i < intModeCheckedList.Items.Count; i++)
            {
                if ((TRx_InterruptMode)intModeCheckedList.Items[i] == mode)
                {
                    intModeCheckedList.SetItemChecked(i, isChecked);
                    break;
                }
            }
        }

        // =============== GPIO ===============
        public void Initial_Gpio0_Combo()
        {
            gpio0_Combo.DataSource = Enum.GetValues(typeof(TRx_GPIOMode));
        }
        public void Initial_Gpio1_Combo()
        {
            gpio1_Combo.DataSource = Enum.GetValues(typeof(TRx_GPIOMode));
        }

        public void GPIO0_Initial()
        {
            if (GPIO0_Enable.Checked == true)
            {
                gpio0_Combo.DataSource = null;
                gpio0_Combo.SelectedIndex = -1;
                Initial_Gpio0_Combo();
                gpio0_Combo.Enabled = true;
            }
            else
                gpio0_Combo.Enabled = false;
        }

        public void GPIO1_Initial()
        {
            if (GPIO1_Enable.Checked == true)
            {
                gpio1_Combo.DataSource = null;
                gpio1_Combo.SelectedIndex = -1;
                Initial_Gpio1_Combo();
                gpio1_Combo.Enabled = true;
            }
            else
                gpio1_Combo.Enabled = false;
        }

        private void GPIO0_Enable_CheckedChanged(object sender, EventArgs e)
        {
            GPIO0_Initial();
        }

        private void GPIO1_Enable_CheckedChanged(object sender, EventArgs e)
        {
            GPIO1_Initial();
        }

        // =============== FREQUENCY TABLE ===============
        public bool CheckFreqValue(double X)
        {
            if ((X >= 1558 && X <= 2038) ||
                (X >= 780 && X <= 1018) ||
                (X >= 391 && X <= 508) ||
                (X >= 196 && X <= 253) ||
                (X >= 99 && X <= 125))
            {
                return true;
            }
            else
                return false;
        }

        public double AdjustFreqValue(double X)
        {
            // 1. 先判斷是否在定義的五個範圍內
            if ((X >= 1558 && X <= 2038) ||
                (X >= 780 && X <= 1018) ||
                (X >= 391 && X <= 508) ||
                (X >= 196 && X <= 253) ||
                (X >= 99 && X <= 125))
            {
                return X;
            }
            // 2. 如果都不在範圍內，找最接近的邊界值
            double[] boundaries = { 99, 125, 196, 253, 391, 508, 780, 1018, 1558, 2038 };
            double closestValue = boundaries.OrderBy(b => Math.Abs(b - X)).First();

            GenMsg.Show("Frequency: " + X.ToString() + "MHz is out of range:\r\n(1558 <= X <= 2038)\r\n(780 <= X <= 1018)\r\n(391 <= X <= 508)\r\n(196 <= X <= 253)\r\n(99 <= X <= 125))\r\nNow " + X.ToString() + "MHz change to " + closestValue.ToString() + "MHz", "WARNING");
            return closestValue;
        }

        public double[] GetFreqArray(double freqDouble)
        {
            if (freqData.Count == 0)
                return new double[] { freqDouble };
            return freqData.ToArray();
        }

        // 封裝一個方法來同步 UI
        private void UpdateListView(double highlightedValue)
        {
            freqList.BeginUpdate();
            freqList.Items.Clear();

            ListViewItem targetItem = null;

            foreach (double val in freqData)
            {
                ListViewItem item = new ListViewItem(val.ToString("F2"));
                item.SubItems.Add("");
                freqList.Items.Add(item);

                if (val == highlightedValue)
                {
                    targetItem = item;
                }
            }

            if (targetItem != null)
            {
                targetItem.Selected = true;
                targetItem.Focused = true;
                targetItem.EnsureVisible();
            }

            freqList.EndUpdate();
            freqList.Focus();

            // FREQ TABLE 的增/刪都會經過這裡, 是 freqData 變動的共同出口 -> 順便重算 REG TABLE
            RefreshRegisterListFromUi();
        }

        private void btnAddFreq_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(inFreq.Text))
            {
                inFreq.BackColor = Color.Yellow;
                GenMsg.Show(this, "Please enter a value before leaving.", "WARNING");
                inFreq.Focus();
                return;
            }

            string text = inFreq.Text.Trim();
            double freqDouble = Convert.ToDouble(text);
            bool res = CheckFreqValue(freqDouble);
            if (!res)
            {
                inFreq.BackColor = Color.Yellow;
                inFreq.Focus();
                freqDouble = AdjustFreqValue(freqDouble);
                inFreq.Text = freqDouble.ToString();
            }
            else
            {
                inFreq.BackColor = SystemColors.Window;
                if (!double.TryParse(text, out double freq))
                {
                    GenMsg.Show("Must be a base-10 number", "WARNING");
                    return;
                }

                // 檢查是否已存在於 freqData 中
                if (freqData.Contains(freq))
                {
                    GenMsg.Show("Value already exists", "WARNING");
                    return;
                }

                // 筆數限制
                if (freqData.Count >= MAX_FREQ_COUNT)
                {
                    GenMsg.Show("Max counts=" + MAX_FREQ_COUNT, "WARNING");
                    return;
                }

                freqData.Add(freq);
                freqData.Sort();
                UpdateListView(freq);
            }
        }

        private void RemoveSelectedFreq()
        {
            if (freqList.SelectedIndices.Count == 0)
                return;

            int index = freqList.SelectedIndices[0];
            freqData.RemoveAt(index);
            freqList.Items.RemoveAt(index);
        }

        private void freqList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && freqList.SelectedIndices.Count > 0)
            {
                RemoveSelectedFreq();
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            // 從最後一個項目開始往前檢查, 刪除所有勾選項目
            for (int i = freqList.Items.Count - 1; i >= 0; i--)
            {
                if (freqList.Items[i].Checked)
                {
                    freqData.RemoveAt(i);
                    freqList.Items.RemoveAt(i);
                }
            }
        }

        // =============== SYNCWORD (PARAMETER 內 TX/RX 共用欄位) ===============
        public void syncwordOrderList(bool isTx)
        {
            if (isTx)
            {
                if (syncLSBCheckbox.Checked)
                    // invert: 實際送出的是位元反轉後的值 (跟 Syncword.Set() 寫進 0xA038 前做的 hexReverse 是同一個轉換)
                    lblTxBitOrder.Text = Txsyncword_Invert + FormatSyncwordBytes(TrySyncwordReverse(txSyncword.Text));
                else
                    // 沒 invert: 實際送出的 bit stream 就是 txSyncword 欄位打的值本身, 直接排成 byte 顯示方便對照
                    lblTxBitOrder.Text = Txsyncword_Forward + FormatSyncwordBytes(txSyncword.Text);
            }
            else
            {
                if (rxsyncInvertCheckbox.Checked)
                    rxBitOrder.Text = Rxsyncword_Invert  + FormatSyncwordBytes(TrySyncwordReverse(rxSyncword.Text));
                else
                    rxBitOrder.Text = Rxsyncword_Forward + FormatSyncwordBytes(rxSyncword.Text);
            }
        }

        /// <summary>把 syncword 欄位打的 8 碼 hex 字串排成 "XX XX XX XX" 方便對照; 打字打到一半 (長度不對/非hex) 時原樣照顯示, 不丟例外</summary>
        private static string FormatSyncwordBytes(string hex)
        {
            string h = (hex ?? "").Trim();
            if (h.Length == 0)
                return "";
            if (h.Length % 2 != 0 || !Regex.IsMatch(h, @"\A[0-9A-Fa-f]+\z"))
                return h;   // 還沒打完/不是合法 hex, 先照原樣顯示

            List<string> bytes = new List<string>();
            for (int i = 0; i < h.Length; i += 2)
                bytes.Add(h.Substring(i, 2).ToUpperInvariant());
            return string.Join(" ", bytes);
        }

        /// <summary>只有打完整、合法的 8 碼 hex 才呼叫 hexReverse() 做位元反轉; 還沒打完/不合法時照原樣傳回, 避免 hexReverse 內部 Convert.ToUInt32 對不合法輸入丟例外 (這裡會在每次 TextChanged 都被呼叫到)</summary>
        private string TrySyncwordReverse(string hex)
        {
            string h = (hex ?? "").Trim();
            if (h.Length == 8 && Regex.IsMatch(h, @"\A[0-9A-Fa-f]{8}\z"))
                return hexReverse(h);
            return h;
        }

        private void rxsyncInvertCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            syncwordOrderList(false);
        }

        private void syncLSBCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            syncwordOrderList(true);
        }

        // =============== H 值檢查 (EzCodeGen 版本) ===============
        public double CheckDataRateGen(double dataRate, double dataRate_offset, double xtal, double fdev, double H)
        {
            double newH = 0;
            string str = "";

            switch (dataRate_offset)
            {
                case 2000000:    //2M
                    if ((H > 0) && (H <= 1))
                        newH = H;
                    else
                    {
                        newH = 1;
                        ShowInsLog("RX Frequency Derivation: " + fdev + "->");
                        fdev = newH * dataRate / 2;
                        freqDev.Text = (fdev / 1000).ToString();
                        ShowInsLog(fdev + "\r\n");
                        str = "Now H=" + H + "\r\nBut H need to be 0<H<=1\r\nHere the setting is H=1\r\nwhen FreqDev=" + fdev;
                        ShowMesgBox(str);
                    }
                    break;

                case 1000000:    //1M
                    if (H > 0 && H <= 2)
                        newH = H;
                    else
                    {
                        newH = 2;
                        ShowInsLog("RX Frequency Derivation: " + fdev + "->");
                        fdev = newH * dataRate / 2;
                        freqDev.Text = (fdev / 1000).ToString();
                        ShowInsLog(fdev + "\r\n");
                        str = "Now H=" + H + "\r\nBut H need to be 0<H<=2\r\nHere the setting is H=2\r\nwhen FreqDev=" + fdev;
                        ShowMesgBox(str);
                    }
                    break;

                case 500000:
                    if (H > 0 && H <= 3)
                        newH = H;
                    else
                    {
                        newH = 3;
                        ShowInsLog("RX Frequency Derivation: " + fdev + "->");
                        fdev = newH * dataRate / 2;
                        freqDev.Text = (fdev / 1000).ToString();
                        ShowInsLog(fdev + "\r\n");
                        str = "Now H=" + H + "\r\nBut H need to be 0<H<=3\r\nHere the setting is H=3\r\nwhen FreqDev=" + fdev;
                        ShowMesgBox(str);
                    }
                    break;

                case 250000:
                case 200000:
                    if (H > 0 && H <= 4)
                        newH = H;
                    else
                    {
                        newH = 4;
                        ShowInsLog("RX Frequency Derivation: " + fdev + "->");
                        fdev = newH * dataRate / 2;
                        freqDev.Text = (fdev / 1000).ToString();
                        ShowInsLog(fdev + "\r\n");
                        str = "Now H=" + H + "\r\nBut H need to be 0<H<=4\r\nHere the setting is H=4\r\nwhen FreqDev=" + fdev;
                        ShowMesgBox(str);
                    }
                    break;

                default:
                    if (H > 0 && H <= 8)
                        newH = H;
                    else
                    {
                        newH = 8;
                        ShowInsLog("RX Frequency Derivation: " + fdev + "->");
                        fdev = newH * dataRate / 2;
                        freqDev.Text = (fdev / 1000).ToString();
                        ShowInsLog(fdev + "\r\n");
                        str = "Now H=" + H + "\r\nBut H need to be 0<H<=8\r\nHere the setting is H=8\r\nwhen FreqDev=" + fdev;
                        ShowMesgBox(str);
                    }
                    break;
            }
            return newH;
        }

        /// <summary>
        /// RxGenerate() 判斷用的 dataRate_offset/H 公式, REG TABLE 灰字判斷 (IsRegisterGreyedOut) 共用同一份,
        /// 避免各算各的算出不同結果。純公式, 不讀任何 UI 控制項 —— drValue 由呼叫端提供,
        /// 是因為 GenerateTxt() 裡 drValue 有可能已經被「REG TABLE 的 0x1988 手動值優先」那段覆蓋過,
        /// 這裡不能自己重新從 txDRCombo.Text 算一次, 否則會跟覆蓋後的其餘計算對不起來。
        /// </summary>
        internal static void ComputeRxDispatchValues(uint drValue, uint devValue, double xtalValue, out double dataRateOffset, out double H)
        {
            dataRateOffset = drValue / (xtalValue / 40.0);
            H = Math.Round(2.0 * devValue / drValue, 4);
        }

        /// <summary>對應 RxGenerate() 底下 switch(dataRate) 的 24 個分支, 只用來讓 REG TABLE 判斷灰字, 不影響實際產生的內容 (RxGenerate 本身的 switch 保持原樣不動)。</summary>
        internal enum RxRateBucket { R2M, R1M, R500K, R250K, R200K, R125K, R100K, R62K5, R50K, R40K, R31K25, R25K, R20K, R15K625, R12K5, R10K, R7K8125, R6K25, R5K, R3K125, R2K5, R1K25, R1K, R0K625, Unknown }

        internal static RxRateBucket GetRxRateBucket(double dataRateOffset)
        {
            switch (dataRateOffset)
            {
                case 2000000: return RxRateBucket.R2M;
                case 1000000: return RxRateBucket.R1M;
                case 500000: return RxRateBucket.R500K;
                case 250000: return RxRateBucket.R250K;
                case 200000: return RxRateBucket.R200K;
                case 125000: return RxRateBucket.R125K;
                case 100000: return RxRateBucket.R100K;
                case 62500: return RxRateBucket.R62K5;
                case 50000: return RxRateBucket.R50K;
                case 40000: return RxRateBucket.R40K;
                case 31250: return RxRateBucket.R31K25;
                case 25000: return RxRateBucket.R25K;
                case 20000: return RxRateBucket.R20K;
                case 15625: return RxRateBucket.R15K625;
                case 12500: return RxRateBucket.R12K5;
                case 10000: return RxRateBucket.R10K;
                case 7812.5: return RxRateBucket.R7K8125;
                case 6250: return RxRateBucket.R6K25;
                case 5000: return RxRateBucket.R5K;
                case 3125: return RxRateBucket.R3K125;
                case 2500: return RxRateBucket.R2K5;
                case 1250: return RxRateBucket.R1K25;
                case 1000: return RxRateBucket.R1K;
                case 625: return RxRateBucket.R0K625;
                default: return RxRateBucket.Unknown;
            }
        }

        // =============== GENERATE ===============
        public int RxGenerate(StreamWriter sw, double dataRate, double H, out string modemClkReg)
        {
            int res = 0;
            modemClkReg = "";
            sw.WriteLine("    //====== " + RegCategories.Get(0xB304) + " ======");
            // dataRate 已 * BPSOffset
            // MODEM_CLK_REG (0x0004) 不在這裡直接寫: 由各 RXDRxxx.Set() 透過 out 參數回傳,
            // 留到 SetStandardParameter()/FREQSET 之後、POWER CONFIG 之前才寫, 對齊新格式範例的位置。
            switch (dataRate)
            {
                case 2000000: // 2M
                    res = RXDR2M.Set(sw, H, out modemClkReg);
                    break;
                case 1000000: //1M
                    res = RXDR1M.Set(sw, H, out modemClkReg);
                    break;
                case 500000:  // 500K
                    res = RXDR500K.Set(sw, H, out modemClkReg);
                    break;
                case 250000:  // 250K
                    res = RXDR250K.Set(sw, H, out modemClkReg);
                    break;
                case 200000: // 200K
                    res = RXDR200K.Set(sw, H, out modemClkReg);
                    break;
                case 125000:  // 125K
                    res = RXDR125K.Set(sw, H, out modemClkReg);
                    break;
                case 100000:  // 100K
                    res = RXDR100K.Set(sw, H, out modemClkReg);
                    break;
                case 62500: // 62.5K
                    res = RXDR62K5.Set(sw, H, out modemClkReg);
                    break;
                case 50000: // 50K
                    res = RXDR50K.Set(sw, H, out modemClkReg);
                    break;
                case 40000: // 40K
                    res = RXDR40K.Set(sw, H, out modemClkReg);
                    break;
                case 31250: // 31.25K
                    res = RXDR31K25.Set(sw, H, out modemClkReg);
                    break;
                case 25000: // 25K
                    res = RXDR25K.Set(sw, H, out modemClkReg);
                    break;
                case 20000: // 20K
                    res = RXDR20K.Set(sw, H, out modemClkReg);
                    break;
                case 15625: // 15.625K
                    res = RXDR15K625.Set(sw, H, out modemClkReg);
                    break;
                case 12500:    //12.5K
                    res = RXDR12K5.Set(sw, H, out modemClkReg);
                    break;
                case 10000: // 10K
                    res = RXDR10K.Set(sw, H, out modemClkReg);
                    break;
                case 7812.5:  //7.8125K
                    res = RXDR7K8125.Set(sw, H, out modemClkReg);
                    break;
                case 6250: //6.25K
                    res = RXDR6K25.Set(sw, H, out modemClkReg);
                    break;
                case 5000: //5K
                    res = RXDR5K.Set(sw, H, out modemClkReg);
                    break;
                case 3125: //3.125K
                    res = RXDR3K125.Set(sw, H, out modemClkReg);
                    break;
                case 2500:  // 2.5K
                    res = RXDR2K5.Set(sw, H, out modemClkReg);
                    break;
                case 1250:  // 1.25K
                    res = RXDR1K25.Set(sw, H, out modemClkReg);
                    break;
                case 1000:  // 1K
                    res = RXDR1K.Set(sw, H, out modemClkReg);
                    break;
                case 625:  // 0.625K
                    res = RXDR0K625.Set(sw, H, out modemClkReg);
                    break;
                default:
                    ShowInsLog("WRONG DataRate\r\n");
                    res = 1;
                    break;
            }
            return res;
        }

        /// <param name="devRegValue">寫入 0x1984 (SYNTH_CAL_OFFSET_IN) 的值, 呼叫端需已乘 2 (= 2 * Deviation(Hz))</param>
        public void SetStandardParameter(StreamWriter sw, int freqValue, uint devRegValue, uint drValue)
        {
            uint i = 1;
            string hex = $"0x{freqValue:X8},";
            sw.WriteLine("    //====== " + RegCategories.Get(0x1980) + " ======");
            CodeOutHelper.CodeOut(sw, "0x1980, " + hex);
            hex = $"0x{devRegValue:X8},";
            CodeOutHelper.CodeOut(sw, "0x1984, " + hex);
            hex = $"0x{drValue:X8},";
            CodeOutHelper.CodeOut(sw, "0x1988, " + hex);
            hex = $"0x{i:X8},";
            CodeOutHelper.CodeOut(sw, "0x1990, " + hex);
            // 0x1E04 移到 SetTx.TxRegister() 了 (跟 0x13AC 同屬 TRx_TX_SETUP() 那組, 不屬於這裡)。
        }

        public void RxExtent(StreamWriter sw, uint dataRate)
        {
            uint i = 0;
            //固定寫 0xb19c 及 0xb1a8
            CodeOutHelper.CodeOut(sw, "0xb19c, 0x00015f04,");
            CodeOutHelper.CodeOut(sw, "0xb1a8, 0x00000050,");
            // CSR中沒有
            if (dataRate > 1000000)
            {
                CodeOutHelper.CodeOut(sw, "0xb000, 0x00000986,");
                i = 0x4;
            }
            else
            {
                CodeOutHelper.CodeOut(sw, "0xb000, 0x00000906,");
                i = 0x2;
            }
            string hex = $"0x{i:X8},";
            CodeOutHelper.CodeOut(sw, "0x1188, " + hex);
        }

        /// <summary>
        /// GENERATE 前檢查必要參數是否為空, 空的話提示並回 false
        /// </summary>
        private bool CheckGenParameterReady()
        {
            string missing = "";
            if (string.IsNullOrWhiteSpace(xtalCombo.Text)) missing += "CRYSTAL\r\n";
            if (string.IsNullOrWhiteSpace(capioStr.Text)) missing += "CAPIO\r\n";
            if (string.IsNullOrWhiteSpace(txFreqText.Text)) missing += "FREQUENCY\r\n";
            if (string.IsNullOrWhiteSpace(txDRCombo.Text)) missing += "DATA RATE\r\n";
            if (string.IsNullOrWhiteSpace(freqDev.Text)) missing += "DEVIATION\r\n";
            if (string.IsNullOrWhiteSpace(PAConfigCombo.Text)) missing += "PA CONFIG\r\n";
            if (string.IsNullOrWhiteSpace(pwrlvl.Text)) missing += "POWER LEVEL\r\n";
            if (missing != "")
            {
                GenMsg.Show("Below parameter(s) is empty:\r\n\r\n" + missing, "GENERATE");
                return false;
            }
            return true;
        }

        public bool GenerateTxt(string apiVerison)
        {
            bool res = false;
            if (!CheckGenParameterReady())
                return false;
            bool isTXEmpty = TXAEMPTY_Grp.Visible;
            bool isRXFull = RXFULL_Grp.Visible;
            int txEmptyThrValue = -1;
            int rxFullThrValue = -1;
            bool enGpio0 = GPIO0_Enable.Checked;
            bool enGpio1 = GPIO1_Enable.Checked;
            int gpio0Type = 0;
            int gpio1Type = 0;
            if (enGpio0)
                gpio0Type = gpio0_Combo.SelectedIndex;
            if (enGpio1)
                gpio1Type = gpio1_Combo.SelectedIndex;
            bool pcrmuEn = pcrmuEnable.Checked;
            bool msbEnable = payloadMSB.Checked;
            bool isGFSK = true;
            uint crystal_HZ = 0;
            uint drValue = 0;
            uint devValue = 0;
            string xtal = xtalCombo.Text;                       // RF TOOL 固定 40MHz
            double xtalValue = Convert.ToDouble(xtal);
            string capio = capioStr.Text;
            string freq = txFreqText.Text;                      // 共用 RF TOOL 的 FREQUENCY 欄位
            double drTextValue = double.Parse(txDRCombo.Text);
            double freqDevDouble = Convert.ToDouble(freqDev.Text);
            double freqDouble = Convert.ToDouble(txFreqText.Text);

            int freqValue = (int)(1000000 * freqDouble);
            drValue = (uint)(1000 * drTextValue);
            devValue = (uint)(1000 * freqDevDouble);
            crystal_HZ = (UInt32)(xtalValue * 1000000);

            // REG TABLE 為主: 如果使用者手動改過 REG TABLE 的 0x1988 (DataRate), 跟 PARAMETER
            // 算出來的 drValue 不一樣, 以 REG TABLE 目前顯示的值為準, 但必須是合法離散值才放行。
            // 只在真的按下 GENERATE 鍵時生效 (GenMsg.Quiet 只有 REG TABLE 背景自動重算
            // BuildRegistersFromUi() 會設成 true, 那段呼叫必須維持「純粹依 PARAMETER 算」,
            // 否則 PARAMETER 分頁改了 DataRate 也會被卡在 REG TABLE 的舊值上, 自動重算會失效)。
            if (!GenMsg.Quiet && regItemsByName.TryGetValue("SYNTH_CAL_CONFIG_2", out RegItemTag drRegTag) && drRegTag.RawValue.HasValue)
            {
                uint regTableDrValue = drRegTag.RawValue.Value & 0xFFFFFF;
                if (regTableDrValue != drValue)
                {
                    double regTableKbps;
                    if (!TryGetLegalDataRateKbps(regTableDrValue, out regTableKbps))
                    {
                        GenMsg.Show("REG TABLE DataRate (0x1988 = 0x" + regTableDrValue.ToString("X6") +
                            " -> " + regTableKbps.ToString("0.000") + " kbps) is not valid. Cannot GENERATE.\r\n" +
                            "Please fix the REG TABLE value, or reapply DATA RATE from the PARAMETER tab to REG TABLE.", "GENERATE");
                        return res;
                    }
                    drValue = regTableDrValue;
                }
            }
            // ------------------ interrupt mode -----------------------
            uint intMode = GetInterruptModeBitMask();
            if (isTXEmpty)
            {
                txEmptyThrValue = CheckInput.CheckTxRxThr(txAemptyThr.Text, 0, 128);
                if (txEmptyThrValue == 999)
                {
                    GenMsg.Show("Value out of range", "TX_AEMPTY_THR");
                    return res;
                }
            }
            if (isRXFull)
            {
                rxFullThrValue = CheckInput.CheckTxRxThr(rxFullThr.Text, 1, 128);
                if (rxFullThrValue == 999)
                {
                    GenMsg.Show("Value out of range", "RX_FULL_THR");
                    return res;
                }
            }
            List<string> interruptList = GetInterruptItem();
            // ------------------ interrupt mode END -----------------------
            double[] freqArray = GetFreqArray(freqDouble);   //unit: MHz
            int freqArraySize = freqArray.Length;
            // 檢查 freqArray 中是否存在與 freqDouble 相同的值
            if (freqArraySize > 0)
            {
                if (!freqArray.Any(x => x == freqDouble))
                {
                    DialogResult result = GenMsg.Show(
                        "The frequency value is not including in frequency table, do you really want to generate this array again?",
                        "Warning",
                        MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Cancel)
                    {
                        return res;
                    }
                }
            }
            int capValue = Convert.ToInt32(capio);
            if (capValue > 127 || capValue < 0)
            {
                GenMsg.Show("CAP VALUE: 0 < CAP < 128", "WARNING");
                return res;
            }
            string mod = txModCombo.Text;
            string PAConfig = PAConfigCombo.Text;
            string pwr = pwrlvl.Text;

            uint txPayloadType = 0;
            if (radioTransparent.Checked)
                txPayloadType = (int)TxPayloadMode.TX_TRANSPARENT;
            else if (radioTransWithPresync.Checked)
                txPayloadType = (int)TxPayloadMode.TX_TRANSPARENTwithPRESYNC;
            else if (radio802154.Checked)
                txPayloadType = (int)TxPayloadMode.TX_802154withPRESYNC;

            string txsyncword = CheckInput.GetSyncword(txSyncword.Text);   // PARAMETER 內共用的 TX SYNCWORD
            if (txsyncword == null)
                return res;
            if (syncLSBCheckbox.Checked)                       // PARAMETER 內共用的 TX INVERT
                txsyncword = hexReverse(txsyncword);
            string rxsyncword = CheckInput.GetSyncword(rxSyncword.Text);
            if (rxsyncword == null)
                return res;
            if (rxsyncInvertCheckbox.Checked)
                rxsyncword = hexReverse(rxsyncword);

            uint bitError = CheckInput.GetAllowBitError(allowBitError.Text);
            if (bitError > 7)
                return res;

            uint preambleContentValue = CheckInput.GetPreambleContent(preambleContent.Text.Trim());
            if (preambleContentValue > 300)
                return res;
            uint preambleLengthValue = CheckInput.GetPreambleLength(preambleLen.Text.Trim());
            if (preambleLengthValue > 300)
                return res;
            uint pwrlvlValue = Convert.ToUInt32(pwr);
            double dataRate_offset;
            double H;
            ComputeRxDispatchValues(drValue, devValue, xtalValue, out dataRate_offset, out H);

            if ((drValue == 1920000) && (H > 0.5))
            {
                ShowInsLog("ERROR !!!\r\n");
                ShowInsLog("DR=1.92Mbps, H can not bigger than 0.5!!\r\n");
                return res;
            }
            if ((drValue == 960000) && (H > 1))
            {
                ShowInsLog("ERROR !!!\r\n");
                ShowInsLog("DR=0.96Mbps, H can not bigger than 1 !!\r\n");
                return res;
            }
            H = CheckDataRateGen(drValue, dataRate_offset, xtalValue, devValue, H);

            // 產生到記憶體字串 (不存檔)，結果顯示在預覽視窗；存檔改由預覽視窗的 SAVE 鈕負責
            using (MemoryStream ms = new MemoryStream())
            using (StreamWriter sw = new StreamWriter(ms, Encoding.Default))
            {
                StandardTitle.WriteItem(sw, apiVerison);
                Comment.WriteItem(sw, xtal, capio, freqDouble.ToString(), drValue.ToString(), devValue.ToString(), mod, PAConfig, pwr, preambleContentValue, preambleLengthValue, txsyncword, rxsyncword, bitError, interruptList);
                DefineItem.WriteItem(sw, drValue, devValue, txsyncword, rxsyncword, (xtal == "38.4"), freqArraySize);
                StructItem.WriteItem(sw);
                if (freqArraySize > 0)                               // Unit MHz
                    channel_pll_table.WriteItem(sw, (xtal == "38.4"), freqArray, freqArraySize, (double)drValue, H);

                if (IncludeInitArrayCheckBox.Checked)
                {
                    // Initial_Reg_Array 的位址不在 RegTableItems 的範圍內, 不受 Topic/DependsOn 過濾、
                    // 也不受「Generate 以 REG TABLE 目前值為資料來源」影響 (只受這顆 checkbox 控制),
                    // 產生期間暫時關掉 GenerateAllowedAddrs/GenerateValueOverrides。
                    HashSet<ushort> savedFilter = GenerateAllowedAddrs;
                    Dictionary<ushort, uint> savedOverrides = GenerateValueOverrides;
                    GenerateAllowedAddrs = null;
                    GenerateValueOverrides = null;
                    try
                    {
                        if (is8BitsFormat)
                            Initial_Reg_Array.WriteItem8Bits(sw);
                        else
                            Initial_Reg_Array.WriteItem32Bits(sw);
                    }
                    finally
                    {
                        GenerateAllowedAddrs = savedFilter;
                        GenerateValueOverrides = savedOverrides;
                    }
                }

                sw.WriteLine("code const t_reg_array code TRx_Config_Array[] = {");
                // 區塊順序對齊 SPI_ER41xx_config_38C_output.h 新格式:
                //   PA SETTING -> TX SETTING -> RX SETTING -> Set Freq/Dev/DataRate(含 channel 0 PLL cal) -> POWER CONFIG
                // 這幾個區塊寫的位址互不重疊, 所以換順序不會改變任何 register 的最終值。
                PaSetting.Set(sw);      // 固定的 PA 對照表 0x1F00~0x1F38, 與參數無關

                // if (xtalValue == 38.4)
                // {
                //     // BB_setting Excel 的 TOOL_CSR_GEN 分頁, N 欄起(含 0x13ac/0xa030 這組)都標註
                //     // "If xtal=38.4MHz", 只有 38.4MHz 才需要算這兩顆; 40MHz 維持 Initial_Reg_Array
                //     // 的預設值 (0x13AC=0x30300028, 0xA030=0x00140000) 不覆寫。
                //     SetTx.TxRegister(sw, crystal_HZ, drValue, isGFSK);
                // }
                SetTx.TxRegister(sw, crystal_HZ, drValue, isGFSK);
                //SetTx.TxCondition_1(sw, drValue, freqDouble, H);   // 0x1380 最終生效值在此寫入

                string modemClkReg;
                RxGenerate(sw, dataRate_offset, H, out modemClkReg);    // Version_2
                RxExtent(sw, drValue);   // CSR_Tool 中沒有的

                // reg 0x1984 (SYNTH_CAL_OFFSET_IN) = 2 * Frequency Deviation(Hz);
                // devValue 本身其餘地方 (H 計算、GLB_DEVIATION 註解) 代表的是物理 fdev(Hz), 只有寫進 register 時才乘 2。
                SetStandardParameter(sw, freqValue, devValue * 2, drValue);
                FREQSET.TxCondition_1(sw, drValue, freqDouble, H);   // 0x1380 最終生效值在此寫入
                if (xtalValue == 38.4)
                {
                    //38.4M 才需要設的 channel 0 PLL calibration 值 (0x1998/199C/19A0/1994)
                    //緊接在 Set Freq/Dev/DataRate 區塊後面, 對齊新格式範例的順序
                    FREQSET.SetFrequency(sw, freqDouble, (double)drValue, H);
                }
                if (!string.IsNullOrEmpty(modemClkReg))
                    CodeOutHelper.CodeOut(sw, modemClkReg);   // MODEM_CLK_REG, 對齊新格式範例的位置 (POWER CONFIG 之前)

                TxPowerLv.Set(sw, freqValue, PAConfig, pwrlvlValue);
                if (is8BitsFormat)
                    sw.WriteLine("    TABLE_END_ADDR, { TABLE_END_DATA_A, TABLE_END_DATA_B, TABLE_END_DATA_C, TABLE_END_DATA_D}};");
                else
                    sw.WriteLine("    TABLE_END_ADDR,  TABLE_END_DATA};");
                sw.WriteLine("");
                // Write TRx_ConfigEx_Array
                sw.WriteLine("code const t_reg_array code TRx_ConfigEx_Array[] = {");
                WriteCapValue.Set(sw, (uint)capValue);
                if (enGpio0)
                    GPIO_Def.SetGpio0(sw, enGpio0, gpio0Type);
                if (enGpio1)
                    GPIO_Def.SetGpio1(sw, enGpio1, gpio1Type);
                PCRMU.Set(sw, pcrmuEn);
                EnableInt.Set(sw, intMode);
                if (isTXEmpty || isRXFull)
                {
                    if (txEmptyThrValue < 0)
                        txEmptyThrValue = 0;
                    if (rxFullThrValue < 0)
                        rxFullThrValue = 0;
                    EnableInt.SetTxRxThr(sw, (uint)txEmptyThrValue, (uint)rxFullThrValue);
                }
                Preamble.Set(sw, preambleContentValue, preambleLengthValue);
                TxPayloadFormat.Set(sw, txPayloadType);
                Syncword.Set(sw, txsyncword, rxsyncword, bitError);
                PayloadOrder.Set(sw, msbEnable);
                if (is8BitsFormat)
                    sw.WriteLine("    TABLE_END_ADDR, { TABLE_END_DATA_A, TABLE_END_DATA_B, TABLE_END_DATA_C, TABLE_END_DATA_D}};");
                else
                    sw.WriteLine("    TABLE_END_ADDR,  TABLE_END_DATA};");
                EndOfFile.WriteItem(sw);
                sw.Flush();
                string rawText = Encoding.Default.GetString(ms.ToArray());
                generatedText = is8BitsFormat ? NamedMacroFormatter.Convert(rawText) : rawText;
                res = true;
            }
            string currentTime = DateTime.Now.ToString("HH:mm:ss");
            if (res)
                ShowInsLog("Generate at " + currentTime + "\r\n");
            else
                ShowInsLog("Generate failed at " + currentTime + "\r\n");
            return res;
        }

        private void SaveGeneratedTextToFile(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                GenMsg.Show("Please click GENERATE first to produce the content before saving.", "SAVE");
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = lastDir;
                saveFileDialog.Title = "SAVE FILE at";
                saveFileDialog.FileName = "SPI_ER41xx_config.h"; // 預設檔名

                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                    return;

                string filePath = saveFileDialog.FileName;
                lastDir = Path.GetDirectoryName(filePath);
                File.WriteAllText(filePath, text, Encoding.Default);

                string currentTime = DateTime.Now.ToString("HH:mm:ss");
                ShowInsLog("Save: " + filePath + "   at " + currentTime + "\r\n");

                // 存檔後自動用記事本開啟該檔案   //owen
                //System.Diagnostics.Process.Start("notepad.exe", filePath);
            }
        }

        // =============== LOAD/SAVE PARAMETER (EZCODE ini) ===============
        private void loadParameterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string iniFileLoc = ChooseEzINIFile();
            if (iniFileLoc.Equals("WRONG"))
            {
                ShowInsLog("********* WRONG INI FILE *********\r\n");
            }
            else
            {
                ShowInsLog("Choose ini File =" + iniFileLoc + "\r\n");
                LoadEzIniFile(iniFileLoc);
                syncwordOrderList(true);
                syncwordOrderList(false);
            }
        }

        private void saveParameterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveEzIniProcess();
        }

        /// <summary>
        /// 程式打開時把 PARAMETER 等分頁套上官方預設值 (DefaultParameters.IniContent,
        /// 38.4MHz/435MHz/96kbps/48kHz/PA23dBm, 見該檔案註解)。直接借用 LoadEzIniFile()
        /// 既有的逐欄位解析/比對邏輯 (跟「LOAD PARAMETER」選單走同一條路), 不另寫一套；
        /// 因為 LoadEzIniFile 是靠 Win32 GetPrivateProfileString 讀實體檔案, 內容先寫到暫存檔
        /// 再讀, 讀完即刪, 預設值本身還是編譯在 exe 裡, 執行期不依賴外部檔案是否存在。
        /// </summary>
        private void ApplyDefaultParameters()
        {
            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "ER41xx_DefaultParameters.ini");
            try
            {
                File.WriteAllText(tempPath, DefaultParameters.IniContent);
                LoadEzIniFile(tempPath);
                syncwordOrderList(true);
                syncwordOrderList(false);
            }
            finally
            {
                try { File.Delete(tempPath); } catch { }
            }
        }

        public string ChooseEzINIFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            fileDialog.Filter = "ini file|*.ini";
            fileDialog.DefaultExt = ".ini";
            string fileStr = "";
            string tmpStr = "";
            bool res = false;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                fileStr = fileDialog.FileName;
                using (var sr = new StreamReader(fileDialog.FileName))
                {
                    tmpStr = sr.ReadLine();
                    // "EZCODE": 舊版 EzCodeGen 存的 ini (相容讀取); "RF TEST TOOL": 現行 ToolTitle 存的檔頭
                    if (tmpStr != null && (tmpStr.Equals("EZCODE") || tmpStr.Contains("RF TEST TOOL")))
                    {
                        res = true;
                    }
                }
            }
            if (!res)
            {
                fileStr = "WRONG";
            }
            return fileStr;
        }

        public void LoadEzIniFile(string fileStr)
        {
            var MyIni = new IniFile3(fileStr);
            string tmpRead = "";
            double tmpDouble = 0.0;
            int tmpInt = 0;
            // [BAUDRATE] / [CONFIG]FREQBAND_CONFIG: 沿用自舊版 setting.ini, EZCODE 格式原本沒有
            if (MyIni.KeyExists("BR", "BAUDRATE"))
            {
                tmpRead = MyIni.Read("BR", "BAUDRATE").Replace(" ", "");
                if (int.TryParse(tmpRead, out tmpInt))
                {
                    baudRateNumber = tmpInt;
                    ShowInsLog("Baudrate = " + baudRateNumber + " bps\r\n");
                }
                else
                    GenMsg.Show("[BAUDRATE]BR value is wrong");
            }
            if (MyIni.KeyExists("FREQBAND_CONFIG", "CONFIG"))
                DVBBandCombo.Text = MyIni.Read("FREQBAND_CONFIG", "CONFIG").Replace(" ", "");
            // [PARAMETER]CRYSTAL: 要在 DATARATE 之前套用, 因為切 xtalCombo 會連動換掉 txDRCombo
            // 的候選清單 (40MHz->drList / 38.4MHz->drList384, 見 xtalCombo_SelectedIndexChanged),
            // 晚套用的話 DATARATE 會拿舊清單去比對, 找不到 (例如 38.4MHz 才有的 "96.000") 就悄悄跳過。
            if (MyIni.KeyExists("CRYSTAL", "PARAMETER"))
            {
                tmpRead = MyIni.Read("CRYSTAL", "PARAMETER");
                if (double.TryParse(tmpRead.Replace(" ", ""), out tmpDouble))
                {
                    for (int i = 0; i < xtalCombo.Items.Count; i++)
                    {
                        if (double.TryParse(xtalCombo.Items[i].ToString(), out double itemVal) && itemVal == tmpDouble)
                        {
                            xtalCombo.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else
                    GenMsg.Show("[PARAMETER]CRYSTAL value is wrong");
            }
            if (MyIni.KeyExists("CAPIO", "PARAMETER"))
            {
                tmpRead = MyIni.Read("CAPIO", "PARAMETER");
                if (int.TryParse(tmpRead.Replace(" ", ""), out tmpInt))
                    capioStr.Text = tmpInt.ToString();
                else
                    GenMsg.Show("[PARAMETER]CAPIO value is wrong");
            }
            if (MyIni.KeyExists("FREQUENCY", "PARAMETER"))
            {
                tmpRead = MyIni.Read("FREQUENCY", "PARAMETER");
                if (double.TryParse(tmpRead.Replace(" ", ""), out tmpDouble))
                    txFreqText.Text = tmpDouble.ToString();
                else
                    GenMsg.Show("[PARAMETER]FREQUENCY value is wrong");
            }
            if (MyIni.KeyExists("DATARATE", "PARAMETER"))
            {
                tmpRead = MyIni.Read("DATARATE", "PARAMETER");
                // 以數值比對 (EzCodeGen 存 "100", RF TOOL 顯示 "100.000")
                if (double.TryParse(tmpRead.Replace(" ", ""), out tmpDouble))
                {
                    for (int i = 0; i < txDRCombo.Items.Count; i++)
                    {
                        if (double.TryParse(txDRCombo.Items[i].ToString(), out double itemVal) && itemVal == tmpDouble)
                        {
                            txDRCombo.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            if (MyIni.KeyExists("DEVIATION", "PARAMETER"))
            {
                tmpRead = MyIni.Read("DEVIATION", "PARAMETER");
                if (double.TryParse(tmpRead.Replace(" ", ""), out tmpDouble))
                    freqDev.Text = tmpDouble.ToString();
                else
                    GenMsg.Show("[PARAMETER]FREQ DEV value is wrong");
            }
            if (MyIni.KeyExists("MODULATION", "PARAMETER"))
            {
                tmpRead = MyIni.Read("MODULATION", "PARAMETER");
                txModCombo.Text = tmpRead;
            }
            if (MyIni.KeyExists("PACONFIG", "PARAMETER"))
            {
                tmpRead = MyIni.Read("PACONFIG", "PARAMETER");
                if (int.TryParse(tmpRead.Replace(" ", ""), out tmpInt))
                    PAConfigCombo.Text = tmpInt.ToString();
                else
                    GenMsg.Show("[PARAMETER]PACONFIG value is wrong");
            }
            if (MyIni.KeyExists("POWERLEVEL", "PARAMETER"))
            {
                tmpRead = MyIni.Read("POWERLEVEL", "PARAMETER");
                if (int.TryParse(tmpRead.Replace(" ", ""), out tmpInt))
                    pwrlvl.Text = tmpInt.ToString();
                else
                    GenMsg.Show("[PARAMETER]POWERLEVEL value is wrong");
            }

            if (MyIni.KeyExists("CONTENT", "PREAMBLE"))
            {
                tmpRead = MyIni.Read("CONTENT", "PREAMBLE");
                preambleContent.Text = tmpRead;
            }
            if (MyIni.KeyExists("LEN", "PREAMBLE"))
            {
                tmpRead = MyIni.Read("LEN", "PREAMBLE");
                if (int.TryParse(tmpRead.Replace(" ", ""), out tmpInt))
                    preambleLen.Text = tmpInt.ToString();
                else
                    GenMsg.Show("[PREAMBLE]LEN value is wrong");
            }

            if (MyIni.KeyExists("MSB", "PAYLOADORDER"))
            {
                tmpRead = MyIni.Read("MSB", "PAYLOADORDER").ToUpper();
                if (tmpRead.Equals("TRUE"))
                    payloadMSB.Checked = true;
                else
                    payloadLSB.Checked = true;
            }

            if (MyIni.KeyExists("VALUE", "TXPAYLOADFORMAT"))
            {
                tmpRead = MyIni.Read("VALUE", "TXPAYLOADFORMAT").ToUpper();
                if (tmpRead.Equals("TRANSPARENT"))
                    radioTransparent.Checked = true;
                else if (tmpRead.Equals("802154"))
                    radio802154.Checked = true;
                else
                    radioTransWithPresync.Checked = true;
            }

            if (MyIni.KeyExists("ENABLE", "PCRMU"))
            {
                tmpRead = MyIni.Read("ENABLE", "PCRMU").ToUpper();
                if (tmpRead.Equals("TRUE"))
                    pcrmuEnable.Checked = true;
                else
                    pcrmuDisable.Checked = true;
            }

            if (MyIni.KeyExists("TXSYNCWORD", "SYNCWORD"))
            {
                tmpRead = MyIni.Read("TXSYNCWORD", "SYNCWORD");
                txSyncword.Text = tmpRead;
            }
            if (MyIni.KeyExists("TXINVERT", "SYNCWORD"))
            {
                tmpRead = MyIni.Read("TXINVERT", "SYNCWORD").ToUpper();
                if (tmpRead.Equals("TRUE"))
                    syncLSBCheckbox.Checked = true;
                else
                    syncLSBCheckbox.Checked = false;
            }
            if (MyIni.KeyExists("RXSYNCWORD", "SYNCWORD"))
            {
                tmpRead = MyIni.Read("RXSYNCWORD", "SYNCWORD");
                rxSyncword.Text = tmpRead;
            }
            if (MyIni.KeyExists("RXINVERT", "SYNCWORD"))
            {
                tmpRead = MyIni.Read("RXINVERT", "SYNCWORD").ToUpper();
                if (tmpRead.Equals("TRUE"))
                    rxsyncInvertCheckbox.Checked = true;
                else
                    rxsyncInvertCheckbox.Checked = false;
            }

            if (MyIni.KeyExists("ALLOWBITERROR", "SYNCWORD"))
            {
                tmpRead = MyIni.Read("ALLOWBITERROR", "SYNCWORD");
                if (int.TryParse(tmpRead.Replace(" ", ""), out tmpInt))
                    allowBitError.Text = tmpInt.ToString();
                else
                    GenMsg.Show("[SYNCWORD]ALLOWBITERROR value is wrong");
            }

            // [INTERRUPT] 各 mode
            foreach (TRx_InterruptMode mode in Enum.GetValues(typeof(TRx_InterruptMode)))
            {
                string keyName = mode.ToString();
                if (MyIni.KeyExists(keyName, "INTERRUPT"))
                {
                    tmpRead = MyIni.Read(keyName, "INTERRUPT").ToUpper();
                    SetDefaultInterruptMode(mode, tmpRead.Equals("TRUE"));
                }
            }

            if (MyIni.KeyExists("VALUE", "TX_AEMPTY_THR"))
            {
                tmpRead = MyIni.Read("VALUE", "TX_AEMPTY_THR");
                if (int.TryParse(tmpRead.Replace(" ", ""), out tmpInt))
                    txAemptyThr.Text = tmpInt.ToString();
                else
                    GenMsg.Show("[TX_AEMPTY_THR]VALUE value is wrong");
            }

            if (MyIni.KeyExists("VALUE", "RX_AFULL_THR"))
            {
                tmpRead = MyIni.Read("VALUE", "RX_AFULL_THR");
                if (int.TryParse(tmpRead.Replace(" ", ""), out tmpInt))
                    rxFullThr.Text = tmpInt.ToString();
                else
                    GenMsg.Show("[RX_AFULL_THR]VALUE value is wrong");
            }

            if (MyIni.KeyExists("ENABLE", "GPIO0"))
            {
                tmpRead = MyIni.Read("ENABLE", "GPIO0").ToUpper();
                GPIO0_Enable.Checked = tmpRead.Equals("TRUE");
            }
            if (MyIni.KeyExists("VALUE", "GPIO0"))
            {
                tmpRead = MyIni.Read("VALUE", "GPIO0");
                if (Enum.TryParse<TRx_GPIOMode>(tmpRead, out var mode))
                    gpio0_Combo.SelectedItem = mode;
                else
                    gpio0_Combo.SelectedItem = TRx_GPIOMode.TR_BSY;
            }

            if (MyIni.KeyExists("ENABLE", "GPIO1"))
            {
                tmpRead = MyIni.Read("ENABLE", "GPIO1").ToUpper();
                GPIO1_Enable.Checked = tmpRead.Equals("FALSE");
            }
            if (MyIni.KeyExists("VALUE", "GPIO1"))
            {
                tmpRead = MyIni.Read("VALUE", "GPIO1");
                if (Enum.TryParse<TRx_GPIOMode>(tmpRead, out var mode))
                    gpio1_Combo.SelectedItem = mode;
                else
                    gpio1_Combo.SelectedItem = TRx_GPIOMode.TR_BSY;
            }

            freqData.Clear();
            freqList.Items.Clear();
            if (MyIni.KeyExists("NUM", "FREQUENCYTABLE"))
            {
                tmpRead = MyIni.Read("NUM", "FREQUENCYTABLE");
                if (int.TryParse(tmpRead.Replace(" ", ""), out tmpInt))
                {
                    int listSize = tmpInt;
                    if (tmpInt > 0)
                    {
                        freqData.Clear();
                        for (int i = 0; i < listSize; i++)
                        {
                            if (MyIni.KeyExists("VALUE" + (i + 1), "FREQUENCYTABLE"))
                            {
                                tmpRead = MyIni.Read("VALUE" + (i + 1), "FREQUENCYTABLE").ToUpper();
                                if (double.TryParse(tmpRead.Replace(" ", ""), out tmpDouble))
                                {
                                    freqData.Add(tmpDouble);
                                    ListViewItem item = new ListViewItem(tmpDouble.ToString());
                                    freqList.Items.Add(item);
                                }
                                else
                                {
                                    GenMsg.Show("[FREQUENCYTABLE]VALUE" + (i + 1) + " is wrong");
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                    GenMsg.Show("[FREQUENCYTABLE] NUM is wrong");
            }

            // 上面每個欄位都是直接設 .Text/.Checked/.SelectedItem, 不是使用者操作,
            // 不會觸發 HookParameterChangeEvents() 掛的 Leave/SelectionChangeCommitted,
            // REG TABLE 不會自動跟著動 —— 這裡讀完整份 ini 後補一次明確重算。
            // (MainForm_Load 呼叫 ApplyDefaultParameters() 時 bRegAutoRefreshReady 還是 false,
            // 這裡呼叫等於no-op, 不會跟後面 ApplyUiValuesToRegisterList() 重複算。)
            RefreshRegisterListFromUi();
        }

        public void SaveEzIniProcess()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save Configuration File";
                saveFileDialog.Filter = "Configuration files (*.ini)|*.ini";
                saveFileDialog.DefaultExt = "ini";
                saveFileDialog.AddExtension = true;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                saveFileDialog.FileName = "settings.ini";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;
                    SaveEzDataToIni(filePath);
                    ShowInsLog(filePath + "\r\n");
                }
            }
        }

        public void SaveEzDataToIni(string filePath)
        {
            var MyIni = new IniFile3(filePath);
            MyIni.Clear();
            File.AppendAllText(filePath, ToolTitle + "\r\n");
            File.AppendAllText(filePath, Environment.NewLine);
            MyIni.Write("BR", baudRateNumber.ToString(), "BAUDRATE");
            File.AppendAllText(filePath, Environment.NewLine);
            MyIni.Write("FREQBAND_CONFIG", DVBBandCombo.Text, "CONFIG");
            File.AppendAllText(filePath, Environment.NewLine);
            MyIni.Write("CRYSTAL", xtalCombo.Text, "PARAMETER");
            MyIni.Write("CAPIO", capioStr.Text, "PARAMETER");
            MyIni.Write("FREQUENCY", txFreqText.Text, "PARAMETER");
            MyIni.Write("DATARATE", txDRCombo.Text, "PARAMETER");
            MyIni.Write("DEVIATION", freqDev.Text, "PARAMETER");
            MyIni.Write("MODULATION", txModCombo.Text, "PARAMETER");
            MyIni.Write("PACONFIG", PAConfigCombo.Text, "PARAMETER");
            MyIni.Write("POWERLEVEL", pwrlvl.Text, "PARAMETER");
            File.AppendAllText(filePath, Environment.NewLine);
            MyIni.Write("CONTENT", preambleContent.Text, "PREAMBLE");
            MyIni.Write("LEN", preambleLen.Text, "PREAMBLE");
            File.AppendAllText(filePath, Environment.NewLine);
            MyIni.Write("MSB", (payloadMSB.Checked ? "true" : "false"), "PAYLOADORDER");
            File.AppendAllText(filePath, Environment.NewLine);
            string val = "";
            if (radioTransparent.Checked)
                val = "TRANSPARENT";
            else if (radioTransWithPresync.Checked)
                val = "TRANSPARENTwithPRESYNC";
            else if (radio802154.Checked)
                val = "802154";

            MyIni.Write("VALUE", val, "TXPAYLOADFORMAT");
            File.AppendAllText(filePath, Environment.NewLine);
            MyIni.Write("ENABLE", (pcrmuEnable.Checked ? "true" : "false"), "PCRMU");
            File.AppendAllText(filePath, Environment.NewLine);
            MyIni.Write("TXSYNCWORD", txSyncword.Text, "SYNCWORD");
            MyIni.Write("TXINVERT", (syncLSBCheckbox.Checked ? "true" : "false"), "SYNCWORD");
            MyIni.Write("RXSYNCWORD", rxSyncword.Text, "SYNCWORD");
            MyIni.Write("RXINVERT", (rxsyncInvertCheckbox.Checked ? "true" : "false"), "SYNCWORD");
            MyIni.Write("ALLOWBITERROR", allowBitError.Text, "SYNCWORD");
            File.AppendAllText(filePath, Environment.NewLine);
            for (int i = 0; i < intModeCheckedList.Items.Count; i++)
            {
                bool isChecked = intModeCheckedList.GetItemChecked(i);
                val = isChecked ? "true" : "false";
                MyIni.Write(intModeCheckedList.Items[i].ToString(), val, "INTERRUPT");
            }
            File.AppendAllText(filePath, Environment.NewLine);
            MyIni.Write("VALUE", txAemptyThr.Text, "TX_AEMPTY_THR");
            File.AppendAllText(filePath, Environment.NewLine);
            MyIni.Write("VALUE", rxFullThr.Text, "RX_AFULL_THR");
            File.AppendAllText(filePath, Environment.NewLine);
            MyIni.Write("ENABLE", (GPIO0_Enable.Checked ? "true" : "false"), "GPIO0");
            MyIni.Write("VALUE", gpio0_Combo.Text, "GPIO0");
            File.AppendAllText(filePath, Environment.NewLine);
            MyIni.Write("ENABLE", (GPIO1_Enable.Checked ? "true" : "false"), "GPIO1");
            MyIni.Write("VALUE", gpio1_Combo.Text, "GPIO1");
            File.AppendAllText(filePath, Environment.NewLine);
            int freqListNum = freqData.Count;
            MyIni.Write("NUM", freqListNum.ToString(), "FREQUENCYTABLE");
            if (freqListNum > 0)
            {
                for (int i = 0; i < freqListNum; i++)
                {
                    MyIni.Write("VALUE" + (i + 1), freqData[i].ToString(), "FREQUENCYTABLE");
                }
            }
        }

        // =============== 新增欄位的輸入驗證 (自 EzCodeGen 移植) ===============
        private void preambleContent_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (char.IsControl(e.KeyChar)) return;

            bool isHex = (e.KeyChar >= '0' && e.KeyChar <= '9') ||
                         (e.KeyChar >= 'a' && e.KeyChar <= 'f') ||
                         (e.KeyChar >= 'A' && e.KeyChar <= 'F');

            if (!isHex)
            {
                e.Handled = true;
                return;
            }
            string nextText = tb.Text.Remove(tb.SelectionStart, tb.SelectionLength)
                                     .Insert(tb.SelectionStart, e.KeyChar.ToString());

            if (nextText.Length > 2)
            {
                e.Handled = true;
                return;
            }
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void preambleContent_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.BackColor = Color.Yellow;
                tb.Focus();
                GenMsg.Show(this, "Please enter a value before leaving!", "WARNING");
                e.Cancel = true;
                return;
            }
            else
                tb.BackColor = SystemColors.Window;

            if (tb.Text.Length < 2)
            {
                GenMsg.Show(this, "Please enter 2 hex characters (1 byte).", "Format Error");
                tb.SelectAll();
                e.Cancel = true;
            }
        }

        private void preambleLen_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckInput.Int(sender, e, 1, 16);
        }

        private void preambleLen_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.BackColor = Color.Yellow;
                tb.Focus();
                GenMsg.Show(this, "Please enter a value before leaving!", "WARNING");
                tb.Text = "8";
                e.Cancel = true;
                return;
            }
            else
                tb.BackColor = SystemColors.Window;

            if (int.TryParse(tb.Text, out int val))
            {
                if (val < 1 || val > 16)
                {
                    GenMsg.Show(this, "Please enter a number between 1 and 16!", "RANGE ERROR");
                    tb.SelectAll();
                    e.Cancel = true;
                }
            }
        }

        private void rxSyncword_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (char.IsControl(e.KeyChar)) return;

            bool isHex = (e.KeyChar >= '0' && e.KeyChar <= '9') ||
                         (e.KeyChar >= 'a' && e.KeyChar <= 'f') ||
                         (e.KeyChar >= 'A' && e.KeyChar <= 'F');

            if (!isHex)
            {
                e.Handled = true;
                return;
            }
            string nextText = tb.Text.Remove(tb.SelectionStart, tb.SelectionLength)
                                     .Insert(tb.SelectionStart, e.KeyChar.ToString());

            if (nextText.Length > 8)
            {
                e.Handled = true;
                return;
            }
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void rxSyncword_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.BackColor = Color.Yellow;
                tb.Focus();
                GenMsg.Show(this, "Please enter a value before leaving!", "WARNING");
                e.Cancel = true;
                return;
            }
            else
                tb.BackColor = SystemColors.Window;

            if (tb.Text.Length < 8)
            {
                tb.BackColor = Color.Yellow;
                tb.Focus();
                GenMsg.Show(this, "Please enter 8 hex characters (4bytes).", "Format Error");
                tb.SelectAll();
                e.Cancel = true;
            }
            else
                tb.BackColor = SystemColors.Window;
        }

        private void allowBitError_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            if (char.IsControl(e.KeyChar)) return;

            string nextText = tb.Text.Remove(tb.SelectionStart, tb.SelectionLength)
                                     .Insert(tb.SelectionStart, e.KeyChar.ToString());

            if (int.TryParse(nextText, out int val))
            {
                if (val < 0 || val > 7 || nextText.Length > 1)
                {
                    e.Handled = true;
                }
            }
        }

        private void allowBitError_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.BackColor = Color.Yellow;
                tb.Focus();
                GenMsg.Show(this, "Please enter a value before leaving!", "WARNING");
                tb.Text = "0";
                e.Cancel = true;
                return;
            }
            else
                tb.BackColor = SystemColors.Window;

            if (int.TryParse(tb.Text, out int val))
            {
                if (val < 0 || val > 7)
                {
                    GenMsg.Show(this, "Please enter a number between 0 and 7!", "RANGE ERROR");
                    tb.SelectAll();
                    e.Cancel = true;
                }
            }
        }

        private void txAemptyThr_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            if (char.IsControl(e.KeyChar)) return;

            string nextText = tb.Text.Remove(tb.SelectionStart, tb.SelectionLength)
                                     .Insert(tb.SelectionStart, e.KeyChar.ToString());

            if (int.TryParse(nextText, out int val))
            {
                if (val > 128)
                {
                    e.Handled = true;
                    return;
                }
                if (nextText.Length > 1 && nextText.StartsWith("0"))
                {
                    e.Handled = true;
                }
            }
        }

        private void txAemptyThr_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.BackColor = Color.Yellow;
                tb.Focus();
                tb.Text = "1";
                GenMsg.Show(this, "Please enter a value before leaving.", "WARNING");
                e.Cancel = true;
                return;
            }
            else
                tb.BackColor = SystemColors.Window;

            if (int.TryParse(tb.Text, out int val))
            {
                if (val < 0 || val > 128)
                {
                    GenMsg.Show(this, "Please enter a number between 0 and 128.", "RANGE ERROR");
                    tb.SelectAll();
                    e.Cancel = true;
                }
            }
        }

        private void rxFullThr_KeyPress(object sender, KeyPressEventArgs e)
        {
            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                return;
            }
            if (char.IsControl(e.KeyChar)) return;

            string nextText = tb.Text.Remove(tb.SelectionStart, tb.SelectionLength)
                                     .Insert(tb.SelectionStart, e.KeyChar.ToString());

            if (int.TryParse(nextText, out int val))
            {
                if (val == 0)
                {
                    e.Handled = true;
                    return;
                }
                if (val > 128)
                {
                    e.Handled = true;
                }
            }
        }

        private void rxFullThr_Validating(object sender, CancelEventArgs e)
        {
            System.Windows.Forms.TextBox tb = sender as System.Windows.Forms.TextBox;

            if (string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.BackColor = Color.Yellow;
                tb.Focus();
                tb.Text = "1";
                GenMsg.Show(this, "Please enter a value before leaving.", "Input Required");
                e.Cancel = true;
                return;
            }
            else
                tb.BackColor = SystemColors.Window;

            if (int.TryParse(tb.Text, out int val))
            {
                if (val < 1 || val > 128)
                {
                    GenMsg.Show(this, "Please enter a number between 1 and 128.", "Range Error");
                    tb.SelectAll();
                    e.Cancel = true;
                }
            }
        }
    }
}
