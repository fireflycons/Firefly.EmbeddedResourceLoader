namespace Firefly.EmbeddedResourceLoader.Tests
{
    using System;

    using Firefly.EmbeddedResourceLoader.Materialization;

    public class TempFileResourceClass : IDisposable
    {
        [EmbeddedResource("Resources.TestResource1.txt")]
        public TempFile FileResource { get; set; }

        [EmbeddedResource("Resources.TestResource1.txt")]
        public string ResourceContent { get; set; }

        [EmbeddedResource("TempDirectory")]
        public TempDirectory TempDirectory { get; set; }


        public void Dispose()
        {
            this.FileResource?.Dispose();
            this.TempDirectory?.Dispose();
        }
    }
}