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
    public class IpNetworkLayerParams
    {
        [JsonProperty(propertyName: "以太网参数")]
        public EthernetDataLinkLayerParams ethernetDataLinkLayerParams = new EthernetDataLinkLayerParams();

        [JsonIgnore]
        public IPAddress localIP = IPAddress.Any;

        [JsonProperty(propertyName: "本地 IP 地址")]
        public string LocalIP
        {
            get
            {
                return localIP.ToString();
            }
            set
            {
                localIP = IPAddress.Parse(value);
            }
        }

        [JsonIgnore]
        public IPAddress localMask = IPAddress.Parse("255.255.0.0");

        [JsonProperty(propertyName: "本地 IP 掩码")]
        public string LocalMask
        {
            get
            {
                return localMask.ToString();
            }
            set
            {
                localMask = IPAddress.Parse(value);
            }
        }

        [JsonIgnore]
        public IPAddress remoteIP = IPAddress.Broadcast;

        [JsonProperty(propertyName: "远端 IP 地址")]
        public string RemoteIP
        {
            get
            {
                return remoteIP.ToString();
            }
            set
            {
                remoteIP = IPAddress.Parse(value);
            }
        }

        [JsonIgnore]
        public IPAddress remoteMask = IPAddress.Parse("255.255.0.0");

        [JsonProperty(propertyName: "远端 IP 掩码")]
        public string RemoteMask
        {
            get
            {
                return remoteMask.ToString();
            }
            set
            {
                remoteMask = IPAddress.Parse(value);
            }
        }
    }
}
