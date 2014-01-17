using System;
using System.Runtime.Serialization;

namespace Jwc.Funz
{
    /// <summary>
    /// Exception thrown by the container when a service cannot be resolved.
    /// </summary>
    [Serializable]
    public class ResolutionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionException"/> class.
        /// </summary>
        public ResolutionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionException"/> class
        /// with a message.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ResolutionException(string message) : base(message)
        {
            if (message == null)
                throw new ArgumentNullException("message");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionException"/> class
        /// with a message and an inner exception.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ResolutionException(string message, Exception innerException) : base(message, innerException)
        {
            if (message == null)
                throw new ArgumentNullException("message");

            if (innerException == null)
                throw new ArgumentNullException("innerException");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolutionException"/> class.
        /// </summary>
        protected ResolutionException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}