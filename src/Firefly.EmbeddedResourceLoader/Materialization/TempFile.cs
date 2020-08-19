namespace Firefly.EmbeddedResourceLoader.Materialization
{
    using System;
    using System.IO;

    /// <summary>
    /// Provides file backing for an embedded resource. It is up to the caller to dispose any <c>TempFile</c> instances to ensure clean-up of files created.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class TempFile : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TempFile"/> class.
        /// </summary>
        /// <param name="resourceData">The resource data to copy to the file.</param>
        public TempFile(Stream resourceData)
        {
            this.FullPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".tmp");

            using (var fs = File.OpenWrite(this.FullPath))
            {
                resourceData.CopyTo(fs);
            }
        }

        /// <summary>
        /// Gets the full path to the temporary file.
        /// </summary>
        /// <value>
        /// The full path to the temporary file
        /// </value>
        public string FullPath { get; }

        /// <summary>
        /// Removes the temporary file.
        /// </summary>
        public void Dispose()
        {
            if (File.Exists(this.FullPath))
            {
                File.Delete(this.FullPath);
            }
        }
    }
}