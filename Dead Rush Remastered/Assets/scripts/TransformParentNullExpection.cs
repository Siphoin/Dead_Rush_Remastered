
    public  class TransformParentNullExpection : System.Exception
    {

    public TransformParentNullExpection() : base() { }
    public TransformParentNullExpection(string message) : base(message) { }
    public TransformParentNullExpection(string message, System.Exception inner) : base(message, inner) { }

    protected TransformParentNullExpection(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
