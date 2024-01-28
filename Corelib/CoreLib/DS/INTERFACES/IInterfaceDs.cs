namespace CoreLib.DS.INTERFACES
{
    public interface IInterfaceDs
    {
    
    }
    
    /// <summary>
    ///  All (NAME | QUAL | CVALUETYPE | PVALUETYPE | STRUCTURE | STRUCTCOMPLEXDT , could implement this interface)
    /// </summary>
    internal interface IDescProp
    {

        String TextPropertyToString();
        internal string _Text();

    }
    
}

