namespace CoreLib.DS.DATATYPES.COMPLEX.StructComplex
{
    using System;
    using System.Xml;
    using System.Diagnostics;
    using CoreLib.DS.DATATYPES.QUAL;
    using CoreLib.DS.DATATYPES.QUAL.Ctor;
    using CoreLib.DS.DATATYPES.COMMONSTRUCTURE.COMMONOBJ.BaseDataobj;
    using CoreLib.DS.DATATYPES.COMMONSTRUCTURE.COMMONOBJ.BaseDataobj.Ctor;
    using CoreLib.DS.DATATYPES.COMMONSTRUCTURE;
    using static CoreLib.DS.DATATYPES.QUAL.Util.Delegates;
    
    
    public abstract class StructComplex : Structure
    {
        public StructComplex() : base()
        {

        }
        public StructComplex(System.Xml.XmlNode? baseXStructComplexNode) : base(baseXStructComplexNode)
        {

            try
            {
                try { var nodesArray = (FindNodes(baseXStructComplexNode, "NAME").NodeArrList()).ToArray(); _nameInnerNode = (System.Xml.XmlNode)nodesArray?[0]; } catch { }
                try { var nodesArray = (FindNodes(baseXStructComplexNode, "QUAL").NodeArrList()).ToArray(); _qualInnerNode = (System.Xml.XmlNode)nodesArray?[0]; } catch { }
                ////

            }
            catch (Exception nodeErr) { }

        }


        public/*protected*/ override /*virtual*/ String MainSearchKey =>  //throw new NotImplementedException();
            this.DtRef;                                    //  也是作为寻找 IDENT 类型的外键
        public String? DtRef { get { return _dtref; } protected set { _dtref = value; } }
        private string? _dtref;
        public String? Spec { get { return _spec; } protected set { _spec = value; } }
        private string? _spec;
        public String? Temploid { get { return _temploid; } protected set { _temploid = value; } }
        private string? _temploid;
        public String? Oid { get { return _oid; } protected set { _oid = value; } }
        private string? _oid;

        public BaseQual structQualInnerObj;
        protected XmlNode? _qualInnerNode;
        internal BaseName structNameInnerObj;
        protected XmlNode? _nameInnerNode;

        public virtual string QualExternalShowInfoOfStructInfo => throw new NotImplementedException("methods QualExternalShowInfoOfStructInfo not implemented in Abstract StructComplex!");

    }


    namespace Ctor
    {
        
        public/*internal*/ sealed class StructComplexImpl : StructComplex
        {

            public StructComplexImpl() : base() { }

            public StructComplexImpl(System.Xml.XmlNode? targetStructComplexDtNode) : base(targetStructComplexDtNode)
            {
                //try { var nodesArray = (FindNodes(targetStructComplexDtNode, "NAME").NodeArrList()).ToArray(); _nameInnerNode = (System.Xml.XmlNode)nodesArray?[0]; } catch { }
                //try { var nodesArray = (FindNodes(targetStructComplexDtNode, "QUAL").NodeArrList()).ToArray(); _nameInnerNode = (System.Xml.XmlNode)nodesArray?[0]; } catch { }

                try
                {
                    structQualInnerObj = new CompoQualImpl(_qualInnerNode);
                    structNameInnerObj = new CompoNameImpl(_nameInnerNode);
                }
                catch { }
                try
                {
                    XmlAttributeCollection? rc = targetStructComplexDtNode?.Attributes;
                    if (rc != null)
                    {
                        foreach (XmlAttribute attr in rc)
                        {

                            if (attr.Name == "oid") { Oid = attr?.Value ?? ""; continue; }
                            else if (attr.Name == "temploid") { Temploid = attr?.Value ?? ""; continue; }
                            else if (attr.Name == "spec") { Spec = attr?.Value ?? ""; continue; }
                            else if (attr.Name == "dtref") { DtRef = attr?.Value ?? ""; continue; }

                            ////
                        }
                    }
                }
                catch (Exception valueNodeErr) { }
            }

            public override string MainSearchKey => base.MainSearchKey;


            public override string QualExternalShowInfoOfStructInfo => structQualInnerObj?.Text;

        }
        
    }
    
    
}
