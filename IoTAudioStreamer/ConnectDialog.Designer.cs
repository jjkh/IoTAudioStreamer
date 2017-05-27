namespace IoTAudioStreamer
{
    partial class ConnectDialog
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
            this.ipAddrTxtBox = new System.Windows.Forms.TextBox();
            this.portTxtBox = new System.Windows.Forms.TextBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ipAddrTxtBox
            // 
            this.ipAddrTxtBox.Location = new System.Drawing.Point(12, 12);
            this.ipAddrTxtBox.Name = "ipAddrTxtBox";
            this.ipAddrTxtBox.Size = new System.Drawing.Size(389, 38);
            this.ipAddrTxtBox.TabIndex = 0;
            // 
            // portTxtBox
            // 
            this.portTxtBox.Location = new System.Drawing.Point(407, 12);
            this.portTxtBox.Name = "portTxtBox";
            this.portTxtBox.Size = new System.Drawing.Size(109, 38);
            this.portTxtBox.TabIndex = 0;
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(12, 57);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(504, 73);
            this.connectBtn.TabIndex = 1;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // ConnectDialog
            // 
            this.AcceptButton = this.connectBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 142);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.portTxtBox);
            this.Controls.Add(this.ipAddrTxtBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ConnectDialog";
            this.Text = "Connect to AudioStreamer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ipAddrTxtBox;
        private System.Windows.Forms.TextBox portTxtBox;
        private System.Windows.Forms.Button connectBtn;
    }
}