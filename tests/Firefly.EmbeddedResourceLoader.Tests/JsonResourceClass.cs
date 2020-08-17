namespace Firefly.EmbeddedResourceLoader.Tests
{
    using Newtonsoft.Json.Linq;

    public class JsonResourceClass
    {
        [EmbeddedResource("Array.json")]
        public JArray Array { get; set; }

        [EmbeddedResource("Object.json")]
        public JObject Object { get; set; }
    }
}