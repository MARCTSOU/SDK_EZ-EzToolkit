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
            this.portLabel = new System.Windows.Forms.Label();
            this.portCombo = new System.Windows.Forms.ComboBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.baudLabel = new System.Windows.Forms.Label();
            this.deviceInfoGroup = new System.Windows.Forms.GroupBox();
            this.deviceNameLabel = new System.Windows.Forms.Label();
            this.vidPidLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.deviceInfoGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(15, 25);
            this.portLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(34, 17);
            this.portLabel.TabIndex = 0;
            this.portLabel.Text = "Port";
            // 
            // portCombo
            // 
            this.portCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portCombo.Location = new System.Drawing.Point(75, 20);
            this.portCombo.Margin = new System.Windows.Forms.Padding(4);
            this.portCombo.Name = "portCombo";
            this.portCombo.Size = new System.Drawing.Size(189, 24);
            this.portCombo.TabIndex = 1;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(272, 19);
            this.refreshButton.Margin = new System.Windows.Forms.Padding(4);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(88, 29);
            this.refreshButton.TabIndex = 2;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            // 
            // baudLabel
            // 
            this.baudLabel.AutoSize = true;
            this.baudLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.baudLabel.Location = new System.Drawing.Point(75, 55);
            this.baudLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.baudLabel.Name = "baudLabel";
            this.baudLabel.Size = new System.Drawing.Size(141, 17);
            this.baudLabel.TabIndex = 3;
            this.baudLabel.Text = "Baudrate: 57600 bps";
            // 
            // deviceInfoGroup
            // 
            this.deviceInfoGroup.Controls.Add(this.deviceNameLabel);
            this.deviceInfoGroup.Controls.Add(this.vidPidLabel);
            this.deviceInfoGroup.Location = new System.Drawing.Point(15, 80);
            this.deviceInfoGroup.Margin = new System.Windows.Forms.Padding(4);
            this.deviceInfoGroup.Name = "deviceInfoGroup";
            this.deviceInfoGroup.Padding = new System.Windows.Forms.Padding(4);
            this.deviceInfoGroup.Size = new System.Drawing.Size(345, 94);
            this.deviceInfoGroup.TabIndex = 4;
            this.deviceInfoGroup.TabStop = false;
            this.deviceInfoGroup.Text = "Device Info";
            // 
            // deviceNameLabel
            // 
            this.deviceNameLabel.AutoSize = true;
            this.deviceNameLabel.Location = new System.Drawing.Point(10, 29);
            this.deviceNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.deviceNameLabel.Name = "deviceNameLabel";
            this.deviceNameLabel.Size = new System.Drawing.Size(167, 17);
            this.deviceNameLabel.TabIndex = 0;
            this.deviceNameLabel.Text = "USB Serial Port (COM12)";
            // 
            // vidPidLabel
            // 
            this.vidPidLabel.AutoSize = true;
            this.vidPidLabel.ForeColor = System.Drawing.SystemColors.GrayText;
            this.vidPidLabel.Location = new System.Drawing.Point(10, 63);
            this.vidPidLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.vidPidLabel.Name = "vidPidLabel";
            this.vidPidLabel.Size = new System.Drawing.Size(140, 17);
            this.vidPidLabel.TabIndex = 1;
            this.vidPidLabel.Text = "VID: 0403  PID: 6001";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(15, 182);
            this.okButton.Margin = new System.Windows.Forms.Padding(4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 43);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "Connect";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // disconnectButton
            // 
            this.disconnectButton.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.disconnectButton.Location = new System.Drawing.Point(137, 182);
            this.disconnectButton.Margin = new System.Windows.Forms.Padding(4);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(100, 43);
            this.disconnectButton.TabIndex = 6;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(259, 182);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(4);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 43);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // ComPortSelectDialog
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(375, 243);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.portCombo);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.baudLabel);
            this.Controls.Add(this.deviceInfoGroup);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.cancelButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ComPortSelectDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connect to ER4100-DVB";
            this.deviceInfoGroup.ResumeLayout(false);
            this.deviceInfoGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.ComboBox portCombo;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.Label baudLabel;
        private System.Windows.Forms.GroupBox deviceInfoGroup;
        private System.Windows.Forms.Label deviceNameLabel;
        private System.Windows.Forms.Label vidPidLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Button cancelButton;
    }
}
