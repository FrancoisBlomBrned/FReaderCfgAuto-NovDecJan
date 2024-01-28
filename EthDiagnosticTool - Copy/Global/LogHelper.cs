using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;



namespace EthDiagnosticTool.Global
{
    using System.IO;
    public static class LogHelper
    {
        public enum LogLevel
        {
            All,
            Debug,
            Info,
            Warning,
            Error,
            Fatal,
            None
        }

        public delegate void AddLog(string message, LogLevel level = LogLevel.Info);

        public static readonly ILog log = LogManager.GetLogger(typeof(LogHelper));

        public static void LoadConfig()
        {
            XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4Net.config")));
        }
    }
}
