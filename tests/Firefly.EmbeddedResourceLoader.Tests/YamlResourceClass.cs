namespace Firefly.EmbeddedResourceLoader.Tests
{
    using YamlDotNet.RepresentationModel;

    public class YamlResourceClass
    {
        [EmbeddedResource("Array.yaml")]
        public YamlStream YamlArray { get; set; }

        [EmbeddedResource("Object.yaml")]
        public YamlStream YamlObject { get; set; }
    }
}