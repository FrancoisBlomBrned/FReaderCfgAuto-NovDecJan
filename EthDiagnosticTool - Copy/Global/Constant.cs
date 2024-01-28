using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthDiagnosticTool.Global
{
    using System.IO;
    using System.Windows.Forms;
    public static class Constant
    {
        public static string ApplicationVersion
        {
            get
            {
#if DEBUG
                return $"Debug v{Application.ProductVersion}";
#else
                return $"Release v{Application.ProductVersion}";
#endif
            }
        }
    }
}
