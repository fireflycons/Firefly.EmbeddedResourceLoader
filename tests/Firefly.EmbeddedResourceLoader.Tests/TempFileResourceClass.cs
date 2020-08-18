namespace Firefly.EmbeddedResourceLoader.Tests
{
    using System;

    public class TempFileResourceClass : IDisposable
    {
        [EmbeddedResource("Resources.TestResource1.txt")]
        public TempFile FileResource { get; set; }

        [EmbeddedResource("Resources.TestResource1.txt")]
        public string ResourceContent { get; set; }

        public void Dispose()
        {
            this.FileResource?.Dispose();
        }
    }
}