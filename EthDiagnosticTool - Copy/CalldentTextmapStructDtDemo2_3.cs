using System;
using System.Collections.Generic;


namespace EthDiagnosticTool
{
    using System.Xml;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.Collections;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Threading;
    using DATATYPES.IDENT;
    using MoreStructueImports.IDENT;
    //using static FindDid.ImportDidSegment.Util;
    using static FindDid.ImportDidSegment;
    using static FindDtSequenceLibrary.ImportIdentRec;
    using static MetasCommon.Metas;
    using static MetasCommon.Metas.MetasInternal;

    internal static class CallDtDemo
    {

#if NET7_0_OR_GREATER
#else
        public sealed class DtDemoHelper
        {
            public DtDemoHelper() { }

            public static List<string>? ReadDidList()
            {
                return Util.GetDidsSearch;
            }

            private static string WriteMethodContent(List<AIdent?>? varIdentXObjList, string qualDescriptionSig)
            {
                string r = string.Empty;
                //BytesDimArrayStruct
                ByteMaximumInfoArrayStruct
                    dobjDimMatrix = new ByteMaximumInfoArrayStruct/*BytesDimArrayStruct*/(varIdentXObjList);

                //BytesDimArrayStruct
                ByteMaximumInfoArrayStruct
                    dDM = dobjDimMatrix;

                string methodDeclaration = $@"        public static string CDDFILE_{qualDescriptionSig} (byte[] recvByteArrToDecode"
                    + " , params object[] argvs) \r\n";

                string methodBody =
@"
                ////
                ///////
                Encoding[,] encMatrix = new Encoding[12,8]
" + "                {\r\n"
  + "                           { " + $"Encoding.{dDM[0] ?? 00}, Encoding.{dDM[1] ?? 00}, Encoding.{dDM[2] ?? 00}, Encoding.{dDM[3] ?? 00}"
                                                           + $", Encoding.{dDM[4] ?? 00}, Encoding.{dDM[5] ?? 00}, Encoding.{dDM[6] ?? 00}, Encoding.{dDM[7] ?? 00}" + " },\r\n"
  + "                           { " + $"Encoding.{dDM[8] ?? 00}, Encoding.{dDM[9] ?? 00}, Encoding.{dDM[10] ?? 00}, Encoding.{dDM[11] ?? 00}"
                                                           + $", Encoding.{dDM[12] ?? 00}, Encoding.{dDM[13] ?? 00}, Encoding.{dDM[14] ?? 00}, Encoding.{dDM[15] ?? 00}" + " },\r\n"
  + "                           { " + $"Encoding.{dDM[16] ?? 00}, Encoding.{dDM[17] ?? 00}, Encoding.{dDM[18] ?? 00}, Encoding.{dDM[19] ?? 00}"
                                                           + $", Encoding.{dDM[20] ?? 00}, Encoding.{dDM[21] ?? 00}, Encoding.{dDM[22] ?? 00}, Encoding.{dDM[23] ?? 00}" + " },\r\n"
  + "                           { " + $"Encoding.{dDM[24] ?? 00}, Encoding.{dDM[25] ?? 00}, Encoding.{dDM[26] ?? 00}, Encoding.{dDM[27] ?? 00}"
                                                           + $", Encoding.{dDM[28] ?? 00}, Encoding.{dDM[29] ?? 00}, Encoding.{dDM[30] ?? 00}, Encoding.{dDM[31] ?? 00}" + " },\r\n"

  + "                           { " + $"Encoding.{dDM[32] ?? 00}, Encoding.{dDM[33] ?? 00}, Encoding.{dDM[34] ?? 00}, Encoding.{dDM[35] ?? 00}"
                                                           + $", Encoding.{dDM[36] ?? 00}, Encoding.{dDM[37] ?? 00}, Encoding.{dDM[38] ?? 00}, Encoding.{dDM[39] ?? 00}" + " },\r\n"
  + "                           { " + $"Encoding.{dDM[40] ?? 00}, Encoding.{dDM[41] ?? 00}, Encoding.{dDM[42] ?? 00}, Encoding.{dDM[43] ?? 00}"
                                                           + $", Encoding.{dDM[44] ?? 00}, Encoding.{dDM[45] ?? 00}, Encoding.{dDM[46] ?? 00}, Encoding.{dDM[47] ?? 00}" + " },\r\n"
  + "                           { " + $"Encoding.{dDM[48] ?? 00}, Encoding.{dDM[49] ?? 00}, Encoding.{dDM[50] ?? 00}, Encoding.{dDM[51] ?? 00}"
                                                           + $", Encoding.{dDM[52] ?? 00}, Encoding.{dDM[53] ?? 00}, Encoding.{dDM[54] ?? 00}, Encoding.{dDM[55] ?? 00}" + " },\r\n"
  + "                           { " + $"Encoding.{dDM[56] ?? 00}, Encoding.{dDM[57] ?? 00}, Encoding.{dDM[58] ?? 00}, Encoding.{dDM[59] ?? 00}"
                                                           + $", Encoding.{dDM[60] ?? 00}, Encoding.{dDM[61] ?? 00}, Encoding.{dDM[62] ?? 00}, Encoding.{dDM[63] ?? 00}" + " },\r\n"

  + "                           { " + $"Encoding.{dDM[64] ?? 00}, Encoding.{dDM[65] ?? 00}, Encoding.{dDM[66] ?? 00}, Encoding.{dDM[67] ?? 00}"
                                                           + $", Encoding.{dDM[68] ?? 00}, Encoding.{dDM[69] ?? 00}, Encoding.{dDM[70] ?? 00}, Encoding.{dDM[71] ?? 00}" + " },\r\n"
  + "                           { " + $"Encoding.{dDM[72] ?? 00}, Encoding.{dDM[73] ?? 00}, Encoding.{dDM[74] ?? 00}, Encoding.{dDM[75] ?? 00}"
                                                           + $", Encoding.{dDM[76] ?? 00}, Encoding.{dDM[77] ?? 00}, Encoding.{dDM[78] ?? 00}, Encoding.{dDM[79] ?? 00}" + " },\r\n"
  + "                           { " + $"Encoding.{dDM[80] ?? 00}, Encoding.{dDM[81] ?? 00}, Encoding.{dDM[82] ?? 00}, Encoding.{dDM[83] ?? 00}"
                                                           + $", Encoding.{dDM[84] ?? 00}, Encoding.{dDM[85] ?? 00}, Encoding.{dDM[86] ?? 00}, Encoding.{dDM[87] ?? 00}" + " },\r\n"
  + "                           { " + $"Encoding.{dDM[88] ?? 00}, Encoding.{dDM[89] ?? 00}, Encoding.{dDM[90] ?? 00}, Encoding.{dDM[91] ?? 00}"
                                                           + $", Encoding.{dDM[92] ?? 00}, Encoding.{dDM[93] ?? 00}, Encoding.{dDM[94] ?? 00}, Encoding.{dDM[95] ?? 00}" + " },\r\n"


+ "                  };"
+ Environment.NewLine
+@"
                //// DelimStrs
                ///////
                string[,] delimMatrix = new string[12,8]
" + "                {\r\n"
  + "                           { " + $"\"{dDM[0,0] ?? ""}\", \"{dDM[0,1] ?? ""}\", \"{dDM[0,2] ?? ""}\", \"{dDM[0,3] ?? ""}\" ,"
                                                           + $"\"{dDM[0, 4] ?? ""}\", \"{dDM[0, 5] ?? ""}\", \"{dDM[0, 6] ?? ""}\", \"{dDM[0, 7] ?? ""}\"" + " },\r\n"
  + "                           { " + $"\"{dDM[1, 0] ?? ""}\", \"{dDM[1, 1] ?? ""}\", \"{dDM[1, 2] ?? ""}\", \"{dDM[1, 3] ?? ""}\" ,"
                                                           + $"\"{dDM[1, 4] ?? ""}\", \"{dDM[1, 5] ?? ""}\", \"{dDM[1, 6] ?? ""}\", \"{dDM[1, 7] ?? ""}\"" + " },\r\n"
  + "                           { " + $"\"{dDM[2, 0] ?? ""}\", \"{dDM[2, 1] ?? ""}\", \"{dDM[2, 2] ?? ""}\", \"{dDM[2, 3] ?? ""}\" ,"
                                                           + $"\"{dDM[2, 4] ?? ""}\", \"{dDM[2, 5] ?? ""}\", \"{dDM[2, 6] ?? ""}\", \"{dDM[2, 7] ?? ""}\"" + " },\r\n"
  + "                           { " + $"\"{dDM[3, 0] ?? ""}\", \"{dDM[3, 1] ?? ""}\", \"{dDM[3, 2] ?? ""}\", \"{dDM[3, 3] ?? ""}\" ,"
                                                           + $"\"{dDM[3, 4] ?? ""}\", \"{dDM[3, 5] ?? ""}\", \"{dDM[3, 6] ?? ""}\", \"{dDM[3, 7] ?? ""}\"" + " },\r\n"

  + "                           { " + $"\"{dDM[4, 0] ?? ""}\", \"{dDM[4, 1] ?? ""}\", \"{dDM[4, 2] ?? ""}\", \"{dDM[4, 3] ?? ""}\" ,"
                                                           + $"\"{dDM[4, 4] ?? ""}\", \"{dDM[4, 5] ?? ""}\", \"{dDM[4, 6] ?? ""}\", \"{dDM[4, 7] ?? ""}\"" + " },\r\n"
  + "                           { " + $"\"{dDM[5, 0] ?? ""}\", \"{dDM[5, 1] ?? ""}\", \"{dDM[5, 2] ?? ""}\", \"{dDM[5, 3] ?? ""}\" ,"
                                                           + $"\"{dDM[5, 4] ?? ""}\", \"{dDM[5, 5] ?? ""}\", \"{dDM[5, 6] ?? ""}\", \"{dDM[5, 7] ?? ""}\"" + " },\r\n"
  + "                           { " + $"\"{dDM[6, 0] ?? ""}\", \"{dDM[6, 1] ?? ""}\", \"{dDM[6, 2] ?? ""}\", \"{dDM[6, 3] ?? ""}\" ,"
                                                           + $"\"{dDM[6, 4] ?? ""}\", \"{dDM[6, 5] ?? ""}\", \"{dDM[6, 6] ?? ""}\", \"{dDM[6, 7] ?? ""}\"" + " },\r\n"
  + "                           { " + $"\"{dDM[7, 0] ?? ""}\", \"{dDM[7, 1] ?? ""}\", \"{dDM[7, 2] ?? ""}\", \"{dDM[7, 3] ?? ""}\" ,"
                                                           + $"\"{dDM[7, 4] ?? ""}\", \"{dDM[7, 5] ?? ""}\", \"{dDM[7, 6] ?? ""}\", \"{dDM[7, 7] ?? ""}\"" + " },\r\n"

  + "                           { " + $"\"{dDM[8, 0] ?? ""}\", \"{dDM[8, 1] ?? ""}\", \"{dDM[8, 2] ?? ""}\", \"{dDM[8, 3] ?? ""}\" ,"
                                                           + $"\"{dDM[8, 4] ?? ""}\", \"{dDM[8, 5] ?? ""}\", \"{dDM[8, 6] ?? ""}\", \"{dDM[8, 7] ?? ""}\"" + " },\r\n"
  + "                           { " + $"\"{dDM[9, 0] ?? ""}\", \"{dDM[9, 1] ?? ""}\", \"{dDM[9, 2] ?? ""}\", \"{dDM[9, 3] ?? ""}\" ,"
                                                           + $"\"{dDM[9, 4] ?? ""}\", \"{dDM[9, 5] ?? ""}\", \"{dDM[9, 6] ?? ""}\", \"{dDM[9, 7] ?? ""}\"" + " },\r\n"
  + "                           { " + $"\"{dDM[10, 0] ?? ""}\", \"{dDM[10, 1] ?? ""}\", \"{dDM[10, 2] ?? ""}\", \"{dDM[10, 3] ?? ""}\" ,"
                                                            + $"\"{dDM[10, 4] ?? ""}\", \"{dDM[10, 5] ?? ""}\", \"{dDM[10, 6] ?? ""}\", \"{dDM[10, 7] ?? ""}\"" + " },\r\n"
  + "                           { " + $"\"{dDM[11, 0] ?? ""}\", \"{dDM[11, 1] ?? ""}\", \"{dDM[11, 2] ?? ""}\", \"{dDM[11, 3] ?? ""}\" ,"
                                                            + $"\"{dDM[11, 4] ?? ""}\", \"{dDM[11, 5] ?? ""}\", \"{dDM[11, 6] ?? ""}\", \"{dDM[11, 7] ?? ""}\"" + " },\r\n"


+ "                  };"

+ Environment.NewLine

+ @"
                ///
                return GetDecodesStrByteByByte(recvByteArrToDecode, encMatrix, delimMatrix);
                ///////////
                ///
";

                r = $"{methodDeclaration}" + "         {\r\n" + $"{methodBody}" + "\r\n        }";

                // 生成字符串
                // textBuilder.Append(r + Environment.NewLine);

                return r += Environment.NewLine;
            }




            public static bool ReadWriteDidBtnCfgCsv(List<string>? inputdids, string readRawRdidBtnCfgCsvFilePath, string copyRdidBtnCfgCsvFilePath)
            {
                var templateDidCsvBtnContentRaw = string.Empty;
                using (FileStream fsCsv = new FileStream(readRawRdidBtnCfgCsvFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader r = new StreamReader(fsCsv, System.Text.Encoding.UTF8))
                    {
                        templateDidCsvBtnContentRaw = r.ReadToEnd();
                        
                    }
                }

                if (inputdids == null) return false;


                try
                {
                    string addedCsvContent = string.Empty;
                    foreach (var didIdStr in inputdids)
                    {
                        BaseDid? didObj = null;
                        XmlNode? didElem = GetDidNodeById(didIdStr);
                        if (didElem == null) continue;
                        ConstructDid(out didObj, (XmlElement)didElem);

                        string? qualName = didObj?.QualInnerObj?.Text;
                        string? hexNStr = didObj?.HexN;

                        if (qualName != null && hexNStr != null)  // 只有对于 QualName 和 HexN（$22__ ）属性都存在的情形， 才可以写 Csv
                        {
                            var line = $"{qualName},{hexNStr},,0x01,0x00,CDDFILE_{qualName}";
                            addedCsvContent += line + Environment.NewLine;
                        }

                    }

                    string allContent = templateDidCsvBtnContentRaw /*+ Environment.NewLine*/ + addedCsvContent;

                    try { File.Delete(copyRdidBtnCfgCsvFilePath);  } catch { }


                    using (FileStream fsCsvToWrite = new FileStream(copyRdidBtnCfgCsvFilePath, FileMode.CreateNew, FileAccess.Write))
                    {
                        using (StreamWriter r = new StreamWriter(fsCsvToWrite, System.Text.Encoding.UTF8))
                        {
                            r.WriteLine(allContent);

                        }
                    }

                    return true;
                }

                catch { throw;  }

                

            }



            public static List<string>? ReadWriteMethodWithDidList(List<string>? inputdids, string readRawToStringFilePath, 
                string copyDestFilePath, string moreToCopyDestTsFilePath)
            {
                string templateEndlines = ("\r\n    }\r\n}" + Environment.NewLine);

                string templateContent = string.Empty;

                string oriToStringFilePath = readRawToStringFilePath; // @"..\..\..\bin\Debug\ConfigTemp\ToString.cs";

                using (FileStream fs = new FileStream(oriToStringFilePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader r = new StreamReader(fs, System.Text.Encoding.UTF8))
                    {
                        var templateContentRaw = r.ReadToEnd();
                        templateContent = templateContentRaw.Replace("}//DELSTART\r\n\r\n", "") + "        //..\r\n" + "        //..\r\n";
                    }
                }

                //////////////////////////////////////////try {  File.Copy(oriToStringFilePath, copyDestFilePath);  } catch { }
                ////////////////try { File.Copy(oriToStringFilePath, @"C:\Users\Public\Documents\ToString.cs"); } catch { }
                try { File.Copy(oriToStringFilePath, moreToCopyDestTsFilePath); } catch { }

                List<string> methodsWrittenStrLines = new List<string>();
                if (inputdids == null) return null;
                string? qualName = string.Empty;

                foreach(string inDid in inputdids)
                {
                    BaseDid? didObj = null;
                    XmlNode? didElem = GetDidNodeById(inDid);
                    if (didElem == null) continue;
                    ConstructDid(out didObj, (XmlElement)didElem);

                    qualName = didObj?.QualInnerObj?.Text;


                    if (qualName == null) continue; // 无法对于空的函数签名 写入cs文件
                    ConstructAIdentListForOneDid(out List<AIdent?>? thisIdentXObjList, didObj);

                    if (thisIdentXObjList == null) continue;
                    string addContentLines = WriteMethodContent(thisIdentXObjList, qualName);
                    methodsWrittenStrLines.Add(addContentLines);
                }

                // concatenate file lines
                var debugOutput = string.Join('\n', methodsWrittenStrLines);


                string retWritenText = templateContent + debugOutput + templateEndlines;

                // wait files writen completed
                // return methodsWrittenStrLines;

                // then write file
                WriteToLogAsync(retWritenText, copyDestFilePath).Wait();

                ///////////////////////////////////try { File.Copy(oriToStringFilePath, copyDestFilePath); } catch { }
                /////////try { File.Copy(copyDestFilePath, @"C:\Users\Public\Documents\ToString.cs"); } catch { }
                try { WriteToLogAsync(retWritenText, moreToCopyDestTsFilePath).Wait(); } catch { }


                return methodsWrittenStrLines;

            }

        }
#endif

#if NET7_0_OR_GREATER
#else
        private static async Task WriteToLogAsync(string text, string path)
        {
            if (File.Exists(path))
            {
                File.WriteAllText(path, string.Empty);
            }

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (TextWriter trwr = new StreamWriter(fs))
                {
                    await trwr.WriteAsync(text); //ok
                }
            }

            await Task.Delay(300);

        }
#endif

#if NET7_0_OR_GREATER
#else
        internal static void ConstructDid(out BaseDid? didXObj, XmlElement? didXElem)
        {
            didXObj = null;
            var t = new DidImpl(didXElem);

            try
            {
                if (t == null) throw new MissingFieldException("无法构建 out BaseDid didXObj 实例！");

                else didXObj = (BaseDid)t;

                return;
            }
            catch { throw; }

        }
#endif

#if NET7_0_OR_GREATER
#else

        internal static void ConstructIdent(DataobjImpl? dObjImpl, out AIdent? identXObj, XmlElement? identXElem)
        {
            identXObj = null;
            var t = new IdentImpl(dObjImpl, identXElem);

            try
            {
                if (t == null) throw new MissingFieldException("无法构建 out AIdent identXObj 实例！");

                else identXObj = (AIdent)t;

                return;
            }
            catch { throw; }

        }

        internal static void ConstructIdent(MetasCommFile.Metas.MetasInternal.StructComplexImpl? dStructComplexObjImpl, out AIdent? identXObj, XmlElement? identXElem)
        {
            identXObj = null;
            var t = new IdentImpl(dStructComplexObjImpl, identXElem);

            try
            {
                if (t == null) throw new MissingFieldException("无法构建 out AIdent identXObj 实例！");

                else identXObj = (AIdent)t;

                return;
            }
            catch { throw; }

        }


        internal static void ConstructIdent(out AIdent? identXObj, XmlElement? identXElem)
        {
            identXObj = null;
            var t = new IdentImpl(identXElem);

            try
            {
                if (t == null) throw new MissingFieldException("无法构建 out AIdent identXObj 实例！");

                else identXObj = (AIdent)t;

                return;
            }
            catch { throw; }

        }
#endif

#if NET7_0_OR_GREATER
#else
        /// <summary>
        ///  根据 didObject 构建出每一个 structure -> dataobj 的 Dtref 指向的细分的数据结构（IDENT |）的抽象列表
        /// </summary>
        /// <out_param name="aIdentXObjList"></out_param>
        /// <param name="didXObj"></param>
        internal/*private*/ static void ConstructAIdentListForOneDid(out List<AIdent?>? aIdentXObjList, BaseDid? didXObj)
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

                // Find dataobjs
                var structInnerdataObjEnumeratorExtends = didXObj?.StructureInner?.GetStructInnerEnumer(); ; // TODO: output debug info.. (总共有多少一级 Structure -> Struct ？)

                if (structInnerdataObjEnumeratorExtends == null) ;//continue;
                else
                {
                    foreach (var varDt in structInnerdataObjEnumeratorExtends) // // 对于每一个 Dataobj， 
                    {
                        if (varDt == null) continue;     //

                        string? tIdKey = varDt?.MainSearchKey; // 根据 Dataobj 指向的 IdKey 寻找 Datatype 细节
                        if (tIdKey == null) continue;
                        XmlNode? identDatatypeNode = GetDatatypeNodeById(tIdKey);
                        if (identDatatypeNode == null) continue;

                        StackFrame? sf = new StackTrace(new StackFrame(true)).GetFrame(0);
                        AIdent? aIdent = null;
                        ConstructIdent((MetasCommFile.Metas.MetasInternal.StructComplexImpl)varDt, out aIdent, (XmlElement)identDatatypeNode);

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
