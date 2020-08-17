namespace Firefly.EmbeddedResourceLoader
{
    using System;

    /// <summary>
    /// Plug-in modules implement this interface to register a plug-in with <see cref="EmbeddedResourceLoader"/>
    /// </summary>
    public interface IResourceLoaderPlugin
    {
        /// <summary>
        /// Gets the format supported by the type's construction.
        /// </summary>
        /// <value>
        /// The resource format.
        /// </value>
        ResourceFormat ResourceFormat { get; }

        /// <summary>
        /// Gets the type of the object that will be initialized from embedded resource content.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        Type Type { get; }

        /// <summary>
        /// <para>
        /// Called to construct the type. An implementation of this interface should construct an object of the given <see cref="Type"/>, load the embedded resource and return the object.
        /// </para>
        /// </summary>
        /// <param name="resourceData">The resource data. This will be a <see cref="string"/> or a <see cref="System.IO.Stream"/> depending on the value of <see cref="ResourceFormat"/></param>
        /// <returns>A constructed object of the plug-in's type</returns>
        object GetObject(object resourceData);
    }
}