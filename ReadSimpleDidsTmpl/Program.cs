
using System.Reflection.Metadata.Ecma335;
using CoreLib.DS.DIDSGROUP.DIDS.Ctor;
using ReadSimple.ConfigNamespace;

namespace ReadSimple
{
    using System.Diagnostics;
    using System.Xml;
    using CoreLib.DS.DIDSGROUP.DIDS;
    
    
    namespace PecuDidsTmpl
    {
        
        using System.Threading;
        using System.Diagnostics;
        using System.Collections;
        using System.Collections.Generic;
        using System.Text;
        using System.Xml;
        using System.IO;
        using CoreLib;
        using CoreLib.Handler.XmlNodesElements;

        
        namespace PreStart
        {
            public static class Initializer
            {
                static Initializer()
                {
                    _didsXElementNode = DIDSElementNs;
                }
                
                
                #region dids-dict

                private static Dictionary<string, XmlElement?> DIDElementDict
                {
                    get
                    {
                        Dictionary<string, XmlElement?> tmpDict = new Dictionary<string, XmlElement?>();
                        XmlElement? didsElem = null;
                        try
                        {
                            if ((didsElem = DIDSElementNs) != null)
                            {
                                // search-did
                                if (didsElem?.ChildNodes != null)
                                {
                                    foreach (XmlElement? DIDElem in didsElem.ChildNodes)
                                    {
                                        if (DIDElem == null) continue;
                                        try
                                        {
                                            tmpDict.Add(
                                                Convert.ToInt32(DIDElem?.Attributes?["n"]?.Value ?? "").ToString("X").Substring(0,4),
                                                DIDElem);
                                        }
                                        catch
                                        {
                                            continue; //return null;;
                                        }
                                    }
                                    return tmpDict;
                                }
                            }
                            return null;
                        }
                        catch { return null; }
                    }
                }

                public static XmlNode? FindDIdNodeByHexN(string? didHexN)
                {
                    if (didHexN == null /*|| didHexN.Length >4*/) return null; // 4位 HexStr
                    DIDElementDict.TryGetValue(didHexN, out XmlElement? foundDidElem);
                    if (foundDidElem == null) return null;
                    else return (XmlNode)foundDidElem;
                }
                
                #endregion
                
                #region dids-node | ecudoc - node | candela - node

                public static XmlElement? DidsXElementNode => _didsXElementNode?.Clone() != null ? (XmlElement)(_didsXElementNode?.Clone()) : null;

                private static XmlElement? _didsXElementNode = null;
                private static XmlElement? DIDSElementNs
                {
                    get
                    {
                        try
                        {
                            var _dids = XElemReader.FindDids(
                                editableTempCddFile:
                                "C:\\Users\\Public\\Documents\\UdsDidTemplateConfig\\PECUZK.cdd.xml.cpy.Temp.xml");
                            return (XmlElement)_dids;
                        }
                        catch
                        {
                            throw;
                        }
                    }
                    
                }
                

                
                #endregion

            }
        }

        namespace FullTmplUsage
        {
            
        }
        
    }


    namespace SampleProgram
    {
        using ReadSimple;
        using ReadSimple.PecuDidsTmpl;
        using ReadSimple.PecuDidsTmpl.PreStart;
        
        
        public partial class Starter
        {

            static void Main(string[] argv)
            {
                try { MainIPM(argv); }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                try { MainPECU(argv); }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            
            #region PECU ~ IPM
            static void MainPECU(string[] arg)
            {
                 new Starter().main();
                 new Starter().mainGetDidsHexSetting(out List<string?>? getDidHexs);

                 List<BaseDid>? theDidObjs = null;
                 new Starter().mainConstructObjectOfDidByDidsHexSetting(out theDidObjs, getDidHexs);
                 
                 // read ToStr
                 string pathOfOldToStr = CfgSetting.OriRawToStringFilePath;
                 // string pathOfOldToStrMoreToCopy = CfgSetting.OriRawToStringFilePath; //DEBUG..
                 
                 // read BtnCfgCsv
                 string pathOfOldBtnCfgCsv = CfgSetting.OriRawBtnCfgCsv;
                 
                 // output ToStr
                 string outputsNewToStr = CfgSetting.DestToStringFileToCopy;
#if DEBUG
#if NET6_0
                 string outputsNewToStrMoreToCopy = CfgSetting.DEBUGOUTPUT_DestToStringFileToCopy;
#else                
#endif
#endif                
                 // output BtnCfgCsv
                 string outputsNewBtnCfgCsv = CfgSetting.DestBtnCfgCsv;
                 
                 // Exec full methods:
                 if(getDidHexs != null) new Starter().WriteTargetToStrFileWithDidList(getDidHexs, pathOfOldToStr, outputsNewToStr
#if DEBUG
#if NET6_0
                     , outputsNewToStrMoreToCopy
#else                
#endif
#endif
                 );
                 if (getDidHexs != null)
                     new Starter().WriteTargetBtnCfgCsvWithDidList(getDidHexs, pathOfOldBtnCfgCsv, outputsNewBtnCfgCsv);
                 ;

                 Console.ReadKey();
            }

            static void MainIPM(string[] arg)
            {
                new Starter().main(); //
                new Starter().mainGetDidsHexSettingIPM(out List<string?>? getDidHexs); // IPM | PECU

                List<BaseDid>? theDidObjs = null;
                new Starter().mainConstructObjectOfDidByDidsHexSetting(out theDidObjs, getDidHexs);
                 
                // read ToStr
                string pathOfOldToStr = ConfigIPMNamespace.IPMCfgSetting.OriRawToStringFilePath;
                // string pathOfOldToStrMoreToCopy = CfgSetting.OriRawToStringFilePath; //DEBUG..
                 
                // read BtnCfgCsv
                string pathOfOldBtnCfgCsv = ConfigIPMNamespace.IPMCfgSetting.OriRawBtnCfgCsv;
                 
                // output ToStr
                string outputsNewToStr = ConfigIPMNamespace.IPMCfgSetting.DestToStringFileToCopy;
#if DEBUG
#if NET6_0
                string outputsNewToStrMoreToCopy = ConfigIPMNamespace.IPMCfgSetting.DEBUGOUTPUT_DestToStringFileToCopy;
#else                
#endif
#endif                
                // output BtnCfgCsv
                string outputsNewBtnCfgCsv = ConfigIPMNamespace.IPMCfgSetting.DestBtnCfgCsv;
                 
                // Exec full methods:
                if(getDidHexs != null) new Starter().WriteTargetToStrFileWithDidList(getDidHexs, pathOfOldToStr, outputsNewToStr
#if DEBUG
#if NET6_0
                    , outputsNewToStrMoreToCopy
#else                
#endif
#endif
                );
                if (getDidHexs != null)
                    new Starter().WriteTargetBtnCfgCsvWithDidList(getDidHexs, pathOfOldBtnCfgCsv, outputsNewBtnCfgCsv);
                ;

                Console.ReadKey();
            }

            #endregion
            
            
            #region  OTHER

            void main() { Debug.WriteLine(Initializer.DidsXElementNode?.ChildNodes?.Count);}
            void mainGetDidsHexSetting(out List<string?>? didsHexList) { Debug.WriteLine(@$"{"ToGetUsableDidCount: "}" + ConfigNamespace.CfgSetting.ReadDidHexSettingsFromExternalCsv(out didsHexList));}
            void mainGetDidsHexSettingIPM(out List<string?>? didsHexList) { Debug.WriteLine(@$"{"ToGetUsableDidCount: "}" + ConfigIPMNamespace.IPMCfgSetting.ReadDidHexSettingsFromExternalCsv(out didsHexList));}
            void mainConstructObjectOfDidByDidsHexSetting(out List<BaseDid>? bDidObjs, List<string?>? inputDidHexs)
            {
                bDidObjs = null;
                if (inputDidHexs == null || inputDidHexs?.Count < 1) return;
                try
                {
                    bDidObjs = new List<BaseDid>();
                    foreach (string inputDidHex in inputDidHexs)
                    {
                        try
                        {
                            XmlNode? tDid = Initializer.FindDIdNodeByHexN(inputDidHex);
                            if (tDid == null) continue; // TODO: Try-Throw exception here
                            bDidObjs.Add(new DidImpl(tDid));
                        }
                        catch{ continue;}
                    }
                }
                catch { throw; }
            }

            #endregion
            

        }
        
    }
    
}