namespace Firefly.EmbeddedResourceLoader.Tests
{
    using System;

    public class JsonResourceClassFixture : IDisposable
    {
        public JsonResourceClassFixture()
        {
            this.ResourceClass = new JsonResourceClass();

            ResourceLoader.LoadResources(this.ResourceClass);
        }

        public JsonResourceClass ResourceClass { get; }

        public void Dispose()
        {
        }
    }
}
