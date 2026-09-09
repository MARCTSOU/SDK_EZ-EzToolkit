namespace ER41xxTestTool
{
    partial class ComPortSelectDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dvbPortLabel = new System.Windows.Forms.Label();
            this.dvbPortCombo = new System.Windows.Forms.ComboBox();
            this.dvbPortRefreshButton = new System.Windows.Forms.Button();
            this.dvbPortBaudLabel = new System.Windows.Forms.Label();
            this.dvbPortDeviceInfoGroup = new System.Windows.Forms.GroupBox();
            this.dvbPortDeviceNameLabel = new System.Windows.Forms.Label();
            this.dvbPortVidPidLabel = new System.Windows.Forms.Label();
            this.dvbConnectButton = new System.Windows.Forms.Button();
            this.dvbDisconnectButton = new System.Windows.Forms.Button();
            this.dvbConnCancelButton = new System.Windows.Forms.Button();
            this.dvbPortDeviceInfoGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // dvbPortLabel
            // 
            this.dvbPortLabel.AutoSize = true;
            this.dvbPortLabel.Location = new System.Drawing.Point(15, 25);
            this.dvbPortLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dvbPortLabel.Name = "dvbPortLabel";
            this.dvbPortLabel.Size = new System.Drawing.Size(34, 17);
            this.dvbPortLabel.TabIndex = 0;
            this.dvbPortLabel.Text = "Port";
            // 
            // dvbPortCombo
            // 
            this.dvbPortCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dvbPortCombo.Location = new System.Drawing.Point(75, 20);
            this.dvbPortCombo.Margin = new System.Windows.Forms.Padding(4);
            this.dvbPortCombo.Name = "dvbPortCombo";
            this.dvbPortCombo.Size = new System.Drawing.Size(189, 24);
            this.dvbPortCombo.TabIndex = 1;
            this.dvbPortCombo.SelectedIndexChanged += new System.EventHandler(this.dvbPortCombo_SelectedIndexChanged);
            // 
            // dvbPortRefreshButton
            // 
            this.dvbPortRefreshButton.Location = new System.Drawing.Point(272, 19);
            this.dvbPortRefreshButton.Margin = new System.Windows.Forms.Padding(4);
            this.dvbPortRefreshButton.Name = "dvbPortRefreshButton";
            this.dvbPortRefreshButton.Size = new System.Drawing.Size(88, 29);
            this.dvbPortRefreshButton.TabIndex = 2;
            this.dvbPortRefreshButton.Text = "Refresh";
            this.dvbPortRefreshButton.UseVisualStyleBackColor = true;
            this.dvbPortRefreshButton.Click += new System.EventHandler(this.dvbPortRefreshButton_Click);
            // 
            // dvbPortBaudLabel
            // 
            this.dvbPortBaudLabel.AutoSize = true;
            this.dvbPortBaudLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dvbPortBaudLabel.Location = new System.Drawing.Point(75, 55);
            this.dvbPortBaudLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dvbPortBaudLabel.Name = "dvbPortBaudLabel";
            this.dvbPortBaudLabel.Size = new System.Drawing.Size(141, 17);
            this.dvbPortBaudLabel.TabIndex = 3;
            this.dvbPortBaudLabel.Text = "Baudrate: 57600 bps";
            // 
            // dvbPortDeviceInfoGroup
            // 
            this.dvbPortDeviceInfoGroup.Controls.Add(this.dvbPortDeviceNameLabel);
            this.dvbPortDeviceInfoGroup.Controls.Add(this.dvbPortVidPidLabel);
            this.dvbPortDeviceInfoGroup.Location = new System.Drawing.Point(15, 80);
            this.dvbPortDeviceInfoGroup.Margin = new System.Windows.Forms.Padding(4);
            this.dvbPortDeviceInfoGroup.Name = "dvbPortDeviceInfoGroup";
            this.dvbPortDeviceInfoGroup.Padding = new System.Windows.Forms.Padding(4);
            this.dvbPortDeviceInfoGroup.Size = new System.Drawing.Size(345, 94);
            this.dvbPortDeviceInfoGroup.TabIndex = 4;
            this.dvbPortDeviceInfoGroup.TabStop = false;
            this.dvbPortDeviceInfoGroup.Text = "Device Info";
            // 
            // dvbPortDeviceNameLabel
            // 
            this.dvbPortDeviceNameLabel.AutoSize = true;
            this.dvbPortDeviceNameLabel.Location = new System.Drawing.Point(10, 29);
            this.dvbPortDeviceNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dvbPortDeviceNameLabel.Name = "dvbPortDeviceNameLabel";
            this.dvbPortDeviceNameLabel.Size = new System.Drawing.Size(167, 17);
            this.dvbPortDeviceNameLabel.TabIndex = 0;
            this.dvbPortDeviceNameLabel.Text = "USB Serial Port (COM12)";
            // 
            // dvbPortVidPidLabel
            // 
            this.dvbPortVidPidLabel.AutoSize = true;
            this.dvbPortVidPidLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.dvbPortVidPidLabel.Location = new System.Drawing.Point(10, 63);
            this.dvbPortVidPidLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dvbPortVidPidLabel.Name = "dvbPortVidPidLabel";
            this.dvbPortVidPidLabel.Size = new System.Drawing.Size(140, 17);
            this.dvbPortVidPidLabel.TabIndex = 1;
            this.dvbPortVidPidLabel.Text = "VID: 0403  PID: 6001";
            // 
            // dvbConnectButton
            // 
            this.dvbConnectButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.dvbConnectButton.Location = new System.Drawing.Point(15, 182);
            this.dvbConnectButton.Margin = new System.Windows.Forms.Padding(4);
            this.dvbConnectButton.Name = "dvbConnectButton";
            this.dvbConnectButton.Size = new System.Drawing.Size(100, 43);
            this.dvbConnectButton.TabIndex = 5;
            this.dvbConnectButton.Text = "Connect";
            this.dvbConnectButton.UseVisualStyleBackColor = true;
            this.dvbConnectButton.Click += new System.EventHandler(this.dvbConnectButton_Click);
            // 
            // dvbDisconnectButton
            // 
            this.dvbDisconnectButton.Location = new System.Drawing.Point(137, 182);
            this.dvbDisconnectButton.Margin = new System.Windows.Forms.Padding(4);
            this.dvbDisconnectButton.Name = "dvbDisconnectButton";
            this.dvbDisconnectButton.Size = new System.Drawing.Size(100, 43);
            this.dvbDisconnectButton.TabIndex = 6;
            this.dvbDisconnectButton.Text = "Disconnect";
            this.dvbDisconnectButton.UseVisualStyleBackColor = true;
            this.dvbDisconnectButton.Click += new System.EventHandler(this.dvbDisconnectButton_Click);
            // 
            // dvbConnCancelButton
            // 
            this.dvbConnCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.dvbConnCancelButton.Location = new System.Drawing.Point(259, 182);
            this.dvbConnCancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.dvbConnCancelButton.Name = "dvbConnCancelButton";
            this.dvbConnCancelButton.Size = new System.Drawing.Size(100, 43);
            this.dvbConnCancelButton.TabIndex = 7;
            this.dvbConnCancelButton.Text = "Cancel";
            this.dvbConnCancelButton.UseVisualStyleBackColor = true;
            // 
            // ComPortSelectDialog
            // 
            this.AcceptButton = this.dvbConnectButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.dvbConnCancelButton;
            this.ClientSize = new System.Drawing.Size(375, 243);
            this.Controls.Add(this.dvbPortLabel);
            this.Controls.Add(this.dvbPortCombo);
            this.Controls.Add(this.dvbPortRefreshButton);
            this.Controls.Add(this.dvbPortBaudLabel);
            this.Controls.Add(this.dvbPortDeviceInfoGroup);
            this.Controls.Add(this.dvbConnectButton);
            this.Controls.Add(this.dvbDisconnectButton);
            this.Controls.Add(this.dvbConnCancelButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ComPortSelectDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connect to ER4100-DVB";
            this.dvbPortDeviceInfoGroup.ResumeLayout(false);
            this.dvbPortDeviceInfoGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dvbPortLabel;
        private System.Windows.Forms.ComboBox dvbPortCombo;
        private System.Windows.Forms.Button dvbPortRefreshButton;
        private System.Windows.Forms.Label dvbPortBaudLabel;
        private System.Windows.Forms.GroupBox dvbPortDeviceInfoGroup;
        private System.Windows.Forms.Label dvbPortDeviceNameLabel;
        private System.Windows.Forms.Label dvbPortVidPidLabel;
        private System.Windows.Forms.Button dvbConnectButton;
        private System.Windows.Forms.Button dvbDisconnectButton;
        private System.Windows.Forms.Button dvbConnCancelButton;
    }
}
