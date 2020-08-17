namespace Firefly.EmbeddedResourceLoader.Tests
{
    using System;

    public class YamlResourceClassFixture : IDisposable
    {
        public YamlResourceClassFixture()
        {
            this.ResourceClass = new YamlResourceClass();

            ResourceLoader.LoadResources(this.ResourceClass);
        }

        public YamlResourceClass ResourceClass { get; }

        public void Dispose()
        {
        }
    }
}
