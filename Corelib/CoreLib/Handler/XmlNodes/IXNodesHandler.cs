namespace CoreLib.Handler.XmlNodes.Interfaces
{
    using System.Xml;
    
    
    internal interface IXmlComm
    {

        /// <summary>
        /// 此接口实现功能是——给定的文件路径加载 Xml 格式的结构 （XmlDocument 类型） 
        /// 
        /// 不抛异常， 加载 Xml 遇到IO异常则返回 null
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <param name="xDoc"></param>
        /// <returns></returns>
        public bool LoadXml(string xmlFilePath, out XmlDocument? xDoc);
        //

        


    }
    
    public interface IXNodesHandler
    {
        /// <summary>
        /// 此接口实现功能是——通过给定的 XNode， SubNodeName， 返回 IHSXmlNodeList
        /// 
        /// 不抛异常， 遍历 SubXmlNodes 遇到异常（！HasChildNodes == true）则返回 null
        /// </summary>
        public Func<System.Xml.XmlNode?, string?, XElemSelector.IHSXmlNodeList?> FindNodes();
    }
    
}

