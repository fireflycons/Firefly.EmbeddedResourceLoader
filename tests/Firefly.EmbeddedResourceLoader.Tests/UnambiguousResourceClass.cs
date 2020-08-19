namespace Firefly.EmbeddedResourceLoader.Tests
{
    using Firefly.EmbeddedResourceLoader.Exceptions;

#pragma warning disable 169

    /// <summary>
    /// Class to demonstrate <see cref="ResourceLoaderAmbiguousPathException"/> not thrown when the resource name is unambiguous.
    /// </summary>
    internal class UnambiguousResourceClass
    {
        #region Fields

        [EmbeddedResource("Resources.AmbiguousResource.txt")]
        private string string1;

        [EmbeddedResource("Resources2.AmbiguousResource.txt")]
        private string string2;

        #endregion
    }
}