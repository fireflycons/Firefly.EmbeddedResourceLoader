namespace Firefly.EmbeddedResourceLoader.NewtonsoftJson
{
    using System;
    using System.Linq;

    /// <summary>
    /// Base class for JSON resource loaders
    /// </summary>
    /// <seealso cref="Firefly.EmbeddedResourceLoader.IResourceLoaderPlugin" />
    public abstract class JsonResourceLoader : IResourceLoaderPlugin
    {
        /// <summary>
        /// Gets the format supported by the type's construction.
        /// </summary>
        /// <value>
        /// The resource format.
        /// </value>
        public abstract ResourceFormat ResourceFormat { get; }

        /// <summary>
        /// Gets the type of the object that will be initialized from embedded resource content.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public abstract Type Type { get; }

        /// <summary>
        /// Called to construct the type.
        /// </summary>
        /// <param name="resourceData">The resource data. This will be a <see cref="T:System.String" /> or a <see cref="T:System.IO.Stream" /> depending on the value of <see cref="P:Firefly.EmbeddedResourceLoader.IResourceLoaderPlugin.ResourceFormat" /></param>
        /// <returns>
        /// A constructed object of the plug-in's type
        /// </returns>
        public abstract object GetObject(object resourceData);

        /// <summary>
        /// Registers the plugin.
        /// </summary>
        public static void RegisterPlugin()
        {
            ResourceLoader.RegisterPlugin(
                new[] { (IResourceLoaderPlugin)new JArrayResourceLoader(), new JObjectResourceLoader() });
        }

        /// <summary>
        /// Gets the type of the resource.
        /// </summary>
        /// <param name="resourceData">The resource data.</param>
        /// <returns>Indicator as to whether the resource data contains a JSON object or array</returns>
        /// <exception cref="Firefly.EmbeddedResourceLoader.NewtonsoftJson.JsonResourceLoaderException">Embedded resource data is not JSON</exception>
        internal JsonType GetResourceType(string resourceData)
        {
            switch (resourceData.Trim().FirstOrDefault())
            {
                case '[':

                    return JsonType.Array;

                case '{':

                    return JsonType.Object;

                default:

                    throw new JsonResourceLoaderException("Embedded resource data is not JSON.");
            }
        }
    }
}