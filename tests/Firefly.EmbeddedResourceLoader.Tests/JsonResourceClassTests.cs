namespace Firefly.EmbeddedResourceLoader.Tests
{
    using FluentAssertions;

    using Xunit;

    public class JsonResourceClassTests : IClassFixture<JsonResourceClassFixture>
    {
        /// <summary>
        /// Fixture contains a resource-loaded instance of <see cref="JsonResourceClass"/>
        /// </summary>
        private readonly JsonResourceClassFixture fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonResourceClassTests"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public JsonResourceClassTests(JsonResourceClassFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public void ShouldInitializeJObjectProperty()
        {
            this.fixture.ResourceClass.Object.Should().NotBeNull();
        }
    }
}
