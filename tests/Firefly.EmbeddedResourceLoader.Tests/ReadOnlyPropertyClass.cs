// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReadOnlyProperty.cs" company="">
//   
// </copyright>
// <summary>
//   The read only property.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Firefly.EmbeddedResourceLoader.Tests
{
    /// <summary>
    /// The read only property.
    /// </summary>
    internal class ReadOnlyPropertyClass
    {
        #region Public Properties

        /// <summary>
        /// Gets the test prop.
        /// </summary>
        [EmbeddedResource(
            "Resources.TestResource1.txt")]
        public string TestProp => string.Empty;

        #endregion
    }
}