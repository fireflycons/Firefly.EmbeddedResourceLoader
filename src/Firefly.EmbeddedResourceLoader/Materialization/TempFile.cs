namespace Firefly.EmbeddedResourceLoader.Materialization
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Provides file backing for an embedded resource. It is up to the caller to dispose any <c>TempFile</c> instances to ensure clean-up of files created.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class TempFile : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TempFile"/> class.
        /// It is not created until written to
        /// </summary>
        /// <param name="fileExtension">File extension to use for the temp file.</param>
        public TempFile(string fileExtension = ".tmp")
        {
            this.FullPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + fileExtension);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TempFile"/> class.
        /// </summary>
        /// <param name="resourceData">The resource data to copy to the file.</param>
        /// <param name="fileExtension">File extension to use for the temp file.</param>
        public TempFile(Stream resourceData, string fileExtension = ".tmp")
            : this(fileExtension)
        {
            this.Write(resourceData);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TempFile"/> class.
        /// </summary>
        /// <param name="content">String content to copy to the file.</param>
        /// <param name="fileExtension">File extension to use for the temp file.</param>
        public TempFile(string content, string fileExtension = ".tmp")
            : this(fileExtension)
        {
            this.Write(content);
        }

        /// <summary>
        /// Gets the full path to the temporary file.
        /// </summary>
        /// <value>
        /// The full path to the temporary file
        /// </value>
        public string FullPath { get; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="TempFile"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="self">The <see cref="TempFile"/> object.</param>
        /// <returns>
        /// The fill path to the temp file.
        /// </returns>
        public static implicit operator string(TempFile self)
        {
            return self.FullPath;
        }

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

        /// <summary>
        /// Writes data from stream to the temp file.
        /// </summary>
        /// <param name="streamData">Stream to write to the file.</param>
        public void Write(Stream streamData)
        {
            using (var fs = File.OpenWrite(this.FullPath))
            {
                streamData.CopyTo(fs);
            }
        }

        /// <summary>
        ///  Writes data from string to the temp file.
        /// </summary>
        /// <param name="stringData">String to write to the file.</param>
        public void Write(string stringData)
        {
            File.WriteAllText(this.FullPath, stringData, new UTF8Encoding(false));
        }
    }
}