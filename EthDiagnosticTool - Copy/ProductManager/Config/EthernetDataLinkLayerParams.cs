using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace EthDiagnosticTool.ProductManager.Config
{
    public class EthernetDataLinkLayerParams
    {
        [JsonIgnore]
        public PhysicalAddress localPhy = new PhysicalAddress(new byte[6] { 0, 0, 0, 0, 0, 0 });

        [JsonProperty(propertyName: "本地 MAC 地址")]
        public string LocalMAC
        {
            get
            {
                return BitConverter.ToString(localPhy.GetAddressBytes()).Replace("-", ":");
            }
            set
            {
                localPhy = PhysicalAddress.Parse(value);
            }
        }

        [JsonIgnore]
        public PhysicalAddress remotePhy = new PhysicalAddress(new byte[6] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF });

        [JsonProperty(propertyName: "远端 MAC 地址")]
        public string RemoteMAC
        {
            get
            {
                return BitConverter.ToString(remotePhy.GetAddressBytes()).Replace("-", ":");
            }
            set
            {
                remotePhy = PhysicalAddress.Parse(value);
            }
        }

        [JsonProperty(propertyName: "Tagged")]
        public bool tagged = false;

        [JsonIgnore]
        private int vlanId = 1;

        [JsonProperty(propertyName: "VLAN ID")]
        public int VlanId
        {
            get
            {
                return vlanId;
            }
            set
            {
                value = Math.Max(0, value);
                value = Math.Min(4095, value);
                vlanId = value;
            }
        }
    }
}
