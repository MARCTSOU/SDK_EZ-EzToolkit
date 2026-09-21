using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ER41xxTestTool
{
    /// <summary>
    /// 主視窗右側 "REG TABLE" 群組 (regGroup) 的 REGISTER DUMP 功能:
    /// 送出韌體新增的 "rdump" 命令讀回實機值, 以三欄 (REG_NAME / ADDR / VALUE) 列出
    /// RegTableItems (目前 75 筆, 排除 RF_REG_INIT_ARRAY 的 register)。
    /// 值以 MSB-first 顯示, 例如 32-bit 值 0x12345678 顯示成 "12 34 56 78"。
    /// </summary>
    public partial class MainForm : Form
    {
        private ListView regDumpView;
        //private Button btnRegDump;
        //private Button btnRegTableWrite;
        //private Label lblRegDumpStatus;
        private Button btnCategoryFilter;
        private CheckedListBox categoryFilterCheckedList;
        private ToolStripDropDown categoryFilterDropDown;

        /* DVB否需更新設定的判斷資源 */
        // 自動重算的兩個旗標: 開機初始化完成前不要動, 以及防止事件重入。
        private bool bRegAutoRefreshReady = false;
        private bool bRegRefreshing = false;
        // PARAMETER 區的值改了、但還沒按 Write DVB Register 寫進板子 —— 用來在
        // dvbTstPktTxStartBtn/dvbTstContiTxStartBtn/dvbTstPktRxScanBtn/dvbTstPktRxStartBtn/dvbTstStateSetBtn 按下前提醒使用者。
        //private bool bRegTableDirty = false;
        // Dict to sotre current Register table, 用來比對是否被更動
        private readonly Dictionary<ushort, uint> lastWrittenRegSnapshot = new Dictionary<ushort, uint>();

        /* Register filter and Search */
        private Button btnDependsOnFilter;
        private CheckedListBox dependsOnFilterCheckedList;
        private ToolStripDropDown dependsOnFilterDropDown;
        private Label lblSearchHint;
        private TextBox txtSearchName;
        private Button btnSearchName;

        private const string CategoryFilterAllLabel = "ALL";
        private const string NoDependsOnLabel = "None Freq & Modem";   // DependsOn 欄位空字串 (跟 Freq/DataRate/Dev 都無關) 的register, 在 DependsOn filter 裡歸這一項
        private bool bUpdatingCategoryFilter = false;   // 避免 ALL <-> 個別分類互相同步時的 ItemCheck 重入
        private bool bUpdatingDependsOnFilter = false;      // 同上, 但是給 DependsOn 那個獨立的 CheckedListBox 用

        /// <summary>REG TABLE 的一列顯示資料</summary>
        private struct RegRow
        {
            public string Name;
            public ushort Addr;
            public string ValueText;

            public string Category;
            public uint? RawValue;   // null = 值未知 (rdump 沒讀到), 這種列不給展開/編輯

            public string DependsOn { get; internal set; }
            public string Descp { get; internal set; }
        }

        /// <summary>頂層 register 列的 Tag, 用來記錄展開狀態與目前的完整 32-bit 值</summary>
        private class RegItemTag
        {
            public string RegName;
            public ushort Addr;
            public uint? RawValue;
            public string DependsOn;
            public string Descp;
            public bool Expanded;
        }

        /// <summary>展開後 bit field 子列的 Tag</summary>
        private class FieldItemTag
        {
            public string RegName;
            public RegBitField Field;
        }

        // 展開/收合圖示 (刻意用 ASCII 而非 Unicode 三角形, 避免 Consolas 字型沒有對應字形顯示成方框)
        private const string ExpandMarkerCollapsed = "[+] ";
        private const string ExpandMarkerExpanded = "[-] ";
        private const string FieldRowIndent = "   ";

        // channel 0 PLL calibration (SYNTH_CAL_CONFIG_5/6/7/4): 只有 Xtal=38.4MHz 時 GenerateTxt()
        // 才會呼叫 FREQSET.SetFrequency() 去算這4顆, 40MHz 時維持 Initial_Reg_Array 預設值不會被覆寫
        // (見 CodeGenProcess.cs 的 "if (xtalValue == 38.4)" 判斷式), REG TABLE 上用灰字提示不用填。
        private static readonly HashSet<ushort> Xtal384OnlySynthAddrs = new HashSet<ushort> { 0x1998, 0x199C, 0x19A0, 0x1994 };

        /*-------------------------------------------------------------
         *  IsRegisterGreyedOut
         * 依目前PARAMETER分頁的設定，判斷某顆register現在用不用得到(用不到就灰字提示)。
         * 除了既有的Xtal=40MHz Synthesizer 4顆之外，這裡也涵蓋只在特定DataRate/H條件下
         * 才會被GenerateTxt()寫入的幾顆RX微調register，以及只在啟用TX_AEMPTY/RX_FULL
         * 中斷時才會寫的FIFO threshold——判斷條件對照CodeGenProcess.cs的
         * RxGenerate()/RXDRxxx.Set()/GenerateTxt()。
         * Param :
         *   addr - 要判斷的register位址
         * Return:
         *   true - 目前設定下這顆reg用不到(REG TABLE要灰字提示) / false - 有用到
         *-------------------------------------------------------------*/
        private bool IsRegisterGreyedOut(ushort addr)
        {
            try
            {
                if (paramXtalCombo.Text.Trim() == "40" && Xtal384OnlySynthAddrs.Contains(addr))
                    return true;

                if (addr == 0xA0A4)
                    return !(paramIntModeTxAemptyGroup.Visible || paramIntModeRxAfullGroup.Visible);

                if (addr == 0xB0D0 || addr == 0xB0F0 || addr == 0xB108 || addr == 0xB10C || addr == 0xB110 ||
                    addr == 0xB114 || addr == 0xB118 || addr == 0xB11C)
                {
                    double xtalValue = Convert.ToDouble(paramXtalCombo.Text);
                    double drTextValue = double.Parse(paramDataRateCombo.Text);
                    double freqDevDouble = Convert.ToDouble(paramDeviationText.Text);
                    uint drValue = (uint)(1000 * drTextValue);
                    uint devValue = (uint)(1000 * freqDevDouble);
                    double dataRateOffset, H;
                    ComputeRxDispatchValues(drValue, devValue, xtalValue, out dataRateOffset, out H);
                    RxRateBucket bucket = GetRxRateBucket(dataRateOffset);

                    if (addr == 0xB0F0)
                        return !(bucket == RxRateBucket.R1M || bucket == RxRateBucket.R2M);
                    if (addr == 0xB114 || addr == 0xB118 || addr == 0xB11C)
                        return bucket != RxRateBucket.R2M;
                    if (addr == 0xB108 || addr == 0xB10C || addr == 0xB110)
                        return bucket == RxRateBucket.R2M;
                    // 0xB0D0: 只有非 500K/1M/2M 且 h>=2 才會寫
                    return bucket == RxRateBucket.R500K || bucket == RxRateBucket.R1M || bucket == RxRateBucket.R2M || H < 2;
                }

                return false;
            }
            catch
            {
                // PARAMETER 欄位暫時空白/格式不對(例如打字打到一半)時不影響顯示, 比照現有 Xtal 純字串比對永遠不丟例外的寬鬆原則
                return false;
            }
        }

        // 展開中的 bit field 子列正在編輯的 overlay TextBox (同時最多一個)
        private TextBox regEditBox;

        // RegName -> 該 register 目前的狀態 (RawValue/Expanded), 是展開/收合與編輯的唯一資料來源;
        // regDumpView.Items 只是依這份資料重畫出來的畫面, 詳見 RebuildAllItems()。
        private Dictionary<string, RegItemTag> regItemsByName = new Dictionary<string, RegItemTag>();
        private string lastRenderTooltip = "";

        // 分類標題 (RegCategories.CategoryOrder) -> ListViewGroup, 開機時建一次, 之後重畫清單直接重用。
        private Dictionary<string, ListViewGroup> regCategoryGroups;

        // PacketRXTest 的 UartReceiveDataByte 是否掛在 serialPort1 上;
        // dump 期間要先拆掉才不會被搶走資料, 結束後再依此旗標還原。
        private bool bUartRxHandlerOn = false;

        private const string RDUMP_BEGIN = "RDUMP_BEGIN";
        private const string RDUMP_END = "RDUMP_END";

        // 開機初始化 REG TABLE 期間把 ShowInsLog 靜音。
        // GenerateTxt() 內部會印 "Generate at ..." —— 那行對按下 GENERATE 的使用者有意義,
        // 但開機時只是雜訊, 所以用旗標擋掉而不是刪掉那行。
        private bool bQuietLog = false;

        // 用來確認ViewCtrl是不是已經自動適應字串調整欄寬，只要第一次繪圖時調一次就好
        private bool bColumnsAutoSized = false;

        /*-------------------------------------------------------------
         *  AttachUartRxHandler
         * 掛上Packet RX的位元組接收處理常式(集中在此以便追蹤掛載狀態)。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void AttachUartRxHandler()
        {
            serialPort1.DataReceived -= UartReceiveDataByte;
            serialPort1.DataReceived += UartReceiveDataByte;
            bUartRxHandlerOn = true;
        }

        /*-------------------------------------------------------------
         *  DetachUartRxHandler
         * 拆掉Packet RX的位元組接收處理常式。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void DetachUartRxHandler()
        {
            serialPort1.DataReceived -= UartReceiveDataByte;
            bUartRxHandlerOn = false;
        }

        /*-------------------------------------------------------------
         *  InitRegDumpUI
         * [物件控制] - 建立btnCategoryFilter/btnDependsOnFilter/txtSearchName/btnSearchName/
         * regDumpView等REG TABLE分頁的所有控制項並加入regTableTopicGroup，開機時呼叫一次。
         * 建完後呼叫ShowRegisterDefaults()先列出.h的預設值。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void InitRegDumpUI()
        {
            // regGroup ("REG TABLE") 固定在主視窗右側, 寬度已加大到 320 以容納三欄
            // (REG_NAME/ADDR/VALUE); 主視窗寬度也隨之縮到 regGroup 右邊緣, 詳見 MainForm.Designer.cs。

            int inner = regTableTopicGroup.ClientSize.Width;      // 約 314

            ///* ----- 生成DVB讀按鈕，但先將之隱藏，由main form的按鈕驅動 ----------*/
            //btnRegDumpr = new Button();
            //btnRegDump.Text = "Read DVB";
            //btnRegDump.Font = new Font("Microsoft Sans Serif", 8F);
            ////btnRegDump.Location = new Point(8, 20);
            ////btnRegDump.Size = new Size(80, 24);
            //btnRegDump.Location = new Point(0, 0);
            //btnRegDump.Size = new Size(0, 0);
            //btnRegDump.Click += btnRegDump_Click;
            //btnRegDump.Visible = false;
            //EnhanceButton(btnRegDump);

            ///* ----- 生成DVB寫按鈕，但將之隱藏，由main form的按鈕驅動 ---------*/
            //btnRegTableWrite = new Button();
            //btnRegTableWrite.Text = "Wrtie DVB";
            //btnRegTableWrite.Font = new Font("Microsoft Sans Serif", 8F);
            ////btnRegTableWrite.Location = new Point(100, 20);
            ////btnRegTableWrite.Size = new Size(80, 24);
            //btnRegTableWrite.Location = new Point(0, 0);
            //btnRegTableWrite.Size = new Size(0, 0);
            //btnRegTableWrite.Click += btnRegTableWrite_Click;
            //btnRegTableWrite.Visible = false;
            //EnhanceButton(btnRegTableWrite);

            /* ----- 生成Register table 標籤以顯示數量，但將之隱藏 ----------*/
            //lblRegDumpStatus = new Label();
            //lblRegDumpStatus.AutoSize = false;
            ////lblRegDumpStatus.Location = new Point(190, 20);
            ////lblRegDumpStatus.Size = new Size(inner - 152, 16);
            //lblRegDumpStatus.Location = new Point(0, 0);
            //lblRegDumpStatus.Size = new Size(inner - 152, 16);
            //lblRegDumpStatus.Font = new Font("Microsoft Sans Serif", 8F);
            //lblRegDumpStatus.ForeColor = Color.Black;
            //lblRegDumpStatus.TextAlign = ContentAlignment.MiddleLeft;
            //lblRegDumpStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            //lblRegDumpStatus.Visible = false;
            //lblRegDumpStatus.Text = "";

            /* ===== 生成Register table 過瀘器, 搜尋器 =====================*/
            // 兩個過濾按鈕 (Topic/DependsOn, 外觀都像 ComboBox) + 依名稱/位址搜尋, 放在原 READ/WRITE
            // 按鈕列的位置 (READ/WRITE 已收起改放到左下角分頁)。Topic/DependsOn 同一列, 搜尋框另起一列,
            // 三列的高度都是 filterRowH, y=20 剛好貼齊、不會擋到 GroupBox 標題文字。
            const int filterRowY = 20;
            const int filterRowH = 28;
            const int gap = 6;
            const int topicBtnW = 150;
            const int dependsOnBtnW = 150;
            const int searchBtnW = 80;
            const int searchLabelW = 130;
            int rowLeft = 6;
            int rowRight = inner - 6;   // 對齊 regDumpView 右緣
            int searchRowY = filterRowY + filterRowH + gap;
            int searchTxtW = (rowRight - rowLeft) - searchLabelW - searchBtnW - gap * 2;

            /* ----- 生成Register table Topic過瀘器 ------------------------*/
            btnCategoryFilter = new Button();
            btnCategoryFilter.Text = "Topic ▼";
            btnCategoryFilter.Font = new Font("Microsoft Sans Serif", 8f);
            btnCategoryFilter.ForeColor = Color.Black;
            btnCategoryFilter.Location = new Point(rowLeft, filterRowY);
            btnCategoryFilter.Size = new Size(topicBtnW, filterRowH);
            btnCategoryFilter.BackColor = Color.White;
            btnCategoryFilter.Click += BtnCategoryFilter_Click;
            //EnhanceButton(btnCategoryFilter);

            categoryFilterCheckedList = new CheckedListBox();
            categoryFilterCheckedList.Font = new Font("Microsoft Sans Serif", 8f);
            categoryFilterCheckedList.CheckOnClick = true;
            categoryFilterCheckedList.BorderStyle = BorderStyle.None;
            categoryFilterCheckedList.Width = 180;
            categoryFilterCheckedList.Height = 200;
            categoryFilterCheckedList.Items.Add(CategoryFilterAllLabel, true);   // index 0 固定是 ALL, 用來一次全勾/全消
            foreach (string cat in RegCategories.CategoryOrder)
                categoryFilterCheckedList.Items.Add(cat, true);   // 預設全選 = 不過濾
            categoryFilterCheckedList.ItemCheck += CategoryFilterCheckedList_ItemCheck;

            ToolStripControlHost categoryFilterHost = new ToolStripControlHost(categoryFilterCheckedList);
            categoryFilterHost.Margin = Padding.Empty;
            categoryFilterHost.Padding = Padding.Empty;
            categoryFilterHost.AutoSize = false;
            categoryFilterHost.Size = categoryFilterCheckedList.Size;

            categoryFilterDropDown = new ToolStripDropDown();
            categoryFilterDropDown.Padding = Padding.Empty;
            categoryFilterDropDown.Items.Add(categoryFilterHost);
            /* ----- 生成Register table DependsOn(哪個畫面參數會影響這顆 reg) -----*/
            btnDependsOnFilter = new Button();
            btnDependsOnFilter.Text = "Depends On ▼";
            btnDependsOnFilter.Font = new Font("Microsoft Sans Serif", 8f);
            btnDependsOnFilter.ForeColor = Color.Black;
            btnDependsOnFilter.Location = new Point(rowLeft + topicBtnW + gap, filterRowY);
            btnDependsOnFilter.Size = new Size(dependsOnBtnW, filterRowH);
            btnDependsOnFilter.BackColor = Color.White;
            btnDependsOnFilter.Click += BtnDependsOnFilter_Click;
            //EnhanceButton(btnDependsOnFilter);

            dependsOnFilterCheckedList = new CheckedListBox();
            dependsOnFilterCheckedList.Font = new Font("Microsoft Sans Serif", 8f);
            dependsOnFilterCheckedList.CheckOnClick = true;
            dependsOnFilterCheckedList.BorderStyle = BorderStyle.None;
            dependsOnFilterCheckedList.Width = 180;
            dependsOnFilterCheckedList.Height = 200;
            dependsOnFilterCheckedList.Items.Add(CategoryFilterAllLabel, true);   // index 0 固定是 ALL
            HashSet<string> seenDependsOn = new HashSet<string>();
            foreach (RegTableItem it in RegTableItems.Items)
            {
                // DependsOn 可能是 "Freq/DataRate/Dev" 這種組合標籤 (一顆 reg 同時被多個參數影響),
                // 拆成原子標籤各自列一個勾選項, 這樣勾選單一個 "Freq" 就能連組合標籤的 reg 一起找出來
                // (見 PassesFilter), 不用使用者自己去想「還要多勾哪個組合字串」。
                if (string.IsNullOrEmpty(it.DependsOn))
                {
                    if (seenDependsOn.Add(NoDependsOnLabel))
                        dependsOnFilterCheckedList.Items.Add(NoDependsOnLabel, true);
                    continue;
                }

                foreach (string token in it.DependsOn.Split('/'))
                {
                    string t = token.Trim();
                    if (t.Length > 0 && seenDependsOn.Add(t))
                        dependsOnFilterCheckedList.Items.Add(t, true);   // 預設全選 = 不過濾
                }
            }
            dependsOnFilterCheckedList.ItemCheck += DependsOnFilterCheckedList_ItemCheck;

            ToolStripControlHost dependsOnFilterHost = new ToolStripControlHost(dependsOnFilterCheckedList);
            dependsOnFilterHost.Margin = Padding.Empty;
            dependsOnFilterHost.Padding = Padding.Empty;
            dependsOnFilterHost.AutoSize = false;
            dependsOnFilterHost.Size = dependsOnFilterCheckedList.Size;

            dependsOnFilterDropDown = new ToolStripDropDown();
            dependsOnFilterDropDown.Padding = Padding.Empty;
            dependsOnFilterDropDown.Items.Add(dependsOnFilterHost);

            /* ----- 生成Register table 搜尋功能 ------------------------------*/
            // Register的搜尋鈕 : 搜名字及位址, 另起一列放在 Topic/DependsOn 下方
            lblSearchHint = new Label();
            lblSearchHint.Text = "RegName or Addr";
            lblSearchHint.Font = new Font("Microsoft Sans Serif", 8f);
            lblSearchHint.ForeColor = Color.Black;
            lblSearchHint.AutoSize = false;
            lblSearchHint.TextAlign = ContentAlignment.MiddleLeft;
            lblSearchHint.Location = new Point(rowLeft, searchRowY);
            lblSearchHint.Size = new Size(searchLabelW, filterRowH);

            txtSearchName = new TextBox();
            txtSearchName.Font = new Font("Microsoft Sans Serif", 8f);
            txtSearchName.Location = new Point(rowLeft + searchLabelW + gap, searchRowY + 3);
            txtSearchName.Size = new Size(searchTxtW, filterRowH);
            txtSearchName.KeyDown += TxtSearchName_KeyDown;
            txtSearchName.Text = "1F00";    //give a search example

            btnSearchName = new Button();
            btnSearchName.Text = "Search";
            btnSearchName.Font = new Font("Microsoft Sans Serif", 8f);
            btnSearchName.ForeColor = Color.Black;
            btnSearchName.Location = new Point(rowRight - searchBtnW, searchRowY);
            btnSearchName.Size = new Size(searchBtnW, filterRowH);
            btnSearchName.BackColor = Color.White;
            btnSearchName.Click += BtnSearchName_Click;
            //EnhanceButton(btnSearchName);

            /* ----- 生成Register table本體 -----------------------------*/
            regDumpView = new ListView();
            regDumpView.Location = new Point(6, searchRowY + filterRowH + gap);
            regDumpView.Size = new Size(inner - 12, regTableTopicGroup.ClientSize.Height - (searchRowY + filterRowH + gap) - 6);
            regDumpView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            regDumpView.View = System.Windows.Forms.View.Details;
            regDumpView.FullRowSelect = true;
            regDumpView.GridLines = true;
            regDumpView.MultiSelect = true;
            regDumpView.HideSelection = false;
            regDumpView.Font = new Font("Consolas", 8F);
            // ADDR 縮窄 (子列不顯示位址, 頂層列只需容納 "0x1F00" 這種 6 字元), 空間挪給 REG_NAME
            // (展開後子列要放 "[bit範圍] 欄位名", 比純 register 名稱長不少) 和 VALUE (要完整顯示
            // "03 02 01 01" 這種 11 字元, 不能被裁到)。
            regDumpView.Columns.Add("REG_NAME", 130, HorizontalAlignment.Left);
            regDumpView.Columns.Add("ADDR", 55, HorizontalAlignment.Left);
            regDumpView.Columns.Add("VAL[B0:B3]", 105, HorizontalAlignment.Left);
            regDumpView.Columns.Add("DEPENDS ON", 130, HorizontalAlignment.Left);
            regDumpView.MouseClick += regDumpView_MouseClick;


            // 分類標題列 (PA SETTING / TX SETTING / RX SETTING / Synthesizer...),
            // 依 RegCategories.CategoryOrder 的順序建立, 之後每次重畫清單重用同一批 ListViewGroup。
            regCategoryGroups = new Dictionary<string, ListViewGroup>();
            foreach (string cat in RegCategories.CategoryOrder)
            {
                ListViewGroup grp = new ListViewGroup(cat, cat);
                regCategoryGroups[cat] = grp;
                regDumpView.Groups.Add(grp);
            }

            //regTableTopicGroup.Controls.Add(btnRegDump);
            //regTableTopicGroup.Controls.Add(btnRegTableWrite);
            //regTableTopicGroup.Controls.Add(lblRegDumpStatus);
            regTableTopicGroup.Controls.Add(btnCategoryFilter);
            regTableTopicGroup.Controls.Add(btnDependsOnFilter);
            regTableTopicGroup.Controls.Add(lblSearchHint);
            regTableTopicGroup.Controls.Add(txtSearchName);
            regTableTopicGroup.Controls.Add(btnSearchName);
            regTableTopicGroup.Controls.Add(regDumpView);

            // 開啟程式時先列出 .h 的預設值; 按下 READ 後會被實機讀回的值取代
            ShowRegisterDefaults();
        }

        /*-------------------------------------------------------------
         *  BtnCategoryFilter_Click
         * [物件控制] - 開關categoryFilterDropDown
         * Topic過濾按鈕: 已顯示就收起下拉選單, 否則在按鈕下方展開。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void BtnCategoryFilter_Click(object sender, EventArgs e)
        {
            if (categoryFilterDropDown.Visible)
                categoryFilterDropDown.Close();
            else
                categoryFilterDropDown.Show(btnCategoryFilter, new Point(0, btnCategoryFilter.Height));
        }

        /*-------------------------------------------------------------
         *  BtnDependsOnFilter_Click
         * [物件控制] - 開關dependsOnFilterDropDown
         * DependsOn過濾按鈕: 已顯示就收起下拉選單, 否則在按鈕下方展開。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void BtnDependsOnFilter_Click(object sender, EventArgs e)
        {
            if (dependsOnFilterDropDown.Visible)
                dependsOnFilterDropDown.Close();
            else
                dependsOnFilterDropDown.Show(btnDependsOnFilter, new Point(0, btnDependsOnFilter.Height));
        }

        /*-------------------------------------------------------------
         *  SyncAllToggle
         * 給Topic/DependsOn兩個CheckedListBox共用: index 0是"ALL", 點它會把其餘項目一次設成
         * 同一個狀態(全勾/全消); 點任一個別項目則事後檢查其餘項目是不是全部都勾了, 藉此同步
         * ALL的勾選狀態。呼叫端要用各自的bUpdating*Filter旗標包住, 避免這裡呼叫SetItemChecked()
         * 觸發的巢狀ItemCheck又跑一次這段邏輯。
         * Param :
         *   list - 要同步的CheckedListBox (categoryFilterCheckedList或dependsOnFilterCheckedList)
         *   e - 觸發的ItemCheck事件參數 (取其Index/NewValue)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private static void SyncAllToggle(CheckedListBox list, ItemCheckEventArgs e)
        {
            bool newChecked = e.NewValue == CheckState.Checked;
            if (e.Index == 0)
            {
                for (int i = 1; i < list.Items.Count; i++)
                    list.SetItemChecked(i, newChecked);
            }
            else
            {
                bool allChecked = true;
                for (int i = 1; i < list.Items.Count; i++)
                {
                    bool checkedNow = (i == e.Index) ? newChecked : list.GetItemChecked(i);
                    if (!checkedNow)
                    {
                        allChecked = false;
                        break;
                    }
                }
                list.SetItemChecked(0, allChecked);
            }
        }

        /*-------------------------------------------------------------
         *  CategoryFilterCheckedList_ItemCheck
         * [物件控制] - 刷新regDumpView (依categoryFilterCheckedList新的勾選狀態重畫)
         * Topic勾選清單有勾選變動時, 先同步ALL項目狀態(SyncAllToggle), 再排到訊息迴圈後面
         * 重畫REG TABLE (ItemCheck觸發當下勾選狀態還沒真的套用, 不能立刻重畫)。
         * Param :
         *   sender, e - ItemCheck事件參數
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void CategoryFilterCheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!bUpdatingCategoryFilter)
            {
                bUpdatingCategoryFilter = true;
                try { SyncAllToggle(categoryFilterCheckedList, e); }
                finally { bUpdatingCategoryFilter = false; }
            }

            // ItemCheck 觸發時勾選狀態還沒真的套用, 排到訊息迴圈後面再重畫
            BeginInvoke((MethodInvoker)delegate { RebuildAllItems(); });
        }

        /*-------------------------------------------------------------
         *  DependsOnFilterCheckedList_ItemCheck
         * [物件控制] - 刷新regDumpView (依dependsOnFilterCheckedList新的勾選狀態重畫)
         * DependsOn勾選清單有勾選變動時, 同CategoryFilterCheckedList_ItemCheck的處理方式。
         * Param :
         *   sender, e - ItemCheck事件參數
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void DependsOnFilterCheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!bUpdatingDependsOnFilter)
            {
                bUpdatingDependsOnFilter = true;
                try { SyncAllToggle(dependsOnFilterCheckedList, e); }
                finally { bUpdatingDependsOnFilter = false; }
            }

            BeginInvoke((MethodInvoker)delegate { RebuildAllItems(); });
        }

        /*-------------------------------------------------------------
         *  BtnSearchName_Click
         * Search按鈕: 以txtSearchName目前輸入的文字執行搜尋並跳轉。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void BtnSearchName_Click(object sender, EventArgs e)
        {
            SearchAndJump(txtSearchName.Text.Trim());
        }

        /*-------------------------------------------------------------
         *  TxtSearchName_KeyDown
         * 在txtSearchName裡按Enter等同按一次Search按鈕 (並吃掉Enter, 避免觸發其他預設行為)。
         * Param :
         *   sender - 未使用
         *   e - KeyDown事件參數, 取其KeyCode判斷是否為Enter
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void TxtSearchName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SearchAndJump(txtSearchName.Text.Trim());
            }
        }

        // "Search Next" 狀態: 文字不變時, 每次呼叫依序跳下一筆命中結果 (循環到底再繞回第一筆);
        // 文字一變就視為新搜尋, 從第一筆命中重新開始。
        private string lastSearchText;
        private List<RegTableItem> lastSearchMatches;
        private int lastSearchMatchIndex = -1;

        /*-------------------------------------------------------------
         *  SearchAndJump
         * 依輸入文字找到register後捲動過去並自動展開(若有bit field可展開)。
         * 輸入可以是位址(例如"0x13AC"/"13AC", 精確比對)或REG_NAME的一部分(不分大小寫)。
         * 同一段文字重複呼叫(按鈕連點或Enter)會依序跳到下一筆命中結果, 循環到底繞回第一筆
         * ——即"Search Next"。若命中的分類目前被FILTER隱藏, 會自動連同該分類一併勾選顯示,
         * 確保一定找得到。
         * Param :
         *   text - 搜尋文字 (位址或REG_NAME/部分名稱)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void SearchAndJump(string text)
        {
            if (string.IsNullOrEmpty(text))
                return;

            if (lastSearchMatches == null || !string.Equals(lastSearchText, text, StringComparison.OrdinalIgnoreCase))
            {
                lastSearchText = text;
                lastSearchMatches = BuildSearchMatches(text);
                lastSearchMatchIndex = -1;
            }

            if (lastSearchMatches.Count == 0)
            {
                GenMsg.Show("No register matches \"" + text + "\".", "REG TABLE");
                lastSearchMatches = null;   // 讓下次同樣文字重新搜一次, 而不是一直卡在"沒有結果"的舊狀態
                return;
            }

            lastSearchMatchIndex = (lastSearchMatchIndex + 1) % lastSearchMatches.Count;
            JumpToRegister(lastSearchMatches[lastSearchMatchIndex]);
        }

        /*-------------------------------------------------------------
         *  BuildSearchMatches
         * text先當成16進位位址(可帶"0x"前綴)精確比對Addr; 沒有位址命中就退回以REG_NAME比對
         * (完全相符排最前面, 其餘依原順序排在後面)。
         * Param :
         *   text - 搜尋文字
         * Return:
         *   符合的RegTableItem清單, 依上述優先順序排列; 完全沒命中則為空清單
         *-------------------------------------------------------------*/
        private List<RegTableItem> BuildSearchMatches(string text)
        {
            string hex = text.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? text.Substring(2) : text;
            ushort addr;
            if (IsHex(hex) && TryParseHex16(hex, out addr))
            {
                List<RegTableItem> byAddr = new List<RegTableItem>();
                foreach (RegTableItem it in RegTableItems.Items)
                    if (it.Addr == addr)
                        byAddr.Add(it);
                if (byAddr.Count > 0)
                    return byAddr;
            }

            List<RegTableItem> exact = new List<RegTableItem>();
            List<RegTableItem> contains = new List<RegTableItem>();
            foreach (RegTableItem it in RegTableItems.Items)
            {
                if (string.Equals(it.Name, text, StringComparison.OrdinalIgnoreCase))
                    exact.Add(it);
                else if (it.Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
                    contains.Add(it);
            }
            exact.AddRange(contains);
            return exact;
        }

        /*-------------------------------------------------------------
         *  JumpToRegister
         * [物件控制] - 刷新regDumpView (捲動/選取/展開指定register列)
         * 把指定register捲動到可視範圍最上緣、選取, 若有bit field就順便展開; 分類被FILTER
         * 隱藏的話先自動勾選。
         * Param :
         *   match - 要跳轉過去的RegTableItem
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void JumpToRegister(RegTableItem match)
        {
            if (categoryFilterCheckedList != null)
            {
                string cat = RegCategories.Get(match.Addr);
                int idx = categoryFilterCheckedList.Items.IndexOf(cat);
                if (idx >= 0 && !categoryFilterCheckedList.GetItemChecked(idx))
                    categoryFilterCheckedList.SetItemChecked(idx, true);
            }

            RegItemTag tag;
            if (regItemsByName.TryGetValue(match.Name, out tag) && RegBitFields.ByRegName.ContainsKey(tag.RegName))
                tag.Expanded = true;

            RebuildAllItems();

            ListViewItem item = FindRegisterItem(match.Name);
            if (item != null)
            {
                item.Selected = true;
                // EnsureVisible 只會做「最小捲動」讓項目剛好進入可視範圍(可能停在最底邊)。
                // 先把最後一列捲到可視範圍逼整份清單捲到底, 此時目標列必定在可視範圍「上方」,
                // 再對它呼叫 EnsureVisible 就會被捲到可視範圍最上緣, 效果等同置頂 (RebuildAllItems 同一招)。
                regDumpView.Items[regDumpView.Items.Count - 1].EnsureVisible();
                item.EnsureVisible();
                regDumpView.Focus();
            }
        }

        /*-------------------------------------------------------------
         *  PassesFilter
         * 目前的分類勾選是否放行這筆register(只影響RebuildAllItems要不要畫出來, 不影響
         * regItemsByName的資料)。
         * Param :
         *   it - 要判斷的RegTableItem
         * Return:
         *   true - Topic/DependsOn目前的勾選都放行 / false - 被過濾掉, 不顯示
         *-------------------------------------------------------------*/
        private bool PassesFilter(RegTableItem it)
        {
            if (categoryFilterCheckedList != null && !categoryFilterCheckedList.CheckedItems.Contains(RegCategories.Get(it.Addr)))
                return false;

            // DependsOn 用「拆字後 OR 比對」而非整串比對: 勾了 "Freq" 就要連 DependsOn="Freq/DataRate/Dev"
            // 這種組合標籤的 reg 也一起放行, 只要組合裡任一個原子標籤有被勾就算通過。
            // DependsOn 是空字串的 (跟 Freq/DataRate/Dev 都無關) 則看 NoDependsOnLabel 這個項目有沒有被勾。
            if (dependsOnFilterCheckedList != null)
            {
                if (string.IsNullOrEmpty(it.DependsOn))
                {
                    if (!dependsOnFilterCheckedList.CheckedItems.Contains(NoDependsOnLabel))
                        return false;
                }
                else
                {
                    bool anyChecked = false;
                    foreach (string token in it.DependsOn.Split('/'))
                    {
                        string t = token.Trim();
                        if (t.Length > 0 && dependsOnFilterCheckedList.CheckedItems.Contains(t))
                        {
                            anyChecked = true;
                            break;
                        }
                    }
                    if (!anyChecked)
                        return false;
                }
            }
            return true;
        }

        /*-------------------------------------------------------------
         *  GetCurrentRegTableValue
         * REG TABLE目前顯示的值(含手動bit field編輯/DVB讀回/PARAMETER重算, 以最後一次為準);
         * 沒有值時退回編譯進exe的預設值。供Generate讀取當資料來源用。
         * Param :
         *   it - 要取值的RegTableItem
         * Return:
         *   目前應該使用的32-bit register值
         *-------------------------------------------------------------*/
        private uint GetCurrentRegTableValue(RegTableItem it)
        {
            RegItemTag tag;
            return (regItemsByName.TryGetValue(it.Name, out tag) && tag.RawValue.HasValue) ? tag.RawValue.Value : it.Value;
        }

        /*-------------------------------------------------------------
         *  RenderRegisterRows
         * [物件控制] - 刷新regDumpView
         * 依RegTableItems的順序(= reg_list.txt的功能分組順序)更新整份清單的值。
         * regItemsByName才是狀態的唯一來源(RawValue/Expanded); 這裡只更新RawValue, 再視情況
         * 決定要「就地改文字」(保留scrollbar位置與展開狀態)還是「整份重畫」(第一次建立清單時)。
         * 展開中的列也會一併重算bit field子列的值, 不會因為READ/參數變動而被收合。
         * Param :
         *   rows - 要顯示的整份RegRow清單 (依RegTableItems順序)
         *   tooltip - 這批資料的來源說明 (存到lastRenderTooltip, 目前僅供除錯追蹤用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void RenderRegisterRows(List<RegRow> rows, string tooltip)
        {
            EndEditField(false);   // 重畫前先關掉可能開著的 bit field 編輯框, 避免它指到失效的列
            lastRenderTooltip = tooltip;

            foreach (RegRow row in rows)
            {
                RegItemTag tag;
                if (!regItemsByName.TryGetValue(row.Name, out tag))
                {
                    tag = new RegItemTag { 
                        RegName = row.Name, 
                        Addr = row.Addr,
                        DependsOn = row.DependsOn,
                        Descp = row.Descp, 
                        Expanded = false };
                    regItemsByName[row.Name] = tag;
                }
                tag.RawValue = row.RawValue;
            }

            regDumpView.Columns[0].Text = "REG_NAME(" + rows.Count + ")";

            if (regDumpView.Items.Count == 0)
                RebuildAllItems();
            else
                UpdateItemTextsInPlace();
        }

        /*-------------------------------------------------------------
         *  RebuildAllItems
         * [物件控制] - 刷新regDumpView
         * 清空regDumpView並依RegTableItems順序整份重建, 展開中的register會把bit field子列
         * 緊接著加在自己下面。ListView有Groups時, Items.Insert(index, ...)並不會決定組內視覺
         * 順序(實測子列一律被排到該分類的最後面), 所以子列一定要用「整份依最終順序重新Add」
         * 來排版, 不能用插入既有清單中間的方式——這是這個方法存在的唯一理由。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void RebuildAllItems()
        {
            string topKey = GetItemKey(GetTopVisibleItem());

            regDumpView.BeginUpdate();
            regDumpView.Items.Clear();
            ListViewItem topItemAfter = null;
            int topLevelCount = 0;   // 只算頂層 register 列, 不含展開後的 bit field 子列 (FieldItemTag)
            foreach (RegTableItem it in RegTableItems.Items)
            {
                if (!PassesFilter(it))
                    continue;   // 分類未勾選或不符搜尋文字: 這筆(含展開中的 bit field 子列)不畫出來, 但 regItemsByName 的資料不受影響

                RegItemTag tag;
                if (!regItemsByName.TryGetValue(it.Name, out tag))
                {
                    tag = new RegItemTag { 
                        RegName = it.Name, 
                        Addr = it.Addr, 
                        RawValue = it.Value, 
                        DependsOn = it.DependsOn,
                        Descp = it.Descption, 
                        Expanded = false };

                    regItemsByName[it.Name] = tag;
                }

                bool hasFields = RegBitFields.ByRegName.ContainsKey(tag.RegName);
                //string valueText = tag.RawValue.HasValue ? FormatRegValueMsbFirst(tag.RawValue.Value) : "-";
                string valueText = tag.RawValue.HasValue ? FormatRegValueLsbFirst(tag.RawValue.Value) : "-";
                string addrText = "0x" + tag.Addr.ToString("X4");
                string dependsOnText = tag.DependsOn;
                ListViewItem item = new ListViewItem(((hasFields ? (tag.Expanded ? ExpandMarkerExpanded : ExpandMarkerCollapsed) : "")) + tag.RegName);
                item.SubItems.Add(addrText);
                item.SubItems.Add(valueText);
                item.SubItems.Add(dependsOnText);
                item.ToolTipText = tag.Descp;
                item.Tag = tag;
                item.UseItemStyleForSubItems = false;
                if (IsRegisterGreyedOut(tag.Addr))
                {
                    // 目前 PARAMETER 設定下這顆不會被 GenerateTxt() 寫入, 畫面上不用填, 整列用灰字提示
                    item.SubItems[0].ForeColor = Color.DarkGray;
                    item.SubItems[1].ForeColor = Color.DarkGray;
                    item.SubItems[2].ForeColor = Color.DarkGray;
                    item.SubItems[3].ForeColor = Color.DarkGray;
                }
                else
                {
                    item.SubItems[0].ForeColor = Color.Black;
                    item.SubItems[1].ForeColor = Color.DarkBlue;
                    item.SubItems[2].ForeColor = Color.Black;
                    item.SubItems[3].ForeColor = Color.Gray;
                }
                ListViewGroup grp;
                if (regCategoryGroups.TryGetValue(RegCategories.Get(tag.Addr), out grp))
                    item.Group = grp;
                regDumpView.Items.Add(item);
                topLevelCount++;
                if (topItemAfter == null && topKey != null && GetItemKey(item) == topKey)
                    topItemAfter = item;

                if (tag.Expanded)
                {
                    ListViewItem matchedField = AddFieldRows(tag, grp, topItemAfter == null ? topKey : null);
                    if (topItemAfter == null)
                        topItemAfter = matchedField;
                }
            }
            regDumpView.EndUpdate();
            regDumpView.ShowItemToolTips = true;
            if (!bColumnsAutoSized)
            {
                //讓Register table欄位寬度符合字串寬
                regDumpView.Columns[0].Width = -1;
                regDumpView.Columns[1].Width = -2;
                regDumpView.Columns[2].Width = -1;
                regDumpView.Columns[3].Width = -1;
                regDumpView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                bColumnsAutoSized = true;
            }

            regDumpView.Columns[0].Text = "REG_NAME(" + topLevelCount + ")";

            if (topItemAfter != null)
            {
                // TopItem 的捲動位置是用 Item.Position 算像素差, 對有 Groups 的 ListView 在剛
                // Clear()+重新 Add() 完、版面還沒重新計算時常算錯 (量測結果永遠跳回最上面)。
                // 改用原生 LVM_ENSUREVISIBLE (EnsureVisible): 先把最後一列捲到可視範圍逼整份
                // 清單捲到底, 再對目標列呼叫 EnsureVisible, 因為此時目標列必定在可視範圍「上方」,
                // 所以會被捲到可視範圍最上緣 —— 等效於把它設成 TopItem, 但對 Groups 版面正確。
                regDumpView.Items[regDumpView.Items.Count - 1].EnsureVisible();
                topItemAfter.EnsureVisible();
            }
        }

        /*-------------------------------------------------------------
         *  GetTopVisibleItem
         * 找出目前實際顯示在可視範圍最上面的那一列。regDumpView.TopItem這個property底層是
         * LVM_GETTOPINDEX, 實測在有Groups的ListView上永遠回傳第0筆, 不會反映真正的捲動位置
         * ——這正是先前「還原捲動位置」失敗、每次都跳回第一筆的根因。改用GetItemAt()對client
         * area左上角逐點HitTest(底層是groups-aware的LVM_HITTEST), 若第一個點落在group標題列上
         * (回傳null)就往下探幾個像素, 直到打中第一列真正的item。
         * Param :
         *   (無)
         * Return:
         *   目前捲動位置最上面那一列; 找不到(例如清單是空的)則回傳null
         *-------------------------------------------------------------*/
        private ListViewItem GetTopVisibleItem()
        {
            for (int y = 2; y < regDumpView.ClientSize.Height; y += 4)
            {
                ListViewItem it = regDumpView.GetItemAt(2, y);
                if (it != null)
                    return it;
            }
            return null;
        }

        /*-------------------------------------------------------------
         *  GetItemKey
         * 依Tag內容組出可跨重建比對的識別字串, 用來在RebuildAllItems後找回原本捲動位置
         * 對應的列。
         * Param :
         *   item - 要取得識別字串的ListViewItem (Tag須為RegItemTag或FieldItemTag)
         * Return:
         *   識別字串 (register用RegName, bit field子列用"RegName|FieldName"); item為null
         *   或Tag型別不符則回傳null
         *-------------------------------------------------------------*/
        private static string GetItemKey(ListViewItem item)
        {
            if (item == null)
                return null;
            RegItemTag regTag = item.Tag as RegItemTag;
            if (regTag != null)
                return regTag.RegName;
            FieldItemTag fieldTag = item.Tag as FieldItemTag;
            if (fieldTag != null)
                return fieldTag.RegName + "|" + fieldTag.Field.FieldName;
            return null;
        }

        /*-------------------------------------------------------------
         *  AddFieldRows
         * [物件控制] - 刷新regDumpView (加入bit field子列)
         * 把tag目前展開狀態對應的bit field子列加到清單末端(呼叫端負責控制加入順序)。
         * 若wantedKey非null, 回傳其中符合該key的子列(供RebuildAllItems還原捲動位置用)。
         * Param :
         *   tag - 要展開的register (取其RegName/RawValue)
         *   grp - 子列要歸屬的ListViewGroup (跟上層register同一組)
         *   wantedKey - 要順便找回的子列key (GetItemKey格式); 不需要找就傳null
         * Return:
         *   符合wantedKey的子列; 沒有bit field定義、或wantedKey為null/沒找到則回傳null
         *-------------------------------------------------------------*/
        private ListViewItem AddFieldRows(RegItemTag tag, ListViewGroup grp, string wantedKey)
        {
            ListViewItem matched = null;
            RegBitField[] fields;
            if (!RegBitFields.ByRegName.TryGetValue(tag.RegName, out fields))
                return matched;

            uint raw = tag.RawValue ?? 0;
            foreach (RegBitField f in fields)
            {
                string fieldName = StripBitRangeSuffix(f.FieldName);
                ListViewItem sub = new ListViewItem(FieldRowIndent + "[" + f.BitRangeText + "] " + fieldName);
                sub.SubItems.Add("");
                uint fieldVal = (raw >> f.LowBit) & FieldMask(f.Width);
                int hexDigits = Math.Max(2, (f.Width + 3) / 4);
                sub.SubItems.Add(fieldVal.ToString("X" + hexDigits));
                sub.ForeColor = f.RW == "RW" ? Color.DarkGreen : Color.Gray;
                sub.Group = grp;
                sub.Tag = new FieldItemTag { RegName = tag.RegName, Field = f };
                sub.ToolTipText = fieldName + "  bits [" + f.BitRangeText + "]  " + f.RW +
                    (f.RW == "RW" ? "  (click VALUE to edit)" : "  (read-only)");
                regDumpView.Items.Add(sub);
                if (matched == null && wantedKey != null && GetItemKey(sub) == wantedKey)
                    matched = sub;
            }
            return matched;
        }

        /*-------------------------------------------------------------
         *  StripBitRangeSuffix
         * RegBitField.FieldName本身帶了"[高:低]"後綴(例如"ANCTL_PWR01[6:0]"), 顯示時已經有
         * 前綴的bit range, 這裡去掉重複的後綴。
         * Param :
         *   fieldName - 原始欄位名稱 (可能帶"[..]"後綴)
         * Return:
         *   去掉"[..]"後綴的欄位名稱; 沒有後綴則原樣傳回
         *-------------------------------------------------------------*/
        private static string StripBitRangeSuffix(string fieldName)
        {
            int idx = fieldName.IndexOf('[');
            return idx >= 0 ? fieldName.Substring(0, idx) : fieldName;
        }

        /*-------------------------------------------------------------
         *  UpdateItemTextsInPlace
         * [物件控制] - 刷新regDumpView (就地改文字, 不清空重建)
         * 清單筆數/順序不變(只是值變了)的快速路徑: 逐一更新既有ListViewItem的文字, 不清空/
         * 不重排, scrollbar位置與目前展開狀態都保留。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void UpdateItemTextsInPlace()
        {
            regDumpView.BeginUpdate();
            foreach (ListViewItem item in regDumpView.Items)
            {
                RegItemTag rt = item.Tag as RegItemTag;
                if (rt != null)
                {
                    string valueText = rt.RawValue.HasValue ? FormatRegValueLsbFirst(rt.RawValue.Value) : "-";
                    item.SubItems[2].Text = valueText;
                    item.ToolTipText = rt.Descp;

                    // Xtal/DataRate/Dev/中斷開關變動不會觸發 RebuildAllItems() (走的是這條就地更新的快速路徑),
                    // 灰字提示要在這裡也重算一次, 不然只有整份重建時才會套用, 不會立即反映
                    if (IsRegisterGreyedOut(rt.Addr))
                    {
                        item.SubItems[0].ForeColor = Color.DarkGray;
                        item.SubItems[1].ForeColor = Color.DarkGray;
                        item.SubItems[2].ForeColor = Color.DarkGray;
                        item.SubItems[3].ForeColor = Color.DarkGray;
                    }
                    else
                    {
                        item.SubItems[0].ForeColor = Color.Black;
                        item.SubItems[1].ForeColor = Color.DarkBlue;
                        item.SubItems[2].ForeColor = Color.Black;
                        item.SubItems[3].ForeColor = Color.Gray;
                    }
                    continue;
                }

                FieldItemTag ft = item.Tag as FieldItemTag;
                if (ft != null)
                {
                    RegItemTag owner;
                    uint raw = regItemsByName.TryGetValue(ft.RegName, out owner) ? (owner.RawValue ?? 0) : 0;
                    uint fieldVal = (raw >> ft.Field.LowBit) & FieldMask(ft.Field.Width);
                    int hexDigits = Math.Max(2, (ft.Field.Width + 3) / 4);
                    item.SubItems[2].Text = fieldVal.ToString("X" + hexDigits);
                }
            }
            
            regDumpView.EndUpdate();
        }

        /*-------------------------------------------------------------
         *  ShowRegisterDefaults
         * [物件控制] - 刷新regDumpView
         * 列出內建的register預設值(RegTableItems, 由reg_list.txt的名稱/位址 +
         * SPI_ER41xx_config.h的初始值事先產生並編譯進程式, 執行期不會去讀檔), 依reg_list.txt
         * 的順序排列。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void ShowRegisterDefaults()
        {
            List<RegRow> rows = new List<RegRow>();
            foreach (RegTableItem it in RegTableItems.Items)
                rows.Add(new RegRow { 
                    Name = it.Name, 
                    Addr = it.Addr, 
                    //ValueText = FormatRegValueMsbFirst(it.Value),
                    ValueText = FormatRegValueLsbFirst(it.Value),
                    DependsOn = it.DependsOn,
                    Descp = it.Descption,
                    Category = RegCategories.Get(it.Addr), 
                    RawValue = it.Value });

            RenderRegisterRows(rows, "default value from .h");
            //lblRegDumpStatus.Text = "default " + rows.Count + " regs";
            
        }

        /*-------------------------------------------------------------
         *  btnDefault_Click
         * [物件控制] - 刷新regDumpView
         * DEFAULT鈕: 把REG TABLE全部重設回.h內建預設值(放棄READ回來的實機值/畫面參數推算值)。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void btnDefault_Click(object sender, EventArgs e)
        {
            ShowRegisterDefaults();
        }


        /*-------------------------------------------------------------
         *  RefreshRegisterListFromUi
         * [物件控制] - 刷新regDumpView (經由ApplyUiValuesToRegisterList)
         * 參數有變動時重算整份REG TABLE: 先回到.h預設值, 再套上畫面現值。
         * 先歸零再套是必要的——否則按過READ之後列表是實機值, 只更新部分列會變成新舊混雜。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        public void RefreshRegisterListFromUi()
        {
            if (!bRegAutoRefreshReady || bRegRefreshing || regDumpView == null)
                return;
            bRegRefreshing = true;
            try
            {
                // ApplyUiValuesToRegisterList() 內部已經是「.h 預設值打底 + 畫面參數覆蓋」,
                // 整份重建一次即可; 算不出來時保持原樣不動。
                ApplyUiValuesToRegisterList();
                //bRegTableDirty = true;
            }
            finally
            {
                bRegRefreshing = false;
            }
        }

        /// <summary>
        /// 「這個控制項算不算 PARAMETER 區」的唯一清單來源 —— HookParameterChangeEvents() 掛
        /// Leave/SelectionChangeCommitted/CheckedChanged 用的是這份清單, MainForm_KeyDown()
        /// 判斷焦點在不在 PARAMETER 區(按 Enter 要不要重算)用的也是同一份, 避免兩邊各維護一次
        /// 造成以後改一邊忘了改另一邊。
        /// </summary>
        private HashSet<Control> parameterControls;

        /*-------------------------------------------------------------
         *  HookParameterChangeEvents
         * [物件控制] - 掛事件到所有PARAMETER區控制項 (paramBaseFreqText等TextBox/
         * paramDataRateCombo等ComboBox/paramTxSyncSoftInvertCheck等CheckBox/
         * paramPcrmuEnableRadioBtn等RadioButton/intModeCheckedList), 並建立parameterControls
         * 把所有會影響register的參數控制項掛上「變動就重算」。
         * TextBox用Leave(離開欄位才算, 不是每打一個字就算);
         * ComboBox用SelectionChangeCommitted + Leave, 且一律排到訊息迴圈後面才重算
         * (BeginInvoke) —— SelectionChangeCommitted觸發當下.Text/.SelectedItem有時還沒
         * 真的切換過去, 直接讀會拿到舊值, 症狀是"要再改一次上一次的值才生效";
         * CheckBox / RadioButton用CheckedChanged。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void HookParameterChangeEvents()
        {
            EventHandler onChange = delegate { RefreshRegisterListFromUi(); };
            // RadioButton 一次切換會讓兩顆都觸發 (舊的變 false、新的變 true), 只在被選中時重算
            EventHandler onRadio = delegate(object s, EventArgs e)
            {
                RadioButton rb = s as RadioButton;
                if (rb == null || rb.Checked)
                    RefreshRegisterListFromUi();
            };

            TextBox[] boxes = { paramBaseFreqText, paramDeviationText, paramPwrLvText, paramCapIoText, paramTxSyncText, paramRxSyncText,
                                paramPbContentText, paramPbLenText, paramRxSyncAllowBitErrText, paramIntModeTxAemptyText, paramIntModeRxAfullText };
            foreach (TextBox tb in boxes)
                if (tb != null) tb.Leave += onChange;

            // ComboBox 用 SelectionChangeCommitted 而不是 SelectedIndexChanged ——
            // 後者連「程式改動」也會觸發: txFreqText 每打一個字都會呼叫 UpdatePAConfigCombo()
            // 重建 PA CONFIG 的選項, 用 SelectedIndexChanged 就會在打字途中一直重算。
            //
            // 全部一律用 onComboChange (排到訊息迴圈後面再重算), 不用上面 TextBox/CheckBox 共用的
            // onChange: ComboBox 的 SelectionChangeCommitted 觸發當下, .Text/.SelectedItem 有時
            // 還沒真的切換過去 (跟下面 intModeCheckedList.ItemCheck 同一類 WinForms 時序問題) ——
            // 這原本只在 paramXtalCombo 上發現過, 所以曾經只特例處理它一個; 但 paramXtalGainCombo
            // 加進來後也重現了同樣「要多改一次上一次的值才會套用」的症狀, 顯示這其實是 ComboBox
            // 這個控制項類型共通的問題, 因此不再逐一特例, 所有 ComboBox 一律排到訊息迴圈後面再重算。
            EventHandler onComboChange = delegate
            {
                BeginInvoke((MethodInvoker)delegate { RefreshRegisterListFromUi(); });
            };
            ComboBox[] combos = { paramDataRateCombo, paramPAConfigCombo, paramModulationCombo, paramXtalGainCombo, paramXtalCombo,
                                  paramGpio0ModeCombo, paramGpio0GioSelCombo, paramGpio0PullCtrlCombo,
                                  paramGpio1ModeCombo, paramGpio1GioSelCombo, paramGpio1PullCtrlCombo, paramDvbBandCombo };
            foreach (ComboBox cb in combos)
                if (cb != null) { cb.SelectionChangeCommitted += onComboChange; cb.Leave += onComboChange; }

            CheckBox[] checks = { paramTxSyncSoftInvertCheck, paramRxSyncSoftInvertCheck, paramGpio0GIoInvCheck, paramGpio1GIoInvCheck
                                   /*, paramGpio0EnableCheck, paramGpio1EnableCheck*/ };
            foreach (CheckBox c in checks)
                if (c != null) c.CheckedChanged += onChange;

            RadioButton[] radios = { paramPcrmuEnableRadioBtn, paramPcrmuDisableRadioBtn, paramPcrShdEnableRadioBtn, paramFIFOBitOrderMSBRadioBtn, paramFIFOBitOrderLSBRadioBtn,
                                     paramTxFifoModeTransparentRadioBtn, paramTxFifoModeTransPresyncRadioBtn, paramTxFifoMode802154RadioBtn };
            foreach (RadioButton r in radios)
                if (r != null) r.CheckedChanged += onRadio;

            // PCRMU三選一(Enable/Enable + Shutdown/Disable): 選到"Enable + Shutdown"才鎖住GPIO1
            // Mode只能是Default_Mode(當Shutdown實體觸發腳位用)、同時解鎖STATE CTRL分頁的
            // dvbTstStateShutdownRadioBtn; 選其他兩個選項時反過來解鎖GPIO1、鎖住Shutdown狀態選項。
            // 只需要掛在paramPcrShdEnableRadioBtn自己身上: 不管是它被選中(true)還是被取消選中
            // (切去另外兩個時變成false), 都會觸發它自己的CheckedChanged。
            if (paramPcrShdEnableRadioBtn != null) paramPcrShdEnableRadioBtn.CheckedChanged += (s, e) => UpdateGpio1ModeLockFromShutdown();

            // INTERRUPT MODE 的勾選清單: ItemCheck 時狀態還沒套用, 要排到訊息迴圈後面再算
            if (intModeCheckedList != null)
                intModeCheckedList.ItemCheck += delegate
                {
                    BeginInvoke((MethodInvoker)delegate { RefreshRegisterListFromUi(); });
                };

            // parameterControls: 上面 boxes/combos/checks/radios (+intModeCheckedList) 全部收進同一個
            // set, 供 MainForm_KeyDown() 判斷「目前焦點在不在 PARAMETER 區」。
            parameterControls = new HashSet<Control>();
            foreach (TextBox tb in boxes) if (tb != null) parameterControls.Add(tb);
            foreach (ComboBox cb in combos) if (cb != null) parameterControls.Add(cb);
            foreach (CheckBox c in checks) if (c != null) parameterControls.Add(c);
            foreach (RadioButton r in radios) if (r != null) parameterControls.Add(r);
            if (intModeCheckedList != null) parameterControls.Add(intModeCheckedList);

            // 上面這些控制項各自的 Leave/SelectionChangeCommitted/CheckedChanged 已經涵蓋「改完值、
            // 焦點離開」的情況; 這裡額外補「焦點還在欄位裡、直接按 Enter」——KeyPreview 讓 Form
            // 在控制項自己處理按鍵之前先攔到, 不用逐一幫每個控制項掛按鍵事件。
            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;

            bRegAutoRefreshReady = true;
        }

        /*-------------------------------------------------------------
         *  MainForm_KeyDown
         * 焦點在PARAMETER區的控制項上時按Enter, 等同該控制項失焦(Leave)的效果——立即依目前
         * 畫面值重算REG TABLE, 不用先點別的地方讓焦點離開。
         * Param :
         *   sender - 未使用
         *   e - KeyDown事件參數, 取其KeyCode判斷是否為Enter
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && parameterControls != null && parameterControls.Contains(ActiveControl))
            {
                RefreshRegisterListFromUi();
                this.ActiveControl = null; //取消焦點，比如原本在填頻率，按下enter後會讓游標離開
            }
        }

        /*-------------------------------------------------------------
         *  ApplyUiValuesToRegisterList
         * [物件控制] - 刷新regDumpView
         * 開啟程式的第二步(由MainForm_Load呼叫, 此時各下拉選單才都填好了):
         * 依畫面上目前的參數算出對應的register值, 蓋掉第一步載入的預設值。
         * 例如FREQUENCY = 915MHz -> 0x1980顯示0x369CCAC0。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        public void ApplyUiValuesToRegisterList()
        {
            if (regDumpView == null)
                return;

            Dictionary<ushort, uint> cur = BuildRegistersFromUi();
            if (cur == null || cur.Count == 0)
                return;     // 失敗就維持預設值; 狀態列會停在 "default N regs" 作為區別

            // .h 預設值打底, 再用畫面參數算出來的值覆蓋 (畫面沒設定到的就維持預設值)
            List<RegRow> rows = new List<RegRow>();
            foreach (RegTableItem it in RegTableItems.Items)
            {
                uint value;
                if (!cur.TryGetValue(it.Addr, out value))
                    value = it.Value;
                rows.Add(new RegRow { 
                    Name = it.Name, 
                    Addr = it.Addr, 
                    //ValueText = FormatRegValueMsbFirst(value),
                    ValueText = FormatRegValueLsbFirst(value),
                    DependsOn = it.DependsOn,
                    Descp = it.Descption,
                    Category = RegCategories.Get(it.Addr), 
                    RawValue = value });
            }

            RenderRegisterRows(rows, "calculated from parameters");
            //lblRegDumpStatus.Text = rows.Count + " regs (UI)";
        }

        /*-------------------------------------------------------------
         *  BuildRegistersFromUi
         * 用Code Gen的引擎(GenerateTxt)依畫面現值算出整份register設定再解析回來。
         * 刻意重用GENERATE的流程, 保證列表的值與按下GENERATE產出的.h一致。
         * Param :
         *   (無)
         * Return:
         *   位址->值的對照表; 算不出來(欄位空白/格式錯誤/例外)則回傳null
         *-------------------------------------------------------------*/
        private Dictionary<ushort, uint> BuildRegistersFromUi()
        {
            string text = GetGeneratedRawText();
            return text == null ? null : ParseGeneratedRegisters(text);
        }

        /*-------------------------------------------------------------
         *  GetGeneratedRawText
         * 依GenerateTxt()的quiet/32-bit模式跑一次, 回傳原始產生文字。
         * BuildRegistersFromUi()(REG TABLE數值)跟GetGeneratedRegisterOrder()(Write DVB
         * Register順序)共用同一份, 避免各自重複一次「切旗標 -> 呼叫GenerateTxt -> 還原旗標」
         * 的邏輯。
         * Param :
         *   (無)
         * Return:
         *   GenerateTxt()產生的原始文字; PARAMETER必要欄位空白或產生失敗則回傳null
         *-------------------------------------------------------------*/
        private string GetGeneratedRawText()
        {
            // 靜默前置檢查: 欄位空的話直接放棄, 避免 CheckGenParameterReady() 在開機時跳 MessageBox
            if (string.IsNullOrWhiteSpace(paramXtalCombo.Text) ||
                string.IsNullOrWhiteSpace(paramXtalGainCombo.Text) ||
                string.IsNullOrWhiteSpace(paramCapIoText.Text) ||
                string.IsNullOrWhiteSpace(paramBaseFreqText.Text) ||
                string.IsNullOrWhiteSpace(paramDataRateCombo.Text) ||
                string.IsNullOrWhiteSpace(paramDeviationText.Text) ||
                string.IsNullOrWhiteSpace(paramPwrLvText.Text) ||
                string.IsNullOrWhiteSpace(paramPAConfigCombo.Text))
                return null;

            bool saved8Bits = is8BitsFormat;
            string text = null;
            try
            {
                is8BitsFormat = false;         // 32-bit 格式好解析, 產生完馬上還原
                bQuietLog = true;              // 擋掉 GenerateTxt() 內部的 "Generate at ..."
                GenMsg.Quiet = true;           // 這是背景計算, 不要跳對話框卡住使用者
                if (GenerateTxt("V4.04"))
                    text = generatedText;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                is8BitsFormat = saved8Bits;
                bQuietLog = false;
                GenMsg.Quiet = false;
            }
            return text;
        }

        /*-------------------------------------------------------------
         *  GetGeneratedRegisterOrder
         * 依GenerateTxt()實際寫入TRx_Config_Array/TRx_ConfigEx_Array的先後順序回傳位址清單
         * (不含Initial_Reg_Array, 那~40顆不在REG TABLE 82顆範圍內, 跟Write DVB Register無關)。
         * Write DVB Register靠這個順序寫, 保證跟Generate產生的內容順序一致——以後GenerateTxt()
         * 內部呼叫順序再怎麼調整, 這裡都會自動跟著對, 不用回頭手動調RegTableItems.cs的陣列位置。
         * Param :
         *   (無)
         * Return:
         *   依實際寫入順序排列的位址清單; GetGeneratedRawText()失敗則回傳空清單
         *-------------------------------------------------------------*/
        private List<ushort> GetGeneratedRegisterOrder()
        {
            var order = new List<ushort>();
            string text = GetGeneratedRawText();
            if (text == null)
                return order;

            string[] arrays = { "TRx_Config_Array", "TRx_ConfigEx_Array" };
            var seen = new HashSet<ushort>();
            foreach (string name in arrays)
            {
                int at = text.IndexOf(name, StringComparison.Ordinal);
                if (at < 0) continue;
                int open = text.IndexOf('{', at);
                if (open < 0) continue;
                int close = text.IndexOf("};", open, StringComparison.Ordinal);
                if (close < 0) close = text.Length;
                string body = text.Substring(open, close - open);
                body = Regex.Replace(body, @"//[^\n]*", "");
                foreach (Match m in RegPair.Matches(body))
                {
                    ushort addr;
                    if (TryParseHex16(m.Groups[1].Value, out addr) && seen.Add(addr))
                        order.Add(addr);
                }
            }
            return order;
        }

        /// <summary>
        /// 從 GenerateTxt 產生的 .h 內容抽出 register/值。只掃三個 t_reg_array 的範圍,
        /// 避免把 channel_pll_table 或 #define 誤判成 register。同一位址取最後出現的值。
        /// </summary>
        private static readonly Regex RegPair =
            new Regex(@"0x([0-9A-Fa-f]{1,4})\s*,\s*0x([0-9A-Fa-f]{1,8})\s*,", RegexOptions.Compiled);

        /*-------------------------------------------------------------
         *  ParseGeneratedRegisters
         * 從h(GenerateTxt產生的.h內容)抽出Initial_Reg_Array/TRx_Config_Array/
         * TRx_ConfigEx_Array三個陣列範圍內的register/值 (用RegPair這個regex比對, 避免把
         * channel_pll_table或#define誤判成register)。同一位址取最後出現的值。
         * Param :
         *   h - GenerateTxt()產生的原始文字
         * Return:
         *   位址->值的對照表 (三個陣列合併後的結果)
         *-------------------------------------------------------------*/
        private static Dictionary<ushort, uint> ParseGeneratedRegisters(string h)
        {
            Dictionary<ushort, uint> map = new Dictionary<ushort, uint>();
            string[] arrays = { "Initial_Reg_Array", "TRx_Config_Array", "TRx_ConfigEx_Array" };
            foreach (string name in arrays)
            {
                int at = h.IndexOf(name, StringComparison.Ordinal);
                if (at < 0)
                    continue;
                int open = h.IndexOf('{', at);
                if (open < 0)
                    continue;
                int close = h.IndexOf("};", open, StringComparison.Ordinal);
                if (close < 0)
                    close = h.Length;
                // 注意 Initial_Reg_Array 是「一行塞多筆」的格式 (0xA, 0xV, 0xA, 0xV, ...),
                // 不能用「一行兩欄」去切; 這裡用 regex 由左到右非重疊配對, 一定是 addr/value 成對。
                string body = h.Substring(open, close - open);
                body = Regex.Replace(body, @"//[^\n]*", "");
                foreach (Match m in RegPair.Matches(body))
                {
                    ushort addr;
                    uint val;
                    if (TryParseHex16(m.Groups[1].Value, out addr) && TryParseHex32(m.Groups[2].Value, out val))
                        map[addr] = val;
                }
            }
            return map;
        }

        /*-------------------------------------------------------------
         *  TryParseHex16
         * 把字串解析成16-bit十六進位值 (可帶"0x"前綴), 借用TryParseHex32並檢查範圍。
         * Param :
         *   s - 要解析的字串
         *   v - 輸出解析結果 (out); 解析失敗則為0
         * Return:
         *   true - 解析成功且值不超過0xFFFF / false - 格式錯誤或超出範圍
         *-------------------------------------------------------------*/
        private static bool TryParseHex16(string s, out ushort v)
        {
            uint u;
            v = 0;
            if (!TryParseHex32(s, out u) || u > 0xFFFF)
                return false;
            v = (ushort)u;
            return true;
        }

        /*-------------------------------------------------------------
         *  TryParseHex32
         * 把字串解析成32-bit十六進位值 (可帶"0x"前綴)。
         * Param :
         *   s - 要解析的字串
         *   v - 輸出解析結果 (out); 解析失敗則為0
         * Return:
         *   true - 解析成功 / false - s為null、長度不對、或含非十六進位字元
         *-------------------------------------------------------------*/
        private static bool TryParseHex32(string s, out uint v)
        {
            v = 0;
            if (s == null)
                return false;
            s = s.Trim();
            if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                s = s.Substring(2);
            if (s.Length == 0 || s.Length > 8 || !IsHex(s))
                return false;
            v = Convert.ToUInt32(s, 16);
            return true;
        }

        /*-------------------------------------------------------------
         *  regTableDumpDvbRegBtn_Click
         * [物件控制] - 刷新regDumpView
         * Read DVB鈕: 送"rdump"讀回實機值並顯示 (ReadRegisterDump + ShowRegisterDump)。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void regTableDumpDvbRegBtn_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                ShowInsLog("REG READ: COM port not open, please connect via the COM PORT menu\r\n");
                return;
            }

            //btnRegDump.Enabled = false;
            //btnRegTableWrite.Enabled = false;
            //lblRegDumpStatus.Text = "Reading ...";
            //lblRegDumpStatus.Update();
            regDumpView.Items.Clear();
            Cursor.Current = Cursors.WaitCursor;

            DateTime start = DateTime.Now;
            try
            {
                List<KeyValuePair<string, uint>> regs = ReadRegisterDump();
                if (regs == null)
                {
                    //lblRegDumpStatus.Text = "FAIL";
                }
                else
                {
                    ShowRegisterDump(regs);
                    TimeSpan t = DateTime.Now - start;
                    //lblRegDumpStatus.Text = regs.Count + " regs, " + t.TotalSeconds.ToString("0.00") + "s";
                }
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                //btnRegDump.Enabled = true;
                //btnRegTableWrite.Enabled = true;
            }
        }

        /// <summary>不管REG TABLE有沒有勾選、有沒有值, 一律不寫進DVB的位址清單。</summary>
        private static readonly HashSet<ushort> WriteIgnoredAddrList = new HashSet<ushort>
        {
            0xA028, //TX FIFO Format
        };

        /*-------------------------------------------------------------
         *  BuildRegWriteOrder
         * 算出這次Write DVB Register要寫的位址清單。順序跟Generate產生的內容一致:
         * 從GenerateTxt()實際寫入TRx_Config_Array/TRx_ConfigEx_Array的先後順序取位址清單
         * (不含Initial_Reg_Array), 排除WriteIgnoredAddrList, 再依目前REG TABLE的
         * Topic/DependsOn過濾狀態篩掉沒勾選的位址、以及還沒有值(畫面顯示"-")的位址。
         * Param :
         *   (無)
         * Return:
         *   這次實際要寫的位址清單 (依寫入順序排列)
         *-------------------------------------------------------------*/
        private List<ushort> BuildRegWriteOrder()
        {
            List<ushort> writeOrder = new List<ushort>();
            foreach (ushort addr in GetGeneratedRegisterOrder())
            {
                if (WriteIgnoredAddrList.Contains(addr))
                    continue;
                RegTableItem item;
                if (!RegTableItems.TryGetByAddr(addr, out item) || !PassesFilter(item))
                    continue;
                RegItemTag tag;
                if (!regItemsByName.TryGetValue(item.Name, out tag) || !tag.RawValue.HasValue)
                    continue;   // 還沒有值 (畫面上顯示 "-") 的不寫
                writeOrder.Add(addr);
            }
            return writeOrder;
        }

        /*-------------------------------------------------------------
         *  PerformRegWrite
         * [物件控制] - 附加顯示到logResultText (經由ShowInsLog)
         * 把writeOrder裡每個位址目前的值(regItemsByName現值), 逐筆用韌體既有的
         * "wr <addr> <value>" 命令寫回開發板, 並記錄進lastWrittenRegSnapshot供
         * IsRegTableDirty()之後比對用。每寫一筆就把REG_NAME/位址/值印一行到Log,
         * 留下實際寫了哪些register的紀錄。沿用舊版regPage(WriteAllRegProc2)的作法:
         * 不逐筆等回應, 只在送出之間留15ms, 韌體端wr沒有另外新增命令。
         * Param :
         *   writeOrder - 要寫入的位址清單 (依BuildRegWriteOrder()算出的順序)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void PerformRegWrite(List<ushort> writeOrder)
        {
            //btnRegDump.Enabled = false;
            //btnRegTableWrite.Enabled = false;
            //lblRegDumpStatus.Text = "Writing ...";
            //lblRegDumpStatus.Update();
            Cursor.Current = Cursors.WaitCursor;

            DateTime start = DateTime.Now;
            int written = 0;
            try
            {
                foreach (ushort addr in writeOrder)
                {
                    RegTableItem item;
                    RegTableItems.TryGetByAddr(addr, out item);
                    RegItemTag tag;
                    regItemsByName.TryGetValue(item.Name, out tag);
                    string cmd = "wr 0x" + addr.ToString("X4") + " 0x" + tag.RawValue.Value.ToString("X8") + "\r";
                    serialPort1.Write(cmd);
                    lastWrittenRegSnapshot[addr] = tag.RawValue.Value;   // 記下這次真的寫了什麼值
                    ShowInsLog("  wr " + item.Name + " (0x" + addr.ToString("X4") + ") = 0x" + tag.RawValue.Value.ToString("X8") + "\r\n");
                    written++;
                    Thread.Sleep(15);
                }
                TimeSpan t = DateTime.Now - start;
                //lblRegDumpStatus.Text = written + " regs written, " + t.TotalSeconds.ToString("0.00") + "s";
                ShowInsLog("REG WRITE: " + written + " regs written\r\n");
                //bRegTableDirty = false;
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                //btnRegDump.Enabled = true;
                //btnRegTableWrite.Enabled = true;
            }
        }

        /*-------------------------------------------------------------
         *  regTableWriteDvbRegBtn_Click
         * Write DVB鈕: 算出要寫的位址清單(BuildRegWriteOrder), 跳出確認對話框,
         * 使用者按Yes才真的執行寫入(PerformRegWrite)。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void regTableWriteDvbRegBtn_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                ShowInsLog("REG WRITE: COM port not open, please connect via the COM PORT menu\r\n");
                return;
            }

            List<ushort> writeOrder = BuildRegWriteOrder();
            if (writeOrder.Count == 0)
                return;

            PerformRegWrite(writeOrder);
        }

        /*-------------------------------------------------------------
         *  BuildSelectedRegWriteOrder
         * 跟BuildRegWriteOrder()一樣的過濾/順序邏輯(WriteIgnoredAddrList/PassesFilter/
         * 要有值), 但只保留目前在regDumpView裡被選取(反白, 可Shift/Ctrl多選)的頂層
         * register列所對應的位址。
         * Param :
         *   (無)
         * Return:
         *   目前選取中、且符合一般寫入條件的位址清單 (依BuildRegWriteOrder()的順序排列)
         *-------------------------------------------------------------*/
        private List<ushort> BuildSelectedRegWriteOrder()
        {
            HashSet<ushort> selectedAddrs = new HashSet<ushort>();
            foreach (ListViewItem item in regDumpView.SelectedItems)
            {
                RegItemTag tag = item.Tag as RegItemTag;
                if (tag != null)
                    selectedAddrs.Add(tag.Addr);
            }

            List<ushort> writeOrder = new List<ushort>();
            foreach (ushort addr in BuildRegWriteOrder())
            {
                if (selectedAddrs.Contains(addr))
                    writeOrder.Add(addr);
            }
            return writeOrder;
        }

        /*-------------------------------------------------------------
         *  regTableWriteSelDvbRegBtn_Click
         * "Wr Selected DVB Reg"鈕: 只把REG TABLE目前選取(反白)的register寫回開發板
         * (BuildSelectedRegWriteOrder), 其餘流程跟regTableWriteDvbRegBtn_Click相同
         * (先確認COM port已連線才真的寫入)。選取的列裡如果沒有任何一筆符合可寫入條件
         * (未被過濾掉、有值), 跳提示訊息並中止。
         * Param :
         *   sender, e - Click事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void regTableWriteSelDvbRegBtn_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                ShowInsLog("REG WRITE: COM port not open, please connect via the COM PORT menu\r\n");
                return;
            }

            List<ushort> writeOrder = BuildSelectedRegWriteOrder();
            if (writeOrder.Count == 0)
            {
                GenMsg.Show("No writable register is currently selected in REG TABLE.\r\nSelect one or more rows (Shift/Ctrl+Click) first.", "REG WRITE");
                return;
            }

            PerformRegWrite(writeOrder);
        }

        /*-------------------------------------------------------------
         *  IsRegTableDirty
         * 比對writeOrder裡每個位址的現值(regItemsByName)跟lastWrittenRegSnapshot(上次
         * Write DVB Register實際寫入的值), 只要有一個位址不同(含快照裡從沒寫過的新位址)
         * 就算Dirty。取代舊版單純的bRegTableDirty旗標, 避免值改回去後仍誤判為Dirty。
         * Param :
         *   writeOrder - 要比對的位址清單 (通常是BuildRegWriteOrder()算出的結果)
         * Return:
         *   true - 至少一個位址的現值跟上次寫入的快照不同 / false - 跟快照完全一致
         *-------------------------------------------------------------*/
        private bool IsRegTableDirty(List<ushort> writeOrder)
        {
            foreach (ushort addr in writeOrder)
            {
                RegTableItem item;
                RegTableItems.TryGetByAddr(addr, out item);
                RegItemTag tag;
                regItemsByName.TryGetValue(item.Name, out tag);
                uint snapshotValue;
                if (!lastWrittenRegSnapshot.TryGetValue(addr, out snapshotValue) || snapshotValue != tag.RawValue.Value)
                    return true;   // 快照裡沒這個位址(從沒寫過), 或值不一樣
            }
            return false;
        }
        /*-------------------------------------------------------------
         *  ConfirmRewriteIfRegTableDirty
         * dvbTstPktTxStartBtn/dvbTstContiTxStartBtn/dvbTstPktRxScanBtn/dvbTstPktRxStartBtn/
         * dvbTstStateSetBtn開始動作前呼叫: 用IsRegTableDirty()比對現值跟上次Write DVB
         * Register的快照(lastWrittenRegSnapshot), 有差異才詢問是否要先寫入。
         * 不管選Yes或No, 呼叫端原本要做的動作都照常繼續, 只差在有沒有先補寫暫存器。
         * Param :
         *   (無)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void ConfirmRewriteIfRegTableDirty()
        {
            //if (!bRegTableDirty)
            //    return;

            //if (MessageBox.Show("REG TABLE has been changed. Do you want to rewrite the settings to the DVB?",
            //    "REG TABLE Changed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    List<ushort> writeOrder = BuildRegWriteOrder();
            //    if (writeOrder.Count > 0)
            //        PerformRegWrite(writeOrder);
            //}

            List<ushort> writeOrder = BuildRegWriteOrder();
            if (writeOrder.Count == 0 || !IsRegTableDirty(writeOrder))
                return;

            if (MessageBox.Show("REG TABLE has been changed. Do you want to rewrite the settings to the DVB?", "REG TABLE Changed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                PerformRegWrite(writeOrder);
        }

        /*-------------------------------------------------------------
         *  ReadRegisterDump
         * 送"rdump"並讀回整份dump。韌體輸出格式: RDUMP_BEGIN,<count> / "AAAA=DDDDDDDD" xN /
         * RDUMP_END。dump期間會先拆掉Packet RX的DataReceived處理常式, 避免資料被搶走。
         * Param :
         *   (無)
         * Return:
         *   解析後的(位址字串,值)清單; COM port沒開/逾時/例外則回傳null
         *-------------------------------------------------------------*/
        private List<KeyValuePair<string, uint>> ReadRegisterDump()
        {
            // 不自動猜 COM port; 沿用主流程 (COM PORT 選單 → CONNECT) 開好的 serialPort1
            if (!serialPort1.IsOpen)
            {
                ShowInsLog("REG DUMP: COM port not open, please connect via the COM PORT menu\r\n");
                return null;
            }

            // dump 期間獨占 serialPort1, 避免 Packet RX 的 DataReceived 把資料吃掉
            bool bRestoreRxHandler = bUartRxHandlerOn;
            if (bRestoreRxHandler)
                DetachUartRxHandler();

            string raw = "";
            try
            {
                serialPort1.DiscardInBuffer();
                serialPort1.Write("rdump\r");
                raw = ReadUntil(RDUMP_END, 5000);
            }
            catch (Exception ex)
            {
                ShowInsLog("REG DUMP: " + ex.Message + "\r\n");
                return null;
            }
            finally
            {
                if (bRestoreRxHandler)
                    AttachUartRxHandler();
            }

            if (raw.IndexOf(RDUMP_END, StringComparison.Ordinal) < 0)
            {
                ShowInsLog("REG DUMP: timeout, no " + RDUMP_END + " received (firmware must support the rdump command)\r\n");
                if (raw.Length > 0)
                    ShowInsLog("REG DUMP: got " + raw.Length + " bytes\r\n");
                return null;
            }

            return ParseRegisterDump(raw);
        }

        /*-------------------------------------------------------------
         *  ReadUntil
         * 讀到出現terminator或逾時為止 (serialPort1沒有現成的read-until helper)。
         * Param :
         *   terminator - 要等到的結束字串
         *   timeoutMs - 逾時毫秒數
         * Return:
         *   目前為止讀到的所有內容 (可能不含terminator, 若逾時的話)
         *-------------------------------------------------------------*/
        private string ReadUntil(string terminator, int timeoutMs)
        {
            StringBuilder sb = new StringBuilder();
            DateTime deadline = DateTime.Now.AddMilliseconds(timeoutMs);
            while (DateTime.Now < deadline)
            {
                if (serialPort1.BytesToRead > 0)
                {
                    sb.Append(serialPort1.ReadExisting());
                    if (sb.ToString().IndexOf(terminator, StringComparison.Ordinal) >= 0)
                        break;
                }
                else
                {
                    Thread.Sleep(5);
                }
            }
            return sb.ToString();
        }

        /*-------------------------------------------------------------
         *  ParseRegisterDump
         * 解析ReadRegisterDump()讀回的原始文字, 逐行找"AAAA=DDDDDDDD"格式的位址/值配對
         * (行首可能被"CMD> "之類的prompt黏住, 用IndexOf而非StartsWith), 順便從
         * RDUMP_BEGIN行取出韌體回報的預期筆數跟實際解析出的筆數比對, 不符就記警告。
         * Param :
         *   raw - ReadRegisterDump()讀到的原始文字
         * Return:
         *   解析出的(位址字串,值)清單
         *-------------------------------------------------------------*/
        private List<KeyValuePair<string, uint>> ParseRegisterDump(string raw)
        {
            List<KeyValuePair<string, uint>> list = new List<KeyValuePair<string, uint>>();
            int expected = -1;
            string[] lines = raw.Replace("\r", "\n").Split('\n');

            foreach (string line in lines)
            {
                string s = line.Trim();
                if (s.Length == 0)
                    continue;
                // 用 IndexOf 而非 StartsWith: 行首可能被 "CMD> " 之類的 prompt 黏住
                int posBegin = s.IndexOf(RDUMP_BEGIN, StringComparison.Ordinal);
                if (posBegin >= 0)
                {
                    int comma = s.IndexOf(',', posBegin);
                    if (comma > 0)
                        int.TryParse(s.Substring(comma + 1).Trim(), out expected);
                    continue;
                }
                if (s.IndexOf(RDUMP_END, StringComparison.Ordinal) >= 0)
                    break;

                int eq = s.IndexOf('=');
                if (eq < 4 || s.Length < eq + 9)
                    continue;   // 不是 "AAAA=DDDDDDDD" 就跳過 (例如 CMD prompt)

                string addr = s.Substring(eq - 4, 4);
                string val = s.Substring(eq + 1, 8);
                if (!IsHex(addr) || !IsHex(val))
                    continue;
                uint u;
                try
                {
                    u = Convert.ToUInt32(val, 16);
                }
                catch (Exception)
                {
                    continue;
                }
                list.Add(new KeyValuePair<string, uint>(addr.ToUpper(), u));
            }

            if (expected > 0 && list.Count != expected)
                ShowInsLog("REG DUMP: expected " + expected + " but parsed " + list.Count + "\r\n");

            return list;
        }

        /*-------------------------------------------------------------
         *  ShowRegisterDump
         * [物件控制] - 刷新regDumpView
         * 韌體rdump回來的是完整位址表; 這裡只挑出RegTableItems這75筆, 跟預設值/畫面計算的
         * 清單保持同一組register、同一個順序, 才好逐列對照, 沒讀到值的顯示"-"。
         * Param :
         *   regs - ReadRegisterDump()回傳的(位址字串,值)清單
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void ShowRegisterDump(List<KeyValuePair<string, uint>> regs)
        {
            // 韌體 rdump 回來的是完整位址表; 這裡只挑出 RegTableItems 這 75 筆, 跟預設值/
            // 畫面計算的清單保持同一組 register、同一個順序, 才好逐列對照。
            Dictionary<ushort, uint> byAddr = new Dictionary<ushort, uint>();
            foreach (KeyValuePair<string, uint> kv in regs)
            {
                ushort addr;
                if (TryParseHex16(kv.Key, out addr))
                    byAddr[addr] = kv.Value;
            }

            List<RegRow> rows = new List<RegRow>();
            foreach (RegTableItem it in RegTableItems.Items)
            {
                uint value;
                bool known = byAddr.TryGetValue(it.Addr, out value);
                //string valueText = known ? FormatRegValueMsbFirst(value) : "-";
                string valueText = known ? FormatRegValueLsbFirst(value) : "-";
                rows.Add(new RegRow { 
                    Name = it.Name, 
                    Addr = it.Addr,
                    ValueText = valueText,
                    DependsOn = it.DependsOn,
                    Descp = it.Descption,
                    Category = RegCategories.Get(it.Addr), 
                    RawValue = known ? (uint?)value : null });
            }
            RenderRegisterRows(rows, "read from device (rdump)");
        }

        /*-------------------------------------------------------------
         *  IsHex
         * 判斷字串是否每個字元都是合法十六進位字元(0-9/A-F/a-f)且非空字串。
         * Param :
         *   s - 要檢查的字串
         * Return:
         *   true - 全部字元都是十六進位字元且長度>0 / false - 含非法字元或空字串
         *-------------------------------------------------------------*/
        private static bool IsHex(string s)
        {
            foreach (char c in s)
            {
                bool ok = (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f');
                if (!ok)
                    return false;
            }
            return s.Length > 0;
        }

        /*-------------------------------------------------------------
         *  FormatRegValueMsbFirst
         * 32-bit值轉成MSB-first的4個byte字串, 例如0x12345678 → "12 34 56 78"。
         * Param :
         *   value - 要格式化的32-bit值
         * Return:
         *   "XX XX XX XX"格式的字串 (高位元組在前)
         *-------------------------------------------------------------*/
        private static string FormatRegValueMsbFirst(uint value)
        {
            return ((value >> 24) & 0xFF).ToString("X2") + " " +
                   ((value >> 16) & 0xFF).ToString("X2") + " " +
                   ((value >> 8) & 0xFF).ToString("X2") + " " +
                   (value & 0xFF).ToString("X2");
        }

        /*-------------------------------------------------------------
         *  FormatRegValueLsbFirst
         * 32-bit值轉成LSB-first的4個byte字串, 例如0x12345678 → "78 56 34 12"。
         * Param :
         *   value - 要格式化的32-bit值
         * Return:
         *   "XX XX XX XX"格式的字串 (低位元組在前)
         *-------------------------------------------------------------*/
        private static string FormatRegValueLsbFirst(uint value)
        {
            return ((value >> 0) & 0xFF).ToString("X2") + " " +
                   ((value >> 8) & 0xFF).ToString("X2") + " " +
                   ((value >> 16) & 0xFF).ToString("X2") + " " +
                   ((value >> 24) & 0xFF).ToString("X2");
        }

        // =============== REG TABLE 展開 bit field / 編輯 ===============

        /*-------------------------------------------------------------
         *  FieldMask
         * width bit的遮罩, width>=32特別處理(1u&lt;&lt;32在C#會被當成1u&lt;&lt;0)。
         * Param :
         *   width - bit寬度
         * Return:
         *   低width bit全為1的遮罩值
         *-------------------------------------------------------------*/
        private static uint FieldMask(int width)
        {
            return width >= 32 ? 0xFFFFFFFFu : ((1u << width) - 1u);
        }

        /*-------------------------------------------------------------
         *  regDumpView_MouseClick
         * [物件控制] - 刷新regDumpView / 開啟regEditBox編輯框
         * 點到頂層register列: 只有點在REG_NAME欄位最前面的[+]/[-]展開圖示上才切換
         * 展開/收合(ToggleExpand); 點在同一列的其他地方只當一般選取(反白), 交給
         * ListView自己處理, 不做額外動作。點到已展開的可編輯bit field子列的VALUE欄:
         * 開始編輯(BeginEditField)。GPIO_0/1_CFG_REG兩顆一律唯讀, 提醒改去PARAMETER分頁。
         * Param :
         *   sender - 未使用
         *   e - MouseClick事件參數, 取其Location做HitTest
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void regDumpView_MouseClick(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo hit = regDumpView.HitTest(e.Location);
            if (hit.Item == null)
                return;

            RegItemTag regTag = hit.Item.Tag as RegItemTag;
            if (regTag != null)
            {
                if (RegBitFields.ByRegName.ContainsKey(regTag.RegName))
                {
                    // 展開圖示是REG_NAME欄位最前面的"[+] "/"[-] "這4個字元, 只有點在這個範圍內才展開/收合
                    Rectangle nameBounds = GetSubItemBounds(hit.Item, 0);
                    int markerWidth = TextRenderer.MeasureText(ExpandMarkerCollapsed, regDumpView.Font).Width;
                    if (e.Location.X >= nameBounds.Left && e.Location.X <= nameBounds.Left + markerWidth)
                        ToggleExpand(regTag);
                }
                return;
            }

            FieldItemTag fieldTag = hit.Item.Tag as FieldItemTag;
            if (fieldTag == null || hit.SubItem != hit.Item.SubItems[2] || fieldTag.Field.RW != "RW")
                return;

            // GPIO1 的值是 enable(MODE)+type(GIO_SEL) 組合出來的, 沒有像 DataRate 那樣的合法值清單可比對,
            // 為避免 REG TABLE 手改出 PARAMETER 分頁選不到的組合, 這顆改成唯讀, 一律以 PARAMETER 為主。
            if ((fieldTag.RegName == "GPIO_1_CFG_REG") || (fieldTag.RegName == "GPIO_0_CFG_REG"))
            {
                GenMsg.Show("GPIO# setting is controlled by GPIO1 Enable / Type on the PARAMETER tab. REG TABLE is read-only here.\r\n" +
                    "Please go to the PARAMETER tab to change it.", "REG TABLE");
                return;
            }

            BeginEditField(hit.Item, fieldTag);
        }

        /*-------------------------------------------------------------
         *  ToggleExpand
         * [物件控制] - 刷新regDumpView
         * 展開/收合只是切換tag.Expanded再整份重畫(RebuildAllItems)——不直接操作
         * ListViewItem, 因為Groups底下Insert沒辦法正確控制組內順序, 見RebuildAllItems的
         * 說明。切換前會先關掉可能開著的bit field編輯框。
         * Param :
         *   tag - 要展開/收合的register
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void ToggleExpand(RegItemTag tag)
        {
            EndEditField(true);
            if (!RegBitFields.ByRegName.ContainsKey(tag.RegName))
                return;
            tag.Expanded = !tag.Expanded;
            RebuildAllItems();
        }

        /*-------------------------------------------------------------
         *  GetSubItemBounds
         * Details view的SubItem.Bounds在.NET Framework不可靠, 用欄寬手動算位置。
         * Param :
         *   item - 目標列
         *   columnIndex - 要取得座標的欄位索引
         * Return:
         *   該欄位在regDumpView上的實際顯示範圍
         *-------------------------------------------------------------*/
        private Rectangle GetSubItemBounds(ListViewItem item, int columnIndex)
        {
            Rectangle itemBounds = item.Bounds;
            int x = itemBounds.Left;
            for (int i = 0; i < columnIndex; i++)
                x += regDumpView.Columns[i].Width;
            return new Rectangle(x, itemBounds.Top, regDumpView.Columns[columnIndex].Width, itemBounds.Height);
        }

        /*-------------------------------------------------------------
         *  BeginEditField
         * [物件控制] - 建立regEditBox並疊在regDumpView的VALUE欄位置上
         * 在指定bit field子列的VALUE欄位置上蓋一個TextBox overlay開始編輯, 先關掉前一個
         * 可能還開著的編輯框。
         * Param :
         *   item - 要編輯的bit field子列
         *   ft - 該子列的FieldItemTag (記到regEditBox.Tag供EndEditField/CommitFieldEdit使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void BeginEditField(ListViewItem item, FieldItemTag ft)
        {
            EndEditField(true);

            regEditBox = new TextBox();
            regEditBox.Font = regDumpView.Font;
            regEditBox.BorderStyle = BorderStyle.FixedSingle;
            regEditBox.Text = item.SubItems[2].Text;
            regEditBox.Bounds = GetSubItemBounds(item, 2);
            regEditBox.Tag = new object[] { item, ft };
            regEditBox.KeyDown += regEditBox_KeyDown;
            regEditBox.Leave += regEditBox_Leave;

            regDumpView.Controls.Add(regEditBox);
            regEditBox.BringToFront();
            regEditBox.Focus();
            regEditBox.SelectAll();
        }

        /*-------------------------------------------------------------
         *  regEditBox_KeyDown
         * bit field編輯框內按Enter提交(commit)修改, 按Escape放棄修改, 兩者都吃掉按鍵
         * 避免觸發其他預設行為。
         * Param :
         *   sender - 未使用
         *   e - KeyDown事件參數, 取其KeyCode判斷Enter/Escape
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void regEditBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                EndEditField(true);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true;
                EndEditField(false);
            }
        }

        /*-------------------------------------------------------------
         *  regEditBox_Leave
         * bit field編輯框失焦(點別的地方)時等同按Enter, 提交(commit)目前輸入的值。
         * Param :
         *   sender, e - Leave事件參數 (未使用)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void regEditBox_Leave(object sender, EventArgs e)
        {
            EndEditField(true);
        }

        /*-------------------------------------------------------------
         *  EndEditField
         * [物件控制] - 移除regEditBox
         * 關掉目前開著的bit field編輯框; commit=true才會套用內容(呼叫CommitFieldEdit)。
         * Param :
         *   commit - true=套用編輯框內容 / false=直接放棄
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void EndEditField(bool commit)
        {
            if (regEditBox == null)
                return;

            TextBox box = regEditBox;
            regEditBox = null;   // 先清掉, 避免 Controls.Remove 觸發的事件重入
            box.KeyDown -= regEditBox_KeyDown;
            box.Leave -= regEditBox_Leave;

            object[] ctx = (object[])box.Tag;
            ListViewItem item = (ListViewItem)ctx[0];
            FieldItemTag ft = (FieldItemTag)ctx[1];

            if (commit)
                CommitFieldEdit(item, ft, box.Text);

            regDumpView.Controls.Remove(box);
            box.Dispose();
        }

        /*-------------------------------------------------------------
         *  CommitFieldEdit
         * [物件控制] - 刷新regDumpView上該register列與其bit field子列的VALUE顯示
         * 把子列輸入的新值(text)寫回該bit field, 並重算、更新上層register的VALUE。
         * 輸入值超出該欄位寬度範圍會跳提示並放棄。若改的是SYNTH_CAL_CONFIG_2(DataRate),
         * 手改可以跳過離散合法值清單的限制, 這裡只即時提醒, 真正封鎖在GENERATE那一步。
         * Param :
         *   fieldItem - 被編輯的bit field子列
         *   ft - 該子列的FieldItemTag
         *   text - 使用者輸入的新值文字 (十六進位, 可帶"0x"前綴)
         * Return:
         *   (無)
         *-------------------------------------------------------------*/
        private void CommitFieldEdit(ListViewItem fieldItem, FieldItemTag ft, string text)
        {
            text = text.Trim();
            if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase) || text.StartsWith("0X", StringComparison.OrdinalIgnoreCase))
                text = text.Substring(2);

            uint maxVal = FieldMask(ft.Field.Width);
            uint newFieldVal;
            if (text.Length == 0 || !uint.TryParse(text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out newFieldVal) || newFieldVal > maxVal)
            {
                GenMsg.Show("Please enter a hex value between 0x0 and 0x" + maxVal.ToString("X") +
                    " (" + ft.Field.Width + "-bit field).", "REG TABLE");
                return;
            }

            ListViewItem regItem = FindRegisterItem(ft.RegName);
            RegItemTag regTag = regItem == null ? null : regItem.Tag as RegItemTag;
            if (regTag == null)
                return;

            uint oldRaw = regTag.RawValue ?? 0;
            uint mask = maxVal;
            uint newRaw = (oldRaw & ~(mask << ft.Field.LowBit)) | (newFieldVal << ft.Field.LowBit);

            regTag.RawValue = newRaw;
            regItem.SubItems[2].Text = FormatRegValueLsbFirst(newRaw);

            int hexDigits = Math.Max(2, (ft.Field.Width + 3) / 4);
            fieldItem.SubItems[2].Text = newFieldVal.ToString("X" + hexDigits);

            // DataRate (0x1988/SYNTH_CAL_CONFIG_2) 只允許離散清單內的值, 手改 REG TABLE 可以跳過
            // 這個限制, 這裡只即時提醒, 不擋這次編輯; 真正的封鎖在 GENERATE 那一步 (CodeGenProcess.GenerateTxt)。
            if (regTag.RegName == "SYNTH_CAL_CONFIG_2")
            {
                double kbps;
                if (!TryGetLegalDataRateKbps(newRaw, out kbps))
                {
                    GenMsg.Show("This DataRate value (" + kbps.ToString("0.000") + " kbps) is not valid.\r\n" +
                        "GENERATE will be blocked. Please use a valid value, or reapply DATA RATE from the PARAMETER tab to REG TABLE.",
                        "REG TABLE");
                }
            }
        }

        /*-------------------------------------------------------------
         *  FindRegisterItem
         * 在regDumpView目前顯示的列裡, 依RegName找出對應的頂層register列。
         * Param :
         *   regName - 要找的register名稱
         * Return:
         *   對應的ListViewItem; 目前沒顯示(被過濾掉或還沒建立)則回傳null
         *-------------------------------------------------------------*/
        private ListViewItem FindRegisterItem(string regName)
        {
            foreach (ListViewItem it in regDumpView.Items)
            {
                RegItemTag t = it.Tag as RegItemTag;
                if (t != null && t.RegName == regName)
                    return it;
            }
            return null;
        }
    }
}
