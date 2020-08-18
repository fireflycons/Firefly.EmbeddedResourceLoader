namespace Firefly.EmbeddedResourceLoader.Tests
{
    using System.IO;

    using FluentAssertions;

    using Xunit;

    public class TempFileResourceClassTests
    {
        /// <summary>
        /// Test that a <see cref="TempFile"/> decorated with <see cref="EmbeddedResourceAttribute"/> receives the content of the embedded resource
        /// </summary>
        [Fact]
        public void ShouldCopyResourceContentToTempFile()
        {
            // Class containing TempFile resources should be disposable to clean up temporary files.
            using var tempFileClass = new TempFileResourceClass();

            ResourceLoader.LoadResources(tempFileClass);

            var content = File.ReadAllText(tempFileClass.FileResource.Path);

            // Assume that the string property is correctly loaded with the same content.
            // Other tests validate that works properly
            content.Should().Be(tempFileClass.ResourceContent);
        }
    }
}