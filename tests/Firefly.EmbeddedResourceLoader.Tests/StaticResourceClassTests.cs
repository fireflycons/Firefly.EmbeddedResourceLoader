namespace Firefly.EmbeddedResourceLoader.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using FluentAssertions;

    using Xunit;

    public class StaticResourceClassTests : IClassFixture<StaticResourceClassFixture>
    {
        private readonly StaticResourceClassFixture fixture;

        public StaticResourceClassTests(StaticResourceClassFixture fixture)
        {
            this.fixture = fixture;
        }

        /// <summary>
        /// Stream constructed types are types that have a constructor taking a single <see cref="Stream"/> argument, e.g. <see cref="System.Drawing.Bitmap"/>
        /// </summary>
        /// <param name="visibility">The visibility.</param>
        [Theory]
        [InlineData("private")]
        [InlineData("internal")]
        [InlineData("public")]
        public void ShouldInitializeStreamContructedField(string visibility)
        {
            var fieldValue = visibility switch
                {
                    "private" => StaticResourceClass.GetPrivateBitmapField(),
                    "internal" => StaticResourceClass.GetInternalBitmapField(),
                    "public" => StaticResourceClass.GetPublicBitmapField(),
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
        [InlineData("internal")]
        [InlineData("public")]
        public void ShouldInitializeStreamContructedProperty(string visibility)
        {
            var propertyValue = visibility switch
                {
                    "private" => StaticResourceClass.GetPrivateBitmapProperty(),
                    "internal" => StaticResourceClass.GetInternalBitmapProperty(),
                    "public" => StaticResourceClass.GetPublicBitmapProperty(),
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
        [InlineData("internal")]
        [InlineData("public")]
        public void ShouldInitializeStreamLoadedField(string visibility)
        {
            var fieldValue = visibility switch
                {
                    "private" => StaticResourceClass.GetPrivateXDocumentField(),
                    "internal" => StaticResourceClass.GetInternalXDocumentField(),
                    "public" => StaticResourceClass.GetPublicXDocumentField(),
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
        [InlineData("internal")]
        [InlineData("public")]
        public void ShouldInitializeStreamLoadedProperty(string visibility)
        {
            var propertyValue = visibility switch
                {
                    "private" => StaticResourceClass.GetPrivateXDocumentProperty(),
                    "internal" => StaticResourceClass.GetInternalXDocumentProperty(),
                    "public" => StaticResourceClass.GetPublicXDocumentProperty(),
                    _ => throw new ArgumentException($"Unsupported visibility {visibility}", nameof(visibility))
                };

            propertyValue.Descendants("Child").ToArray()[0].FirstAttribute.Value.Should().Be("1");
        }

        /// <summary>
        /// Private static fields should be initialized
        /// </summary>
        [Theory]
        [InlineData("private")]
        [InlineData("internal")]
        [InlineData("public")]
        public void ShouldInitializeStringField(string visibility)
        {
            var fieldValue = visibility switch
                {
                    "private" => StaticResourceClass.GetPrivateStaticStringField(),
                    "internal" => StaticResourceClass.GetInternalStaticStringField(),
                    "public" => StaticResourceClass.GetPublicStaticStringField(),
                    _ => throw new ArgumentException($"Unsupported visibility {visibility}", nameof(visibility))
                };

            fieldValue.Should().Be("TestResource1");
        }

        /// <summary>
        /// Static properties should be initialized
        /// </summary>
        [Theory]
        [InlineData("private")]
        [InlineData("internal")]
        [InlineData("public")]
        public void ShouldInitializeStringProperty(string visibility)
        {
            var propertyValue = visibility switch
                {
                    "private" => StaticResourceClass.GetPrivateStaticStringProperty(),
                    "internal" => StaticResourceClass.GetInternalStaticStringProperty(),
                    "public" => StaticResourceClass.GetPublicStaticStringProperty(),
                    _ => throw new ArgumentException($"Unsupported visibility {visibility}", nameof(visibility))
                };

            propertyValue.Should().Be("TestResource1");
        }
    }
}