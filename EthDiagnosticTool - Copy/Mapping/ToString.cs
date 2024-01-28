using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EthDiagnosticTool.UDS.StandardServers.Sid_0x19;

namespace EthDiagnosticTool.Mapping
{
    /// <summary>
    /// UDS 包内容直接传入，输出为可读内容。
    /// </summary>
    public static class ToString
    {
        public delegate string ToStringDelegate(byte[] content, params object[] objects);

        public static string HEX(byte[] content, params object[] argvs)
        {
            return BitConverter.ToString(content);
        }

        public static string ASCII(byte[] content, params object[] argvs)
        {
            return Encoding.ASCII.GetString(content);
        }

        public static string BIN(byte[] content, params object[] argvs)
        {
            var binList = new List<string>();
            foreach(var b in content)
            {
                binList.Add(Convert.ToString(b, 2).PadLeft(8, '0'));
            }
            return string.Join(" ", binList);
        }

        public static string VERSION(byte[] content, params object[] argvs)
        {
            if (content.Length <= 4)
            {
                uint sum = 0;
                for (int i = 0; i < content.Length; i++)
                {
                    sum += (uint)(content[i] << (8 * (content.Length - i - 1)));
                }
                return sum.ToString();
            }
            else
            {
                return string.Join(".", content);
            }
        }

        public static string DEC(byte[] content, params object[] argvs)
        {
            ulong t = 0;
            for (int i = 0; i < content.Length; i++)
            {
                t += (ulong)content[i] << ((content.Length - 1 - i) * 8);
            }
            return t.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static string ReportNumberOfDtc(byte[] data, params object[] objects)
        {
            byte availabilityMask = data[2];
            byte formatIdentifier = data[3];
            ushort dtcCount = (ushort)(data[4] << 8 | data[5]);
            string info = "";
            info += $"支持的 DTC 掩码：0x{availabilityMask:X2}\r\n";
            if (formatIdentifier == 0x01)
            {
                info += $"当前的 DTC 格式：ISO-14229-1\r\n";
            }
            else if (formatIdentifier == 0x02)
            {
                info += $"当前的 DTC 格式：J1939\r\n";
            }
            else
            {
                info += $"当前的 DTC 格式：{formatIdentifier:X2}\r\n";
            }
            info += $"DTC 数量：{dtcCount}";
            return info;
        }

        /// <summary>
        /// 构造 DTC 报告信息。
        /// </summary>
        /// <param name="data">仅 DTC + status</param>
        /// <param name="objects"></param>
        /// <returns></returns>
        public static string ReportDtcInfomation(byte[] data, params object[] objects)
        {
            var dtcDescriptions = UDS.Server.udsDiagnosticLayerParams.dtcDescriptions;

            string info = "";
            for (int i = 0; i < data.Length / 4; i++)
            {
                var dtcHexCodeString = data[i * 4 + 3].ToString("X2") + data[i * 4 + 4].ToString("X2") + data[i * 4 + 5].ToString("X2");
                var dtcStatusCode = data[i * 4 + 6];
                info += "\r\n";
                info += $"DTC Code: {dtcHexCodeString}\r\n";
                var s = (from d in dtcDescriptions where d.DTC == dtcHexCodeString select d).ToArray();
                if (s.Length > 0)
                {
                    info += string.Format("DTC Name: {0}\r\n", s[0].FaultName);
                    info += string.Format("DTC Description: {0}\r\n", s[0].Description);
                }
                else
                {
                    info += string.Format("DTC Name: {0}\r\n", "UNKNOW");
                    info += string.Format("DTC Description: {0}\r\n", "UNKNOWN");
                }
                info += $"DTC Status: {dtcStatusCode.ToString("X2")}\r\n";
                var lines = UDS.StandardServers.Sid_0x19.GetDtcStatus((UDS.StandardServers.Sid_0x19.StatusFlags)dtcStatusCode);
                info += string.Join("\n", lines);
                info += "\r\n";
            }
            return info;
        }
    }
}
