using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace EthDiagnosticTool.ProductManager.Config
{
    [JsonObject(MemberSerialization.OptOut)]
    public class UdsTransportLayerParams_DoCAN
    {
        #region 定义
        /// <summary>
        /// CAN ID
        /// </summary>
        [JsonObject(MemberSerialization.OptIn)]
        public struct CanId
        {
            private uint _id; // = 0;
            private bool _isExtend; // = false;

            /// <summary>
            /// CAN ID。
            /// 值为 0 时无效，值大于 0x7FF 时自动设置为扩展 ID。
            /// </summary>
            public uint Id
            {
                get
                {
                    return _id;
                }
                set
                {
                    _id = value;

                    if (value > 0x7FF)
                    {
                        _isExtend = true;
                    }
                }
            }

            /// <summary>
            /// 是否为扩展 ID。
            /// </summary>
            public bool IsExtend
            {
                get
                {
                    return _isExtend;
                }
                set
                {
                    if (!value && _id > 0x7FF)
                    {
                        throw new Exception(string.Format("无法将此扩展 CAN ID 0x{0} 设为标准帧 CAN ID。", Id.ToString("X3")));
                    }
                    _isExtend = value;
                }
            }

            /// <summary>
            /// 字符串格式的 CAN ID。
            /// 使用末尾 x 表示扩展帧。
            /// </summary>
            [JsonProperty(PropertyName = "CAN ID")]
            public string Id_string
            {
                get
                {
                    return string.Format("0x{0:X}{1}", Id, IsExtend ? "x" : "");
                }
                set
                {
                    bool isExtend = value.EndsWith('x');
                    if (isExtend)
                    {
                        value = value.Remove(value.Length - 1);
                    }
                    uint canId = Convert.ToUInt32(value, 16);
                    if (canId > 0x7FF) isExtend = true;

                    Id = canId;
                    IsExtend = isExtend;
                }
            }

            /// <summary>
            /// 直接构造
            /// </summary>
            /// <param name="canId"></param>
            /// <param name="isExtend"></param>
            public CanId(uint canId, bool? isExtend = null)
            {
                //Id = canId;
                _id = canId;
                if (isExtend.HasValue)
                {
                    //IsExtend = isExtend.Value; 
                    _isExtend = isExtend.Value; // _isExtend
                }
                else _isExtend = (/*null ??*/ false); // modified by Json // by-default-not- Extends
            }

            /// <summary>
            /// 通过字符串构造
            /// </summary>
            /// <param name="canId_s"></param>
            public CanId(string canId_s)
            {
                ///Id_string = canId_s;
                ///

                bool isExtend = canId_s.EndsWith('x');
                if (isExtend)
                {
                    var value = canId_s.Remove(canId_s.Length - 1);
                }
                uint canId = Convert.ToUInt32(canId_s, 16);
                if (canId > 0x7FF) isExtend = true;

                ///Id = canId;
                _id = canId;
                ///IsExtend = isExtend;
                _isExtend = isExtend;
                ///
            }
        }

        /// <summary>
        /// CAN Classic 与 CAN FD 混合模式。
        /// </summary>
        public enum CanMixingModes
        {
            /// <summary>
            /// 忽略模式。配置为 CAN Classic 时，忽略 CAN FD 帧。
            /// </summary>
            Ignore,

            /// <summary>
            /// 接受模式。配置为 CAN Classic 时，收到 CAN FD 帧，返回 CAN 帧。
            /// </summary>
            Accept,

            /// <summary>
            /// 适应模式。配置为 CAN Classic 时，收到 CAN FD 帧，返回 CAN FD 帧。
            /// </summary>
            Adapt
        }

        #endregion

        #region 字段

        #region 地址相关字段

        /// <summary>
        /// ECU 响应 Tester 的 Tester ID。
        /// </summary>
        [JsonIgnore]
        public CanId? responseFromEcuCanId = null;

        /// <summary>
        /// Tester 发起请求的 ECU ID。
        /// </summary>
        [JsonIgnore]
        public CanId? requestToEcuCanId = null;

        /// <summary>
        /// Tester 发起请求的广播 ID。
        /// </summary>
        [JsonProperty(propertyName: "Functional ID")]
        public CanId? functionalToEcuCanId = null;

        #endregion

        #region 附加的 ISO TP 协议参数

        /// <summary>
        /// 流控帧参数：块大小，单批次连续帧的最大数量。
        /// </summary>
        [JsonProperty(propertyName: "块大小")]
        public byte blockSize = 0;

        /// <summary>
        /// 流控帧参数：Space Time。
        /// </summary>
        [JsonIgnore]
        public byte sTmin = 0x14;

        /// <summary>
        /// 接收到首帧或连续帧后，回复流控帧的延迟时间（ms）。
        /// </summary>
        [JsonIgnore]
        public uint fcDelay = 20;

        /// <summary>
        /// 允许消息内容长度的最大值。
        /// </summary>
        [JsonProperty(propertyName: "最大内容长度")]
        public uint maxLength = 0xFFF;

        #endregion

        #region CAN FD 参数

        /// <summary>
        /// CAN FD 标志
        /// </summary>
        private bool _isCanFd = false;

        /// <summary>
        /// 允许的最小 DLC。
        /// </summary>
        private byte _mindlc = 8;

        /// <summary>
        /// 允许的最大 DLC。
        /// </summary>
        private byte _maxdlc = 15;

        /// <summary>
        /// 占位符。
        /// </summary>
        [JsonIgnore]
        public byte placeHolder = 0xCC;

        [JsonProperty(propertyName: "填充字节")]
        public string PlaceHolder
        {
            get
            {
                return placeHolder.ToString("X2");
            }
            set
            {
                placeHolder = Convert.ToByte(value, 16);
            }
        }

        /// <summary>
        /// CAN 协议混合模式。
        /// </summary>
        [JsonIgnore]
        public CanMixingModes canMixingMode = CanMixingModes.Ignore;

        #endregion

        #endregion

        #region 属性
        /// <summary>
        /// 从 ECU 获取响应的 Tester ID
        /// </summary>
        [JsonProperty(propertyName: "响应 CAN ID（Tester ID）")]
        public CanId? ResponseCanId
        {
            get
            {
                return responseFromEcuCanId;
            }
        }

        /// <summary>
        /// 向 ECU 发起请求的 ECU ID
        /// </summary>
        [JsonProperty(propertyName: "请求 CAN ID（ECU ID）")]
        public CanId? RequestCanId
        {
            get
            {
                return requestToEcuCanId ?? functionalToEcuCanId;
            }
        }

        /// <summary>
        /// 是否是 CAN FD，而不是 CAN Classic
        /// </summary>
        [JsonProperty(propertyName: "使用 CAN FD 协议")]
        public bool IsCanFd
        {
            get
            {
                return _isCanFd;
            }
            set
            {
                if (!value)
                {
                    MaxDlc = 8;
                }
                _isCanFd = value;
            }
        }

        /// <summary>
        /// 可接受的最小的 DLC。
        /// 将在设置值时做合理的修正。
        /// </summary>
        [JsonProperty(propertyName: "最小 DLC")]
        public byte MinDlc
        {
            get  // 不做检查，要求设置值的时候必须是合法的
            {
                return _mindlc;
            }
            set
            {
                // 检查值域
                // 限制最大值
                if (_isCanFd)  // CAN FD 时值域为 [0, 15]
                {
                    value = Math.Min(value, (byte)15);
                }
                else  // CAN Classic 时值域为 [0, 8]
                {
                    value = Math.Min(value, (byte)8);
                }
                // 限制最小值
                value = Math.Max(value, (byte)8);

                // 限制 min <= max，值大时提升 max 值
                if (value > _maxdlc)  // 限制值不能比 MaxDlc 大
                {
                    _maxdlc = value;
                }

                _mindlc = value;
            }
        }

        /// <summary>
        /// 可接受的最大的 DLC。
        /// 将在设置值时做合理的修正。
        /// </summary>
        [JsonProperty(propertyName: "最大 DLC")]
        public byte MaxDlc
        {
            get  // 不做检查，要求设置值的时候必须是合法的
            {
                // 检查值域
                if (_isCanFd)  // CAN FD 时值域为 [0, 15]
                {
                    return Math.Min(_maxdlc, (byte)15);
                }
                else  // CAN Classic 时值域为 [0, 8]
                {
                    return Math.Min(_maxdlc, (byte)8);
                }
            }
            set
            {
                // 检查值域
                // 限制最大值
                if (_isCanFd)  // CAN FD 时值域为 [0, 15]
                {
                    value = Math.Min(value, (byte)15);
                }
                else  // CAN Classic 时值域为 [0, 8]
                {
                    value = Math.Min(value, (byte)8);
                }
                // 限制最小值
                value = Math.Max(value, (byte)8);

                // 限制 min <= max，值小时降低 min 值
                if (value < _mindlc)  // 限制值不能比 MinDlc 小
                {
                    _mindlc = value;
                }

                _maxdlc = value;
            }
        }

        /// <summary>
        /// 流控帧参数：Space Time，连续帧最小间隔时间（ms）。
        /// </summary>
        [JsonProperty(propertyName: "连续帧最小间隔时间（ms）")]
        public byte STmin_ms
        {
            get
            {
                if (sTmin <= 0x7F)
                {
                    return sTmin;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                sTmin = Math.Min(value, (byte)0x7F);
            }
        }

        /// <summary>
        /// 流控帧参数：Space Time，连续帧最小间隔时间（us）。
        /// </summary>
        [JsonProperty(propertyName: "连续帧最小间隔时间（us）")]
        public uint STmin_us
        {
            get
            {
                if (sTmin <= 0x7F)
                {
                    return sTmin * 1000u;
                }
                else if (sTmin >= 0xF1 && sTmin <= 0xF9)
                {
                    return (sTmin - 0xF0u) * 100u;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (value == 0)
                {
                    sTmin = (byte)value;
                }
                else if (value < 1000)  // 以 100 us 为单位
                {
                    byte s = (byte)Math.Ceiling((double)value / 100);  // 向上取整
                    sTmin = s switch
                    {
                        0 => 0,
                        10 => 1,
                        _ => (byte)(0xF0 + s),
                    };
                }
                else
                {
                    sTmin = Math.Min((byte)0x7F, (byte)Math.Ceiling((double)value / 1000));
                }
            }
        }

        #endregion



        #region 构造

        /// <summary>
        /// 默认参数构造
        /// </summary>
        public UdsTransportLayerParams_DoCAN()
        {
            responseFromEcuCanId = new CanId(1);
            requestToEcuCanId = new CanId(1);
        }

        #endregion
    }
}
