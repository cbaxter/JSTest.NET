using System;
using System.Runtime.Serialization;

namespace JSTest.ScriptElements
{
    [Serializable]
    public class MissingEmbeddedResourceException : Exception
    {
        public MissingEmbeddedResourceException()
            : this("Unable to find embedded resource.")
        { }

        public MissingEmbeddedResourceException(String message)
            : base(message)
        { }

        public MissingEmbeddedResourceException(String message, Exception inner)
            : base(message, inner)
        { }

        protected MissingEmbeddedResourceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
