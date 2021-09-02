using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HPLC.Net.TCP
{
    public class AsyncTcpClientBase
    {
        public const int MAX_RESPONSE_TIME = 3000; //2000;
        private const int MAX_EXE_TIME = 3100;
        //private readonly Object sendSignal = new Object();
        private readonly AutoResetEvent _are = new AutoResetEvent(false);

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private volatile byte _WaitCommandID = 0;

        private string _NetId;
        private DateTime lastRxTime;
        private DateTime connectTime;

        public string NetId { get { return _NetId; } set { _NetId = value; } }
        public DateTime ConnectTime { get { return connectTime; } }
        public DateTime LastRxTime { get { return lastRxTime; } }
        public bool IsEmptyKey { get { return string.IsNullOrEmpty(_NetId); } }

        private Socket socket;
        private IPAddress _serverAddress;
        private int _serverPort;

        public string Host { get { return (_serverAddress == null) ? string.Empty : _serverAddress.ToString(); } }
        public int Port { get { return _serverPort; } } //set { port = value; } }

        public bool Connected { get { return (this.socket == null) ? false : this.socket.Connected; } }

        private readonly byte[] _receiveBuffer;

        public delegate void MessageReceiveEventHandler(AsyncTcpClientBase client, string data);

        public event MessageReceiveEventHandler ReceiveMessage;


        protected AsyncTcpClientBase() : this(string.Empty) { }



        protected AsyncTcpClientBase(string key)
        {
            this.lastRxTime = DateTime.Now; //DateTime.MinValue;
            this.connectTime = DateTime.MinValue;
            this.socket = null;
            this._serverAddress = null;
            this._receiveBuffer = new byte[1024];
        }

        protected virtual void RecvMsg(string data)
        {
            //if (_commandMessage != null && message.Command.ToUpper() == _commandMessage.Command)
            //{
            //    _responseMessage = message;
                _are.Set();
            //}
            this.lastRxTime = DateTime.Now;

            if (ReceiveMessage != null)
                ReceiveMessage(this, data);
        }

        protected void StartReceive()
        {
            //TODO: [검토] 일정 시간 소켓수신이 안될 경우 타임아웃 적용되는가?
            Task task = ReceiveAsyncInternal(this.socket);
            task.ContinueWith((prevTask) =>
            {
                if (prevTask.IsFaulted)
                {
                    foreach (var ex in prevTask.Exception.Flatten().InnerExceptions)
                    {
                        if (ex is SocketException)
                        {
                            LogError("Receive error", (SocketException)ex);
                        }
                        else if (ex is ObjectDisposedException)
                        {
                            LogError("Receive error", ex);
                        }
                        else
                            LogError("Receive error", ex);
                    }
                }
                else
                {
                    Log("Receive End Status={0}, IsFaulted={1}, IsCanceled={2}", prevTask.Status, prevTask.IsFaulted, prevTask.IsCanceled);
                }
                //if (prevTask.IsCanceled) { }
                //if (prevTask.IsCompleted)
                //if (prevTask.Status == TaskStatus.RanToCompletion) { }
                this.Disconnect();
                //this.OnDisconnected();
            }); //, TaskScheduler.FromCurrentSynchronizationContext());
            //DebugEx.WriteLine("Task ReceiveAsyncInternal Started.");
        }

        protected virtual void Log(string message) { }
        protected void Log(string format, params object[] args)
        {
            Log(string.Format(format, args));
        }

        protected virtual void LogError(string message) { }
        protected void LogError(string message, Exception ex)
        {
            LogError("Receive error {0}", ex.Message);
        }
        protected void LogError(string message, SocketException soe)
        {
            LogError("Receive error ErrorCode({0}) {1}: {2}", soe.ErrorCode, soe.Message, soe.StackTrace);
        }
        protected void LogError(string format, params object[] args)
        {
            LogError(string.Format(format, args));
        }

        //protected virtual Task WritePacketLog(string message) { return Task.FromResult(0); }
        //protected Task WritePacketLog(string format, params object[] args)
        //{
        //    return WritePacketLog(string.Format(format, args));
        //}

        protected virtual async Task ReceiveAsyncInternal(Socket socket)
        {
            await Task.FromResult(0);
        }

        public async Task<bool> SendAsync(string message)
        {
            try
            {
                await _semaphore.WaitAsync(MAX_EXE_TIME);

                var buffer = Encoding.ASCII.GetBytes(message);
                //buffer = Encoding.ASCII.GetBytes(message);
                var sent = await socket.SendTaskAsync(buffer, 0, buffer.Length);
                return sent == buffer.Length;
            }
            catch (Exception)
            {
                //Console.WriteLine("ReadAsync Error {0}", ex.Message);
                throw;
            }
            finally
            {
                _semaphore.Release();
                await Task.Delay(1);
            }
        }


        public async Task<bool> SendAsync(byte[] buffer)
        {
            try
            {
                await _semaphore.WaitAsync(MAX_EXE_TIME);

                var sent = await socket.SendTaskAsync(buffer, 0, buffer.Length);

                return sent == buffer.Length;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ReadAsync Error {0}", ex.Message);
                return false;
            }
            finally
            {
                _semaphore.Release();
                await Task.Delay(1);
            }
        }

        protected async Task<TxResult> SendWaitAsync(byte waitPid, string message)
        {
            //bool wait = await semaphoreSlim.WaitAsync(20 * 1000); //기존 요청중이면 20초 대기
            try
            {
                await _semaphore.WaitAsync(MAX_EXE_TIME);

                var buffer = Encoding.ASCII.GetBytes(message);
                _WaitCommandID = waitPid;

                //WriteLog("SendWait 0x{0:X2} hex={1}", txPid, buffer.Array.ToHexString());
                if (buffer.Length == await this.socket.SendTaskAsync(buffer, 0, buffer.Length))
                {
                    if (await _are.WaitOneAsync(MAX_RESPONSE_TIME))
                        return TxResult.Success;
                    else
                        return TxResult.ResponseTimeout;
                }
                return TxResult.ExceptionRaised;
            }
            catch(Exception ex)
            {
                Disconnect();
                Console.WriteLine(ex);
                return TxResult.ExceptionRaised;
            }
            finally
            {
                _WaitCommandID = 0;
                _semaphore.Release();
                await Task.Delay(1);
            }
        }

        protected async Task<string> ReadAsync()
        {
            var read = await socket.ReceiveTaskAsync(_receiveBuffer, 0, _receiveBuffer.Length);
            return Encoding.UTF8.GetString(_receiveBuffer, 0, read);
        }

        public Task<bool> ConnectAsync(string ipString, int port)
        {
            if (IPAddress.TryParse(ipString, out IPAddress address))
            {
                _serverAddress = address;
                _serverPort = port;
                return  ConnectAsync();
            }
            throw new ArgumentException("Invalid Ipaddress");
        }

        //public async Task<bool> ConnectAsync()
        //{
        //    this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) { SendTimeout = MAX_RESPONSE_TIME, ReceiveTimeout = MAX_RESPONSE_TIME };
        //    //var task = this.socket.ConnectAsync(_serverAddress, _serverPort);
        //    var task = this.socket.ConnectTaskAsync(_serverAddress, _serverPort);
        //    var tDelay = Task.Delay(MAX_RESPONSE_TIME);
        //    var tick = Environment.TickCount;
        //    var completed = await Task.WhenAny(task, tDelay);
        //    if (completed == task)
        //    {
        //        if (this.socket.Connected)
        //        {
        //            Console.WriteLine("connected {0}", Environment.TickCount - tick);
        //            this.connectTime = DateTime.Now;
        //            StartReceive();
        //            return true;
        //        }
        //        else
        //        {
        //            Console.WriteLine("not connected {0}", Environment.TickCount - tick);
        //            this.socket.Close();
        //            return false;
        //        }
        //    }
        //    else
        //        throw new TimeoutException(string.Format("Connection to server ({0}:{1}) timed out", _serverAddress, _serverPort));
        //    //throw new SocketException(10060); //timeout
        //}

        public async Task<bool> ConnectAsync()
        {
            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) { SendTimeout = MAX_RESPONSE_TIME, ReceiveTimeout = MAX_RESPONSE_TIME };
            var task = this.socket.ConnectTaskAsync(_serverAddress, Port);
            //var task = this.socket.ConnectAsync(_serverAddress, _serverPort);
            //var task = this.socket.Connect(new IPEndPoint(IPAddress.Any,_serverPort));
            if (await Task.WhenAny(task, Task.Delay(MAX_RESPONSE_TIME)) == task)
            {
                if (this.socket.Connected)
                {
                    this.connectTime = DateTime.Now;
                    //OnConnected();
                    StartReceive();
                    return true;
                }
                else // localhost 에서 연결 실패 시간이 짧다
                {
                    this.socket.Close();
                    return false;
                }
            }
            else
                throw new TimeoutException(string.Format("Connection to server ({0}:{1}) timed out", _serverAddress, _serverPort));
            //throw new SocketException(10060); //timeout
        }

        //public async Task<bool> ConnectAsync2()
        //{
        //    this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) { SendTimeout = MAX_RESPONSE_TIME, ReceiveTimeout = MAX_RESPONSE_TIME };
        //    //var task = this.socket.ConnectAsync(_serverAddress, _serverPort);
        //    var task = this.socket.ConnectTaskAsync(_serverAddress, _serverPort);
        //    if (await Task.WhenAny(task, Task.Delay(MAX_RESPONSE_TIME - 500)) == task)
        //    {
        //        if (this.socket.Connected)
        //        {
        //            this.connectTime = DateTime.Now;
        //            OnConnected();
        //            return true;
        //        }
        //        else // localhost 에서 연결 실패 시간이 짧다
        //        {
        //            this.socket.Close();
        //            return false;
        //        }
        //    }
        //    else
        //        throw new TimeoutException(string.Format("Connection to server ({0}:{1}) timed out", _serverAddress, _serverPort));
        //    //throw new SocketException(10060); //timeout
        //}

        protected void Init(string ipString, int port)
        {
            if (IPAddress.TryParse(ipString, out IPAddress address))
                _serverAddress = address;
            else
                throw new ArgumentException("Invalid Ipaddress");
            _serverPort = port;
        }

        //public bool Connect(string ipString, int port)
        //{
        //    if (IPAddress.TryParse(ipString, out IPAddress address))
        //        _serverAddress = address;
        //    else
        //        throw new ArgumentException("Invalid Ipaddress");
        //    _serverPort = port;
        //    Task<bool> task = Task.Run<bool>(async () => await ConnectAsync());
        //    return task.Result;
        //}

        //[Obsolete]
        //public virtual bool Connect()
        //{
        //    //Task.Run(() => { Console.WriteLine(); });
        //    //Task<bool> task = Task.Run<bool>(async () => await ConnectAsync());
        //    //return task.Result;
        //    throw new NotSupportedException();
        //}

        public bool Disconnect()
        {
            if (this.socket == null)
                return true;
            else
            {
                try
                {
                    if (this.socket.Connected)
                        this.socket.Shutdown(SocketShutdown.Both);

                    this.socket.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        #region Events

        //public event Func<object, EventArgs, Task> Shutdown;
        //public async Task OnShutdown()
        //{
        //    Func<object, EventArgs, Task> handler = Shutdown;
        //    if (handler == null)
        //    {
        //        return;
        //    }

        //    Delegate[] invocationList = handler.GetInvocationList();
        //    Task[] handlerTasks = new Task[invocationList.Length];

        //    for (int i = 0; i < invocationList.Length; i++)
        //    {
        //        handlerTasks[i] = ((Func<object, EventArgs, Task>)invocationList[i])(this, EventArgs.Empty);
        //    }

        //    await Task.WhenAll(handlerTasks);
        //}

        //protected virtual async Task OnCommandReceivedAsync(CommandEventArgs e)
        //{
        //    if (this.WaitCommandID != 0 && e.Cmd == this.WaitCommandID)
        //    {
        //        //DebugEx.WriteLine("Before Set 0x{0:X2} {0}", this.WaitCommandID);
        //        are.Set();
        //        //DebugEx.WriteLine("After Set 0x{0:X2} {0}", this.WaitCommandID);
        //    }

        //    this.lastRxTime = DateTime.Now;

        //    CommandReceivedEventHandler handler = CommandReceived;
        //    //if (handler != null)
        //    //{
        //    //    Delegate[] invocationList = handler.GetInvocationList();
        //    //    Parallel.ForEach<Delegate>(invocationList, (hndler) => { ((Command
        //    dEventHandler)hndler)(this, e); });
        //    //}
        //    if (handler != null)
        //    {
        //        var invocationList = handler.GetInvocationList();
        //        var handlerTasks = new Task[invocationList.Length];

        //        for (int i = 0; i < handlerTasks.Length; i++)
        //        {
        //            //DebugEx.WriteLine("OnCommandReceivedAsync {0}/{1}", i, handlerTasks.Length);
        //            //Action<object, CommandEventArgs> action = (CommandReceivedEventHandler)invocationList[i];
        //            var k = i;
        //            Action<object, CommandEventArgs> action = new Action<object, CommandEventArgs>((CommandReceivedEventHandler)invocationList[k]);
        //            //Action a = new Action(((CommandReceivedEventHandler)invocationList[k])(this, e));
        //            //handlerTasks[k] = Task.Factory.StartNew(action);
        //            handlerTasks[k] = Task.Factory.StartNew(() =>
        //            {
        //                action(this, e);
        //                //((CommandReceivedEventHandler)invocationList[k])(this, e);
        //            });
        //            //handlerTasks[i] = ((Func<object, EventArgs, Task>)invocationList[i])(this, e);
        //        }
        //        await Task.WhenAll(handlerTasks);
        //    }
        //}

        private CommandEventArgs _LastRxCommand;
        protected CommandEventArgs LastRxCommand { get { return _LastRxCommand; } }

        public event CommandReceivedEventHandler CommandReceived;
        protected virtual void OnCommandReceived(CommandEventArgs e)
        {
            if (this._WaitCommandID != 0 && e.Cmd == this._WaitCommandID)
            {
                _LastRxCommand = e;
                _are.Set();
            }
            this.lastRxTime = DateTime.Now;
            if (CommandReceived != null)
                CommandReceived(this, e);
        }
        public event EventHandler ConnectedEvent;
        protected virtual void OnConnected()
        {
            if (ConnectedEvent != null)
                ConnectedEvent(this, EventArgs.Empty);
        }
        public event EventHandler Disconnected;
        protected virtual void OnDisconnected()
        {
            if (Disconnected != null)
                Disconnected(this, EventArgs.Empty);
        }
        #endregion
    }
}
