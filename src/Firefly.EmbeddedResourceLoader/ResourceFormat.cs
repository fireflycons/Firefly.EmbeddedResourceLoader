namespace Firefly.EmbeddedResourceLoader
{
    /// <summary>
    /// Type of resource supported by a plugin
    /// </summary>
    public enum ResourceFormat
    {
        /// <summary>
        /// Resource content should be provided as string
        /// </summary>
        String,

        /// <summary>
        /// Resource content should be provided as stream
        /// </summary>
        Stream
    }
}