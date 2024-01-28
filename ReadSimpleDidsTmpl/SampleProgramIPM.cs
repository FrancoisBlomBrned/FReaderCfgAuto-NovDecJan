

namespace ReadSimple.SampleProgram
{
    using ReadSimple;
    using ReadSimple.PecuDidsTmpl;
    using ReadSimple.PecuDidsTmpl.PreStart;
    using System.Xml;
    using System.Threading;
    using System.Diagnostics;
    using CoreLib.DS.DIDSGROUP.DIDS;
    using CoreLib.DS.DIDSGROUP.DIDS.Ctor;
    using CoreLib.Handler.XmlNodesElements;
    using CoreLib.DS.DATATYPES.VARVALUE.VARIDENT;
    using static CoreLib.DS.DATATYPES.VARVALUE.VARIDENT.UtilFun;
    using CoreLib.DS.DATATYPES.VARVALUE.VARIDENT.Ctor;
    using CoreLib.DS.DATATYPES.COMPLEX.StructComplex.Ctor;
    using CoreLib.DS.DATATYPES.COMMONSTRUCTURE.COMMONOBJ.BaseDataobj.Ctor;
    
        
}


namespace ReadSimple
{
    
    namespace ConfigIPMNamespace
    {

        public static class IPMCfgSetting
        {
            
            // from one file read inputPecuToStringTmplCsFile
            /*读取可动态编译程序旧模板*/public const String OriRawToStringFilePath = @"C:\Users\Public\Documents\UdsDidTemplateConfigDest\InputDefaultIPMOldToString_IPM.cs";
            
            // from one csv read inputPecuBtnCfgCsvContentFile
            /*读取按键配置旧模板*/public const String  OriRawBtnCfgCsv = @"C:\Users\Public\Documents\UdsDidTemplateConfigDest\InputDefaultIPMOldDidButtonsConfig_IPM.csv";
            
            // 配置哪些 Did ($22(__)) 需要被加入到最终的生成 cs / csv 文件中
            public const String DidHexSettingsFilePathDefault = @"C:\Users\Public\Documents\didItemSettingsForIPM.csv";
            //public const String DidHexSettingsFilePathDefault = @"C:\Users\Public\Documents\";
            
            // create toStr file
            /*生成目标可动态编译文件*/public const String DestToStringFileToCopy = @"C:\Users\Public\Documents\UdsDidTemplateConfigDest\ToString_IPM.cs";
            
            
            public static string DEBUGOUTPUT_DestToStringFileToCopy = @"D:\Code2023(172.65.251.78)\Winform\UnitTestFrameJan28\TestToString_IPM.cs";
            
            
            // create readDidBtn file
            /*生成目标可动态编译文件相关的 按键配置 (UdsDiagnosticTool 1.0.3)*/public const String DestBtnCfgCsv = @"C:\Users\Public\Documents\UdsDidTemplateConfigDest\readDidButtonsConfig_IPM.csv";
            
            internal static int ReadDidHexSettingsFromExternalCsv(out List<string?>? didsExternal, string? didHexSettingsFilePath = null)
            {
                if (didHexSettingsFilePath == null) didHexSettingsFilePath = DidHexSettingsFilePathDefault;
                /**if (!File.Exists(didHexSettingsFilePath)) {
                    didsExternal = null;
                    return -1;
                }*/

                try
                {
                    didsExternal = new List<string?>();

                    System.Diagnostics.Debug.WriteLine(TempInit(ref didsExternal)); //init...

                    return didsExternal?.Count ?? -1;
                }

                catch { throw; }
            }

            private static bool TempInit(ref List<string?>? didsWithHexSrSymbol)
            {
                if (didsWithHexSrSymbol == null) return false;

                try
                {
                    foreach (var VARIABLE in new string[16]
                             {
                                 "FD20", "FE13",
                                 "FD18", "FD16",
                                 "D103", "F199",
                                 "F12A", "F18C",
                                 "", "",
                                 
                                 "F1AE", "F18A",
                                 //"F1AB", "D0B4",
                                 "FE10", "F197",
                                 //"F196", "F195",
                                 "F198", "F19A",
                             }
                    )
                    {
                        didsWithHexSrSymbol.Add(VARIABLE);
                    }

                    return true;
                }
                catch
                {
                    throw; 
                }

            }
            
        }
        
    }
    
}



