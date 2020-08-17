namespace Firefly.EmbeddedResourceLoader.NewtonsoftJson
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Exception thrown when resource content does not appear to be JSON
    /// </summary>
    /// <seealso cref="Firefly.EmbeddedResourceLoader.ResourceLoaderException" />
    [Serializable]
    public class JsonResourceLoaderException : ResourceLoaderException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonResourceLoaderException"/> class.
        /// </summary>
        public JsonResourceLoaderException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonResourceLoaderException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public JsonResourceLoaderException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonResourceLoaderException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public JsonResourceLoaderException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonResourceLoaderException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"></see> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"></see> that contains contextual information about the source or destination.</param>
        protected JsonResourceLoaderException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}