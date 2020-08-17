namespace Firefly.EmbeddedResourceLoader
{
    using System;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    /// Base class for exceptions thrown when a resource path cannot be resolved.
    /// </summary>
    [Serializable]
    public abstract class ResourceLoaderPathException : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderPathException"/> class.
        /// </summary>
        protected ResourceLoaderPathException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderPathException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        protected ResourceLoaderPathException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderPathException"/> class.
        /// </summary>
        /// <param name="resourceAttribute">
        /// The resource attribute.
        /// </param>
        protected ResourceLoaderPathException(EmbeddedResourceAttribute resourceAttribute)
        {
            this.ResourceAttribute = resourceAttribute;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderPathException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="inner">
        /// The inner.
        /// </param>
        protected ResourceLoaderPathException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoaderPathException"/> class.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected ResourceLoaderPathException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets the assembly containing, or expected to contain the resource.
        /// </summary>
        /// <value>
        ///     The resource location.
        /// </value>
        public Assembly ResourceLocation => this.ResourceAttribute?.ContainingAssembly;

        /// <summary>
        ///     Gets the resource path as entered in the attribute's constructor.
        /// </summary>
        /// <value>
        ///     The resource path.
        /// </value>
        public string ResourcePath => this.ResourceAttribute?.ResourcePath;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the resource attribute.
        /// </summary>
        protected EmbeddedResourceAttribute ResourceAttribute { get; set; }

        #endregion
    }
}