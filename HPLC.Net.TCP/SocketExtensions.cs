using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HPLC.Net.TCP
{
    public static class SocketExtensions
    {
        public static Task<Socket> AcceptTaskAsync(this Socket socket)
        {
            return Task.Factory.FromAsync<Socket>(socket.BeginAccept, socket.EndAccept, null);
        }

        public static Task<int> ReceiveTaskAsync(this Socket socket, byte[] buffer, int offset, int count)
        {
            return Task.Factory.FromAsync<int>(socket.BeginReceive(buffer, offset, count, SocketFlags.None, null, socket), socket.EndReceive);
        }

        public static Task<int> SendTaskAsync(this Socket socket, byte[] buffer, int offset, int count)
        {
            return Task.Factory.FromAsync<int>(socket.BeginSend(buffer, offset, count, SocketFlags.None, null, socket), socket.EndSend);
        }

        //public static Task<Socket> ConnectTaskAsync(this Socket socket, IPEndPoint serverEP)
        //{
        //    return Task.Factory.FromAsync<Socket>(socket.BeginConnect(serverEP, null, null), null);
        //}

        public static Task ConnectTaskAsync(this Socket socket, EndPoint serverEP)
        {
            return Task.Factory.FromAsync(socket.BeginConnect, socket.EndConnect, serverEP, null);
        }

        public static Task ConnectTaskAsync(this Socket socket, IPAddress address, int port)
        {
            return Task.Factory.FromAsync(socket.BeginConnect, socket.EndConnect, new IPEndPoint(address, port), null);
        }
    }
}
