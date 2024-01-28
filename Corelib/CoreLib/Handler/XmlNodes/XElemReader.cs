
namespace CoreLib.Handler.XmlNodesElements
{
    
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using XElemSelector;
    using System.Xml;
    
    
    
    public /*abstract */partial class XElemReader : CoreLib.Handler.XmlNodes.XNodesCaller
    {
    
        private static List<XmlNode?> _usableDidsLoadedFromCddFile = new List<XmlNode?>();
        
        
        private static string _staticDocPath = "C:\\Users\\Public\\Documents\\UdsDidTemplateConfig\\PECUZK.cdd.xml.cpy.Temp.xmlss";
        // wrong Path
        
        
        #region backupCodes
        
        public static IHSXmlNodeList? AllIdentNodesList{ get; private set; } = null;
        public static IHSXmlNodeList? AllDidNodesList{ get; private set; } = null;

        private static List<XmlNode?> _identXNodesList = new List<XmlNode?>();


        #region Did

        public static XmlNode? FindDids()
        {
            List<XmlNode?>? ret = new List<XmlNode?>();

            //// var mainIdNameS = new string[3] { "ECUDOC", "DIDS", "DID" };
            XmlNode? ecudocNode = null;

            XElemReader xDr = new XElemReader();
            bool loadResult = xDr.LoadXml(_staticDocPath, out var xDocument);

            if (!loadResult) throw new InvalidOperationException("加载 Xml 过程 (Get Did) Error！");

            XmlNode? rootNode = null;
            XmlNode? foundDataTypesNode = null;

            foreach (XmlNode? rtNodeSelect in xDocument?.ChildNodes)
            {
                if (rtNodeSelect?.Name == "CANDELA" && (rtNodeSelect?.HasChildNodes ?? false)) rootNode = rtNodeSelect;
            }

            foreach (string subSearchTag in new string[2] { "ECUDOC", "DIDS" })
            {
                if (subSearchTag == "ECUDOC" && rootNode != null)
                {
                    foreach (XmlNode? rtNodeSelect in rootNode?.ChildNodes)
                    {
                        if (rtNodeSelect?.Name == "ECUDOC" && (rtNodeSelect?.HasChildNodes ?? false)) ecudocNode = rtNodeSelect;
                    }
                }
                else if (subSearchTag == "DIDS" && ecudocNode != null)
                {
                    foreach (XmlNode? rtNodeSelect in ecudocNode?.ChildNodes)
                    {
                        if (rtNodeSelect?.Name == "DIDS" && (rtNodeSelect?.HasChildNodes ?? false))
                        {
                            foundDataTypesNode /*foundDidsNode*/ = rtNodeSelect;
                            return rtNodeSelect;
                        }

                        else continue;
                    }
                }
                else continue;
            }

            return null;
        }
        
        
        public static List<XmlNode?>? FindAllDidNodesList(string[] mainIdNameS = null/* = new string[3] { "ECUDOC", "DIDS", "DID" }*/)
        {
            List<XmlNode?>? ret = new List<XmlNode?>();

            if (mainIdNameS == null) mainIdNameS = new string[3] { "ECUDOC", "DIDS", "DID" };
            XmlNode? ecudocNode = null;

            XElemReader xDr = new XElemReader();
            bool loadResult = xDr.LoadXml(_staticDocPath, out var xDocument);

            if (!loadResult) throw new InvalidOperationException("加载 Xml 过程 (Get Did) Error！");

            XmlNode? rootNode = null;
            XmlNode? foundDataTypesNode = null;

            foreach (XmlNode? rtNodeSelect in xDocument?.ChildNodes)
            {
                if (rtNodeSelect?.Name == "CANDELA" && (rtNodeSelect?.HasChildNodes ?? false)) rootNode = rtNodeSelect;
            }

            foreach (string subSearchTag in mainIdNameS)
            {
                if (subSearchTag == "ECUDOC" && rootNode != null)
                {
                    foreach (XmlNode? rtNodeSelect in rootNode?.ChildNodes)
                    {
                        if (rtNodeSelect?.Name == "ECUDOC" && (rtNodeSelect?.HasChildNodes ?? false)) ecudocNode = rtNodeSelect;
                    }
                }
                else if (subSearchTag == "DIDS" && ecudocNode != null)
                {
                    foreach (XmlNode? rtNodeSelect in ecudocNode?.ChildNodes)
                    {
                        if (rtNodeSelect?.Name == "DIDS" && (rtNodeSelect?.HasChildNodes ?? false)) foundDataTypesNode/*foundDidsNode*/ = rtNodeSelect;
                    }
                }
                else continue;
            }

            if (foundDataTypesNode == null) AllIdentNodesList = null;
            else AllDidNodesList = xDr.FindNodes()(foundDataTypesNode, "DID");

            var didNodesArr = AllDidNodesList?.NodeArrList()?.ToArray();
            if (didNodesArr != null) foreach (var node in didNodesArr) { _usableDidsLoadedFromCddFile.Add((System.Xml.XmlNode)node); }
            //ret = (from System.Xml.XmlNode _oneNode in AllIdentNodesList.NodeArrList()
            //       select (_oneNode).Cast<System.Xml.XmlNode>().ToList());

            return ret = _usableDidsLoadedFromCddFile;

        }


        public static XmlNode? FindDidByIdKey(string d_id_key)
        {
            List<XmlNode?>? retDidNodesList = FindAllDidNodesList(/**new string[3] { "ECUDOC", "DIDS", "DID" }*/);
            if (retDidNodesList == null) return null;

            foreach(XmlNode? retDid in retDidNodesList)
            {
                if (retDid?.Name.ToUpper() == "DID" && retDid?.Attributes?["id"]?.Value?.ToLower() == d_id_key.Trim().ToLower()) return retDid;
            }

            return null;
        }

        #endregion
        

        #region TestSample
        
        public void TestSample(XmlNode? xN, TextWriter? ttw)
        {
            if (xN == null) System.Diagnostics.Debug.WriteLine($"TestSample 方法， 传入了空的 XmlNode ！ Error ！");
            XElemReaderSampleApp a = new XElemReaderSampleApp();
            ttw?.WriteLine(a.DisplayXElemInnerXml((XmlElement)xN));
        }

        
        #endregion
        
        
        #region internal-class
        internal class XElemReaderSampleApp
        {
            internal string? DisplayXElemInnerXml(XmlElement? xElem)
            {
                return xElem?.InnerXml;
            }
        }
        #endregion
        
        
        #endregion
        
    }
    
    
    
    
    
}
