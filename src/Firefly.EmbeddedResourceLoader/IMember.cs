namespace Firefly.EmbeddedResourceLoader
{
    using System;

    /// <summary>
    /// Interface that abstracts the differences between the implementation of fields and properties 
    /// </summary>
    internal interface IMember
    {
        /// <summary>
        /// Gets a value indicating whether the member is read only.
        /// </summary>
        /// <value>
        /// <c>true</c> if the member is read only; otherwise, <c>false</c>.
        /// </value>
        bool IsReadOnly { get; }

        /// <summary>
        /// Gets a value indicating whether the member is static.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the member is static; otherwise, <c>false</c>.
        /// </value>
        bool IsStatic { get; }

        /// <summary>
        /// Gets a value indicating whether the member is initialized.
        /// </summary>
        /// <value>
        /// <c>true</c> if the member is initialized; otherwise, <c>false</c>.
        /// </value>
        bool IsInitialized { get; }

        /// <summary>
        /// Gets the type of the target.
        /// </summary>
        /// <value>
        /// The type of the target.
        /// </value>
        Type TargetType { get; }

        /// <summary>
        /// Applies the loaded resource to the contained member.
        /// </summary>
        /// <param name="resourceData">The resource data.</param>
        void SetValue(object resourceData);
    }
}
