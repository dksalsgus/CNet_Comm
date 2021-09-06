using HPLC.Net.TCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace XGTTest.Cls
{
    class XGTClient : AsyncTcpClientBase
    {


        const char STX = (char)0x02;
        const char ETX = (char)0x03; //End Text [응답용Asc]
        const char EOT = (char)0x04; //End of Text[요구용 Asc]
        const char ENQ = (char)0x05; //Enquire[프레임시작코드]
        const char ACK = (char)0x06; //Acknowledge[응답 시작]
        const char NAK = (char)0x15; //Not Acknoledge[에러응답시작]

        public XGTClient()
        {

        }

        protected override async Task ReceiveAsyncInternal(Socket socket)
        {
            int offset = 0, size = 0;
            byte[] array = new byte[1024];
            var list = new List<byte>();

            while (true)
            {
                int count = await socket.ReceiveTaskAsync(array, 0, array.Length);
                if (count == 0)
                    break;

                for (int i = 0; i < count; i++)
                    list.Add(array[i]);

                //if (list.Count < MIN_PACKET_LENGTH)
                //    continue;

                while (true)
                {
                    //var startIndex = list.IndexOf((byte)ACK);
                    var startIndex = list.IndexOf((byte)ACK) >= 0 ? list.IndexOf((byte)ACK) : list.IndexOf((byte)NAK);
                    if (startIndex < 0)
                    {
                        list.Clear();
                        break;
                    }
                    else
                    {
                        var eIndex = list.IndexOf((byte)ETX);
                        if (eIndex < 0)
                            break;
                        else
                        {
                            var length = eIndex - startIndex + 1;
                            var buffer = new byte[length];
                            list.CopyTo(startIndex, buffer, 0, length);
                            list.RemoveRange(0, length + startIndex);
                            OnDataReceive(length, buffer);
                        }
                    }
                }
            }
        }

        private void OnDataReceive(int size, byte[] buffer)
        {
            try
            {
                //WriteLine(size, buffer);

                if (buffer[0] == 0x15)
                    Console.WriteLine("Nak");
                else if (buffer[0] == 0x06)
                    Console.WriteLine("Ack");

                var str = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
                Console.WriteLine(str);

                RecvMsg(str);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"OnDataReceive Error : {ex}");
            }
        }

        public static void WriteLine(int size, byte[] bytes)
        {
            if (bytes != null)
            {
                var sb = new StringBuilder();
                for (int i = 0; i < size; i++)
                {
                    sb.AppendFormat("{0:X2} ", bytes[i]);
                }
                //foreach (var item in bytes)
                //{
                //    sb.AppendFormat("{0:X2} ", item);
                //}
                Console.WriteLine(sb.ToString());
            }
        }
    }
}
