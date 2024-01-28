using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;

namespace EthDiagnosticTool.ProductManager.Config
{
    [JsonObject(MemberSerialization.OptOut)]
    public class UdsTransportLayerParams_DoIP
    {
        [JsonProperty(propertyName: "TCP 参数")]
        public TcpTransportLayerParams tcpTransportLayerParams = new TcpTransportLayerParams();

        [JsonIgnore]
        public byte version = 2;

        [JsonProperty(propertyName: "DoIP Version")]
        public string Version
        {
            get
            {
                return version.ToString("X2");
            }
            set
            {
                version = Convert.ToByte(value, 16);
            }
        }

        [JsonIgnore]
        public ushort localLA = 0x0E80;

        [JsonProperty(propertyName: "本地逻辑地址")]
        public string LocalLA
        {
            get
            {
                return localLA.ToString("X4");
            }
            set
            {
                localLA = Convert.ToUInt16(value, 16);
            }
        }

        [JsonIgnore]
        public ushort remoteLA = 0x1015;

        [JsonProperty(propertyName: "远端逻辑地址")]
        public string RemoteLA
        {
            get
            {
                return remoteLA.ToString("X4");
            }
            set
            {
                remoteLA = Convert.ToUInt16(value, 16);
            }
        }


        public UdsTransportLayerParams_DoIP()
        {
            
        }
    }
}
