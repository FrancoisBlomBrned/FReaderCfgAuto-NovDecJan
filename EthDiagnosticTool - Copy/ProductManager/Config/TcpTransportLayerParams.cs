using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EthDiagnosticTool.ProductManager.Config
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TcpTransportLayerParams
    {
        public enum TcpType
        {
            /// <summary>
            /// 被动模式（作为服务端）
            /// </summary>
            Server,

            /// <summary>
            /// 主动模式（作为客户端）
            /// </summary>
            Client,
        }

        [JsonIgnore]
        public TcpType connectMode = TcpType.Server;

        [JsonProperty(propertyName: "连接模式")]
        public string ConnectMode
        {
            get
            {
                return connectMode.ToString();
            }
            set
            {
                connectMode = (TcpType)Enum.Parse(typeof(TcpType), value);
            }
        }

        [JsonProperty(propertyName: "IP 参数")]
        public IpNetworkLayerParams ipNetworkLayerParams = new IpNetworkLayerParams();

        [JsonProperty(propertyName: "本地端口号")]
        public ushort localPort = 13400;

        [JsonProperty(propertyName: "远端端口号")]
        public ushort remotePort = 13400;
    }
}
