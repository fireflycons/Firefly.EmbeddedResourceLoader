namespace Firefly.EmbeddedResourceLoader.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using FluentAssertions;

    using Xunit;

    public class AutoLoadedInstanceResourceClassTests : IClassFixture<AutoLoadedInstanceResourceClassFixture>
    {
        private readonly AutoLoadedInstanceResourceClassFixture fixture;

        public AutoLoadedInstanceResourceClassTests(AutoLoadedInstanceResourceClassFixture fixture)
        {
            this.fixture = fixture;
        }

        /// <summary>
        /// Private static fields should be initialized
        /// </summary>
        [Theory]
        [InlineData("private")]
        [InlineData("protected")]
        [InlineData("internal")]
        [InlineData("public")]
        public void ShouldInitializeStaticStringField(string visibility)
        {
            var fieldValue = visibility switch
                {
                    "private" => AutoLoadedInstanceResourceClass.GetPrivateStaticStringField(),
                    "protected" => AutoLoadedInstanceResourceClass.GetProtectedStaticStringField(),
                    "internal" => AutoLoadedInstanceResourceClass.GetInternalStaticStringField(),
                    "public" => AutoLoadedInstanceResourceClass.GetPublicStaticStringField(),
                    _ => throw new ArgumentException($"Unsupported visibility {visibility}", nameof(visibility))
                };

            fieldValue.Should().Be("TestResource1");
        }

        /// <summary>
        /// Static properties should be initialized
        /// </summary>
        [Theory]
        [InlineData("private")]
        [InlineData("protected")]
        [InlineData("internal")]
        [InlineData("public")]
        public void ShouldInitializeStaticStringProperty(string visibility)
        {
            var propertyValue = visibility switch
                {
                    "private" => AutoLoadedInstanceResourceClass.GetPrivateStaticStringProperty(),
                    "protected" => AutoLoadedInstanceResourceClass.GetProtectedStaticStringProperty(),
                    "internal" => AutoLoadedInstanceResourceClass.GetInternalStaticStringProperty(),
                    "public" => AutoLoadedInstanceResourceClass.GetPublicStaticStringProperty(),
                    _ => throw new ArgumentException($"Unsupported visibility {visibility}", nameof(visibility))
                };

            propertyValue.Should().Be("TestResource1");
        }

        /// <summary>
        /// Stream constructed types are types that have a constructor taking a single <see cref="Stream"/> argument, e.g. <see cref="System.Drawing.Bitmap"/>
        /// </summary>
        /// <param name="visibility">The visibility.</param>
        [Theory]
        [InlineData("private")]
        [InlineData("protected")]
        [InlineData("internal")]
        [InlineData("public")]
        public void ShouldInitializeStreamConstructedField(string visibility)
        {
            var fieldValue = visibility switch
                {
                    "private" => this.fixture.ResourceClass.GetPrivateBitmapField(),
                    "protected" => this.fixture.ResourceClass.GetProtectedBitmapField(),
                    "internal" => this.fixture.ResourceClass.GetInternalBitmapField(),
                    "public" => this.fixture.ResourceClass.GetPublicBitmapField(),
                    _ => throw new ArgumentException($"Unsupported visibility {visibility}", nameof(visibility))
                };

            fieldValue.Should().NotBeNull();
        }

        /// <summary>
        /// Stream constructed types are types that have a constructor taking a single <see cref="Stream"/> argument, e.g. <see cref="System.Drawing.Bitmap"/>
        /// </summary>
        /// <param name="visibility">The visibility.</param>
        [Theory]
        [InlineData("private")]
        [InlineData("protected")]
        [InlineData("internal")]
        [InlineData("public")]
        public void ShouldInitializeStreamConstructedProperty(string visibility)
        {
            var propertyValue = visibility switch
                {
                    "private" => this.fixture.ResourceClass.GetPrivateBitmapProperty(),
                    "protected" => this.fixture.ResourceClass.GetProtectedBitmapProperty(),
                    "internal" => this.fixture.ResourceClass.GetInternalBitmapProperty(),
                    "public" => this.fixture.ResourceClass.GetPublicBitmapProperty(),
                    _ => throw new ArgumentException($"Unsupported visibility {visibility}", nameof(visibility))
                };

            propertyValue.Should().NotBeNull();
        }

        /// <summary>
        /// Stream loaded types are types that have a static <c>Load</c> method taking a single <see cref="Stream"/> argument, e.g. <see cref="XDocument"/>
        /// </summary>
        /// <param name="visibility">The visibility.</param>
        [Theory]
        [InlineData("private")]
        [InlineData("protected")]
        [InlineData("internal")]
        [InlineData("public")]
        public void ShouldInitializeStreamLoadedField(string visibility)
        {
            var fieldValue = visibility switch
                {
                    "private" => this.fixture.ResourceClass.GetPrivateXDocumentField(),
                    "protected" => this.fixture.ResourceClass.GetProtectedXDocumentField(),
                    "internal" => this.fixture.ResourceClass.GetInternalXDocumentField(),
                    "public" => this.fixture.ResourceClass.GetPublicXDocumentField(),
                    _ => throw new ArgumentException($"Unsupported visibility {visibility}", nameof(visibility))
                };

            fieldValue.Descendants("Child").ToArray()[0].FirstAttribute.Value.Should().Be("1");
        }

        /// <summary>
        /// Stream loaded types are types that have a static <c>Load</c> method taking a single <see cref="Stream"/> argument, e.g. <see cref="XDocument"/>
        /// </summary>
        /// <param name="visibility">The visibility.</param>
        [Theory]
        [InlineData("private")]
        [InlineData("protected")]
        [InlineData("internal")]
        [InlineData("public")]
        public void ShouldInitializeStreamLoadedProperty(string visibility)
        {
            var propertyValue = visibility switch
                {
                    "private" => this.fixture.ResourceClass.GetPrivateXDocumentProperty(),
                    "protected" => this.fixture.ResourceClass.GetProtectedXDocumentProperty(),
                    "internal" => this.fixture.ResourceClass.GetInternalXDocumentProperty(),
                    "public" => this.fixture.ResourceClass.GetPublicXDocumentProperty(),
                    _ => throw new ArgumentException($"Unsupported visibility {visibility}", nameof(visibility))
                };

            propertyValue.Descendants("Child").ToArray()[0].FirstAttribute.Value.Should().Be("1");
        }

        [Theory]
        [InlineData("private")]
        [InlineData("protected")]
        [InlineData("internal")]
        [InlineData("public")]
        public void ShouldInitializeStringField(string visibility)
        {
            var fieldValue = visibility switch
                {
                    "private" => this.fixture.ResourceClass.GetPrivateStringField(),
                    "protected" => this.fixture.ResourceClass.GetProtectedStringField(),
                    "internal" => this.fixture.ResourceClass.GetInternalStringField(),
                    "public" => this.fixture.ResourceClass.GetPublicStringField(),
                    _ => throw new ArgumentException($"Unsupported visibility {visibility}", nameof(visibility))
                };

            fieldValue.Should().Be("TestResource1");
        }

        [Theory]
        [InlineData("private")]
        [InlineData("protected")]
        [InlineData("internal")]
        [InlineData("public")]
        [InlineData("public virtual")]
        public void ShouldInitializeStringProperty(string visibility)
        {
            var propetyValue = visibility switch
                {
                    "private" => this.fixture.ResourceClass.GetPrivateStringProperty(),
                    "protected" => this.fixture.ResourceClass.GetProtectedStringProperty(),
                    "internal" => this.fixture.ResourceClass.GetInternalStringProperty(),
                    "public" => this.fixture.ResourceClass.GetPublicStringProperty(),
                    "public virtual" => this.fixture.ResourceClass.GetPublicVirtualStringProperty(),
                    _ => throw new ArgumentException($"Unsupported visibility {visibility}", nameof(visibility))
                };

            propetyValue.Should().Be("TestResource1");
        }
    }
}