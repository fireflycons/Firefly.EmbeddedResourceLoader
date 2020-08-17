namespace Firefly.EmbeddedResourceLoader
{
    using System;
    using System.Reflection;

    /// <summary>
    /// <see cref="IMember"/> implementation for a property
    /// </summary>
    internal class PropertyMember : IMember
    {
        /// <summary>
        /// The property info
        /// </summary>
        private readonly PropertyInfo propertyInfo;

        /// <summary>
        /// The instance
        /// </summary>
        private readonly object instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMember"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="instance">The instance.</param>
        public PropertyMember(PropertyInfo propertyInfo, object instance)
        {
            this.propertyInfo = propertyInfo;
            this.instance = instance;
        }

        #region IMember

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly => !this.propertyInfo.CanWrite;

        /// <summary>
        /// Gets a value indicating whether this instance is static.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is static; otherwise, <c>false</c>.
        /// </value>
        public bool IsStatic => false;

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized => this.propertyInfo.GetValue(this.instance, null) != null;

        /// <summary>
        /// Gets the type of the target.
        /// </summary>
        /// <value>
        /// The type of the target.
        /// </value>
        public Type TargetType => this.propertyInfo.PropertyType;

        /// <summary>
        /// Applies the loaded resource to the contained member.
        /// </summary>
        /// <param name="resourceData">The resource data.</param>
        public void SetValue(object resourceData)
        {
            this.propertyInfo.SetValue(this.instance, resourceData, null);
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return ResourceLoader.GetMemberName(this.propertyInfo);
        }
    }
}
