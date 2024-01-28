using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EthDiagnosticTool.Global;
using NetUds = NetworkFrames.Packets.UDS;

namespace EthDiagnosticTool.UDS
{

    using System.Threading;
    public static class Server
    {
        public enum RunStatus
        {
            NotStart,
            Running,
        }

        private static RunStatus status;

        public static RunStatus Status
        {
            get
            {
                return status;
            }
        }

        public delegate bool SendUdsPacket(NetUds.UDSPacket udsPacket);

        public delegate bool ReceiveUdsPacket(out NetUds.UDSPacket udsPacket);

        public static ProductManager.Config.UdsDiagnosticLayerParams udsDiagnosticLayerParams = new ProductManager.Config.UdsDiagnosticLayerParams();

        private static Queue<NetUds.UDSPacket> udsPacketSendingQueue = new Queue<NetUds.UDSPacket>();

        private static Queue<NetUds.UDSPacket> udsPacketReceivedQueue = new Queue<NetUds.UDSPacket>();

        private static Thread? udsPacketSendThread;

        private static Thread? udsPacketReceiveThread;

        private static object udsPacketSendLock = new object();

        private static object udsPacketReceiveLock = new object();

        public static SendUdsPacket? SendUdsPacketMethod { get; set; }

        public static ReceiveUdsPacket? ReceiveUdsPacketMethod { get; set; }

        public static LogHelper.AddLog? AddLogMethod { get; set; }



        /// <summary>
        /// 从接收队列中读取
        /// </summary>
        /// <param name="udsPacket"></param>
        /// <returns></returns>
        public static bool GetReceivedUdsPacket(out NetUds.UDSPacket udsPacket)
        {
            udsPacket = new NetUds.UDSPacket();
            if (Monitor.TryEnter(udsPacketReceiveLock, 20))
            {
                if (udsPacketReceivedQueue.Count > 0)
                {
                    udsPacket = udsPacketReceivedQueue.Dequeue();
                    Monitor.Exit(udsPacketReceiveLock);
                    return true;
                }
                Monitor.Exit(udsPacketReceiveLock);
                return false;
            }
            else
            {
                return false;
            }
        }

        private static bool AddReceivedUdsPacket(NetUds.UDSPacket udsPacket)
        {
            if (Monitor.TryEnter(udsPacketReceiveLock, 20))
            {
                udsPacketReceivedQueue.Enqueue(udsPacket);
                Monitor.Exit(udsPacketReceiveLock);
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool GetSengingUdsPacket(out NetUds.UDSPacket udsPacket)
        {
            udsPacket = new NetUds.UDSPacket();
            if (Monitor.TryEnter(udsPacketSendLock, 20))
            {
                if (udsPacketSendingQueue.Count > 0)
                {
                    udsPacket = udsPacketSendingQueue.Dequeue();
                    Monitor.Exit(udsPacketSendLock);
                    return true;
                }
                Monitor.Exit(udsPacketSendLock);
                return false;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加到发送队列
        /// </summary>
        /// <param name="udsPacket"></param>
        /// <returns></returns>
        public static bool AddSendingUdsPacket(NetUds.UDSPacket udsPacket)
        {
            if (Monitor.TryEnter(udsPacketSendLock, 20))
            {
                udsPacketSendingQueue.Enqueue(udsPacket);
                Monitor.Exit(udsPacketSendLock);
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool udsPacketSendThreadFlag = false;

        /// <summary>
        /// 调用发送方法，把 UDS 包消息发送队列发送出去
        /// </summary>
        public static void UdsPacketSendThread()
        {
            udsPacketSendThreadFlag = true;
            AddLogMethod?.Invoke($"已启动 UDS 发送线程。");
            while (udsPacketSendThreadFlag)
            {
                if (SendUdsPacketMethod == null)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                if (GetSengingUdsPacket(out var udsPacket))  // CPU 高热函数
                {
                    if (!SendUdsPacketMethod(udsPacket))
                    {
                        AddLogMethod?.Invoke($"消息发送失败！{BitConverter.ToString(udsPacket.Data)}", LogHelper.LogLevel.Warning);
                    }
                }
                else
                {
                    Thread.Sleep(1);  // 没有任何消息时小憩以降低 CPU 负载。
                }
            }
            AddLogMethod?.Invoke($"已结束 UDS 发送线程。");
        }

        private static bool udsPacketReceiveThreadFlag = false;

        /// <summary>
        /// 调用收取方法，把收取的内容添加到 UDS 包消息队列
        /// </summary>
        public static void UdsPacketReceiveThread()
        {
            udsPacketReceiveThreadFlag = true;
            AddLogMethod?.Invoke($"已启动 UDS 接收线程。");
            while (udsPacketReceiveThreadFlag)
            {
                if (ReceiveUdsPacketMethod == null)
                {
                    Thread.Sleep(1000);
                    continue;
                }
                if (ReceiveUdsPacketMethod(out var udsPacket))  // CPU 高热函数
                {
                    if (!AddReceivedUdsPacket(udsPacket))
                    {
                        AddLogMethod?.Invoke($"添加到接受列表失败！{BitConverter.ToString(udsPacket.Data)}", LogHelper.LogLevel.Warning);
                    }
                }
                else
                {
                    Thread.Sleep(1);  // 没有任何消息时小憩以降低 CPU 负载。
                }
            }
            AddLogMethod?.Invoke($"已结束 UDS 接收线程。");
        }

        /// <summary>
        /// 启动
        /// </summary>
        public static void Start()
        {
            udsPacketReceiveThread = new Thread(UdsPacketReceiveThread)
            {
                Name = "UDS 接收线程",
            };
            udsPacketSendThread = new Thread(UdsPacketSendThread)
            {
                Name = "UDS 发送线程",
            };
            udsPacketReceiveThread.Start();
            udsPacketSendThread.Start();
            status = RunStatus.Running;
        }

        /// <summary>
        /// 停止
        /// </summary>
        public static void Stop()
        {
            udsPacketSendThreadFlag = false;
            udsPacketReceiveThreadFlag = false;
            while (udsPacketSendThread != null && udsPacketSendThread.IsAlive) { }
            while (udsPacketReceiveThread!= null && udsPacketReceiveThread.IsAlive) { }
            status = RunStatus.NotStart;
        }

        private static object udsSessionMonopolyLock = new object();

        public static bool singleUdsCommunicateFlag = false;

        public static bool SingleUdsCommunicate(byte[] sendingData, out byte[] receivedData, out string message)
        {
            NetUds.UDSPacket sendingUdsPacket = new NetUds.UDSPacket(sendingData);
            var b = SingleUdsCommunicate(sendingUdsPacket, out var receivedUdsPacket, out message);
            receivedData = receivedUdsPacket.Data;
            return b;
        }

        /// <summary>
        /// 单次 UDS 通信
        /// </summary>
        /// <param name="sendingUdsPacket"></param>
        /// <param name="receivedUdsPacket"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool SingleUdsCommunicate(NetUds.UDSPacket sendingUdsPacket, out NetUds.UDSPacket receivedUdsPacket, out string message)
        {
            if (status != RunStatus.Running)
            {
                message = "未启动 UDS 通信服务！";
                goto failed;
            }

            message = "";
            singleUdsCommunicateFlag = true;
            bool result = false;
            uint waitTime = udsDiagnosticLayerParams.p2Client;

            if (Monitor.TryEnter(udsSessionMonopolyLock, 2000))
            {
                udsPacketSendingQueue.Clear();
                udsPacketReceivedQueue.Clear();  // 清空接收列表以避免先前通信的遗留消息的影响

                // ------------------------------------------------------------------------------------------------------------------------
                // 发送请求
                result = AddSendingUdsPacket(sendingUdsPacket);
                if (!result)
                {
                    message = $"发送失败！{BitConverter.ToString(sendingUdsPacket.Data)}";
                    goto failed;
                }
                AddLogMethod?.Invoke($"完成发送 UDS 消息！{BitConverter.ToString(sendingUdsPacket.Data)}", LogHelper.LogLevel.Debug);
                // 至此，完成发送
                // ------------------------------------------------------------------------------------------------------------------------



                // ------------------------------------------------------------------------------------------------------------------------
                // 接收响应
                var startTime = DateTime.Now;
                while (singleUdsCommunicateFlag)
                {
                    var usedTime = (DateTime.Now - startTime).TotalMilliseconds;
                    result = GetReceivedUdsPacket(out receivedUdsPacket);
                    if (result)  // 收到了消息
                    {
                        if (receivedUdsPacket.ServerType == sendingUdsPacket.ServerType)  // 匹配服务类型
                        {
                            return AddReceivedUdsPacket(receivedUdsPacket);

                // 至此，完成接收
                // ------------------------------------------------------------------------------------------------------------------------
                        }
                        else if ((receivedUdsPacket.ServerType == NetUds.ServerTypes.NegativeResponse) && receivedUdsPacket.IsChildPacketValid && (receivedUdsPacket.ChildPacket?.GetType() == typeof(NetUds.NegativeResponsePacket)))  // 否定响应
                        {
                            var negativeResponsePacket = (NetUds.NegativeResponsePacket)receivedUdsPacket.ChildPacket;
                            if (negativeResponsePacket.ServerType == sendingUdsPacket.ServerType)  // 匹配服务类型
                            {
                                switch (negativeResponsePacket.NegativeResponseCode)
                                {
                                    case NetUds.NegativeResponsePacket.NegativeResponseCodes.RequestCorrectlyReceived_ResponsePending:
                                        waitTime = udsDiagnosticLayerParams.p2xClient;  // 延长等待时间
                                        startTime = DateTime.Now;  // 重新计时
                                        continue;
                                    default:
                                        message = $"收到否定响应！{BitConverter.ToString(receivedUdsPacket.Data)}";
                                        goto failed;
                                }
                            }
                            else  // 不匹配服务类型，与本次通信不相关，丢弃不处理
                            {
                                continue;
                            }
                        }
                        else  // 不匹配服务类型，与本次通信不相关，丢弃不处理
                        {
                            continue;
                        }
                    }
                    else  // 没有收到消息
                    {
                        if (usedTime > waitTime)  // 超出等待时间限制
                        {
                            message = $"响应超时！{BitConverter.ToString(sendingUdsPacket.Data)}";
                            goto failed;
                        }
                        else  // 在可等待的时间范围内，继续等待
                        {
                            continue;
                        }
                    }
                }


                
            }
            else
            {
                message = $"UDS 会话被占用，无法完成发送！{BitConverter.ToString(sendingUdsPacket.Data)}";
                goto failed;
            }

        failed:
            receivedUdsPacket = new NetUds.UDSPacket();
            AddLogMethod?.Invoke(message, LogHelper.LogLevel.Warning);
            return false;
        }
    }
}
