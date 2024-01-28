namespace CoreLib.DS.DATATYPES.QUAL
{

    using System;
    using System.Linq;
    using CoreLib.DS.INTERFACES;
    using System.Diagnostics;
    using static Util.Delegates;


    public abstract class BaseQual : IDescProp
        {

            public BaseQual()
            {
                StackFrame sf = new StackTrace(new StackFrame(true)).GetFrame(0);
                throw new MissingFieldException($"Dynamic Error found at Row({sf.GetFileLineNumber()} Col({sf.GetFileColumnNumber()})"
                    + $" in File {sf.GetFileName()} when Exec METHOD [{sf.GetMethod().Name}]\n"
                    + $"[...trace... 缺少初始化此实例(Ast(Abstract)Qual)所需的参数]\n");
            }
            public BaseQual(System.Xml.XmlNode? baseXNameNode) // : this()  // TODO:  Remove throwing
            {
                ;
                _someTuvNodeList = new List<System.Xml.XmlNode?>();
                var tuvNodesArr = (FindNodes(baseXNameNode, "Tuv").NodeArrList()).ToArray();
                if (tuvNodesArr != null) foreach (var node in tuvNodesArr) { _someTuvNodeList.Add((System.Xml.XmlNode)node); }
                try
                {
                    /**
                    List<AstTuv> someTuvInnerObjObjList =
                        (from System.Xml.XmlNode _tuvInnerNode in _someTuvNodeList
                         select (new TuvImpl(_tuvInnerNode))).Cast<AstTuv>().ToList();
                    TuvInnerObjArray = someTuvInnerObjObjList.ToArray();
                    TuvInnerObj = TuvInnerObjArray?[0];*/

                }
                catch (Exception tuvNodeErr) { }
            }

            string IDescProp._Text() => throw new NotImplementedException("no tuv-text needed for \"qual\""); // TuvInnerObj?.NameText ?? "";
            public abstract string TextPropertyToString();
            public virtual string Text => ((IDescProp)this)._Text();
            internal string TuvNameText => throw new NotImplementedException("no tuv-text needed for \"qual\""); // TuvInnerObj?.NameText ?? "";

            protected internal class TuvImpl : AstTuv
            {
                public TuvImpl()
                {
                    StackFrame sf = new StackTrace(new StackFrame(true)).GetFrame(0);
                    throw new MissingFieldException($"Dynamic Error found at Row({sf.GetFileLineNumber()} Col({sf.GetFileColumnNumber()})"
                        + $" in File {sf.GetFileName()} when Exec METHOD [{sf.GetMethod().Name}]\n"
                        + $"[...trace... 缺少初始化此实例(Ast(Abstract)Tuv)所需的参数]\n");
                }

                public TuvImpl(System.Xml.XmlNode? tNode)
                {
                    XmlLang = tNode?.Attributes["xml:lang"]?.Value;
                    NameText = tNode?.InnerText;
                }

                public override string TextPropertyToString()
                {
                    throw new NotImplementedException();
                }
            }

            protected internal abstract class AstTuv : IDescProp
            {
                string IDescProp._Text() => this.NameText ?? "";
                public abstract string TextPropertyToString();

                protected string XmlLang { get; set; }
                protected internal string NameText { get; internal set; }

            }

            private List<System.Xml.XmlNode?> _someTuvNodeList;
            // protected readonly AstTuv? TuvInnerObj;
            // protected readonly AstTuv[]? TuvInnerObjArray;
        }
    
}


namespace CoreLib.DS.DATATYPES.QUAL
{
    
    using CoreLib.DS.INTERFACES;
    using System.Linq;
    using System.Diagnostics;
    using static Util.Delegates;
    
    
    public abstract class BaseName : IDescProp
    {
        public BaseName()
        {
            StackFrame sf = new StackTrace(new StackFrame(true)).GetFrame(0);
                throw new MissingFieldException($"Dynamic Error found at Row({sf.GetFileLineNumber()} Col({sf.GetFileColumnNumber()})"
                    + $" in File {sf.GetFileName()} when Exec METHOD [{sf.GetMethod().Name}]\n"
                    + $"[...trace... 缺少初始化此实例(Ast(Abstract)Name)所需的参数]\n");
        }
        public BaseName(System.Xml.XmlNode? baseXNameNode) // : this()  // TODO:  Remove throwing
        {
                ;
                _someTuvNodeList = new List<System.Xml.XmlNode?>();
                var tuvNodesArr = (FindNodes(baseXNameNode, "Tuv").NodeArrList()).ToArray();
                if (tuvNodesArr != null) foreach (var node in tuvNodesArr) { _someTuvNodeList.Add((System.Xml.XmlNode)node); }
            try
            {

                    List<AstTuv> someTuvInnerObjObjList =
                        (from System.Xml.XmlNode _tuvInnerNode in _someTuvNodeList
                         select (new TuvImpl(_tuvInnerNode))).Cast<AstTuv>().ToList();
                    TuvInnerObjArray = someTuvInnerObjObjList.ToArray();
                    TuvInnerObj = TuvInnerObjArray?[0];

            }
                catch (Exception tuvNodeErr) { }
        }

        string IDescProp._Text() => TuvInnerObj?.NameText ?? "";
        public abstract string TextPropertyToString();
        public virtual string Text => ((IDescProp)this)._Text();
        internal string TuvNameText => TuvInnerObj?.NameText;

        protected internal class TuvImpl : AstTuv
        {
                public TuvImpl()
                {
                    StackFrame sf = new StackTrace(new StackFrame(true)).GetFrame(0);
                    throw new MissingFieldException($"Dynamic Error found at Row({sf.GetFileLineNumber()} Col({sf.GetFileColumnNumber()})"
                        + $" in File {sf.GetFileName()} when Exec METHOD [{sf.GetMethod().Name}]\n"
                        + $"[...trace... 缺少初始化此实例(Ast(Abstract)Tuv)所需的参数]\n");
                }

                public TuvImpl(System.Xml.XmlNode? tNode)
                {
                    XmlLang = tNode?.Attributes["xml:lang"]?.Value;
                    NameText = tNode?.InnerText;
                }

                public override string TextPropertyToString()
                {
                    throw new NotImplementedException();
                }
        }

        protected internal abstract class AstTuv : IDescProp
        {
                string IDescProp._Text() => this.NameText ?? "";
                public abstract string TextPropertyToString();

                protected string XmlLang { get; set; }
                protected internal string NameText { get; internal set; }

        }

        private List<System.Xml.XmlNode?> _someTuvNodeList;
        protected readonly AstTuv? TuvInnerObj;
        protected readonly AstTuv[]? TuvInnerObjArray;
            
    }



    namespace Util
    {
        internal static class Delegates
        {
            public static Func<System.Xml.XmlNode?, string?, XElemSelector.IHSXmlNodeList?> FindNodes => XElemSelector.StaticHelper.GetNodes();
        }
    }


    namespace Ctor
    {
        
        public class CompoQualImpl : BaseQual
            {
                public CompoQualImpl()
                {
                    StackFrame sf = new StackTrace(new StackFrame(true)).GetFrame(0);
                    throw new MissingFieldException($"Dynamic Error found at Row({sf.GetFileLineNumber()} Col({sf.GetFileColumnNumber()})"
                        + $" in File {sf.GetFileName()} when Exec METHOD [{sf.GetMethod().Name}]\n"
                        + $"[...trace... 缺少初始化此实例(CompoQualImpl)所需的参数]\n");
                }

                public CompoQualImpl(System.Xml.XmlNode? targetXNode) : base(targetXNode)
                {
                    _qualNode = targetXNode;

                    //var tuvNodesArrList = SelectorLib.SysXmlWrapper.StaticHelper.GetNodes()(baseXNameNode, "Tuv").NodeArrList();
                    //if (tuvNodesArrList != null && tuvNodesArrList.Count > 0)
                    //{
                    //    TuvInnerObjArray = (AstTuv[])tuvNodesArrList.ToArray();
                    //    TuvInnerObj = TuvInnerObjArray[0];
                    //}
#if DEBUG
                    Console.WriteLine(this.Text);
                    StackFrame sf = new StackTrace(new StackFrame(true)).GetFrame(0);
                    System.Diagnostics.Debug.WriteLine($"st Row({sf.GetFileLineNumber()} Col({sf.GetFileColumnNumber()})"
                        + $"Dynamic DEBUG in File {sf.GetFileName()} when Exec METHOD [{sf.GetMethod().Name}]\n"
                        + $"[...trace ret... ]  - \"Qual\"Text Result :: {this.Text}\n");
#endif
                }

                public override string Text => _qualNode?.InnerText ?? "";

                public override string TextPropertyToString()
                {
                    throw new NotImplementedException();
                }

                public override bool Equals(object obj)
                {
                    return obj is CompoQualImpl impl &&
                           TuvNameText == impl.TuvNameText;
                }

                public override int GetHashCode()
                {
                    return -635058128 + EqualityComparer<string>.Default.GetHashCode(TuvNameText);
                }


                private string GetDebuggerDisplay()
                {
                    // return base.ToString() + Environment.NewLine + 
                    return this.ToString();
                }

                private System.Xml.XmlNode? _qualNode;

            }
        
    }
    
    
    namespace Ctor
    {
        
        
        public class CompoNameImpl : BaseName
            {
                public CompoNameImpl()
                {
                    StackFrame sf = new StackTrace(new StackFrame(true)).GetFrame(0);
                    throw new MissingFieldException($"Dynamic Error found at Row({sf.GetFileLineNumber()} Col({sf.GetFileColumnNumber()})"
                        + $" in File {sf.GetFileName()} when Exec METHOD [{sf.GetMethod().Name}]\n"
                        + $"[...trace... 缺少初始化此实例所需的参数]\n");
                }

                public CompoNameImpl(System.Xml.XmlNode? targetXNode) : base(targetXNode)
                {
                    _nameNode = targetXNode;

                    //var tuvNodesArrList = SelectorLib.SysXmlWrapper.StaticHelper.GetNodes()(baseXNameNode, "Tuv").NodeArrList();
                    //if (tuvNodesArrList != null && tuvNodesArrList.Count > 0)
                    //{
                    //    TuvInnerObjArray = (AstTuv[])tuvNodesArrList.ToArray();
                    //    TuvInnerObj = TuvInnerObjArray[0];
                    //}
#if DEBUG
                    Console.WriteLine(this.Text);
                    StackFrame sf = new StackTrace(new StackFrame(true)).GetFrame(0);
                    System.Diagnostics.Debug.WriteLine($"st Row({sf.GetFileLineNumber()} Col({sf.GetFileColumnNumber()})"
                        + $"Dynamic DEBUG in File {sf.GetFileName()} when Exec METHOD [{sf.GetMethod().Name}]\n"
                        + $"[...trace... CompoNameTuvTextResult :: {this.Text}]\n");
#endif
                }

                public override string TextPropertyToString()
                {
                    throw new NotImplementedException();
                }

                public override bool Equals(object obj)
                {
                    return obj is CompoNameImpl impl &&
                           TuvNameText == impl.TuvNameText;
                }

                public override int GetHashCode()
                {
                    return -635058128 + EqualityComparer<string>.Default.GetHashCode(TuvNameText);
                }


                private string GetDebuggerDisplay()
                {
                    // return base.ToString() + Environment.NewLine + 
                    return this.ToString();
                }

                #region compTuv
                internal class CompoTuvImpl : BaseName.AstTuv
                {

                    internal CompoTuvImpl()
                    {
                        throw new NotImplementedException("无法从给定个数【0】的参数列表构造 TuvImpl 实例");
                    }

                    internal CompoTuvImpl(System.Xml.XmlNode? targetXNode, String xmlLangAttrib = "xml:lang")
                    {
                        if (targetXNode != null)
                        {
                            XmlLang = targetXNode?.Attributes?[xmlLangAttrib]?.Value ?? "";
                            NameText = targetXNode?.InnerText;
                        }
                    }

                    public override string TextPropertyToString()
                    {
                        throw new NotImplementedException();
                    }

                    internal string AccessibleXmlLang
                    {
                        get => this.XmlLang;
                        private set => this.XmlLang = value;
                    }
                }

                #endregion

                private System.Xml.XmlNode? _nameNode;

            }
        
    }
    
}

