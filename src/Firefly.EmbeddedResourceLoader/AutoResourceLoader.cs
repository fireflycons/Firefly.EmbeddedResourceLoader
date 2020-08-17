namespace Firefly.EmbeddedResourceLoader
{
    /// <summary>
    /// <para>
    /// The lazy person's resource loader!
    /// </para>
    /// <para>
    /// Classes inherited from this with fields or properties decorated with <see cref="EmbeddedResourceAttribute"/> will load resources at construction
    /// so long as the default constructor is called
    /// </para>
    /// </summary>
    public abstract class AutoResourceLoader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoResourceLoader"/> class.
        /// </summary>
        protected AutoResourceLoader()
        {
            ResourceLoader.LoadResources(this);
        }
    }
}