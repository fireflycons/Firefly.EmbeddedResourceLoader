namespace Firefly.EmbeddedResourceLoader.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using FluentAssertions;

    using Xunit;

    /// <summary>
    /// Resource loading on an instantiated class
    /// </summary>
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

        /*
                        /// <summary>
                        /// Placing a resource attribute on a read only property should throw
                        /// </summary>
                        [Fact]
                        public void ShouldThrowWhenAttributedPropertyIsReadOnly()
                        {
                            var p = new ReadOnlyProperty();
                
                            Action action = () => ResourceLoader.LoadResources(p);
                
                            action.Should().Throw<ResourceLoaderReadOnlyException>();
                        }
                
                        /// <summary>
                        /// If a property has an initial value, this should not be overwritten by resource content
                        /// </summary>
                        [Fact]
                        public void ShouldNotOverwriteInitializedProperty()
                        {
                            const string InitialValue = "I have a value";
                
                            var p = new PublicStringProperty { TestProp = InitialValue };
                
                            ResourceLoader.LoadResources(p);
                
                            p.TestProp.Should().Be(InitialValue);
                        }
                */

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
                    "private" => InstanceResourceClass.GetPrivateStaticStringField(),
                    "protected" => InstanceResourceClass.GetProtectedStaticStringField(),
                    "internal" => InstanceResourceClass.GetInternalStaticStringField(),
                    "public" => InstanceResourceClass.GetPublicStaticStringField(),
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
                    "private" => InstanceResourceClass.GetPrivateStaticStringProperty(),
                    "protected" => InstanceResourceClass.GetProtectedStaticStringProperty(),
                    "internal" => InstanceResourceClass.GetInternalStaticStringProperty(),
                    "public" => InstanceResourceClass.GetPublicStaticStringProperty(),
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