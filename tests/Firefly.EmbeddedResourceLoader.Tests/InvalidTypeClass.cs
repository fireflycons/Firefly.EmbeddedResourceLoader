namespace Firefly.EmbeddedResourceLoader.Tests
{
    #pragma warning disable 169

    /// <summary>
    /// Asserts that a class that isn't directly supported by the resource loader
    /// will cause a <see cref="ResourceLoaderInvalidTypeException"/>
    /// </summary>
    public class InvalidTypeClass
    {
        [EmbeddedResource("Resources.TestResource1.txt")]
        private SomeOtherType invalidType;

        private class SomeOtherType
        {
            public string StringValue { get; set; }
        }
    }
}