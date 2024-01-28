namespace CoreLib.DS.DATATYPES.COMMONSTRUCTURE
{
    
    using System;
    using System.Xml;
    using System.Diagnostics;
    using CoreLib.DS.DATATYPES.QUAL;
    using CoreLib.DS.DATATYPES.QUAL.Ctor;
    using CoreLib.DS.DATATYPES.COMMONSTRUCTURE.COMMONOBJ.BaseDataobj;
    using CoreLib.DS.DATATYPES.COMMONSTRUCTURE.COMMONOBJ.BaseDataobj.Ctor;
    using static CoreLib.DS.DATATYPES.QUAL.Util.Delegates;
    using CoreLib.DS.DATATYPES.COMPLEX.StructComplex;/*.Ctor;*/
    
    
    
    public/*internal*/ abstract class Structure
        {
            public Structure()
            {
                StackFrame sf = new StackTrace(new StackFrame(true)).GetFrame(0);
                throw new MissingFieldException($"Dynamic Error found at Row({sf.GetFileLineNumber()} Col({sf.GetFileColumnNumber()})"
                    + $" in File {sf.GetFileName()} when Exec METHOD [{sf.GetMethod().Name}]\n"
                    + $"[...trace... 缺少初始化此实例(Ast(Abstract)Structure)所需的参数]\n");
            }
            public Structure(System.Xml.XmlNode? baseXStructureNode) // : this()  // TODO:  Remove throwing
            {
                _structureTargetNode = baseXStructureNode;
                _dataobjInnerNodesList = new List<System.Xml.XmlNode?>();
                var dataobjNodesArr = (FindNodes(_structureTargetNode, "Dataobj").NodeArrList()).ToArray();
                if (dataobjNodesArr != null) foreach (var node in dataobjNodesArr) { _dataobjInnerNodesList.Add((System.Xml.XmlNode)node); }
                try
                {

                    List<BaseDataobj > innerDataobjObjList =
                        (from System.Xml.XmlNode _dataobjInnerNode in _dataobjInnerNodesList
                         select (new DataobjImpl(_dataobjInnerNode))).Cast<BaseDataobj>().ToList();
                    DataobjInnerObjArr = innerDataobjObjList.ToArray();
                    // TOTO: IEnumerable - DataobjObj

                }
                catch (Exception dataobjnodesErr) { }
            }


            public virtual IEnumerable<BaseDataobj> GetDataobjInstanceEnemerator()
            {
                if (DataobjInnerObjArr == null || DataobjInnerObjArr.Length < 1)
                    yield break; //return null;

                int length = DataobjInnerObjArr.Length;
                //while (length-- > 0)
                int startIdx = 0;
                do
                {
                    var tmp = DataobjInnerObjArr[startIdx++];
                    yield return tmp;
                }
                while (startIdx < length);

            }


            public /*internal*/ virtual IEnumerable<StructComplex> GetStructInnerEnumer()
            {

                throw new NotImplementedException("Abstract structure cannot have this menmber function call!");

            }


            public/*protected*/ virtual String MainSearchKey => throw new NotImplementedException();

            protected System.Xml.XmlNode? _structureTargetNode;

            public BaseDataobj[] DataobjInnerObjArr;
            protected List<XmlNode?>? _dataobjInnerNodesList;

            
        }


    namespace  Ctor
    {
        using CoreLib.DS.DATATYPES.COMPLEX.StructComplex.Ctor;/**/
        
        internal sealed class StructureImpl : Structure
            {
                public StructureImpl() : base() { }

                public StructureImpl(System.Xml.XmlNode? targetStructureNode) : base(targetStructureNode)
                {
                    try 
                    {
                        _structManyNodes = new List<XmlNode?>();
                        var structNodesArr = (FindNodes(_structureTargetNode, "Struct").NodeArrList()).ToArray();
                        if (structNodesArr != null) foreach (var node in structNodesArr) { _structManyNodes.Add((System.Xml.XmlNode)node); }
                        
                    }
                    catch { }


                    try
                    {
                        List<StructComplex> innerStructComplexList =
                            (from System.Xml.XmlNode _structComplexInnerNode in _structManyNodes
                             select (new StructComplexImpl(_structComplexInnerNode))).Cast<StructComplex>().ToList();
                        StructComplexInnerObjArr = innerStructComplexList.ToArray();
                        // TOTO: IEnumerable - StructComplex

                    }
                    catch (Exception innerStructComplexList) { }

                }

                public override IEnumerable<StructComplex> GetStructInnerEnumer()
                {


                    if (StructComplexInnerObjArr == null || StructComplexInnerObjArr.Length < 1)
                        yield break; //return null;

                    int length = StructComplexInnerObjArr.Length;
                    //while (length-- > 0)
                    int startIdx = 0;
                    do
                    {
                        var tmp = StructComplexInnerObjArr[startIdx++];
                        yield return tmp;
                    }
                    while (startIdx < length);
                }

                
                // public List<dataobj> innerStructForMoreInfoDataObjArr;

                public StructComplex[] StructComplexInnerObjArr { get { return _innerStructArrForMoreInfo; } private set { _innerStructArrForMoreInfo = value; } }

                private StructComplex[] _innerStructArrForMoreInfo;
                private List<XmlNode?> _structManyNodes;

            }
    }
    
}

