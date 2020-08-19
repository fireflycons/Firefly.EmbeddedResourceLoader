namespace Firefly.EmbeddedResourceLoader.Exceptions
{
    using System;

    /// <summary>
    /// Base class for exceptions thrown by the resource loading mechanism
    /// </summary>
    [Serializable]
    public class ResourceLoaderException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderException"/> class.
        /// </summary>
        public ResourceLoaderException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ResourceLoaderException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public ResourceLoaderException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ResourceLoaderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}