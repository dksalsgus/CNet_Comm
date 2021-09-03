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
using static XGTTest.Cls.XGT_Response;

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

        private void InitCotrol()
        {
            btnConnect.Click += BtnConnect_Click;
            btnSend.Click += BtnSend_Click;
            btnDisconnect.Click += BtnDisconnect_Click;
            btnListen.Click += BtnListen_Click;

            _ipString = txtHost.Text.Trim();
            _port = int.Parse(txtPort.Text.Trim());

        }

        private async void BtnListen_Click(object sender, EventArgs e)
        {
            await Task.Run(() => LinsteningTCP(_ipString, _port));
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
            ipString = "127.0.0.1";
            port = 4001;
            TcpListener listener = new TcpListener(new IPEndPoint(IPAddress.Parse(ipString), port));
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
                    data += Encoding.ASCII.GetString(bytes, 0, length);
                }
                Console.WriteLine($"받은 데이터 : {data}");


                ResponseAck res = new ResponseAck();

                res.NationalNo = "01";
                res.Cmd = "R";
                res.CmdType = "SS";
                res.Block = "01";
                res.DataName = "0000";
                res.DataSize = res.DataName.Length.ToString("X02"); // txtDataSize1.Text.Trim();

                var bcc = string.Empty;
                System.Reflection.PropertyInfo[] propertyInfos = res.GetType().GetProperties();
                var sendString = string.Empty;
                foreach (var item in propertyInfos)
                {
                    if (item.Name == "Header" || item.Name == "Tail")
                        continue;

                    sendString += string.Concat(item.GetValue(res));
                }
                var body = Encoding.ASCII.GetBytes(sendString);

                List<byte> byteList = new List<byte>();
                byteList.Add((byte)ENQ);
                byteList.AddRange(body);
                byteList.Add((byte)EOT);
                var str = Encoding.ASCII.GetString(byteList.ToArray());
                Console.WriteLine($"보내는 데이터  : {str}");
                stream.Write(byteList.ToArray(), 0, byteList.ToArray().Length);

                stream.Close();
                //ReadData(bytes);
            }

        }

        void ReadData(string data)
        {
            try
            {

                //var receiveMsg = data;
                //var res = new XGT_Response();
                //byte[] readBytes = new byte[data.Length];


                //if (receiveMsg[0] == ACK)
                //{
                //    // Ack
                //    var ack = res as ResponseAck;
                //    ack.Header = Convert.ToByte(receiveMsg.Substring(0, 1));
                //    ack.NationalNo = receiveMsg.Substring(1, 2);
                //    ack.Cmd = receiveMsg.Substring(3, 1);
                //    ack.CmdType = receiveMsg.Substring(4, 2);
                //    ack.Block = receiveMsg.Substring(6, 2);
                //    ack.DataSize = receiveMsg.Substring(8, 2);
                //    ack.DataName = receiveMsg.Substring(10, 4);
                //    ack.Tail = Convert.ToByte(receiveMsg.Substring(14, 1));
                //    readBytes = ConvertStringToBytes(data, ack);

                //}
                //else if (receiveMsg[0] == NAK)
                //{
                //    // Nak
                //    var nak = res as ResponseNak;
                //    nak.Header = Convert.ToByte(receiveMsg.Substring(0, 1));
                //    nak.NationalNo = receiveMsg.Substring(1, 2);
                //    nak.Cmd = receiveMsg.Substring(3, 1);
                //    nak.CmdType = receiveMsg.Substring(4, 2);
                //    nak.ErrorCode = receiveMsg.Substring(6, 4);
                //    nak.Tail = Convert.ToByte(receiveMsg.Substring(10, 1));
                //    readBytes = ConvertStringToBytes(data, nak);
                //}

                //var str = Encoding.ASCII.GetString(readBytes);
                if (data[0] == NAK)
                {
                    if (Enum.TryParse<EnErrorCode>(data.Substring(6, 4), out EnErrorCode ret))
                    {
                        switch ((int)ret)
                        {
                            case (int)EnErrorCode.블록수초과에러:
                                data = ret.ToString();
                                break;

                            case (int)EnErrorCode.변수길이에러:
                                data = ret.ToString();
                                break;
                            case (int)EnErrorCode.데이터타입에러:
                                data = ret.ToString();
                                break;
                            case (int)EnErrorCode.데이터에러:
                                data = ret.ToString();
                                break;
                            case (int)EnErrorCode.모니터실행에러1:
                                data = ret.ToString();
                                break;
                            case (int)EnErrorCode.모니터실행에러2:
                                data = ret.ToString();
                                break;
                            case (int)EnErrorCode.모니터실행에러3:
                                data = ret.ToString();
                                break;

                            case (int)EnErrorCode.데이터크기에러:
                                data = ret.ToString();
                                break;
                            case (int)EnErrorCode.여유프레임에러:
                                data = ret.ToString();
                                break;
                            case (int)EnErrorCode.데이터값에러:
                                data = ret.ToString();
                                break;

                            case (int)EnErrorCode.변수요구영역초과에러:
                                data = ret.ToString();
                                break;
                            default:
                                break;
                        }
                    }
                }

                // Invoke
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(delegate ()
                    {
                        rbReadMsg.Text = data;
                    }));
                }
                else
                {
                    rbReadMsg.Text = data;
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Read Data Error : {ex}");
            }
        }

        private byte[] ConvertStringToBytes(string data, XGT_Response res)
        {
            System.Reflection.PropertyInfo[] propertyInfos = res.GetType().GetProperties();

            byte[] readBytes = new byte[data.Length];
            int offset = 0;
            foreach (var item in propertyInfos)
            {
                offset = ByteCopy(item.GetValue(res).ToString(), offset, readBytes);
            }
            return readBytes;
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
