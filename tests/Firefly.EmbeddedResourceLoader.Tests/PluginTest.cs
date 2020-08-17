namespace Firefly.EmbeddedResourceLoader.Tests
{
    using System;

    using FluentAssertions;

    using Xunit;

    public class PluginTest
    {
        /// <summary>
        /// Tests the plugin system.
        /// </summary>
        [Fact]
        public void ShouldInitializePluggedInClass()
        {
            // Arrange - set up plugin
            ResourceLoader.RegisterPlugin(new[] { new PluginDefinition() });

            // Act - initialize resource
            var instance = new ClassWithPluginMember();
            ResourceLoader.LoadResources(instance);

            // Verify
            instance.PluginSupportedClass.StringValue.Should().Be("TestResource1");
        }

        /// <summary>
        /// Class for which we want to load resources that has a member of the type defined by the plugin
        /// </summary>
        private class ClassWithPluginMember
        {
            /// <summary>
            /// Gets or sets instance of type supported by the plugin.
            /// </summary>
            [EmbeddedResource("Resources.TestResource1.txt")]
            public PluginSupportedClass PluginSupportedClass { get; set; }
        }

        /// <summary>
        /// Defines the plugin to the <see cref="ResourceLoader"/>
        /// </summary>
        /// <seealso cref="IResourceLoaderPlugin" />
        private class PluginDefinition : IResourceLoaderPlugin
        {
            /// <summary>
            /// Gets the format supported by the type's construction.
            /// </summary>
            /// <value>
            /// The resource format.
            /// </value>
            public ResourceFormat ResourceFormat => ResourceFormat.String;

            /// <summary>
            /// Gets the type of the object that will be initialized from embedded resource content.
            /// </summary>
            /// <value>
            /// The type.
            /// </value>
            public Type Type => typeof(PluginSupportedClass);

            /// <summary>
            /// Called to construct the type.
            /// </summary>
            /// <param name="resourceData">The resource data. This will be a <see cref="T:System.String" /> or a <see cref="T:System.IO.Stream" /> depending on the value of <see cref="P:Firefly.EmbeddedResourceLoader.IResourceLoaderPlugin.ResourceFormat" /></param>
            /// <returns>
            /// A constructed object of the plug-in's type
            /// </returns>
            public object GetObject(object resourceData)
            {
                return new PluginSupportedClass { StringValue = (string)resourceData };
            }
        }

        /// <summary>
        /// Type that the plugin will initialize. This could be an object in a third party library.
        /// </summary>
        private class PluginSupportedClass
        {
            /// <summary>
            /// Gets or sets the string value, which will be initialized from embedded resource via the plugin.
            /// </summary>
            /// <value>
            /// The string value.
            /// </value>
            public string StringValue { get; set; }
        }
    }
}