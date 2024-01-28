using EthDiagnosticTool.Global;
using EthDiagnosticTool.Global.Helper;
using EthDiagnosticTool.ProductManager.Config;
using EthDiagnosticTool.UDS.StandardServers;
using log4net;
using Microsoft.VisualBasic.Logging;
using NetworkFrames.Packets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EthDiagnosticTool.Mapping.ToString;
using static EthDiagnosticTool.Global.Helper.ConfigHelper;
using System.Text.RegularExpressions;

namespace EthDiagnosticTool.Forms
{
    public partial class Demo : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        private UDS.DoIP.Server? doipServer;

        public enum FormStatus
        {
            Unknow,

            /// <summary>
            /// 未选定产品
            /// </summary>
            UnsetProduct,

            /// <summary>
            /// 正在配置产品
            /// </summary>
            SettingProduct,

            /// <summary>
            /// 未开始或停止状态
            /// </summary>
            NotStart,

            /// <summary>
            /// 正在启动
            /// </summary>
            Starting,

            /// <summary>
            /// 已启动，运行中
            /// </summary>
            Running,

            /// <summary>
            /// 正忙
            /// </summary>
            Busy,

            /// <summary>
            /// 正在停止
            /// </summary>
            Stopping,
        }

        public Demo()
        {
            InitializeComponent();
        }


        private FormStatus status;

        /// <summary>
        /// 运行状态
        /// </summary>
        private FormStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                label3.Text = status switch
                {
                    FormStatus.UnsetProduct => "未选择产品",
                    FormStatus.SettingProduct => "正在配置产品...",
                    FormStatus.NotStart => "等待开始",
                    FormStatus.Starting => "正在启动...",
                    FormStatus.Running => "已启动",
                    FormStatus.Busy => "正忙...",
                    FormStatus.Stopping => "正在关闭...",
                    _ => "未知状态",
                };
                if (status == FormStatus.Unknow)
                {
                    throw new Exception("异常的未知状态！");
                }
                UpdateButton_CheckEnable();
            }
        }

        /// <summary>
        /// 检查产品选框的可用性
        /// </summary>
        /// <returns></returns>
        private bool ProductComboBox_CheckEnable()
        {
            if (status == FormStatus.UnsetProduct)  // 仅当是 Unset 的状态，产品选框才可用
            {
                comboBox1.Enabled = true;
            }
            else
            {
                comboBox1.Enabled = false;
            }
            return comboBox1.Enabled;
        }

        /// <summary>
        /// 检查选择按键的可用性
        /// </summary>
        private void SelectProductButton_CheckEnable()
        {
            if (status == FormStatus.UnsetProduct)  // 仅当是 Unset 的状态，产品选框选定按键才可用
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        /// <summary>
        /// 检查重置按键的可用性
        /// </summary>
        private void ResetProductButton_CheckEnable()
        {
            if (status != FormStatus.UnsetProduct)  // 只要不是 Unset 的状态，产品选框重置按键均可用
            {
                button7.Enabled = true;
            }
            else
            {
                button7.Enabled = false;
            }
        }

        /// <summary>
        /// 检查开始按键的可用性
        /// </summary>
        private void StartButton_CheckEnable()
        {
            if (status == FormStatus.NotStart)  // 仅当是 NotStart 的状态，开始按键才可用
            {
                button2.Enabled = true;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        /// <summary>
        /// 检查停止按键的可用性
        /// </summary>
        private void StopButton_CheckEnable()
        {
            if (status == FormStatus.Running)  // 仅当是 Running 的状态，停止按键才可用
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }

        /// <summary>
        /// 更新按键的可用性
        /// </summary>
        private void UpdateButton_CheckEnable()
        {
            ProductComboBox_CheckEnable();
            SelectProductButton_CheckEnable();
            ResetProductButton_CheckEnable();
            StartButton_CheckEnable();
            StopButton_CheckEnable();
        }

        /// <summary>
        /// 刷新产品清单
        /// </summary>
        private void RefreshProductList()
        {
            productConfig = null;
            ConfigHelper.RefreshProductList();
            comboBox1.Items.Clear();
            foreach (var p in ConfigHelper.productConfigs)
            {
                comboBox1.Items.Add(p.productBasicInfo.Name);
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 加载窗口前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Demo_Load(object sender, EventArgs e)
        {
            Text = $"{Text}（{Global.Constant.ApplicationVersion}）";
            UpdateButton_CheckEnable();
            Status = FormStatus.UnsetProduct;
            RefreshProductList();
        }

        private ConfigHelper.ProductConfig? productConfig;

        /// <summary>
        /// 映射方法字典（绑定 comboBox2）
        /// </summary>
        private Dictionary<string, ToStringDelegate> mappingMethod = new Dictionary<string, ToStringDelegate>();

        /// <summary>
        /// 动态编译的方法转换器
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private ToStringDelegate ToStringMethod(ToStringDelegate method)
        {
            var m = new Func<byte[], object[], string>((content, os) =>
            {
                var data = new byte[content.Length - 3];
                Array.Copy(content, 3, data, 0, data.Length);
                return method.Invoke(data);
            });
            return new ToStringDelegate(m);
        }

        /// <summary>
        /// 刷新映射方法
        /// </summary>
        private void RefreshMappingMethod()
        {
            mappingMethod.Clear();

            Type? type = Global.Reflection.assembly.GetType("EthDiagnosticTool.Mapping.ToString");
            var commonNames = new string[] { "Equals", "GetHashCode", "GetType", "ToString" };


            var methodInfos = type?.GetMethods();
            if (methodInfos != null)
            {
                foreach (var m in methodInfos)
                {
                    if (commonNames.Contains(m.Name))
                    {
                        continue;
                    }
                    mappingMethod[m.Name] = m.CreateDelegate<ToStringDelegate>();
                }
            }
            if (UDS.Server.udsDiagnosticLayerParams.mappingBytesToString != null)
            {
                var mappingToStringCode = FileHelper.ReadFromFile(PathHelper.GetRootPath(UDS.Server.udsDiagnosticLayerParams.mappingBytesToString, UDS.Server.udsDiagnosticLayerParams.directoryPath ?? ""));
                if (Global.CompileHelper.GenerateAssemblyFromCode(mappingToStringCode, out var assembly, out string message))
                {
                    type = assembly?.GetType("Mapping.ToString");

                    methodInfos = type?.GetMethods();
                    if (methodInfos != null)
                    {
                        foreach (var m in methodInfos)
                        {
                            if (commonNames.Contains(m.Name))
                            {
                                continue;
                            }
                            mappingMethod[m.Name] = ToStringMethod(m.CreateDelegate<ToStringDelegate>());  // 动态编译的解析算法，定义传入参数为仅内容，不包含 UDS 结构，因此需要套入一层转接口方法。
                        }
                    }
                }

            }

            comboBox2.Items.Clear();
            foreach (var m in mappingMethod)
            {
                comboBox2.Items.Add(m.Key);
            }
            if (comboBox2.Items.Count > 0)
            {
                comboBox2.SelectedIndex = 0;
            }
        }

        private void DidButton_Click(object sender, EventArgs e)
        {
            if (productConfig == null)
            {
                return;
            }
            if (sender.GetType() == typeof(Button))
            {
                var sendingUdsPacket = new NetworkFrames.Packets.UDS.UDSPacket();
                var button = (Button)sender;
                string buttonName = button.Name.ToString();
                var match = Regex.Match(buttonName, @"(\w+):(.+)");
                if (match.Success)
                {
                    var title = match.Groups[1].Value;
                    var type = match.Groups[2].Value;

                    if (title == "ReadDid")
                    {
                        sendingUdsPacket.ServerType = NetworkFrames.Packets.UDS.ServerTypes.ReadDID;
                        var didButtonConfigs = (from _didButtonConfigs in productConfig.Value.udsSection.readDidButtonConfig where _didButtonConfigs.Name == type select _didButtonConfigs).ToArray();
                        if (didButtonConfigs.Length > 0)
                        {
                            var didButtonConfig = didButtonConfigs[0];
                            sendingUdsPacket.Payload = didButtonConfig.did;
                            SingleUdsCommunicate(didButtonConfig.Name, sendingUdsPacket, SingleUdsCommunicate, SingleUdsCommunicateDefaultCallBack, SingleUdsCommunicateDefaultCallBack, mappingMethod[didButtonConfig.Mapping]);
                        }
                        else
                        {
                            AddLogNow("未在配置文件列表中找到此项目！", LogHelper.LogLevel.Warning);
                            return;
                        }
                    }
                    else if (title == "WriteDid")
                    {
                        sendingUdsPacket.ServerType = NetworkFrames.Packets.UDS.ServerTypes.WriteDID;
                        var didButtonConfigs = (from _didButtonConfigs in productConfig.Value.udsSection.writeDidButtonConfig where _didButtonConfigs.Name == type select _didButtonConfigs).ToArray();
                        if (didButtonConfigs.Length > 0)
                        {
                            var didButtonConfig = didButtonConfigs[0];
                            sendingUdsPacket.Payload = didButtonConfig.did;
                            SingleUdsCommunicate(didButtonConfig.Name, sendingUdsPacket, SingleUdsCommunicate, SingleUdsCommunicateDefaultCallBack, SingleUdsCommunicateDefaultCallBack, mappingMethod[didButtonConfig.Mapping]);
                        }
                        else
                        {
                            AddLogNow("未在配置文件列表中找到此项目！", LogHelper.LogLevel.Warning);
                            return;
                        }
                    }
                    else
                    {
                        AddLogNow("超出预定义的按键类型！");
                        return;
                    }
                }
                else
                {
                    AddLogNow("未能识别按键类型！");
                    return;
                }
            }
            AddLogNow("该控件类型不可用！");
        }

        private System.Windows.Forms.Button MakeButton(DidButtonConfig didButtonConfig, string title)
        {
            var button = new System.Windows.Forms.Button();

            button.Dock = DockStyle.Fill;
            button.Location = new Point(3, 3);
            button.Name = $"{title}:{didButtonConfig.Name}";
            button.Size = new Size(147, 44);
            button.TabIndex = 0;
            button.Text = didButtonConfig.Name;
            button.UseVisualStyleBackColor = true;
            button.Click += DidButton_Click;

            return button;
        }

        private void RefreshDidButtons(TableLayoutPanel tableLayoutPanel, DidButtonConfig[] didButtonConfigs, string title)
        {
            var index = 0;

            tableLayoutPanel.Controls.Clear();

            foreach (var didButtonConfig in didButtonConfigs)
            {
                if (index % 5 == 0)
                {
                    tableLayoutPanel.RowCount++;
                    tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
                }
                var button = MakeButton(didButtonConfig, title);
                tableLayoutPanel.Controls.Add(button, index % 5, index / 5);
                index++;
            }
        }

        /// <summary>
        /// 选定产品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                return;
            }
            Status = FormStatus.SettingProduct;

            AddLogNow("正在加载产品配置文件，需要数秒时间...", LogHelper.LogLevel.Info);
            productConfig = ConfigHelper.productConfigs[comboBox1.SelectedIndex];
            productConfig.Value.udsSection.GetReady();
            UDS.Server.udsDiagnosticLayerParams = productConfig.Value.udsSection.udsDiagnosticLayerParams;
            AddLogNow("产品配置文件加载完成！", LogHelper.LogLevel.Info);

            RefreshDidButtons(tableLayoutPanel17, productConfig.Value.udsSection.readDidButtonConfig, "ReadDid");
            RefreshDidButtons(tableLayoutPanel21, productConfig.Value.udsSection.writeDidButtonConfig, "WriteDid");
            RefreshMappingMethod();

            UDS.Server.AddLogMethod = AddLog;
            UDS.Server.Start();
            Status = FormStatus.NotStart;
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            AddLogNow("重置", LogHelper.LogLevel.Info);
            button3_Click(sender, e);
            UDS.Server.Stop();
            RefreshProductList();
            Status = FormStatus.UnsetProduct;
            AddLogNow("重置完成", LogHelper.LogLevel.Info);
        }

        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (productConfig == null)
            {
                AddLog("没有选定产品！");
                return;
            }
            if (productConfig.Value.udsSection.udsTransportLayerParams.transportLayerType == UdsTransportLayerParams.UdsTransportLayerType.DoIP)
            {
                if (productConfig.Value.udsSection.udsTransportLayerParams.DoIP == null)
                {
                    AddLog("缺失 DoIP 参数！");
                    return;
                }
                if (doipServer == null)
                {
                    doipServer = new UDS.DoIP.Server(productConfig.Value.udsSection.udsTransportLayerParams.DoIP);
                }
                else
                {
                    doipServer.UdsTransportLayerParams_DoIP = productConfig.Value.udsSection.udsTransportLayerParams.DoIP;
                }
                doipServer.AddLogMethod = AddLog;
                doipServer.AddResultMethod = AddResult;
                doipServer.SetStatusMethod = SetStatus;
                doipServer.Start();
                UDS.Server.ReceiveUdsPacketMethod = doipServer.ReceiveUdsPacket;
                UDS.Server.SendUdsPacketMethod = doipServer.SendUdsPacket;
            }
            else
            {
                throw new NotImplementedException();
            }
        }


        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            UDS.Server.SendUdsPacketMethod = null;
            UDS.Server.ReceiveUdsPacketMethod = null;
            if (doipServer != null)
            {
                doipServer.Stop();
            }
        }

        /// <summary>
        /// 立即添加 Log（仅在本线程中可用）
        /// </summary>
        /// <param name="message"></param>
        /// <param name="level"></param>
        public void AddLogNow(string message, LogHelper.LogLevel level = LogHelper.LogLevel.Debug)
        {
            switch (level)
            {
                case LogHelper.LogLevel.Debug:
                    log.Debug(message); break;
                case LogHelper.LogLevel.Info:
                    log.Info(message); break;
                case LogHelper.LogLevel.Warning:
                    log.Warn(message); break;
                case LogHelper.LogLevel.Error:
                    log.Error(message); break;
                case LogHelper.LogLevel.Fatal:
                    log.Fatal(message); break;
                default:
                    break;
            }
            textBox1.AppendText(string.Format("{0} {1}\r\n", DateTime.Now.ToLongTimeString(), message));
        }

        /// <summary>
        /// 追加 log
        /// </summary>
        /// <param name="message"></param>
        public void AddLog(string message, LogHelper.LogLevel level = LogHelper.LogLevel.Debug)
        {
            BeginInvoke(new Action(() =>
            {
                AddLogNow(message, level);
            }));
        }

        /// <summary>
        /// 追加结果
        /// </summary>
        /// <param name="message"></param>
        public void AddResult(string message)
        {
            BeginInvoke(new Action(() =>
            {
                log.Debug(message);
                richTextBox1.AppendText(string.Format("{0}\r\n", message));
                richTextBox1.ScrollToCaret();
            }));
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="status"></param>
        internal void SetStatus(UDS.DoIP.Server.ConnectStatus status)
        {
            BeginInvoke(new Action(() =>
            {
                Status = status switch
                {
                    UDS.DoIP.Server.ConnectStatus.NotConnected => FormStatus.NotStart,
                    UDS.DoIP.Server.ConnectStatus.Connecting => FormStatus.Starting,
                    UDS.DoIP.Server.ConnectStatus.Connected => FormStatus.Running,
                    UDS.DoIP.Server.ConnectStatus.Closing => FormStatus.Stopping,
                    _ => FormStatus.Unknow,
                };
                if (productConfig == null)
                {
                    Status = FormStatus.UnsetProduct;
                }
            }));
        }

        private bool SingleUdsCommunicate(NetworkFrames.Packets.UDS.UDSPacket udsPacket, out NetworkFrames.Packets.UDS.UDSPacket receivedUdsPacket, out string message)
        {
            return UDS.Server.SingleUdsCommunicate(udsPacket, out receivedUdsPacket, out message);
        }

        /// <summary>
        /// 标准会话
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            var sendingUdsPacket = new NetworkFrames.Packets.UDS.UDSPacket()
            {
                ServerType = NetworkFrames.Packets.UDS.ServerTypes.DiagnosticSessionControl,
            };
            sendingUdsPacket.Payload = new byte[] { 0x01 };
            SingleUdsCommunicate(button8.Text, sendingUdsPacket, SingleUdsCommunicate, SingleUdsCommunicateDefaultCallBack, SingleUdsCommunicateDefaultCallBack);
        }

        /// <summary>
        /// 切换会话回调方法
        /// </summary>
        /// <param name="sendingUdsPacket"></param>
        /// <param name="receivedUdsPacket"></param>
        private void SingleUdsCommunicateDefaultCallBack(NetworkFrames.Packets.UDS.UDSPacket sendingUdsPacket, NetworkFrames.Packets.UDS.UDSPacket receivedUdsPacket, ToStringDelegate? mappingToString = null)
        {
            if (mappingToString != null)
            {
                AddResult(mappingToString(receivedUdsPacket.Data));
            }
            else
            {
                AddResult($"{BitConverter.ToString(receivedUdsPacket.Data)}");
            }
        }

        private delegate void UdsSingleUdsCommunicateCallBackDelegate(NetworkFrames.Packets.UDS.UDSPacket sendingUdsPacket, NetworkFrames.Packets.UDS.UDSPacket receivedUdsPacket, ToStringDelegate? mappingToString = null);

        private delegate bool SingleUdsCommunicateDelegate(NetworkFrames.Packets.UDS.UDSPacket udsPacket, out NetworkFrames.Packets.UDS.UDSPacket receivedUdsPacket, out string message);

        /// <summary>
        /// 单次 UDS 通信
        /// </summary>
        /// <param name="title"></param>
        /// <param name="sendingUdsPacket"></param>
        /// <param name="successCallBack"></param>
        /// <param name="failCallBack"></param>
        private void SingleUdsCommunicate(string title, NetworkFrames.Packets.UDS.UDSPacket sendingUdsPacket, SingleUdsCommunicateDelegate singleUdsCommunicate, UdsSingleUdsCommunicateCallBackDelegate successCallBack, UdsSingleUdsCommunicateCallBackDelegate failCallBack, ToStringDelegate? mappingToString = null)
        {
            AddResult($"∨∨∨∨∨∨∨∨∨∨∨∨∨∨∨∨∨∨∨∨");
            AddResult($"开始执行{title}命令");
            if (Status == FormStatus.Running)
            {
                if (singleUdsCommunicate(sendingUdsPacket, out var receivedUdsPacket, out var message))
                {
                    AddResult($"执行{title}命令成功！");
                    try
                    {
                        successCallBack(sendingUdsPacket, receivedUdsPacket, mappingToString);
                    }
                    catch (Exception ex)
                    {
                        AddResult($"执行{title}遇到错误！{ex}");
                    }
                }
                else
                {
                    AddResult($"执行{title}命令失败：{message}");
                    failCallBack(sendingUdsPacket, receivedUdsPacket);
                }
            }
            else
            {
                AddResult($"执行{title}命令失败：没有建立连接！");
            }
            AddResult($"∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧∧");
        }

        /// <summary>
        /// 清空结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }

        /// <summary>
        /// 清空 log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void Demo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (button7.Enabled)
            {
                button7_Click(sender, e);
            }
        }

        private void SingleUdsCommunicateCustomCallBack(NetworkFrames.Packets.UDS.UDSPacket sendingUdsPacket, NetworkFrames.Packets.UDS.UDSPacket receivedUdsPacket, ToStringDelegate? mappingToString = null)
        {
            if (sendingUdsPacket.ServerType == NetworkFrames.Packets.UDS.ServerTypes.ReadDID)
            {
                var data = new byte[receivedUdsPacket.Data.Length - 0];
                Array.Copy(receivedUdsPacket.Data, 0, data, 0, data.Length);
                AddResult(mappingMethod[comboBox2.SelectedItem.ToString() ?? ""].Invoke(data));
            }
            else
            {
                var data = new byte[receivedUdsPacket.Data.Length - 0];
                Array.Copy(receivedUdsPacket.Data, 0, data, 0, data.Length);
                AddResult(mappingMethod[comboBox2.SelectedItem.ToString() ?? ""].Invoke(data));
            }
        }

        /// <summary>
        /// 发送自定义命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button19_Click(object sender, EventArgs e)
        {
            var hexString = textBox5.Text.Trim().Replace("-", "").Replace(" ", "");
            if (hexString.Length % 2 == 1)
            {
                hexString = "0" + hexString;
            }
            var data = new byte[hexString.Length / 2];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            var sendingUdsPacket = new NetworkFrames.Packets.UDS.UDSPacket(data);
            SingleUdsCommunicate("自定义命令", sendingUdsPacket, SingleUdsCommunicate, SingleUdsCommunicateCustomCallBack, SingleUdsCommunicateDefaultCallBack);
        }

        private void SingleUdsCommunicateReadDtcCountCallBack(NetworkFrames.Packets.UDS.UDSPacket sendingUdsPacket, NetworkFrames.Packets.UDS.UDSPacket receivedUdsPacket, ToStringDelegate? mappingToString = null)
        {
            var data = new byte[receivedUdsPacket.Data.Length - 2];
            Array.Copy(receivedUdsPacket.Data, 2, data, 0, data.Length);
            AddResult(Mapping.ToString.ReportNumberOfDtc(data, UDS.Server.udsDiagnosticLayerParams.dtcDescriptions));
        }

        /// <summary>
        /// 发送读取指定条件 DTC 命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button14_Click(object sender, EventArgs e)
        {
            var statusMask = Convert.ToByte(textBox3.Text, 16);
            var sendingUdsPacket = new NetworkFrames.Packets.UDS.UDSPacket()
            {
                ServerType = NetworkFrames.Packets.UDS.ServerTypes.ReadDtcInformation,
            };
            sendingUdsPacket.Payload = new byte[] { 0x01, statusMask };
            SingleUdsCommunicate(button14.Text, sendingUdsPacket, SingleUdsCommunicate, SingleUdsCommunicateDefaultCallBack, SingleUdsCommunicateDefaultCallBack, Mapping.ToString.ReportNumberOfDtc);
        }

        /// <summary>
        /// 切换会话回调方法
        /// </summary>
        /// <param name="sendingUdsPacket"></param>
        /// <param name="receivedUdsPacket"></param>
        private void SingleUdsCommunicateReadDtcInfoCallBack(NetworkFrames.Packets.UDS.UDSPacket sendingUdsPacket, NetworkFrames.Packets.UDS.UDSPacket receivedUdsPacket, ToStringDelegate? mappingToString = null)
        {
            AddResult(Mapping.ToString.ReportDtcInfomation(receivedUdsPacket.Data, new object[] { }));
        }

        /// <summary>
        /// 发送读取指定条件 DTC 信息命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button18_Click(object sender, EventArgs e)
        {
            var statusMask = Convert.ToByte(textBox4.Text, 16);
            var sendingUdsPacket = new NetworkFrames.Packets.UDS.UDSPacket()
            {
                ServerType = NetworkFrames.Packets.UDS.ServerTypes.ReadDtcInformation,
            };
            sendingUdsPacket.Payload = new byte[] { 0x02, statusMask };
            SingleUdsCommunicate(button14.Text, sendingUdsPacket, SingleUdsCommunicate, SingleUdsCommunicateReadDtcInfoCallBack, SingleUdsCommunicateDefaultCallBack);
        }

        /// <summary>
        /// 切换会话：扩展会话
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            var sendingUdsPacket = new NetworkFrames.Packets.UDS.UDSPacket()
            {
                ServerType = NetworkFrames.Packets.UDS.ServerTypes.DiagnosticSessionControl,
            };
            sendingUdsPacket.Payload = new byte[] { 0x03 };
            SingleUdsCommunicate(button8.Text, sendingUdsPacket, SingleUdsCommunicate, SingleUdsCommunicateDefaultCallBack, SingleUdsCommunicateDefaultCallBack);
        }
    }
}
