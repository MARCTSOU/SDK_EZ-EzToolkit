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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.packetTxPage = new System.Windows.Forms.TabPage();
            this.label29 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.addSeqLevel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txCrcPolyLabel = new System.Windows.Forms.Label();
            this.crcSelectCombo = new System.Windows.Forms.ComboBox();
            this.addSeqCheckbox = new System.Windows.Forms.CheckBox();
            this.lenOfHex = new System.Windows.Forms.TextBox();
            this.lenOfTxtLable = new System.Windows.Forms.Label();
            this.dataTxt = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.btnCalCrc = new System.Windows.Forms.Button();
            this.continusTxPage = new System.Windows.Forms.TabPage();
            this.continueTxGrp = new System.Windows.Forms.GroupBox();
            this.patternGrp = new System.Windows.Forms.GroupBox();
            this.prbs9Radio = new System.Windows.Forms.RadioButton();
            this.pr55Radio = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.modulationRadio = new System.Windows.Forms.RadioButton();
            this.singleToneRadio = new System.Windows.Forms.RadioButton();
            this.btnCWOff = new System.Windows.Forms.Button();
            this.btnCWOn = new System.Windows.Forms.Button();
            this.packetRxPage = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rssiValue = new System.Windows.Forms.TextBox();
            this.rssiLabel = new System.Windows.Forms.Label();
            this.btnPayloadClear = new System.Windows.Forms.Button();
            this.btnScanStart = new System.Windows.Forms.Button();
            this.btnSetPayload = new System.Windows.Forms.Button();
            this.packetCountGroup = new System.Windows.Forms.GroupBox();
            this.packetRxLimitRadio = new System.Windows.Forms.RadioButton();
            this.packetRxNum = new System.Windows.Forms.TextBox();
            this.packetRxNoLimitRadio = new System.Windows.Forms.RadioButton();
            this.packetParserGroup = new System.Windows.Forms.GroupBox();
            this.rxParseSeq = new System.Windows.Forms.CheckBox();
            this.crcLabel = new System.Windows.Forms.Label();
            this.packetRxCrcCombo = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.rcvErrText = new System.Windows.Forms.TextBox();
            this.rxResult = new System.Windows.Forms.TextBox();
            this.btnRxStart = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.rcvOkText = new System.Windows.Forms.TextBox();
            this.statePage = new System.Windows.Forms.TabPage();
            this.btnStateSet = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.shutdownRadioBtn = new System.Windows.Forms.RadioButton();
            this.deepSleepRadioBtn = new System.Windows.Forms.RadioButton();
            this.idleRadioBtn = new System.Windows.Forms.RadioButton();
            this.randomLen = new System.Windows.Forms.TextBox();
            this.randomLenLable = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.numOfPkt = new System.Windows.Forms.TextBox();
            this.infiniteCheckbox = new System.Windows.Forms.CheckBox();
            this.randomCheckbox = new System.Windows.Forms.CheckBox();
            this.btnSendStop = new System.Windows.Forms.Button();
            this.btnSendStart = new System.Windows.Forms.Button();
            this.regWrPage = new System.Windows.Forms.TabPage();
            this.grpFreqTable = new System.Windows.Forms.GroupBox();
            this.inFreq = new System.Windows.Forms.TextBox();
            this.lblCgMhz = new System.Windows.Forms.Label();
            this.btnAddFreq = new System.Windows.Forms.Button();
            this.freqList = new System.Windows.Forms.ListView();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.lblCgContent = new System.Windows.Forms.Label();
            this.preambleContent = new System.Windows.Forms.TextBox();
            this.lblCgLen = new System.Windows.Forms.Label();
            this.preambleLen = new System.Windows.Forms.TextBox();
            this.lblTxSync = new System.Windows.Forms.Label();
            this.lblTxBitOrder = new System.Windows.Forms.Label();
            this.lblRxSync = new System.Windows.Forms.Label();
            this.rxSyncword = new System.Windows.Forms.TextBox();
            this.rxsyncInvertCheckbox = new System.Windows.Forms.CheckBox();
            this.rxBitOrder = new System.Windows.Forms.Label();
            this.allowBitError = new System.Windows.Forms.TextBox();
            this.lblCgAbe = new System.Windows.Forms.Label();
            this.grpPayloadOrder = new System.Windows.Forms.GroupBox();
            this.payloadLSB = new System.Windows.Forms.RadioButton();
            this.payloadMSB = new System.Windows.Forms.RadioButton();
            this.grpTxPayloadFmt = new System.Windows.Forms.GroupBox();
            this.radioTransWithPresync = new System.Windows.Forms.RadioButton();
            this.radioTransparent = new System.Windows.Forms.RadioButton();
            this.radio802154 = new System.Windows.Forms.RadioButton();
            this.grpPcrmu = new System.Windows.Forms.GroupBox();
            this.pcrmuEnable = new System.Windows.Forms.RadioButton();
            this.pcrmuDisable = new System.Windows.Forms.RadioButton();
            this.grpDataFormat = new System.Windows.Forms.GroupBox();
            this.radio8Bits = new System.Windows.Forms.RadioButton();
            this.radio32Bits = new System.Windows.Forms.RadioButton();
            this.intModeGrp = new System.Windows.Forms.GroupBox();
            this.lblCgSel = new System.Windows.Forms.Label();
            this.showIntNum = new System.Windows.Forms.Label();
            this.lblCgItems = new System.Windows.Forms.Label();
            this.TXAEMPTY_Grp = new System.Windows.Forms.GroupBox();
            this.txAemptyThr = new System.Windows.Forms.TextBox();
            this.RXFULL_Grp = new System.Windows.Forms.GroupBox();
            this.rxFullThr = new System.Windows.Forms.TextBox();
            this.grpGpio = new System.Windows.Forms.GroupBox();
            this.GPIO0_Enable = new System.Windows.Forms.CheckBox();
            this.gpio0_Combo = new System.Windows.Forms.ComboBox();
            this.GPIO1_Enable = new System.Windows.Forms.CheckBox();
            this.gpio1_Combo = new System.Windows.Forms.ComboBox();
            this.btnClearInsLog = new System.Windows.Forms.Button();
            this.txResult = new System.Windows.Forms.TextBox();
            this.syncLSBCheckbox = new System.Windows.Forms.CheckBox();
            this.capioStr = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.pwrlvl = new System.Windows.Forms.TextBox();
            this.freqDev = new System.Windows.Forms.TextBox();
            this.PwrLvLabel = new System.Windows.Forms.Label();
            this.txSyncword = new System.Windows.Forms.TextBox();
            this.txDRCombo = new System.Windows.Forms.ComboBox();
            this.txFreqText = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txModCombo = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.PAConfigCombo = new System.Windows.Forms.ComboBox();
            this.maxPwrSelLabel = new System.Windows.Forms.Label();
            this.btnTxSet = new System.Windows.Forms.Button();
            this.regPage = new System.Windows.Forms.TabPage();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.initialDataPath = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.searchTxt = new System.Windows.Forms.TextBox();
            this.btnClrListview = new System.Windows.Forms.Button();
            this.wrAllRegGrp = new System.Windows.Forms.GroupBox();
            this.btnTableConnect = new System.Windows.Forms.Button();
            this.btnTableComRefresh = new System.Windows.Forms.Button();
            this.tableCombobox = new System.Windows.Forms.ComboBox();
            this.btnWriteAll = new System.Windows.Forms.Button();
            this.regRead = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bitColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.insertValue = new System.Windows.Forms.TextBox();
            this.btnRead1 = new System.Windows.Forms.Button();
            this.hexTxt = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.btnRegWrite = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.regPage2 = new System.Windows.Forms.TabPage();
            this.registerList = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.registerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.registerValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveInitValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadInitFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadParameterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveParameterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshComToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comPortToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.connectComToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort3 = new System.IO.Ports.SerialPort(this.components);
            this.paramTab = new System.Windows.Forms.TabControl();
            this.pageParameter = new System.Windows.Forms.TabPage();
            this.grpPktParam = new System.Windows.Forms.GroupBox();
            this.grpSyncword = new System.Windows.Forms.GroupBox();
            this.lblRxSyncTolerance = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.grpModAndDR = new System.Windows.Forms.GroupBox();
            this.btnDevComment = new System.Windows.Forms.Button();
            this.grpFreqAndPwr = new System.Windows.Forms.GroupBox();
            this.xtalCombo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DVBBandCombo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pageFreqTable = new System.Windows.Forms.TabPage();
            this.pageTxPayload = new System.Windows.Forms.TabPage();
            this.labelForPayloadFormat = new System.Windows.Forms.Label();
            this.pageIntMode = new System.Windows.Forms.TabPage();
            this.pageGpio = new System.Windows.Forms.TabPage();
            this.pagePcrmu = new System.Windows.Forms.TabPage();
            this.btnGen = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.regGroup = new System.Windows.Forms.GroupBox();
            this.LabelTopicDVB = new System.Windows.Forms.Label();
            this.LabelTopicPRAM = new System.Windows.Forms.Label();
            this.btnReadDVB = new System.Windows.Forms.Button();
            this.btnWriteDVB = new System.Windows.Forms.Button();
            this.IncludeInitArrayCheckBox = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.packetTxPage.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.continusTxPage.SuspendLayout();
            this.continueTxGrp.SuspendLayout();
            this.patternGrp.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.packetRxPage.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.packetCountGroup.SuspendLayout();
            this.packetParserGroup.SuspendLayout();
            this.statePage.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpFreqTable.SuspendLayout();
            this.grpPayloadOrder.SuspendLayout();
            this.grpTxPayloadFmt.SuspendLayout();
            this.grpPcrmu.SuspendLayout();
            this.grpDataFormat.SuspendLayout();
            this.intModeGrp.SuspendLayout();
            this.TXAEMPTY_Grp.SuspendLayout();
            this.RXFULL_Grp.SuspendLayout();
            this.grpGpio.SuspendLayout();
            this.regPage.SuspendLayout();
            this.wrAllRegGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.paramTab.SuspendLayout();
            this.pageParameter.SuspendLayout();
            this.grpPktParam.SuspendLayout();
            this.grpSyncword.SuspendLayout();
            this.grpModAndDR.SuspendLayout();
            this.grpFreqAndPwr.SuspendLayout();
            this.pageFreqTable.SuspendLayout();
            this.pageTxPayload.SuspendLayout();
            this.pageIntMode.SuspendLayout();
            this.pageGpio.SuspendLayout();
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.packetTxPage);
            this.tabControl1.Controls.Add(this.continusTxPage);
            this.tabControl1.Controls.Add(this.packetRxPage);
            this.tabControl1.Controls.Add(this.statePage);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.tabControl1.ItemSize = new System.Drawing.Size(100, 24);
            this.tabControl1.Location = new System.Drawing.Point(10, 345);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(868, 360);
            this.tabControl1.TabIndex = 40;
            // 
            // packetTxPage
            // 
            this.packetTxPage.BackColor = System.Drawing.Color.White;
            this.packetTxPage.Controls.Add(this.label29);
            this.packetTxPage.Controls.Add(this.groupBox3);
            this.packetTxPage.Controls.Add(this.label28);
            this.packetTxPage.Controls.Add(this.btnCalCrc);
            this.packetTxPage.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.packetTxPage.Location = new System.Drawing.Point(4, 28);
            this.packetTxPage.Margin = new System.Windows.Forms.Padding(2);
            this.packetTxPage.Name = "packetTxPage";
            this.packetTxPage.Padding = new System.Windows.Forms.Padding(2);
            this.packetTxPage.Size = new System.Drawing.Size(860, 328);
            this.packetTxPage.TabIndex = 0;
            this.packetTxPage.Text = "PACKET TX";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(225, 301);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(195, 17);
            this.label29.TabIndex = 45;
            this.label29.Text = "Max Payload (with Seq) = 123";
            this.label29.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.btnSendStop);
            this.groupBox3.Controls.Add(this.lenOfHex);
            this.groupBox3.Controls.Add(this.lenOfTxtLable);
            this.groupBox3.Controls.Add(this.btnSendStart);
            this.groupBox3.Controls.Add(this.dataTxt);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(848, 293);
            this.groupBox3.TabIndex = 32;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Payload (Hex)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(8, 22);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(784, 68);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 43;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.randomLenLable);
            this.groupBox4.Controls.Add(this.txCrcPolyLabel);
            this.groupBox4.Controls.Add(this.randomCheckbox);
            this.groupBox4.Controls.Add(this.randomLen);
            this.groupBox4.Controls.Add(this.crcSelectCombo);
            this.groupBox4.Controls.Add(this.addSeqCheckbox);
            this.groupBox4.Controls.Add(this.addSeqLevel);
            this.groupBox4.Location = new System.Drawing.Point(418, 98);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(319, 122);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Packet Content";
            // 
            // txCrcPolyLabel
            // 
            this.txCrcPolyLabel.AutoSize = true;
            this.txCrcPolyLabel.Location = new System.Drawing.Point(14, 23);
            this.txCrcPolyLabel.Name = "txCrcPolyLabel";
            this.txCrcPolyLabel.Size = new System.Drawing.Size(108, 17);
            this.txCrcPolyLabel.TabIndex = 44;
            this.txCrcPolyLabel.Text = "CRC Polynomial";
            // 
            // crcSelectCombo
            // 
            this.crcSelectCombo.FormattingEnabled = true;
            this.crcSelectCombo.Location = new System.Drawing.Point(127, 20);
            this.crcSelectCombo.Margin = new System.Windows.Forms.Padding(2);
            this.crcSelectCombo.Name = "crcSelectCombo";
            this.crcSelectCombo.Size = new System.Drawing.Size(88, 24);
            this.crcSelectCombo.TabIndex = 0;
            this.crcSelectCombo.SelectedIndexChanged += new System.EventHandler(this.crcSelectCombo_SelectedIndexChanged);
            // 
            // addSeqCheckbox
            // 
            this.addSeqCheckbox.Location = new System.Drawing.Point(17, 44);
            this.addSeqCheckbox.Margin = new System.Windows.Forms.Padding(4);
            this.addSeqCheckbox.Name = "addSeqCheckbox";
            this.addSeqCheckbox.Size = new System.Drawing.Size(235, 40);
            this.addSeqCheckbox.TabIndex = 26;
            this.addSeqCheckbox.Text = "Prepend Seq Num (+2 bytes)";
            this.addSeqCheckbox.UseVisualStyleBackColor = true;
            this.addSeqCheckbox.CheckedChanged += new System.EventHandler(this.addSeqCheckbox_CheckedChanged);
            //
            // addSeqLevel
            //
            this.addSeqLevel.AutoSize = true;
            this.addSeqLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.addSeqLevel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.addSeqLevel.Location = new System.Drawing.Point(17, 86);
            this.addSeqLevel.Name = "addSeqLevel";
            this.addSeqLevel.Size = new System.Drawing.Size(200, 12);
            this.addSeqLevel.TabIndex = 27;
            this.addSeqLevel.Text = "Seq: adds 2 bytes to the front of payload";
            this.addSeqLevel.Visible = false;
            // 
            // lenOfHex
            // 
            this.lenOfHex.BackColor = System.Drawing.Color.Silver;
            this.lenOfHex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lenOfHex.Enabled = false;
            this.lenOfHex.Location = new System.Drawing.Point(8, 100);
            this.lenOfHex.Margin = new System.Windows.Forms.Padding(4);
            this.lenOfHex.Name = "lenOfHex";
            this.lenOfHex.ReadOnly = true;
            this.lenOfHex.Size = new System.Drawing.Size(55, 23);
            this.lenOfHex.TabIndex = 28;
            this.lenOfHex.Text = "0";
            // 
            // lenOfTxtLable
            // 
            this.lenOfTxtLable.AutoSize = true;
            this.lenOfTxtLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lenOfTxtLable.Location = new System.Drawing.Point(69, 102);
            this.lenOfTxtLable.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lenOfTxtLable.Name = "lenOfTxtLable";
            this.lenOfTxtLable.Size = new System.Drawing.Size(301, 17);
            this.lenOfTxtLable.TabIndex = 27;
            this.lenOfTxtLable.Text = "-Bytes payload has been filled in following box.";
            // 
            // dataTxt
            // 
            this.dataTxt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.dataTxt.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataTxt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dataTxt.Location = new System.Drawing.Point(8, 131);
            this.dataTxt.Margin = new System.Windows.Forms.Padding(4);
            this.dataTxt.Multiline = true;
            this.dataTxt.Name = "dataTxt";
            this.dataTxt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataTxt.Size = new System.Drawing.Size(401, 145);
            this.dataTxt.TabIndex = 18;
            this.dataTxt.TextChanged += new System.EventHandler(this.dataTxt_TextChanged);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(15, 301);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(187, 17);
            this.label28.TabIndex = 44;
            this.label28.Text = "Max Payload (no Seq) = 125";
            this.label28.Visible = false;
            // 
            // btnCalCrc
            // 
            this.btnCalCrc.Location = new System.Drawing.Point(703, 299);
            this.btnCalCrc.Margin = new System.Windows.Forms.Padding(4);
            this.btnCalCrc.Name = "btnCalCrc";
            this.btnCalCrc.Size = new System.Drawing.Size(114, 20);
            this.btnCalCrc.TabIndex = 19;
            this.btnCalCrc.Text = "Crc";
            this.btnCalCrc.UseVisualStyleBackColor = true;
            this.btnCalCrc.Visible = false;
            this.btnCalCrc.Click += new System.EventHandler(this.btnCalCrc_Click);
            // 
            // continusTxPage
            // 
            this.continusTxPage.AutoScroll = true;
            this.continusTxPage.BackColor = System.Drawing.Color.Transparent;
            this.continusTxPage.Controls.Add(this.continueTxGrp);
            this.continusTxPage.Location = new System.Drawing.Point(4, 28);
            this.continusTxPage.Margin = new System.Windows.Forms.Padding(2);
            this.continusTxPage.Name = "continusTxPage";
            this.continusTxPage.Padding = new System.Windows.Forms.Padding(2);
            this.continusTxPage.Size = new System.Drawing.Size(860, 328);
            this.continusTxPage.TabIndex = 1;
            this.continusTxPage.Text = "CONTINOUS WAVE";
            // 
            // continueTxGrp
            // 
            this.continueTxGrp.Controls.Add(this.patternGrp);
            this.continueTxGrp.Controls.Add(this.groupBox6);
            this.continueTxGrp.Controls.Add(this.btnCWOff);
            this.continueTxGrp.Controls.Add(this.btnCWOn);
            this.continueTxGrp.Location = new System.Drawing.Point(38, 16);
            this.continueTxGrp.Margin = new System.Windows.Forms.Padding(2);
            this.continueTxGrp.Name = "continueTxGrp";
            this.continueTxGrp.Padding = new System.Windows.Forms.Padding(2);
            this.continueTxGrp.Size = new System.Drawing.Size(711, 198);
            this.continueTxGrp.TabIndex = 0;
            this.continueTxGrp.TabStop = false;
            // 
            // patternGrp
            // 
            this.patternGrp.Controls.Add(this.prbs9Radio);
            this.patternGrp.Controls.Add(this.pr55Radio);
            this.patternGrp.Location = new System.Drawing.Point(514, 26);
            this.patternGrp.Margin = new System.Windows.Forms.Padding(2);
            this.patternGrp.Name = "patternGrp";
            this.patternGrp.Padding = new System.Windows.Forms.Padding(2);
            this.patternGrp.Size = new System.Drawing.Size(158, 132);
            this.patternGrp.TabIndex = 5;
            this.patternGrp.TabStop = false;
            this.patternGrp.Text = "Pattern";
            // 
            // prbs9Radio
            // 
            this.prbs9Radio.AutoSize = true;
            this.prbs9Radio.Location = new System.Drawing.Point(12, 66);
            this.prbs9Radio.Margin = new System.Windows.Forms.Padding(2);
            this.prbs9Radio.Name = "prbs9Radio";
            this.prbs9Radio.Size = new System.Drawing.Size(66, 21);
            this.prbs9Radio.TabIndex = 1;
            this.prbs9Radio.Text = "Prbs9";
            this.prbs9Radio.UseVisualStyleBackColor = true;
            // 
            // pr55Radio
            // 
            this.pr55Radio.AutoSize = true;
            this.pr55Radio.Checked = true;
            this.pr55Radio.Location = new System.Drawing.Point(12, 35);
            this.pr55Radio.Margin = new System.Windows.Forms.Padding(2);
            this.pr55Radio.Name = "pr55Radio";
            this.pr55Radio.Size = new System.Drawing.Size(117, 21);
            this.pr55Radio.TabIndex = 0;
            this.pr55Radio.TabStop = true;
            this.pr55Radio.Text = "Bit_01010101";
            this.pr55Radio.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.modulationRadio);
            this.groupBox6.Controls.Add(this.singleToneRadio);
            this.groupBox6.Location = new System.Drawing.Point(348, 26);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(161, 132);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Mode";
            // 
            // modulationRadio
            // 
            this.modulationRadio.AutoSize = true;
            this.modulationRadio.Location = new System.Drawing.Point(32, 66);
            this.modulationRadio.Margin = new System.Windows.Forms.Padding(2);
            this.modulationRadio.Name = "modulationRadio";
            this.modulationRadio.Size = new System.Drawing.Size(75, 21);
            this.modulationRadio.TabIndex = 1;
            this.modulationRadio.Text = "Pattern";
            this.modulationRadio.UseVisualStyleBackColor = true;
            this.modulationRadio.CheckedChanged += new System.EventHandler(this.modulationRadio_CheckedChanged);
            // 
            // singleToneRadio
            // 
            this.singleToneRadio.AutoSize = true;
            this.singleToneRadio.Checked = true;
            this.singleToneRadio.Location = new System.Drawing.Point(32, 35);
            this.singleToneRadio.Margin = new System.Windows.Forms.Padding(2);
            this.singleToneRadio.Name = "singleToneRadio";
            this.singleToneRadio.Size = new System.Drawing.Size(101, 21);
            this.singleToneRadio.TabIndex = 0;
            this.singleToneRadio.TabStop = true;
            this.singleToneRadio.Text = "SingleTone";
            this.singleToneRadio.UseVisualStyleBackColor = true;
            this.singleToneRadio.CheckedChanged += new System.EventHandler(this.singleToneRadio_CheckedChanged);
            // 
            // btnCWOff
            // 
            this.btnCWOff.Location = new System.Drawing.Point(175, 26);
            this.btnCWOff.Margin = new System.Windows.Forms.Padding(2);
            this.btnCWOff.Name = "btnCWOff";
            this.btnCWOff.Size = new System.Drawing.Size(160, 78);
            this.btnCWOff.TabIndex = 3;
            this.btnCWOff.Text = "Cw Off";
            this.btnCWOff.UseVisualStyleBackColor = true;
            this.btnCWOff.Click += new System.EventHandler(this.btnCWOff_Click);
            // 
            // btnCWOn
            // 
            this.btnCWOn.Location = new System.Drawing.Point(26, 26);
            this.btnCWOn.Margin = new System.Windows.Forms.Padding(2);
            this.btnCWOn.Name = "btnCWOn";
            this.btnCWOn.Size = new System.Drawing.Size(142, 78);
            this.btnCWOn.TabIndex = 2;
            this.btnCWOn.Text = "Cw On";
            this.btnCWOn.UseVisualStyleBackColor = true;
            this.btnCWOn.Click += new System.EventHandler(this.btnCWOn_Click);
            // 
            // packetRxPage
            // 
            this.packetRxPage.Controls.Add(this.groupBox2);
            this.packetRxPage.Location = new System.Drawing.Point(4, 28);
            this.packetRxPage.Margin = new System.Windows.Forms.Padding(4);
            this.packetRxPage.Name = "packetRxPage";
            this.packetRxPage.Size = new System.Drawing.Size(860, 328);
            this.packetRxPage.TabIndex = 2;
            this.packetRxPage.Text = "PACKET RX";
            this.packetRxPage.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.rssiValue);
            this.groupBox2.Controls.Add(this.rssiLabel);
            this.groupBox2.Controls.Add(this.btnPayloadClear);
            this.groupBox2.Controls.Add(this.btnScanStart);
            this.groupBox2.Controls.Add(this.btnSetPayload);
            this.groupBox2.Controls.Add(this.packetCountGroup);
            this.groupBox2.Controls.Add(this.packetParserGroup);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.rcvErrText);
            this.groupBox2.Controls.Add(this.rxResult);
            this.groupBox2.Controls.Add(this.btnRxStart);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.rcvOkText);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(1, 2);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(855, 340);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            // 
            // rssiValue
            // 
            this.rssiValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rssiValue.Location = new System.Drawing.Point(744, 185);
            this.rssiValue.Margin = new System.Windows.Forms.Padding(4);
            this.rssiValue.Name = "rssiValue";
            this.rssiValue.Size = new System.Drawing.Size(99, 23);
            this.rssiValue.TabIndex = 21;
            // 
            // rssiLabel
            // 
            this.rssiLabel.AutoSize = true;
            this.rssiLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rssiLabel.Location = new System.Drawing.Point(744, 161);
            this.rssiLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.rssiLabel.Name = "rssiLabel";
            this.rssiLabel.Size = new System.Drawing.Size(67, 17);
            this.rssiLabel.TabIndex = 22;
            this.rssiLabel.Text = "Avg RSSI";
            // 
            // btnPayloadClear
            // 
            this.btnPayloadClear.Location = new System.Drawing.Point(637, 33);
            this.btnPayloadClear.Margin = new System.Windows.Forms.Padding(2);
            this.btnPayloadClear.Name = "btnPayloadClear";
            this.btnPayloadClear.Size = new System.Drawing.Size(77, 35);
            this.btnPayloadClear.TabIndex = 35;
            this.btnPayloadClear.Text = "Clear";
            this.btnPayloadClear.UseVisualStyleBackColor = true;
            this.btnPayloadClear.Click += new System.EventHandler(this.btnPayloadClear_Click);
            // 
            // btnScanStart
            // 
            this.btnScanStart.Location = new System.Drawing.Point(741, 225);
            this.btnScanStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnScanStart.Name = "btnScanStart";
            this.btnScanStart.Size = new System.Drawing.Size(99, 35);
            this.btnScanStart.TabIndex = 34;
            this.btnScanStart.Text = "Scan RSSI";
            this.btnScanStart.UseVisualStyleBackColor = true;
            this.btnScanStart.Click += new System.EventHandler(this.btnScanRssi_Click);
            // 
            // btnSetPayload
            // 
            this.btnSetPayload.Location = new System.Drawing.Point(532, 33);
            this.btnSetPayload.Margin = new System.Windows.Forms.Padding(4);
            this.btnSetPayload.Name = "btnSetPayload";
            this.btnSetPayload.Size = new System.Drawing.Size(99, 35);
            this.btnSetPayload.TabIndex = 16;
            this.btnSetPayload.Text = "Set Payload";
            this.btnSetPayload.UseVisualStyleBackColor = true;
            this.btnSetPayload.Visible = false;
            this.btnSetPayload.Click += new System.EventHandler(this.btnSetPayload_Click);
            // 
            // packetCountGroup
            // 
            this.packetCountGroup.Controls.Add(this.packetRxLimitRadio);
            this.packetCountGroup.Controls.Add(this.packetRxNum);
            this.packetCountGroup.Controls.Add(this.packetRxNoLimitRadio);
            this.packetCountGroup.Location = new System.Drawing.Point(10, 3);
            this.packetCountGroup.Margin = new System.Windows.Forms.Padding(2);
            this.packetCountGroup.Name = "packetCountGroup";
            this.packetCountGroup.Padding = new System.Windows.Forms.Padding(2);
            this.packetCountGroup.Size = new System.Drawing.Size(230, 95);
            this.packetCountGroup.TabIndex = 36;
            this.packetCountGroup.TabStop = false;
            this.packetCountGroup.Text = "Packet Count";
            // 
            // packetRxLimitRadio
            // 
            this.packetRxLimitRadio.AutoSize = true;
            this.packetRxLimitRadio.Location = new System.Drawing.Point(10, 22);
            this.packetRxLimitRadio.Margin = new System.Windows.Forms.Padding(2);
            this.packetRxLimitRadio.Name = "packetRxLimitRadio";
            this.packetRxLimitRadio.Size = new System.Drawing.Size(142, 21);
            this.packetRxLimitRadio.TabIndex = 31;
            this.packetRxLimitRadio.Text = "Max Packet Count";
            this.packetRxLimitRadio.UseVisualStyleBackColor = true;
            this.packetRxLimitRadio.CheckedChanged += new System.EventHandler(this.packetRxCountMode_CheckedChanged);
            // 
            // packetRxNum
            // 
            this.packetRxNum.Enabled = false;
            this.packetRxNum.Location = new System.Drawing.Point(135, 20);
            this.packetRxNum.Margin = new System.Windows.Forms.Padding(2);
            this.packetRxNum.Name = "packetRxNum";
            this.packetRxNum.Size = new System.Drawing.Size(76, 23);
            this.packetRxNum.TabIndex = 32;
            this.packetRxNum.Text = "100";
            // 
            // packetRxNoLimitRadio
            // 
            this.packetRxNoLimitRadio.AutoSize = true;
            this.packetRxNoLimitRadio.Checked = true;
            this.packetRxNoLimitRadio.Location = new System.Drawing.Point(10, 55);
            this.packetRxNoLimitRadio.Margin = new System.Windows.Forms.Padding(2);
            this.packetRxNoLimitRadio.Name = "packetRxNoLimitRadio";
            this.packetRxNoLimitRadio.Size = new System.Drawing.Size(106, 21);
            this.packetRxNoLimitRadio.TabIndex = 33;
            this.packetRxNoLimitRadio.TabStop = true;
            this.packetRxNoLimitRadio.Text = "No limitation";
            this.packetRxNoLimitRadio.UseVisualStyleBackColor = true;
            this.packetRxNoLimitRadio.CheckedChanged += new System.EventHandler(this.packetRxCountMode_CheckedChanged);
            // 
            // packetParserGroup
            // 
            this.packetParserGroup.Controls.Add(this.rxParseSeq);
            this.packetParserGroup.Controls.Add(this.crcLabel);
            this.packetParserGroup.Controls.Add(this.packetRxCrcCombo);
            this.packetParserGroup.Location = new System.Drawing.Point(248, 3);
            this.packetParserGroup.Margin = new System.Windows.Forms.Padding(2);
            this.packetParserGroup.Name = "packetParserGroup";
            this.packetParserGroup.Padding = new System.Windows.Forms.Padding(2);
            this.packetParserGroup.Size = new System.Drawing.Size(270, 95);
            this.packetParserGroup.TabIndex = 37;
            this.packetParserGroup.TabStop = false;
            this.packetParserGroup.Text = "Packet Parser";
            // 
            // rxParseSeq
            // 
            this.rxParseSeq.AutoSize = true;
            this.rxParseSeq.Location = new System.Drawing.Point(10, 22);
            this.rxParseSeq.Margin = new System.Windows.Forms.Padding(2);
            this.rxParseSeq.Name = "rxParseSeq";
            this.rxParseSeq.Size = new System.Drawing.Size(185, 21);
            this.rxParseSeq.TabIndex = 28;
            this.rxParseSeq.Text = "Packet Has Seq Number";
            this.rxParseSeq.UseVisualStyleBackColor = true;
            // 
            // crcLabel
            // 
            this.crcLabel.AutoSize = true;
            this.crcLabel.Location = new System.Drawing.Point(10, 57);
            this.crcLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.crcLabel.Name = "crcLabel";
            this.crcLabel.Size = new System.Drawing.Size(108, 17);
            this.crcLabel.TabIndex = 29;
            this.crcLabel.Text = "CRC Polynomial";
            // 
            // packetRxCrcCombo
            // 
            this.packetRxCrcCombo.FormattingEnabled = true;
            this.packetRxCrcCombo.Location = new System.Drawing.Point(130, 54);
            this.packetRxCrcCombo.Margin = new System.Windows.Forms.Padding(2);
            this.packetRxCrcCombo.Name = "packetRxCrcCombo";
            this.packetRxCrcCombo.Size = new System.Drawing.Size(109, 24);
            this.packetRxCrcCombo.TabIndex = 30;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(741, 107);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(76, 17);
            this.label20.TabIndex = 27;
            this.label20.Text = "Rcvd Error";
            // 
            // rcvErrText
            // 
            this.rcvErrText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rcvErrText.Location = new System.Drawing.Point(741, 131);
            this.rcvErrText.Margin = new System.Windows.Forms.Padding(2);
            this.rcvErrText.Name = "rcvErrText";
            this.rcvErrText.Size = new System.Drawing.Size(99, 23);
            this.rcvErrText.TabIndex = 26;
            // 
            // rxResult
            // 
            this.rxResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rxResult.Location = new System.Drawing.Point(6, 105);
            this.rxResult.Margin = new System.Windows.Forms.Padding(2);
            this.rxResult.Multiline = true;
            this.rxResult.Name = "rxResult";
            this.rxResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.rxResult.Size = new System.Drawing.Size(708, 203);
            this.rxResult.TabIndex = 23;
            this.rxResult.WordWrap = false;
            // 
            // btnRxStart
            // 
            this.btnRxStart.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnRxStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.btnRxStart.Location = new System.Drawing.Point(741, 273);
            this.btnRxStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnRxStart.Name = "btnRxStart";
            this.btnRxStart.Size = new System.Drawing.Size(99, 35);
            this.btnRxStart.TabIndex = 16;
            this.btnRxStart.Text = "Start RX";
            this.btnRxStart.UseVisualStyleBackColor = false;
            this.btnRxStart.Click += new System.EventHandler(this.btnRxStart_Click_1);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(741, 53);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 17);
            this.label10.TabIndex = 12;
            this.label10.Text = "Rcvd OK";
            // 
            // rcvOkText
            // 
            this.rcvOkText.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rcvOkText.Location = new System.Drawing.Point(741, 77);
            this.rcvOkText.Margin = new System.Windows.Forms.Padding(2);
            this.rcvOkText.Name = "rcvOkText";
            this.rcvOkText.Size = new System.Drawing.Size(99, 23);
            this.rcvOkText.TabIndex = 9;
            // 
            // statePage
            // 
            this.statePage.Controls.Add(this.btnStateSet);
            this.statePage.Controls.Add(this.groupBox10);
            this.statePage.Location = new System.Drawing.Point(4, 28);
            this.statePage.Margin = new System.Windows.Forms.Padding(2);
            this.statePage.Name = "statePage";
            this.statePage.Size = new System.Drawing.Size(860, 328);
            this.statePage.TabIndex = 3;
            this.statePage.Text = "STATE CTRL";
            this.statePage.UseVisualStyleBackColor = true;
            // 
            // btnStateSet
            // 
            this.btnStateSet.Location = new System.Drawing.Point(368, 68);
            this.btnStateSet.Margin = new System.Windows.Forms.Padding(2);
            this.btnStateSet.Name = "btnStateSet";
            this.btnStateSet.Size = new System.Drawing.Size(182, 71);
            this.btnStateSet.TabIndex = 1;
            this.btnStateSet.Text = "Set";
            this.btnStateSet.UseVisualStyleBackColor = true;
            this.btnStateSet.Click += new System.EventHandler(this.btnStateSet_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.shutdownRadioBtn);
            this.groupBox10.Controls.Add(this.deepSleepRadioBtn);
            this.groupBox10.Controls.Add(this.idleRadioBtn);
            this.groupBox10.Location = new System.Drawing.Point(65, 51);
            this.groupBox10.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox10.Size = new System.Drawing.Size(261, 229);
            this.groupBox10.TabIndex = 0;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "State";
            // 
            // shutdownRadioBtn
            // 
            this.shutdownRadioBtn.AutoSize = true;
            this.shutdownRadioBtn.Location = new System.Drawing.Point(38, 138);
            this.shutdownRadioBtn.Margin = new System.Windows.Forms.Padding(2);
            this.shutdownRadioBtn.Name = "shutdownRadioBtn";
            this.shutdownRadioBtn.Size = new System.Drawing.Size(91, 21);
            this.shutdownRadioBtn.TabIndex = 2;
            this.shutdownRadioBtn.TabStop = true;
            this.shutdownRadioBtn.Text = "Shutdown";
            this.shutdownRadioBtn.UseVisualStyleBackColor = true;
            // 
            // deepSleepRadioBtn
            // 
            this.deepSleepRadioBtn.AutoSize = true;
            this.deepSleepRadioBtn.Location = new System.Drawing.Point(38, 92);
            this.deepSleepRadioBtn.Margin = new System.Windows.Forms.Padding(2);
            this.deepSleepRadioBtn.Name = "deepSleepRadioBtn";
            this.deepSleepRadioBtn.Size = new System.Drawing.Size(103, 21);
            this.deepSleepRadioBtn.TabIndex = 1;
            this.deepSleepRadioBtn.TabStop = true;
            this.deepSleepRadioBtn.Text = "Deep Sleep";
            this.deepSleepRadioBtn.UseVisualStyleBackColor = true;
            // 
            // idleRadioBtn
            // 
            this.idleRadioBtn.AutoSize = true;
            this.idleRadioBtn.Checked = true;
            this.idleRadioBtn.Location = new System.Drawing.Point(38, 48);
            this.idleRadioBtn.Margin = new System.Windows.Forms.Padding(2);
            this.idleRadioBtn.Name = "idleRadioBtn";
            this.idleRadioBtn.Size = new System.Drawing.Size(51, 21);
            this.idleRadioBtn.TabIndex = 0;
            this.idleRadioBtn.TabStop = true;
            this.idleRadioBtn.Text = "idle";
            this.idleRadioBtn.UseVisualStyleBackColor = true;
            // 
            // randomLen
            // 
            this.randomLen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.randomLen.Enabled = false;
            this.randomLen.Location = new System.Drawing.Point(216, 81);
            this.randomLen.Margin = new System.Windows.Forms.Padding(4);
            this.randomLen.Name = "randomLen";
            this.randomLen.Size = new System.Drawing.Size(36, 23);
            this.randomLen.TabIndex = 0;
            this.randomLen.Text = "5";
            this.randomLen.Leave += new System.EventHandler(this.randomLen_Leave);
            // 
            // randomLenLable
            // 
            this.randomLenLable.AutoSize = true;
            this.randomLenLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.randomLenLable.Location = new System.Drawing.Point(258, 84);
            this.randomLenLable.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.randomLenLable.Name = "randomLenLable";
            this.randomLenLable.Size = new System.Drawing.Size(43, 17);
            this.randomLenLable.TabIndex = 13;
            this.randomLenLable.Text = "Bytes";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.numOfPkt);
            this.groupBox1.Controls.Add(this.infiniteCheckbox);
            this.groupBox1.Location = new System.Drawing.Point(418, 226);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(319, 61);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Target Packet Count";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.label11.Location = new System.Drawing.Point(95, 30);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(45, 17);
            this.label11.TabIndex = 14;
            this.label11.Text = "Pkt(s)";
            // 
            // numOfPkt
            // 
            this.numOfPkt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numOfPkt.Location = new System.Drawing.Point(21, 28);
            this.numOfPkt.Margin = new System.Windows.Forms.Padding(4);
            this.numOfPkt.Name = "numOfPkt";
            this.numOfPkt.Size = new System.Drawing.Size(63, 23);
            this.numOfPkt.TabIndex = 15;
            this.numOfPkt.Text = "5";
            // 
            // infiniteCheckbox
            // 
            this.infiniteCheckbox.AutoSize = true;
            this.infiniteCheckbox.Location = new System.Drawing.Point(148, 26);
            this.infiniteCheckbox.Margin = new System.Windows.Forms.Padding(4);
            this.infiniteCheckbox.Name = "infiniteCheckbox";
            this.infiniteCheckbox.Size = new System.Drawing.Size(71, 21);
            this.infiniteCheckbox.TabIndex = 20;
            this.infiniteCheckbox.Text = "Infinite";
            this.infiniteCheckbox.UseVisualStyleBackColor = true;
            this.infiniteCheckbox.CheckedChanged += new System.EventHandler(this.infiniteCheckbox_CheckedChanged);
            // 
            // randomCheckbox
            // 
            this.randomCheckbox.AutoSize = true;
            this.randomCheckbox.Location = new System.Drawing.Point(17, 83);
            this.randomCheckbox.Margin = new System.Windows.Forms.Padding(4);
            this.randomCheckbox.Name = "randomCheckbox";
            this.randomCheckbox.Size = new System.Drawing.Size(202, 21);
            this.randomCheckbox.TabIndex = 23;
            this.randomCheckbox.Text = "Generate Random Payload";
            this.randomCheckbox.UseVisualStyleBackColor = true;
            this.randomCheckbox.CheckedChanged += new System.EventHandler(this.randomCheckbox_CheckedChanged);
            // 
            // btnSendStop
            // 
            this.btnSendStop.Enabled = false;
            this.btnSendStop.Location = new System.Drawing.Point(743, 246);
            this.btnSendStop.Margin = new System.Windows.Forms.Padding(4);
            this.btnSendStop.Name = "btnSendStop";
            this.btnSendStop.Size = new System.Drawing.Size(90, 31);
            this.btnSendStop.TabIndex = 25;
            this.btnSendStop.Text = "Stop";
            this.btnSendStop.UseVisualStyleBackColor = true;
            this.btnSendStop.Click += new System.EventHandler(this.btnSendStop_Click);
            // 
            // btnSendStart
            // 
            this.btnSendStart.Location = new System.Drawing.Point(743, 207);
            this.btnSendStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnSendStart.Name = "btnSendStart";
            this.btnSendStart.Size = new System.Drawing.Size(90, 31);
            this.btnSendStart.TabIndex = 22;
            this.btnSendStart.Text = "Start";
            this.btnSendStart.UseVisualStyleBackColor = true;
            this.btnSendStart.Click += new System.EventHandler(this.btnSendStart_Click);
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
            // grpFreqTable
            // 
            this.grpFreqTable.BackColor = System.Drawing.Color.Transparent;
            this.grpFreqTable.Controls.Add(this.inFreq);
            this.grpFreqTable.Controls.Add(this.lblCgMhz);
            this.grpFreqTable.Controls.Add(this.btnAddFreq);
            this.grpFreqTable.Controls.Add(this.freqList);
            this.grpFreqTable.Controls.Add(this.btnDeleteItem);
            this.grpFreqTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.grpFreqTable.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpFreqTable.Location = new System.Drawing.Point(10, 8);
            this.grpFreqTable.Margin = new System.Windows.Forms.Padding(4);
            this.grpFreqTable.Name = "grpFreqTable";
            this.grpFreqTable.Padding = new System.Windows.Forms.Padding(4);
            this.grpFreqTable.Size = new System.Drawing.Size(842, 208);
            this.grpFreqTable.TabIndex = 0;
            this.grpFreqTable.TabStop = false;
            this.grpFreqTable.Text = "Predefined Channel Table";
            // 
            // inFreq
            // 
            this.inFreq.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inFreq.ForeColor = System.Drawing.SystemColors.ControlText;
            this.inFreq.Location = new System.Drawing.Point(88, 36);
            this.inFreq.Margin = new System.Windows.Forms.Padding(4);
            this.inFreq.Name = "inFreq";
            this.inFreq.Size = new System.Drawing.Size(76, 22);
            this.inFreq.TabIndex = 0;
            // 
            // lblCgMhz
            // 
            this.lblCgMhz.AutoSize = true;
            this.lblCgMhz.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCgMhz.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCgMhz.Location = new System.Drawing.Point(170, 39);
            this.lblCgMhz.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCgMhz.Name = "lblCgMhz";
            this.lblCgMhz.Size = new System.Drawing.Size(36, 17);
            this.lblCgMhz.TabIndex = 1;
            this.lblCgMhz.Text = "MHz";
            // 
            // btnAddFreq
            // 
            this.btnAddFreq.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddFreq.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAddFreq.Location = new System.Drawing.Point(18, 32);
            this.btnAddFreq.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddFreq.Name = "btnAddFreq";
            this.btnAddFreq.Size = new System.Drawing.Size(62, 30);
            this.btnAddFreq.TabIndex = 1;
            this.btnAddFreq.Text = "Add";
            this.btnAddFreq.UseVisualStyleBackColor = true;
            this.btnAddFreq.Click += new System.EventHandler(this.btnAddFreq_Click);
            // 
            // freqList
            // 
            this.freqList.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.freqList.ForeColor = System.Drawing.SystemColors.ControlText;
            this.freqList.FullRowSelect = true;
            this.freqList.GridLines = true;
            this.freqList.HideSelection = false;
            this.freqList.Location = new System.Drawing.Point(225, 25);
            this.freqList.Margin = new System.Windows.Forms.Padding(4);
            this.freqList.MultiSelect = false;
            this.freqList.Name = "freqList";
            this.freqList.Size = new System.Drawing.Size(606, 166);
            this.freqList.TabIndex = 2;
            this.freqList.UseCompatibleStateImageBehavior = false;
            this.freqList.View = System.Windows.Forms.View.Details;
            this.freqList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.freqList_KeyDown);
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDeleteItem.Location = new System.Drawing.Point(18, 75);
            this.btnDeleteItem.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(190, 30);
            this.btnDeleteItem.TabIndex = 3;
            this.btnDeleteItem.Text = "Delete";
            this.btnDeleteItem.UseVisualStyleBackColor = true;
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click);
            // 
            // lblCgContent
            // 
            this.lblCgContent.AutoSize = true;
            this.lblCgContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCgContent.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCgContent.Location = new System.Drawing.Point(10, 28);
            this.lblCgContent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCgContent.Name = "lblCgContent";
            this.lblCgContent.Size = new System.Drawing.Size(68, 17);
            this.lblCgContent.TabIndex = 0;
            this.lblCgContent.Text = "Preamble";
            // 
            // preambleContent
            // 
            this.preambleContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.preambleContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preambleContent.Location = new System.Drawing.Point(10, 50);
            this.preambleContent.Margin = new System.Windows.Forms.Padding(4);
            this.preambleContent.MaxLength = 2;
            this.preambleContent.Name = "preambleContent";
            this.preambleContent.Size = new System.Drawing.Size(34, 22);
            this.preambleContent.TabIndex = 0;
            this.preambleContent.Text = "AA";
            this.preambleContent.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.preambleContent_KeyPress);
            this.preambleContent.Validating += new System.ComponentModel.CancelEventHandler(this.preambleContent_Validating);
            // 
            // lblCgLen
            // 
            this.lblCgLen.AutoSize = true;
            this.lblCgLen.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCgLen.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCgLen.Location = new System.Drawing.Point(118, 52);
            this.lblCgLen.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCgLen.Name = "lblCgLen";
            this.lblCgLen.Size = new System.Drawing.Size(53, 17);
            this.lblCgLen.TabIndex = 2;
            this.lblCgLen.Text = "Byte(s)";
            // 
            // preambleLen
            // 
            this.preambleLen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.preambleLen.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.preambleLen.Location = new System.Drawing.Point(76, 50);
            this.preambleLen.Margin = new System.Windows.Forms.Padding(4);
            this.preambleLen.MaxLength = 2;
            this.preambleLen.Name = "preambleLen";
            this.preambleLen.Size = new System.Drawing.Size(34, 22);
            this.preambleLen.TabIndex = 1;
            this.preambleLen.Text = "4";
            this.preambleLen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.preambleLen_KeyPress);
            this.preambleLen.Validating += new System.ComponentModel.CancelEventHandler(this.preambleLen_Validating);
            // 
            // lblTxSync
            // 
            this.lblTxSync.AutoSize = true;
            this.lblTxSync.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTxSync.Location = new System.Drawing.Point(11, 30);
            this.lblTxSync.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTxSync.Name = "lblTxSync";
            this.lblTxSync.Size = new System.Drawing.Size(48, 17);
            this.lblTxSync.TabIndex = 19;
            this.lblTxSync.Text = "TX: 0x";
            // 
            // lblTxBitOrder
            // 
            this.lblTxBitOrder.AutoSize = true;
            this.lblTxBitOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTxBitOrder.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblTxBitOrder.Location = new System.Drawing.Point(255, 31);
            this.lblTxBitOrder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTxBitOrder.Name = "lblTxBitOrder";
            this.lblTxBitOrder.Size = new System.Drawing.Size(128, 15);
            this.lblTxBitOrder.TabIndex = 22;
            this.lblTxBitOrder.Text = "Transfer : 5A 0F BE 66";
            // 
            // lblRxSync
            // 
            this.lblRxSync.AutoSize = true;
            this.lblRxSync.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRxSync.Location = new System.Drawing.Point(11, 61);
            this.lblRxSync.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRxSync.Name = "lblRxSync";
            this.lblRxSync.Size = new System.Drawing.Size(49, 17);
            this.lblRxSync.TabIndex = 23;
            this.lblRxSync.Text = "RX: 0x";
            // 
            // rxSyncword
            // 
            this.rxSyncword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rxSyncword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.rxSyncword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rxSyncword.Location = new System.Drawing.Point(61, 58);
            this.rxSyncword.Margin = new System.Windows.Forms.Padding(4);
            this.rxSyncword.MaxLength = 8;
            this.rxSyncword.Name = "rxSyncword";
            this.rxSyncword.Size = new System.Drawing.Size(83, 23);
            this.rxSyncword.TabIndex = 13;
            this.rxSyncword.Text = "5A0FBE66";
            this.rxSyncword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rxSyncword_KeyPress);
            this.rxSyncword.Validating += new System.ComponentModel.CancelEventHandler(this.rxSyncword_Validating);
            // 
            // rxsyncInvertCheckbox
            // 
            this.rxsyncInvertCheckbox.AutoSize = true;
            this.rxsyncInvertCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rxsyncInvertCheckbox.Location = new System.Drawing.Point(151, 60);
            this.rxsyncInvertCheckbox.Margin = new System.Windows.Forms.Padding(4);
            this.rxsyncInvertCheckbox.Name = "rxsyncInvertCheckbox";
            this.rxsyncInvertCheckbox.Size = new System.Drawing.Size(94, 21);
            this.rxsyncInvertCheckbox.TabIndex = 14;
            this.rxsyncInvertCheckbox.Text = "Soft Invert";
            this.rxsyncInvertCheckbox.UseVisualStyleBackColor = true;
            this.rxsyncInvertCheckbox.CheckedChanged += new System.EventHandler(this.rxsyncInvertCheckbox_CheckedChanged);
            // 
            // rxBitOrder
            // 
            this.rxBitOrder.AutoSize = true;
            this.rxBitOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rxBitOrder.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.rxBitOrder.Location = new System.Drawing.Point(255, 62);
            this.rxBitOrder.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rxBitOrder.Name = "rxBitOrder";
            this.rxBitOrder.Size = new System.Drawing.Size(118, 15);
            this.rxBitOrder.TabIndex = 25;
            this.rxBitOrder.Text = "Detect : 5A 0F BE 66";
            // 
            // allowBitError
            // 
            this.allowBitError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.allowBitError.Location = new System.Drawing.Point(120, 89);
            this.allowBitError.Margin = new System.Windows.Forms.Padding(4);
            this.allowBitError.MaxLength = 1;
            this.allowBitError.Name = "allowBitError";
            this.allowBitError.Size = new System.Drawing.Size(34, 22);
            this.allowBitError.TabIndex = 0;
            this.allowBitError.Text = "1";
            this.allowBitError.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.allowBitError_KeyPress);
            this.allowBitError.Validating += new System.ComponentModel.CancelEventHandler(this.allowBitError_Validating);
            // 
            // lblCgAbe
            // 
            this.lblCgAbe.AutoSize = true;
            this.lblCgAbe.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCgAbe.Location = new System.Drawing.Point(162, 92);
            this.lblCgAbe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCgAbe.Name = "lblCgAbe";
            this.lblCgAbe.Size = new System.Drawing.Size(80, 17);
            this.lblCgAbe.TabIndex = 1;
            this.lblCgAbe.Text = "Bit(s), (0-7)";
            // 
            // grpPayloadOrder
            // 
            this.grpPayloadOrder.Controls.Add(this.payloadLSB);
            this.grpPayloadOrder.Controls.Add(this.payloadMSB);
            this.grpPayloadOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPayloadOrder.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpPayloadOrder.Location = new System.Drawing.Point(254, 25);
            this.grpPayloadOrder.Margin = new System.Windows.Forms.Padding(4);
            this.grpPayloadOrder.Name = "grpPayloadOrder";
            this.grpPayloadOrder.Padding = new System.Windows.Forms.Padding(4);
            this.grpPayloadOrder.Size = new System.Drawing.Size(157, 54);
            this.grpPayloadOrder.TabIndex = 34;
            this.grpPayloadOrder.TabStop = false;
            this.grpPayloadOrder.Text = "FIFO Data Order";
            // 
            // payloadLSB
            // 
            this.payloadLSB.AutoSize = true;
            this.payloadLSB.Checked = true;
            this.payloadLSB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.payloadLSB.Location = new System.Drawing.Point(12, 24);
            this.payloadLSB.Margin = new System.Windows.Forms.Padding(4);
            this.payloadLSB.Name = "payloadLSB";
            this.payloadLSB.Size = new System.Drawing.Size(55, 21);
            this.payloadLSB.TabIndex = 0;
            this.payloadLSB.TabStop = true;
            this.payloadLSB.Text = "LSB";
            this.payloadLSB.UseVisualStyleBackColor = true;
            // 
            // payloadMSB
            // 
            this.payloadMSB.AutoSize = true;
            this.payloadMSB.ForeColor = System.Drawing.SystemColors.ControlText;
            this.payloadMSB.Location = new System.Drawing.Point(72, 24);
            this.payloadMSB.Margin = new System.Windows.Forms.Padding(4);
            this.payloadMSB.Name = "payloadMSB";
            this.payloadMSB.Size = new System.Drawing.Size(58, 21);
            this.payloadMSB.TabIndex = 1;
            this.payloadMSB.Text = "MSB";
            this.payloadMSB.UseVisualStyleBackColor = true;
            // 
            // grpTxPayloadFmt
            // 
            this.grpTxPayloadFmt.Controls.Add(this.radioTransWithPresync);
            this.grpTxPayloadFmt.Controls.Add(this.radioTransparent);
            this.grpTxPayloadFmt.Controls.Add(this.radio802154);
            this.grpTxPayloadFmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.grpTxPayloadFmt.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpTxPayloadFmt.Location = new System.Drawing.Point(10, 8);
            this.grpTxPayloadFmt.Margin = new System.Windows.Forms.Padding(4);
            this.grpTxPayloadFmt.Name = "grpTxPayloadFmt";
            this.grpTxPayloadFmt.Padding = new System.Windows.Forms.Padding(4);
            this.grpTxPayloadFmt.Size = new System.Drawing.Size(212, 115);
            this.grpTxPayloadFmt.TabIndex = 4;
            this.grpTxPayloadFmt.TabStop = false;
            this.grpTxPayloadFmt.Text = "TX FIFO in MAC";
            // 
            // radioTransWithPresync
            // 
            this.radioTransWithPresync.AutoSize = true;
            this.radioTransWithPresync.Checked = true;
            this.radioTransWithPresync.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.radioTransWithPresync.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioTransWithPresync.Location = new System.Drawing.Point(8, 22);
            this.radioTransWithPresync.Margin = new System.Windows.Forms.Padding(4);
            this.radioTransWithPresync.Name = "radioTransWithPresync";
            this.radioTransWithPresync.Size = new System.Drawing.Size(166, 21);
            this.radioTransWithPresync.TabIndex = 0;
            this.radioTransWithPresync.TabStop = true;
            this.radioTransWithPresync.Text = "Transparent+Presync";
            this.radioTransWithPresync.UseVisualStyleBackColor = true;
            // 
            // radioTransparent
            // 
            this.radioTransparent.AutoSize = true;
            this.radioTransparent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.radioTransparent.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radioTransparent.Location = new System.Drawing.Point(8, 52);
            this.radioTransparent.Margin = new System.Windows.Forms.Padding(4);
            this.radioTransparent.Name = "radioTransparent";
            this.radioTransparent.Size = new System.Drawing.Size(107, 21);
            this.radioTransparent.TabIndex = 1;
            this.radioTransparent.Text = "Transparent";
            this.radioTransparent.UseVisualStyleBackColor = true;
            // 
            // radio802154
            // 
            this.radio802154.AutoSize = true;
            this.radio802154.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.radio802154.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radio802154.Location = new System.Drawing.Point(8, 82);
            this.radio802154.Margin = new System.Windows.Forms.Padding(4);
            this.radio802154.Name = "radio802154";
            this.radio802154.Size = new System.Drawing.Size(85, 21);
            this.radio802154.TabIndex = 2;
            this.radio802154.Text = "802.15.4";
            this.radio802154.UseVisualStyleBackColor = true;
            // 
            // grpPcrmu
            // 
            this.grpPcrmu.Controls.Add(this.pcrmuEnable);
            this.grpPcrmu.Controls.Add(this.pcrmuDisable);
            this.grpPcrmu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.grpPcrmu.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpPcrmu.Location = new System.Drawing.Point(10, 8);
            this.grpPcrmu.Margin = new System.Windows.Forms.Padding(4);
            this.grpPcrmu.Name = "grpPcrmu";
            this.grpPcrmu.Padding = new System.Windows.Forms.Padding(4);
            this.grpPcrmu.Size = new System.Drawing.Size(208, 60);
            this.grpPcrmu.TabIndex = 5;
            this.grpPcrmu.TabStop = false;
            this.grpPcrmu.Text = "PCR Management";
            // 
            // pcrmuEnable
            // 
            this.pcrmuEnable.AutoSize = true;
            this.pcrmuEnable.Checked = true;
            this.pcrmuEnable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.pcrmuEnable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pcrmuEnable.Location = new System.Drawing.Point(8, 25);
            this.pcrmuEnable.Margin = new System.Windows.Forms.Padding(4);
            this.pcrmuEnable.Name = "pcrmuEnable";
            this.pcrmuEnable.Size = new System.Drawing.Size(73, 21);
            this.pcrmuEnable.TabIndex = 0;
            this.pcrmuEnable.TabStop = true;
            this.pcrmuEnable.Text = "Enable";
            this.pcrmuEnable.UseVisualStyleBackColor = true;
            // 
            // pcrmuDisable
            // 
            this.pcrmuDisable.AutoSize = true;
            this.pcrmuDisable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.pcrmuDisable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pcrmuDisable.Location = new System.Drawing.Point(110, 25);
            this.pcrmuDisable.Margin = new System.Windows.Forms.Padding(4);
            this.pcrmuDisable.Name = "pcrmuDisable";
            this.pcrmuDisable.Size = new System.Drawing.Size(76, 21);
            this.pcrmuDisable.TabIndex = 1;
            this.pcrmuDisable.Text = "Disable";
            this.pcrmuDisable.UseVisualStyleBackColor = true;
            // 
            // grpDataFormat
            // 
            this.grpDataFormat.Controls.Add(this.radio8Bits);
            this.grpDataFormat.Controls.Add(this.radio32Bits);
            this.grpDataFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.grpDataFormat.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpDataFormat.Location = new System.Drawing.Point(10, 80);
            this.grpDataFormat.Margin = new System.Windows.Forms.Padding(4);
            this.grpDataFormat.Name = "grpDataFormat";
            this.grpDataFormat.Padding = new System.Windows.Forms.Padding(4);
            this.grpDataFormat.Size = new System.Drawing.Size(208, 60);
            this.grpDataFormat.TabIndex = 6;
            this.grpDataFormat.TabStop = false;
            this.grpDataFormat.Text = "Data Format";
            this.grpDataFormat.Visible = false;
            // 
            // radio8Bits
            // 
            this.radio8Bits.AutoSize = true;
            this.radio8Bits.Checked = true;
            this.radio8Bits.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.radio8Bits.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radio8Bits.Location = new System.Drawing.Point(8, 25);
            this.radio8Bits.Margin = new System.Windows.Forms.Padding(4);
            this.radio8Bits.Name = "radio8Bits";
            this.radio8Bits.Size = new System.Drawing.Size(59, 21);
            this.radio8Bits.TabIndex = 0;
            this.radio8Bits.TabStop = true;
            this.radio8Bits.Text = "8bits";
            this.radio8Bits.UseVisualStyleBackColor = true;
            // 
            // radio32Bits
            // 
            this.radio32Bits.AutoSize = true;
            this.radio32Bits.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.radio32Bits.ForeColor = System.Drawing.SystemColors.ControlText;
            this.radio32Bits.Location = new System.Drawing.Point(110, 25);
            this.radio32Bits.Margin = new System.Windows.Forms.Padding(4);
            this.radio32Bits.Name = "radio32Bits";
            this.radio32Bits.Size = new System.Drawing.Size(68, 21);
            this.radio32Bits.TabIndex = 1;
            this.radio32Bits.Text = "32Bits";
            this.radio32Bits.UseVisualStyleBackColor = true;
            // 
            // intModeGrp
            // 
            this.intModeGrp.Controls.Add(this.lblCgSel);
            this.intModeGrp.Controls.Add(this.showIntNum);
            this.intModeGrp.Controls.Add(this.lblCgItems);
            this.intModeGrp.Controls.Add(this.TXAEMPTY_Grp);
            this.intModeGrp.Controls.Add(this.RXFULL_Grp);
            this.intModeGrp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.intModeGrp.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.intModeGrp.Location = new System.Drawing.Point(10, 8);
            this.intModeGrp.Margin = new System.Windows.Forms.Padding(4);
            this.intModeGrp.Name = "intModeGrp";
            this.intModeGrp.Padding = new System.Windows.Forms.Padding(4);
            this.intModeGrp.Size = new System.Drawing.Size(213, 180);
            this.intModeGrp.TabIndex = 7;
            this.intModeGrp.TabStop = false;
            this.intModeGrp.Text = "Interrupt Event Selection";
            // 
            // lblCgSel
            // 
            this.lblCgSel.AutoSize = true;
            this.lblCgSel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblCgSel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCgSel.Location = new System.Drawing.Point(10, 22);
            this.lblCgSel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCgSel.Name = "lblCgSel";
            this.lblCgSel.Size = new System.Drawing.Size(67, 17);
            this.lblCgSel.TabIndex = 0;
            this.lblCgSel.Text = "Selected:";
            // 
            // showIntNum
            // 
            this.showIntNum.AutoSize = true;
            this.showIntNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.showIntNum.ForeColor = System.Drawing.SystemColors.ControlText;
            this.showIntNum.Location = new System.Drawing.Point(80, 22);
            this.showIntNum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.showIntNum.Name = "showIntNum";
            this.showIntNum.Size = new System.Drawing.Size(16, 17);
            this.showIntNum.TabIndex = 1;
            this.showIntNum.Text = "0";
            // 
            // lblCgItems
            // 
            this.lblCgItems.AutoSize = true;
            this.lblCgItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.lblCgItems.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCgItems.Location = new System.Drawing.Point(105, 22);
            this.lblCgItems.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCgItems.Name = "lblCgItems";
            this.lblCgItems.Size = new System.Drawing.Size(41, 17);
            this.lblCgItems.TabIndex = 2;
            this.lblCgItems.Text = "items";
            // 
            // TXAEMPTY_Grp
            // 
            this.TXAEMPTY_Grp.Controls.Add(this.txAemptyThr);
            this.TXAEMPTY_Grp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.TXAEMPTY_Grp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.TXAEMPTY_Grp.Location = new System.Drawing.Point(10, 85);
            this.TXAEMPTY_Grp.Margin = new System.Windows.Forms.Padding(4);
            this.TXAEMPTY_Grp.Name = "TXAEMPTY_Grp";
            this.TXAEMPTY_Grp.Padding = new System.Windows.Forms.Padding(4);
            this.TXAEMPTY_Grp.Size = new System.Drawing.Size(174, 45);
            this.TXAEMPTY_Grp.TabIndex = 3;
            this.TXAEMPTY_Grp.TabStop = false;
            this.TXAEMPTY_Grp.Text = "Tx_Aempty(0-128)";
            this.TXAEMPTY_Grp.Visible = false;
            // 
            // txAemptyThr
            // 
            this.txAemptyThr.Location = new System.Drawing.Point(122, 15);
            this.txAemptyThr.Margin = new System.Windows.Forms.Padding(4);
            this.txAemptyThr.MaxLength = 3;
            this.txAemptyThr.Name = "txAemptyThr";
            this.txAemptyThr.Size = new System.Drawing.Size(42, 23);
            this.txAemptyThr.TabIndex = 0;
            this.txAemptyThr.Text = "64";
            this.txAemptyThr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txAemptyThr_KeyPress);
            this.txAemptyThr.Validating += new System.ComponentModel.CancelEventHandler(this.txAemptyThr_Validating);
            // 
            // RXFULL_Grp
            // 
            this.RXFULL_Grp.Controls.Add(this.rxFullThr);
            this.RXFULL_Grp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.RXFULL_Grp.ForeColor = System.Drawing.SystemColors.ControlText;
            this.RXFULL_Grp.Location = new System.Drawing.Point(10, 132);
            this.RXFULL_Grp.Margin = new System.Windows.Forms.Padding(4);
            this.RXFULL_Grp.Name = "RXFULL_Grp";
            this.RXFULL_Grp.Padding = new System.Windows.Forms.Padding(4);
            this.RXFULL_Grp.Size = new System.Drawing.Size(174, 45);
            this.RXFULL_Grp.TabIndex = 4;
            this.RXFULL_Grp.TabStop = false;
            this.RXFULL_Grp.Text = "Rx_Afull(1-128)";
            this.RXFULL_Grp.Visible = false;
            // 
            // rxFullThr
            // 
            this.rxFullThr.Location = new System.Drawing.Point(122, 15);
            this.rxFullThr.Margin = new System.Windows.Forms.Padding(4);
            this.rxFullThr.MaxLength = 3;
            this.rxFullThr.Name = "rxFullThr";
            this.rxFullThr.Size = new System.Drawing.Size(42, 23);
            this.rxFullThr.TabIndex = 0;
            this.rxFullThr.Text = "64";
            this.rxFullThr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rxFullThr_KeyPress);
            this.rxFullThr.Validating += new System.ComponentModel.CancelEventHandler(this.rxFullThr_Validating);
            // 
            // grpGpio
            // 
            this.grpGpio.Controls.Add(this.GPIO0_Enable);
            this.grpGpio.Controls.Add(this.gpio0_Combo);
            this.grpGpio.Controls.Add(this.GPIO1_Enable);
            this.grpGpio.Controls.Add(this.gpio1_Combo);
            this.grpGpio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.grpGpio.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpGpio.Location = new System.Drawing.Point(10, 8);
            this.grpGpio.Margin = new System.Windows.Forms.Padding(4);
            this.grpGpio.Name = "grpGpio";
            this.grpGpio.Padding = new System.Windows.Forms.Padding(4);
            this.grpGpio.Size = new System.Drawing.Size(194, 150);
            this.grpGpio.TabIndex = 8;
            this.grpGpio.TabStop = false;
            this.grpGpio.Text = "GPIO Mode";
            // 
            // GPIO0_Enable
            // 
            this.GPIO0_Enable.AutoSize = true;
            this.GPIO0_Enable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.GPIO0_Enable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GPIO0_Enable.Location = new System.Drawing.Point(10, 22);
            this.GPIO0_Enable.Margin = new System.Windows.Forms.Padding(4);
            this.GPIO0_Enable.Name = "GPIO0_Enable";
            this.GPIO0_Enable.Size = new System.Drawing.Size(120, 21);
            this.GPIO0_Enable.TabIndex = 0;
            this.GPIO0_Enable.Text = "GPIO0 Enable";
            this.GPIO0_Enable.UseVisualStyleBackColor = true;
            this.GPIO0_Enable.CheckedChanged += new System.EventHandler(this.GPIO0_Enable_CheckedChanged);
            // 
            // gpio0_Combo
            // 
            this.gpio0_Combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gpio0_Combo.Enabled = false;
            this.gpio0_Combo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.gpio0_Combo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gpio0_Combo.FormattingEnabled = true;
            this.gpio0_Combo.Location = new System.Drawing.Point(10, 48);
            this.gpio0_Combo.Margin = new System.Windows.Forms.Padding(4);
            this.gpio0_Combo.Name = "gpio0_Combo";
            this.gpio0_Combo.Size = new System.Drawing.Size(172, 24);
            this.gpio0_Combo.TabIndex = 1;
            // 
            // GPIO1_Enable
            // 
            this.GPIO1_Enable.AutoSize = true;
            this.GPIO1_Enable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.GPIO1_Enable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.GPIO1_Enable.Location = new System.Drawing.Point(10, 82);
            this.GPIO1_Enable.Margin = new System.Windows.Forms.Padding(4);
            this.GPIO1_Enable.Name = "GPIO1_Enable";
            this.GPIO1_Enable.Size = new System.Drawing.Size(120, 21);
            this.GPIO1_Enable.TabIndex = 2;
            this.GPIO1_Enable.Text = "GPIO1 Enable";
            this.GPIO1_Enable.UseVisualStyleBackColor = true;
            this.GPIO1_Enable.CheckedChanged += new System.EventHandler(this.GPIO1_Enable_CheckedChanged);
            // 
            // gpio1_Combo
            // 
            this.gpio1_Combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gpio1_Combo.Enabled = false;
            this.gpio1_Combo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.gpio1_Combo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gpio1_Combo.FormattingEnabled = true;
            this.gpio1_Combo.Location = new System.Drawing.Point(10, 108);
            this.gpio1_Combo.Margin = new System.Windows.Forms.Padding(4);
            this.gpio1_Combo.Name = "gpio1_Combo";
            this.gpio1_Combo.Size = new System.Drawing.Size(172, 24);
            this.gpio1_Combo.TabIndex = 3;
            // 
            // btnClearInsLog
            // 
            this.btnClearInsLog.Location = new System.Drawing.Point(789, 715);
            this.btnClearInsLog.Margin = new System.Windows.Forms.Padding(2);
            this.btnClearInsLog.Name = "btnClearInsLog";
            this.btnClearInsLog.Size = new System.Drawing.Size(89, 92);
            this.btnClearInsLog.TabIndex = 33;
            this.btnClearInsLog.Text = "Clear";
            this.btnClearInsLog.UseVisualStyleBackColor = true;
            this.btnClearInsLog.Click += new System.EventHandler(this.btnClearInsLog_Click);
            // 
            // txResult
            // 
            this.txResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txResult.Location = new System.Drawing.Point(10, 715);
            this.txResult.Margin = new System.Windows.Forms.Padding(4);
            this.txResult.Multiline = true;
            this.txResult.Name = "txResult";
            this.txResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txResult.Size = new System.Drawing.Size(776, 92);
            this.txResult.TabIndex = 0;
            // 
            // syncLSBCheckbox
            // 
            this.syncLSBCheckbox.AutoSize = true;
            this.syncLSBCheckbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.syncLSBCheckbox.Location = new System.Drawing.Point(151, 29);
            this.syncLSBCheckbox.Margin = new System.Windows.Forms.Padding(2);
            this.syncLSBCheckbox.Name = "syncLSBCheckbox";
            this.syncLSBCheckbox.Size = new System.Drawing.Size(94, 21);
            this.syncLSBCheckbox.TabIndex = 12;
            this.syncLSBCheckbox.Text = "Soft Invert";
            this.syncLSBCheckbox.UseVisualStyleBackColor = true;
            this.syncLSBCheckbox.CheckedChanged += new System.EventHandler(this.syncLSBCheckbox_CheckedChanged);
            // 
            // capioStr
            // 
            this.capioStr.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.capioStr.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.capioStr.Location = new System.Drawing.Point(128, 55);
            this.capioStr.Margin = new System.Windows.Forms.Padding(2);
            this.capioStr.Name = "capioStr";
            this.capioStr.Size = new System.Drawing.Size(96, 22);
            this.capioStr.TabIndex = 29;
            this.capioStr.Text = "33";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label25.Location = new System.Drawing.Point(128, 28);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(101, 17);
            this.label25.TabIndex = 28;
            this.label25.Text = "Cap IO(0~127)";
            // 
            // pwrlvl
            // 
            this.pwrlvl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pwrlvl.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pwrlvl.Location = new System.Drawing.Point(128, 178);
            this.pwrlvl.Margin = new System.Windows.Forms.Padding(2);
            this.pwrlvl.Name = "pwrlvl";
            this.pwrlvl.Size = new System.Drawing.Size(96, 22);
            this.pwrlvl.TabIndex = 26;
            this.pwrlvl.Text = "127";
            // 
            // freqDev
            // 
            this.freqDev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.freqDev.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.freqDev.Location = new System.Drawing.Point(10, 178);
            this.freqDev.Margin = new System.Windows.Forms.Padding(4);
            this.freqDev.Name = "freqDev";
            this.freqDev.Size = new System.Drawing.Size(96, 22);
            this.freqDev.TabIndex = 21;
            this.freqDev.Text = "24";
            // 
            // PwrLvLabel
            // 
            this.PwrLvLabel.AutoSize = true;
            this.PwrLvLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PwrLvLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.PwrLvLabel.Location = new System.Drawing.Point(128, 150);
            this.PwrLvLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PwrLvLabel.Name = "PwrLvLabel";
            this.PwrLvLabel.Size = new System.Drawing.Size(118, 17);
            this.PwrLvLabel.TabIndex = 18;
            this.PwrLvLabel.Text = "Power LV(0~127)";
            this.PwrLvLabel.Click += new System.EventHandler(this.label13_Click);
            // 
            // txSyncword
            // 
            this.txSyncword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txSyncword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.txSyncword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txSyncword.Location = new System.Drawing.Point(61, 28);
            this.txSyncword.Margin = new System.Windows.Forms.Padding(4);
            this.txSyncword.MaxLength = 8;
            this.txSyncword.Name = "txSyncword";
            this.txSyncword.Size = new System.Drawing.Size(84, 23);
            this.txSyncword.TabIndex = 11;
            this.txSyncword.Text = "5A0FBE66";
            this.txSyncword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rxSyncword_KeyPress);
            this.txSyncword.Validating += new System.ComponentModel.CancelEventHandler(this.rxSyncword_Validating);
            // 
            // txDRCombo
            // 
            this.txDRCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txDRCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txDRCombo.FormattingEnabled = true;
            this.txDRCombo.Location = new System.Drawing.Point(10, 116);
            this.txDRCombo.Margin = new System.Windows.Forms.Padding(4);
            this.txDRCombo.Name = "txDRCombo";
            this.txDRCombo.Size = new System.Drawing.Size(96, 24);
            this.txDRCombo.TabIndex = 15;
            // 
            // txFreqText
            // 
            this.txFreqText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txFreqText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txFreqText.Location = new System.Drawing.Point(128, 116);
            this.txFreqText.Margin = new System.Windows.Forms.Padding(4);
            this.txFreqText.Name = "txFreqText";
            this.txFreqText.Size = new System.Drawing.Size(96, 22);
            this.txFreqText.TabIndex = 2;
            this.txFreqText.Text = "915";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(128, 89);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Frequency(MHz)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label15.Location = new System.Drawing.Point(10, 150);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(111, 17);
            this.label15.TabIndex = 7;
            this.label15.Text = "Deviation(±KHz)";
            // 
            // txModCombo
            // 
            this.txModCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txModCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txModCombo.FormattingEnabled = true;
            this.txModCombo.Location = new System.Drawing.Point(10, 55);
            this.txModCombo.Margin = new System.Windows.Forms.Padding(4);
            this.txModCombo.Name = "txModCombo";
            this.txModCombo.Size = new System.Drawing.Size(96, 24);
            this.txModCombo.TabIndex = 12;
            this.txModCombo.SelectedIndexChanged += new System.EventHandler(this.txModCombo_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label16.Location = new System.Drawing.Point(10, 28);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 17);
            this.label16.TabIndex = 5;
            this.label16.Text = "Modulation";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(10, 89);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(114, 17);
            this.label14.TabIndex = 3;
            this.label14.Text = "Data Rate(Kbps)";
            // 
            // PAConfigCombo
            // 
            this.PAConfigCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PAConfigCombo.FormattingEnabled = true;
            this.PAConfigCombo.Location = new System.Drawing.Point(12, 178);
            this.PAConfigCombo.Margin = new System.Windows.Forms.Padding(4);
            this.PAConfigCombo.Name = "PAConfigCombo";
            this.PAConfigCombo.Size = new System.Drawing.Size(96, 24);
            this.PAConfigCombo.TabIndex = 28;
            // 
            // maxPwrSelLabel
            // 
            this.maxPwrSelLabel.AutoSize = true;
            this.maxPwrSelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxPwrSelLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.maxPwrSelLabel.Location = new System.Drawing.Point(12, 150);
            this.maxPwrSelLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.maxPwrSelLabel.Name = "maxPwrSelLabel";
            this.maxPwrSelLabel.Size = new System.Drawing.Size(108, 17);
            this.maxPwrSelLabel.TabIndex = 26;
            this.maxPwrSelLabel.Text = "PA Config(dBm)";
            // 
            // btnTxSet
            // 
            this.btnTxSet.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnTxSet.Location = new System.Drawing.Point(15, 741);
            this.btnTxSet.Margin = new System.Windows.Forms.Padding(4);
            this.btnTxSet.Name = "btnTxSet";
            this.btnTxSet.Size = new System.Drawing.Size(205, 68);
            this.btnTxSet.TabIndex = 0;
            this.btnTxSet.Text = "Set";
            this.btnTxSet.UseVisualStyleBackColor = true;
            this.btnTxSet.Visible = false;
            this.btnTxSet.Click += new System.EventHandler(this.btnSetER4100_Click);
            // 
            // regPage
            // 
            this.regPage.AutoScroll = true;
            this.regPage.Controls.Add(this.label23);
            this.regPage.Controls.Add(this.label22);
            this.regPage.Controls.Add(this.initialDataPath);
            this.regPage.Controls.Add(this.label21);
            this.regPage.Controls.Add(this.searchTxt);
            this.regPage.Controls.Add(this.btnClrListview);
            this.regPage.Controls.Add(this.wrAllRegGrp);
            this.regPage.Controls.Add(this.dataGridView1);
            this.regPage.Controls.Add(this.insertValue);
            this.regPage.Controls.Add(this.btnRead1);
            this.regPage.Controls.Add(this.hexTxt);
            this.regPage.Controls.Add(this.txtValue);
            this.regPage.Controls.Add(this.btnRegWrite);
            this.regPage.Controls.Add(this.txtName);
            this.regPage.Controls.Add(this.txtID);
            this.regPage.Controls.Add(this.listView1);
            this.regPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.regPage.Location = new System.Drawing.Point(4, 34);
            this.regPage.Name = "regPage";
            this.regPage.Size = new System.Drawing.Size(1204, 769);
            this.regPage.TabIndex = 2;
            this.regPage.Text = "   Register Table   ";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(127, 550);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(23, 18);
            this.label23.TabIndex = 24;
            this.label23.Text = "0x";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(127, 503);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(23, 18);
            this.label22.TabIndex = 23;
            this.label22.Text = "0x";
            // 
            // initialDataPath
            // 
            this.initialDataPath.Font = new System.Drawing.Font("Footlight MT Light", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.initialDataPath.Location = new System.Drawing.Point(7, 8);
            this.initialDataPath.Name = "initialDataPath";
            this.initialDataPath.Size = new System.Drawing.Size(1008, 19);
            this.initialDataPath.TabIndex = 22;
            this.initialDataPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label21.Location = new System.Drawing.Point(423, 455);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(58, 18);
            this.label21.TabIndex = 21;
            this.label21.Text = "Search:";
            // 
            // searchTxt
            // 
            this.searchTxt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.searchTxt.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.searchTxt.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.searchTxt.Location = new System.Drawing.Point(514, 449);
            this.searchTxt.Name = "searchTxt";
            this.searchTxt.Size = new System.Drawing.Size(502, 25);
            this.searchTxt.TabIndex = 20;
            this.searchTxt.TextChanged += new System.EventHandler(this.searchTxt_TextChanged);
            // 
            // btnClrListview
            // 
            this.btnClrListview.Location = new System.Drawing.Point(1042, 563);
            this.btnClrListview.Name = "btnClrListview";
            this.btnClrListview.Size = new System.Drawing.Size(130, 41);
            this.btnClrListview.TabIndex = 3;
            this.btnClrListview.Text = "Clear";
            this.btnClrListview.UseVisualStyleBackColor = true;
            this.btnClrListview.Visible = false;
            this.btnClrListview.Click += new System.EventHandler(this.btnClrListview_Click);
            // 
            // wrAllRegGrp
            // 
            this.wrAllRegGrp.Controls.Add(this.btnTableConnect);
            this.wrAllRegGrp.Controls.Add(this.btnTableComRefresh);
            this.wrAllRegGrp.Controls.Add(this.tableCombobox);
            this.wrAllRegGrp.Controls.Add(this.btnWriteAll);
            this.wrAllRegGrp.Controls.Add(this.regRead);
            this.wrAllRegGrp.Location = new System.Drawing.Point(1025, 28);
            this.wrAllRegGrp.Name = "wrAllRegGrp";
            this.wrAllRegGrp.Size = new System.Drawing.Size(165, 413);
            this.wrAllRegGrp.TabIndex = 1;
            this.wrAllRegGrp.TabStop = false;
            // 
            // btnTableConnect
            // 
            this.btnTableConnect.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnTableConnect.Location = new System.Drawing.Point(6, 141);
            this.btnTableConnect.Name = "btnTableConnect";
            this.btnTableConnect.Size = new System.Drawing.Size(146, 52);
            this.btnTableConnect.TabIndex = 29;
            this.btnTableConnect.Text = "Connect";
            this.btnTableConnect.UseVisualStyleBackColor = true;
            this.btnTableConnect.Click += new System.EventHandler(this.btnTableConnect_Click);
            // 
            // btnTableComRefresh
            // 
            this.btnTableComRefresh.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnTableComRefresh.Location = new System.Drawing.Point(6, 32);
            this.btnTableComRefresh.Name = "btnTableComRefresh";
            this.btnTableComRefresh.Size = new System.Drawing.Size(146, 52);
            this.btnTableComRefresh.TabIndex = 28;
            this.btnTableComRefresh.Text = "Resfresh";
            this.btnTableComRefresh.UseVisualStyleBackColor = true;
            this.btnTableComRefresh.Click += new System.EventHandler(this.btnTableComRefresh_Click);
            // 
            // tableCombobox
            // 
            this.tableCombobox.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableCombobox.FormattingEnabled = true;
            this.tableCombobox.Location = new System.Drawing.Point(6, 90);
            this.tableCombobox.Name = "tableCombobox";
            this.tableCombobox.Size = new System.Drawing.Size(146, 25);
            this.tableCombobox.TabIndex = 27;
            // 
            // btnWriteAll
            // 
            this.btnWriteAll.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnWriteAll.Location = new System.Drawing.Point(6, 293);
            this.btnWriteAll.Name = "btnWriteAll";
            this.btnWriteAll.Size = new System.Drawing.Size(146, 52);
            this.btnWriteAll.TabIndex = 2;
            this.btnWriteAll.Text = "Write All";
            this.btnWriteAll.UseVisualStyleBackColor = true;
            this.btnWriteAll.Click += new System.EventHandler(this.btnWriteAll_Click);
            // 
            // regRead
            // 
            this.regRead.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.regRead.Location = new System.Drawing.Point(6, 218);
            this.regRead.Name = "regRead";
            this.regRead.Size = new System.Drawing.Size(146, 52);
            this.regRead.TabIndex = 0;
            this.regRead.Text = "Read All";
            this.regRead.UseVisualStyleBackColor = true;
            this.regRead.Click += new System.EventHandler(this.regRead_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameColumn,
            this.bitColumn,
            this.valueColumn});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(421, 495);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(594, 264);
            this.dataGridView1.TabIndex = 19;
            // 
            // nameColumn
            // 
            this.nameColumn.FillWeight = 130F;
            this.nameColumn.HeaderText = "Location";
            this.nameColumn.MinimumWidth = 6;
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.nameColumn.Width = 280;
            // 
            // bitColumn
            // 
            this.bitColumn.HeaderText = "Bit";
            this.bitColumn.MinimumWidth = 6;
            this.bitColumn.Name = "bitColumn";
            this.bitColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.bitColumn.Width = 80;
            // 
            // valueColumn
            // 
            this.valueColumn.FillWeight = 70F;
            this.valueColumn.HeaderText = "Value";
            this.valueColumn.MinimumWidth = 6;
            this.valueColumn.Name = "valueColumn";
            this.valueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.valueColumn.Width = 150;
            // 
            // insertValue
            // 
            this.insertValue.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.insertValue.Location = new System.Drawing.Point(7, 590);
            this.insertValue.Multiline = true;
            this.insertValue.Name = "insertValue";
            this.insertValue.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.insertValue.Size = new System.Drawing.Size(408, 169);
            this.insertValue.TabIndex = 18;
            // 
            // btnRead1
            // 
            this.btnRead1.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRead1.Location = new System.Drawing.Point(7, 541);
            this.btnRead1.Name = "btnRead1";
            this.btnRead1.Size = new System.Drawing.Size(114, 42);
            this.btnRead1.TabIndex = 17;
            this.btnRead1.Text = "Read";
            this.btnRead1.UseVisualStyleBackColor = true;
            this.btnRead1.Click += new System.EventHandler(this.btnTree_Click);
            // 
            // hexTxt
            // 
            this.hexTxt.BackColor = System.Drawing.SystemColors.Info;
            this.hexTxt.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hexTxt.ForeColor = System.Drawing.SystemColors.Highlight;
            this.hexTxt.Location = new System.Drawing.Point(160, 546);
            this.hexTxt.Name = "hexTxt";
            this.hexTxt.Size = new System.Drawing.Size(255, 25);
            this.hexTxt.TabIndex = 16;
            // 
            // txtValue
            // 
            this.txtValue.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValue.Location = new System.Drawing.Point(160, 500);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(255, 25);
            this.txtValue.TabIndex = 14;
            // 
            // btnRegWrite
            // 
            this.btnRegWrite.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegWrite.Location = new System.Drawing.Point(7, 495);
            this.btnRegWrite.Name = "btnRegWrite";
            this.btnRegWrite.Size = new System.Drawing.Size(114, 41);
            this.btnRegWrite.TabIndex = 15;
            this.btnRegWrite.Text = "Write";
            this.btnRegWrite.UseVisualStyleBackColor = true;
            this.btnRegWrite.Click += new System.EventHandler(this.btnRegWrite_Click);
            // 
            // txtName
            // 
            this.txtName.Enabled = false;
            this.txtName.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtName.Location = new System.Drawing.Point(127, 447);
            this.txtName.Multiline = true;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(288, 42);
            this.txtName.TabIndex = 13;
            // 
            // txtID
            // 
            this.txtID.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtID.Font = new System.Drawing.Font("Book Antiqua", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(7, 447);
            this.txtID.MaxLength = 4;
            this.txtID.Multiline = true;
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(114, 43);
            this.txtID.TabIndex = 12;
            this.txtID.TextChanged += new System.EventHandler(this.txtID_TextChanged);
            // 
            // listView1
            // 
            this.listView1.Font = new System.Drawing.Font("微軟正黑體", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(10, 39);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1009, 402);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // regPage2
            // 
            this.regPage2.Location = new System.Drawing.Point(4, 26);
            this.regPage2.Name = "regPage2";
            this.regPage2.Padding = new System.Windows.Forms.Padding(3);
            this.regPage2.Size = new System.Drawing.Size(1028, 717);
            this.regPage2.TabIndex = 1;
            this.regPage2.Text = "     F000-B000     ";
            this.regPage2.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.comPortToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1405, 32);
            this.menuStrip1.TabIndex = 41;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.loadFileMenuItem,
            this.loadParameterToolStripMenuItem,
            this.saveParameterToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.aboutToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(44, 28);
            this.aboutToolStripMenuItem.Text = "File";
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
            this.loadFileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadInitFileToolStripMenuItem});
            this.loadFileMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.loadFileMenuItem.Name = "loadFileMenuItem";
            this.loadFileMenuItem.Size = new System.Drawing.Size(192, 26);
            this.loadFileMenuItem.Text = "Load";
            this.loadFileMenuItem.Visible = false;
            // 
            // loadInitFileToolStripMenuItem
            // 
            this.loadInitFileToolStripMenuItem.Name = "loadInitFileToolStripMenuItem";
            this.loadInitFileToolStripMenuItem.Size = new System.Drawing.Size(170, 26);
            this.loadInitFileToolStripMenuItem.Text = "Load Init File";
            this.loadInitFileToolStripMenuItem.Click += new System.EventHandler(this.loadInitFileToolStripMenuItem_Click);
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
            // comPortToolStripMenuItem
            // 
            this.comPortToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.comPortToolStripMenuItem.Name = "comPortToolStripMenuItem";
            this.comPortToolStripMenuItem.Size = new System.Drawing.Size(125, 28);
            this.comPortToolStripMenuItem.Text = "DVB Connection";
            this.comPortToolStripMenuItem.Click += new System.EventHandler(this.comPortToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(59, 28);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click_1);
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
            this.comPortToolStripComboBox.Size = new System.Drawing.Size(121, 23);
            // 
            // connectComToolStripMenuItem
            // 
            this.connectComToolStripMenuItem.Name = "connectComToolStripMenuItem";
            this.connectComToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.connectComToolStripMenuItem.Text = "CONNECT";
            this.connectComToolStripMenuItem.Click += new System.EventHandler(this.connectComToolStripMenuItem_Click);
            // 
            // paramTab
            // 
            this.paramTab.Controls.Add(this.pageParameter);
            this.paramTab.Controls.Add(this.pageFreqTable);
            this.paramTab.Controls.Add(this.pageTxPayload);
            this.paramTab.Controls.Add(this.pageIntMode);
            this.paramTab.Controls.Add(this.pageGpio);
            this.paramTab.Controls.Add(this.pagePcrmu);
            this.paramTab.ItemSize = new System.Drawing.Size(100, 24);
            this.paramTab.Location = new System.Drawing.Point(10, 54);
            this.paramTab.Margin = new System.Windows.Forms.Padding(4);
            this.paramTab.Name = "paramTab";
            this.paramTab.SelectedIndex = 0;
            this.paramTab.Size = new System.Drawing.Size(868, 264);
            this.paramTab.TabIndex = 42;
            this.paramTab.Tag = "";
            // 
            // pageParameter
            // 
            this.pageParameter.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pageParameter.Controls.Add(this.grpPktParam);
            this.pageParameter.Controls.Add(this.grpModAndDR);
            this.pageParameter.Controls.Add(this.grpFreqAndPwr);
            this.pageParameter.Location = new System.Drawing.Point(4, 28);
            this.pageParameter.Margin = new System.Windows.Forms.Padding(4);
            this.pageParameter.Name = "pageParameter";
            this.pageParameter.Padding = new System.Windows.Forms.Padding(4);
            this.pageParameter.Size = new System.Drawing.Size(860, 232);
            this.pageParameter.TabIndex = 0;
            this.pageParameter.Text = "RF PARAM";
            this.pageParameter.Click += new System.EventHandler(this.pageParameter_Click_1);
            // 
            // grpPktParam
            // 
            this.grpPktParam.Controls.Add(this.grpSyncword);
            this.grpPktParam.Controls.Add(this.lblCgContent);
            this.grpPktParam.Controls.Add(this.grpPayloadOrder);
            this.grpPktParam.Controls.Add(this.preambleContent);
            this.grpPktParam.Controls.Add(this.preambleLen);
            this.grpPktParam.Controls.Add(this.lblCgLen);
            this.grpPktParam.Controls.Add(this.label3);
            this.grpPktParam.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpPktParam.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpPktParam.Location = new System.Drawing.Point(423, 9);
            this.grpPktParam.Margin = new System.Windows.Forms.Padding(2);
            this.grpPktParam.Name = "grpPktParam";
            this.grpPktParam.Padding = new System.Windows.Forms.Padding(2);
            this.grpPktParam.Size = new System.Drawing.Size(431, 212);
            this.grpPktParam.TabIndex = 37;
            this.grpPktParam.TabStop = false;
            this.grpPktParam.Text = "Packet";
            // 
            // grpSyncword
            // 
            this.grpSyncword.Controls.Add(this.lblRxSyncTolerance);
            this.grpSyncword.Controls.Add(this.allowBitError);
            this.grpSyncword.Controls.Add(this.lblRxSync);
            this.grpSyncword.Controls.Add(this.lblCgAbe);
            this.grpSyncword.Controls.Add(this.syncLSBCheckbox);
            this.grpSyncword.Controls.Add(this.rxBitOrder);
            this.grpSyncword.Controls.Add(this.lblTxSync);
            this.grpSyncword.Controls.Add(this.rxsyncInvertCheckbox);
            this.grpSyncword.Controls.Add(this.lblTxBitOrder);
            this.grpSyncword.Controls.Add(this.rxSyncword);
            this.grpSyncword.Controls.Add(this.txSyncword);
            this.grpSyncword.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSyncword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpSyncword.Location = new System.Drawing.Point(10, 81);
            this.grpSyncword.Margin = new System.Windows.Forms.Padding(2);
            this.grpSyncword.Name = "grpSyncword";
            this.grpSyncword.Padding = new System.Windows.Forms.Padding(2);
            this.grpSyncword.Size = new System.Drawing.Size(401, 119);
            this.grpSyncword.TabIndex = 35;
            this.grpSyncword.TabStop = false;
            this.grpSyncword.Text = "Syncword(4Bytes)";
            // 
            // lblRxSyncTolerance
            // 
            this.lblRxSyncTolerance.AutoSize = true;
            this.lblRxSyncTolerance.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRxSyncTolerance.Location = new System.Drawing.Point(16, 92);
            this.lblRxSyncTolerance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRxSyncTolerance.Name = "lblRxSyncTolerance";
            this.lblRxSyncTolerance.Size = new System.Drawing.Size(96, 17);
            this.lblRxSyncTolerance.TabIndex = 26;
            this.lblRxSyncTolerance.Text = "Allow Bit Error";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(55, 52);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "x";
            // 
            // grpModAndDR
            // 
            this.grpModAndDR.Controls.Add(this.btnDevComment);
            this.grpModAndDR.Controls.Add(this.txModCombo);
            this.grpModAndDR.Controls.Add(this.label16);
            this.grpModAndDR.Controls.Add(this.freqDev);
            this.grpModAndDR.Controls.Add(this.label15);
            this.grpModAndDR.Controls.Add(this.txDRCombo);
            this.grpModAndDR.Controls.Add(this.label14);
            this.grpModAndDR.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpModAndDR.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpModAndDR.Location = new System.Drawing.Point(265, 9);
            this.grpModAndDR.Margin = new System.Windows.Forms.Padding(2);
            this.grpModAndDR.Name = "grpModAndDR";
            this.grpModAndDR.Padding = new System.Windows.Forms.Padding(2);
            this.grpModAndDR.Size = new System.Drawing.Size(151, 212);
            this.grpModAndDR.TabIndex = 36;
            this.grpModAndDR.TabStop = false;
            this.grpModAndDR.Text = "Modem";
            // 
            // btnDevComment
            // 
            this.btnDevComment.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDevComment.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDevComment.Location = new System.Drawing.Point(113, 177);
            this.btnDevComment.Name = "btnDevComment";
            this.btnDevComment.Size = new System.Drawing.Size(28, 23);
            this.btnDevComment.TabIndex = 22;
            this.btnDevComment.Text = "?";
            this.btnDevComment.UseVisualStyleBackColor = true;
            this.btnDevComment.Click += new System.EventHandler(this.btnDevComment_Click);
            // 
            // grpFreqAndPwr
            // 
            this.grpFreqAndPwr.Controls.Add(this.capioStr);
            this.grpFreqAndPwr.Controls.Add(this.xtalCombo);
            this.grpFreqAndPwr.Controls.Add(this.label4);
            this.grpFreqAndPwr.Controls.Add(this.label25);
            this.grpFreqAndPwr.Controls.Add(this.PAConfigCombo);
            this.grpFreqAndPwr.Controls.Add(this.pwrlvl);
            this.grpFreqAndPwr.Controls.Add(this.DVBBandCombo);
            this.grpFreqAndPwr.Controls.Add(this.label5);
            this.grpFreqAndPwr.Controls.Add(this.label6);
            this.grpFreqAndPwr.Controls.Add(this.PwrLvLabel);
            this.grpFreqAndPwr.Controls.Add(this.txFreqText);
            this.grpFreqAndPwr.Controls.Add(this.maxPwrSelLabel);
            this.grpFreqAndPwr.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFreqAndPwr.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.grpFreqAndPwr.Location = new System.Drawing.Point(6, 9);
            this.grpFreqAndPwr.Margin = new System.Windows.Forms.Padding(2);
            this.grpFreqAndPwr.Name = "grpFreqAndPwr";
            this.grpFreqAndPwr.Padding = new System.Windows.Forms.Padding(2);
            this.grpFreqAndPwr.Size = new System.Drawing.Size(252, 212);
            this.grpFreqAndPwr.TabIndex = 35;
            this.grpFreqAndPwr.TabStop = false;
            this.grpFreqAndPwr.Text = "Frequency and Power";
            // 
            // xtalCombo
            // 
            this.xtalCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtalCombo.FormattingEnabled = true;
            this.xtalCombo.Location = new System.Drawing.Point(12, 55);
            this.xtalCombo.Margin = new System.Windows.Forms.Padding(2);
            this.xtalCombo.Name = "xtalCombo";
            this.xtalCombo.Size = new System.Drawing.Size(96, 24);
            this.xtalCombo.TabIndex = 33;
            this.xtalCombo.SelectedIndexChanged += new System.EventHandler(this.xtalCombo_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(12, 89);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 17);
            this.label4.TabIndex = 29;
            this.label4.Text = "DVB Band(MHz)";
            // 
            // DVBBandCombo
            // 
            this.DVBBandCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DVBBandCombo.FormattingEnabled = true;
            this.DVBBandCombo.Location = new System.Drawing.Point(12, 116);
            this.DVBBandCombo.Margin = new System.Windows.Forms.Padding(2);
            this.DVBBandCombo.Name = "DVBBandCombo";
            this.DVBBandCombo.Size = new System.Drawing.Size(96, 24);
            this.DVBBandCombo.TabIndex = 32;
            this.DVBBandCombo.SelectedIndexChanged += new System.EventHandler(this.DVBBandCombo_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(12, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 17);
            this.label6.TabIndex = 34;
            this.label6.Text = "Xtal(MHz)";
            // 
            // pageFreqTable
            // 
            this.pageFreqTable.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pageFreqTable.Controls.Add(this.grpFreqTable);
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
            this.pageTxPayload.Controls.Add(this.labelForPayloadFormat);
            this.pageTxPayload.Controls.Add(this.grpTxPayloadFmt);
            this.pageTxPayload.Location = new System.Drawing.Point(4, 28);
            this.pageTxPayload.Margin = new System.Windows.Forms.Padding(4);
            this.pageTxPayload.Name = "pageTxPayload";
            this.pageTxPayload.Padding = new System.Windows.Forms.Padding(4);
            this.pageTxPayload.Size = new System.Drawing.Size(860, 232);
            this.pageTxPayload.TabIndex = 2;
            this.pageTxPayload.Text = "TX FIFO MODE";
            // 
            // labelForPayloadFormat
            // 
            this.labelForPayloadFormat.AutoSize = true;
            this.labelForPayloadFormat.BackColor = System.Drawing.Color.OldLace;
            this.labelForPayloadFormat.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelForPayloadFormat.ForeColor = System.Drawing.Color.Black;
            this.labelForPayloadFormat.Location = new System.Drawing.Point(10, 135);
            this.labelForPayloadFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelForPayloadFormat.Name = "labelForPayloadFormat";
            this.labelForPayloadFormat.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.labelForPayloadFormat.Size = new System.Drawing.Size(581, 70);
            this.labelForPayloadFormat.TabIndex = 5;
            this.labelForPayloadFormat.Text = "Only for Register setting generation\r\nthis setting determines how the MAC layer p" +
    "ackets data using the FIFO.\r\nRX FIFO must be configured via API in the firmware " +
    "flow.";
            // 
            // pageIntMode
            // 
            this.pageIntMode.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pageIntMode.Controls.Add(this.intModeGrp);
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
            this.pageGpio.Controls.Add(this.grpGpio);
            this.pageGpio.Location = new System.Drawing.Point(4, 28);
            this.pageGpio.Margin = new System.Windows.Forms.Padding(4);
            this.pageGpio.Name = "pageGpio";
            this.pageGpio.Padding = new System.Windows.Forms.Padding(4);
            this.pageGpio.Size = new System.Drawing.Size(860, 232);
            this.pageGpio.TabIndex = 4;
            this.pageGpio.Text = "GPIO";
            // 
            // pagePcrmu
            // 
            this.pagePcrmu.BackColor = System.Drawing.Color.LightSteelBlue;
            this.pagePcrmu.Controls.Add(this.grpPcrmu);
            this.pagePcrmu.Controls.Add(this.grpDataFormat);
            this.pagePcrmu.Location = new System.Drawing.Point(4, 28);
            this.pagePcrmu.Margin = new System.Windows.Forms.Padding(4);
            this.pagePcrmu.Name = "pagePcrmu";
            this.pagePcrmu.Padding = new System.Windows.Forms.Padding(4);
            this.pagePcrmu.Size = new System.Drawing.Size(860, 232);
            this.pagePcrmu.TabIndex = 5;
            this.pagePcrmu.Text = "PCR";
            // 
            // btnGen
            // 
            this.btnGen.Location = new System.Drawing.Point(1190, 765);
            this.btnGen.Margin = new System.Windows.Forms.Padding(4);
            this.btnGen.Name = "btnGen";
            this.btnGen.Size = new System.Drawing.Size(96, 44);
            this.btnGen.TabIndex = 44;
            this.btnGen.Text = "Generate";
            this.btnGen.UseVisualStyleBackColor = true;
            this.btnGen.Click += new System.EventHandler(this.btnGen_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(1296, 765);
            this.btnDefault.Margin = new System.Windows.Forms.Padding(4);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(96, 44);
            this.btnDefault.TabIndex = 47;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // regGroup
            // 
            this.regGroup.BackColor = System.Drawing.Color.Transparent;
            this.regGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.regGroup.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.regGroup.Location = new System.Drawing.Point(891, 36);
            this.regGroup.Margin = new System.Windows.Forms.Padding(4);
            this.regGroup.Name = "regGroup";
            this.regGroup.Padding = new System.Windows.Forms.Padding(4);
            this.regGroup.Size = new System.Drawing.Size(501, 694);
            this.regGroup.TabIndex = 46;
            this.regGroup.TabStop = false;
            this.regGroup.Text = "REG Table";
            // 
            // LabelTopicDVB
            // 
            this.LabelTopicDVB.AutoSize = true;
            this.LabelTopicDVB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTopicDVB.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.LabelTopicDVB.Location = new System.Drawing.Point(11, 322);
            this.LabelTopicDVB.Name = "LabelTopicDVB";
            this.LabelTopicDVB.Size = new System.Drawing.Size(108, 20);
            this.LabelTopicDVB.TabIndex = 48;
            this.LabelTopicDVB.Text = "DVB Tester";
            // 
            // LabelTopicPRAM
            // 
            this.LabelTopicPRAM.AutoSize = true;
            this.LabelTopicPRAM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTopicPRAM.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.LabelTopicPRAM.Location = new System.Drawing.Point(10, 32);
            this.LabelTopicPRAM.Name = "LabelTopicPRAM";
            this.LabelTopicPRAM.Size = new System.Drawing.Size(96, 20);
            this.LabelTopicPRAM.TabIndex = 48;
            this.LabelTopicPRAM.Text = "Parameter";
            // 
            // btnReadDVB
            // 
            this.btnReadDVB.Location = new System.Drawing.Point(891, 763);
            this.btnReadDVB.Name = "btnReadDVB";
            this.btnReadDVB.Size = new System.Drawing.Size(96, 44);
            this.btnReadDVB.TabIndex = 49;
            this.btnReadDVB.Text = "Read DVB Register";
            this.btnReadDVB.UseVisualStyleBackColor = true;
            this.btnReadDVB.Click += new System.EventHandler(this.btnReadDVB_Click);
            // 
            // btnWriteDVB
            // 
            this.btnWriteDVB.Location = new System.Drawing.Point(997, 763);
            this.btnWriteDVB.Name = "btnWriteDVB";
            this.btnWriteDVB.Size = new System.Drawing.Size(96, 44);
            this.btnWriteDVB.TabIndex = 50;
            this.btnWriteDVB.Text = "Write DVB Register";
            this.btnWriteDVB.UseVisualStyleBackColor = true;
            this.btnWriteDVB.Click += new System.EventHandler(this.btnWriteDVB_Click);
            // 
            // IncludeInitArrayCheckBox
            // 
            this.IncludeInitArrayCheckBox.AutoSize = true;
            this.IncludeInitArrayCheckBox.Checked = true;
            this.IncludeInitArrayCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.IncludeInitArrayCheckBox.Location = new System.Drawing.Point(1190, 737);
            this.IncludeInitArrayCheckBox.Name = "IncludeInitArrayCheckBox";
            this.IncludeInitArrayCheckBox.Size = new System.Drawing.Size(135, 21);
            this.IncludeInitArrayCheckBox.TabIndex = 51;
            this.IncludeInitArrayCheckBox.Text = "Include Init Array";
            this.IncludeInitArrayCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(1405, 838);
            this.Controls.Add(this.IncludeInitArrayCheckBox);
            this.Controls.Add(this.btnWriteDVB);
            this.Controls.Add(this.btnReadDVB);
            this.Controls.Add(this.LabelTopicPRAM);
            this.Controls.Add(this.LabelTopicDVB);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.regGroup);
            this.Controls.Add(this.btnGen);
            this.Controls.Add(this.btnClearInsLog);
            this.Controls.Add(this.paramTab);
            this.Controls.Add(this.txResult);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnTxSet);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "ER4100  RF Test Tool 20260826 V1.20L";
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
            this.tabControl1.ResumeLayout(false);
            this.packetTxPage.ResumeLayout(false);
            this.packetTxPage.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.continusTxPage.ResumeLayout(false);
            this.continueTxGrp.ResumeLayout(false);
            this.patternGrp.ResumeLayout(false);
            this.patternGrp.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.packetRxPage.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.packetCountGroup.ResumeLayout(false);
            this.packetCountGroup.PerformLayout();
            this.packetParserGroup.ResumeLayout(false);
            this.packetParserGroup.PerformLayout();
            this.statePage.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpFreqTable.ResumeLayout(false);
            this.grpFreqTable.PerformLayout();
            this.grpPayloadOrder.ResumeLayout(false);
            this.grpPayloadOrder.PerformLayout();
            this.grpTxPayloadFmt.ResumeLayout(false);
            this.grpTxPayloadFmt.PerformLayout();
            this.grpPcrmu.ResumeLayout(false);
            this.grpPcrmu.PerformLayout();
            this.grpDataFormat.ResumeLayout(false);
            this.grpDataFormat.PerformLayout();
            this.intModeGrp.ResumeLayout(false);
            this.intModeGrp.PerformLayout();
            this.TXAEMPTY_Grp.ResumeLayout(false);
            this.TXAEMPTY_Grp.PerformLayout();
            this.RXFULL_Grp.ResumeLayout(false);
            this.RXFULL_Grp.PerformLayout();
            this.grpGpio.ResumeLayout(false);
            this.grpGpio.PerformLayout();
            this.regPage.ResumeLayout(false);
            this.regPage.PerformLayout();
            this.wrAllRegGrp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.paramTab.ResumeLayout(false);
            this.pageParameter.ResumeLayout(false);
            this.grpPktParam.ResumeLayout(false);
            this.grpPktParam.PerformLayout();
            this.grpSyncword.ResumeLayout(false);
            this.grpSyncword.PerformLayout();
            this.grpModAndDR.ResumeLayout(false);
            this.grpModAndDR.PerformLayout();
            this.grpFreqAndPwr.ResumeLayout(false);
            this.grpFreqAndPwr.PerformLayout();
            this.pageFreqTable.ResumeLayout(false);
            this.pageTxPayload.ResumeLayout(false);
            this.pageTxPayload.PerformLayout();
            this.pageIntMode.ResumeLayout(false);
            this.pageGpio.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage continusTxPage;
        private System.Windows.Forms.TabPage regPage;
        private System.Windows.Forms.TabPage regPage2;
        private System.Windows.Forms.GroupBox wrAllRegGrp;
        private System.Windows.Forms.Button regRead;
        private System.Windows.Forms.ColumnHeader registerList;
        private System.Windows.Forms.ColumnHeader registerName;
        private System.Windows.Forms.ColumnHeader registerValue;
        private System.Windows.Forms.Button btnClrListview;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnWriteAll;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox searchTxt;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox insertValue;
        private System.Windows.Forms.Button btnRead1;
        private System.Windows.Forms.TextBox hexTxt;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Button btnRegWrite;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label initialDataPath;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn bitColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueColumn;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TabPage packetTxPage;
        private System.Windows.Forms.TextBox txResult;
        private System.Windows.Forms.Button btnTxSet;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox txModCombo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem comPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshComToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox comPortToolStripComboBox;
        private System.Windows.Forms.ToolStripMenuItem connectComToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveInitValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadInitFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ComboBox tableCombobox;
        private System.Windows.Forms.Button btnTableComRefresh;
        private System.Windows.Forms.TextBox txSyncword;
        private System.Windows.Forms.Button btnTableConnect;
        private System.IO.Ports.SerialPort serialPort2;
        private System.Windows.Forms.ComboBox txDRCombo;
        private System.IO.Ports.SerialPort serialPort3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox randomLen;
        private System.Windows.Forms.TextBox numOfPkt;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label randomLenLable;
        private System.Windows.Forms.Button btnSetPayload;
        private System.Windows.Forms.TabPage packetRxPage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label rssiLabel;
        private System.Windows.Forms.TextBox rssiValue;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox rcvOkText;
        private System.Windows.Forms.Label PwrLvLabel;
        private System.Windows.Forms.TextBox dataTxt;
        private System.Windows.Forms.TextBox freqDev;
        private System.Windows.Forms.Button btnCalCrc;
        private System.Windows.Forms.CheckBox infiniteCheckbox;
        private System.Windows.Forms.Button btnSendStart;
        private System.Windows.Forms.CheckBox randomCheckbox;
        private System.Windows.Forms.Button btnSendStop;
        private System.Windows.Forms.CheckBox addSeqCheckbox;
        private System.Windows.Forms.Label addSeqLevel;
        private System.Windows.Forms.TextBox lenOfHex;
        private System.Windows.Forms.Label lenOfTxtLable;
        private System.Windows.Forms.Label maxPwrSelLabel;
        private System.Windows.Forms.ComboBox PAConfigCombo;
        private System.Windows.Forms.Button btnClearInsLog;
        private System.Windows.Forms.GroupBox continueTxGrp;
        private System.Windows.Forms.Button btnCWOn;
        private System.Windows.Forms.Button btnCWOff;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox crcSelectCombo;
        private System.Windows.Forms.TabControl paramTab;
        private System.Windows.Forms.TabPage pageParameter;
        private System.Windows.Forms.TabPage pageFreqTable;
        private System.Windows.Forms.TabPage pageTxPayload;
        private System.Windows.Forms.TabPage pageIntMode;
        private System.Windows.Forms.TabPage pageGpio;
        private System.Windows.Forms.TabPage pagePcrmu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox DVBBandCombo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox xtalCombo;
        private System.Windows.Forms.TextBox pwrlvl;
        private System.Windows.Forms.Button btnRxStart;
        private System.Windows.Forms.TextBox rxResult;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox rcvErrText;
        private System.Windows.Forms.GroupBox packetParserGroup;
        private System.Windows.Forms.CheckBox rxParseSeq;
        private System.Windows.Forms.Label crcLabel;
        private System.Windows.Forms.ComboBox packetRxCrcCombo;
        private System.Windows.Forms.GroupBox packetCountGroup;
        private System.Windows.Forms.RadioButton packetRxLimitRadio;
        private System.Windows.Forms.TextBox packetRxNum;
        private System.Windows.Forms.RadioButton packetRxNoLimitRadio;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton modulationRadio;
        private System.Windows.Forms.RadioButton singleToneRadio;
        private System.Windows.Forms.TextBox capioStr;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.CheckBox syncLSBCheckbox;
        private System.Windows.Forms.GroupBox patternGrp;
        private System.Windows.Forms.RadioButton prbs9Radio;
        private System.Windows.Forms.RadioButton pr55Radio;
        private System.Windows.Forms.TabPage statePage;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button btnStateSet;
        private System.Windows.Forms.RadioButton deepSleepRadioBtn;
        private System.Windows.Forms.RadioButton idleRadioBtn;
        private System.Windows.Forms.Button btnScanStart;
        private System.Windows.Forms.RadioButton shutdownRadioBtn;
        private System.Windows.Forms.Button btnPayloadClear;
        private System.Windows.Forms.Button btnGen;
        private System.Windows.Forms.ToolStripMenuItem loadParameterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveParameterToolStripMenuItem;
        private System.Windows.Forms.TabPage regWrPage;
        private System.Windows.Forms.GroupBox grpFreqTable;
        private System.Windows.Forms.TextBox inFreq;
        private System.Windows.Forms.Label lblCgMhz;
        private System.Windows.Forms.Button btnAddFreq;
        private System.Windows.Forms.ListView freqList;
        private System.Windows.Forms.Button btnDeleteItem;
        private System.Windows.Forms.Label lblCgContent;
        private System.Windows.Forms.TextBox preambleContent;
        private System.Windows.Forms.Label lblCgLen;
        private System.Windows.Forms.TextBox preambleLen;
        private System.Windows.Forms.Label lblTxSync;
        private System.Windows.Forms.Label lblTxBitOrder;
        private System.Windows.Forms.Label lblRxSync;
        private System.Windows.Forms.TextBox rxSyncword;
        private System.Windows.Forms.CheckBox rxsyncInvertCheckbox;
        private System.Windows.Forms.Label rxBitOrder;
        private System.Windows.Forms.Label lblCgAbe;
        private System.Windows.Forms.TextBox allowBitError;
        private System.Windows.Forms.GroupBox grpPayloadOrder;
        private System.Windows.Forms.RadioButton payloadLSB;
        private System.Windows.Forms.RadioButton payloadMSB;
        private System.Windows.Forms.GroupBox grpTxPayloadFmt;
        private System.Windows.Forms.RadioButton radioTransWithPresync;
        private System.Windows.Forms.RadioButton radioTransparent;
        private System.Windows.Forms.RadioButton radio802154;
        private System.Windows.Forms.GroupBox grpPcrmu;
        private System.Windows.Forms.RadioButton pcrmuEnable;
        private System.Windows.Forms.RadioButton pcrmuDisable;
        private System.Windows.Forms.GroupBox grpDataFormat;
        private System.Windows.Forms.RadioButton radio8Bits;
        private System.Windows.Forms.RadioButton radio32Bits;
        private System.Windows.Forms.GroupBox intModeGrp;
        private System.Windows.Forms.Label lblCgSel;
        private System.Windows.Forms.Label showIntNum;
        private System.Windows.Forms.Label lblCgItems;
        private System.Windows.Forms.GroupBox TXAEMPTY_Grp;
        private System.Windows.Forms.TextBox txAemptyThr;
        private System.Windows.Forms.GroupBox RXFULL_Grp;
        private System.Windows.Forms.TextBox rxFullThr;
        private System.Windows.Forms.GroupBox grpGpio;
        private System.Windows.Forms.CheckBox GPIO0_Enable;
        private System.Windows.Forms.ComboBox gpio0_Combo;
        private System.Windows.Forms.CheckBox GPIO1_Enable;
        private System.Windows.Forms.ComboBox gpio1_Combo;
        private System.Windows.Forms.Label labelForPayloadFormat;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.GroupBox grpModAndDR;
        private System.Windows.Forms.GroupBox grpFreqAndPwr;
        private System.Windows.Forms.TextBox txFreqText;
        private System.Windows.Forms.GroupBox grpPktParam;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox grpSyncword;
        private System.Windows.Forms.Label lblRxSyncTolerance;
        private System.Windows.Forms.GroupBox regGroup;
        private System.Windows.Forms.Label LabelTopicDVB;
        private System.Windows.Forms.Label LabelTopicPRAM;
        private System.Windows.Forms.Button btnReadDVB;
        private System.Windows.Forms.Button btnWriteDVB;
        private System.Windows.Forms.CheckBox IncludeInitArrayCheckBox;
        private System.Windows.Forms.Button btnDevComment;
        private System.Windows.Forms.Label txCrcPolyLabel;
    }
}

