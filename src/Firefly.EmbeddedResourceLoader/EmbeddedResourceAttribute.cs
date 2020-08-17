namespace Firefly.EmbeddedResourceLoader
{
    using System;
    using System.Reflection;

    /// <summary>
    /// <para>
    /// Attribute that declares an embedded resource to load.
    /// </para>
    /// <para>
    /// Indicates that the named resource should be loaded into the decorated field or property
    /// when the class instance is passed to <see cref="ResourceLoader.LoadResources"/>.
    /// </para>
    /// <seealso cref="ResourceLoader.LoadResources"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class EmbeddedResourceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedResourceAttribute"/> class.
        /// </summary>
        /// <param name="resourcePath">The resource path.</param>
        /// <remarks>
        /// <para>
        /// Resource is assumed to be contained within the assembly containing the declaration of the decorated class.
        /// </para>
        /// <para>
        /// You don't need to give the complete path including the whole namespace, only enough to make the requested resource
        /// unambiguous from any other similarly named resource in the assembly.
        /// </para>
        /// </remarks>
        public EmbeddedResourceAttribute(string resourcePath)
            : this(resourcePath, (Assembly)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedResourceAttribute"/> class.
        /// </summary>
        /// <remarks>
        /// You don't need to give the complete path including the whole namespace, only enough to make the requested resource
        /// unambiguous from any other similarly named resource in the assembly.
        /// </remarks>
        /// <param name="resourcePath">The resource path.</param>
        /// <param name="assemblyFullyQualifiedName">Fully qualified name of assembly to load resource from.</param>
        public EmbeddedResourceAttribute(string resourcePath, string assemblyFullyQualifiedName)
            : this(resourcePath, Assembly.Load(assemblyFullyQualifiedName))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EmbeddedResourceAttribute"/> class.
        /// </summary>
        /// <remarks>
        /// You don't need to give the complete path including the whole namespace, only enough to make the requested resource
        /// unambiguous from any other similarly named resource in the assembly.
        /// </remarks>
        /// <param name="resourcePath">The resource path.</param>
        /// <param name="containingAssembly">The assembly that contains the resource.</param>
        public EmbeddedResourceAttribute(string resourcePath, Assembly containingAssembly)
        {
            this.ContainingAssembly = containingAssembly ?? Assembly.GetCallingAssembly();
            this.ResourcePath = resourcePath;   
        }

        /// <summary>
        /// Gets the containing assembly, that is, the assembly that is expected to contain the embedded resource.
        /// </summary>
        /// <value>
        /// The containing assembly.
        /// </value>
        public Assembly ContainingAssembly { get; internal set; }

        /// <summary>
        /// Gets the resource path.
        /// </summary>
        /// <value>
        /// The resource path.
        /// </value>
        public string ResourcePath { get; }
    }
}