using MessagePack;
using NAudio.Lame;
using NAudio.Wave;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IoTAudioStreamer
{
    public enum ProgState
    {
        NotConnected,
        Listening,
        Connecting,
        Connected
    }

    public struct ConnectionInfo
    {
        public IPAddress IpAddress;
        public int Port;

        public ConnectionInfo(string ipAddr, int port)
        {
            IpAddress = IPAddress.Parse(ipAddr);
            Port = port;
        }
    }

    public partial class MainForm : Form
    {
        private ProgState _progState = ProgState.NotConnected;
        private bool _sending = false;
        private bool _receiving = false;
        private SslStream _sslClient;
        private X509Certificate2 _cert = new X509Certificate2("server.pfx", "IoTBox");
        private MemoryStream _inStream = new MemoryStream();
        private MemoryStream _outStream = new MemoryStream();
        private LameMP3FileWriter _writer = null;
        private Thread _audioThread = null;

        private static IPAddress GetLocalIPAddress()
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint.Address;
            }
        }

        public MainForm()
        {
            InitializeComponent();
        }
        
        private async void listenBtn_ClickAsync(object sender, EventArgs e)
        {
            if (_progState != ProgState.NotConnected)
                return;
            _progState = ProgState.Listening;
            listenBtn.Enabled = false;
            connectBtn.Enabled = false;

            await StartServerAsync();

            statusLbl.Text = "client connected";
            _progState = ProgState.Connected;
            receiveChkBox.Enabled = true;
            sendChkBox.Enabled = true;
            willReceiveChkBox.Enabled = true;
            willSendChkBox.Enabled = true;
            try
            {
                while (_progState == ProgState.Connected)
                {

                    await ReceiveDataAsync();
                }
            }
            catch (IOException)
            {
                // socket gone
                this.Close();
            }
            finally
            {
                _audioThread.Abort();
            }
        }

        private async Task StartServerAsync()
        {
            var server = new TcpListener(IPAddress.Any, 0);
            server.Start();
            statusLbl.Text = String.Format("listening on {0} at port {1}",
                GetLocalIPAddress(),
                ((IPEndPoint)server.LocalEndpoint).Port);
            TcpClient tcpClient = await server.AcceptTcpClientAsync();
            _sslClient = new SslStream(tcpClient.GetStream(), false);
            await _sslClient.AuthenticateAsServerAsync(_cert, false, SslProtocols.Tls, false);
            outputTxtBox.AppendText(String.Format("new client connected: {0}\r\n",
                tcpClient.Client.RemoteEndPoint));
        }

        private async void connectBtn_ClickAsync(object sender, EventArgs e)
        {
            if (_progState != ProgState.NotConnected)
                return;

            var connDialog = new ConnectDialog("192.168.1.21");
            if (connDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            _progState = ProgState.Connecting;
            listenBtn.Enabled = false;
            connectBtn.Enabled = false;
            statusLbl.Text = "connecting to server";

            await ConnectServerAsync(connDialog.ConnInfo);

            statusLbl.Text = "connected to server";
            _progState = ProgState.Connected;
            receiveChkBox.Enabled = true;
            sendChkBox.Enabled = true;
            willReceiveChkBox.Enabled = true;
            willSendChkBox.Enabled = true;

            try
            {
                while (_progState == ProgState.Connected)
                {
                    await ReceiveDataAsync();
                }
            }
            catch (IOException)
            {
                this.Close();
            }
            finally
            {
                _audioThread.Abort();
            }
        }

        private async Task ConnectServerAsync(ConnectionInfo connInfo)
        {
            TcpClient tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(connInfo.IpAddress, connInfo.Port);
            _sslClient = new SslStream(tcpClient.GetStream(), false,
                 new RemoteCertificateValidationCallback((obj, a, b, c) => true));
            await _sslClient.AuthenticateAsClientAsync("iot.duality.co.nz");
        }

        private async void checkboxChangedAsync(object sender, EventArgs e)
        {
            if (_progState == ProgState.Connected)
            {
                _receiving = receiveChkBox.Checked && willSendChkBox.Checked;
                if (_receiving && _audioThread == null)
                {
                    _audioThread = new Thread(new ThreadStart(PlayAudio));
                    _audioThread.Start();
                }
                else if (_audioThread != null)
                {
                    _audioThread.Abort();
                    _audioThread = null;
                }

                _sending = sendChkBox.Checked && willReceiveChkBox.Checked;
                if (_sending)
                {
                    StartSendingAudio();
                }
                await SendData(receiveChkBox.Checked, sendChkBox.Checked, null);
            }
        }

        private void PlayAudio()
        {
            while (_inStream.Length < 20000)
                Thread.Sleep(100);

            _inStream.Position = 0;
            using (WaveStream blockAlignedStream = new BlockAlignReductionStream(
                WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(_inStream))))
            {
                using (WaveOut waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback()))
                {
                    waveOut.Init(blockAlignedStream);
                    waveOut.Play();
                    //outputTxtBox.AppendText("starting playback\r\n");
                    while (waveOut.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                    //outputTxtBox.AppendText("stopping playback\r\n");
                }
            }
        }

        private async void StartSendingAudio()
        {
            if (_writer != null)
                return;
            var waveIn = new WasapiLoopbackCapture();
            using (_writer = new LameMP3FileWriter(_outStream, waveIn.WaveFormat, 128))
            {
                waveIn.DataAvailable += OnDataAvailable;
                waveIn.StartRecording();

                while (_progState == ProgState.Connected && _sending)
                {
                    while (_outStream.Length < 4196)
                        await Task.Delay(100);

                    _outStream.Position = 0;
                    byte[] mp3Data = new byte[32768];
                    int bytesRead = await _outStream.ReadAsync(mp3Data, 0, mp3Data.Length);
                    _outStream.SetLength(0);
                    _outStream.Position = 0;

                    if (bytesRead != 0)
                        await SendData(receiveChkBox.Checked, sendChkBox.Checked,
                            mp3Data.Take(bytesRead).ToArray());
                }
            }
            waveIn.StopRecording();
            waveIn.DataAvailable -= OnDataAvailable;
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            _writer.Write(e.Buffer, 0, e.BytesRecorded);
        }

        private async Task ReceiveDataAsync()
        {
            byte[] buffer = new byte[65536];
            int bytesRead = 1;
            int totalLen = 0;
            while (bytesRead > 0)
            {
                bytesRead = await _sslClient.ReadAsync(buffer,
                    totalLen, buffer.Length - totalLen);
                totalLen += bytesRead;

                if (Encoding.UTF8.GetString(buffer.Take(totalLen).ToArray()).Contains("<EOF>"))
                    break;
            }

            if (bytesRead < 0)
            {
                outputTxtBox.AppendText("Socket closed?\r\n");
                _sslClient.Dispose();
                _progState = ProgState.NotConnected;
                return;
            }

            outputTxtBox.AppendText("Received data\r\n");

            var resp = MessagePackSerializer.Deserialize<AudioPack>(buffer.Take(totalLen).ToArray());
            willReceiveChkBox.Checked = resp.WillReceive;
            willSendChkBox.Checked = resp.WillSend;

            if (resp.Mp3Data != null)
            {
                var pos = _inStream.Position;
                _inStream.Position = _inStream.Length;
                await _inStream.WriteAsync(resp.Mp3Data, 0, resp.Mp3Data.Length);
                _inStream.Position = pos;
            }
        }

        private async Task SendData(bool willReceive, bool willSend, byte[] mp3Data)
        {
            if (_sslClient == null)
                return;

            byte[] buffer = MessagePackSerializer.Serialize(new AudioPack()
            {
                WillReceive = willReceive,
                WillSend = willSend,
                Mp3Data = mp3Data
            });
            await _sslClient.WriteAsync(buffer, 0, buffer.Length);
            if (mp3Data != null)
                outputTxtBox.AppendText(String.Format("wrote {0} bytes\r\n", buffer.Length));
            //await Task.Delay(5); // slow everything down
        }
    }

    [MessagePackObject]
    public class AudioPack
    {
        [Key(0)]
        public bool WillReceive { get; set; }

        [Key(1)]
        public bool WillSend { get; set; }

        [Key(2)]
        public byte[] Mp3Data { get; set; }

        [Key(3)]
        public string EOF = "<EOF>";
    }
}
