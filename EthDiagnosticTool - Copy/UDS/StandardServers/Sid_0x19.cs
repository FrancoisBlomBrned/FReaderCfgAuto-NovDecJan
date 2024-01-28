using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace EthDiagnosticTool.UDS.StandardServers
{
    /// <summary>
    /// DTC 信息服务
    /// </summary>
    public static class Sid_0x19
    {
        #region 定义

        /// <summary>
        /// DTC 状态掩码标志
        /// </summary>
        [Flags]
        public enum StatusFlags : byte
        {
            /// <summary>
            /// 测试失效（当前）
            /// </summary>
            Test_failed = 1,

            /// <summary>
            /// 本次检测周期测试失效
            /// </summary>
            Test_failed_this_monitoring_cycle = 1 << 1,

            /// <summary>
            /// 待定 DTC
            /// </summary>
            Pending_DTC = 1 << 2,

            /// <summary>
            /// 确认 DTC（历史）
            /// </summary>
            Confirmed_DTC = 1 << 3,

            /// <summary>
            /// 上次清零后测试未完成
            /// </summary>
            Test_not_completed_since_last_clear = 1 << 4,

            /// <summary>
            /// 上次清零后测试失效
            /// </summary>
            Test_failed_since_last_clear = 1 << 5,

            /// <summary>
            /// 本次检测周期测试未完成
            /// </summary>
            Test_not_completed_this_monitoring_cycle = 1 << 6,

            /// <summary>
            /// 警告指示位请求
            /// </summary>
            Warning_indicator_requested = 1 << 7
        }

        public static string[] StatusFlagsDescription
        {
            get
            {
                return new string[8]
                {
                        "Test failed",
                        "Test failed this operation cycle",
                        "Pending DTC",
                        "Confirmed DTC",
                        "Test not completed since last clear",
                        "Test failed since last clear",
                        "Test not completed this operation cycle",
                        "Warning indicator requested"
                };
            }
        }

        /// <summary>
        /// DTC 信息
        /// </summary>
        public class DtcInfo
        {
            /// <summary>
            /// 诊断测试代码
            /// </summary>
            public byte[] dtc = new byte[3];

            /// <summary>
            /// 状态码
            /// </summary>
            public byte status;
            
            /// <summary>
            /// DTC 描述信息
            /// </summary>
            public ProductManager.Config.UdsDiagnosticLayerParams.DtcDescription dtcDescription = new ProductManager.Config.UdsDiagnosticLayerParams.DtcDescription();

            /// <summary>
            /// DTC 字符串
            /// </summary>
            public string DTC_string
            {
                get
                {
                    return BitConverter.ToString(dtc).Replace("-", "");
                }
            }

            /// <summary>
            /// 状态码标志
            /// </summary>
            public StatusFlags StatusFlags
            {
                get
                {
                    return (StatusFlags)status;
                }
            }

            /// <summary>
            /// 状态码字符串
            /// </summary>
            public string Status_string
            {
                get
                {
                    return status.ToString("X2");
                }
            }

            /// <summary>
            /// 是否显示
            /// </summary>
            public bool Show
            {
                get
                {
                    if (dtcDescription.enableFilter)
                    {
                        if (((status & dtcDescription.maskFilter1) == dtcDescription.maskFilter1) && ((status | dtcDescription.maskFilter2) == dtcDescription.maskFilter2))  // 被过滤掉，不显示
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }


        #endregion

        #region 方法

        public static string[] GetDtcStatus(StatusFlags statusCode)
        {
            var bits = Convert.ToString((byte)statusCode, 2).PadLeft(8, '0');
            bits = string.Join("", bits.Reverse());
            var lines = new List<string>();
            for (int i = 0; i < 8; i++)
            {
                lines.Add(string.Format("{0}: {1}", bits[i], StatusFlagsDescription[i]));
            }
            return lines.ToArray();
        }

        /// <summary>
        /// 按照状态掩码获取符合条件的 DTC 的数量报告的解析方法
        /// </summary>
        /// <param name="receiveContent"></param>
        /// <param name="availabilityMask"></param>
        /// <param name="formatIdentifier"></param>
        /// <param name="dtcCount"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool Sub_0x01_ReportNumberOfDtcByStatusMask(byte[] receiveContent, out byte availabilityMask, out byte formatIdentifier, out uint dtcCount, out string message)
        {
            if (receiveContent.Length < 6 || receiveContent[0] != 0x59 || receiveContent[1] != 0x01)
            {
                message = "响应内容不符合预期规则！";
                availabilityMask = 0;
                formatIdentifier = 0;
                dtcCount = 0;
                return false;
            }
            availabilityMask = receiveContent[2];
            formatIdentifier = receiveContent[3];
            dtcCount = (uint)(receiveContent[4] << 8 | receiveContent[5]);
            message = "";
            return true;
        }

        /// <summary>
        /// 按照状态掩码获取符合条件的 DTC 报告的解析方法
        /// </summary>
        /// <param name="receiveContent"></param>
        /// <param name="availabilityMask"></param>
        /// <param name="dtcInfos"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool Sub_0x02_ReportDtcByStatusMask(byte[] receiveContent, out byte availabilityMask, out DtcInfo[] dtcInfos, out string message, ProductManager.Config.UdsDiagnosticLayerParams.DtcDescription[]? dtcDescriptions = null)
        {
            if (receiveContent.Length < 6 || receiveContent[0] != 0x59 || receiveContent[1] != 0x02)
            {
                message = "响应内容不符合预期规则！";
                availabilityMask = 0;
                dtcInfos = Array.Empty<DtcInfo>();
                return false;
            }

            message = "";
            List<DtcInfo> dtcInfos_list = new();
            availabilityMask = receiveContent[2];
            for (int i = 3; i + 3 < receiveContent.Length; i += 4)
            {
                dtcInfos_list.Add(new()
                {
                    dtc = new byte[3] { receiveContent[i], receiveContent[i + 1], receiveContent[i + 2] },
                    status = receiveContent[i + 3],
                });
            }
            dtcInfos = dtcInfos_list.ToArray();

            if (dtcDescriptions != null)
            {
                for (int i = 0; i < dtcInfos.Length; i++)
                {
                    AddDtcDescription(dtcDescriptions, ref dtcInfos[i]);
                }
            }

            return true;
        }

        /// <summary>
        /// 给 DTC 列表添加描述信息
        /// </summary>
        /// <param name="dtcDescriptions"></param>
        /// <param name="dtcInfo"></param>
        public static void AddDtcDescription(ProductManager.Config.UdsDiagnosticLayerParams.DtcDescription[] dtcDescriptions, ref DtcInfo dtcInfo)
        {
            var DTC = dtcInfo.DTC_string;
            var l = dtcDescriptions.Where(d => d.DTC == DTC).ToList();
            if (l.Count() > 0)
            {
                dtcInfo.dtcDescription = l.First();
            }
        }

        #endregion
    }
}
