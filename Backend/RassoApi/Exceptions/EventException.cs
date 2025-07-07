using System.Runtime.Serialization;

namespace RassoApi.Exceptions
{
    internal class EventException : Exception
    {
        public EventException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EventException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        internal EventException(string message) : base(message) { }
    }
}
