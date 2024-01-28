


namespace CoreLib.Handler.XmlNodes
{
    using CoreLib.Handler.XmlNodes.Interfaces;
    using static XElemSelector.StaticHelper;
    using XElemSelector;
    using System.Xml;
    
        
    public abstract class XNodesCaller : IXNodesHandler , IXmlComm
    {
        public Func<XmlNode?, string?, IHSXmlNodeList?> FindNodes()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///  overiden
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <param name="xDoc"></param>
        /// <returns></returns>
        /// <exception cref="InvalidProgramException"></exception>
        public bool LoadXml(string xmlFilePath, out XmlDocument? xDoc)
        {
            try
            {
                if (!File.Exists(xmlFilePath)) { xDoc = null; return false; }

                try{ xDoc = StaticDllMethod.FindDocDefault()(xmlFilePath); }
                // 首先尝试根据指定的 xPath 来建立 XmlDoc 对象
                
                // 
                catch {xDoc = StaticDllMethod.FindDocDefault()(StaticDllMethod.DllConstants.Cdd_1_path);}

                return true;

            }

            catch (FileNotFoundException) { xDoc = null; return false; }
            catch (FileLoadException) { xDoc = null; return false; }
            catch (Exception otherE) { throw new InvalidProgramException($"\n{otherE.ToString()}"); }
        }
    }
    
    internal static class StaticDllMethod
    {

        internal static class DllConstants
        {

            public const string Cdd_1_path = "\\\\ad.trw.com\\anting\\Electronics\\Diagnosis\\Personal folder\\lijx\\ProtectedFilesBakup\\Hists\\PECU_TEMP_V2.0_CDD.xml.bakJan";

        }


        
        #region GetXDocumentFromFilePath
        /*private*/
        internal static Func<string?, XmlDocument?> FindDocDefault()
        {
            return delegate (string? docPath/* = null*/)
            {
                try
                {
                    string staticDocPath = docPath;
                    if (File.Exists(staticDocPath))
                    {
                        XmlDocument fDocument = new XmlDocument();
                        fDocument.Load(staticDocPath);
                        return fDocument;
                    }
                }


                catch
                {
                    string staticDocPath = "\\\\ad.trw.com\\anting\\Electronics\\Diagnosis\\Personal folder\\lijx\\ProtectedFileFolderEasyToLocate\\HistoryCdds\\PECU_TEMP_V2.0_CDD.xml";
                    if (File.Exists(staticDocPath))
                    {
                        XmlDocument fDocument = new XmlDocument();
                        fDocument.Load(staticDocPath);
                        return fDocument;
                    }
                }

                return null;
            };
        }

        #endregion


    }
    
}
