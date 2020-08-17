namespace Firefly.EmbeddedResourceLoader.Tests
{
    using System;

    public class StaticResourceClassFixture : IDisposable
    {
        public StaticResourceClassFixture()
        {
            ResourceLoader.LoadResources(typeof(StaticResourceClass));
        }

        public void Dispose()
        {
        }
    }
}