namespace Firefly.EmbeddedResourceLoader
{
    using System;
    using System.Globalization;
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
        /// <param name="resourceLocation">
        /// The resource location.
        /// </param>
        public ResourceLoaderInvalidPathException(EmbeddedResourceAttribute resourceLocation)
            : base(resourceLocation)
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
                if (this.ResourceAttribute != null)
                {
                    return string.Format(
                        CultureInfo.CurrentCulture, 
                        "Unable to load '{0}' from '{1}'. Did you set it to Embedded Resource?", 
                        this.ResourceAttribute.ResourcePath, 
                        this.ResourceAttribute.ContainingAssembly.FullName);
                }

                return base.Message;
            }
        }

        #endregion
    }
}