namespace Firefly.EmbeddedResourceLoader.Tests
{
    using System.IO;
    using System.Linq;

    using Firefly.EmbeddedResourceLoader.Materialization;

    using FluentAssertions;

    using Xunit;

    /// <summary>
    /// Tests for <see cref="TempFile"/> and <see cref="TempDirectory"/> resource materialization.
    /// </summary>
    public class TempFileResourceClassTests : IClassFixture<TempFileResourceTestsFixture>
    {
        private readonly TempFileResourceTestsFixture fixture;

        public TempFileResourceClassTests(TempFileResourceTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        /// <summary>
        /// Test that a <see cref="TempFile"/> decorated with <see cref="EmbeddedResourceAttribute"/> receives the content of the embedded resource
        /// </summary>
        [Fact]
        public void ShouldCopyResourceContentToTempFile()
        {
            // Class containing TempFile resources should be dispo
            var content = File.ReadAllText(this.fixture.Resources.FileResource.FullPath);

            // Assume that the string property is correctly loaded with the same content.
            // Other tests validate that works properly
            content.Should().Be(this.fixture.Resources.ResourceContent);
        }

        /// <summary>
        /// Test that a collection of resources identified by a partial manifest path are extracted to a directory.
        /// Also tests for bug in Issue #1
        /// </summary>
        [Fact]
        public void ShouldMaterializeTempDirectoryContainingResources()
        {
            Directory.Exists(this.fixture.Resources.TempDirectory.FullPath).Should().BeTrue();
            var files = Directory.GetFiles(this.fixture.Resources.TempDirectory.FullPath);

            files.Should().HaveCount(2);
            files.Select(Path.GetFileName).Should().Contain(new[] { "test.json", "test.yaml" });
        }

        /// <summary>
        /// Verifies a nasty behaviour where <c>directory-name</c> is materialized as <c>directory_name</c>.
        /// Seems that's how the embedded resource manifest sees it.
        /// </summary>
        [Fact]
        public void ShouldMaterializeDirectoryNameWithHyphenConvertedToUnderscore()
        {
            Directory.Exists(Path.Combine(this.fixture.Resources.HyphenatedDirectory.FullPath, "hyphenated_directory"))
                .Should().BeTrue();
        }

        /// <summary>
        /// Verify that using a rename list on the attribute correctly materializes the directory.
        /// </summary>
        [Fact]
        public void ShouldMaterializeDirectoryNameWithHyphenCorrectlyWhenRenamePresentOnAttribute()
        {
            Directory.Exists(Path.Combine(this.fixture.Resources.HyphenatedDirectoryWithRename.FullPath, "hyphenated-directory"))
                .Should().BeTrue();
        }

        /// <summary>
        /// Verifies a TempFile can be used like a string, e.g. for IO operations taking path.
        /// </summary>
        [Fact]
        public void TempFileShouldCastToStringAndResultInPathToFile()
        {
            string GetAsString(string tempFile)
            {
                return tempFile;
            }

            GetAsString(this.fixture.Resources.FileResource).Should().Be(this.fixture.Resources.FileResource.FullPath);
        }

        /// <summary>
        /// Verifies a TempFile can be used like a string, e.g. for IO operations taking path.
        /// </summary>
        [Fact]
        public void TempDirectoryShouldCastToStringAndResultInPathToDirectory()
        {
            string GetAsString(string tempDirectory)
            {
                return tempDirectory;
            }

            GetAsString(this.fixture.Resources.TempDirectory).Should().Be(this.fixture.Resources.TempDirectory.FullPath);
        }

        /// <summary>
        /// Verifies a TemFile resource with PreserveExtension does preserve the extension
        /// </summary>
        [Fact]
        public void TempFileWithPreserveExtensionShouldHaveCorrectExtension()
        {
            Path.GetExtension(this.fixture.Resources.XmlFileWithExtension).Should().Be(".xml");
        }

        [Fact]
        public void GetFileResourceCreatesTempFile()
        {
            using var tempFile = ResourceLoader.GetFileResource("test.yaml");

            File.Exists(tempFile).Should().BeTrue();
        }

        [Fact]
        public void GetFileResourceWithPreserveExtensionCreatesTempFileWithCorrectExtension()
        {
            using var tempFile = ResourceLoader.GetFileResource("test.yaml", true);
            Path.GetExtension(tempFile).Should().Be(".yaml");
        }
    }
}