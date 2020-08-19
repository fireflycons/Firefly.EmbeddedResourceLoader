namespace Firefly.EmbeddedResourceLoader.Exceptions
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    /// Thrown if an attempt to initialize a <see cref="TempDirectory"/> does not yield any resources.
    /// </summary>
    /// <seealso cref="ResourceLoaderPathException" />
    [Serializable]
    public class ResourceLoaderInvalidDirectoryException : ResourceLoaderPathException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderInvalidDirectoryException"/> class.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        /// <param name="containingAssembly">The containing assembly.</param>
        public ResourceLoaderInvalidDirectoryException(string resourcePath, Assembly containingAssembly)
            : base(resourcePath, containingAssembly)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderInvalidDirectoryException"/> class.
        /// </summary>
        protected ResourceLoaderInvalidDirectoryException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderInvalidDirectoryException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        protected ResourceLoaderInvalidDirectoryException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderInvalidDirectoryException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        protected ResourceLoaderInvalidDirectoryException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderInvalidDirectoryException"/> class.
        /// </summary>
        /// <param name="info">The info.</param>
        /// <param name="context">The context.</param>
        protected ResourceLoaderInvalidDirectoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message
        {
            get
            {
                if (this.ResourcePath != null)
                {
                    return string.Format(
                        CultureInfo.CurrentCulture,
                        "Unable to find any resource directory given path {0} in assembly {1}",
                        this.ResourcePath,
                        this.ResourceLocation?.FullName);
                }

                return base.Message;
            }
        }
    }
}