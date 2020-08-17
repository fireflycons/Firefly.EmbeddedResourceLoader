namespace Firefly.EmbeddedResourceLoader.Tests
{
    using FluentAssertions;

    using Xunit;

    public class YamlResourceClassTests : IClassFixture<YamlResourceClassFixture>
    {
        /// <summary>
        /// Fixture contains a resource-loaded instance of <see cref="YamlResourceClass"/>
        /// </summary>
        private readonly YamlResourceClassFixture fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="YamlResourceClassTests"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public YamlResourceClassTests(YamlResourceClassFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void ShouldInitializeYamlObjectProperty()
        {
            this.fixture.ResourceClass.YamlObject.Should().NotBeNull();
            this.fixture.ResourceClass.YamlObject.Documents.Count.Should().Be(1);
        }

        [Fact]
        public void ShouldInitializeYamlArraytProperty()
        {
            this.fixture.ResourceClass.YamlArray.Should().NotBeNull();
            this.fixture.ResourceClass.YamlObject.Documents.Count.Should().Be(1);
        }
    }
}
