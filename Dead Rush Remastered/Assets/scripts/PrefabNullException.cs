using System;

public class PrefabNullException : Exception
{
    public PrefabNullException() : base() { }
    public PrefabNullException(string message) : base(message) { }
    public PrefabNullException(string message, Exception inner) : base(message, inner) { }

    protected PrefabNullException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
