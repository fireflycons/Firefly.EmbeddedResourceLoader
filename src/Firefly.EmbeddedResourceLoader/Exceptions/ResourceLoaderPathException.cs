namespace Firefly.EmbeddedResourceLoader.Exceptions
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
        /// <param name="resourcePath">The resource path.</param>
        /// <param name="containingAssembly">The containing assembly.</param>
        protected ResourceLoaderPathException(string resourcePath, Assembly containingAssembly)
        {
            this.ResourcePath = resourcePath;
            this.ResourceLocation = containingAssembly;
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
        public Assembly ResourceLocation { get; }

        /// <summary>
        ///     Gets the resource path as entered in the attribute's constructor.
        /// </summary>
        /// <value>
        ///     The resource path.
        /// </value>
        public string ResourcePath { get; }

        #endregion
    }
}