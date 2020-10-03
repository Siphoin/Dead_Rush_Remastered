
using System;

public  class TransformParentNullExpection : Exception
    {

    public TransformParentNullExpection() : base() { }
    public TransformParentNullExpection(string message) : base(message) { }
    public TransformParentNullExpection(string message, Exception inner) : base(message, inner) { }

    protected TransformParentNullExpection(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
