using System;
using System.Runtime.Serialization;

namespace ReloadedRPCS3TestsNoConfig.RPCS3
{
    [Serializable]
    internal class OutOfRPCS3VirtualMemoryException : Exception
    {
        public OutOfRPCS3VirtualMemoryException()
        {
        }

        public OutOfRPCS3VirtualMemoryException( string message ) : base( message )
        {
        }

        public OutOfRPCS3VirtualMemoryException( string message, Exception innerException ) : base( message, innerException )
        {
        }

        protected OutOfRPCS3VirtualMemoryException( SerializationInfo info, StreamingContext context ) : base( info, context )
        {
        }
    }
}