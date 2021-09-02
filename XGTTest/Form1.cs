using HPLC.Net.TCP;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XGTTest.Cls;
using static XGTTest.Cls.XGT_Enum;

namespace XGTTest
{
    public partial class Form1 : Form
    {
        private string _ipString;
        private int _port;
        private XGTClient _client;

        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen();
            _client = new XGTClient();
            _client.Disconnected += _client_Disconnected;
            _client.ReceiveMessage += HandleMessage;
        }

        private void _client_Disconnected(object sender, EventArgs e)
        {
            Console.WriteLine("Disconnected");
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitCotrol();
        }

        protected void HandleMessage(AsyncTcpClientBase client, string data)
        {
            if (client != null)
            {
                // 메세지
                ReadData(data);
            }
        }

        private  void InitCotrol()
        {
            btnConnect.Click += BtnConnect_Click;
            btnSend.Click += BtnSend_Click;
            btnDisconnect.Click += BtnDisconnect_Click;

            _ipString = txtHost.Text.Trim();
            _port = int.Parse(txtPort.Text.Trim());

            //await Task.Run(() => LinsteningTCP(_ipString, _port));
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (_client != null)
                {
                    bool ret = _client.Disconnect();
                    if (ret)
                    {
                        btnDisconnect.Enabled = false;
                        btnConnect.Enabled = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Disconnect Error : {ex}");
            }

        }

        private async Task LinsteningTCP(string ipString, int port)
        {
            TcpListener listener = new TcpListener(new IPEndPoint(IPAddress.Parse(_ipString), port));
            listener.Start();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                int length;
                string data = null;
                byte[] bytes = new byte[1024];
                while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    data += Encoding.Default.GetString(bytes, 0, length);
                }
                Console.WriteLine($"받은 데이터 : {data}");

                //ReadData(bytes);
            }

        }

        void ReadData(string data)
        {
            var receiveMsg = data;
            //var readMsg = data;

            XGT_Response res = new XGT_Response();

            res.Header = Convert.ToByte(receiveMsg.Substring(0, 1));
            res.NationalNo = receiveMsg.Substring(1, 4);
            res.Cmd = receiveMsg.Substring(5, 8);
            res.CmdType = receiveMsg.Substring(13, 4);
            res.ErrorCode = receiveMsg.Substring(17, 4);
            res.Tail = Convert.ToByte(receiveMsg.Substring(21, 1));


            System.Reflection.PropertyInfo[] propertyInfos = res.GetType().GetProperties();

            byte[] readBytes = new byte[data.Length];
            int offset = 0;
            foreach (var item in propertyInfos)
            {
                offset = ByteCopy(item.GetValue(res).ToString(), offset, readBytes);
            }

            // Invoke
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(delegate ()
                {
                    rbReadMsg.Text = Encoding.UTF8.GetString(readBytes);
                }));
            }
            else
            {
                rbReadMsg.Text = Encoding.UTF8.GetString(readBytes);
            }
        }

        private int ByteCopy(string value, int offset, byte[] dest)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(value);
            int size = buffer.Length;
            Array.Copy(buffer, 0, dest, offset, size);

            return offset + size;
        }

        public string StringToHex(string strData)
        {
            string resultHex = string.Empty;
            byte[] arr_byteStr = Encoding.Default.GetBytes(strData);

            foreach (byte byteStr in arr_byteStr)
                resultHex += string.Format("{0:X2}", byteStr);

            return resultHex;
        }

        const char STX = (char)0x02;
        const char ETX = (char)0x03; //End Text [응답용Asc]
        const char EOT = (char)0x04; //End of Text[요구용 Asc]
        const char ENQ = (char)0x05; //Enquire[프레임시작코드]
        const char ACK = (char)0x06; //Acknowledge[응답 시작]
        const char NAK = (char)0x15; //Not Acknoledge[에러응답시작]


        private void BtnSend_Click(object sender, EventArgs e)
        {
            rbSendMsg.Clear();
            XGT_Request req = new XGT_Request();
            //req.Header = (byte)ENQ; // 05
            //req.NationalNo = StringToHex(txtNationalNo.Text.Trim()); // 3031
            //req.Cmd = StringToHex(txtCmd.Text.Trim()); // 52(72)
            //req.CmdType = StringToHex(txtCmdType.Text.Trim()); // 5353
            //req.Block = StringToHex(txtBlock.Text.Trim()); // 3032
            //req.DataSize= StringToHex(txtDataSize1.Text.Trim()); // 3036
            //req.DataName = StringToHex(txtDataName1.Text.Trim()); // 254D57303230
            ////req.DataSize2 = StringToHex(txtDataSize2.Text.Trim()); // 3036
            ////req.DataName2 = StringToHex(txtDataName2.Text.Trim()); // 25505730303031
            //req.Tail = (byte)EOT; // 04

            req.NationalNo = txtNationalNo.Text.Trim();
            req.Cmd = txtCmd.Text.Trim();
            req.CmdType = txtCmdType.Text.Trim();
            req.Block = txtBlock.Text.Trim();
            req.DataName = txtDataName1.Text.Trim();
            req.DataSize = req.DataName.Length.ToString("X02"); // txtDataSize1.Text.Trim();

            var bcc = string.Empty;
            System.Reflection.PropertyInfo[] propertyInfos = req.GetType().GetProperties();
            var sendString = string.Empty;
            foreach (var item in propertyInfos)
            {
                if (item.Name == "Header" || item.Name == "Tail")
                    continue;

                sendString += string.Concat(item.GetValue(req));
            }
            var data = Encoding.ASCII.GetBytes(sendString);

            List<byte> byteList = new List<byte>();
            byteList.Add((byte)ENQ);
            byteList.AddRange(data);
            byteList.Add((byte)EOT);

                //var ret = await _client.SendAsync(byteList.ToArray());
            Task.Run(() => TimerTask(byteList));
        }

        private async Task TimerTask(List<byte> byteList)
        {
            while (true)
            {
                var ret = await _client.SendAsync(byteList.ToArray());
                await Task.Delay(1000);
            }
        }

        private async void BtnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            try
            {
                _ipString = txtHost.Text.Trim();//"192.168.1.204"; 
                _port = int.Parse(txtPort.Text.Trim());//4001;
                bool ret = await _client.ConnectAsync(_ipString, _port);
                if (ret)
                {
                    btnConnect.Enabled = false;
                    btnDisconnect.Enabled = true;
                    Console.WriteLine($"{_client.Host}[{_client.Port}] Connected {ret}");
                }
                else
                    Console.WriteLine($"{_client.Host}[{_client.Port}] Connected {ret}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Console.WriteLine($"Connect Error : {ex}");
            }
        }
    }
}
