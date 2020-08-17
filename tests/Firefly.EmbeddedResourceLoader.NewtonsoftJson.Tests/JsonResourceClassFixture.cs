using System;
using System.Collections.Generic;
using System.Text;

namespace Firefly.EmbeddedResourceLoader.NewtonsoftJson.Tests
{
    public class InstanceResourceClassFixture : IDisposable
    {
        public InstanceResourceClassFixture()
        {
            JsonResourceLoader.RegisterPlugin();

            this.ResourceClass = new InstanceResourceClass();

            ResourceLoader.LoadResources(this.ResourceClass);
        }

        public InstanceResourceClass ResourceClass { get; }

        public void Dispose()
        {
        }
    }
}
