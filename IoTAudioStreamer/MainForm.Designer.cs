namespace IoTAudioStreamer
{
    partial class MainForm
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
            this.connectBtn = new System.Windows.Forms.Button();
            this.outputTxtBox = new System.Windows.Forms.TextBox();
            this.sendChkBox = new System.Windows.Forms.CheckBox();
            this.receiveChkBox = new System.Windows.Forms.CheckBox();
            this.statusLbl = new System.Windows.Forms.Label();
            this.willReceiveChkBox = new System.Windows.Forms.CheckBox();
            this.willSendChkBox = new System.Windows.Forms.CheckBox();
            this.listenBtn = new System.Windows.Forms.Button();
            this.disconnectBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(13, 82);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(194, 64);
            this.connectBtn.TabIndex = 0;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_ClickAsync);
            // 
            // outputTxtBox
            // 
            this.outputTxtBox.Location = new System.Drawing.Point(13, 152);
            this.outputTxtBox.Multiline = true;
            this.outputTxtBox.Name = "outputTxtBox";
            this.outputTxtBox.ReadOnly = true;
            this.outputTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTxtBox.Size = new System.Drawing.Size(960, 284);
            this.outputTxtBox.TabIndex = 1;
            // 
            // sendChkBox
            // 
            this.sendChkBox.AutoSize = true;
            this.sendChkBox.Enabled = false;
            this.sendChkBox.Location = new System.Drawing.Point(290, 27);
            this.sendChkBox.Name = "sendChkBox";
            this.sendChkBox.Size = new System.Drawing.Size(201, 36);
            this.sendChkBox.TabIndex = 2;
            this.sendChkBox.Text = "Send Audio";
            this.sendChkBox.UseVisualStyleBackColor = true;
            this.sendChkBox.CheckedChanged += new System.EventHandler(this.checkboxChangedAsync);
            // 
            // receiveChkBox
            // 
            this.receiveChkBox.AutoSize = true;
            this.receiveChkBox.Enabled = false;
            this.receiveChkBox.Location = new System.Drawing.Point(290, 97);
            this.receiveChkBox.Name = "receiveChkBox";
            this.receiveChkBox.Size = new System.Drawing.Size(237, 36);
            this.receiveChkBox.TabIndex = 2;
            this.receiveChkBox.Text = "Receive Audio";
            this.receiveChkBox.UseVisualStyleBackColor = true;
            this.receiveChkBox.CheckedChanged += new System.EventHandler(this.checkboxChangedAsync);
            // 
            // statusLbl
            // 
            this.statusLbl.AutoSize = true;
            this.statusLbl.Location = new System.Drawing.Point(13, 439);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(194, 32);
            this.statusLbl.TabIndex = 3;
            this.statusLbl.Text = "not connected";
            // 
            // willReceiveChkBox
            // 
            this.willReceiveChkBox.AutoCheck = false;
            this.willReceiveChkBox.AutoSize = true;
            this.willReceiveChkBox.Enabled = false;
            this.willReceiveChkBox.Location = new System.Drawing.Point(639, 27);
            this.willReceiveChkBox.Name = "willReceiveChkBox";
            this.willReceiveChkBox.Size = new System.Drawing.Size(210, 36);
            this.willReceiveChkBox.TabIndex = 2;
            this.willReceiveChkBox.Text = "Will Receive";
            this.willReceiveChkBox.UseVisualStyleBackColor = true;
            this.willReceiveChkBox.CheckedChanged += new System.EventHandler(this.checkboxChangedAsync);
            // 
            // willSendChkBox
            // 
            this.willSendChkBox.AutoCheck = false;
            this.willSendChkBox.AutoSize = true;
            this.willSendChkBox.Enabled = false;
            this.willSendChkBox.Location = new System.Drawing.Point(639, 97);
            this.willSendChkBox.Name = "willSendChkBox";
            this.willSendChkBox.Size = new System.Drawing.Size(174, 36);
            this.willSendChkBox.TabIndex = 2;
            this.willSendChkBox.Text = "Will Send";
            this.willSendChkBox.UseVisualStyleBackColor = true;
            this.willSendChkBox.CheckedChanged += new System.EventHandler(this.checkboxChangedAsync);
            // 
            // listenBtn
            // 
            this.listenBtn.Location = new System.Drawing.Point(12, 12);
            this.listenBtn.Name = "listenBtn";
            this.listenBtn.Size = new System.Drawing.Size(194, 64);
            this.listenBtn.TabIndex = 0;
            this.listenBtn.Text = "LIsten";
            this.listenBtn.UseVisualStyleBackColor = true;
            this.listenBtn.Click += new System.EventHandler(this.listenBtn_ClickAsync);
            // 
            // disconnectBtn
            // 
            this.disconnectBtn.Location = new System.Drawing.Point(12, 12);
            this.disconnectBtn.Name = "disconnectBtn";
            this.disconnectBtn.Size = new System.Drawing.Size(194, 134);
            this.disconnectBtn.TabIndex = 4;
            this.disconnectBtn.Text = "Disconnect";
            this.disconnectBtn.UseVisualStyleBackColor = true;
            this.disconnectBtn.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 483);
            this.Controls.Add(this.statusLbl);
            this.Controls.Add(this.willSendChkBox);
            this.Controls.Add(this.willReceiveChkBox);
            this.Controls.Add(this.receiveChkBox);
            this.Controls.Add(this.sendChkBox);
            this.Controls.Add(this.outputTxtBox);
            this.Controls.Add(this.listenBtn);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.disconnectBtn);
            this.Name = "MainForm";
            this.Text = "Audio Streamer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.TextBox outputTxtBox;
        private System.Windows.Forms.CheckBox sendChkBox;
        private System.Windows.Forms.CheckBox receiveChkBox;
        private System.Windows.Forms.Label statusLbl;
        private System.Windows.Forms.CheckBox willReceiveChkBox;
        private System.Windows.Forms.CheckBox willSendChkBox;
        private System.Windows.Forms.Button listenBtn;
        private System.Windows.Forms.Button disconnectBtn;
    }
}

