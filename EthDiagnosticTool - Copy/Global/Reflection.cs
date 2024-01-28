using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EthDiagnosticTool.Global
{
    public static class Reflection
    {
        public static System.Reflection.Assembly assembly;

        static Reflection()
        {
            assembly = System.Reflection.Assembly.GetExecutingAssembly();
        }
    }
}
