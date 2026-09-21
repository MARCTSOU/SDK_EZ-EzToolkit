using System.Drawing;

namespace ER41xxTestTool
{
    partial class MainForm
    {
        /// <summary>
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// clear all resource
        /// </summary>
        /// <param name="disposing">  if release resource ==> true；else==> false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statuslabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTimeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statustimer = new System.Windows.Forms.Timer(this.components);
            this.autoSendtimer = new System.Windows.Forms.Timer(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.recHexRadiobtn = new System.Windows.Forms.RadioButton();
            this.recStrRadiobtn = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cleartxsendbtn = new System.Windows.Forms.Button();
            this.sendHexRadiobtn = new System.Windows.Forms.RadioButton();
            this.sendStrRadiobtn = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.addCRCcbx = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.sendIntervalTimetbx = new System.Windows.Forms.TextBox();
            this.autoReplyCbx = new System.Windows.Forms.CheckBox();
            this.autoSendcbx = new System.Windows.Forms.CheckBox();
            this.dvbTstZoneTab = new System.Windows.Forms.TabControl();
            this.dvbTstPktTxPage = new System.Windows.Forms.TabPage();
            this.dvbTstPktFifoDgmHelpBtn = new System.Windows.Forms.Button();
            this.dvbTstPktTxDgmSyncLenLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxDgmSyncLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxDgmCrcLenLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxDgmMacRangeLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxDgmFifoRangeLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxDgmCrcLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxDgmPayloadLenLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxDgmPayloadLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxDgmSeqNumLenLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxDgmSeqNumLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxDgmLengthLenLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxDgmLengthLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxDgmPbLenLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxDgmPbLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxCntGroup = new System.Windows.Forms.GroupBox();
            this.dvbTstPktTxCntIntvUnitLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxCntUnitLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxCntIntvText = new System.Windows.Forms.TextBox();
            this.dvbTstPktTxCntText = new System.Windows.Forms.TextBox();
            this.dvbTstPktTxInfiniteCheck = new System.Windows.Forms.CheckBox();
            this.dvbTstPktTxContentGroup = new System.Windows.Forms.GroupBox();
            this.dvbTstPktTxCrcCalcBtn = new System.Windows.Forms.Button();
            this.dvbTstPktTxRandomPlLenUnitLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxCrcPolyLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxRandomPlCheck = new System.Windows.Forms.CheckBox();
            this.dvbTstPktTxRandomPlLenText = new System.Windows.Forms.TextBox();
            this.dvbTstPktTxCrcCombo = new System.Windows.Forms.ComboBox();
            this.dvbTstPktTxPrependSeqCheck = new System.Windows.Forms.CheckBox();
            this.dvbTstPktTxStopBtn = new System.Windows.Forms.Button();
            this.dvbTstPktTxFilledLenText = new System.Windows.Forms.TextBox();
            this.dvbTstPktTxFilledLenDescLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxStartBtn = new System.Windows.Forms.Button();
            this.dvbTstPktTxPayloadText = new System.Windows.Forms.TextBox();
            this.dvbTstPktTxMaxPayloadSeqLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxMaxPayloadNoSeqLabel = new System.Windows.Forms.Label();
            this.dvbTstPktTxTestBtn = new System.Windows.Forms.Button();
            this.dvbTstContiTxPage = new System.Windows.Forms.TabPage();
            this.dvbTstContiTxStopBtn = new System.Windows.Forms.Button();
            this.dvbTstContiTxPatternGroup = new System.Windows.Forms.GroupBox();
            this.dvbTstContiTxPrbs9RadioBtn = new System.Windows.Forms.RadioButton();
            this.dvbTstContiTxBit0101RadioBtn = new System.Windows.Forms.RadioButton();
            this.dvbTstContiTxStartBtn = new System.Windows.Forms.Button();
            this.dvbTstContiTxModeGroup = new System.Windows.Forms.GroupBox();
            this.dvbTstContiTxPatternRadioBtn = new System.Windows.Forms.RadioButton();
            this.dvbTstContiTxSingleToneRadioBtn = new System.Windows.Forms.RadioButton();
            this.dvbTstPktRxPage = new System.Windows.Forms.TabPage();
            this.dvbTstPktRxRssiText = new System.Windows.Forms.TextBox();
            this.dvbTstPktRxRssiLabel = new System.Windows.Forms.Label();
            this.dvbTstPktRxClearLogBtn = new System.Windows.Forms.Button();
            this.dvbTstPktRxScanBtn = new System.Windows.Forms.Button();
            this.dvbTstPktRxSetPayloadBtn = new System.Windows.Forms.Button();
            this.dvbTstPktRxCntGroup = new System.Windows.Forms.GroupBox();
            this.dvbTstPktRxCntLimitRadioBtn = new System.Windows.Forms.RadioButton();
            this.dvbTstPktRxCntText = new System.Windows.Forms.TextBox();
            this.dvbTstPktRxCntNoLimitRadioBtn = new System.Windows.Forms.RadioButton();
            this.dvbTstPktRxParserGroup = new System.Windows.Forms.GroupBox();
            this.dvbTstPktRxParseSeqCheck = new System.Windows.Forms.CheckBox();
            this.dvbTstPktRxCrcPolyLabel = new System.Windows.Forms.Label();
            this.dvbTstPktRxCrcCombo = new System.Windows.Forms.ComboBox();
            this.dvbTstPktRxErrCntLabel = new System.Windows.Forms.Label();
            this.dvbTstPktRxErrCntText = new System.Windows.Forms.TextBox();
            this.dvbTstPktRxLogText = new System.Windows.Forms.TextBox();
            this.dvbTstPktRxStartBtn = new System.Windows.Forms.Button();
            this.dvbTstPktRxOkCntLabel = new System.Windows.Forms.Label();
            this.dvbTstPktRxOkCntText = new System.Windows.Forms.TextBox();
            this.dvbTstStatePage = new System.Windows.Forms.TabPage();
            this.dvbTstStateSetBtn = new System.Windows.Forms.Button();
            this.dvbTstStateGroup = new System.Windows.Forms.GroupBox();
            this.dvbTstStateShutdownRadioBtn = new System.Windows.Forms.RadioButton();
            this.dvbTstStateDeepSleepRadioBtn = new System.Windows.Forms.RadioButton();
            this.dvbTstStateIdleRadioBtn = new System.Windows.Forms.RadioButton();
            this.regWrPage = new System.Windows.Forms.TabPage();
            this.paramFreqTableGroup = new System.Windows.Forms.GroupBox();
            this.paramFreqTableInputText = new System.Windows.Forms.TextBox();
            this.paramFreqTableUnitLabel = new System.Windows.Forms.Label();
            this.paramFreqTableAddBtn = new System.Windows.Forms.Button();
            this.paramFreqTableList = new System.Windows.Forms.ListView();
            this.paramFreqTableDelAllBtn = new System.Windows.Forms.Button();
            this.paramFreqTableDeleteBtn = new System.Windows.Forms.Button();
            this.paramPbLabel = new System.Windows.Forms.Label();
            this.paramPbContentText = new System.Windows.Forms.TextBox();
            this.paramPbUnitLabel = new System.Windows.Forms.Label();
            this.paramPbLenText = new System.Windows.Forms.TextBox();
            this.paramTxSyncLabel = new System.Windows.Forms.Label();
            this.paramTxSyncActualLabel = new System.Windows.Forms.Label();
            this.paramRxSyncLabel = new System.Windows.Forms.Label();
            this.paramRxSyncText = new System.Windows.Forms.TextBox();
            this.paramRxSyncSoftInvertCheck = new System.Windows.Forms.CheckBox();
            this.paramRxSyncActualLabel = new System.Windows.Forms.Label();
            this.paramRxSyncAllowBitErrText = new System.Windows.Forms.TextBox();
            this.paramRxSyncAllowBitErrUnitLabel = new System.Windows.Forms.Label();
            this.paramFIFOBitOrderGroup = new System.Windows.Forms.GroupBox();
            this.paramFIFOBitOrderLSBRadioBtn = new System.Windows.Forms.RadioButton();
            this.paramFIFOBitOrderMSBRadioBtn = new System.Windows.Forms.RadioButton();
            this.paramTxFifoModeGroup = new System.Windows.Forms.GroupBox();
            this.paramTxFifoModeTransPresyncRadioBtn = new System.Windows.Forms.RadioButton();
            this.paramTxFifoModeTransparentRadioBtn = new System.Windows.Forms.RadioButton();
            this.paramTxFifoMode802154RadioBtn = new System.Windows.Forms.RadioButton();
            this.paramPcrmuGroup = new System.Windows.Forms.GroupBox();
            this.paramPcrShdEnableRadioBtn = new System.Windows.Forms.RadioButton();
            this.paramPcrmuEnableRadioBtn = new System.Windows.Forms.RadioButton();
            this.paramPcrmuDisableRadioBtn = new System.Windows.Forms.RadioButton();
            this.paramDataFormatGroup = new System.Windows.Forms.GroupBox();
            this.paramDataFormat8BitsRadioBtn = new System.Windows.Forms.RadioButton();
            this.paramDataFormat32BitsRadioBtn = new System.Windows.Forms.RadioButton();
            this.paramIntModeGroup = new System.Windows.Forms.GroupBox();
            this.paramIntModeSelectedLabel = new System.Windows.Forms.Label();
            this.paramIntModeSelectedCountLabel = new System.Windows.Forms.Label();
            this.paramIntModeSelectedUnitLabel = new System.Windows.Forms.Label();
            this.paramIntModeTxAemptyGroup = new System.Windows.Forms.GroupBox();
            this.paramIntModeTxAemptyText = new System.Windows.Forms.TextBox();
            this.paramIntModeRxAfullGroup = new System.Windows.Forms.GroupBox();
            this.paramIntModeRxAfullText = new System.Windows.Forms.TextBox();
            this.paramGpio0Group = new System.Windows.Forms.GroupBox();
            this.paramGpio0GIoInvCheck = new System.Windows.Forms.CheckBox();
            this.paramGpio0ModeLabel = new System.Windows.Forms.Label();
            this.paramGpio0PullCtrlLabel = new System.Windows.Forms.Label();
            this.paramGpio0GIoSelLabel = new System.Windows.Forms.Label();
            this.paramGpio0ModeCombo = new System.Windows.Forms.ComboBox();
            this.paramGpio0PullCtrlCombo = new System.Windows.Forms.ComboBox();
            this.paramGpio0GioSelCombo = new System.Windows.Forms.ComboBox();
            this.paramGpio1GioSelCombo = new System.Windows.Forms.ComboBox();
            this.logResultClearBtn = new System.Windows.Forms.Button();
            this.logResultText = new System.Windows.Forms.TextBox();
            this.paramTxSyncSoftInvertCheck = new System.Windows.Forms.CheckBox();
            this.paramCapIoText = new System.Windows.Forms.TextBox();
            this.paramCapIoLabel = new System.Windows.Forms.Label();
            this.paramPwrLvText = new System.Windows.Forms.TextBox();
            this.paramDeviationText = new System.Windows.Forms.TextBox();
            this.paramPwrLvLabel = new System.Windows.Forms.Label();
            this.paramTxSyncText = new System.Windows.Forms.TextBox();
            this.paramDataRateCombo = new System.Windows.Forms.ComboBox();
            this.paramBaseFreqText = new System.Windows.Forms.TextBox();
            this.paramBaseFreqLabel = new System.Windows.Forms.Label();
            this.paramDeviationLabel = new System.Windows.Forms.Label();
            this.paramModulationCombo = new System.Windows.Forms.ComboBox();
            this.paramModulationLabel = new System.Windows.Forms.Label();
            this.paramDataRateLabel = new System.Windows.Forms.Label();
            this.paramPAConfigCombo = new System.Windows.Forms.ComboBox();
            this.paramPAConfigLabel = new System.Windows.Forms.Label();
            this.btnTxSet = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileHdlrToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveInitValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadParameterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveParameterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dvbConnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshComToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comPortToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.connectComToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort3 = new System.IO.Ports.SerialPort(this.components);
            this.paramZoneTab = new System.Windows.Forms.TabControl();
            this.pageParameter = new System.Windows.Forms.TabPage();
            this.paramPktGroup = new System.Windows.Forms.GroupBox();
            this.paramSyncGroup = new System.Windows.Forms.GroupBox();
            this.paramRxSyncAllowBitErrLabel = new System.Windows.Forms.Label();
            this.paramPbMultipLabel = new System.Windows.Forms.Label();
            this.paramModemGroup = new System.Windows.Forms.GroupBox();
            this.paramDeviationHelpBtn = new System.Windows.Forms.Button();
            this.paramFreqAndPwrGroup = new System.Windows.Forms.GroupBox();
            this.paramXtalGainLabel = new System.Windows.Forms.Label();
            this.paramXtalGainCombo = new System.Windows.Forms.ComboBox();
            this.paramXtalHelpBtn = new System.Windows.Forms.Button();
            this.paramFreqHelpBtn = new System.Windows.Forms.Button();
            this.paramPwrHelpBtn = new System.Windows.Forms.Button();
            this.paramXtalCombo = new System.Windows.Forms.ComboBox();
            this.paramDvbBandLabel = new System.Windows.Forms.Label();
            this.paramDvbBandCombo = new System.Windows.Forms.ComboBox();
            this.paramXtalLabel = new System.Windows.Forms.Label();
            this.pageFreqTable = new System.Windows.Forms.TabPage();
            this.pageTxPayload = new System.Windows.Forms.TabPage();
            this.paramTxFifoModeTransHelpLabel = new System.Windows.Forms.Label();
            this.paramTxFifoMode802HelpLabel = new System.Windows.Forms.Label();
            this.paramTxFifoModePreSyncHelpLabel = new System.Windows.Forms.Label();
            this.paramTxFifoModeNoticeLabel = new System.Windows.Forms.Label();
            this.pageIntMode = new System.Windows.Forms.TabPage();
            this.pageGpio = new System.Windows.Forms.TabPage();
            this.paramGpioHelpBtn = new System.Windows.Forms.Button();
            this.paramGpio1Group = new System.Windows.Forms.GroupBox();
            this.paramGpio1GIoInvCheck = new System.Windows.Forms.CheckBox();
            this.paramGpio1GIoSelLabel = new System.Windows.Forms.Label();
            this.paramGpio1PullCtrlLabel = new System.Windows.Forms.Label();
            this.paramGpio1ModeLabel = new System.Windows.Forms.Label();
            this.paramGpio1PullCtrlCombo = new System.Windows.Forms.ComboBox();
            this.paramGpio1ModeCombo = new System.Windows.Forms.ComboBox();
            this.pagePcrmu = new System.Windows.Forms.TabPage();
            this.paramPcrShdEnableHelpLabel = new System.Windows.Forms.Label();
            this.regTableGenBtn = new System.Windows.Forms.Button();
            this.regTableDefaultBtn = new System.Windows.Forms.Button();
            this.regTableTopicGroup = new System.Windows.Forms.GroupBox();
            this.dvbTstTopicLabel = new System.Windows.Forms.Label();
            this.paramTopicLabel = new System.Windows.Forms.Label();
            this.regTableDumpDvbRegBtn = new System.Windows.Forms.Button();
            this.regTableWriteDvbRegBtn = new System.Windows.Forms.Button();
            this.regTableIncInitArrayCheck = new System.Windows.Forms.CheckBox();
            this.dvbTstSubTopicLabel = new System.Windows.Forms.Label();
            this.regTableWriteSelDvbRegBtn = new System.Windows.Forms.Button();
            this.dvbTstConnStateLabel = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.dvbTstZoneTab.SuspendLayout();
            this.dvbTstPktTxPage.SuspendLayout();
            this.dvbTstPktTxCntGroup.SuspendLayout();
            this.dvbTstPktTxContentGroup.SuspendLayout();
            this.dvbTstContiTxPage.SuspendLayout();
            this.dvbTstContiTxPatternGroup.SuspendLayout();
            this.dvbTstContiTxModeGroup.SuspendLayout();
            this.dvbTstPktRxPage.SuspendLayout();
            this.dvbTstPktRxCntGroup.SuspendLayout();
            this.dvbTstPktRxParserGroup.SuspendLayout();
            this.dvbTstStatePage.SuspendLayout();
            this.dvbTstStateGroup.SuspendLayout();
            this.paramFreqTableGroup.SuspendLayout();
            this.paramFIFOBitOrderGroup.SuspendLayout();
            this.paramTxFifoModeGroup.SuspendLayout();
            this.paramPcrmuGroup.SuspendLayout();
            this.paramDataFormatGroup.SuspendLayout();
            this.paramIntModeGroup.SuspendLayout();
            this.paramIntModeTxAemptyGroup.SuspendLayout();
            this.paramIntModeRxAfullGroup.SuspendLayout();
            this.paramGpio0Group.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.paramZoneTab.SuspendLayout();
            this.pageParameter.SuspendLayout();
            this.paramPktGroup.SuspendLayout();
            this.paramSyncGroup.SuspendLayout();
            this.paramModemGroup.SuspendLayout();
            this.paramFreqAndPwrGroup.SuspendLayout();
            this.pageFreqTable.SuspendLayout();
            this.pageTxPayload.SuspendLayout();
            this.pageIntMode.SuspendLayout();
            this.pageGpio.SuspendLayout();
            this.paramGpio1Group.SuspendLayout();
            this.pagePcrmu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statuslabel,
            this.toolStripStatusLabel1,
            this.statusTimeLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 816);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 20, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1405, 22);
            this.statusStrip1.TabIndex = 21;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statuslabel
            // 
            this.statuslabel.ActiveLinkColor = System.Drawing.SystemColors.ButtonHighlight;
            this.statuslabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statuslabel.Name = "statuslabel";
            this.statuslabel.Size = new System.Drawing.Size(1384, 16);
            this.statuslabel.Spring = true;
            this.statuslabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 16);
            // 
            // statusTimeLabel
            // 
            this.statusTimeLabel.Name = "statusTimeLabel";
            this.statusTimeLabel.Size = new System.Drawing.Size(0, 16);
            // 
            // statustimer
            // 
            this.statustimer.Enabled = true;
            this.statustimer.Interval = 1000;
            this.statustimer.Tick += new System.EventHandler(this.statustimer_Tick);
            // 
            // autoSendtimer
            // 
            this.autoSendtimer.Interval = 1000;
            this.autoSendtimer.Tick += new System.EventHandler(this.autoSendtimer_Tick);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1088, 42);
            this.panel3.TabIndex = 12;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.recHexRadiobtn);
            this.panel1.Controls.Add(this.recStrRadiobtn);
            this.panel1.Location = new System.Drawing.Point(108, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(159, 29);
            this.panel1.TabIndex = 21;
            // 
            // recHexRadiobtn
            // 
            this.recHexRadiobtn.AutoSize = true;
            this.recHexRadiobtn.Location = new System.Drawing.Point(84, 5);
            this.recHexRadiobtn.Margin = new System.Windows.Forms.Padding(4);
            this.recHexRadiobtn.Name = "recHexRadiobtn";
            this.recHexRadiobtn.Size = new System.Drawing.Size(51, 19);
            this.recHexRadiobtn.TabIndex = 15;
            this.recHexRadiobtn.TabStop = true;
            this.recHexRadiobtn.Text = "Hex";
            this.recHexRadiobtn.UseVisualStyleBackColor = true;
            // 
            // recStrRadiobtn
            // 
            this.recStrRadiobtn.AutoSize = true;
            this.recStrRadiobtn.Checked = true;
            this.recStrRadiobtn.Location = new System.Drawing.Point(3, 5);
            this.recStrRadiobtn.Margin = new System.Windows.Forms.Padding(4);
            this.recStrRadiobtn.Name = "recStrRadiobtn";
            this.recStrRadiobtn.Size = new System.Drawing.Size(63, 19);
            this.recStrRadiobtn.TabIndex = 14;
            this.recStrRadiobtn.TabStop = true;
            this.recStrRadiobtn.Text = "String";
            this.recStrRadiobtn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Received:";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.cleartxsendbtn);
            this.panel5.Controls.Add(this.sendHexRadiobtn);
            this.panel5.Controls.Add(this.sendStrRadiobtn);
            this.panel5.Controls.Add(this.label2);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1088, 44);
            this.panel5.TabIndex = 12;
            // 
            // cleartxsendbtn
            // 
            this.cleartxsendbtn.Location = new System.Drawing.Point(309, 9);
            this.cleartxsendbtn.Margin = new System.Windows.Forms.Padding(4);
            this.cleartxsendbtn.Name = "cleartxsendbtn";
            this.cleartxsendbtn.Size = new System.Drawing.Size(77, 31);
            this.cleartxsendbtn.TabIndex = 19;
            this.cleartxsendbtn.Text = "Clear";
            this.cleartxsendbtn.UseVisualStyleBackColor = true;
            // 
            // sendHexRadiobtn
            // 
            this.sendHexRadiobtn.AutoSize = true;
            this.sendHexRadiobtn.Location = new System.Drawing.Point(152, 9);
            this.sendHexRadiobtn.Margin = new System.Windows.Forms.Padding(4);
            this.sendHexRadiobtn.Name = "sendHexRadiobtn";
            this.sendHexRadiobtn.Size = new System.Drawing.Size(51, 19);
            this.sendHexRadiobtn.TabIndex = 18;
            this.sendHexRadiobtn.TabStop = true;
            this.sendHexRadiobtn.Text = "Hex";
            this.sendHexRadiobtn.UseVisualStyleBackColor = true;
            // 
            // sendStrRadiobtn
            // 
            this.sendStrRadiobtn.AutoSize = true;
            this.sendStrRadiobtn.Checked = true;
            this.sendStrRadiobtn.Location = new System.Drawing.Point(65, 9);
            this.sendStrRadiobtn.Margin = new System.Windows.Forms.Padding(4);
            this.sendStrRadiobtn.Name = "sendStrRadiobtn";
            this.sendStrRadiobtn.Size = new System.Drawing.Size(63, 19);
            this.sendStrRadiobtn.TabIndex = 17;
            this.sendStrRadiobtn.TabStop = true;
            this.sendStrRadiobtn.Text = "String";
            this.sendStrRadiobtn.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Send:";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.addCRCcbx);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.sendIntervalTimetbx);
            this.panel6.Controls.Add(this.autoReplyCbx);
            this.panel6.Controls.Add(this.autoSendcbx);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 285);
            this.panel6.Margin = new System.Windows.Forms.Padding(4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1088, 65);
            this.panel6.TabIndex = 14;
            // 
            // addCRCcbx
            // 
            this.addCRCcbx.AutoSize = true;
            this.addCRCcbx.Enabled = false;
            this.addCRCcbx.Location = new System.Drawing.Point(253, 9);
            this.addCRCcbx.Margin = new System.Windows.Forms.Padding(4);
            this.addCRCcbx.Name = "addCRCcbx";
            this.addCRCcbx.Size = new System.Drawing.Size(77, 19);
            this.addCRCcbx.TabIndex = 29;
            this.addCRCcbx.Text = "Add Crc";
            this.addCRCcbx.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(184, 9);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 15);
            this.label8.TabIndex = 28;
            this.label8.Text = "ms";
            // 
            // sendIntervalTimetbx
            // 
            this.sendIntervalTimetbx.Location = new System.Drawing.Point(117, 4);
            this.sendIntervalTimetbx.Margin = new System.Windows.Forms.Padding(4);
            this.sendIntervalTimetbx.MaxLength = 9;
            this.sendIntervalTimetbx.Name = "sendIntervalTimetbx";
            this.sendIntervalTimetbx.Size = new System.Drawing.Size(57, 25);
            this.sendIntervalTimetbx.TabIndex = 27;
            this.sendIntervalTimetbx.Text = "1000";
            // 
            // autoReplyCbx
            // 
            this.autoReplyCbx.AutoSize = true;
            this.autoReplyCbx.Enabled = false;
            this.autoReplyCbx.Location = new System.Drawing.Point(13, 35);
            this.autoReplyCbx.Margin = new System.Windows.Forms.Padding(4);
            this.autoReplyCbx.Name = "autoReplyCbx";
            this.autoReplyCbx.Size = new System.Drawing.Size(90, 19);
            this.autoReplyCbx.TabIndex = 26;
            this.autoReplyCbx.Text = "AutoReply";
            this.autoReplyCbx.UseVisualStyleBackColor = true;
            // 
            // autoSendcbx
            // 
            this.autoSendcbx.AutoSize = true;
            this.autoSendcbx.Enabled = false;
            this.autoSendcbx.Location = new System.Drawing.Point(13, 8);
            this.autoSendcbx.Margin = new System.Windows.Forms.Padding(4);
            this.autoSendcbx.Name = "autoSendcbx";
            this.autoSendcbx.Size = new System.Drawing.Size(85, 19);
            this.autoSendcbx.TabIndex = 22;
            this.autoSendcbx.Text = "AutoSend";
            this.autoSendcbx.UseVisualStyleBackColor = true;
            // 
            // dvbTstZoneTab
            // 
            this.dvbTstZoneTab.Controls.Add(this.dvbTstPktTxPage);
            this.dvbTstZoneTab.Controls.Add(this.dvbTstContiTxPage);
            this.dvbTstZoneTab.Controls.Add(this.dvbTstPktRxPage);
            this.dvbTstZoneTab.Controls.Add(this.dvbTstStatePage);
            this.dvbTstZoneTab.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.dvbTstZoneTab.ItemSize = new System.Drawing.Size(100, 24);
            this.dvbTstZoneTab.Location = new System.Drawing.Point(10, 345);
            this.dvbTstZoneTab.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstZoneTab.Name = "dvbTstZoneTab";
            this.dvbTstZoneTab.SelectedIndex = 0;
            this.dvbTstZoneTab.Size = new System.Drawing.Size(868, 360);
            this.dvbTstZoneTab.TabIndex = 40;
            // 
            // dvbTstPktTxPage
            // 
            this.dvbTstPktTxPage.BackColor = System.Drawing.Color.White;
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktFifoDgmHelpBtn);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxDgmSyncLenLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxDgmSyncLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxDgmCrcLenLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxDgmMacRangeLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxDgmFifoRangeLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxDgmCrcLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxDgmPayloadLenLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxDgmPayloadLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxDgmSeqNumLenLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxDgmSeqNumLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxDgmLengthLenLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxDgmLengthLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxDgmPbLenLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxDgmPbLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxCntGroup);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxContentGroup);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxStopBtn);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxFilledLenText);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxFilledLenDescLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxStartBtn);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxPayloadText);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxMaxPayloadSeqLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxMaxPayloadNoSeqLabel);
            this.dvbTstPktTxPage.Controls.Add(this.dvbTstPktTxTestBtn);
            this.dvbTstPktTxPage.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.dvbTstPktTxPage.Location = new System.Drawing.Point(4, 28);
            this.dvbTstPktTxPage.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktTxPage.Name = "dvbTstPktTxPage";
            this.dvbTstPktTxPage.Padding = new System.Windows.Forms.Padding(2);
            this.dvbTstPktTxPage.Size = new System.Drawing.Size(860, 328);
            this.dvbTstPktTxPage.TabIndex = 0;
            this.dvbTstPktTxPage.Text = "PACKET TX";
            // 
            // dvbTstPktFifoDgmHelpBtn
            // 
            this.dvbTstPktFifoDgmHelpBtn.Location = new System.Drawing.Point(768, 38);
            this.dvbTstPktFifoDgmHelpBtn.Name = "dvbTstPktFifoDgmHelpBtn";
            this.dvbTstPktFifoDgmHelpBtn.Size = new System.Drawing.Size(28, 23);
            this.dvbTstPktFifoDgmHelpBtn.TabIndex = 54;
            this.dvbTstPktFifoDgmHelpBtn.Text = "?";
            this.dvbTstPktFifoDgmHelpBtn.UseVisualStyleBackColor = true;
            this.dvbTstPktFifoDgmHelpBtn.Click += new System.EventHandler(this.dvbTstPktFifoDgmHelpBtn_Click);
            // 
            // dvbTstPktTxDgmSyncLenLabel
            // 
            this.dvbTstPktTxDgmSyncLenLabel.ForeColor = System.Drawing.Color.Gray;
            this.dvbTstPktTxDgmSyncLenLabel.Location = new System.Drawing.Point(192, 69);
            this.dvbTstPktTxDgmSyncLenLabel.Name = "dvbTstPktTxDgmSyncLenLabel";
            this.dvbTstPktTxDgmSyncLenLabel.Size = new System.Drawing.Size(111, 17);
            this.dvbTstPktTxDgmSyncLenLabel.TabIndex = 53;
            this.dvbTstPktTxDgmSyncLenLabel.Text = "4Bytes";
            this.dvbTstPktTxDgmSyncLenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dvbTstPktTxDgmSyncLabel
            // 
            this.dvbTstPktTxDgmSyncLabel.BackColor = System.Drawing.Color.Silver;
            this.dvbTstPktTxDgmSyncLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dvbTstPktTxDgmSyncLabel.Location = new System.Drawing.Point(192, 36);
            this.dvbTstPktTxDgmSyncLabel.Name = "dvbTstPktTxDgmSyncLabel";
            this.dvbTstPktTxDgmSyncLabel.Size = new System.Drawing.Size(111, 27);
            this.dvbTstPktTxDgmSyncLabel.TabIndex = 53;
            this.dvbTstPktTxDgmSyncLabel.Text = "Syncword";
            this.dvbTstPktTxDgmSyncLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dvbTstPktTxDgmCrcLenLabel
            // 
            this.dvbTstPktTxDgmCrcLenLabel.ForeColor = System.Drawing.Color.Gray;
            this.dvbTstPktTxDgmCrcLenLabel.Location = new System.Drawing.Point(652, 69);
            this.dvbTstPktTxDgmCrcLenLabel.Name = "dvbTstPktTxDgmCrcLenLabel";
            this.dvbTstPktTxDgmCrcLenLabel.Size = new System.Drawing.Size(111, 17);
            this.dvbTstPktTxDgmCrcLenLabel.TabIndex = 53;
            this.dvbTstPktTxDgmCrcLenLabel.Text = "2Bytes";
            this.dvbTstPktTxDgmCrcLenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dvbTstPktTxDgmMacRangeLabel
            // 
            this.dvbTstPktTxDgmMacRangeLabel.BackColor = System.Drawing.Color.DarkGray;
            this.dvbTstPktTxDgmMacRangeLabel.Location = new System.Drawing.Point(77, 9);
            this.dvbTstPktTxDgmMacRangeLabel.Name = "dvbTstPktTxDgmMacRangeLabel";
            this.dvbTstPktTxDgmMacRangeLabel.Size = new System.Drawing.Size(226, 27);
            this.dvbTstPktTxDgmMacRangeLabel.TabIndex = 53;
            this.dvbTstPktTxDgmMacRangeLabel.Text = "Generated by MAC";
            this.dvbTstPktTxDgmMacRangeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dvbTstPktTxDgmFifoRangeLabel
            // 
            this.dvbTstPktTxDgmFifoRangeLabel.BackColor = System.Drawing.Color.PowderBlue;
            this.dvbTstPktTxDgmFifoRangeLabel.Location = new System.Drawing.Point(307, 9);
            this.dvbTstPktTxDgmFifoRangeLabel.Name = "dvbTstPktTxDgmFifoRangeLabel";
            this.dvbTstPktTxDgmFifoRangeLabel.Size = new System.Drawing.Size(456, 27);
            this.dvbTstPktTxDgmFifoRangeLabel.TabIndex = 53;
            this.dvbTstPktTxDgmFifoRangeLabel.Text = "FIFO Data (Max 128 Bytes)";
            this.dvbTstPktTxDgmFifoRangeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dvbTstPktTxDgmCrcLabel
            // 
            this.dvbTstPktTxDgmCrcLabel.BackColor = System.Drawing.Color.LightBlue;
            this.dvbTstPktTxDgmCrcLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dvbTstPktTxDgmCrcLabel.Location = new System.Drawing.Point(652, 36);
            this.dvbTstPktTxDgmCrcLabel.Name = "dvbTstPktTxDgmCrcLabel";
            this.dvbTstPktTxDgmCrcLabel.Size = new System.Drawing.Size(111, 27);
            this.dvbTstPktTxDgmCrcLabel.TabIndex = 53;
            this.dvbTstPktTxDgmCrcLabel.Text = "CRC";
            this.dvbTstPktTxDgmCrcLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dvbTstPktTxDgmPayloadLenLabel
            // 
            this.dvbTstPktTxDgmPayloadLenLabel.ForeColor = System.Drawing.Color.Gray;
            this.dvbTstPktTxDgmPayloadLenLabel.Location = new System.Drawing.Point(537, 69);
            this.dvbTstPktTxDgmPayloadLenLabel.Name = "dvbTstPktTxDgmPayloadLenLabel";
            this.dvbTstPktTxDgmPayloadLenLabel.Size = new System.Drawing.Size(111, 17);
            this.dvbTstPktTxDgmPayloadLenLabel.TabIndex = 53;
            this.dvbTstPktTxDgmPayloadLenLabel.Text = "Max 125 Bytes";
            this.dvbTstPktTxDgmPayloadLenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dvbTstPktTxDgmPayloadLabel
            // 
            this.dvbTstPktTxDgmPayloadLabel.BackColor = System.Drawing.Color.Black;
            this.dvbTstPktTxDgmPayloadLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dvbTstPktTxDgmPayloadLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.dvbTstPktTxDgmPayloadLabel.ForeColor = System.Drawing.Color.Yellow;
            this.dvbTstPktTxDgmPayloadLabel.Location = new System.Drawing.Point(537, 36);
            this.dvbTstPktTxDgmPayloadLabel.Name = "dvbTstPktTxDgmPayloadLabel";
            this.dvbTstPktTxDgmPayloadLabel.Size = new System.Drawing.Size(111, 27);
            this.dvbTstPktTxDgmPayloadLabel.TabIndex = 53;
            this.dvbTstPktTxDgmPayloadLabel.Text = "Payload";
            this.dvbTstPktTxDgmPayloadLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dvbTstPktTxDgmSeqNumLenLabel
            // 
            this.dvbTstPktTxDgmSeqNumLenLabel.ForeColor = System.Drawing.Color.Gray;
            this.dvbTstPktTxDgmSeqNumLenLabel.Location = new System.Drawing.Point(422, 69);
            this.dvbTstPktTxDgmSeqNumLenLabel.Name = "dvbTstPktTxDgmSeqNumLenLabel";
            this.dvbTstPktTxDgmSeqNumLenLabel.Size = new System.Drawing.Size(111, 17);
            this.dvbTstPktTxDgmSeqNumLenLabel.TabIndex = 53;
            this.dvbTstPktTxDgmSeqNumLenLabel.Text = "2Bytes";
            this.dvbTstPktTxDgmSeqNumLenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dvbTstPktTxDgmSeqNumLabel
            // 
            this.dvbTstPktTxDgmSeqNumLabel.BackColor = System.Drawing.Color.LightBlue;
            this.dvbTstPktTxDgmSeqNumLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dvbTstPktTxDgmSeqNumLabel.Location = new System.Drawing.Point(422, 36);
            this.dvbTstPktTxDgmSeqNumLabel.Name = "dvbTstPktTxDgmSeqNumLabel";
            this.dvbTstPktTxDgmSeqNumLabel.Size = new System.Drawing.Size(111, 27);
            this.dvbTstPktTxDgmSeqNumLabel.TabIndex = 53;
            this.dvbTstPktTxDgmSeqNumLabel.Text = "Seq Number";
            this.dvbTstPktTxDgmSeqNumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dvbTstPktTxDgmLengthLenLabel
            // 
            this.dvbTstPktTxDgmLengthLenLabel.ForeColor = System.Drawing.Color.Gray;
            this.dvbTstPktTxDgmLengthLenLabel.Location = new System.Drawing.Point(307, 69);
            this.dvbTstPktTxDgmLengthLenLabel.Name = "dvbTstPktTxDgmLengthLenLabel";
            this.dvbTstPktTxDgmLengthLenLabel.Size = new System.Drawing.Size(111, 17);
            this.dvbTstPktTxDgmLengthLenLabel.TabIndex = 53;
            this.dvbTstPktTxDgmLengthLenLabel.Text = "1Bytes";
            this.dvbTstPktTxDgmLengthLenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dvbTstPktTxDgmLengthLabel
            // 
            this.dvbTstPktTxDgmLengthLabel.BackColor = System.Drawing.Color.LightBlue;
            this.dvbTstPktTxDgmLengthLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dvbTstPktTxDgmLengthLabel.Location = new System.Drawing.Point(307, 36);
            this.dvbTstPktTxDgmLengthLabel.Name = "dvbTstPktTxDgmLengthLabel";
            this.dvbTstPktTxDgmLengthLabel.Size = new System.Drawing.Size(111, 27);
            this.dvbTstPktTxDgmLengthLabel.TabIndex = 53;
            this.dvbTstPktTxDgmLengthLabel.Text = "Length";
            this.dvbTstPktTxDgmLengthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dvbTstPktTxDgmPbLenLabel
            // 
            this.dvbTstPktTxDgmPbLenLabel.ForeColor = System.Drawing.Color.Gray;
            this.dvbTstPktTxDgmPbLenLabel.Location = new System.Drawing.Point(77, 69);
            this.dvbTstPktTxDgmPbLenLabel.Name = "dvbTstPktTxDgmPbLenLabel";
            this.dvbTstPktTxDgmPbLenLabel.Size = new System.Drawing.Size(111, 17);
            this.dvbTstPktTxDgmPbLenLabel.TabIndex = 53;
            this.dvbTstPktTxDgmPbLenLabel.Text = "4Bytes";
            this.dvbTstPktTxDgmPbLenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dvbTstPktTxDgmPbLabel
            // 
            this.dvbTstPktTxDgmPbLabel.BackColor = System.Drawing.Color.Silver;
            this.dvbTstPktTxDgmPbLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dvbTstPktTxDgmPbLabel.Location = new System.Drawing.Point(77, 36);
            this.dvbTstPktTxDgmPbLabel.Name = "dvbTstPktTxDgmPbLabel";
            this.dvbTstPktTxDgmPbLabel.Size = new System.Drawing.Size(111, 27);
            this.dvbTstPktTxDgmPbLabel.TabIndex = 53;
            this.dvbTstPktTxDgmPbLabel.Text = "Preamble";
            this.dvbTstPktTxDgmPbLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dvbTstPktTxCntGroup
            // 
            this.dvbTstPktTxCntGroup.Controls.Add(this.dvbTstPktTxCntIntvUnitLabel);
            this.dvbTstPktTxCntGroup.Controls.Add(this.dvbTstPktTxCntUnitLabel);
            this.dvbTstPktTxCntGroup.Controls.Add(this.dvbTstPktTxCntIntvText);
            this.dvbTstPktTxCntGroup.Controls.Add(this.dvbTstPktTxCntText);
            this.dvbTstPktTxCntGroup.Controls.Add(this.dvbTstPktTxInfiniteCheck);
            this.dvbTstPktTxCntGroup.Location = new System.Drawing.Point(545, 220);
            this.dvbTstPktTxCntGroup.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktTxCntGroup.Name = "dvbTstPktTxCntGroup";
            this.dvbTstPktTxCntGroup.Padding = new System.Windows.Forms.Padding(2);
            this.dvbTstPktTxCntGroup.Size = new System.Drawing.Size(298, 61);
            this.dvbTstPktTxCntGroup.TabIndex = 51;
            this.dvbTstPktTxCntGroup.TabStop = false;
            this.dvbTstPktTxCntGroup.Text = "Target Packet Count";
            // 
            // dvbTstPktTxCntIntvUnitLabel
            // 
            this.dvbTstPktTxCntIntvUnitLabel.AutoSize = true;
            this.dvbTstPktTxCntIntvUnitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.dvbTstPktTxCntIntvUnitLabel.Location = new System.Drawing.Point(205, 31);
            this.dvbTstPktTxCntIntvUnitLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dvbTstPktTxCntIntvUnitLabel.Name = "dvbTstPktTxCntIntvUnitLabel";
            this.dvbTstPktTxCntIntvUnitLabel.Size = new System.Drawing.Size(74, 17);
            this.dvbTstPktTxCntIntvUnitLabel.TabIndex = 14;
            this.dvbTstPktTxCntIntvUnitLabel.Text = "ms per pkt";
            // 
            // dvbTstPktTxCntUnitLabel
            // 
            this.dvbTstPktTxCntUnitLabel.AutoSize = true;
            this.dvbTstPktTxCntUnitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.dvbTstPktTxCntUnitLabel.Location = new System.Drawing.Point(70, 31);
            this.dvbTstPktTxCntUnitLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dvbTstPktTxCntUnitLabel.Name = "dvbTstPktTxCntUnitLabel";
            this.dvbTstPktTxCntUnitLabel.Size = new System.Drawing.Size(77, 17);
            this.dvbTstPktTxCntUnitLabel.TabIndex = 14;
            this.dvbTstPktTxCntUnitLabel.Text = "Pkt(s), and";
            // 
            // dvbTstPktTxCntIntvText
            // 
            this.dvbTstPktTxCntIntvText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dvbTstPktTxCntIntvText.Location = new System.Drawing.Point(153, 28);
            this.dvbTstPktTxCntIntvText.Margin = new System.Windows.Forms.Padding(4);
            this.dvbTstPktTxCntIntvText.Name = "dvbTstPktTxCntIntvText";
            this.dvbTstPktTxCntIntvText.Size = new System.Drawing.Size(47, 23);
            this.dvbTstPktTxCntIntvText.TabIndex = 15;
            this.dvbTstPktTxCntIntvText.Text = "50";
            // 
            // dvbTstPktTxCntText
            // 
            this.dvbTstPktTxCntText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dvbTstPktTxCntText.Location = new System.Drawing.Point(17, 28);
            this.dvbTstPktTxCntText.Margin = new System.Windows.Forms.Padding(4);
            this.dvbTstPktTxCntText.Name = "dvbTstPktTxCntText";
            this.dvbTstPktTxCntText.Size = new System.Drawing.Size(47, 23);
            this.dvbTstPktTxCntText.TabIndex = 15;
            this.dvbTstPktTxCntText.Text = "5";
            // 
            // dvbTstPktTxInfiniteCheck
            // 
            this.dvbTstPktTxInfiniteCheck.AutoSize = true;
            this.dvbTstPktTxInfiniteCheck.Location = new System.Drawing.Point(147, 0);
            this.dvbTstPktTxInfiniteCheck.Margin = new System.Windows.Forms.Padding(4);
            this.dvbTstPktTxInfiniteCheck.Name = "dvbTstPktTxInfiniteCheck";
            this.dvbTstPktTxInfiniteCheck.Size = new System.Drawing.Size(71, 21);
            this.dvbTstPktTxInfiniteCheck.TabIndex = 20;
            this.dvbTstPktTxInfiniteCheck.Text = "Infinite";
            this.dvbTstPktTxInfiniteCheck.UseVisualStyleBackColor = true;
            this.dvbTstPktTxInfiniteCheck.CheckedChanged += new System.EventHandler(this.dvbTstPktTxInfiniteCheck_CheckedChanged);
            // 
            // dvbTstPktTxContentGroup
            // 
            this.dvbTstPktTxContentGroup.Controls.Add(this.dvbTstPktTxCrcCalcBtn);
            this.dvbTstPktTxContentGroup.Controls.Add(this.dvbTstPktTxRandomPlLenUnitLabel);
            this.dvbTstPktTxContentGroup.Controls.Add(this.dvbTstPktTxCrcPolyLabel);
            this.dvbTstPktTxContentGroup.Controls.Add(this.dvbTstPktTxRandomPlCheck);
            this.dvbTstPktTxContentGroup.Controls.Add(this.dvbTstPktTxRandomPlLenText);
            this.dvbTstPktTxContentGroup.Controls.Add(this.dvbTstPktTxCrcCombo);
            this.dvbTstPktTxContentGroup.Controls.Add(this.dvbTstPktTxPrependSeqCheck);
            this.dvbTstPktTxContentGroup.Location = new System.Drawing.Point(545, 99);
            this.dvbTstPktTxContentGroup.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktTxContentGroup.Name = "dvbTstPktTxContentGroup";
            this.dvbTstPktTxContentGroup.Padding = new System.Windows.Forms.Padding(2);
            this.dvbTstPktTxContentGroup.Size = new System.Drawing.Size(298, 120);
            this.dvbTstPktTxContentGroup.TabIndex = 52;
            this.dvbTstPktTxContentGroup.TabStop = false;
            this.dvbTstPktTxContentGroup.Text = "Packet Content";
            // 
            // dvbTstPktTxCrcCalcBtn
            // 
            this.dvbTstPktTxCrcCalcBtn.Location = new System.Drawing.Point(220, 16);
            this.dvbTstPktTxCrcCalcBtn.Name = "dvbTstPktTxCrcCalcBtn";
            this.dvbTstPktTxCrcCalcBtn.Size = new System.Drawing.Size(45, 31);
            this.dvbTstPktTxCrcCalcBtn.TabIndex = 45;
            this.dvbTstPktTxCrcCalcBtn.Text = "Calc";
            this.dvbTstPktTxCrcCalcBtn.UseVisualStyleBackColor = true;
            this.dvbTstPktTxCrcCalcBtn.Click += new System.EventHandler(this.dvbTstPktTxCrcCalcBtn_Click);
            // 
            // dvbTstPktTxRandomPlLenUnitLabel
            // 
            this.dvbTstPktTxRandomPlLenUnitLabel.AutoSize = true;
            this.dvbTstPktTxRandomPlLenUnitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.dvbTstPktTxRandomPlLenUnitLabel.Location = new System.Drawing.Point(211, 85);
            this.dvbTstPktTxRandomPlLenUnitLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dvbTstPktTxRandomPlLenUnitLabel.Name = "dvbTstPktTxRandomPlLenUnitLabel";
            this.dvbTstPktTxRandomPlLenUnitLabel.Size = new System.Drawing.Size(43, 17);
            this.dvbTstPktTxRandomPlLenUnitLabel.TabIndex = 13;
            this.dvbTstPktTxRandomPlLenUnitLabel.Text = "Bytes";
            // 
            // dvbTstPktTxCrcPolyLabel
            // 
            this.dvbTstPktTxCrcPolyLabel.AutoSize = true;
            this.dvbTstPktTxCrcPolyLabel.Location = new System.Drawing.Point(14, 23);
            this.dvbTstPktTxCrcPolyLabel.Name = "dvbTstPktTxCrcPolyLabel";
            this.dvbTstPktTxCrcPolyLabel.Size = new System.Drawing.Size(108, 17);
            this.dvbTstPktTxCrcPolyLabel.TabIndex = 44;
            this.dvbTstPktTxCrcPolyLabel.Text = "CRC Polynomial";
            // 
            // dvbTstPktTxRandomPlCheck
            // 
            this.dvbTstPktTxRandomPlCheck.AutoSize = true;
            this.dvbTstPktTxRandomPlCheck.Location = new System.Drawing.Point(17, 83);
            this.dvbTstPktTxRandomPlCheck.Margin = new System.Windows.Forms.Padding(4);
            this.dvbTstPktTxRandomPlCheck.Name = "dvbTstPktTxRandomPlCheck";
            this.dvbTstPktTxRandomPlCheck.Size = new System.Drawing.Size(138, 21);
            this.dvbTstPktTxRandomPlCheck.TabIndex = 23;
            this.dvbTstPktTxRandomPlCheck.Text = "Random Payload";
            this.dvbTstPktTxRandomPlCheck.UseVisualStyleBackColor = true;
            this.dvbTstPktTxRandomPlCheck.CheckedChanged += new System.EventHandler(this.dvbTstPktTxRandomPlCheck_CheckedChanged);
            // 
            // dvbTstPktTxRandomPlLenText
            // 
            this.dvbTstPktTxRandomPlLenText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dvbTstPktTxRandomPlLenText.Enabled = false;
            this.dvbTstPktTxRandomPlLenText.Location = new System.Drawing.Point(163, 82);
            this.dvbTstPktTxRandomPlLenText.Margin = new System.Windows.Forms.Padding(4);
            this.dvbTstPktTxRandomPlLenText.Name = "dvbTstPktTxRandomPlLenText";
            this.dvbTstPktTxRandomPlLenText.Size = new System.Drawing.Size(36, 23);
            this.dvbTstPktTxRandomPlLenText.TabIndex = 0;
            this.dvbTstPktTxRandomPlLenText.Text = "5";
            this.dvbTstPktTxRandomPlLenText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dvbTstPktTxRandomPlLenText_KeyDown);
            this.dvbTstPktTxRandomPlLenText.Leave += new System.EventHandler(this.dvbTstPktTxRandomPlLenText_Leave);
            // 
            // dvbTstPktTxCrcCombo
            // 
            this.dvbTstPktTxCrcCombo.FormattingEnabled = true;
            this.dvbTstPktTxCrcCombo.Location = new System.Drawing.Point(127, 20);
            this.dvbTstPktTxCrcCombo.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktTxCrcCombo.Name = "dvbTstPktTxCrcCombo";
            this.dvbTstPktTxCrcCombo.Size = new System.Drawing.Size(88, 24);
            this.dvbTstPktTxCrcCombo.TabIndex = 0;
            this.dvbTstPktTxCrcCombo.SelectedIndexChanged += new System.EventHandler(this.dvbTstPktTxCrcCombo_SelectedIndexChanged);
            // 
            // dvbTstPktTxPrependSeqCheck
            // 
            this.dvbTstPktTxPrependSeqCheck.Location = new System.Drawing.Point(17, 44);
            this.dvbTstPktTxPrependSeqCheck.Margin = new System.Windows.Forms.Padding(4);
            this.dvbTstPktTxPrependSeqCheck.Name = "dvbTstPktTxPrependSeqCheck";
            this.dvbTstPktTxPrependSeqCheck.Size = new System.Drawing.Size(235, 40);
            this.dvbTstPktTxPrependSeqCheck.TabIndex = 26;
            this.dvbTstPktTxPrependSeqCheck.Text = "Prepend Seq Num (+2 bytes)";
            this.dvbTstPktTxPrependSeqCheck.UseVisualStyleBackColor = true;
            this.dvbTstPktTxPrependSeqCheck.CheckedChanged += new System.EventHandler(this.dvbTstPktTxPrependSeqCheck_CheckedChanged);
            // 
            // dvbTstPktTxStopBtn
            // 
            this.dvbTstPktTxStopBtn.Enabled = false;
            this.dvbTstPktTxStopBtn.Location = new System.Drawing.Point(753, 287);
            this.dvbTstPktTxStopBtn.Margin = new System.Windows.Forms.Padding(4);
            this.dvbTstPktTxStopBtn.Name = "dvbTstPktTxStopBtn";
            this.dvbTstPktTxStopBtn.Size = new System.Drawing.Size(90, 31);
            this.dvbTstPktTxStopBtn.TabIndex = 48;
            this.dvbTstPktTxStopBtn.Text = "Stop";
            this.dvbTstPktTxStopBtn.UseVisualStyleBackColor = true;
            this.dvbTstPktTxStopBtn.Click += new System.EventHandler(this.dvbTstPktTxStopBtn_Click);
            // 
            // dvbTstPktTxFilledLenText
            // 
            this.dvbTstPktTxFilledLenText.BackColor = System.Drawing.Color.Gainsboro;
            this.dvbTstPktTxFilledLenText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dvbTstPktTxFilledLenText.Enabled = false;
            this.dvbTstPktTxFilledLenText.Location = new System.Drawing.Point(22, 100);
            this.dvbTstPktTxFilledLenText.Margin = new System.Windows.Forms.Padding(4);
            this.dvbTstPktTxFilledLenText.Name = "dvbTstPktTxFilledLenText";
            this.dvbTstPktTxFilledLenText.ReadOnly = true;
            this.dvbTstPktTxFilledLenText.Size = new System.Drawing.Size(55, 23);
            this.dvbTstPktTxFilledLenText.TabIndex = 50;
            this.dvbTstPktTxFilledLenText.Text = "0";
            // 
            // dvbTstPktTxFilledLenDescLabel
            // 
            this.dvbTstPktTxFilledLenDescLabel.AutoSize = true;
            this.dvbTstPktTxFilledLenDescLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.dvbTstPktTxFilledLenDescLabel.Location = new System.Drawing.Point(89, 102);
            this.dvbTstPktTxFilledLenDescLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dvbTstPktTxFilledLenDescLabel.Name = "dvbTstPktTxFilledLenDescLabel";
            this.dvbTstPktTxFilledLenDescLabel.Size = new System.Drawing.Size(301, 17);
            this.dvbTstPktTxFilledLenDescLabel.TabIndex = 49;
            this.dvbTstPktTxFilledLenDescLabel.Text = "-Bytes payload has been filled in following box.";
            // 
            // dvbTstPktTxStartBtn
            // 
            this.dvbTstPktTxStartBtn.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dvbTstPktTxStartBtn.Location = new System.Drawing.Point(655, 287);
            this.dvbTstPktTxStartBtn.Margin = new System.Windows.Forms.Padding(4);
            this.dvbTstPktTxStartBtn.Name = "dvbTstPktTxStartBtn";
            this.dvbTstPktTxStartBtn.Size = new System.Drawing.Size(90, 31);
            this.dvbTstPktTxStartBtn.TabIndex = 47;
            this.dvbTstPktTxStartBtn.Text = "Start";
            this.dvbTstPktTxStartBtn.UseVisualStyleBackColor = false;
            this.dvbTstPktTxStartBtn.Click += new System.EventHandler(this.dvbTstPktTxStartBtn_Click);
            // 
            // dvbTstPktTxPayloadText
            // 
            this.dvbTstPktTxPayloadText.BackColor = System.Drawing.Color.Black;
            this.dvbTstPktTxPayloadText.Font = new System.Drawing.Font("Consolas", 9F);
            this.dvbTstPktTxPayloadText.ForeColor = System.Drawing.Color.Yellow;
            this.dvbTstPktTxPayloadText.Location = new System.Drawing.Point(18, 131);
            this.dvbTstPktTxPayloadText.Margin = new System.Windows.Forms.Padding(4);
            this.dvbTstPktTxPayloadText.Multiline = true;
            this.dvbTstPktTxPayloadText.Name = "dvbTstPktTxPayloadText";
            this.dvbTstPktTxPayloadText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dvbTstPktTxPayloadText.Size = new System.Drawing.Size(521, 151);
            this.dvbTstPktTxPayloadText.TabIndex = 46;
            this.dvbTstPktTxPayloadText.TextChanged += new System.EventHandler(this.dvbTstPktTxPayloadText_TextChanged);
            // 
            // dvbTstPktTxMaxPayloadSeqLabel
            // 
            this.dvbTstPktTxMaxPayloadSeqLabel.AutoSize = true;
            this.dvbTstPktTxMaxPayloadSeqLabel.Location = new System.Drawing.Point(225, 301);
            this.dvbTstPktTxMaxPayloadSeqLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dvbTstPktTxMaxPayloadSeqLabel.Name = "dvbTstPktTxMaxPayloadSeqLabel";
            this.dvbTstPktTxMaxPayloadSeqLabel.Size = new System.Drawing.Size(195, 17);
            this.dvbTstPktTxMaxPayloadSeqLabel.TabIndex = 45;
            this.dvbTstPktTxMaxPayloadSeqLabel.Text = "Max Payload (with Seq) = 123";
            this.dvbTstPktTxMaxPayloadSeqLabel.Visible = false;
            // 
            // dvbTstPktTxMaxPayloadNoSeqLabel
            // 
            this.dvbTstPktTxMaxPayloadNoSeqLabel.AutoSize = true;
            this.dvbTstPktTxMaxPayloadNoSeqLabel.Location = new System.Drawing.Point(15, 301);
            this.dvbTstPktTxMaxPayloadNoSeqLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dvbTstPktTxMaxPayloadNoSeqLabel.Name = "dvbTstPktTxMaxPayloadNoSeqLabel";
            this.dvbTstPktTxMaxPayloadNoSeqLabel.Size = new System.Drawing.Size(187, 17);
            this.dvbTstPktTxMaxPayloadNoSeqLabel.TabIndex = 44;
            this.dvbTstPktTxMaxPayloadNoSeqLabel.Text = "Max Payload (no Seq) = 125";
            this.dvbTstPktTxMaxPayloadNoSeqLabel.Visible = false;
            // 
            // dvbTstPktTxTestBtn
            // 
            this.dvbTstPktTxTestBtn.Location = new System.Drawing.Point(553, 287);
            this.dvbTstPktTxTestBtn.Margin = new System.Windows.Forms.Padding(4);
            this.dvbTstPktTxTestBtn.Name = "dvbTstPktTxTestBtn";
            this.dvbTstPktTxTestBtn.Size = new System.Drawing.Size(90, 31);
            this.dvbTstPktTxTestBtn.TabIndex = 19;
            this.dvbTstPktTxTestBtn.Text = "Crc";
            this.dvbTstPktTxTestBtn.UseVisualStyleBackColor = true;
            this.dvbTstPktTxTestBtn.Visible = false;
            this.dvbTstPktTxTestBtn.Click += new System.EventHandler(this.dvbTstPktTxTestBtn_Click);
            // 
            // dvbTstContiTxPage
            // 
            this.dvbTstContiTxPage.AutoScroll = true;
            this.dvbTstContiTxPage.BackColor = System.Drawing.Color.Transparent;
            this.dvbTstContiTxPage.Controls.Add(this.dvbTstContiTxStopBtn);
            this.dvbTstContiTxPage.Controls.Add(this.dvbTstContiTxPatternGroup);
            this.dvbTstContiTxPage.Controls.Add(this.dvbTstContiTxStartBtn);
            this.dvbTstContiTxPage.Controls.Add(this.dvbTstContiTxModeGroup);
            this.dvbTstContiTxPage.Location = new System.Drawing.Point(4, 28);
            this.dvbTstContiTxPage.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstContiTxPage.Name = "dvbTstContiTxPage";
            this.dvbTstContiTxPage.Padding = new System.Windows.Forms.Padding(2);
            this.dvbTstContiTxPage.Size = new System.Drawing.Size(860, 328);
            this.dvbTstContiTxPage.TabIndex = 1;
            this.dvbTstContiTxPage.Text = "CONTINUOUS TX";
            // 
            // dvbTstContiTxStopBtn
            // 
            this.dvbTstContiTxStopBtn.BackColor = System.Drawing.Color.White;
            this.dvbTstContiTxStopBtn.Location = new System.Drawing.Point(320, 65);
            this.dvbTstContiTxStopBtn.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstContiTxStopBtn.Name = "dvbTstContiTxStopBtn";
            this.dvbTstContiTxStopBtn.Size = new System.Drawing.Size(96, 35);
            this.dvbTstContiTxStopBtn.TabIndex = 3;
            this.dvbTstContiTxStopBtn.Text = "Stop";
            this.dvbTstContiTxStopBtn.UseVisualStyleBackColor = false;
            this.dvbTstContiTxStopBtn.Click += new System.EventHandler(this.dvbTstContiTxStopBtn_Click);
            // 
            // dvbTstContiTxPatternGroup
            // 
            this.dvbTstContiTxPatternGroup.Controls.Add(this.dvbTstContiTxPrbs9RadioBtn);
            this.dvbTstContiTxPatternGroup.Controls.Add(this.dvbTstContiTxBit0101RadioBtn);
            this.dvbTstContiTxPatternGroup.Location = new System.Drawing.Point(170, 17);
            this.dvbTstContiTxPatternGroup.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstContiTxPatternGroup.Name = "dvbTstContiTxPatternGroup";
            this.dvbTstContiTxPatternGroup.Padding = new System.Windows.Forms.Padding(2);
            this.dvbTstContiTxPatternGroup.Size = new System.Drawing.Size(139, 104);
            this.dvbTstContiTxPatternGroup.TabIndex = 5;
            this.dvbTstContiTxPatternGroup.TabStop = false;
            this.dvbTstContiTxPatternGroup.Text = "Pattern";
            // 
            // dvbTstContiTxPrbs9RadioBtn
            // 
            this.dvbTstContiTxPrbs9RadioBtn.AutoSize = true;
            this.dvbTstContiTxPrbs9RadioBtn.Location = new System.Drawing.Point(12, 66);
            this.dvbTstContiTxPrbs9RadioBtn.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstContiTxPrbs9RadioBtn.Name = "dvbTstContiTxPrbs9RadioBtn";
            this.dvbTstContiTxPrbs9RadioBtn.Size = new System.Drawing.Size(74, 21);
            this.dvbTstContiTxPrbs9RadioBtn.TabIndex = 1;
            this.dvbTstContiTxPrbs9RadioBtn.Text = "PRBS9";
            this.dvbTstContiTxPrbs9RadioBtn.UseVisualStyleBackColor = true;
            // 
            // dvbTstContiTxBit0101RadioBtn
            // 
            this.dvbTstContiTxBit0101RadioBtn.AutoSize = true;
            this.dvbTstContiTxBit0101RadioBtn.Checked = true;
            this.dvbTstContiTxBit0101RadioBtn.Location = new System.Drawing.Point(12, 35);
            this.dvbTstContiTxBit0101RadioBtn.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstContiTxBit0101RadioBtn.Name = "dvbTstContiTxBit0101RadioBtn";
            this.dvbTstContiTxBit0101RadioBtn.Size = new System.Drawing.Size(117, 21);
            this.dvbTstContiTxBit0101RadioBtn.TabIndex = 0;
            this.dvbTstContiTxBit0101RadioBtn.TabStop = true;
            this.dvbTstContiTxBit0101RadioBtn.Text = "Bit_01010101";
            this.dvbTstContiTxBit0101RadioBtn.UseVisualStyleBackColor = true;
            // 
            // dvbTstContiTxStartBtn
            // 
            this.dvbTstContiTxStartBtn.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dvbTstContiTxStartBtn.Location = new System.Drawing.Point(320, 26);
            this.dvbTstContiTxStartBtn.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstContiTxStartBtn.Name = "dvbTstContiTxStartBtn";
            this.dvbTstContiTxStartBtn.Size = new System.Drawing.Size(96, 35);
            this.dvbTstContiTxStartBtn.TabIndex = 2;
            this.dvbTstContiTxStartBtn.Text = "Start";
            this.dvbTstContiTxStartBtn.UseVisualStyleBackColor = false;
            this.dvbTstContiTxStartBtn.Click += new System.EventHandler(this.dvbTstContiTxStartBtn_Click);
            // 
            // dvbTstContiTxModeGroup
            // 
            this.dvbTstContiTxModeGroup.Controls.Add(this.dvbTstContiTxPatternRadioBtn);
            this.dvbTstContiTxModeGroup.Controls.Add(this.dvbTstContiTxSingleToneRadioBtn);
            this.dvbTstContiTxModeGroup.Location = new System.Drawing.Point(18, 17);
            this.dvbTstContiTxModeGroup.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstContiTxModeGroup.Name = "dvbTstContiTxModeGroup";
            this.dvbTstContiTxModeGroup.Padding = new System.Windows.Forms.Padding(2);
            this.dvbTstContiTxModeGroup.Size = new System.Drawing.Size(139, 104);
            this.dvbTstContiTxModeGroup.TabIndex = 4;
            this.dvbTstContiTxModeGroup.TabStop = false;
            this.dvbTstContiTxModeGroup.Text = "Transfer Mode";
            // 
            // dvbTstContiTxPatternRadioBtn
            // 
            this.dvbTstContiTxPatternRadioBtn.AutoSize = true;
            this.dvbTstContiTxPatternRadioBtn.Location = new System.Drawing.Point(15, 66);
            this.dvbTstContiTxPatternRadioBtn.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstContiTxPatternRadioBtn.Name = "dvbTstContiTxPatternRadioBtn";
            this.dvbTstContiTxPatternRadioBtn.Size = new System.Drawing.Size(75, 21);
            this.dvbTstContiTxPatternRadioBtn.TabIndex = 1;
            this.dvbTstContiTxPatternRadioBtn.Text = "Pattern";
            this.dvbTstContiTxPatternRadioBtn.UseVisualStyleBackColor = true;
            this.dvbTstContiTxPatternRadioBtn.CheckedChanged += new System.EventHandler(this.dvbTstContiTxPatternRadioBtn_CheckedChanged);
            // 
            // dvbTstContiTxSingleToneRadioBtn
            // 
            this.dvbTstContiTxSingleToneRadioBtn.AutoSize = true;
            this.dvbTstContiTxSingleToneRadioBtn.Checked = true;
            this.dvbTstContiTxSingleToneRadioBtn.Location = new System.Drawing.Point(15, 35);
            this.dvbTstContiTxSingleToneRadioBtn.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstContiTxSingleToneRadioBtn.Name = "dvbTstContiTxSingleToneRadioBtn";
            this.dvbTstContiTxSingleToneRadioBtn.Size = new System.Drawing.Size(101, 21);
            this.dvbTstContiTxSingleToneRadioBtn.TabIndex = 0;
            this.dvbTstContiTxSingleToneRadioBtn.TabStop = true;
            this.dvbTstContiTxSingleToneRadioBtn.Text = "SingleTone";
            this.dvbTstContiTxSingleToneRadioBtn.UseVisualStyleBackColor = true;
            this.dvbTstContiTxSingleToneRadioBtn.CheckedChanged += new System.EventHandler(this.dvbTstContiTxSingleToneRadioBtn_CheckedChanged);
            // 
            // dvbTstPktRxPage
            // 
            this.dvbTstPktRxPage.Controls.Add(this.dvbTstPktRxRssiText);
            this.dvbTstPktRxPage.Controls.Add(this.dvbTstPktRxRssiLabel);
            this.dvbTstPktRxPage.Controls.Add(this.dvbTstPktRxClearLogBtn);
            this.dvbTstPktRxPage.Controls.Add(this.dvbTstPktRxScanBtn);
            this.dvbTstPktRxPage.Controls.Add(this.dvbTstPktRxSetPayloadBtn);
            this.dvbTstPktRxPage.Controls.Add(this.dvbTstPktRxCntGroup);
            this.dvbTstPktRxPage.Controls.Add(this.dvbTstPktRxParserGroup);
            this.dvbTstPktRxPage.Controls.Add(this.dvbTstPktRxErrCntLabel);
            this.dvbTstPktRxPage.Controls.Add(this.dvbTstPktRxErrCntText);
            this.dvbTstPktRxPage.Controls.Add(this.dvbTstPktRxLogText);
            this.dvbTstPktRxPage.Controls.Add(this.dvbTstPktRxStartBtn);
            this.dvbTstPktRxPage.Controls.Add(this.dvbTstPktRxOkCntLabel);
            this.dvbTstPktRxPage.Controls.Add(this.dvbTstPktRxOkCntText);
            this.dvbTstPktRxPage.Location = new System.Drawing.Point(4, 28);
            this.dvbTstPktRxPage.Margin = new System.Windows.Forms.Padding(4);
            this.dvbTstPktRxPage.Name = "dvbTstPktRxPage";
            this.dvbTstPktRxPage.Size = new System.Drawing.Size(860, 328);
            this.dvbTstPktRxPage.TabIndex = 2;
            this.dvbTstPktRxPage.Text = "PACKET RX";
            this.dvbTstPktRxPage.UseVisualStyleBackColor = true;
            // 
            // dvbTstPktRxRssiText
            // 
            this.dvbTstPktRxRssiText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dvbTstPktRxRssiText.ForeColor = System.Drawing.Color.MediumBlue;
            this.dvbTstPktRxRssiText.Location = new System.Drawing.Point(747, 186);
            this.dvbTstPktRxRssiText.Margin = new System.Windows.Forms.Padding(4);
            this.dvbTstPktRxRssiText.Name = "dvbTstPktRxRssiText";
            this.dvbTstPktRxRssiText.Size = new System.Drawing.Size(99, 26);
            this.dvbTstPktRxRssiText.TabIndex = 42;
            // 
            // dvbTstPktRxRssiLabel
            // 
            this.dvbTstPktRxRssiLabel.AutoSize = true;
            this.dvbTstPktRxRssiLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dvbTstPktRxRssiLabel.Location = new System.Drawing.Point(747, 162);
            this.dvbTstPktRxRssiLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dvbTstPktRxRssiLabel.Name = "dvbTstPktRxRssiLabel";
            this.dvbTstPktRxRssiLabel.Size = new System.Drawing.Size(67, 17);
            this.dvbTstPktRxRssiLabel.TabIndex = 43;
            this.dvbTstPktRxRssiLabel.Text = "Avg RSSI";
            // 
            // dvbTstPktRxClearLogBtn
            // 
            this.dvbTstPktRxClearLogBtn.Location = new System.Drawing.Point(631, 77);
            this.dvbTstPktRxClearLogBtn.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktRxClearLogBtn.Name = "dvbTstPktRxClearLogBtn";
            this.dvbTstPktRxClearLogBtn.Size = new System.Drawing.Size(99, 35);
            this.dvbTstPktRxClearLogBtn.TabIndex = 48;
            this.dvbTstPktRxClearLogBtn.Text = "Clear Log";
            this.dvbTstPktRxClearLogBtn.UseVisualStyleBackColor = true;
            this.dvbTstPktRxClearLogBtn.Click += new System.EventHandler(this.dvbTstPktRxClearLogBtn_Click);
            // 
            // dvbTstPktRxScanBtn
            // 
            this.dvbTstPktRxScanBtn.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dvbTstPktRxScanBtn.Location = new System.Drawing.Point(747, 235);
            this.dvbTstPktRxScanBtn.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktRxScanBtn.Name = "dvbTstPktRxScanBtn";
            this.dvbTstPktRxScanBtn.Size = new System.Drawing.Size(99, 35);
            this.dvbTstPktRxScanBtn.TabIndex = 47;
            this.dvbTstPktRxScanBtn.Text = "Scan RSSI";
            this.dvbTstPktRxScanBtn.UseVisualStyleBackColor = false;
            this.dvbTstPktRxScanBtn.Click += new System.EventHandler(this.btnScanRssi_Click);
            // 
            // dvbTstPktRxSetPayloadBtn
            // 
            this.dvbTstPktRxSetPayloadBtn.Location = new System.Drawing.Point(631, 36);
            this.dvbTstPktRxSetPayloadBtn.Margin = new System.Windows.Forms.Padding(4);
            this.dvbTstPktRxSetPayloadBtn.Name = "dvbTstPktRxSetPayloadBtn";
            this.dvbTstPktRxSetPayloadBtn.Size = new System.Drawing.Size(99, 35);
            this.dvbTstPktRxSetPayloadBtn.TabIndex = 40;
            this.dvbTstPktRxSetPayloadBtn.Text = "Set Payload";
            this.dvbTstPktRxSetPayloadBtn.UseVisualStyleBackColor = true;
            this.dvbTstPktRxSetPayloadBtn.Visible = false;
            this.dvbTstPktRxSetPayloadBtn.Click += new System.EventHandler(this.dvbTstPktRxSetPayloadBtn_Click);
            // 
            // dvbTstPktRxCntGroup
            // 
            this.dvbTstPktRxCntGroup.Controls.Add(this.dvbTstPktRxCntLimitRadioBtn);
            this.dvbTstPktRxCntGroup.Controls.Add(this.dvbTstPktRxCntText);
            this.dvbTstPktRxCntGroup.Controls.Add(this.dvbTstPktRxCntNoLimitRadioBtn);
            this.dvbTstPktRxCntGroup.Location = new System.Drawing.Point(12, 19);
            this.dvbTstPktRxCntGroup.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktRxCntGroup.Name = "dvbTstPktRxCntGroup";
            this.dvbTstPktRxCntGroup.Padding = new System.Windows.Forms.Padding(2);
            this.dvbTstPktRxCntGroup.Size = new System.Drawing.Size(230, 95);
            this.dvbTstPktRxCntGroup.TabIndex = 49;
            this.dvbTstPktRxCntGroup.TabStop = false;
            this.dvbTstPktRxCntGroup.Text = "Target Packet Count";
            // 
            // dvbTstPktRxCntLimitRadioBtn
            // 
            this.dvbTstPktRxCntLimitRadioBtn.AutoSize = true;
            this.dvbTstPktRxCntLimitRadioBtn.Location = new System.Drawing.Point(11, 31);
            this.dvbTstPktRxCntLimitRadioBtn.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktRxCntLimitRadioBtn.Name = "dvbTstPktRxCntLimitRadioBtn";
            this.dvbTstPktRxCntLimitRadioBtn.Size = new System.Drawing.Size(142, 21);
            this.dvbTstPktRxCntLimitRadioBtn.TabIndex = 31;
            this.dvbTstPktRxCntLimitRadioBtn.Text = "Max Packet Count";
            this.dvbTstPktRxCntLimitRadioBtn.UseVisualStyleBackColor = true;
            this.dvbTstPktRxCntLimitRadioBtn.CheckedChanged += new System.EventHandler(this.packetRxCountMode_CheckedChanged);
            // 
            // dvbTstPktRxCntText
            // 
            this.dvbTstPktRxCntText.Enabled = false;
            this.dvbTstPktRxCntText.Location = new System.Drawing.Point(157, 29);
            this.dvbTstPktRxCntText.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktRxCntText.Name = "dvbTstPktRxCntText";
            this.dvbTstPktRxCntText.Size = new System.Drawing.Size(56, 23);
            this.dvbTstPktRxCntText.TabIndex = 32;
            this.dvbTstPktRxCntText.Text = "100";
            // 
            // dvbTstPktRxCntNoLimitRadioBtn
            // 
            this.dvbTstPktRxCntNoLimitRadioBtn.AutoSize = true;
            this.dvbTstPktRxCntNoLimitRadioBtn.Checked = true;
            this.dvbTstPktRxCntNoLimitRadioBtn.Location = new System.Drawing.Point(11, 64);
            this.dvbTstPktRxCntNoLimitRadioBtn.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktRxCntNoLimitRadioBtn.Name = "dvbTstPktRxCntNoLimitRadioBtn";
            this.dvbTstPktRxCntNoLimitRadioBtn.Size = new System.Drawing.Size(106, 21);
            this.dvbTstPktRxCntNoLimitRadioBtn.TabIndex = 33;
            this.dvbTstPktRxCntNoLimitRadioBtn.TabStop = true;
            this.dvbTstPktRxCntNoLimitRadioBtn.Text = "No limitation";
            this.dvbTstPktRxCntNoLimitRadioBtn.UseVisualStyleBackColor = true;
            this.dvbTstPktRxCntNoLimitRadioBtn.CheckedChanged += new System.EventHandler(this.packetRxCountMode_CheckedChanged);
            // 
            // dvbTstPktRxParserGroup
            // 
            this.dvbTstPktRxParserGroup.Controls.Add(this.dvbTstPktRxParseSeqCheck);
            this.dvbTstPktRxParserGroup.Controls.Add(this.dvbTstPktRxCrcPolyLabel);
            this.dvbTstPktRxParserGroup.Controls.Add(this.dvbTstPktRxCrcCombo);
            this.dvbTstPktRxParserGroup.Location = new System.Drawing.Point(250, 19);
            this.dvbTstPktRxParserGroup.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktRxParserGroup.Name = "dvbTstPktRxParserGroup";
            this.dvbTstPktRxParserGroup.Padding = new System.Windows.Forms.Padding(2);
            this.dvbTstPktRxParserGroup.Size = new System.Drawing.Size(270, 95);
            this.dvbTstPktRxParserGroup.TabIndex = 50;
            this.dvbTstPktRxParserGroup.TabStop = false;
            this.dvbTstPktRxParserGroup.Text = "Packet Parser";
            // 
            // dvbTstPktRxParseSeqCheck
            // 
            this.dvbTstPktRxParseSeqCheck.AutoSize = true;
            this.dvbTstPktRxParseSeqCheck.Location = new System.Drawing.Point(15, 31);
            this.dvbTstPktRxParseSeqCheck.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktRxParseSeqCheck.Name = "dvbTstPktRxParseSeqCheck";
            this.dvbTstPktRxParseSeqCheck.Size = new System.Drawing.Size(185, 21);
            this.dvbTstPktRxParseSeqCheck.TabIndex = 28;
            this.dvbTstPktRxParseSeqCheck.Text = "Packet Has Seq Number";
            this.dvbTstPktRxParseSeqCheck.UseVisualStyleBackColor = true;
            // 
            // dvbTstPktRxCrcPolyLabel
            // 
            this.dvbTstPktRxCrcPolyLabel.AutoSize = true;
            this.dvbTstPktRxCrcPolyLabel.Location = new System.Drawing.Point(15, 66);
            this.dvbTstPktRxCrcPolyLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dvbTstPktRxCrcPolyLabel.Name = "dvbTstPktRxCrcPolyLabel";
            this.dvbTstPktRxCrcPolyLabel.Size = new System.Drawing.Size(108, 17);
            this.dvbTstPktRxCrcPolyLabel.TabIndex = 29;
            this.dvbTstPktRxCrcPolyLabel.Text = "CRC Polynomial";
            // 
            // dvbTstPktRxCrcCombo
            // 
            this.dvbTstPktRxCrcCombo.FormattingEnabled = true;
            this.dvbTstPktRxCrcCombo.Location = new System.Drawing.Point(135, 63);
            this.dvbTstPktRxCrcCombo.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktRxCrcCombo.Name = "dvbTstPktRxCrcCombo";
            this.dvbTstPktRxCrcCombo.Size = new System.Drawing.Size(109, 24);
            this.dvbTstPktRxCrcCombo.TabIndex = 30;
            // 
            // dvbTstPktRxErrCntLabel
            // 
            this.dvbTstPktRxErrCntLabel.AutoSize = true;
            this.dvbTstPktRxErrCntLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dvbTstPktRxErrCntLabel.Location = new System.Drawing.Point(747, 108);
            this.dvbTstPktRxErrCntLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dvbTstPktRxErrCntLabel.Name = "dvbTstPktRxErrCntLabel";
            this.dvbTstPktRxErrCntLabel.Size = new System.Drawing.Size(76, 17);
            this.dvbTstPktRxErrCntLabel.TabIndex = 46;
            this.dvbTstPktRxErrCntLabel.Text = "Rcvd Error";
            // 
            // dvbTstPktRxErrCntText
            // 
            this.dvbTstPktRxErrCntText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dvbTstPktRxErrCntText.Location = new System.Drawing.Point(747, 132);
            this.dvbTstPktRxErrCntText.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktRxErrCntText.Name = "dvbTstPktRxErrCntText";
            this.dvbTstPktRxErrCntText.Size = new System.Drawing.Size(99, 23);
            this.dvbTstPktRxErrCntText.TabIndex = 45;
            // 
            // dvbTstPktRxLogText
            // 
            this.dvbTstPktRxLogText.BackColor = System.Drawing.Color.Black;
            this.dvbTstPktRxLogText.Font = new System.Drawing.Font("Consolas", 9F);
            this.dvbTstPktRxLogText.ForeColor = System.Drawing.Color.Yellow;
            this.dvbTstPktRxLogText.Location = new System.Drawing.Point(12, 118);
            this.dvbTstPktRxLogText.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktRxLogText.Multiline = true;
            this.dvbTstPktRxLogText.Name = "dvbTstPktRxLogText";
            this.dvbTstPktRxLogText.ReadOnly = true;
            this.dvbTstPktRxLogText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.dvbTstPktRxLogText.Size = new System.Drawing.Size(718, 191);
            this.dvbTstPktRxLogText.TabIndex = 44;
            this.dvbTstPktRxLogText.WordWrap = false;
            // 
            // dvbTstPktRxStartBtn
            // 
            this.dvbTstPktRxStartBtn.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dvbTstPktRxStartBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.dvbTstPktRxStartBtn.Location = new System.Drawing.Point(747, 274);
            this.dvbTstPktRxStartBtn.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktRxStartBtn.Name = "dvbTstPktRxStartBtn";
            this.dvbTstPktRxStartBtn.Size = new System.Drawing.Size(99, 35);
            this.dvbTstPktRxStartBtn.TabIndex = 41;
            this.dvbTstPktRxStartBtn.Text = "Start RX";
            this.dvbTstPktRxStartBtn.UseVisualStyleBackColor = false;
            this.dvbTstPktRxStartBtn.Click += new System.EventHandler(this.dvbTstPktRxStartBtn_Click);
            // 
            // dvbTstPktRxOkCntLabel
            // 
            this.dvbTstPktRxOkCntLabel.AutoSize = true;
            this.dvbTstPktRxOkCntLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dvbTstPktRxOkCntLabel.Location = new System.Drawing.Point(747, 54);
            this.dvbTstPktRxOkCntLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dvbTstPktRxOkCntLabel.Name = "dvbTstPktRxOkCntLabel";
            this.dvbTstPktRxOkCntLabel.Size = new System.Drawing.Size(64, 17);
            this.dvbTstPktRxOkCntLabel.TabIndex = 39;
            this.dvbTstPktRxOkCntLabel.Text = "Rcvd OK";
            // 
            // dvbTstPktRxOkCntText
            // 
            this.dvbTstPktRxOkCntText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dvbTstPktRxOkCntText.Location = new System.Drawing.Point(747, 78);
            this.dvbTstPktRxOkCntText.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstPktRxOkCntText.Name = "dvbTstPktRxOkCntText";
            this.dvbTstPktRxOkCntText.Size = new System.Drawing.Size(99, 23);
            this.dvbTstPktRxOkCntText.TabIndex = 38;
            // 
            // dvbTstStatePage
            // 
            this.dvbTstStatePage.Controls.Add(this.dvbTstStateSetBtn);
            this.dvbTstStatePage.Controls.Add(this.dvbTstStateGroup);
            this.dvbTstStatePage.Location = new System.Drawing.Point(4, 28);
            this.dvbTstStatePage.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstStatePage.Name = "dvbTstStatePage";
            this.dvbTstStatePage.Size = new System.Drawing.Size(860, 328);
            this.dvbTstStatePage.TabIndex = 3;
            this.dvbTstStatePage.Text = "STATE CTRL";
            this.dvbTstStatePage.UseVisualStyleBackColor = true;
            // 
            // dvbTstStateSetBtn
            // 
            this.dvbTstStateSetBtn.BackColor = System.Drawing.Color.LightSteelBlue;
            this.dvbTstStateSetBtn.Location = new System.Drawing.Point(163, 24);
            this.dvbTstStateSetBtn.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstStateSetBtn.Name = "dvbTstStateSetBtn";
            this.dvbTstStateSetBtn.Size = new System.Drawing.Size(96, 35);
            this.dvbTstStateSetBtn.TabIndex = 1;
            this.dvbTstStateSetBtn.Text = "Set";
            this.dvbTstStateSetBtn.UseVisualStyleBackColor = false;
            this.dvbTstStateSetBtn.Click += new System.EventHandler(this.dvbTstStateSetBtn_Click);
            // 
            // dvbTstStateGroup
            // 
            this.dvbTstStateGroup.Controls.Add(this.dvbTstStateShutdownRadioBtn);
            this.dvbTstStateGroup.Controls.Add(this.dvbTstStateDeepSleepRadioBtn);
            this.dvbTstStateGroup.Controls.Add(this.dvbTstStateIdleRadioBtn);
            this.dvbTstStateGroup.Location = new System.Drawing.Point(18, 13);
            this.dvbTstStateGroup.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstStateGroup.Name = "dvbTstStateGroup";
            this.dvbTstStateGroup.Padding = new System.Windows.Forms.Padding(2);
            this.dvbTstStateGroup.Size = new System.Drawing.Size(141, 143);
            this.dvbTstStateGroup.TabIndex = 0;
            this.dvbTstStateGroup.TabStop = false;
            this.dvbTstStateGroup.Text = "State";
            // 
            // dvbTstStateShutdownRadioBtn
            // 
            this.dvbTstStateShutdownRadioBtn.AutoSize = true;
            this.dvbTstStateShutdownRadioBtn.Enabled = false;
            this.dvbTstStateShutdownRadioBtn.Location = new System.Drawing.Point(16, 108);
            this.dvbTstStateShutdownRadioBtn.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstStateShutdownRadioBtn.Name = "dvbTstStateShutdownRadioBtn";
            this.dvbTstStateShutdownRadioBtn.Size = new System.Drawing.Size(91, 21);
            this.dvbTstStateShutdownRadioBtn.TabIndex = 2;
            this.dvbTstStateShutdownRadioBtn.TabStop = true;
            this.dvbTstStateShutdownRadioBtn.Text = "Shutdown";
            this.dvbTstStateShutdownRadioBtn.UseVisualStyleBackColor = true;
            // 
            // dvbTstStateDeepSleepRadioBtn
            // 
            this.dvbTstStateDeepSleepRadioBtn.AutoSize = true;
            this.dvbTstStateDeepSleepRadioBtn.Location = new System.Drawing.Point(16, 73);
            this.dvbTstStateDeepSleepRadioBtn.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstStateDeepSleepRadioBtn.Name = "dvbTstStateDeepSleepRadioBtn";
            this.dvbTstStateDeepSleepRadioBtn.Size = new System.Drawing.Size(103, 21);
            this.dvbTstStateDeepSleepRadioBtn.TabIndex = 1;
            this.dvbTstStateDeepSleepRadioBtn.TabStop = true;
            this.dvbTstStateDeepSleepRadioBtn.Text = "Deep Sleep";
            this.dvbTstStateDeepSleepRadioBtn.UseVisualStyleBackColor = true;
            // 
            // dvbTstStateIdleRadioBtn
            // 
            this.dvbTstStateIdleRadioBtn.AutoSize = true;
            this.dvbTstStateIdleRadioBtn.Checked = true;
            this.dvbTstStateIdleRadioBtn.Location = new System.Drawing.Point(16, 38);
            this.dvbTstStateIdleRadioBtn.Margin = new System.Windows.Forms.Padding(2);
            this.dvbTstStateIdleRadioBtn.Name = "dvbTstStateIdleRadioBtn";
            this.dvbTstStateIdleRadioBtn.Size = new System.Drawing.Size(51, 21);
            this.dvbTstStateIdleRadioBtn.TabIndex = 0;
            this.dvbTstStateIdleRadioBtn.TabStop = true;
            this.dvbTstStateIdleRadioBtn.Text = "Idle";
            this.dvbTstStateIdleRadioBtn.UseVisualStyleBackColor = true;
            // 
            // regWrPage
            // 
            this.regWrPage.Location = new System.Drawing.Point(4, 28);
            this.regWrPage.Margin = new System.Windows.Forms.Padding(2);
            this.regWrPage.Name = "regWrPage";
            this.regWrPage.Size = new System.Drawing.Size(686, 274);
            this.regWrPage.TabIndex = 5;
            this.regWrPage.Text = "REG WR";
            this.regWrPage.UseVisualStyleBackColor = true;
            // 
            // paramFreqTableGroup
            // 
            this.paramFreqTableGroup.BackColor = System.Drawing.Color.Transparent;
            this.paramFreqTableGroup.Controls.Add(this.paramFreqTableInputText);
            this.paramFreqTableGroup.Controls.Add(this.paramFreqTableUnitLabel);
            this.paramFreqTableGroup.Controls.Add(this.paramFreqTableAddBtn);
            this.paramFreqTableGroup.Controls.Add(this.paramFreqTableList);
            this.paramFreqTableGroup.Controls.Add(this.paramFreqTableDelAllBtn);
            this.paramFreqTableGroup.Controls.Add(this.paramFreqTableDeleteBtn);
            this.paramFreqTableGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.paramFreqTableGroup.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.paramFreqTableGroup.Location = new System.Drawing.Point(10, 8);
            this.paramFreqTableGroup.Margin = new System.Windows.Forms.Padding(4);
            this.paramFreqTableGroup.Name = "paramFreqTableGroup";
            this.paramFreqTableGroup.Padding = new System.Windows.Forms.Padding(4);
            this.paramFreqTableGroup.Size = new System.Drawing.Size(842, 208);
            this.paramFreqTableGroup.TabIndex = 0;
            this.paramFreqTableGroup.TabStop = false;
            this.paramFreqTableGroup.Text = "Predefined Channel Table";
            // 
            // paramFreqTableInputText
            // 
            this.paramFreqTableInputText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramFreqTableInputText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramFreqTableInputText.Location = new System.Drawing.Point(11, 30);
            this.paramFreqTableInputText.Margin = new System.Windows.Forms.Padding(4);
            this.paramFreqTableInputText.Name = "paramFreqTableInputText";
            this.paramFreqTableInputText.Size = new System.Drawing.Size(76, 22);
            this.paramFreqTableInputText.TabIndex = 0;
            // 
            // paramFreqTableUnitLabel
            // 
            this.paramFreqTableUnitLabel.AutoSize = true;
            this.paramFreqTableUnitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramFreqTableUnitLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramFreqTableUnitLabel.Location = new System.Drawing.Point(95, 33);
            this.paramFreqTableUnitLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramFreqTableUnitLabel.Name = "paramFreqTableUnitLabel";
            this.paramFreqTableUnitLabel.Size = new System.Drawing.Size(36, 17);
            this.paramFreqTableUnitLabel.TabIndex = 1;
            this.paramFreqTableUnitLabel.Text = "MHz";
            // 
            // paramFreqTableAddBtn
            // 
            this.paramFreqTableAddBtn.BackColor = System.Drawing.Color.White;
            this.paramFreqTableAddBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramFreqTableAddBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramFreqTableAddBtn.Location = new System.Drawing.Point(11, 60);
            this.paramFreqTableAddBtn.Margin = new System.Windows.Forms.Padding(4);
            this.paramFreqTableAddBtn.Name = "paramFreqTableAddBtn";
            this.paramFreqTableAddBtn.Size = new System.Drawing.Size(128, 30);
            this.paramFreqTableAddBtn.TabIndex = 1;
            this.paramFreqTableAddBtn.Text = "Add";
            this.paramFreqTableAddBtn.UseVisualStyleBackColor = false;
            this.paramFreqTableAddBtn.Click += new System.EventHandler(this.btnAddFreq_Click);
            // 
            // paramFreqTableList
            // 
            this.paramFreqTableList.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramFreqTableList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramFreqTableList.FullRowSelect = true;
            this.paramFreqTableList.GridLines = true;
            this.paramFreqTableList.HideSelection = false;
            this.paramFreqTableList.Location = new System.Drawing.Point(147, 25);
            this.paramFreqTableList.Margin = new System.Windows.Forms.Padding(4);
            this.paramFreqTableList.MultiSelect = false;
            this.paramFreqTableList.Name = "paramFreqTableList";
            this.paramFreqTableList.Size = new System.Drawing.Size(684, 166);
            this.paramFreqTableList.TabIndex = 2;
            this.paramFreqTableList.UseCompatibleStateImageBehavior = false;
            this.paramFreqTableList.View = System.Windows.Forms.View.Details;
            this.paramFreqTableList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.freqList_KeyDown);
            // 
            // paramFreqTableDelAllBtn
            // 
            this.paramFreqTableDelAllBtn.BackColor = System.Drawing.Color.White;
            this.paramFreqTableDelAllBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramFreqTableDelAllBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramFreqTableDelAllBtn.Location = new System.Drawing.Point(11, 136);
            this.paramFreqTableDelAllBtn.Margin = new System.Windows.Forms.Padding(4);
            this.paramFreqTableDelAllBtn.Name = "paramFreqTableDelAllBtn";
            this.paramFreqTableDelAllBtn.Size = new System.Drawing.Size(128, 30);
            this.paramFreqTableDelAllBtn.TabIndex = 3;
            this.paramFreqTableDelAllBtn.Text = "Delete All";
            this.paramFreqTableDelAllBtn.UseVisualStyleBackColor = false;
            this.paramFreqTableDelAllBtn.Click += new System.EventHandler(this.paramFreqTableDelAllBtn_Click);
            // 
            // paramFreqTableDeleteBtn
            // 
            this.paramFreqTableDeleteBtn.BackColor = System.Drawing.Color.White;
            this.paramFreqTableDeleteBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramFreqTableDeleteBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramFreqTableDeleteBtn.Location = new System.Drawing.Point(11, 98);
            this.paramFreqTableDeleteBtn.Margin = new System.Windows.Forms.Padding(4);
            this.paramFreqTableDeleteBtn.Name = "paramFreqTableDeleteBtn";
            this.paramFreqTableDeleteBtn.Size = new System.Drawing.Size(128, 30);
            this.paramFreqTableDeleteBtn.TabIndex = 3;
            this.paramFreqTableDeleteBtn.Text = "Delete Selected";
            this.paramFreqTableDeleteBtn.UseVisualStyleBackColor = false;
            this.paramFreqTableDeleteBtn.Click += new System.EventHandler(this.paramFreqTableDeleteBtn_Click);
            // 
            // paramPbLabel
            // 
            this.paramPbLabel.AutoSize = true;
            this.paramPbLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramPbLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramPbLabel.Location = new System.Drawing.Point(10, 28);
            this.paramPbLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramPbLabel.Name = "paramPbLabel";
            this.paramPbLabel.Size = new System.Drawing.Size(68, 17);
            this.paramPbLabel.TabIndex = 0;
            this.paramPbLabel.Text = "Preamble";
            // 
            // paramPbContentText
            // 
            this.paramPbContentText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paramPbContentText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramPbContentText.Location = new System.Drawing.Point(10, 50);
            this.paramPbContentText.Margin = new System.Windows.Forms.Padding(4);
            this.paramPbContentText.MaxLength = 2;
            this.paramPbContentText.Name = "paramPbContentText";
            this.paramPbContentText.Size = new System.Drawing.Size(34, 22);
            this.paramPbContentText.TabIndex = 0;
            this.paramPbContentText.Text = "AA";
            this.paramPbContentText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.preambleContent_KeyPress);
            this.paramPbContentText.Validating += new System.ComponentModel.CancelEventHandler(this.preambleContent_Validating);
            // 
            // paramPbUnitLabel
            // 
            this.paramPbUnitLabel.AutoSize = true;
            this.paramPbUnitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramPbUnitLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramPbUnitLabel.Location = new System.Drawing.Point(118, 52);
            this.paramPbUnitLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramPbUnitLabel.Name = "paramPbUnitLabel";
            this.paramPbUnitLabel.Size = new System.Drawing.Size(53, 17);
            this.paramPbUnitLabel.TabIndex = 2;
            this.paramPbUnitLabel.Text = "Byte(s)";
            // 
            // paramPbLenText
            // 
            this.paramPbLenText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paramPbLenText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramPbLenText.Location = new System.Drawing.Point(76, 50);
            this.paramPbLenText.Margin = new System.Windows.Forms.Padding(4);
            this.paramPbLenText.MaxLength = 2;
            this.paramPbLenText.Name = "paramPbLenText";
            this.paramPbLenText.Size = new System.Drawing.Size(34, 22);
            this.paramPbLenText.TabIndex = 1;
            this.paramPbLenText.Text = "4";
            this.paramPbLenText.TextChanged += new System.EventHandler(this.preambleLen_TextChanged);
            this.paramPbLenText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.preambleLen_KeyPress);
            this.paramPbLenText.Validating += new System.ComponentModel.CancelEventHandler(this.preambleLen_Validating);
            // 
            // paramTxSyncLabel
            // 
            this.paramTxSyncLabel.AutoSize = true;
            this.paramTxSyncLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramTxSyncLabel.Location = new System.Drawing.Point(11, 30);
            this.paramTxSyncLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramTxSyncLabel.Name = "paramTxSyncLabel";
            this.paramTxSyncLabel.Size = new System.Drawing.Size(48, 17);
            this.paramTxSyncLabel.TabIndex = 19;
            this.paramTxSyncLabel.Text = "TX: 0x";
            // 
            // paramTxSyncActualLabel
            // 
            this.paramTxSyncActualLabel.AutoSize = true;
            this.paramTxSyncActualLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramTxSyncActualLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.paramTxSyncActualLabel.Location = new System.Drawing.Point(250, 32);
            this.paramTxSyncActualLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramTxSyncActualLabel.Name = "paramTxSyncActualLabel";
            this.paramTxSyncActualLabel.Size = new System.Drawing.Size(128, 15);
            this.paramTxSyncActualLabel.TabIndex = 22;
            this.paramTxSyncActualLabel.Text = "Transfer : 5A 0F BE 66";
            // 
            // paramRxSyncLabel
            // 
            this.paramRxSyncLabel.AutoSize = true;
            this.paramRxSyncLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramRxSyncLabel.Location = new System.Drawing.Point(11, 61);
            this.paramRxSyncLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramRxSyncLabel.Name = "paramRxSyncLabel";
            this.paramRxSyncLabel.Size = new System.Drawing.Size(49, 17);
            this.paramRxSyncLabel.TabIndex = 23;
            this.paramRxSyncLabel.Text = "RX: 0x";
            // 
            // paramRxSyncText
            // 
            this.paramRxSyncText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paramRxSyncText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramRxSyncText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramRxSyncText.Location = new System.Drawing.Point(61, 58);
            this.paramRxSyncText.Margin = new System.Windows.Forms.Padding(4);
            this.paramRxSyncText.MaxLength = 8;
            this.paramRxSyncText.Name = "paramRxSyncText";
            this.paramRxSyncText.Size = new System.Drawing.Size(83, 23);
            this.paramRxSyncText.TabIndex = 13;
            this.paramRxSyncText.Text = "5A0FBE66";
            this.paramRxSyncText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rxSyncword_KeyPress);
            this.paramRxSyncText.Validating += new System.ComponentModel.CancelEventHandler(this.rxSyncword_Validating);
            // 
            // paramRxSyncSoftInvertCheck
            // 
            this.paramRxSyncSoftInvertCheck.AutoSize = true;
            this.paramRxSyncSoftInvertCheck.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramRxSyncSoftInvertCheck.Location = new System.Drawing.Point(151, 60);
            this.paramRxSyncSoftInvertCheck.Margin = new System.Windows.Forms.Padding(4);
            this.paramRxSyncSoftInvertCheck.Name = "paramRxSyncSoftInvertCheck";
            this.paramRxSyncSoftInvertCheck.Size = new System.Drawing.Size(94, 21);
            this.paramRxSyncSoftInvertCheck.TabIndex = 14;
            this.paramRxSyncSoftInvertCheck.Text = "Soft Invert";
            this.paramRxSyncSoftInvertCheck.UseVisualStyleBackColor = true;
            this.paramRxSyncSoftInvertCheck.CheckedChanged += new System.EventHandler(this.rxsyncInvertCheckbox_CheckedChanged);
            // 
            // paramRxSyncActualLabel
            // 
            this.paramRxSyncActualLabel.AutoSize = true;
            this.paramRxSyncActualLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramRxSyncActualLabel.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.paramRxSyncActualLabel.Location = new System.Drawing.Point(260, 63);
            this.paramRxSyncActualLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramRxSyncActualLabel.Name = "paramRxSyncActualLabel";
            this.paramRxSyncActualLabel.Size = new System.Drawing.Size(118, 15);
            this.paramRxSyncActualLabel.TabIndex = 25;
            this.paramRxSyncActualLabel.Text = "Detect : 5A 0F BE 66";
            // 
            // paramRxSyncAllowBitErrText
            // 
            this.paramRxSyncAllowBitErrText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paramRxSyncAllowBitErrText.Location = new System.Drawing.Point(111, 89);
            this.paramRxSyncAllowBitErrText.Margin = new System.Windows.Forms.Padding(4);
            this.paramRxSyncAllowBitErrText.MaxLength = 1;
            this.paramRxSyncAllowBitErrText.Name = "paramRxSyncAllowBitErrText";
            this.paramRxSyncAllowBitErrText.Size = new System.Drawing.Size(33, 22);
            this.paramRxSyncAllowBitErrText.TabIndex = 0;
            this.paramRxSyncAllowBitErrText.Text = "1";
            this.paramRxSyncAllowBitErrText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.allowBitError_KeyPress);
            this.paramRxSyncAllowBitErrText.Validating += new System.ComponentModel.CancelEventHandler(this.allowBitError_Validating);
            // 
            // paramRxSyncAllowBitErrUnitLabel
            // 
            this.paramRxSyncAllowBitErrUnitLabel.AutoSize = true;
            this.paramRxSyncAllowBitErrUnitLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramRxSyncAllowBitErrUnitLabel.Location = new System.Drawing.Point(148, 91);
            this.paramRxSyncAllowBitErrUnitLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramRxSyncAllowBitErrUnitLabel.Name = "paramRxSyncAllowBitErrUnitLabel";
            this.paramRxSyncAllowBitErrUnitLabel.Size = new System.Drawing.Size(80, 17);
            this.paramRxSyncAllowBitErrUnitLabel.TabIndex = 1;
            this.paramRxSyncAllowBitErrUnitLabel.Text = "Bit(s), (0-7)";
            // 
            // paramFIFOBitOrderGroup
            // 
            this.paramFIFOBitOrderGroup.Controls.Add(this.paramFIFOBitOrderLSBRadioBtn);
            this.paramFIFOBitOrderGroup.Controls.Add(this.paramFIFOBitOrderMSBRadioBtn);
            this.paramFIFOBitOrderGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramFIFOBitOrderGroup.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.paramFIFOBitOrderGroup.Location = new System.Drawing.Point(199, 21);
            this.paramFIFOBitOrderGroup.Margin = new System.Windows.Forms.Padding(4);
            this.paramFIFOBitOrderGroup.Name = "paramFIFOBitOrderGroup";
            this.paramFIFOBitOrderGroup.Padding = new System.Windows.Forms.Padding(4);
            this.paramFIFOBitOrderGroup.Size = new System.Drawing.Size(212, 58);
            this.paramFIFOBitOrderGroup.TabIndex = 34;
            this.paramFIFOBitOrderGroup.TabStop = false;
            this.paramFIFOBitOrderGroup.Text = "FIFO Bit Order Per Byte";
            // 
            // paramFIFOBitOrderLSBRadioBtn
            // 
            this.paramFIFOBitOrderLSBRadioBtn.AutoSize = true;
            this.paramFIFOBitOrderLSBRadioBtn.Checked = true;
            this.paramFIFOBitOrderLSBRadioBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramFIFOBitOrderLSBRadioBtn.Location = new System.Drawing.Point(12, 23);
            this.paramFIFOBitOrderLSBRadioBtn.Margin = new System.Windows.Forms.Padding(4);
            this.paramFIFOBitOrderLSBRadioBtn.Name = "paramFIFOBitOrderLSBRadioBtn";
            this.paramFIFOBitOrderLSBRadioBtn.Size = new System.Drawing.Size(81, 21);
            this.paramFIFOBitOrderLSBRadioBtn.TabIndex = 0;
            this.paramFIFOBitOrderLSBRadioBtn.TabStop = true;
            this.paramFIFOBitOrderLSBRadioBtn.Text = "LSB(Bit)";
            this.paramFIFOBitOrderLSBRadioBtn.UseVisualStyleBackColor = true;
            // 
            // paramFIFOBitOrderMSBRadioBtn
            // 
            this.paramFIFOBitOrderMSBRadioBtn.AutoSize = true;
            this.paramFIFOBitOrderMSBRadioBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramFIFOBitOrderMSBRadioBtn.Location = new System.Drawing.Point(105, 23);
            this.paramFIFOBitOrderMSBRadioBtn.Margin = new System.Windows.Forms.Padding(4);
            this.paramFIFOBitOrderMSBRadioBtn.Name = "paramFIFOBitOrderMSBRadioBtn";
            this.paramFIFOBitOrderMSBRadioBtn.Size = new System.Drawing.Size(84, 21);
            this.paramFIFOBitOrderMSBRadioBtn.TabIndex = 1;
            this.paramFIFOBitOrderMSBRadioBtn.Text = "MSB(Bit)";
            this.paramFIFOBitOrderMSBRadioBtn.UseVisualStyleBackColor = true;
            // 
            // paramTxFifoModeGroup
            // 
            this.paramTxFifoModeGroup.Controls.Add(this.paramTxFifoModeTransPresyncRadioBtn);
            this.paramTxFifoModeGroup.Controls.Add(this.paramTxFifoModeTransparentRadioBtn);
            this.paramTxFifoModeGroup.Controls.Add(this.paramTxFifoMode802154RadioBtn);
            this.paramTxFifoModeGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.paramTxFifoModeGroup.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.paramTxFifoModeGroup.Location = new System.Drawing.Point(10, 8);
            this.paramTxFifoModeGroup.Margin = new System.Windows.Forms.Padding(4);
            this.paramTxFifoModeGroup.Name = "paramTxFifoModeGroup";
            this.paramTxFifoModeGroup.Padding = new System.Windows.Forms.Padding(4);
            this.paramTxFifoModeGroup.Size = new System.Drawing.Size(212, 115);
            this.paramTxFifoModeGroup.TabIndex = 4;
            this.paramTxFifoModeGroup.TabStop = false;
            this.paramTxFifoModeGroup.Text = "TX FIFO in MAC";
            // 
            // paramTxFifoModeTransPresyncRadioBtn
            // 
            this.paramTxFifoModeTransPresyncRadioBtn.AutoSize = true;
            this.paramTxFifoModeTransPresyncRadioBtn.Checked = true;
            this.paramTxFifoModeTransPresyncRadioBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramTxFifoModeTransPresyncRadioBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramTxFifoModeTransPresyncRadioBtn.Location = new System.Drawing.Point(8, 22);
            this.paramTxFifoModeTransPresyncRadioBtn.Margin = new System.Windows.Forms.Padding(4);
            this.paramTxFifoModeTransPresyncRadioBtn.Name = "paramTxFifoModeTransPresyncRadioBtn";
            this.paramTxFifoModeTransPresyncRadioBtn.Size = new System.Drawing.Size(174, 21);
            this.paramTxFifoModeTransPresyncRadioBtn.TabIndex = 0;
            this.paramTxFifoModeTransPresyncRadioBtn.TabStop = true;
            this.paramTxFifoModeTransPresyncRadioBtn.Text = "Transparent + Presync";
            this.paramTxFifoModeTransPresyncRadioBtn.UseVisualStyleBackColor = true;
            // 
            // paramTxFifoModeTransparentRadioBtn
            // 
            this.paramTxFifoModeTransparentRadioBtn.AutoSize = true;
            this.paramTxFifoModeTransparentRadioBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramTxFifoModeTransparentRadioBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramTxFifoModeTransparentRadioBtn.Location = new System.Drawing.Point(8, 52);
            this.paramTxFifoModeTransparentRadioBtn.Margin = new System.Windows.Forms.Padding(4);
            this.paramTxFifoModeTransparentRadioBtn.Name = "paramTxFifoModeTransparentRadioBtn";
            this.paramTxFifoModeTransparentRadioBtn.Size = new System.Drawing.Size(107, 21);
            this.paramTxFifoModeTransparentRadioBtn.TabIndex = 1;
            this.paramTxFifoModeTransparentRadioBtn.Text = "Transparent";
            this.paramTxFifoModeTransparentRadioBtn.UseVisualStyleBackColor = true;
            // 
            // paramTxFifoMode802154RadioBtn
            // 
            this.paramTxFifoMode802154RadioBtn.AutoSize = true;
            this.paramTxFifoMode802154RadioBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramTxFifoMode802154RadioBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramTxFifoMode802154RadioBtn.Location = new System.Drawing.Point(8, 82);
            this.paramTxFifoMode802154RadioBtn.Margin = new System.Windows.Forms.Padding(4);
            this.paramTxFifoMode802154RadioBtn.Name = "paramTxFifoMode802154RadioBtn";
            this.paramTxFifoMode802154RadioBtn.Size = new System.Drawing.Size(85, 21);
            this.paramTxFifoMode802154RadioBtn.TabIndex = 2;
            this.paramTxFifoMode802154RadioBtn.Text = "802.15.4";
            this.paramTxFifoMode802154RadioBtn.UseVisualStyleBackColor = true;
            // 
            // paramPcrmuGroup
            // 
            this.paramPcrmuGroup.Controls.Add(this.paramPcrShdEnableRadioBtn);
            this.paramPcrmuGroup.Controls.Add(this.paramPcrmuEnableRadioBtn);
            this.paramPcrmuGroup.Controls.Add(this.paramPcrmuDisableRadioBtn);
            this.paramPcrmuGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.paramPcrmuGroup.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.paramPcrmuGroup.Location = new System.Drawing.Point(10, 8);
            this.paramPcrmuGroup.Margin = new System.Windows.Forms.Padding(4);
            this.paramPcrmuGroup.Name = "paramPcrmuGroup";
            this.paramPcrmuGroup.Padding = new System.Windows.Forms.Padding(4);
            this.paramPcrmuGroup.Size = new System.Drawing.Size(208, 104);
            this.paramPcrmuGroup.TabIndex = 5;
            this.paramPcrmuGroup.TabStop = false;
            this.paramPcrmuGroup.Text = "PCR Management";
            // 
            // paramPcrShdEnableRadioBtn
            // 
            this.paramPcrShdEnableRadioBtn.AutoSize = true;
            this.paramPcrShdEnableRadioBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramPcrShdEnableRadioBtn.ForeColor = System.Drawing.Color.Black;
            this.paramPcrShdEnableRadioBtn.Location = new System.Drawing.Point(8, 65);
            this.paramPcrShdEnableRadioBtn.Name = "paramPcrShdEnableRadioBtn";
            this.paramPcrShdEnableRadioBtn.Size = new System.Drawing.Size(156, 21);
            this.paramPcrShdEnableRadioBtn.TabIndex = 2;
            this.paramPcrShdEnableRadioBtn.Text = "Enable + Shutdown*";
            this.paramPcrShdEnableRadioBtn.UseVisualStyleBackColor = true;
            // 
            // paramPcrmuEnableRadioBtn
            // 
            this.paramPcrmuEnableRadioBtn.AutoSize = true;
            this.paramPcrmuEnableRadioBtn.Checked = true;
            this.paramPcrmuEnableRadioBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramPcrmuEnableRadioBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramPcrmuEnableRadioBtn.Location = new System.Drawing.Point(8, 25);
            this.paramPcrmuEnableRadioBtn.Margin = new System.Windows.Forms.Padding(4);
            this.paramPcrmuEnableRadioBtn.Name = "paramPcrmuEnableRadioBtn";
            this.paramPcrmuEnableRadioBtn.Size = new System.Drawing.Size(73, 21);
            this.paramPcrmuEnableRadioBtn.TabIndex = 0;
            this.paramPcrmuEnableRadioBtn.TabStop = true;
            this.paramPcrmuEnableRadioBtn.Text = "Enable";
            this.paramPcrmuEnableRadioBtn.UseVisualStyleBackColor = true;
            // 
            // paramPcrmuDisableRadioBtn
            // 
            this.paramPcrmuDisableRadioBtn.AutoSize = true;
            this.paramPcrmuDisableRadioBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramPcrmuDisableRadioBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramPcrmuDisableRadioBtn.Location = new System.Drawing.Point(102, 25);
            this.paramPcrmuDisableRadioBtn.Margin = new System.Windows.Forms.Padding(4);
            this.paramPcrmuDisableRadioBtn.Name = "paramPcrmuDisableRadioBtn";
            this.paramPcrmuDisableRadioBtn.Size = new System.Drawing.Size(76, 21);
            this.paramPcrmuDisableRadioBtn.TabIndex = 1;
            this.paramPcrmuDisableRadioBtn.Text = "Disable";
            this.paramPcrmuDisableRadioBtn.UseVisualStyleBackColor = true;
            // 
            // paramDataFormatGroup
            // 
            this.paramDataFormatGroup.Controls.Add(this.paramDataFormat8BitsRadioBtn);
            this.paramDataFormatGroup.Controls.Add(this.paramDataFormat32BitsRadioBtn);
            this.paramDataFormatGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.paramDataFormatGroup.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.paramDataFormatGroup.Location = new System.Drawing.Point(635, 164);
            this.paramDataFormatGroup.Margin = new System.Windows.Forms.Padding(4);
            this.paramDataFormatGroup.Name = "paramDataFormatGroup";
            this.paramDataFormatGroup.Padding = new System.Windows.Forms.Padding(4);
            this.paramDataFormatGroup.Size = new System.Drawing.Size(208, 60);
            this.paramDataFormatGroup.TabIndex = 6;
            this.paramDataFormatGroup.TabStop = false;
            this.paramDataFormatGroup.Text = "Data Format";
            this.paramDataFormatGroup.Visible = false;
            // 
            // paramDataFormat8BitsRadioBtn
            // 
            this.paramDataFormat8BitsRadioBtn.AutoSize = true;
            this.paramDataFormat8BitsRadioBtn.Checked = true;
            this.paramDataFormat8BitsRadioBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramDataFormat8BitsRadioBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramDataFormat8BitsRadioBtn.Location = new System.Drawing.Point(8, 25);
            this.paramDataFormat8BitsRadioBtn.Margin = new System.Windows.Forms.Padding(4);
            this.paramDataFormat8BitsRadioBtn.Name = "paramDataFormat8BitsRadioBtn";
            this.paramDataFormat8BitsRadioBtn.Size = new System.Drawing.Size(59, 21);
            this.paramDataFormat8BitsRadioBtn.TabIndex = 0;
            this.paramDataFormat8BitsRadioBtn.TabStop = true;
            this.paramDataFormat8BitsRadioBtn.Text = "8bits";
            this.paramDataFormat8BitsRadioBtn.UseVisualStyleBackColor = true;
            // 
            // paramDataFormat32BitsRadioBtn
            // 
            this.paramDataFormat32BitsRadioBtn.AutoSize = true;
            this.paramDataFormat32BitsRadioBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramDataFormat32BitsRadioBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramDataFormat32BitsRadioBtn.Location = new System.Drawing.Point(110, 25);
            this.paramDataFormat32BitsRadioBtn.Margin = new System.Windows.Forms.Padding(4);
            this.paramDataFormat32BitsRadioBtn.Name = "paramDataFormat32BitsRadioBtn";
            this.paramDataFormat32BitsRadioBtn.Size = new System.Drawing.Size(68, 21);
            this.paramDataFormat32BitsRadioBtn.TabIndex = 1;
            this.paramDataFormat32BitsRadioBtn.Text = "32Bits";
            this.paramDataFormat32BitsRadioBtn.UseVisualStyleBackColor = true;
            // 
            // paramIntModeGroup
            // 
            this.paramIntModeGroup.Controls.Add(this.paramIntModeSelectedLabel);
            this.paramIntModeGroup.Controls.Add(this.paramIntModeSelectedCountLabel);
            this.paramIntModeGroup.Controls.Add(this.paramIntModeSelectedUnitLabel);
            this.paramIntModeGroup.Controls.Add(this.paramIntModeTxAemptyGroup);
            this.paramIntModeGroup.Controls.Add(this.paramIntModeRxAfullGroup);
            this.paramIntModeGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.paramIntModeGroup.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.paramIntModeGroup.Location = new System.Drawing.Point(10, 8);
            this.paramIntModeGroup.Margin = new System.Windows.Forms.Padding(4);
            this.paramIntModeGroup.Name = "paramIntModeGroup";
            this.paramIntModeGroup.Padding = new System.Windows.Forms.Padding(4);
            this.paramIntModeGroup.Size = new System.Drawing.Size(213, 205);
            this.paramIntModeGroup.TabIndex = 7;
            this.paramIntModeGroup.TabStop = false;
            this.paramIntModeGroup.Text = "Interrupt Event Selection";
            // 
            // paramIntModeSelectedLabel
            // 
            this.paramIntModeSelectedLabel.AutoSize = true;
            this.paramIntModeSelectedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramIntModeSelectedLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramIntModeSelectedLabel.Location = new System.Drawing.Point(10, 22);
            this.paramIntModeSelectedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramIntModeSelectedLabel.Name = "paramIntModeSelectedLabel";
            this.paramIntModeSelectedLabel.Size = new System.Drawing.Size(67, 17);
            this.paramIntModeSelectedLabel.TabIndex = 0;
            this.paramIntModeSelectedLabel.Text = "Selected:";
            // 
            // paramIntModeSelectedCountLabel
            // 
            this.paramIntModeSelectedCountLabel.AutoSize = true;
            this.paramIntModeSelectedCountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramIntModeSelectedCountLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramIntModeSelectedCountLabel.Location = new System.Drawing.Point(80, 22);
            this.paramIntModeSelectedCountLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramIntModeSelectedCountLabel.Name = "paramIntModeSelectedCountLabel";
            this.paramIntModeSelectedCountLabel.Size = new System.Drawing.Size(16, 17);
            this.paramIntModeSelectedCountLabel.TabIndex = 1;
            this.paramIntModeSelectedCountLabel.Text = "0";
            // 
            // paramIntModeSelectedUnitLabel
            // 
            this.paramIntModeSelectedUnitLabel.AutoSize = true;
            this.paramIntModeSelectedUnitLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramIntModeSelectedUnitLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramIntModeSelectedUnitLabel.Location = new System.Drawing.Point(105, 22);
            this.paramIntModeSelectedUnitLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramIntModeSelectedUnitLabel.Name = "paramIntModeSelectedUnitLabel";
            this.paramIntModeSelectedUnitLabel.Size = new System.Drawing.Size(41, 17);
            this.paramIntModeSelectedUnitLabel.TabIndex = 2;
            this.paramIntModeSelectedUnitLabel.Text = "items";
            // 
            // paramIntModeTxAemptyGroup
            // 
            this.paramIntModeTxAemptyGroup.Controls.Add(this.paramIntModeTxAemptyText);
            this.paramIntModeTxAemptyGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramIntModeTxAemptyGroup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramIntModeTxAemptyGroup.Location = new System.Drawing.Point(10, 85);
            this.paramIntModeTxAemptyGroup.Margin = new System.Windows.Forms.Padding(4);
            this.paramIntModeTxAemptyGroup.Name = "paramIntModeTxAemptyGroup";
            this.paramIntModeTxAemptyGroup.Padding = new System.Windows.Forms.Padding(4);
            this.paramIntModeTxAemptyGroup.Size = new System.Drawing.Size(195, 52);
            this.paramIntModeTxAemptyGroup.TabIndex = 3;
            this.paramIntModeTxAemptyGroup.TabStop = false;
            this.paramIntModeTxAemptyGroup.Text = "Tx_Aempty(0-128)";
            this.paramIntModeTxAemptyGroup.Visible = false;
            // 
            // paramIntModeTxAemptyText
            // 
            this.paramIntModeTxAemptyText.Location = new System.Drawing.Point(144, 19);
            this.paramIntModeTxAemptyText.Margin = new System.Windows.Forms.Padding(4);
            this.paramIntModeTxAemptyText.MaxLength = 3;
            this.paramIntModeTxAemptyText.Name = "paramIntModeTxAemptyText";
            this.paramIntModeTxAemptyText.Size = new System.Drawing.Size(42, 23);
            this.paramIntModeTxAemptyText.TabIndex = 0;
            this.paramIntModeTxAemptyText.Text = "64";
            this.paramIntModeTxAemptyText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txAemptyThr_KeyPress);
            this.paramIntModeTxAemptyText.Validating += new System.ComponentModel.CancelEventHandler(this.txAemptyThr_Validating);
            // 
            // paramIntModeRxAfullGroup
            // 
            this.paramIntModeRxAfullGroup.Controls.Add(this.paramIntModeRxAfullText);
            this.paramIntModeRxAfullGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramIntModeRxAfullGroup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramIntModeRxAfullGroup.Location = new System.Drawing.Point(10, 146);
            this.paramIntModeRxAfullGroup.Margin = new System.Windows.Forms.Padding(4);
            this.paramIntModeRxAfullGroup.Name = "paramIntModeRxAfullGroup";
            this.paramIntModeRxAfullGroup.Padding = new System.Windows.Forms.Padding(4);
            this.paramIntModeRxAfullGroup.Size = new System.Drawing.Size(195, 52);
            this.paramIntModeRxAfullGroup.TabIndex = 4;
            this.paramIntModeRxAfullGroup.TabStop = false;
            this.paramIntModeRxAfullGroup.Text = "Rx_Afull(1-128)";
            this.paramIntModeRxAfullGroup.Visible = false;
            // 
            // paramIntModeRxAfullText
            // 
            this.paramIntModeRxAfullText.Location = new System.Drawing.Point(144, 21);
            this.paramIntModeRxAfullText.Margin = new System.Windows.Forms.Padding(4);
            this.paramIntModeRxAfullText.MaxLength = 3;
            this.paramIntModeRxAfullText.Name = "paramIntModeRxAfullText";
            this.paramIntModeRxAfullText.Size = new System.Drawing.Size(42, 23);
            this.paramIntModeRxAfullText.TabIndex = 0;
            this.paramIntModeRxAfullText.Text = "64";
            this.paramIntModeRxAfullText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rxFullThr_KeyPress);
            this.paramIntModeRxAfullText.Validating += new System.ComponentModel.CancelEventHandler(this.rxFullThr_Validating);
            // 
            // paramGpio0Group
            // 
            this.paramGpio0Group.Controls.Add(this.paramGpio0GIoInvCheck);
            this.paramGpio0Group.Controls.Add(this.paramGpio0ModeLabel);
            this.paramGpio0Group.Controls.Add(this.paramGpio0PullCtrlLabel);
            this.paramGpio0Group.Controls.Add(this.paramGpio0GIoSelLabel);
            this.paramGpio0Group.Controls.Add(this.paramGpio0ModeCombo);
            this.paramGpio0Group.Controls.Add(this.paramGpio0PullCtrlCombo);
            this.paramGpio0Group.Controls.Add(this.paramGpio0GioSelCombo);
            this.paramGpio0Group.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.paramGpio0Group.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.paramGpio0Group.Location = new System.Drawing.Point(8, 8);
            this.paramGpio0Group.Margin = new System.Windows.Forms.Padding(4);
            this.paramGpio0Group.Name = "paramGpio0Group";
            this.paramGpio0Group.Padding = new System.Windows.Forms.Padding(4);
            this.paramGpio0Group.Size = new System.Drawing.Size(203, 211);
            this.paramGpio0Group.TabIndex = 8;
            this.paramGpio0Group.TabStop = false;
            this.paramGpio0Group.Text = "GPIO0";
            // 
            // paramGpio0GIoInvCheck
            // 
            this.paramGpio0GIoInvCheck.AutoSize = true;
            this.paramGpio0GIoInvCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramGpio0GIoInvCheck.ForeColor = System.Drawing.Color.Black;
            this.paramGpio0GIoInvCheck.Location = new System.Drawing.Point(100, 81);
            this.paramGpio0GIoInvCheck.Name = "paramGpio0GIoInvCheck";
            this.paramGpio0GIoInvCheck.Size = new System.Drawing.Size(76, 21);
            this.paramGpio0GIoInvCheck.TabIndex = 10;
            this.paramGpio0GIoInvCheck.Text = "Inverse";
            this.paramGpio0GIoInvCheck.UseVisualStyleBackColor = true;
            // 
            // paramGpio0ModeLabel
            // 
            this.paramGpio0ModeLabel.AutoSize = true;
            this.paramGpio0ModeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramGpio0ModeLabel.ForeColor = System.Drawing.Color.Black;
            this.paramGpio0ModeLabel.Location = new System.Drawing.Point(18, 28);
            this.paramGpio0ModeLabel.Name = "paramGpio0ModeLabel";
            this.paramGpio0ModeLabel.Size = new System.Drawing.Size(181, 17);
            this.paramGpio0ModeLabel.TabIndex = 9;
            this.paramGpio0ModeLabel.Text = "Mode(Default is TCXO_EN)";
            // 
            // paramGpio0PullCtrlLabel
            // 
            this.paramGpio0PullCtrlLabel.AutoSize = true;
            this.paramGpio0PullCtrlLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramGpio0PullCtrlLabel.ForeColor = System.Drawing.Color.Black;
            this.paramGpio0PullCtrlLabel.Location = new System.Drawing.Point(18, 138);
            this.paramGpio0PullCtrlLabel.Name = "paramGpio0PullCtrlLabel";
            this.paramGpio0PullCtrlLabel.Size = new System.Drawing.Size(80, 17);
            this.paramGpio0PullCtrlLabel.TabIndex = 9;
            this.paramGpio0PullCtrlLabel.Text = "Pull Control";
            // 
            // paramGpio0GIoSelLabel
            // 
            this.paramGpio0GIoSelLabel.AutoSize = true;
            this.paramGpio0GIoSelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramGpio0GIoSelLabel.ForeColor = System.Drawing.Color.Black;
            this.paramGpio0GIoSelLabel.Location = new System.Drawing.Point(18, 83);
            this.paramGpio0GIoSelLabel.Name = "paramGpio0GIoSelLabel";
            this.paramGpio0GIoSelLabel.Size = new System.Drawing.Size(76, 17);
            this.paramGpio0GIoSelLabel.TabIndex = 9;
            this.paramGpio0GIoSelLabel.Text = "GIO Select";
            // 
            // paramGpio0ModeCombo
            // 
            this.paramGpio0ModeCombo.BackColor = System.Drawing.Color.White;
            this.paramGpio0ModeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paramGpio0ModeCombo.Enabled = false;
            this.paramGpio0ModeCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramGpio0ModeCombo.ForeColor = System.Drawing.Color.Black;
            this.paramGpio0ModeCombo.FormattingEnabled = true;
            this.paramGpio0ModeCombo.Location = new System.Drawing.Point(18, 52);
            this.paramGpio0ModeCombo.Margin = new System.Windows.Forms.Padding(4);
            this.paramGpio0ModeCombo.Name = "paramGpio0ModeCombo";
            this.paramGpio0ModeCombo.Size = new System.Drawing.Size(158, 24);
            this.paramGpio0ModeCombo.TabIndex = 1;
            this.paramGpio0ModeCombo.SelectedIndexChanged += new System.EventHandler(this.paramGpio0ModeCombo_SelectedIndexChanged);
            // 
            // paramGpio0PullCtrlCombo
            // 
            this.paramGpio0PullCtrlCombo.BackColor = System.Drawing.Color.White;
            this.paramGpio0PullCtrlCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paramGpio0PullCtrlCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramGpio0PullCtrlCombo.ForeColor = System.Drawing.Color.Black;
            this.paramGpio0PullCtrlCombo.FormattingEnabled = true;
            this.paramGpio0PullCtrlCombo.Location = new System.Drawing.Point(18, 162);
            this.paramGpio0PullCtrlCombo.Margin = new System.Windows.Forms.Padding(4);
            this.paramGpio0PullCtrlCombo.Name = "paramGpio0PullCtrlCombo";
            this.paramGpio0PullCtrlCombo.Size = new System.Drawing.Size(158, 24);
            this.paramGpio0PullCtrlCombo.TabIndex = 1;
            // 
            // paramGpio0GioSelCombo
            // 
            this.paramGpio0GioSelCombo.BackColor = System.Drawing.Color.White;
            this.paramGpio0GioSelCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paramGpio0GioSelCombo.Enabled = false;
            this.paramGpio0GioSelCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramGpio0GioSelCombo.ForeColor = System.Drawing.Color.Black;
            this.paramGpio0GioSelCombo.FormattingEnabled = true;
            this.paramGpio0GioSelCombo.Location = new System.Drawing.Point(18, 107);
            this.paramGpio0GioSelCombo.Margin = new System.Windows.Forms.Padding(4);
            this.paramGpio0GioSelCombo.Name = "paramGpio0GioSelCombo";
            this.paramGpio0GioSelCombo.Size = new System.Drawing.Size(158, 24);
            this.paramGpio0GioSelCombo.TabIndex = 1;
            // 
            // paramGpio1GioSelCombo
            // 
            this.paramGpio1GioSelCombo.BackColor = System.Drawing.Color.White;
            this.paramGpio1GioSelCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paramGpio1GioSelCombo.Enabled = false;
            this.paramGpio1GioSelCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramGpio1GioSelCombo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramGpio1GioSelCombo.FormattingEnabled = true;
            this.paramGpio1GioSelCombo.Location = new System.Drawing.Point(18, 107);
            this.paramGpio1GioSelCombo.Margin = new System.Windows.Forms.Padding(4);
            this.paramGpio1GioSelCombo.Name = "paramGpio1GioSelCombo";
            this.paramGpio1GioSelCombo.Size = new System.Drawing.Size(158, 24);
            this.paramGpio1GioSelCombo.TabIndex = 3;
            // 
            // logResultClearBtn
            // 
            this.logResultClearBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.logResultClearBtn.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.logResultClearBtn.Location = new System.Drawing.Point(10, 715);
            this.logResultClearBtn.Margin = new System.Windows.Forms.Padding(2);
            this.logResultClearBtn.Name = "logResultClearBtn";
            this.logResultClearBtn.Size = new System.Drawing.Size(54, 92);
            this.logResultClearBtn.TabIndex = 33;
            this.logResultClearBtn.Text = "Clear";
            this.logResultClearBtn.UseVisualStyleBackColor = false;
            this.logResultClearBtn.Click += new System.EventHandler(this.btnClearInsLog_Click);
            // 
            // logResultText
            // 
            this.logResultText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.logResultText.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.logResultText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logResultText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.logResultText.Location = new System.Drawing.Point(70, 715);
            this.logResultText.Margin = new System.Windows.Forms.Padding(4);
            this.logResultText.Multiline = true;
            this.logResultText.Name = "logResultText";
            this.logResultText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logResultText.Size = new System.Drawing.Size(808, 92);
            this.logResultText.TabIndex = 0;
            // 
            // paramTxSyncSoftInvertCheck
            // 
            this.paramTxSyncSoftInvertCheck.AutoSize = true;
            this.paramTxSyncSoftInvertCheck.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramTxSyncSoftInvertCheck.Location = new System.Drawing.Point(151, 29);
            this.paramTxSyncSoftInvertCheck.Margin = new System.Windows.Forms.Padding(2);
            this.paramTxSyncSoftInvertCheck.Name = "paramTxSyncSoftInvertCheck";
            this.paramTxSyncSoftInvertCheck.Size = new System.Drawing.Size(94, 21);
            this.paramTxSyncSoftInvertCheck.TabIndex = 12;
            this.paramTxSyncSoftInvertCheck.Text = "Soft Invert";
            this.paramTxSyncSoftInvertCheck.UseVisualStyleBackColor = true;
            this.paramTxSyncSoftInvertCheck.CheckedChanged += new System.EventHandler(this.syncLSBCheckbox_CheckedChanged);
            // 
            // paramCapIoText
            // 
            this.paramCapIoText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paramCapIoText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramCapIoText.Location = new System.Drawing.Point(154, 56);
            this.paramCapIoText.Margin = new System.Windows.Forms.Padding(2);
            this.paramCapIoText.Name = "paramCapIoText";
            this.paramCapIoText.Size = new System.Drawing.Size(69, 22);
            this.paramCapIoText.TabIndex = 29;
            this.paramCapIoText.Text = "33";
            // 
            // paramCapIoLabel
            // 
            this.paramCapIoLabel.AutoSize = true;
            this.paramCapIoLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramCapIoLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramCapIoLabel.Location = new System.Drawing.Point(154, 28);
            this.paramCapIoLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramCapIoLabel.Name = "paramCapIoLabel";
            this.paramCapIoLabel.Size = new System.Drawing.Size(97, 17);
            this.paramCapIoLabel.TabIndex = 28;
            this.paramCapIoLabel.Text = "CapIO(0~127)";
            // 
            // paramPwrLvText
            // 
            this.paramPwrLvText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paramPwrLvText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramPwrLvText.Location = new System.Drawing.Point(128, 178);
            this.paramPwrLvText.Margin = new System.Windows.Forms.Padding(2);
            this.paramPwrLvText.Name = "paramPwrLvText";
            this.paramPwrLvText.Size = new System.Drawing.Size(96, 22);
            this.paramPwrLvText.TabIndex = 26;
            this.paramPwrLvText.Text = "127";
            // 
            // paramDeviationText
            // 
            this.paramDeviationText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paramDeviationText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramDeviationText.Location = new System.Drawing.Point(10, 178);
            this.paramDeviationText.Margin = new System.Windows.Forms.Padding(4);
            this.paramDeviationText.Name = "paramDeviationText";
            this.paramDeviationText.Size = new System.Drawing.Size(70, 22);
            this.paramDeviationText.TabIndex = 21;
            this.paramDeviationText.Text = "24";
            // 
            // paramPwrLvLabel
            // 
            this.paramPwrLvLabel.AutoSize = true;
            this.paramPwrLvLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramPwrLvLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramPwrLvLabel.Location = new System.Drawing.Point(128, 150);
            this.paramPwrLvLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramPwrLvLabel.Name = "paramPwrLvLabel";
            this.paramPwrLvLabel.Size = new System.Drawing.Size(118, 17);
            this.paramPwrLvLabel.TabIndex = 18;
            this.paramPwrLvLabel.Text = "Power LV(0~127)";
            // 
            // paramTxSyncText
            // 
            this.paramTxSyncText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paramTxSyncText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramTxSyncText.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramTxSyncText.Location = new System.Drawing.Point(61, 28);
            this.paramTxSyncText.Margin = new System.Windows.Forms.Padding(4);
            this.paramTxSyncText.MaxLength = 8;
            this.paramTxSyncText.Name = "paramTxSyncText";
            this.paramTxSyncText.Size = new System.Drawing.Size(84, 23);
            this.paramTxSyncText.TabIndex = 11;
            this.paramTxSyncText.Text = "5A0FBE66";
            this.paramTxSyncText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rxSyncword_KeyPress);
            this.paramTxSyncText.Validating += new System.ComponentModel.CancelEventHandler(this.rxSyncword_Validating);
            // 
            // paramDataRateCombo
            // 
            this.paramDataRateCombo.BackColor = System.Drawing.Color.White;
            this.paramDataRateCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paramDataRateCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramDataRateCombo.FormattingEnabled = true;
            this.paramDataRateCombo.Location = new System.Drawing.Point(10, 116);
            this.paramDataRateCombo.Margin = new System.Windows.Forms.Padding(4);
            this.paramDataRateCombo.Name = "paramDataRateCombo";
            this.paramDataRateCombo.Size = new System.Drawing.Size(96, 24);
            this.paramDataRateCombo.TabIndex = 15;
            // 
            // paramBaseFreqText
            // 
            this.paramBaseFreqText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paramBaseFreqText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramBaseFreqText.Location = new System.Drawing.Point(128, 117);
            this.paramBaseFreqText.Margin = new System.Windows.Forms.Padding(4);
            this.paramBaseFreqText.Name = "paramBaseFreqText";
            this.paramBaseFreqText.Size = new System.Drawing.Size(96, 22);
            this.paramBaseFreqText.TabIndex = 2;
            this.paramBaseFreqText.Text = "915";
            // 
            // paramBaseFreqLabel
            // 
            this.paramBaseFreqLabel.AutoSize = true;
            this.paramBaseFreqLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramBaseFreqLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramBaseFreqLabel.Location = new System.Drawing.Point(128, 89);
            this.paramBaseFreqLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramBaseFreqLabel.Name = "paramBaseFreqLabel";
            this.paramBaseFreqLabel.Size = new System.Drawing.Size(113, 17);
            this.paramBaseFreqLabel.TabIndex = 1;
            this.paramBaseFreqLabel.Text = "Frequency(MHz)";
            // 
            // paramDeviationLabel
            // 
            this.paramDeviationLabel.AutoSize = true;
            this.paramDeviationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramDeviationLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramDeviationLabel.Location = new System.Drawing.Point(10, 150);
            this.paramDeviationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramDeviationLabel.Name = "paramDeviationLabel";
            this.paramDeviationLabel.Size = new System.Drawing.Size(111, 17);
            this.paramDeviationLabel.TabIndex = 7;
            this.paramDeviationLabel.Text = "Deviation(±KHz)";
            // 
            // paramModulationCombo
            // 
            this.paramModulationCombo.BackColor = System.Drawing.Color.White;
            this.paramModulationCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paramModulationCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramModulationCombo.FormattingEnabled = true;
            this.paramModulationCombo.Location = new System.Drawing.Point(10, 55);
            this.paramModulationCombo.Margin = new System.Windows.Forms.Padding(4);
            this.paramModulationCombo.Name = "paramModulationCombo";
            this.paramModulationCombo.Size = new System.Drawing.Size(96, 24);
            this.paramModulationCombo.TabIndex = 12;
            // 
            // paramModulationLabel
            // 
            this.paramModulationLabel.AutoSize = true;
            this.paramModulationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramModulationLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramModulationLabel.Location = new System.Drawing.Point(10, 28);
            this.paramModulationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramModulationLabel.Name = "paramModulationLabel";
            this.paramModulationLabel.Size = new System.Drawing.Size(77, 17);
            this.paramModulationLabel.TabIndex = 5;
            this.paramModulationLabel.Text = "Modulation";
            // 
            // paramDataRateLabel
            // 
            this.paramDataRateLabel.AutoSize = true;
            this.paramDataRateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramDataRateLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramDataRateLabel.Location = new System.Drawing.Point(10, 89);
            this.paramDataRateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramDataRateLabel.Name = "paramDataRateLabel";
            this.paramDataRateLabel.Size = new System.Drawing.Size(114, 17);
            this.paramDataRateLabel.TabIndex = 3;
            this.paramDataRateLabel.Text = "Data Rate(Kbps)";
            // 
            // paramPAConfigCombo
            // 
            this.paramPAConfigCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramPAConfigCombo.FormattingEnabled = true;
            this.paramPAConfigCombo.Location = new System.Drawing.Point(12, 177);
            this.paramPAConfigCombo.Margin = new System.Windows.Forms.Padding(4);
            this.paramPAConfigCombo.Name = "paramPAConfigCombo";
            this.paramPAConfigCombo.Size = new System.Drawing.Size(96, 24);
            this.paramPAConfigCombo.TabIndex = 28;
            // 
            // paramPAConfigLabel
            // 
            this.paramPAConfigLabel.AutoSize = true;
            this.paramPAConfigLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramPAConfigLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramPAConfigLabel.Location = new System.Drawing.Point(12, 150);
            this.paramPAConfigLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramPAConfigLabel.Name = "paramPAConfigLabel";
            this.paramPAConfigLabel.Size = new System.Drawing.Size(108, 17);
            this.paramPAConfigLabel.TabIndex = 26;
            this.paramPAConfigLabel.Text = "PA Config(dBm)";
            // 
            // btnTxSet
            // 
            this.btnTxSet.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnTxSet.Location = new System.Drawing.Point(84, 751);
            this.btnTxSet.Margin = new System.Windows.Forms.Padding(4);
            this.btnTxSet.Name = "btnTxSet";
            this.btnTxSet.Size = new System.Drawing.Size(205, 68);
            this.btnTxSet.TabIndex = 0;
            this.btnTxSet.Text = "Set";
            this.btnTxSet.UseVisualStyleBackColor = true;
            this.btnTxSet.Visible = false;
            this.btnTxSet.Click += new System.EventHandler(this.btnSetER4100_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileHdlrToolStripMenuItem,
            this.dvbConnToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1405, 32);
            this.menuStrip1.TabIndex = 41;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileHdlrToolStripMenuItem
            // 
            this.fileHdlrToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadFileMenuItem,
            this.loadParameterToolStripMenuItem,
            this.saveParameterToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileHdlrToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.fileHdlrToolStripMenuItem.Name = "fileHdlrToolStripMenuItem";
            this.fileHdlrToolStripMenuItem.Size = new System.Drawing.Size(44, 28);
            this.fileHdlrToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveInitValueToolStripMenuItem,
            this.sendDataToolStripMenuItem});
            this.saveToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Visible = false;
            // 
            // SaveInitValueToolStripMenuItem
            // 
            this.SaveInitValueToolStripMenuItem.Name = "SaveInitValueToolStripMenuItem";
            this.SaveInitValueToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.SaveInitValueToolStripMenuItem.Text = "Initial Data";
            this.SaveInitValueToolStripMenuItem.Click += new System.EventHandler(this.SaveInitValueToolStripMenuItem_Click);
            // 
            // sendDataToolStripMenuItem
            // 
            this.sendDataToolStripMenuItem.Name = "sendDataToolStripMenuItem";
            this.sendDataToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.sendDataToolStripMenuItem.Text = "Send Data...";
            this.sendDataToolStripMenuItem.Visible = false;
            // 
            // loadFileMenuItem
            // 
            this.loadFileMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.loadFileMenuItem.Name = "loadFileMenuItem";
            this.loadFileMenuItem.Size = new System.Drawing.Size(192, 26);
            this.loadFileMenuItem.Text = "Load";
            this.loadFileMenuItem.Visible = false;
            // 
            // loadParameterToolStripMenuItem
            // 
            this.loadParameterToolStripMenuItem.Name = "loadParameterToolStripMenuItem";
            this.loadParameterToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.loadParameterToolStripMenuItem.Text = "Load Parameter";
            this.loadParameterToolStripMenuItem.Click += new System.EventHandler(this.loadParameterToolStripMenuItem_Click);
            // 
            // saveParameterToolStripMenuItem
            // 
            this.saveParameterToolStripMenuItem.Name = "saveParameterToolStripMenuItem";
            this.saveParameterToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.saveParameterToolStripMenuItem.Text = "Save Parameter";
            this.saveParameterToolStripMenuItem.Click += new System.EventHandler(this.saveParameterToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(192, 26);
            this.quitToolStripMenuItem.Text = "Exit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click_1);
            // 
            // dvbConnToolStripMenuItem
            // 
            this.dvbConnToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.dvbConnToolStripMenuItem.Name = "dvbConnToolStripMenuItem";
            this.dvbConnToolStripMenuItem.Size = new System.Drawing.Size(125, 28);
            this.dvbConnToolStripMenuItem.Text = "DVB Connection";
            this.dvbConnToolStripMenuItem.Click += new System.EventHandler(this.comPortToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(59, 28);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click_1);
            // 
            // refreshComToolStripMenuItem
            // 
            this.refreshComToolStripMenuItem.Name = "refreshComToolStripMenuItem";
            this.refreshComToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.refreshComToolStripMenuItem.Text = "REFRESH";
            this.refreshComToolStripMenuItem.Click += new System.EventHandler(this.refreshComToolStripMenuItem_Click);
            // 
            // comPortToolStripComboBox
            // 
            this.comPortToolStripComboBox.Name = "comPortToolStripComboBox";
            this.comPortToolStripComboBox.Size = new System.Drawing.Size(121, 27);
            // 
            // connectComToolStripMenuItem
            // 
            this.connectComToolStripMenuItem.Name = "connectComToolStripMenuItem";
            this.connectComToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // paramZoneTab
            // 
            this.paramZoneTab.Controls.Add(this.pageParameter);
            this.paramZoneTab.Controls.Add(this.pageFreqTable);
            this.paramZoneTab.Controls.Add(this.pageTxPayload);
            this.paramZoneTab.Controls.Add(this.pageIntMode);
            this.paramZoneTab.Controls.Add(this.pageGpio);
            this.paramZoneTab.Controls.Add(this.pagePcrmu);
            this.paramZoneTab.ItemSize = new System.Drawing.Size(100, 24);
            this.paramZoneTab.Location = new System.Drawing.Point(10, 54);
            this.paramZoneTab.Margin = new System.Windows.Forms.Padding(4);
            this.paramZoneTab.Name = "paramZoneTab";
            this.paramZoneTab.SelectedIndex = 0;
            this.paramZoneTab.Size = new System.Drawing.Size(868, 264);
            this.paramZoneTab.TabIndex = 42;
            this.paramZoneTab.Tag = "";
            // 
            // pageParameter
            // 
            this.pageParameter.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pageParameter.Controls.Add(this.paramPktGroup);
            this.pageParameter.Controls.Add(this.paramModemGroup);
            this.pageParameter.Controls.Add(this.paramFreqAndPwrGroup);
            this.pageParameter.Location = new System.Drawing.Point(4, 28);
            this.pageParameter.Margin = new System.Windows.Forms.Padding(4);
            this.pageParameter.Name = "pageParameter";
            this.pageParameter.Padding = new System.Windows.Forms.Padding(4);
            this.pageParameter.Size = new System.Drawing.Size(860, 232);
            this.pageParameter.TabIndex = 0;
            this.pageParameter.Text = "RF PARAM";
            // 
            // paramPktGroup
            // 
            this.paramPktGroup.Controls.Add(this.paramSyncGroup);
            this.paramPktGroup.Controls.Add(this.paramPbLabel);
            this.paramPktGroup.Controls.Add(this.paramFIFOBitOrderGroup);
            this.paramPktGroup.Controls.Add(this.paramPbContentText);
            this.paramPktGroup.Controls.Add(this.paramPbLenText);
            this.paramPktGroup.Controls.Add(this.paramPbUnitLabel);
            this.paramPktGroup.Controls.Add(this.paramPbMultipLabel);
            this.paramPktGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramPktGroup.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.paramPktGroup.Location = new System.Drawing.Point(422, 9);
            this.paramPktGroup.Margin = new System.Windows.Forms.Padding(2);
            this.paramPktGroup.Name = "paramPktGroup";
            this.paramPktGroup.Padding = new System.Windows.Forms.Padding(2);
            this.paramPktGroup.Size = new System.Drawing.Size(431, 212);
            this.paramPktGroup.TabIndex = 37;
            this.paramPktGroup.TabStop = false;
            this.paramPktGroup.Text = "Packet";
            // 
            // paramSyncGroup
            // 
            this.paramSyncGroup.Controls.Add(this.paramRxSyncAllowBitErrLabel);
            this.paramSyncGroup.Controls.Add(this.paramRxSyncAllowBitErrText);
            this.paramSyncGroup.Controls.Add(this.paramRxSyncLabel);
            this.paramSyncGroup.Controls.Add(this.paramRxSyncAllowBitErrUnitLabel);
            this.paramSyncGroup.Controls.Add(this.paramTxSyncSoftInvertCheck);
            this.paramSyncGroup.Controls.Add(this.paramRxSyncActualLabel);
            this.paramSyncGroup.Controls.Add(this.paramTxSyncLabel);
            this.paramSyncGroup.Controls.Add(this.paramRxSyncSoftInvertCheck);
            this.paramSyncGroup.Controls.Add(this.paramTxSyncActualLabel);
            this.paramSyncGroup.Controls.Add(this.paramRxSyncText);
            this.paramSyncGroup.Controls.Add(this.paramTxSyncText);
            this.paramSyncGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramSyncGroup.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.paramSyncGroup.Location = new System.Drawing.Point(10, 81);
            this.paramSyncGroup.Margin = new System.Windows.Forms.Padding(2);
            this.paramSyncGroup.Name = "paramSyncGroup";
            this.paramSyncGroup.Padding = new System.Windows.Forms.Padding(2);
            this.paramSyncGroup.Size = new System.Drawing.Size(401, 119);
            this.paramSyncGroup.TabIndex = 35;
            this.paramSyncGroup.TabStop = false;
            this.paramSyncGroup.Text = "Syncword(4Bytes)";
            // 
            // paramRxSyncAllowBitErrLabel
            // 
            this.paramRxSyncAllowBitErrLabel.AutoSize = true;
            this.paramRxSyncAllowBitErrLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramRxSyncAllowBitErrLabel.Location = new System.Drawing.Point(11, 91);
            this.paramRxSyncAllowBitErrLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramRxSyncAllowBitErrLabel.Name = "paramRxSyncAllowBitErrLabel";
            this.paramRxSyncAllowBitErrLabel.Size = new System.Drawing.Size(96, 17);
            this.paramRxSyncAllowBitErrLabel.TabIndex = 26;
            this.paramRxSyncAllowBitErrLabel.Text = "Allow Bit Error";
            // 
            // paramPbMultipLabel
            // 
            this.paramPbMultipLabel.AutoSize = true;
            this.paramPbMultipLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramPbMultipLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramPbMultipLabel.Location = new System.Drawing.Point(55, 52);
            this.paramPbMultipLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramPbMultipLabel.Name = "paramPbMultipLabel";
            this.paramPbMultipLabel.Size = new System.Drawing.Size(14, 17);
            this.paramPbMultipLabel.TabIndex = 2;
            this.paramPbMultipLabel.Text = "x";
            // 
            // paramModemGroup
            // 
            this.paramModemGroup.Controls.Add(this.paramDeviationHelpBtn);
            this.paramModemGroup.Controls.Add(this.paramModulationCombo);
            this.paramModemGroup.Controls.Add(this.paramModulationLabel);
            this.paramModemGroup.Controls.Add(this.paramDeviationText);
            this.paramModemGroup.Controls.Add(this.paramDeviationLabel);
            this.paramModemGroup.Controls.Add(this.paramDataRateCombo);
            this.paramModemGroup.Controls.Add(this.paramDataRateLabel);
            this.paramModemGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramModemGroup.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.paramModemGroup.Location = new System.Drawing.Point(286, 9);
            this.paramModemGroup.Margin = new System.Windows.Forms.Padding(2);
            this.paramModemGroup.Name = "paramModemGroup";
            this.paramModemGroup.Padding = new System.Windows.Forms.Padding(2);
            this.paramModemGroup.Size = new System.Drawing.Size(125, 212);
            this.paramModemGroup.TabIndex = 36;
            this.paramModemGroup.TabStop = false;
            this.paramModemGroup.Text = "Modem";
            // 
            // paramDeviationHelpBtn
            // 
            this.paramDeviationHelpBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramDeviationHelpBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramDeviationHelpBtn.Location = new System.Drawing.Point(87, 178);
            this.paramDeviationHelpBtn.Name = "paramDeviationHelpBtn";
            this.paramDeviationHelpBtn.Size = new System.Drawing.Size(28, 23);
            this.paramDeviationHelpBtn.TabIndex = 22;
            this.paramDeviationHelpBtn.Text = "?";
            this.paramDeviationHelpBtn.UseVisualStyleBackColor = true;
            this.paramDeviationHelpBtn.Click += new System.EventHandler(this.paramDeviationHelpBtn_Click);
            // 
            // paramFreqAndPwrGroup
            // 
            this.paramFreqAndPwrGroup.Controls.Add(this.paramXtalGainLabel);
            this.paramFreqAndPwrGroup.Controls.Add(this.paramXtalGainCombo);
            this.paramFreqAndPwrGroup.Controls.Add(this.paramXtalHelpBtn);
            this.paramFreqAndPwrGroup.Controls.Add(this.paramFreqHelpBtn);
            this.paramFreqAndPwrGroup.Controls.Add(this.paramPwrHelpBtn);
            this.paramFreqAndPwrGroup.Controls.Add(this.paramCapIoText);
            this.paramFreqAndPwrGroup.Controls.Add(this.paramXtalCombo);
            this.paramFreqAndPwrGroup.Controls.Add(this.paramDvbBandLabel);
            this.paramFreqAndPwrGroup.Controls.Add(this.paramCapIoLabel);
            this.paramFreqAndPwrGroup.Controls.Add(this.paramPAConfigCombo);
            this.paramFreqAndPwrGroup.Controls.Add(this.paramPwrLvText);
            this.paramFreqAndPwrGroup.Controls.Add(this.paramDvbBandCombo);
            this.paramFreqAndPwrGroup.Controls.Add(this.paramBaseFreqLabel);
            this.paramFreqAndPwrGroup.Controls.Add(this.paramXtalLabel);
            this.paramFreqAndPwrGroup.Controls.Add(this.paramPwrLvLabel);
            this.paramFreqAndPwrGroup.Controls.Add(this.paramBaseFreqText);
            this.paramFreqAndPwrGroup.Controls.Add(this.paramPAConfigLabel);
            this.paramFreqAndPwrGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramFreqAndPwrGroup.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.paramFreqAndPwrGroup.Location = new System.Drawing.Point(6, 9);
            this.paramFreqAndPwrGroup.Margin = new System.Windows.Forms.Padding(2);
            this.paramFreqAndPwrGroup.Name = "paramFreqAndPwrGroup";
            this.paramFreqAndPwrGroup.Padding = new System.Windows.Forms.Padding(2);
            this.paramFreqAndPwrGroup.Size = new System.Drawing.Size(269, 212);
            this.paramFreqAndPwrGroup.TabIndex = 35;
            this.paramFreqAndPwrGroup.TabStop = false;
            this.paramFreqAndPwrGroup.Text = "Frequency and Power";
            // 
            // paramXtalGainLabel
            // 
            this.paramXtalGainLabel.AutoSize = true;
            this.paramXtalGainLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramXtalGainLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramXtalGainLabel.Location = new System.Drawing.Point(91, 28);
            this.paramXtalGainLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramXtalGainLabel.Name = "paramXtalGainLabel";
            this.paramXtalGainLabel.Size = new System.Drawing.Size(38, 17);
            this.paramXtalGainLabel.TabIndex = 37;
            this.paramXtalGainLabel.Text = "Gain";
            // 
            // paramXtalGainCombo
            // 
            this.paramXtalGainCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paramXtalGainCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramXtalGainCombo.FormattingEnabled = true;
            this.paramXtalGainCombo.Location = new System.Drawing.Point(91, 55);
            this.paramXtalGainCombo.Name = "paramXtalGainCombo";
            this.paramXtalGainCombo.Size = new System.Drawing.Size(47, 24);
            this.paramXtalGainCombo.TabIndex = 36;
            // 
            // paramXtalHelpBtn
            // 
            this.paramXtalHelpBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramXtalHelpBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramXtalHelpBtn.Location = new System.Drawing.Point(230, 56);
            this.paramXtalHelpBtn.Name = "paramXtalHelpBtn";
            this.paramXtalHelpBtn.Size = new System.Drawing.Size(28, 23);
            this.paramXtalHelpBtn.TabIndex = 35;
            this.paramXtalHelpBtn.Text = "?";
            this.paramXtalHelpBtn.UseVisualStyleBackColor = true;
            this.paramXtalHelpBtn.Click += new System.EventHandler(this.paramXtalHelpBtn_Click);
            // 
            // paramFreqHelpBtn
            // 
            this.paramFreqHelpBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramFreqHelpBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramFreqHelpBtn.Location = new System.Drawing.Point(231, 117);
            this.paramFreqHelpBtn.Name = "paramFreqHelpBtn";
            this.paramFreqHelpBtn.Size = new System.Drawing.Size(28, 23);
            this.paramFreqHelpBtn.TabIndex = 36;
            this.paramFreqHelpBtn.Text = "?";
            this.paramFreqHelpBtn.UseVisualStyleBackColor = true;
            this.paramFreqHelpBtn.Click += new System.EventHandler(this.paramFreqHelpBtn_Click);
            // 
            // paramPwrHelpBtn
            // 
            this.paramPwrHelpBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramPwrHelpBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramPwrHelpBtn.Location = new System.Drawing.Point(231, 178);
            this.paramPwrHelpBtn.Name = "paramPwrHelpBtn";
            this.paramPwrHelpBtn.Size = new System.Drawing.Size(28, 23);
            this.paramPwrHelpBtn.TabIndex = 37;
            this.paramPwrHelpBtn.Text = "?";
            this.paramPwrHelpBtn.UseVisualStyleBackColor = true;
            this.paramPwrHelpBtn.Click += new System.EventHandler(this.paramPwrHelpBtn_Click);
            // 
            // paramXtalCombo
            // 
            this.paramXtalCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramXtalCombo.FormattingEnabled = true;
            this.paramXtalCombo.Location = new System.Drawing.Point(12, 55);
            this.paramXtalCombo.Margin = new System.Windows.Forms.Padding(2);
            this.paramXtalCombo.Name = "paramXtalCombo";
            this.paramXtalCombo.Size = new System.Drawing.Size(63, 24);
            this.paramXtalCombo.TabIndex = 33;
            this.paramXtalCombo.SelectedIndexChanged += new System.EventHandler(this.xtalCombo_SelectedIndexChanged);
            // 
            // paramDvbBandLabel
            // 
            this.paramDvbBandLabel.AutoSize = true;
            this.paramDvbBandLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramDvbBandLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramDvbBandLabel.Location = new System.Drawing.Point(12, 89);
            this.paramDvbBandLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramDvbBandLabel.Name = "paramDvbBandLabel";
            this.paramDvbBandLabel.Size = new System.Drawing.Size(111, 17);
            this.paramDvbBandLabel.TabIndex = 29;
            this.paramDvbBandLabel.Text = "DVB Band(MHz)";
            // 
            // paramDvbBandCombo
            // 
            this.paramDvbBandCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramDvbBandCombo.FormattingEnabled = true;
            this.paramDvbBandCombo.Location = new System.Drawing.Point(12, 116);
            this.paramDvbBandCombo.Margin = new System.Windows.Forms.Padding(2);
            this.paramDvbBandCombo.Name = "paramDvbBandCombo";
            this.paramDvbBandCombo.Size = new System.Drawing.Size(96, 24);
            this.paramDvbBandCombo.TabIndex = 32;
            this.paramDvbBandCombo.SelectedIndexChanged += new System.EventHandler(this.DVBBandCombo_SelectedIndexChanged);
            // 
            // paramXtalLabel
            // 
            this.paramXtalLabel.AutoSize = true;
            this.paramXtalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramXtalLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramXtalLabel.Location = new System.Drawing.Point(12, 28);
            this.paramXtalLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.paramXtalLabel.Name = "paramXtalLabel";
            this.paramXtalLabel.Size = new System.Drawing.Size(70, 17);
            this.paramXtalLabel.TabIndex = 34;
            this.paramXtalLabel.Text = "Xtal(MHz)";
            // 
            // pageFreqTable
            // 
            this.pageFreqTable.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pageFreqTable.Controls.Add(this.paramFreqTableGroup);
            this.pageFreqTable.Location = new System.Drawing.Point(4, 28);
            this.pageFreqTable.Margin = new System.Windows.Forms.Padding(4);
            this.pageFreqTable.Name = "pageFreqTable";
            this.pageFreqTable.Padding = new System.Windows.Forms.Padding(4);
            this.pageFreqTable.Size = new System.Drawing.Size(860, 232);
            this.pageFreqTable.TabIndex = 1;
            this.pageFreqTable.Text = "FREQ TABLE";
            // 
            // pageTxPayload
            // 
            this.pageTxPayload.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pageTxPayload.Controls.Add(this.paramTxFifoModeTransHelpLabel);
            this.pageTxPayload.Controls.Add(this.paramTxFifoMode802HelpLabel);
            this.pageTxPayload.Controls.Add(this.paramTxFifoModePreSyncHelpLabel);
            this.pageTxPayload.Controls.Add(this.paramTxFifoModeNoticeLabel);
            this.pageTxPayload.Controls.Add(this.paramTxFifoModeGroup);
            this.pageTxPayload.Location = new System.Drawing.Point(4, 28);
            this.pageTxPayload.Margin = new System.Windows.Forms.Padding(4);
            this.pageTxPayload.Name = "pageTxPayload";
            this.pageTxPayload.Padding = new System.Windows.Forms.Padding(4);
            this.pageTxPayload.Size = new System.Drawing.Size(860, 232);
            this.pageTxPayload.TabIndex = 2;
            this.pageTxPayload.Text = "TX FIFO MODE";
            // 
            // paramTxFifoModeTransHelpLabel
            // 
            this.paramTxFifoModeTransHelpLabel.BackColor = System.Drawing.Color.OldLace;
            this.paramTxFifoModeTransHelpLabel.Location = new System.Drawing.Point(231, 51);
            this.paramTxFifoModeTransHelpLabel.Name = "paramTxFifoModeTransHelpLabel";
            this.paramTxFifoModeTransHelpLabel.Size = new System.Drawing.Size(614, 45);
            this.paramTxFifoModeTransHelpLabel.TabIndex = 5;
            this.paramTxFifoModeTransHelpLabel.Text = "[Transparent] : sends the FIFO data as-is (nothing added) -user must put preamble" +
    " and Syncword into the FIFO themselves (if needed).\r\n";
            // 
            // paramTxFifoMode802HelpLabel
            // 
            this.paramTxFifoMode802HelpLabel.BackColor = System.Drawing.Color.OldLace;
            this.paramTxFifoMode802HelpLabel.Location = new System.Drawing.Point(231, 107);
            this.paramTxFifoMode802HelpLabel.Name = "paramTxFifoMode802HelpLabel";
            this.paramTxFifoMode802HelpLabel.Size = new System.Drawing.Size(614, 49);
            this.paramTxFifoMode802HelpLabel.TabIndex = 5;
            this.paramTxFifoMode802HelpLabel.Text = "[802.15.4] : same auto Preamble/Syncword as above, plus HW inserts a PHR(length, " +
    "<=127) after Syncword and appends CRC after the payload -user must include MHR w" +
    "ithin the payload.";
            // 
            // paramTxFifoModePreSyncHelpLabel
            // 
            this.paramTxFifoModePreSyncHelpLabel.BackColor = System.Drawing.Color.OldLace;
            this.paramTxFifoModePreSyncHelpLabel.Location = new System.Drawing.Point(231, 8);
            this.paramTxFifoModePreSyncHelpLabel.Name = "paramTxFifoModePreSyncHelpLabel";
            this.paramTxFifoModePreSyncHelpLabel.Size = new System.Drawing.Size(614, 32);
            this.paramTxFifoModePreSyncHelpLabel.TabIndex = 5;
            this.paramTxFifoModePreSyncHelpLabel.Text = "[Transparent + Presync] : HW auto-adds Preamble + Syncword before the FIFO data.\r" +
    "\n";
            // 
            // paramTxFifoModeNoticeLabel
            // 
            this.paramTxFifoModeNoticeLabel.BackColor = System.Drawing.Color.Silver;
            this.paramTxFifoModeNoticeLabel.Location = new System.Drawing.Point(231, 167);
            this.paramTxFifoModeNoticeLabel.Name = "paramTxFifoModeNoticeLabel";
            this.paramTxFifoModeNoticeLabel.Size = new System.Drawing.Size(615, 51);
            this.paramTxFifoModeNoticeLabel.TabIndex = 5;
            this.paramTxFifoModeNoticeLabel.Text = "RX FIFO mode is NOT set through this GUI — the firmware decides it at runtime fro" +
    "m the OpCode passed when RX is triggered by TRx_RX_Trigger(...)\r\n\r\n";
            // 
            // pageIntMode
            // 
            this.pageIntMode.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pageIntMode.Controls.Add(this.paramIntModeGroup);
            this.pageIntMode.Location = new System.Drawing.Point(4, 28);
            this.pageIntMode.Margin = new System.Windows.Forms.Padding(4);
            this.pageIntMode.Name = "pageIntMode";
            this.pageIntMode.Padding = new System.Windows.Forms.Padding(4);
            this.pageIntMode.Size = new System.Drawing.Size(860, 232);
            this.pageIntMode.TabIndex = 3;
            this.pageIntMode.Text = "INTERRUPT";
            // 
            // pageGpio
            // 
            this.pageGpio.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pageGpio.Controls.Add(this.paramGpioHelpBtn);
            this.pageGpio.Controls.Add(this.paramGpio1Group);
            this.pageGpio.Controls.Add(this.paramGpio0Group);
            this.pageGpio.Location = new System.Drawing.Point(4, 28);
            this.pageGpio.Margin = new System.Windows.Forms.Padding(4);
            this.pageGpio.Name = "pageGpio";
            this.pageGpio.Padding = new System.Windows.Forms.Padding(4);
            this.pageGpio.Size = new System.Drawing.Size(860, 232);
            this.pageGpio.TabIndex = 4;
            this.pageGpio.Text = "GPIO";
            // 
            // paramGpioHelpBtn
            // 
            this.paramGpioHelpBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paramGpioHelpBtn.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramGpioHelpBtn.Location = new System.Drawing.Point(440, 8);
            this.paramGpioHelpBtn.Name = "paramGpioHelpBtn";
            this.paramGpioHelpBtn.Size = new System.Drawing.Size(28, 23);
            this.paramGpioHelpBtn.TabIndex = 38;
            this.paramGpioHelpBtn.Text = "?";
            this.paramGpioHelpBtn.UseVisualStyleBackColor = true;
            this.paramGpioHelpBtn.Click += new System.EventHandler(this.paramGpioHelpBtn_Click);
            // 
            // paramGpio1Group
            // 
            this.paramGpio1Group.Controls.Add(this.paramGpio1GIoInvCheck);
            this.paramGpio1Group.Controls.Add(this.paramGpio1GIoSelLabel);
            this.paramGpio1Group.Controls.Add(this.paramGpio1PullCtrlLabel);
            this.paramGpio1Group.Controls.Add(this.paramGpio1ModeLabel);
            this.paramGpio1Group.Controls.Add(this.paramGpio1PullCtrlCombo);
            this.paramGpio1Group.Controls.Add(this.paramGpio1ModeCombo);
            this.paramGpio1Group.Controls.Add(this.paramGpio1GioSelCombo);
            this.paramGpio1Group.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.paramGpio1Group.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.paramGpio1Group.Location = new System.Drawing.Point(228, 8);
            this.paramGpio1Group.Margin = new System.Windows.Forms.Padding(4);
            this.paramGpio1Group.Name = "paramGpio1Group";
            this.paramGpio1Group.Padding = new System.Windows.Forms.Padding(4);
            this.paramGpio1Group.Size = new System.Drawing.Size(203, 211);
            this.paramGpio1Group.TabIndex = 11;
            this.paramGpio1Group.TabStop = false;
            this.paramGpio1Group.Text = "GPIO1";
            // 
            // paramGpio1GIoInvCheck
            // 
            this.paramGpio1GIoInvCheck.AutoSize = true;
            this.paramGpio1GIoInvCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramGpio1GIoInvCheck.ForeColor = System.Drawing.Color.Black;
            this.paramGpio1GIoInvCheck.Location = new System.Drawing.Point(100, 81);
            this.paramGpio1GIoInvCheck.Name = "paramGpio1GIoInvCheck";
            this.paramGpio1GIoInvCheck.Size = new System.Drawing.Size(76, 21);
            this.paramGpio1GIoInvCheck.TabIndex = 10;
            this.paramGpio1GIoInvCheck.Text = "Inverse";
            this.paramGpio1GIoInvCheck.UseVisualStyleBackColor = true;
            // 
            // paramGpio1GIoSelLabel
            // 
            this.paramGpio1GIoSelLabel.AutoSize = true;
            this.paramGpio1GIoSelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramGpio1GIoSelLabel.ForeColor = System.Drawing.Color.Black;
            this.paramGpio1GIoSelLabel.Location = new System.Drawing.Point(18, 83);
            this.paramGpio1GIoSelLabel.Name = "paramGpio1GIoSelLabel";
            this.paramGpio1GIoSelLabel.Size = new System.Drawing.Size(76, 17);
            this.paramGpio1GIoSelLabel.TabIndex = 9;
            this.paramGpio1GIoSelLabel.Text = "GIO Select";
            // 
            // paramGpio1PullCtrlLabel
            // 
            this.paramGpio1PullCtrlLabel.AutoSize = true;
            this.paramGpio1PullCtrlLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramGpio1PullCtrlLabel.ForeColor = System.Drawing.Color.Black;
            this.paramGpio1PullCtrlLabel.Location = new System.Drawing.Point(18, 138);
            this.paramGpio1PullCtrlLabel.Name = "paramGpio1PullCtrlLabel";
            this.paramGpio1PullCtrlLabel.Size = new System.Drawing.Size(80, 17);
            this.paramGpio1PullCtrlLabel.TabIndex = 9;
            this.paramGpio1PullCtrlLabel.Text = "Pull Control";
            // 
            // paramGpio1ModeLabel
            // 
            this.paramGpio1ModeLabel.AutoSize = true;
            this.paramGpio1ModeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramGpio1ModeLabel.ForeColor = System.Drawing.Color.Black;
            this.paramGpio1ModeLabel.Location = new System.Drawing.Point(18, 28);
            this.paramGpio1ModeLabel.Name = "paramGpio1ModeLabel";
            this.paramGpio1ModeLabel.Size = new System.Drawing.Size(180, 17);
            this.paramGpio1ModeLabel.TabIndex = 9;
            this.paramGpio1ModeLabel.Text = "Mode(Default is SHD input)";
            // 
            // paramGpio1PullCtrlCombo
            // 
            this.paramGpio1PullCtrlCombo.BackColor = System.Drawing.Color.White;
            this.paramGpio1PullCtrlCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paramGpio1PullCtrlCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramGpio1PullCtrlCombo.ForeColor = System.Drawing.Color.Black;
            this.paramGpio1PullCtrlCombo.FormattingEnabled = true;
            this.paramGpio1PullCtrlCombo.Location = new System.Drawing.Point(18, 162);
            this.paramGpio1PullCtrlCombo.Margin = new System.Windows.Forms.Padding(4);
            this.paramGpio1PullCtrlCombo.Name = "paramGpio1PullCtrlCombo";
            this.paramGpio1PullCtrlCombo.Size = new System.Drawing.Size(158, 24);
            this.paramGpio1PullCtrlCombo.TabIndex = 1;
            // 
            // paramGpio1ModeCombo
            // 
            this.paramGpio1ModeCombo.BackColor = System.Drawing.Color.White;
            this.paramGpio1ModeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.paramGpio1ModeCombo.Enabled = false;
            this.paramGpio1ModeCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramGpio1ModeCombo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.paramGpio1ModeCombo.FormattingEnabled = true;
            this.paramGpio1ModeCombo.Location = new System.Drawing.Point(18, 52);
            this.paramGpio1ModeCombo.Margin = new System.Windows.Forms.Padding(4);
            this.paramGpio1ModeCombo.Name = "paramGpio1ModeCombo";
            this.paramGpio1ModeCombo.Size = new System.Drawing.Size(158, 24);
            this.paramGpio1ModeCombo.TabIndex = 3;
            this.paramGpio1ModeCombo.SelectedIndexChanged += new System.EventHandler(this.paramGpio1ModeCombo_SelectedIndexChanged);
            // 
            // pagePcrmu
            // 
            this.pagePcrmu.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pagePcrmu.Controls.Add(this.paramPcrShdEnableHelpLabel);
            this.pagePcrmu.Controls.Add(this.paramPcrmuGroup);
            this.pagePcrmu.Controls.Add(this.paramDataFormatGroup);
            this.pagePcrmu.Location = new System.Drawing.Point(4, 28);
            this.pagePcrmu.Margin = new System.Windows.Forms.Padding(4);
            this.pagePcrmu.Name = "pagePcrmu";
            this.pagePcrmu.Padding = new System.Windows.Forms.Padding(4);
            this.pagePcrmu.Size = new System.Drawing.Size(860, 232);
            this.pagePcrmu.TabIndex = 5;
            this.pagePcrmu.Text = "PCR";
            // 
            // paramPcrShdEnableHelpLabel
            // 
            this.paramPcrShdEnableHelpLabel.BackColor = System.Drawing.Color.LightSteelBlue;
            this.paramPcrShdEnableHelpLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.paramPcrShdEnableHelpLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.paramPcrShdEnableHelpLabel.Location = new System.Drawing.Point(7, 125);
            this.paramPcrShdEnableHelpLabel.Name = "paramPcrShdEnableHelpLabel";
            this.paramPcrShdEnableHelpLabel.Size = new System.Drawing.Size(396, 24);
            this.paramPcrShdEnableHelpLabel.TabIndex = 3;
            this.paramPcrShdEnableHelpLabel.Text = "*Shutdown is triggered via GPIO1 (must be in Default Mode)*";
            // 
            // regTableGenBtn
            // 
            this.regTableGenBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.regTableGenBtn.BackColor = System.Drawing.Color.White;
            this.regTableGenBtn.Location = new System.Drawing.Point(1191, 745);
            this.regTableGenBtn.Margin = new System.Windows.Forms.Padding(4);
            this.regTableGenBtn.Name = "regTableGenBtn";
            this.regTableGenBtn.Size = new System.Drawing.Size(96, 62);
            this.regTableGenBtn.TabIndex = 44;
            this.regTableGenBtn.Text = "Generate";
            this.regTableGenBtn.UseVisualStyleBackColor = false;
            this.regTableGenBtn.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // regTableDefaultBtn
            // 
            this.regTableDefaultBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.regTableDefaultBtn.BackColor = System.Drawing.Color.White;
            this.regTableDefaultBtn.Location = new System.Drawing.Point(1291, 745);
            this.regTableDefaultBtn.Margin = new System.Windows.Forms.Padding(4);
            this.regTableDefaultBtn.Name = "regTableDefaultBtn";
            this.regTableDefaultBtn.Size = new System.Drawing.Size(96, 62);
            this.regTableDefaultBtn.TabIndex = 47;
            this.regTableDefaultBtn.Text = "Default";
            this.regTableDefaultBtn.UseVisualStyleBackColor = false;
            this.regTableDefaultBtn.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // regTableTopicGroup
            // 
            this.regTableTopicGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.regTableTopicGroup.BackColor = System.Drawing.Color.Transparent;
            this.regTableTopicGroup.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold);
            this.regTableTopicGroup.ForeColor = System.Drawing.Color.Navy;
            this.regTableTopicGroup.Location = new System.Drawing.Point(891, 36);
            this.regTableTopicGroup.Margin = new System.Windows.Forms.Padding(4);
            this.regTableTopicGroup.Name = "regTableTopicGroup";
            this.regTableTopicGroup.Padding = new System.Windows.Forms.Padding(4);
            this.regTableTopicGroup.Size = new System.Drawing.Size(501, 669);
            this.regTableTopicGroup.TabIndex = 46;
            this.regTableTopicGroup.TabStop = false;
            this.regTableTopicGroup.Text = "REG Table";
            // 
            // dvbTstTopicLabel
            // 
            this.dvbTstTopicLabel.AutoSize = true;
            this.dvbTstTopicLabel.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dvbTstTopicLabel.ForeColor = System.Drawing.Color.Navy;
            this.dvbTstTopicLabel.Location = new System.Drawing.Point(11, 322);
            this.dvbTstTopicLabel.Name = "dvbTstTopicLabel";
            this.dvbTstTopicLabel.Size = new System.Drawing.Size(89, 21);
            this.dvbTstTopicLabel.TabIndex = 48;
            this.dvbTstTopicLabel.Text = "DVB Tester";
            // 
            // paramTopicLabel
            // 
            this.paramTopicLabel.AutoSize = true;
            this.paramTopicLabel.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold);
            this.paramTopicLabel.ForeColor = System.Drawing.Color.Navy;
            this.paramTopicLabel.Location = new System.Drawing.Point(10, 32);
            this.paramTopicLabel.Name = "paramTopicLabel";
            this.paramTopicLabel.Size = new System.Drawing.Size(85, 21);
            this.paramTopicLabel.TabIndex = 48;
            this.paramTopicLabel.Text = "Parameter";
            // 
            // regTableDumpDvbRegBtn
            // 
            this.regTableDumpDvbRegBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.regTableDumpDvbRegBtn.BackColor = System.Drawing.Color.White;
            this.regTableDumpDvbRegBtn.Location = new System.Drawing.Point(891, 745);
            this.regTableDumpDvbRegBtn.Name = "regTableDumpDvbRegBtn";
            this.regTableDumpDvbRegBtn.Size = new System.Drawing.Size(96, 62);
            this.regTableDumpDvbRegBtn.TabIndex = 49;
            this.regTableDumpDvbRegBtn.Text = "Dump\r\nAll\r\nDVB Register";
            this.regTableDumpDvbRegBtn.UseVisualStyleBackColor = false;
            this.regTableDumpDvbRegBtn.Click += new System.EventHandler(this.regTableDumpDvbRegBtn_Click);
            // 
            // regTableWriteDvbRegBtn
            // 
            this.regTableWriteDvbRegBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.regTableWriteDvbRegBtn.BackColor = System.Drawing.Color.White;
            this.regTableWriteDvbRegBtn.Location = new System.Drawing.Point(991, 745);
            this.regTableWriteDvbRegBtn.Name = "regTableWriteDvbRegBtn";
            this.regTableWriteDvbRegBtn.Size = new System.Drawing.Size(96, 62);
            this.regTableWriteDvbRegBtn.TabIndex = 50;
            this.regTableWriteDvbRegBtn.Text = "Write\r\nAll\r\nDVB Register";
            this.regTableWriteDvbRegBtn.UseVisualStyleBackColor = false;
            this.regTableWriteDvbRegBtn.Click += new System.EventHandler(this.regTableWriteDvbRegBtn_Click);
            // 
            // regTableIncInitArrayCheck
            // 
            this.regTableIncInitArrayCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.regTableIncInitArrayCheck.AutoSize = true;
            this.regTableIncInitArrayCheck.Checked = true;
            this.regTableIncInitArrayCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.regTableIncInitArrayCheck.Location = new System.Drawing.Point(1194, 716);
            this.regTableIncInitArrayCheck.Name = "regTableIncInitArrayCheck";
            this.regTableIncInitArrayCheck.Size = new System.Drawing.Size(136, 22);
            this.regTableIncInitArrayCheck.TabIndex = 51;
            this.regTableIncInitArrayCheck.Text = "Include Init Array";
            this.regTableIncInitArrayCheck.UseVisualStyleBackColor = true;
            // 
            // dvbTstSubTopicLabel
            // 
            this.dvbTstSubTopicLabel.AutoSize = true;
            this.dvbTstSubTopicLabel.BackColor = System.Drawing.Color.Transparent;
            this.dvbTstSubTopicLabel.Font = new System.Drawing.Font("Calibri", 10F);
            this.dvbTstSubTopicLabel.ForeColor = System.Drawing.Color.Navy;
            this.dvbTstSubTopicLabel.Location = new System.Drawing.Point(235, 322);
            this.dvbTstSubTopicLabel.Name = "dvbTstSubTopicLabel";
            this.dvbTstSubTopicLabel.Size = new System.Drawing.Size(348, 21);
            this.dvbTstSubTopicLabel.TabIndex = 52;
            this.dvbTstSubTopicLabel.Text = "(Only support FIFO mode : Transparent+Presync)";
            // 
            // regTableWriteSelDvbRegBtn
            // 
            this.regTableWriteSelDvbRegBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.regTableWriteSelDvbRegBtn.BackColor = System.Drawing.Color.White;
            this.regTableWriteSelDvbRegBtn.Location = new System.Drawing.Point(1091, 745);
            this.regTableWriteSelDvbRegBtn.Name = "regTableWriteSelDvbRegBtn";
            this.regTableWriteSelDvbRegBtn.Size = new System.Drawing.Size(96, 62);
            this.regTableWriteSelDvbRegBtn.TabIndex = 50;
            this.regTableWriteSelDvbRegBtn.Text = "Write Selected DVB Register";
            this.regTableWriteSelDvbRegBtn.UseVisualStyleBackColor = false;
            this.regTableWriteSelDvbRegBtn.Click += new System.EventHandler(this.regTableWriteSelDvbRegBtn_Click);
            // 
            // dvbTstConnStateLabel
            // 
            this.dvbTstConnStateLabel.AutoSize = true;
            this.dvbTstConnStateLabel.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dvbTstConnStateLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.dvbTstConnStateLabel.Location = new System.Drawing.Point(102, 322);
            this.dvbTstConnStateLabel.Name = "dvbTstConnStateLabel";
            this.dvbTstConnStateLabel.Size = new System.Drawing.Size(118, 21);
            this.dvbTstConnStateLabel.TabIndex = 48;
            this.dvbTstConnStateLabel.Text = "[Disconnected]";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(1405, 838);
            this.Controls.Add(this.dvbTstSubTopicLabel);
            this.Controls.Add(this.regTableIncInitArrayCheck);
            this.Controls.Add(this.regTableWriteSelDvbRegBtn);
            this.Controls.Add(this.regTableWriteDvbRegBtn);
            this.Controls.Add(this.regTableDumpDvbRegBtn);
            this.Controls.Add(this.paramTopicLabel);
            this.Controls.Add(this.dvbTstConnStateLabel);
            this.Controls.Add(this.dvbTstTopicLabel);
            this.Controls.Add(this.regTableDefaultBtn);
            this.Controls.Add(this.regTableTopicGroup);
            this.Controls.Add(this.regTableGenBtn);
            this.Controls.Add(this.logResultClearBtn);
            this.Controls.Add(this.paramZoneTab);
            this.Controls.Add(this.logResultText);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.dvbTstZoneTab);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnTxSet);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "ER41xx RF Test Tool";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.dvbTstZoneTab.ResumeLayout(false);
            this.dvbTstPktTxPage.ResumeLayout(false);
            this.dvbTstPktTxPage.PerformLayout();
            this.dvbTstPktTxCntGroup.ResumeLayout(false);
            this.dvbTstPktTxCntGroup.PerformLayout();
            this.dvbTstPktTxContentGroup.ResumeLayout(false);
            this.dvbTstPktTxContentGroup.PerformLayout();
            this.dvbTstContiTxPage.ResumeLayout(false);
            this.dvbTstContiTxPatternGroup.ResumeLayout(false);
            this.dvbTstContiTxPatternGroup.PerformLayout();
            this.dvbTstContiTxModeGroup.ResumeLayout(false);
            this.dvbTstContiTxModeGroup.PerformLayout();
            this.dvbTstPktRxPage.ResumeLayout(false);
            this.dvbTstPktRxPage.PerformLayout();
            this.dvbTstPktRxCntGroup.ResumeLayout(false);
            this.dvbTstPktRxCntGroup.PerformLayout();
            this.dvbTstPktRxParserGroup.ResumeLayout(false);
            this.dvbTstPktRxParserGroup.PerformLayout();
            this.dvbTstStatePage.ResumeLayout(false);
            this.dvbTstStateGroup.ResumeLayout(false);
            this.dvbTstStateGroup.PerformLayout();
            this.paramFreqTableGroup.ResumeLayout(false);
            this.paramFreqTableGroup.PerformLayout();
            this.paramFIFOBitOrderGroup.ResumeLayout(false);
            this.paramFIFOBitOrderGroup.PerformLayout();
            this.paramTxFifoModeGroup.ResumeLayout(false);
            this.paramTxFifoModeGroup.PerformLayout();
            this.paramPcrmuGroup.ResumeLayout(false);
            this.paramPcrmuGroup.PerformLayout();
            this.paramDataFormatGroup.ResumeLayout(false);
            this.paramDataFormatGroup.PerformLayout();
            this.paramIntModeGroup.ResumeLayout(false);
            this.paramIntModeGroup.PerformLayout();
            this.paramIntModeTxAemptyGroup.ResumeLayout(false);
            this.paramIntModeTxAemptyGroup.PerformLayout();
            this.paramIntModeRxAfullGroup.ResumeLayout(false);
            this.paramIntModeRxAfullGroup.PerformLayout();
            this.paramGpio0Group.ResumeLayout(false);
            this.paramGpio0Group.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.paramZoneTab.ResumeLayout(false);
            this.pageParameter.ResumeLayout(false);
            this.paramPktGroup.ResumeLayout(false);
            this.paramPktGroup.PerformLayout();
            this.paramSyncGroup.ResumeLayout(false);
            this.paramSyncGroup.PerformLayout();
            this.paramModemGroup.ResumeLayout(false);
            this.paramModemGroup.PerformLayout();
            this.paramFreqAndPwrGroup.ResumeLayout(false);
            this.paramFreqAndPwrGroup.PerformLayout();
            this.pageFreqTable.ResumeLayout(false);
            this.pageTxPayload.ResumeLayout(false);
            this.pageIntMode.ResumeLayout(false);
            this.pageGpio.ResumeLayout(false);
            this.paramGpio1Group.ResumeLayout(false);
            this.paramGpio1Group.PerformLayout();
            this.pagePcrmu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statuslabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusTimeLabel;
        private System.Windows.Forms.Timer statustimer;
        private System.Windows.Forms.Timer autoSendtimer;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton recHexRadiobtn;
        private System.Windows.Forms.RadioButton recStrRadiobtn;
        private System.Windows.Forms.Label label1;
        
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button cleartxsendbtn;
        private System.Windows.Forms.RadioButton sendHexRadiobtn;
        private System.Windows.Forms.RadioButton sendStrRadiobtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.CheckBox addCRCcbx;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox sendIntervalTimetbx;
        private System.Windows.Forms.CheckBox autoReplyCbx;
        private System.Windows.Forms.CheckBox autoSendcbx;
        private System.Windows.Forms.TabControl dvbTstZoneTab;
        private System.Windows.Forms.TabPage dvbTstContiTxPage;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TabPage dvbTstPktTxPage;
        private System.Windows.Forms.TextBox logResultText;
        private System.Windows.Forms.Button btnTxSet;
        private System.Windows.Forms.Label paramBaseFreqLabel;
        private System.Windows.Forms.Label paramDataRateLabel;
        private System.Windows.Forms.Label paramDeviationLabel;
        private System.Windows.Forms.Label paramModulationLabel;
        private System.Windows.Forms.ComboBox paramModulationCombo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dvbConnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshComToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox comPortToolStripComboBox;
        private System.Windows.Forms.ToolStripMenuItem connectComToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileHdlrToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveInitValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TextBox paramTxSyncText;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.ComboBox paramDataRateCombo;
        private System.IO.Ports.SerialPort serialPort3;
        private System.Windows.Forms.TabPage dvbTstPktRxPage;
        private System.Windows.Forms.Label paramPwrLvLabel;
        private System.Windows.Forms.TextBox paramDeviationText;
        private System.Windows.Forms.Button dvbTstPktTxTestBtn;
        private System.Windows.Forms.Label paramPAConfigLabel;
        private System.Windows.Forms.ComboBox paramPAConfigCombo;
        private System.Windows.Forms.Button logResultClearBtn;
        private System.Windows.Forms.TabControl paramZoneTab;
        private System.Windows.Forms.TabPage pageParameter;
        private System.Windows.Forms.TabPage pageFreqTable;
        private System.Windows.Forms.TabPage pageTxPayload;
        private System.Windows.Forms.TabPage pageIntMode;
        private System.Windows.Forms.TabPage pageGpio;
        private System.Windows.Forms.TabPage pagePcrmu;
        private System.Windows.Forms.Label paramDvbBandLabel;
        private System.Windows.Forms.ComboBox paramDvbBandCombo;
        private System.Windows.Forms.Label paramXtalLabel;
        private System.Windows.Forms.ComboBox paramXtalCombo;
        private System.Windows.Forms.TextBox paramPwrLvText;
        private System.Windows.Forms.TextBox paramCapIoText;
        private System.Windows.Forms.Label paramCapIoLabel;
        private System.Windows.Forms.Label dvbTstPktTxMaxPayloadNoSeqLabel;
        private System.Windows.Forms.Label dvbTstPktTxMaxPayloadSeqLabel;
        private System.Windows.Forms.CheckBox paramTxSyncSoftInvertCheck;
        private System.Windows.Forms.TabPage dvbTstStatePage;
        private System.Windows.Forms.GroupBox dvbTstStateGroup;
        private System.Windows.Forms.Button dvbTstStateSetBtn;
        private System.Windows.Forms.RadioButton dvbTstStateDeepSleepRadioBtn;
        private System.Windows.Forms.RadioButton dvbTstStateIdleRadioBtn;
        private System.Windows.Forms.RadioButton dvbTstStateShutdownRadioBtn;
        private System.Windows.Forms.Button regTableGenBtn;
        private System.Windows.Forms.ToolStripMenuItem loadParameterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveParameterToolStripMenuItem;
        private System.Windows.Forms.TabPage regWrPage;
        private System.Windows.Forms.GroupBox paramFreqTableGroup;
        private System.Windows.Forms.TextBox paramFreqTableInputText;
        private System.Windows.Forms.Label paramFreqTableUnitLabel;
        private System.Windows.Forms.Button paramFreqTableAddBtn;
        private System.Windows.Forms.ListView paramFreqTableList;
        private System.Windows.Forms.Button paramFreqTableDeleteBtn;
        private System.Windows.Forms.Label paramPbLabel;
        private System.Windows.Forms.TextBox paramPbContentText;
        private System.Windows.Forms.Label paramPbUnitLabel;
        private System.Windows.Forms.TextBox paramPbLenText;
        private System.Windows.Forms.Label paramTxSyncLabel;
        private System.Windows.Forms.Label paramTxSyncActualLabel;
        private System.Windows.Forms.Label paramRxSyncLabel;
        private System.Windows.Forms.TextBox paramRxSyncText;
        private System.Windows.Forms.CheckBox paramRxSyncSoftInvertCheck;
        private System.Windows.Forms.Label paramRxSyncActualLabel;
        private System.Windows.Forms.Label paramRxSyncAllowBitErrUnitLabel;
        private System.Windows.Forms.TextBox paramRxSyncAllowBitErrText;
        private System.Windows.Forms.GroupBox paramFIFOBitOrderGroup;
        private System.Windows.Forms.RadioButton paramFIFOBitOrderLSBRadioBtn;
        private System.Windows.Forms.RadioButton paramFIFOBitOrderMSBRadioBtn;
        private System.Windows.Forms.GroupBox paramTxFifoModeGroup;
        private System.Windows.Forms.RadioButton paramTxFifoModeTransPresyncRadioBtn;
        private System.Windows.Forms.RadioButton paramTxFifoModeTransparentRadioBtn;
        private System.Windows.Forms.RadioButton paramTxFifoMode802154RadioBtn;
        private System.Windows.Forms.GroupBox paramPcrmuGroup;
        private System.Windows.Forms.RadioButton paramPcrmuEnableRadioBtn;
        private System.Windows.Forms.RadioButton paramPcrmuDisableRadioBtn;
        private System.Windows.Forms.GroupBox paramDataFormatGroup;
        private System.Windows.Forms.RadioButton paramDataFormat8BitsRadioBtn;
        private System.Windows.Forms.RadioButton paramDataFormat32BitsRadioBtn;
        private System.Windows.Forms.GroupBox paramIntModeGroup;
        private System.Windows.Forms.Label paramIntModeSelectedLabel;
        private System.Windows.Forms.Label paramIntModeSelectedCountLabel;
        private System.Windows.Forms.Label paramIntModeSelectedUnitLabel;
        private System.Windows.Forms.GroupBox paramIntModeTxAemptyGroup;
        private System.Windows.Forms.TextBox paramIntModeTxAemptyText;
        private System.Windows.Forms.GroupBox paramIntModeRxAfullGroup;
        private System.Windows.Forms.TextBox paramIntModeRxAfullText;
        private System.Windows.Forms.GroupBox paramGpio0Group;
        private System.Windows.Forms.ComboBox paramGpio0GioSelCombo;
        private System.Windows.Forms.ComboBox paramGpio1GioSelCombo;
        private System.Windows.Forms.Button regTableDefaultBtn;
        private System.Windows.Forms.GroupBox paramModemGroup;
        private System.Windows.Forms.GroupBox paramFreqAndPwrGroup;
        private System.Windows.Forms.TextBox paramBaseFreqText;
        private System.Windows.Forms.GroupBox paramPktGroup;
        private System.Windows.Forms.Label paramPbMultipLabel;
        private System.Windows.Forms.GroupBox paramSyncGroup;
        private System.Windows.Forms.Label paramRxSyncAllowBitErrLabel;
        private System.Windows.Forms.GroupBox regTableTopicGroup;
        private System.Windows.Forms.Label dvbTstTopicLabel;
        private System.Windows.Forms.Label paramTopicLabel;
        private System.Windows.Forms.Button regTableDumpDvbRegBtn;
        private System.Windows.Forms.Button regTableWriteDvbRegBtn;
        private System.Windows.Forms.CheckBox regTableIncInitArrayCheck;
        private System.Windows.Forms.Button paramDeviationHelpBtn;
        private System.Windows.Forms.GroupBox dvbTstPktTxCntGroup;
        private System.Windows.Forms.Label dvbTstPktTxCntUnitLabel;
        private System.Windows.Forms.TextBox dvbTstPktTxCntText;
        private System.Windows.Forms.CheckBox dvbTstPktTxInfiniteCheck;
        private System.Windows.Forms.GroupBox dvbTstPktTxContentGroup;
        private System.Windows.Forms.Label dvbTstPktTxRandomPlLenUnitLabel;
        private System.Windows.Forms.Label dvbTstPktTxCrcPolyLabel;
        private System.Windows.Forms.CheckBox dvbTstPktTxRandomPlCheck;
        private System.Windows.Forms.TextBox dvbTstPktTxRandomPlLenText;
        private System.Windows.Forms.ComboBox dvbTstPktTxCrcCombo;
        private System.Windows.Forms.CheckBox dvbTstPktTxPrependSeqCheck;
        private System.Windows.Forms.Button dvbTstPktTxStopBtn;
        private System.Windows.Forms.TextBox dvbTstPktTxFilledLenText;
        private System.Windows.Forms.Label dvbTstPktTxFilledLenDescLabel;
        private System.Windows.Forms.Button dvbTstPktTxStartBtn;
        private System.Windows.Forms.TextBox dvbTstPktTxPayloadText;
        private System.Windows.Forms.Label dvbTstPktTxDgmSyncLabel;
        private System.Windows.Forms.Label dvbTstPktTxDgmCrcLabel;
        private System.Windows.Forms.Label dvbTstPktTxDgmPayloadLabel;
        private System.Windows.Forms.Label dvbTstPktTxDgmSeqNumLabel;
        private System.Windows.Forms.Label dvbTstPktTxDgmLengthLabel;
        private System.Windows.Forms.Label dvbTstPktTxDgmPbLabel;
        private System.Windows.Forms.Label dvbTstPktTxDgmSyncLenLabel;
        private System.Windows.Forms.Label dvbTstPktTxDgmCrcLenLabel;
        private System.Windows.Forms.Label dvbTstPktTxDgmPayloadLenLabel;
        private System.Windows.Forms.Label dvbTstPktTxDgmSeqNumLenLabel;
        private System.Windows.Forms.Label dvbTstPktTxDgmLengthLenLabel;
        private System.Windows.Forms.Label dvbTstPktTxDgmPbLenLabel;
        private System.Windows.Forms.Label dvbTstPktTxDgmFifoRangeLabel;
        private System.Windows.Forms.GroupBox dvbTstContiTxPatternGroup;
        private System.Windows.Forms.RadioButton dvbTstContiTxPrbs9RadioBtn;
        private System.Windows.Forms.RadioButton dvbTstContiTxBit0101RadioBtn;
        private System.Windows.Forms.GroupBox dvbTstContiTxModeGroup;
        private System.Windows.Forms.RadioButton dvbTstContiTxPatternRadioBtn;
        private System.Windows.Forms.RadioButton dvbTstContiTxSingleToneRadioBtn;
        private System.Windows.Forms.Button dvbTstContiTxStopBtn;
        private System.Windows.Forms.Button dvbTstContiTxStartBtn;
        private System.Windows.Forms.TextBox dvbTstPktRxRssiText;
        private System.Windows.Forms.Label dvbTstPktRxRssiLabel;
        private System.Windows.Forms.Button dvbTstPktRxClearLogBtn;
        private System.Windows.Forms.Button dvbTstPktRxScanBtn;
        private System.Windows.Forms.Button dvbTstPktRxSetPayloadBtn;
        private System.Windows.Forms.GroupBox dvbTstPktRxCntGroup;
        private System.Windows.Forms.RadioButton dvbTstPktRxCntLimitRadioBtn;
        private System.Windows.Forms.TextBox dvbTstPktRxCntText;
        private System.Windows.Forms.RadioButton dvbTstPktRxCntNoLimitRadioBtn;
        private System.Windows.Forms.GroupBox dvbTstPktRxParserGroup;
        private System.Windows.Forms.CheckBox dvbTstPktRxParseSeqCheck;
        private System.Windows.Forms.Label dvbTstPktRxCrcPolyLabel;
        private System.Windows.Forms.ComboBox dvbTstPktRxCrcCombo;
        private System.Windows.Forms.Label dvbTstPktRxErrCntLabel;
        private System.Windows.Forms.TextBox dvbTstPktRxErrCntText;
        private System.Windows.Forms.TextBox dvbTstPktRxLogText;
        private System.Windows.Forms.Button dvbTstPktRxStartBtn;
        private System.Windows.Forms.Label dvbTstPktRxOkCntLabel;
        private System.Windows.Forms.TextBox dvbTstPktRxOkCntText;
        private System.Windows.Forms.Button paramXtalHelpBtn;
        private System.Windows.Forms.Button paramFreqHelpBtn;
        private System.Windows.Forms.Button paramPwrHelpBtn;
        private System.Windows.Forms.Button paramGpioHelpBtn;
        private System.Windows.Forms.ComboBox paramGpio0ModeCombo;
        private System.Windows.Forms.ComboBox paramGpio1ModeCombo;
        private System.Windows.Forms.Label paramGpio0ModeLabel;
        private System.Windows.Forms.Label paramGpio0PullCtrlLabel;
        private System.Windows.Forms.Label paramGpio0GIoSelLabel;
        private System.Windows.Forms.ComboBox paramGpio0PullCtrlCombo;
        private System.Windows.Forms.Label paramGpio1GIoSelLabel;
        private System.Windows.Forms.Label paramGpio1ModeLabel;
        private System.Windows.Forms.CheckBox paramGpio0GIoInvCheck;
        private System.Windows.Forms.GroupBox paramGpio1Group;
        private System.Windows.Forms.CheckBox paramGpio1GIoInvCheck;
        private System.Windows.Forms.Label paramGpio1PullCtrlLabel;
        private System.Windows.Forms.ComboBox paramGpio1PullCtrlCombo;
        private System.Windows.Forms.Button paramFreqTableDelAllBtn;
        private System.Windows.Forms.Label dvbTstPktTxDgmMacRangeLabel;
        private System.Windows.Forms.Label dvbTstSubTopicLabel;
        private System.Windows.Forms.Label paramTxFifoModePreSyncHelpLabel;
        private System.Windows.Forms.Label paramTxFifoModeNoticeLabel;
        private System.Windows.Forms.Label paramTxFifoModeTransHelpLabel;
        private System.Windows.Forms.Label paramTxFifoMode802HelpLabel;
        private System.Windows.Forms.Button dvbTstPktFifoDgmHelpBtn;
        private System.Windows.Forms.Button regTableWriteSelDvbRegBtn;
        private System.Windows.Forms.ComboBox paramXtalGainCombo;
        private System.Windows.Forms.Label paramXtalGainLabel;
        private System.Windows.Forms.Label dvbTstConnStateLabel;
        private System.Windows.Forms.RadioButton paramPcrShdEnableRadioBtn;
        private System.Windows.Forms.Label paramPcrShdEnableHelpLabel;
        private System.Windows.Forms.Label dvbTstPktTxCntIntvUnitLabel;
        private System.Windows.Forms.TextBox dvbTstPktTxCntIntvText;
        private System.Windows.Forms.Button dvbTstPktTxCrcCalcBtn;
    }
}

