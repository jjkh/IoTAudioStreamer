using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IoTAudioStreamer
{
    public partial class ConnectDialog : Form
    {
        public ConnectionInfo ConnInfo
        {
            get
            {
                return new ConnectionInfo(ipAddrTxtBox.Text,
                    Convert.ToInt16(portTxtBox.Text));
            }
        }

        public ConnectDialog(string defaultIp = "")
        {
            InitializeComponent();
            ipAddrTxtBox.Text = defaultIp;
        }
        
        private void connectBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
