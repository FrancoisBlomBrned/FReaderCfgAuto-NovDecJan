using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthDiagnosticTool.ProductManager.Config
{
    [JsonObject(MemberSerialization.OptOut)]
    public class UdsTransportLayerParams
    {
        public enum UdsTransportLayerType
        {
            None,
            DoCAN,
            DoIP
        }

        [JsonIgnore]
        public UdsTransportLayerType transportLayerType;

        [JsonProperty(propertyName: "传输层协议类型")]
        public string TransportLayerTypeString
        {
            get
            {
                return transportLayerType.ToString();
            }
            set
            {
                transportLayerType = (UdsTransportLayerType)Enum.Parse(typeof(UdsTransportLayerType), value);
            }
        }

        private UdsTransportLayerParams_DoIP doIP;

        /// <summary>
        /// DoIP 传输层参数
        /// </summary>
        public UdsTransportLayerParams_DoIP DoIP
        {
            get
            {
                return doIP;
            }
            set
            {
                doIP = value;
            }
        }

        private UdsTransportLayerParams_DoCAN doCAN;

        /// <summary>
        /// DoCAN 传输层参数
        /// </summary>
        public UdsTransportLayerParams_DoCAN DoCAN
        {
            get
            {
                return doCAN;
            }
            set
            {
                doCAN = value;
            }
        }

        public UdsTransportLayerParams()
        {
            transportLayerType = UdsTransportLayerType.None;
            doIP = new UdsTransportLayerParams_DoIP();
            doCAN = new UdsTransportLayerParams_DoCAN();
        }
    }
}
