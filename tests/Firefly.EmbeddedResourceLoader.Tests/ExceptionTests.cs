namespace Firefly.EmbeddedResourceLoader.Tests
{
    using System;

    using FluentAssertions;

    using Xunit;

    /// <summary>
    /// The ambiguous resource tests.
    /// </summary>
    public class ExceptionTests
    {
        /// <summary>
        /// The test exception not thrown when resource path unambiguous.
        /// </summary>
        [Fact]
        public void ShouldNotThrowWhenResourcePathUnambiguous()
        {
            var c = new UnambiguousResourceClass();

            Action action = () => ResourceLoader.LoadResources(c);

            action.Should().NotThrow();
        }

        /// <summary>
        /// Test that <see cref="ResourceLoaderAmbiguousPathException"/> is thrown if the resource path is ambiguous.
        /// </summary>
        [Fact]
        public void ShouldThrowWhenResourcePathAmbiguous()
        {
            var c = new AmbiguousResourceClass();

            Action action = () => ResourceLoader.LoadResources(c);

            action.Should().Throw<ResourceLoaderAmbiguousPathException>();
        }

        /// <summary>
        /// Test that <see cref="ResourceLoaderReadOnlyException"/> is thrown if a read-only property is decorated.
        /// </summary>
        [Fact]
        public void ShouldThrowWhenPropertyIsReadOnly()
        {
            var c = new ReadOnlyPropertyClass();

            Action action = () => ResourceLoader.LoadResources(c);

            action.Should().Throw<ResourceLoaderReadOnlyException>();
        }

        /// <summary>
        /// Test that <see cref="ResourceLoaderInvalidTypeException"/> is thrown if a property is of a type that cannot be resource-loaded.
        /// </summary>
        [Fact]
        public void ShoudThrowWhenTypeIsNotResourceLoadable()
        {
            var c = new InvalidTypeClass();

            Action action = () => ResourceLoader.LoadResources(c);

            action.Should().Throw<ResourceLoaderInvalidTypeException>().WithMessage("Type '*' of the attribute target is not yet supported by the Embedded resource Loader");
        }
    }
}