namespace Firefly.EmbeddedResourceLoader.Tests
{
    #pragma warning disable 169

    /// <summary>
    /// Class to demonstrate <see cref="ResourceLoaderAmbiguousPathException"/> is thrown when the resource path is ambiguous
    /// i.e. more than one resource matches the given name.
    /// </summary>
    internal class AmbiguousResourceClass
    {
        #region Fields

        /// <summary>
        ///     The string1
        /// </summary>
        [EmbeddedResource("AmbiguousResource.txt")]
        private string string1;

        #endregion
    }
}