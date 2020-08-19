namespace Firefly.EmbeddedResourceLoader.Exceptions
{
    using System;
    using System.Globalization;

    /// <summary>
    /// <para>
    /// Exception thrown if the type of the decorated member is not supported by this library.
    /// </para>
    /// <para>
    /// Note that value types are always initialized with their default value and as such are initialized, therefore they are ignored rather than this exception being thrown.
    /// </para>
    /// </summary>
    [Serializable]
    public class ResourceLoaderInvalidTypeException : ResourceLoaderException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderInvalidTypeException"/> class.
        /// </summary>
        public ResourceLoaderInvalidTypeException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderInvalidTypeException"/> class.
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        public ResourceLoaderInvalidTypeException(Type targetType)
            : base(FormatUnsupportedTypeMessage(targetType))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderInvalidTypeException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ResourceLoaderInvalidTypeException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderInvalidTypeException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public ResourceLoaderInvalidTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderInvalidTypeException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        protected ResourceLoaderInvalidTypeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Formats the unsupported type message.
        /// </summary>
        /// <param name="targetType">Type of the target.</param>
        /// <returns>Formatted error message.</returns>
        private static string FormatUnsupportedTypeMessage(Type targetType)
        {
            return targetType.IsArray
                       ? string.Format(
                           CultureInfo.InvariantCulture,
                           "The attribute target is an array type, which is not supported by the Embedded resource Loader")
                       : string.Format(
                           CultureInfo.CurrentCulture,
                           "Type '{0}' of the attribute target is not yet supported by the Embedded resource Loader",
                           targetType.FullName);
        }
    }
}