using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace EthDiagnosticTool.ProductManager.Config
{
    public class DidButtonConfig
    {
        public string Name { get; set; }

        public string DID { get; set; }

        [Ignore]
        public byte[] did
        {
            get
            {
                var didUshort = Convert.ToUInt16(DID, 16);
                return BitConverter.GetBytes(didUshort).Reverse().ToArray();
            }
        }

        public string Session { get; set; }

        public string SecurityLevel { get; set; }

        public string Mapping { get; set; }

        public DidButtonConfig()
        {
            Name = "NONAME";
            DID = "0x0000";
            Session = "0x01";
            SecurityLevel = "0x00";
            Mapping = "HEX";
        }
    }
}
