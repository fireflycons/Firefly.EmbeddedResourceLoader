namespace Firefly.EmbeddedResourceLoader.Exceptions
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    ///     Exception thrown when a partial resource path matches more than one resource in the target assembly.
    /// </summary>
    [Serializable]
    public class ResourceLoaderAmbiguousPathException : ResourceLoaderPathException
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResourceLoaderAmbiguousPathException" /> class.
        /// </summary>
        public ResourceLoaderAmbiguousPathException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderAmbiguousPathException"/> class.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        /// <param name="containingAssembly">The containing assembly.</param>
        public ResourceLoaderAmbiguousPathException(string resourcePath, Assembly containingAssembly)
            : base(resourcePath, containingAssembly)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderAmbiguousPathException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ResourceLoaderAmbiguousPathException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderAmbiguousPathException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public ResourceLoaderAmbiguousPathException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderAmbiguousPathException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected ResourceLoaderAmbiguousPathException(SerializationInfo info, StreamingContext context)
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
                        "Ambiguous resource path. Multiple matches for '{0}' in '{1}'", 
                        this.ResourcePath, 
                        this.ResourceLocation?.FullName);
                }

                return base.Message;
            }
        }

        #endregion
    }
}