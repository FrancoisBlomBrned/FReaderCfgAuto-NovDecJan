

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
    using CoreLib.DS.DATATYPES.VARVALUE.VARIDENT;
    using static CoreLib.DS.DATATYPES.VARVALUE.VARIDENT.UtilFun;
    using CoreLib.DS.DATATYPES.VARVALUE.VARIDENT.Ctor;
    using CoreLib.DS.DATATYPES.COMPLEX.StructComplex.Ctor;
    using CoreLib.DS.DATATYPES.COMMONSTRUCTURE.COMMONOBJ.BaseDataobj.Ctor;
    
    public partial class Starter
    {
        
#if NET8_0_OR_GREATER
#else
        /// <summary>
        ///  根据 didObject 构建出每一个 structure -> dataobj 的 Dtref 指向的细分的数据结构（IDENT |）的抽象列表
        /// </summary>
        /// <out_param name="aIdentXObjList"></out_param>
        /// <param name="didXObj"></param>
        internal static void ConstructAIdentListForOneDid(out List<AIdent?>? aIdentXObjList, BaseDid? didXObj)
        {
            aIdentXObjList = null;
            if (didXObj == null) return;


            try
            {
                aIdentXObjList = new List<AIdent?>();
                List<AIdent> varIdentsList = new List<AIdent>();
                string? qualDescrpDid = didXObj?.QualInnerObj?.Text;
                // qual Text 异常， 则返回 Null
                if (qualDescrpDid == null) return;

                // find dataobjs
                var structInnerdataObjEnumeratorExtends = didXObj?.StructureInner?.GetStructInnerEnumer(); ; // TODO: output debug info.. ( Structure -> Struct ？)

                if (structInnerdataObjEnumeratorExtends == null) ;//continue;
                else
                {
                    foreach (var varDt in structInnerdataObjEnumeratorExtends) // // 对于每一个 Dataobj， 
                    {
                        if (varDt == null) continue;     //

                        string? tIdKey = varDt?.MainSearchKey; 
                        if (tIdKey == null) continue;
                        XmlNode? identDatatypeNode = GetDatatypeNodeById(tIdKey); // 根据 Dataobj 指向的 IdKey 寻找 Datatype (IDENT)
                        if (identDatatypeNode == null) continue;

                        StackFrame? sf = new StackTrace(new StackFrame(true)).GetFrame(0);
                        AIdent? aIdent = null;
                        ConstructIdent((StructComplexImpl)varDt, out aIdent, (XmlElement)identDatatypeNode);

                        Debug.Write($"(IDENT STRUCT -- )Dynamic Debugging at Row({sf?.GetFileLineNumber()} Col({sf?.GetFileColumnNumber()})"
                            + $" in File {sf?.GetFileName()} when Exec METHOD [{sf?.GetMethod()?.Name}]\n"
                            + $"[....(输出)....\n");
                        Debug.WriteLine($" [({didXObj?.HexN}){qualDescrpDid}]  (&*  ({aIdent?.QualInnerObj?.Text ?? ""} _{aIdent?.MainKey}_ENC_{aIdent?.CEncStr}_DF_{aIdent?.CDfStr}_({aIdent?.GetCValBytesCnt}-Byte)  *&)" + "..] \n\n");
                        if (aIdent != null) varIdentsList.Add(aIdent);
                    }

                    aIdentXObjList = varIdentsList.ToArray().ToList<AIdent?>();
                }

            }
            catch { throw; }


            try
            {
                ConstructAIdentListForOneDidInternalExtend(ref aIdentXObjList, didXObj);

            }
            catch { throw; }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <ref_param name="aIdentXObjList"></ref_param>
        /// <param name="didXObj"></param>
        private static void ConstructAIdentListForOneDidInternalExtend(ref List<AIdent?>? aIdentXObjList, BaseDid? didXObj)
        {
            if(aIdentXObjList == null) return;  // 默认 Structure 必须优先于 Struct 来解析数据
            if (didXObj == null) return;


            try
            {
                List<AIdent> varIdentsList = new List<AIdent>();
                string? qualDescrpDid = didXObj?.QualInnerObj?.Text;


                // qual Text 异常， 则返回 Null
                if (qualDescrpDid == null) return;

                // Find dataobjs
                var structureInnerdataObjEnumerator = didXObj?.StructureInner?.GetDataobjInstanceEnemerator(); // TODO: output debug info.. (总共有多少一级 Structure -> Dataobj ？)

                if (structureInnerdataObjEnumerator == null) ;//continue;
                else
                {
                    foreach (var varDtDataObj in structureInnerdataObjEnumerator) // // 对于每一个 Dataobj， 
                    {
                        if (varDtDataObj == null) continue;

                        // 对于每一个 Dataobj，  构建 

                        string? tIdKey = varDtDataObj?.MainExternalMapRefernceKey; // 根据 Dataobj 指向的 IdKey 寻找 Datatype 细节
                        if (tIdKey == null) continue;
                        XmlNode? identDatatypeNode = GetDatatypeNodeById(tIdKey);
                        if (identDatatypeNode == null) continue;

                        StackFrame? sf = new StackTrace(new StackFrame(true)).GetFrame(0);
                        AIdent? aIdent = null;
                        // 需要加入 DataobjImpl 的信息， 来构建 IdentImpl
                        ConstructIdent((DataobjImpl)varDtDataObj, out aIdent, (XmlElement)identDatatypeNode);
                        // ConstructIdent(out aIdent, (XmlElement)identDatatypeNode);

                        Debug.Write($"Dynamic Debugging at Row({sf?.GetFileLineNumber()} Col({sf?.GetFileColumnNumber()})"
                            + $" in File {sf?.GetFileName()} when Exec METHOD [{sf?.GetMethod()?.Name}]\n"
                            + $"[....(输出)....\n");
                        Debug.WriteLine($" [({didXObj?.HexN}){qualDescrpDid}]  (&*  ({aIdent?.QualInnerObj?.Text ?? ""} _{aIdent?.MainKey}_ENC_{aIdent?.CEncStr}_DF_{aIdent?.CDfStr}_({aIdent?.GetCValBytesCnt}-Byte)  *&)" + "..] \n\n");
                        if (aIdent != null) varIdentsList.Add(aIdent);
                    }


                    foreach (var newIdentExtend in varIdentsList)
                    {
                        aIdentXObjList.Add(newIdentExtend);
                    }

                }

            }
            catch { throw; }

        }

#endif

        
        
    }
        
}


namespace ReadSimple
{
    
    namespace ConfigNamespace
    {

        public static class CfgSetting
        {
            public const String DidHexSettingsFilePathDefault = @"C:\Users\Public\Documents\didItemSettingsForPecu.csv";
            //public const String DidHexSettingsFilePathDefault = @"C:\Users\Public\Documents\";
            
            
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
                    foreach (var VARIABLE in new string[20]
                             {
                                 "FD20", "FE13",
                                 "FD18", "FD16",
                                 "D103", "F199",
                                 "F12A", "F19A",
                                 "", "",
                                 
                                 "", "",
                                 "", "",
                                 "", "",
                                 "", "",
                                 "", "",
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



