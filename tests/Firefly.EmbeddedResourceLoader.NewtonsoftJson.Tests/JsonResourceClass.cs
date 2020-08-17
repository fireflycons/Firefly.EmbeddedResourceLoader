namespace Firefly.EmbeddedResourceLoader.NewtonsoftJson.Tests
{
    using Newtonsoft.Json.Linq;

    public class InstanceResourceClass
    {
        [EmbeddedResource("Array.json")]
        public JArray Array { get; set; }

        [EmbeddedResource("Object.json")]
        public JObject Object { get; set; }
    }
}