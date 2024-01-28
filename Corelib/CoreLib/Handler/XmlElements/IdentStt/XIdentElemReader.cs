namespace CoreLib.Handler.XmlNodesElements
{

    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using XElemSelector;
    using System.Xml;



    partial class XElemReader
    {

        #region DATATYPES-IDENT

        public static XmlNode? FindDatatypes()
        {
            List<XmlNode?>? ret = new List<XmlNode?>();

            //// var mainIdNameS = new string[3] { "ECUDOC", "DATATYPES", "DID" };
            XmlNode? ecudocNode = null;

            XElemReader xDr = new XElemReader();
            bool loadResult = xDr.LoadXml(_staticDocPath, out var xDocument);

            if (!loadResult) throw new InvalidOperationException("加载 Xml 过程 (Get Datattype) Error！");

            XmlNode? rootNode = null;
            XmlNode? foundDataTypesNode = null;

            foreach (XmlNode? rtNodeSelect in xDocument?.ChildNodes)
            {
                if (rtNodeSelect?.Name == "CANDELA" && (rtNodeSelect?.HasChildNodes ?? false)) rootNode = rtNodeSelect;
            }

            foreach (string subSearchTag in new string[2] { "ECUDOC", "DATATYPES" })
            {
                if (subSearchTag == "ECUDOC" && rootNode != null)
                {
                    foreach (XmlNode? rtNodeSelect in rootNode?.ChildNodes)
                    {
                        if (rtNodeSelect?.Name == "ECUDOC" && (rtNodeSelect?.HasChildNodes ?? false))
                            ecudocNode = rtNodeSelect;
                    }
                }
                else if (subSearchTag == "DATATYPES" && ecudocNode != null)
                {
                    foreach (XmlNode? rtNodeSelect in ecudocNode?.ChildNodes)
                    {
                        if (rtNodeSelect?.Name == "DATATYPES" && (rtNodeSelect?.HasChildNodes ?? false))
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


        public static List<XmlNode?>? FindAllIdentNodesList(
            string[] mainIdNameS = null /* = new string[3] { "ECUDOC", "DATATYPES", "IDENT" }*/)
        {
            List<XmlNode?>? ret = new List<XmlNode?>();

            if (mainIdNameS == null) mainIdNameS = new string[3] { "ECUDOC", "DATATYPES", "IDENT" };
            XmlNode? ecudocNode = null;

            XElemReader xDr = new XElemReader();
            bool loadResult = xDr.LoadXml(_staticDocPath, out var xDocument);

            if (!loadResult) throw new InvalidOperationException("加载 Xml 过程 Error！");

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
                        if (rtNodeSelect?.Name == "ECUDOC" && (rtNodeSelect?.HasChildNodes ?? false))
                            ecudocNode = rtNodeSelect;
                    }
                }
                else if (subSearchTag == "DATATYPES" && ecudocNode != null)
                {
                    foreach (XmlNode? rtNodeSelect in ecudocNode?.ChildNodes)
                    {
                        if (rtNodeSelect?.Name == "DATATYPES" && (rtNodeSelect?.HasChildNodes ?? false))
                            foundDataTypesNode = rtNodeSelect;
                    }
                }
                else continue;
            }

            if (foundDataTypesNode == null) AllIdentNodesList = null;
            else AllIdentNodesList = xDr.FindNodes()(foundDataTypesNode, "IDENT");

            var identNodesArr = AllIdentNodesList?.NodeArrList()?.ToArray();
            if (identNodesArr != null)
                foreach (var node in identNodesArr)
                {
                    _identXNodesList.Add((System.Xml.XmlNode)node);
                }
            //ret = (from System.Xml.XmlNode _oneNode in AllIdentNodesList.NodeArrList()
            //       select (_oneNode).Cast<System.Xml.XmlNode>().ToList());

            return ret = _identXNodesList;

        }

        public static XmlNode? FindDatatypeByIdKey(string t_id_key)
        {
            List<XmlNode?>? retDatatype_IDENT_NodesList = FindAllIdentNodesList();
            ///////////List<XmlNode?>? retDatatype_IDENT_NodesList = FindAllIdentNodesList();
            ///////////List<XmlNode?>? retDatatype_IDENT_NodesList = FindAllIdentNodesList();
            ///////////List<XmlNode?>? retDatatype_IDENT_NodesList = FindAllIdentNodesList();
            ///////List<XmlNode?>? retDidNodesList = FindAllDidNodesListInternal(/**new string[3] { "ECUDOC", "DIDS", "DID" }*/);
            //////List<XmlNode?>? retDidNodesList = FindAllDidNodesListInternal(/**new string[3] { "ECUDOC", "DIDS", "DID" }*/);
            /////List<XmlNode?>? retDidNodesList = FindAllDidNodesListInternal(/**new string[3] { "ECUDOC", "DIDS", "DID" }*/);
            ////List<XmlNode?>? retDidNodesList = FindAllDidNodesListInternal(/**new string[3] { "ECUDOC", "DIDS", "DID" }*/);
            ///
            List<XmlNode?>? retDatatypeNodesList = new List<XmlNode?>();
            if (retDatatype_IDENT_NodesList != null)
                foreach (var node in retDatatype_IDENT_NodesList)
                    retDatatypeNodesList.Add(node);

            if (retDatatypeNodesList == null || retDatatypeNodesList.Count < 1) return null;

            foreach (XmlNode? retDid in retDatatypeNodesList)
            {
                // 只筛选 “IDENT” 类型
                if (retDid?.Name.ToUpper() == "IDENT" &&
                    retDid?.Attributes?["id"]?.Value?.ToLower() == t_id_key.Trim().ToLower()) return retDid;
            }

            return null;
        }

        #endregion

    }

}