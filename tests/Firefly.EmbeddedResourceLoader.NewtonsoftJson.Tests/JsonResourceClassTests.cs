using System;
using Xunit;

namespace Firefly.EmbeddedResourceLoader.NewtonsoftJson.Tests
{
    using FluentAssertions;

    public class InstanceResourceClassTests : IClassFixture<InstanceResourceClassFixture>
    {
        /// <summary>
        /// Fixture contains a resource-loaded instance of <see cref="InstanceResourceClass"/>
        /// </summary>
        private readonly InstanceResourceClassFixture fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceResourceClassTests"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public InstanceResourceClassTests(InstanceResourceClassFixture fixture)
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
