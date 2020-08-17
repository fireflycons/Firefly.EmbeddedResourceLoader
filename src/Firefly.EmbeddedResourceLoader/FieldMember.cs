namespace Firefly.EmbeddedResourceLoader
{
    using System;
    using System.Reflection;

    /// <summary>
    /// <see cref="IMember"/> implementation for a field.
    /// </summary>
    internal class FieldMember : IMember
    {
        /// <summary>
        /// The field info
        /// </summary>
        private readonly FieldInfo fieldInfo;

        /// <summary>
        /// The class instance containing the field
        /// </summary>
        private readonly object instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldMember"/> class.
        /// </summary>
        /// <param name="fieldInfo">The field info.</param>
        /// <param name="instance">The instance.</param>
        public FieldMember(FieldInfo fieldInfo, object instance)
        {
            this.fieldInfo = fieldInfo;
            this.instance = instance;
        }

        #region IMember

        /// <summary>
        /// Gets a value indicating whether this instance is read only.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is read only; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets a value indicating whether this instance is static.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is static; otherwise, <c>false</c>.
        /// </value>
        public bool IsStatic => this.fieldInfo.IsStatic;

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is initialized; otherwise, <c>false</c>.
        /// </value>
        public bool IsInitialized => this.fieldInfo.GetValue(this.instance) != null;

        /// <summary>
        /// Gets the type of the target.
        /// </summary>
        /// <value>
        /// The type of the target.
        /// </value>
        public Type TargetType => this.fieldInfo.FieldType;

        /// <summary>
        /// Applies the loaded resource to the contained member.
        /// </summary>
        /// <param name="resourceData">The resource data.</param>
        public void SetValue(object resourceData)
        {
            this.fieldInfo.SetValue(this.instance, resourceData);
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
            return ResourceLoader.GetMemberName(this.fieldInfo);
        }
    }
}
