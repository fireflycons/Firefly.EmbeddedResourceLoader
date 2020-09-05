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
    }
}