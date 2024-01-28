using EthDiagnosticTool.ProductManager.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using NetworkFrames.Packets;
using NetUds = NetworkFrames.Packets.UDS;
using static EthDiagnosticTool.Global.Helper.ConfigHelper;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EthDiagnosticTool.UDS.DoIP
{
    using System.Threading;

    internal class Server
    {
        public enum ConnectStatus
        {
            /// <summary>
            /// 未连接
            /// </summary>
            NotConnected,

            /// <summary>
            /// 正在连接
            /// </summary>
            Connecting,

            /// <summary>
            /// 已连接
            /// </summary>
            Connected,

            /// <summary>
            /// 正在关闭
            /// </summary>
            Closing,
        }

        public delegate void AddResult(string message);

        public delegate void SetStatus(ConnectStatus connectStatus);

        internal Global.LogHelper.AddLog? AddLogMethod;

        internal AddResult? AddResultMethod;

        internal SetStatus? SetStatusMethod;

        private UdsTransportLayerParams_DoIP udsTransportLayerParams_DoIP;

        public UdsTransportLayerParams_DoIP UdsTransportLayerParams_DoIP
        {
            get
            {
                return udsTransportLayerParams_DoIP;
            }
            set
            {
                udsTransportLayerParams_DoIP = value;
            }
        }

        public IPEndPoint TesterEndPoint
        {
            get
            {
                return new IPEndPoint(udsTransportLayerParams_DoIP.tcpTransportLayerParams.ipNetworkLayerParams.localIP, udsTransportLayerParams_DoIP.tcpTransportLayerParams.localPort);
            }
        }

        public IPEndPoint EcuEndPoint
        {
            get
            {
                return new IPEndPoint(udsTransportLayerParams_DoIP.tcpTransportLayerParams.ipNetworkLayerParams.remoteIP, udsTransportLayerParams_DoIP.tcpTransportLayerParams.remotePort);
            }
        }

        private TcpListener? TesterTcpServer;

        private TcpClient? testerTcpClient;

        private TcpClient? TesterTcpClient
        {
            get
            {
                return testerTcpClient;
            }
            set
            {
                testerTcpClient = value;
            }
        }

        private TcpClient? ecuTcpClient;

        private TcpClient? EcuTcpClient
        {
            get
            {
                return ecuTcpClient;
            }
            set
            {
                ecuTcpClient = value;
            }
        }

        private Thread? connectServerThread;

        private Thread? listenAcceptThread;

        private Thread? sendThread;

        private Thread? receiveThread;

        private ConnectStatus status;

        private ConnectStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                SetStatusMethod?.Invoke(status);
            }
        }

        

        public Server(UdsTransportLayerParams_DoIP doipParams)
        {
            udsTransportLayerParams_DoIP = doipParams;
        }


        /// <summary>
        /// 启动服务
        /// </summary>
        public void Start()
        {
            switch (udsTransportLayerParams_DoIP.tcpTransportLayerParams.connectMode)
            {
                case TcpTransportLayerParams.TcpType.Server: 
                    {
                        AddLogMethod?.Invoke("作为服务端连接。");
                        Status = ConnectStatus.Connecting;
                        listenAcceptThread = new Thread(ListenAcceptThread)
                        {
                            Name = "TCP 监听",
                        };
                        listenAcceptThread.Start();
                        break;   
                    }
                case TcpTransportLayerParams.TcpType.Client:
                    {
                        AddLogMethod?.Invoke("作为客户端连接。");
                        Status = ConnectStatus.Connecting;
                        connectServerThread = new Thread(ConnectServerThread)
                        {
                            Name = "TCP 连接",
                        };
                        connectServerThread.Start();
                        break;
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        private void ConnectServerThread()
        {
            AddLogMethod?.Invoke("开始连接。");
            while (Status == ConnectStatus.Connecting || Status == ConnectStatus.Connected)
            {
                try
                {
                    if (Status == ConnectStatus.Connecting)
                    {
                        ConnectServer();
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        continue;
                    }
                }
                catch (SocketException e)
                {
                    AddLogMethod?.Invoke($"连接错误！{e.Message}");
                    break;
                }
            }
            Status = ConnectStatus.NotConnected; 
            AddLogMethod?.Invoke("已停止连接。");
        }

        private void ConnectServer()
        {
            var testerIP = udsTransportLayerParams_DoIP.tcpTransportLayerParams.ipNetworkLayerParams.localIP;
            var testerPort = udsTransportLayerParams_DoIP.tcpTransportLayerParams.localPort;
            while (true)
            {
                try
                {
                    EcuTcpClient = new TcpClient(new IPEndPoint(testerIP, testerPort));
                    EcuTcpClient.Connect(EcuEndPoint);
                    break;
                }
                catch (SocketException)
                {
                    testerPort += 1;
                }
            }
            Status = ConnectStatus.Connected;
            AddLogMethod?.Invoke($"已连接！本地：{EcuTcpClient.Client.LocalEndPoint}；远端：{EcuTcpClient.Client.RemoteEndPoint}");
        }

        private void ListenAcceptThread()
        {
            TesterTcpServer = new TcpListener(TesterEndPoint);
            TesterTcpServer.Start();
            AddLogMethod?.Invoke("已启动 TCP 服务器。");
            AddLogMethod?.Invoke("开始监听。");
            while (Status == ConnectStatus.Connecting || Status == ConnectStatus.Connected)
            {
                try
                {
                    if (Status == ConnectStatus.Connecting)
                    {
                        ListenAccept();
                    }
                    else
                    {
                        Thread.Sleep(2000);
                        continue;
                    }
                }
                catch (SocketException)
                {
                    break;
                }
            }
            TesterTcpServer?.Stop();
            AddLogMethod?.Invoke("已停止监听。");
        }

        private void ListenAccept()
        {
            if (TesterTcpServer == null) return;
            var tcpClient = TesterTcpServer.AcceptTcpClient();
            if (Status == ConnectStatus.Connecting)
            {
                EcuTcpClient = tcpClient;
                Status = ConnectStatus.Connected;
                AddLogMethod?.Invoke($"已连接：{EcuTcpClient.Client.RemoteEndPoint}");
            }
            else
            {
                tcpClient.Close();
            }
        }


        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            AddLogMethod?.Invoke("正在停止 DoIP 服务...");
            Status = ConnectStatus.Closing;
            try
            {
                EcuTcpClient?.Client?.Shutdown(SocketShutdown.Both);
                EcuTcpClient?.Client?.Close();
            }
            catch (ObjectDisposedException)
            {
                AddLogMethod?.Invoke($"Socket 已经被释放。", Global.LogHelper.LogLevel.Warning);
            }
            TesterTcpServer?.Stop();
            //EcuTcpClient?.Close();
            //EcuTcpClient?.Dispose();
            while (listenAcceptThread?.IsAlive ?? false) { }
            while (connectServerThread?.IsAlive ?? false) { }
            Status = ConnectStatus.NotConnected;
            AddLogMethod?.Invoke("已停止 DoIP 服务。");
        }


        public bool SendBytes(byte[] data)
        {
            int sentCount = 0;
            if (EcuTcpClient == null)  // 无效操作
            {
                return false;
            }
            if (Status == ConnectStatus.Connecting && !EcuTcpClient.Connected)
            {
                AddLogMethod?.Invoke($"发送失败：未建立连接！");
                Stop();
                return false;
            }


            AddLogMethod?.Invoke($"发送内容（{data.Length}字节）：{BitConverter.ToString(data)}");
            sentCount = EcuTcpClient.Client.Send(data);
            if (sentCount == data.Length)
            {
                AddLogMethod?.Invoke($"发送成功（已发送：{sentCount}字节）！");
                return true;
            }
            else
            {
                AddLogMethod?.Invoke($"发送失败（已发送：{sentCount}字节）！");
                return false;
            }
            //var n = new NetworkStream(EcuTcpClient.Client);
            //n.Write(data, 0, data.Length);
        }

        public bool SendUdsPacket(NetUds.UDSPacket udsPacket)
        {
            var diagnosticMessagePacket = new NetworkFrames.Packets.DoIP.DiagnosticMessagePacket()
            {
                SourceLogicalAddress = udsTransportLayerParams_DoIP.localLA,
                DestinationLogicalAddress = udsTransportLayerParams_DoIP.remoteLA,
            };
            diagnosticMessagePacket.Payload = udsPacket.Data;
            var doipPacket = new NetworkFrames.Packets.DoIP.DoIPPacket()
            {
                ProtocolVersion = udsTransportLayerParams_DoIP.version,
                PayloadType = NetworkFrames.Packets.DoIP.PayloadTypes.DiagnosticMessage,
            };
            doipPacket.Payload = diagnosticMessagePacket.Data;
            doipPacket.PayloadLength = (ushort)doipPacket.Payload.Length;
            return SendBytes(doipPacket.Data);
        }

        public Queue<byte[]> sendingData = new Queue<byte[]>();

        private void SendThread()
        {
            AddLogMethod?.Invoke("已启动发送线程。");
            while (Status == ConnectStatus.Connected)
            {
                if (sendingData.Count > 0)
                {
                    var data = sendingData.Dequeue();
                    SendBytes(data);
                }
            }
            AddLogMethod?.Invoke("已停止发送线程。");
        }

        public bool ReceiveBytes(out byte[] data)
        {
            data = Array.Empty<byte>();
            if (EcuTcpClient == null)  // 无效操作
            {
                return false;
            }
            if (!EcuTcpClient.Connected)
            {
                if (Status == ConnectStatus.Connected)
                {
                    AddLogMethod?.Invoke("连接中断。");
                    Stop();
                }
                return false;
            }


            try
            {
                List<byte> bufferList = new List<byte>();
                var n = new NetworkStream(EcuTcpClient.Client);
                int receiveCount = 0;
                if (!n.DataAvailable)
                {
                    data = Array.Empty<byte>();
                    return false;
                }
                while (n.DataAvailable)
                {
                    byte[] buffer = new byte[1500];
                    receiveCount += n.Read(buffer, 0, buffer.Length);
                    bufferList.AddRange(buffer);
                }
                data = new byte[receiveCount];
                Array.Copy(bufferList.ToArray(), data, receiveCount);
            }
            catch (System.IO.IOException)// catch (IOException)
            {
                return false;
            }
            
            AddLogMethod?.Invoke($"收到内容（{data.Length}字节）：{BitConverter.ToString(data)}");
            return true;
        }

        public bool ReceiveUdsPacket(out NetUds.UDSPacket udsPacket)
        {
            if (ReceiveBytes(out var data))
            {
                var doipPacket = new NetworkFrames.Packets.DoIP.DoIPPacket(data);
                if (doipPacket.IsValid && doipPacket.PayloadType == NetworkFrames.Packets.DoIP.PayloadTypes.DiagnosticMessage && doipPacket.IsChildPacketValid && doipPacket.ChildPacket != null)
                {
                    var diagnosticMessage = (NetworkFrames.Packets.DoIP.DiagnosticMessagePacket)doipPacket.ChildPacket;
                    if (diagnosticMessage != null && diagnosticMessage.IsChildPacketValid && diagnosticMessage.ChildPacket != null)
                    {
                        udsPacket = (NetUds.UDSPacket)diagnosticMessage.ChildPacket;
                        return true;
                    }
                }
            }
            udsPacket = new NetUds.UDSPacket();
            return false;
        }

        public Queue<byte[]> receivedData = new Queue<byte[]>();

        private void ReceiveThread()
        {
            AddLogMethod?.Invoke("已启动接收线程。");
            while (Status == ConnectStatus.Connected)
            {
                if (ReceiveBytes(out byte[] data))
                {
                    receivedData.Enqueue(data);
                }
            }
            AddLogMethod?.Invoke("已停止接收线程。");
        }

    }
}
