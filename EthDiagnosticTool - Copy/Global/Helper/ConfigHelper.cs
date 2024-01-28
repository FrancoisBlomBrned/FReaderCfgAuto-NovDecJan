using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using static EthDiagnosticTool.Global.Helper.ConfigHelper;
using static EthDiagnosticTool.Global.Helper.IniHelper;
using EthDiagnosticTool.ProductManager.Config;

namespace EthDiagnosticTool.Global.Helper
{
    public static class ConfigHelper
    {
        /// <summary>
        /// 《加载产品配置文件》的绝对路径。
        /// 将从设置文件的参数中获取的相对路径或绝对路径转为绝对路径。
        /// </summary>
        public static string ProductConfigFilePath
        {
            get
            {
                return Path.GetFullPath(Properties.Settings.Default.ProductConfigFilePath);
            }
        }

        public static string ProductConfigDirectoryPath
        {
            get
            {
                return Path.GetFullPath(Path.GetDirectoryName(ProductConfigFilePath) ?? "");
            }
        }

        /// <summary>
        /// 《加载产品配置文件》的产品信息
        /// </summary>
        public struct ProductBasicInfo
        {
            /// <summary>
            /// 产品名称
            /// </summary>
            [Index(0)]
            public string Name { get; set; }

            /// <summary>
            /// 配置文件夹的相对路径，或绝对路径
            /// </summary>
            [Index(1)]
            public string DirectoryPath { get; set; }
        }

        /// <summary>
        /// 读取《加载产品配置文件》
        /// </summary>
        /// <returns></returns>
        public static ProductBasicInfo[] ReadProductList()
        {
            return FileHelper.ReadFromFile<ProductBasicInfo>(ProductConfigFilePath);
        }

        /// <summary>
        /// 写入《加载产品配置文件》
        /// </summary>
        /// <param name="productList"></param>
        public static void WriteProductList(ProductBasicInfo[] productList)
        {
            FileHelper.WriteToFile(ProductConfigFilePath, productList);
        }

        /// <summary>
        /// 产品的详细配置信息
        /// </summary>
        public struct ProductConfig
        {
            /// <summary>
            /// 《加载产品配置文件》的信息
            /// </summary>
            public ProductBasicInfo productBasicInfo;

            /// <summary>
            /// 产品配置文件
            /// </summary>
            public IniHelper.IniFile iniFile;

            public class UdsSection
            {
                public static string SectionName
                {
                    get
                    {
                        return ProductIniFileSetting.Default.SectionName_UDS;
                    }
                }

                public IniHelper.IniFile iniFile;

                /// <summary>
                /// UDS 诊断层参数
                /// </summary>
                public UdsDiagnosticLayerParams udsDiagnosticLayerParams;

                /// <summary>
                /// UDS 传输层参数
                /// </summary>
                public UdsTransportLayerParams udsTransportLayerParams;

                public DidButtonConfig[] readDidButtonConfig;

                public DidButtonConfig[] writeDidButtonConfig;


                #region 构造
                public UdsSection()
                {
                    iniFile = new IniHelper.IniFile();
                    udsDiagnosticLayerParams = new UdsDiagnosticLayerParams();
                    udsTransportLayerParams = new UdsTransportLayerParams();
                    readDidButtonConfig = Array.Empty<DidButtonConfig>();
                    writeDidButtonConfig = Array.Empty<DidButtonConfig>();
                }

                public UdsSection(IniHelper.IniFile iniFile) : this()
                {
                    this.iniFile = iniFile;
                }

                public void GetReady()
                {
                    udsDiagnosticLayerParams = ReadUdsDiagnosticLayerParamsFromIniFile(iniFile);
                    udsDiagnosticLayerParams.LoadDtcDescriptions();
                    udsTransportLayerParams = ReadUdsTransportLayerParamsFromIniFile(iniFile);
                    readDidButtonConfig = ReadReadDidButtonConfigFromIniFile(iniFile);
                    writeDidButtonConfig = ReadWriteDidButtonConfigFromIniFile(iniFile);
                }

                #endregion

                public static T[] ReadIniCsvObject<T>(IniHelper.IniFile iniFile, string sectionName, string sectionKey, string sectionValue)
                    where T : new()
                {
                    var iniSections = iniFile.content;

                    if (!iniSections.ContainsKey(sectionName))
                    {
                        iniSections[sectionName] = new Dictionary<string, string>();
                    }
                    if (!iniSections[sectionName].ContainsKey(sectionKey))
                    {
                        iniSections[sectionName][sectionKey] = sectionValue;  // 默认值
                    }

                    var filePath = Path.Combine(iniFile.DirectoryPath, iniSections[sectionName][sectionKey]);
                    var ts = FileHelper.ReadFromFile<T>(filePath);
                    WriteIniCsvObject(iniFile, sectionName, sectionKey, sectionValue, ts);
                    return ts;
                }

                public static void WriteIniCsvObject<T>(IniHelper.IniFile iniFile, string sectionName, string sectionKey, string sectionValue, T[] csvObj)
                {
                    var iniSections = iniFile.content;

                    if (!iniSections.ContainsKey(sectionName))
                    {
                        iniSections[sectionName] = new Dictionary<string, string>();
                    }
                    if (!iniSections[sectionName].ContainsKey(sectionKey))
                    {
                        iniSections[sectionName][sectionKey] = sectionValue;  // 默认值
                    }

                    var filePath = Path.Combine(iniFile.DirectoryPath, iniSections[sectionName][sectionKey]);

                    FileHelper.WriteToFile(filePath, csvObj);
                }

                public static T ReadIniJsonObject<T>(IniHelper.IniFile iniFile, string sectionName, string sectionKey, string sectionValue)
                    where T : new()
                {
                    var iniSections = iniFile.content;

                    if (!iniSections.ContainsKey(sectionName))
                    {
                        iniSections[sectionName] = new Dictionary<string, string>();
                    }
                    if (!iniSections[sectionName].ContainsKey(sectionKey))
                    {
                        iniSections[sectionName][sectionKey] = sectionValue;  // 默认值
                    }

                    var filePath = Path.Combine(iniFile.DirectoryPath, iniSections[sectionName][sectionKey]);
                    string fileContent = FileHelper.ReadFromFile(filePath);
                    var jsonObj = JsonConvert.DeserializeObject<T>(fileContent);
                    if (jsonObj == null)
                    {
                        jsonObj = new T();
                    }
                    WriteIniJsonObject(iniFile, sectionName, sectionKey, sectionValue, jsonObj);
                    return jsonObj;
                }

                public static void WriteIniJsonObject<T>(IniHelper.IniFile iniFile, string sectionName, string sectionKey, string sectionValue, T jsonObj)
                    where T : new()
                {
                    var iniSections = iniFile.content;

                    if (!iniSections.ContainsKey(sectionName))
                    {
                        iniSections[sectionName] = new Dictionary<string, string>();
                    }
                    if (!iniSections[sectionName].ContainsKey(sectionKey))
                    {
                        iniSections[sectionName][sectionKey] = sectionValue;  // 默认值
                    }

                    var filePath = Path.Combine(iniFile.DirectoryPath, iniSections[sectionName][sectionKey]);

                    string fileContent = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                    FileHelper.WriteToFile(filePath, fileContent);
                }


                #region UdsDiagnosticLayerParams

                public static string UdsDiagnosticLayerParamsKey
                {
                    get
                    {
                        return ProductIniFileSetting.Default.SectionKeyName_UDS_diagnosticLayerParamsFile;
                    }
                }

                public static string UdsDiagnosticLayerParamsValue
                {
                    get
                    {
                        return ProductIniFileSetting.Default.SectionKeyValue_UDS_diagnosticLayerParamsFile;
                    }
                }


                /// <summary>
                /// 从产品的配置文件中读取 UdsDiagnosticLayerParams。
                /// 如果文件异常，则创建默认的内容。
                /// </summary>
                /// <param name="iniFile"></param>
                /// <returns></returns>
                public static UdsDiagnosticLayerParams ReadUdsDiagnosticLayerParamsFromIniFile(IniHelper.IniFile iniFile)
                {
                    var udsDiagnosticLayerParams = ReadIniJsonObject<UdsDiagnosticLayerParams>(iniFile, SectionName, UdsDiagnosticLayerParamsKey, UdsDiagnosticLayerParamsValue);
                    udsDiagnosticLayerParams.directoryPath = iniFile.DirectoryPath;
                    // udsDiagnosticLayerParams.LoadDtcDescriptions();
                    return udsDiagnosticLayerParams;
                }

                /// <summary>
                /// 把 UdsDiagnosticLayerParams 写入产品的配置文件
                /// </summary>
                /// <param name="iniFile"></param>
                /// <param name="udsDiagnosticLayerParams"></param>
                public static void WriteUdsDiagnosticLayerParamsToIniFile(IniHelper.IniFile iniFile, UdsDiagnosticLayerParams udsDiagnosticLayerParams)
                {
                    WriteIniJsonObject(iniFile, SectionName, UdsDiagnosticLayerParamsKey, UdsDiagnosticLayerParamsValue, udsDiagnosticLayerParams);
                }

                #endregion

                #region UdsTransportLayerParams

                public static string UdsTransportLayerParamsKey
                {
                    get
                    {
                        return ProductIniFileSetting.Default.SectionKeyName_UDS_transportLayerParamsFile;
                    }
                }

                public static string UdsTransportLayerParamsValue
                {
                    get
                    {
                        return ProductIniFileSetting.Default.SectionKeyValue_UDS_transportLayerParamsFile;
                    }
                }

                public static UdsTransportLayerParams ReadUdsTransportLayerParamsFromIniFile(IniHelper.IniFile iniFile)
                {
                    return ReadIniJsonObject<UdsTransportLayerParams>(iniFile, SectionName, UdsTransportLayerParamsKey, UdsTransportLayerParamsValue);
                }

                public static void WriteUdsTransportLayerParamsToIniFile(IniHelper.IniFile iniFile, UdsTransportLayerParams udsTransportLayerParams)
                {
                    WriteIniJsonObject(iniFile, SectionName, UdsTransportLayerParamsKey, UdsTransportLayerParamsValue, udsTransportLayerParams);
                }

                #endregion

                #region ReadDidButtonConfig

                public static string ReadDidButtonConfigKey
                {
                    get
                    {
                        return ProductIniFileSetting.Default.SectionKeyName_UDS_readDidButtonsConfigFile;
                    }
                }

                public static string ReadDidButtonConfigValue
                {
                    get
                    {
                        return ProductIniFileSetting.Default.SectionKeyValue_UDS_readDidButtonsConfigFile;
                    }
                }

                public static DidButtonConfig[] ReadReadDidButtonConfigFromIniFile(IniHelper.IniFile iniFile)
                {
                    return ReadIniCsvObject<DidButtonConfig>(iniFile, SectionName, ReadDidButtonConfigKey, ReadDidButtonConfigValue);
                }

                public static void WriteReadDidButtonConfigToIniFile(IniHelper.IniFile iniFile, DidButtonConfig[] didButtons)
                {
                    WriteIniCsvObject(iniFile, SectionName, ReadDidButtonConfigKey, ReadDidButtonConfigValue, didButtons);
                }

                #endregion

                #region WriteDidButtonConfig

                public static string WriteDidButtonConfigKey
                {
                    get
                    {
                        return ProductIniFileSetting.Default.SectionKeyName_UDS_writeDidButtonsConfigFile;
                    }
                }

                public static string WriteDidButtonConfigValue
                {
                    get
                    {
                        return ProductIniFileSetting.Default.SectionKeyValue_UDS_writeDidButtonsConfigFile;
                    }
                }

                public static DidButtonConfig[] ReadWriteDidButtonConfigFromIniFile(IniHelper.IniFile iniFile)
                {
                    return ReadIniCsvObject<DidButtonConfig>(iniFile, SectionName, WriteDidButtonConfigKey, WriteDidButtonConfigValue);
                }

                public static void WriteWriteDidButtonConfigToIniFile(IniHelper.IniFile iniFile, DidButtonConfig[] didButtons)
                {
                    WriteIniCsvObject(iniFile, SectionName, WriteDidButtonConfigKey, WriteDidButtonConfigValue, didButtons);
                }

                #endregion
            }

            public UdsSection udsSection;

            public ProductConfig(ProductBasicInfo productBasicInfo)
            {
                var productDirectoryPath = PathHelper.GetRootPath(productBasicInfo.DirectoryPath, ProductConfigDirectoryPath);  // 从《加载产品配置文件》的路径中获取绝对路径
                string iniFilePath = PathHelper.GetRootPath(ProductIniFileSetting.Default.ProductIniFileName, productDirectoryPath);  // 产品配置文件的绝对路径

                var iniFile = new IniHelper.IniFile(iniFilePath);
                var udsSection = new UdsSection(iniFile);
                iniFile.WriteContent();

                this.productBasicInfo = productBasicInfo;
                this.iniFile = iniFile;
                this.udsSection = udsSection;
            }

            public void SaveProductConfig()
            {

            }
        }

        /// <summary>
        /// 所有可用的产品的详细配置信息。
        /// 从《加载产品配置文件》列表中加载的产品。
        /// </summary>
        public static ProductConfig[] productConfigs = Array.Empty<ProductConfig>();

        /// <summary>
        /// 从《加载产品配置文件》读取产品清单并提取信息到 productConfigs
        /// </summary>
        public static void RefreshProductList()
        {
            var productList = ReadProductList();  // 从文件中读取产品清单
            var products = new List<ProductConfig>();
            for (int i = 0; i < productList.Length; i++)
            {
                try
                {
                    var productConfig = new ProductConfig(productList[i]);
                    products.Add(productConfig);
                }
                catch (ArgumentNullException)
                {
                    continue;
                }
            }
            productConfigs = products.ToArray();
        }



    }
}
