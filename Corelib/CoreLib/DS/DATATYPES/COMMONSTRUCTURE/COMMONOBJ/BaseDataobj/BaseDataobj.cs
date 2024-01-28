

namespace CoreLib.DS.DATATYPES.COMMONSTRUCTURE.COMMONOBJ.BaseDataobj
{
    
    using System;
    using System.Xml;
    using System.Diagnostics;
    using CoreLib.DS.DATATYPES.QUAL;
    using CoreLib.DS.DATATYPES.QUAL.Ctor;
    using static CoreLib.DS.DATATYPES.QUAL.Util.Delegates;
    
    public abstract class BaseDataobj
    {
        public BaseDataobj()
        {
            StackFrame sf = new StackTrace(new StackFrame(true)).GetFrame(0);
            throw new MissingFieldException($"Dynamic Error found at Row({sf.GetFileLineNumber()} Col({sf.GetFileColumnNumber()})"
                                            + $" in File {sf.GetFileName()} when Exec METHOD [{sf.GetMethod().Name}]\n"
                                            + $"[...trace... 缺少初始化此实例(Ast(Abstract)DataObj)所需的参数]\n");
        }
        public BaseDataobj(System.Xml.XmlNode? baseXDataObjNode) // : this()  // TODO:  Remove throwing
        {
            _dataobjTargetNode = baseXDataObjNode;
            try
            {
                try { var nodesArray = (FindNodes(_dataobjTargetNode, "NAME").NodeArrList()).ToArray(); _nameInnerNode = (System.Xml.XmlNode)nodesArray?[0]; } catch { }
                try { var nodesArray = (FindNodes(_dataobjTargetNode, "QUAL").NodeArrList()).ToArray(); _qualInnerNode = (System.Xml.XmlNode)nodesArray?[0]; } catch { }
                ////
                /// Dataobj - Qual 信息用于新的 Matrix 输出
                try { qualInnerObj = new CompoQualImpl(_qualInnerNode); } catch { }
                try {  } catch { }

            }
            catch (Exception nodeErr) { }
        }

        protected virtual String MainSearchKey => throw new NotImplementedException();

        public virtual string MainExternalMapRefernceKey => throw new NotImplementedException();

        protected System.Xml.XmlNode? _dataobjTargetNode;

        public BaseQual qualInnerObj;
        protected XmlNode? _qualInnerNode;
        internal BaseName nameInnerObj;
        protected XmlNode? _nameInnerNode;

    }

    
    namespace Ctor
    {
        public sealed class DataobjImpl : BaseDataobj
        {
            public DataobjImpl() : base() { }

            public DataobjImpl(System.Xml.XmlNode? baseTargetNode) : base(baseTargetNode)
            {
                try
                {
                    XmlAttributeCollection? rc = _dataobjTargetNode?.Attributes;
                    if (rc != null)
                    {
                        foreach (XmlAttribute attr in rc)
                        {

                            if (attr.Name == "oid") { _oid = attr?.Value ?? ""; continue; }
                            else if (attr.Name == "spec") { _spec = attr?.Value ?? ""; continue; }
                            else if (attr.Name == "dtref") { _dtref = attr?.Value ?? ""; continue; }

                            ////
                        }
                    }
                }
                catch (Exception valueNodeErr) { }
            }


            public String ShowMainReferenceKey => MainExternalMapRefernceKey;


            public String QualExternalShowInfo =>  base.qualInnerObj?.Text;

            protected internal String ShowMainSearchId => MainSearchKey;

            protected override String MainSearchKey => _oid;

            public/*protected*/ override string MainExternalMapRefernceKey => _dtref;

            private string _dtref;
            private string _spec;
            private string _oid;

        }
        
    }
    
}

