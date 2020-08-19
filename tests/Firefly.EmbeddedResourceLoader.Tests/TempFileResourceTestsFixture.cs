using System;
using System.Collections.Generic;
using System.Text;

namespace Firefly.EmbeddedResourceLoader.Tests
{
    public class TempFileResourceTestsFixture : IDisposable
    {
        public TempFileResourceTestsFixture()
        {
            this.Resources = new TempFileResourceClass();
            ResourceLoader.LoadResources(this.Resources);
        }

        public TempFileResourceClass Resources { get; }

        public void Dispose()
        {
            this.Resources?.Dispose();
        }
    }
}
