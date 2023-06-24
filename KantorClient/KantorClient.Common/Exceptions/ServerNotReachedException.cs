using System.Runtime.Serialization;

namespace KantorClient.Common.Exceptions
{
    [Serializable]
    public class ServerNotReachedException : Exception
    {
        public ServerNotReachedException()
        {
        }

        public ServerNotReachedException(string? message) : base(message)
        {
        }

        public ServerNotReachedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ServerNotReachedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
