using EthDiagnosticTool.ProductManager.Config;
using log4net;
using log4net.Config;
using Newtonsoft.Json;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]


namespace EthDiagnosticTool
{
#if DEBUG
#if NET7_0_OR_GREATER
#else
    using System;
    using System.Xml;
    using System.Windows.Forms;
    using static FindDid.ImportDidSegment;
#endif
#endif
    internal static class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log.Info("程序打开。");
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            try
            {
#if NET7_0_OR_GREATER
                ApplicationConfiguration.Initialize();
#else
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
#endif

#if DEBUG
            var didsNode = GetDIDSElements();  // from default OLD|NEW  Cdd  File  ()

                //string oriRawToStringFilePath = @"C:\Users\Public\Documents\UdsDidTemplateConfig\ToStringDefaultPecu.cs";
                /*读取可动态编译程序旧模板*/string oriRawToStringFilePath = @"C:\Users\Public\Documents\UdsDidTemplateConfigNew\ToStringDefaultPecuNew.cs";

                //string destToStringFilePath = @"D:\Code2023(172.65.251.78)\Winform\UnitTestFrameJan28\TestToString.cs";
                /*UNIT-TEST-REPLACE-SCRIPT*/string destToStringFilePath = @"D:\Code2023(172.65.251.78)\Winform\UnitTestFrameJan28\TestToString.cs";
                //@@pri-folder@string destToStringFilePathMore = @"C:\Users\Public\Documents\ToString.cs";
                /*生成目标可动态编译文件*/string destToStringFilePathMoreToCopy = @"C:\Users\Public\Documents\UdsDidTemplateConfigNew\ToString.cs";

                bool rwCsv = false;
                /*读取按键配置旧模板*/string inputBtnCfgCsv = @"C:\Users\Public\Documents\UdsDidTemplateConfigNew\ReadDidButtonsConfigDefaultNew.csv";
                /*生成目标可动态编译文件相关的 按键配置 (UdsDiagnosticTool 1.0.3)*/string copyBtnCfgCsv = @"C:\Users\Public\Documents\UdsDidTemplateConfigNew\readDidButtonsConfig.csv";

                //@@pri-folder@
                /*生成目标可动态编译文件相关的 按键配置 (UdsDiagnosticTool 1.0.3) 公共文档路径下*/string copyBtnCfgCsvMore = @"C:\Users\Public\Documents\readDidButtonsConfig.csv";


                var list = CallDtDemo.DtDemoHelper.ReadWriteMethodWithDidList(CallDtDemo.DtDemoHelper.ReadDidList(), oriRawToStringFilePath, destToStringFilePath, destToStringFilePathMoreToCopy);

                rwCsv = CallDtDemo.DtDemoHelper.ReadWriteDidBtnCfgCsv(CallDtDemo.DtDemoHelper.ReadDidList(), inputBtnCfgCsv, copyBtnCfgCsv);
                rwCsv = CallDtDemo.DtDemoHelper.ReadWriteDidBtnCfgCsv(CallDtDemo.DtDemoHelper.ReadDidList(), inputBtnCfgCsv, copyBtnCfgCsvMore);

#endif
            return;
                Application.Run(new Forms.Demo());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                log.Fatal(ex);
            }
            log.Info("程序关闭。");
        }
        
    }
}