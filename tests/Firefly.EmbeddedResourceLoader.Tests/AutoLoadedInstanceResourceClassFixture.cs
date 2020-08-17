namespace Firefly.EmbeddedResourceLoader.Tests
{
    using System;

    public class AutoLoadedInstanceResourceClassFixture : IDisposable
    {
        public AutoLoadedInstanceResourceClassFixture()
        {
            this.ResourceClass = new AutoLoadedInstanceResourceClass();
        }

        public AutoLoadedInstanceResourceClass ResourceClass { get; }

        public void Dispose()
        {
        }
    }
}