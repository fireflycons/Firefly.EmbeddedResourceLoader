namespace Firefly.EmbeddedResourceLoader.Exceptions
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    ///     Exception thrown when the resource path cannot be found in the given assembly. The developer may have included it, but forgot to set it to embedded resource.
    /// </summary>
    [Serializable]
    public class ResourceLoaderInvalidPathException : ResourceLoaderPathException
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResourceLoaderInvalidPathException" /> class.
        /// </summary>
        public ResourceLoaderInvalidPathException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderInvalidPathException"/> class.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        /// <param name="containingAssembly">The containing assembly.</param>
        public ResourceLoaderInvalidPathException(string resourcePath, Assembly containingAssembly)
            : base(resourcePath, containingAssembly)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderInvalidPathException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        public ResourceLoaderInvalidPathException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderInvalidPathException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public ResourceLoaderInvalidPathException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderInvalidPathException"/> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object
        ///     data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual
        ///     information about the source or destination.
        /// </param>
        protected ResourceLoaderInvalidPathException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets a message that describes the current exception.
        /// </summary>
        /// <returns>The error message that explains the reason for the exception, or an empty string("").</returns>
        public override string Message
        {
            get
            {
                if (this.ResourcePath != null)
                {
                    return string.Format(
                        CultureInfo.CurrentCulture, 
                        "Unable to load '{0}' from '{1}'. Did you set it to Embedded Resource?", 
                        this.ResourcePath, 
                        this.ResourceLocation?.FullName);
                }

                return base.Message;
            }
        }

        #endregion
    }
}