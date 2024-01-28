using CsvHelper.Configuration.Attributes;
using EthDiagnosticTool.Global.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthDiagnosticTool.ProductManager.Config
{
    [JsonObject(MemberSerialization.OptOut)]
    public class UdsDiagnosticLayerParams
    {
        #region 定义

        /// <summary>
        /// DTC 描述信息
        /// </summary>
        public class DtcDescription
        {
            [Ignore]
            public byte[] dtc = new byte[3];

            /// <summary>
            /// DTC
            /// </summary>
            [Index(0)]
            [Name("DTC")]
            public string DTC
            {
                get
                {
                    return BitConverter.ToString(dtc).Replace("-", "");
                }
                set
                {
                    var _dtc = new byte[3];
                    try
                    {
                        var hexString = value.Trim().StartsWith("0x") ? value.Trim().Substring(2, 6) : value.Trim().Substring(0, 6);
                        for (int i = 0; i < _dtc.Length; i++)
                        {
                            _dtc[i] = Convert.ToByte(hexString.Substring(2 * i, 2), 16);
                        }
                        Array.Copy(_dtc, dtc, 3);
                    }
                    catch { }
                }
            }

            /// <summary>
            /// UDS SAE DTC。
            /// PCBU 格式。
            /// P（Power）动力，C（Chassis）底盘，B（Body）车身，U（Network and Vehicle Integration）网络和车辆集成。
            /// </summary>
            [Index(1)]
            [Name("DTC SAE")]
            public string DTC_SAE
            {
                get
                {
                    string s;
                    if (dtc[0] < 0x40)
                    {
                        s = "P" + (dtc[0] >> 4).ToString("X") + (dtc[0] & 0x0F).ToString("X");
                    }
                    else if (dtc[0] < 0x80)
                    {
                        s = "C" + ((dtc[0] >> 4) - 4).ToString("X") + (dtc[0] & 0x0F).ToString("X");
                    }
                    else if (dtc[0] < 0xC0)
                    {
                        s = "B" + ((dtc[0] >> 4) - 8).ToString("X") + (dtc[0] & 0x0F).ToString("X");
                    }
                    else
                    {
                        s = "U" + ((dtc[0] >> 4) - 12).ToString("X") + (dtc[0] & 0x0F).ToString("X");
                    }
                    s += dtc[1].ToString("X2") + dtc[2].ToString("X2");
                    return s;
                }
                set
                {
                    var _dtc = new byte[3];
                    try
                    {
                        _dtc[0] = Convert.ToByte(value.Substring(1, 2), 16);
                        if (value[0] == 'P')
                        {
                            _dtc[0] += 0;
                        }
                        else if (value[0] == 'C')
                        {
                            _dtc[0] += 0x40;
                        }
                        else if (value[0] == 'B')
                        {
                            _dtc[0] += 0x80;
                        }
                        else if (value[0] == 'U')
                        {
                            _dtc[0] += 0xC0;
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        _dtc[1] = Convert.ToByte(value.Substring(3, 2), 16);
                        _dtc[2] = Convert.ToByte(value.Substring(5, 2), 16);
                        Array.Copy(_dtc, dtc, 3);
                    }
                    catch { }
                }
            }

            /// <summary>
            /// 故障名称
            /// </summary>
            /// </summary>
            [Index(2)]
            [Name("故障代码名称")]
            public string FaultName { get; set; }

            /// <summary>
            /// 故障描述
            /// </summary>
            [Index(3)]
            [Name("故障代码描述")]
            public string Description { get; set; }

            [Ignore]
            public bool enableFilter = false;

            [Index(4)]
            [Name("允许过滤")]
            [Default("0")]
            [Optional]
            public int EnableFilter
            {
                get
                {
                    return enableFilter ? 1 : 0;
                }
                set
                {
                    enableFilter = value != 0 ? true : false;
                }
            }

            [Ignore]
            public byte maskFilter1 = 0xFF;

            /// <summary>
            /// 要求必须存在的标志。符合条件 mask & 0xFF == 0xFF 时过滤不显示。
            /// 默认为 0xFF：除去 0xFF 外都显示。
            /// </summary>
            [Index(5)]
            [Name("过滤掩码1")]
            [Default("0xFF")]
            [Optional]
            public string MaskFilter1
            {
                get
                {
                    return maskFilter1.ToString("X2");
                }
                set
                {
                    maskFilter1 = Convert.ToByte(value, 16);
                }
            }

            [Ignore]
            public byte maskFilter2 = 0x00;

            /// <summary>
            /// 要求必须不存在的标志。符合条件 mask | 0x00 == 0x00 时过滤不显示。
            /// 默认为 0x00：除去 0x00 外都显示。
            /// </summary>
            [Index(6)]
            [Name("过滤掩码2")]
            [Default("0x00")]
            [Optional]
            public string MaskFilter2
            {
                get
                {
                    return maskFilter2.ToString("X2");
                }
                set
                {
                    maskFilter2 = Convert.ToByte(value, 16);
                }
            }

            public DtcDescription()
            {
                FaultName = "";
                Description = "";
            }
        }

        #endregion
        #region 字段


        #region Tester Present 请求相关字段

        /// <summary>
        /// 是否自动发送 Tester-Present 消息（0x3E）。
        /// </summary>
        public bool sendTesterPresent = false;

        /// <summary>
        /// 发送 Tester Present 消息的间隔时间（ms）。
        /// </summary>
        public uint sendTesterPresentTime = 2000;

        /// <summary>
        /// Tester 发送一条 UDS 命令之后，下次再发送命令的最小时间间隔（ms）。
        /// </summary>
        public uint s3ClientTime = 100;

        /// <summary>
        /// ECU 保持非 Default Session 的最大空闲时间。
        /// </summary>
        public uint s3ServerTime = 5333;

        #endregion

        #region 时间相关字段

        /// <summary>
        /// Tester 成功发出请求之后，等待收到 ECU 响应的最大时间（ms）。
        /// </summary>
        public uint p2Client = 150;

        /// <summary>
        /// Tester 在没有超时（p2Client）的情况下收到 NRC 0x78 后，等待收到 ECU 响应的最大时间（ms）（在 p2Client 的基础上适当延长）。
        /// </summary>
        public uint p2xClient = 2000;

        /// <summary>
        /// ECU 收到请求之后，处理并发出响应的时间（ms）。
        /// </summary>
        public uint p2Server = 50;

        /// <summary>
        /// ECU 收到请求之后，由于无法在 p2Server 时间内处理，发出 NRC 0x78 后处理并发出响应的最大时间（ms）（如仍无法处理，发送 NRC 0x78 重置 p2xServer 时间）。
        /// </summary>
        public uint p2xServer = 1900;

        /// <summary>
        /// 支持 0x21 （忙：需重复请求）的 NRC 响应，即当收到 NRC 后支持自动重复发送请求。
        /// </summary>
        public bool supportResponseBusyRepeatRequest = false;

        /// <summary>
        /// 在收到 NRC 0x21 后，重复发送请求前的等待时间。
        /// </summary>
        public uint repeatRequestAfter = 10;

        /// <summary>
        /// （与 BRR 有关，暂未知使用方法。）
        /// </summary>
        [JsonIgnore]
        internal uint completeWithin = 1300;

        #endregion

        #region 附件相关

        /// <summary>
        /// 相对路径的相对文件夹路径。
        /// 在导入时设定。
        /// </summary>
        [JsonIgnore]
        public string? directoryPath = null;

        /// <summary>
        /// Seed Key 文件路径。
        /// null 表示未指定。
        /// </summary>
        [JsonProperty(propertyName: "Seed Key 算法文件路径")]
        public string? seedKeyDllFilePath = null;

        /// <summary>
        /// DTC 描述信息文件路径。
        /// null 表示未指定。
        /// </summary>
        [JsonProperty(propertyName: "DTC 清单文件路径")]
        public string? dtcDescriptionFilePath = null;

        [JsonProperty(propertyName: "解析文件路径")]
        public string? mappingBytesToString = null;

        [JsonProperty(propertyName: "算法文件路径")]
        public string? mappingBytesToBytes = null;

        /// <summary>
        /// DTC 信息。
        /// 根据路径读取。
        /// </summary>
        [JsonIgnore]
        public DtcDescription[] dtcDescriptions = Array.Empty<DtcDescription>();

        #endregion

        #endregion

        #region 构造
        /// <summary>
        /// 默认参数构造
        /// </summary>
        public UdsDiagnosticLayerParams()
        {
            ;
        }
        #endregion

        public DtcDescription[] LoadDtcDescriptions()
        {
            if (dtcDescriptionFilePath == null || directoryPath == null)
            {
                dtcDescriptions = Array.Empty<DtcDescription>();
            }
            else
            {
                dtcDescriptions = FileHelper.ReadFromFile<DtcDescription>(PathHelper.GetRootPath(dtcDescriptionFilePath, directoryPath ?? ""));
            }
            return dtcDescriptions;
        }
    }
}
