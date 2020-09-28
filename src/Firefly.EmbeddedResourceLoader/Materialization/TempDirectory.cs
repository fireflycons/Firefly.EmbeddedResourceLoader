namespace Firefly.EmbeddedResourceLoader.Materialization
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// <para>
    /// An object that materializes a collection of embedded resources to a directory structure
    /// </para>
    /// <para>
    /// For this to work properly, embedded resource files are all expected to have file extensions.
    /// This is because the internal manifest format uses dot as a separator character therefore it
    /// is impossible to programmatically determine whether the last dot in a resource name is a directory
    /// separator or a file extension. The assumption used here is that it is a file extension.
    /// </para>
    /// <para>
    /// The compiler has a nasty trick when embedding folders into the resource manifest.
    /// These folders are effectively namespace names when committed to the assembly resource manifest and as such must confirm to .NET lexical structure rules for identifiers.
    /// All invalid characters as per this specification are replaced with underscore, and if the first character of the folder name is invalid,
    /// then an underscore is prefixed to the folder name.
    /// </para>
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    /// <seealso href="https://fireflycons.github.io/Firefly-EmbeddedResourceLoader/articles/materializers.html#tempdirectory">Notes on using TempDirectory</seealso>
    public class TempDirectory : IDisposable
    {
        /// <summary>
        /// <para>
        /// Initializes a new instance of the <see cref="TempDirectory"/> class. It is up to the caller to dispose any <c>TempDirectory</c> instances to ensure clean-up of files created.
        /// </para>
        /// <para>
        /// Convenience constructor allowing this class to be used for any temp directory requirements.
        /// Resource materialization is performed by an internal constructor.
        /// </para>
        /// </summary>
        public TempDirectory()
        {
            this.FullPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".tmp");
            Directory.CreateDirectory(this.FullPath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TempDirectory"/> class.
        /// </summary>
        /// <param name="containingAssembly">The assembly containing the embedded resources.</param>
        /// <param name="resourcePathPrefix">Resource path prefix of resources to materialize. All resources with this prefix in their names will be extracted.</param>
        /// <param name="resources">The resources to materialize.</param>
        /// <param name="directoryRenames">Array of renames for directories in resource path.</param>
        internal TempDirectory(
            Assembly containingAssembly,
            string resourcePathPrefix,
            IEnumerable<string> resources,
            string[] directoryRenames)
            : this()
        {
            var index = resourcePathPrefix.Length;

            foreach (var resource in resources)
            {
                // Caveat! Expect all resource files to have a file extension.
                var filePath = Path.Combine(
                    this.FullPath,
                    ResourceNameToFilePath(resource.Substring(index), directoryRenames));
                var fileDirectory = Path.GetDirectoryName(filePath);

                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }

                using (var stream = containingAssembly.GetManifestResourceStream(resource))
                using (var fs = File.OpenWrite(filePath))
                {
                    // ReSharper disable once PossibleNullReferenceException - resource known to exist
                    stream.CopyTo(fs);
                }
            }
        }

        /// <summary>
        /// Gets the full path to the temporary directory.
        /// </summary>
        /// <value>
        /// The full path to the temporary directory.
        /// </value>
        public string FullPath { get; }

        /// <summary>
        /// Performs an implicit conversion from <see cref="TempDirectory"/> to <see cref="System.String"/>.
        /// </summary>
        /// <param name="self">The <see cref="TempDirectory"/> object.</param>
        /// <returns>
        /// The fill path to the temp file.
        /// </returns>
        public static implicit operator string(TempDirectory self)
        {
            return self.FullPath;
        }

        /// <summary>
        /// Removes the temporary directory and all its content.
        /// </summary>
        public void Dispose()
        {
            if (Directory.Exists(this.FullPath))
            {
                try
                {
                    Directory.Delete(this.FullPath, true);
                }
                catch
                {
                    // Ignore as we are only in user's temp space
                }
            }
        }

        /// <summary>
        /// Convert a resource name to a file system path.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <param name="directoryRenames">Array of renames for directories in resource path.</param>
        /// <returns>File system path relative to the temporary directory</returns>
        private static string ResourceNameToFilePath(string resource, string[] directoryRenames)
        {
            var parts = resource.Split('.').Reverse().ToArray();

            if (directoryRenames != null && directoryRenames.Any())
            {
                if (directoryRenames.Length % 2 != 0)
                {
                    throw new ArgumentException(
                        "DirectoryRenames property of EmbeddedResouceAttribute must have an even number of elements");
                }

                // While we have the resource name split to parts
                // First two parts or resource are file.ext
                var sourceRenames = directoryRenames.TakeEvery(2).ToArray();
                var targetRenames = directoryRenames.TakeEvery(2, 1).ToArray();

                for (var partIndex = 2; partIndex < parts.Length; ++partIndex)
                {
                    for (var renameIndex = 0; renameIndex < sourceRenames.Length; ++renameIndex)
                    {
                        if (parts[partIndex] == sourceRenames[renameIndex])
                        {
                            parts[partIndex] = targetRenames[renameIndex];
                        }
                    }
                }
            }

            return string.Join(Path.DirectorySeparatorChar.ToString(), parts.Skip(1).Reverse()) + "." + parts.First();
        }
    }
}