using System;
using System.Collections.Generic;
using System.Text;

namespace Firefly.EmbeddedResourceLoader.NewtonsoftJson
{
    using Newtonsoft.Json.Linq;

    public class JArrayResourceLoader : JsonResourceLoader
    {
        /// <summary>
        /// Gets the type of the object that will be initialized from embedded resource content.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public override Type Type => typeof(JArray);

        /// <summary>
        /// Gets the format supported by the type's construction.
        /// </summary>
        /// <value>
        /// The resource format.
        /// </value>
        public override ResourceFormat ResourceFormat => ResourceFormat.String;

        /// <summary>
        /// Called to construct the type.
        /// </summary>
        /// <param name="resourceData">The resource data. This will be a <see cref="T:System.String" /> or a <see cref="T:System.IO.Stream" /> depending on the value of <see cref="P:Firefly.EmbeddedResourceLoader.IResourceLoaderPlugin.ResourceFormat" /></param>
        /// <returns>
        /// A constructed object of the plug-in's type
        /// </returns>
        public override object GetObject(object resourceData)
        {
            var json = (string)resourceData;

            if (this.GetResourceType(json) != JsonType.Array)
            {
                throw new JsonResourceLoaderException("Embedded resource data is not JSON array.");
            }

            return JArray.Parse(json);
        }
    }
}
