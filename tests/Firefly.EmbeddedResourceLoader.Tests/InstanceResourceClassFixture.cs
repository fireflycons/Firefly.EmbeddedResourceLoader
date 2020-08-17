namespace Firefly.EmbeddedResourceLoader.Tests
{
    using System;

    public class InstanceResourceClassFixture : IDisposable
    {
        public InstanceResourceClassFixture()
        {
            this.ResourceClass = new InstanceResourceClass();

            ResourceLoader.LoadResources(this.ResourceClass);
        }

        public InstanceResourceClass ResourceClass { get; }

        public void Dispose()
        {
        }
    }
}