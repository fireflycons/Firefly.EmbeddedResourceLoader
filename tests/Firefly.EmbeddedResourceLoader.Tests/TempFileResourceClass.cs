namespace Firefly.EmbeddedResourceLoader.Tests
{
    using System;
    using System.Collections.Generic;

    using Firefly.EmbeddedResourceLoader.Materialization;

    public class TempFileResourceClass : IDisposable
    {
        [EmbeddedResource("Resources.TestResource1.txt")]
        public TempFile FileResource { get; set; }

        [EmbeddedResource("Resources.TestResource1.txt")]
        public string ResourceContent { get; set; }

        [EmbeddedResource("TempDirectory")]
        public TempDirectory TempDirectory { get; set; }

        [EmbeddedResource("TempDirectoryWithHyphen")]
        public TempDirectory HyphenatedDirectory { get; set; }

        [EmbeddedResource("TempDirectoryWithHyphen", DirectoryRenames = new[] { "hyphenated_directory", "hyphenated-directory" })]
        public TempDirectory HyphenatedDirectoryWithRename { get; set; }

        public void Dispose()
        {
            this.FileResource?.Dispose();
            this.TempDirectory?.Dispose();
            this.HyphenatedDirectory?.Dispose();
            this.HyphenatedDirectoryWithRename?.Dispose();
        }
    }
}