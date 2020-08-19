namespace Firefly.EmbeddedResourceLoader.Exceptions
{
    using System;

    /// <summary>
    /// Exception thrown if the decorated member is read only, e.g. a property with no setter.
    /// </summary>
    [Serializable]
    public class ResourceLoaderReadOnlyException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderReadOnlyException"/> class.
        /// </summary>
        public ResourceLoaderReadOnlyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderReadOnlyException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ResourceLoaderReadOnlyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderReadOnlyException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public ResourceLoaderReadOnlyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderReadOnlyException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ResourceLoaderReadOnlyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}