namespace Firefly.EmbeddedResourceLoader
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// <para>
    /// This class does all the magic.
    /// Given a type, or an instance of a type containing fields or properties decorated with the <see cref="EmbeddedResourceAttribute"/>
    /// it loads up all the fields and properties with the indicated embedded resources.
    /// </para>
    /// <para>
    /// Resources are only loaded into members that have not previously been initialized (i.e are <c>null</c>).
    /// This also has the beneficial side effect of ensuring that static members are only ever loaded once, which
    /// is when the first instance of the containing type is presented.
    /// </para>
    /// </summary>
    /// <remarks>
    /// The load process works by looking for an appropriate way of initializing the member in the following order...
    /// <list type="number">
    /// <item>
    /// <description>If the member is a string, then read the resource as a string and assign it directly.</description>
    /// </item>
    /// <item>
    /// <description>Look for a constructor of the member's type that takes a single argument of <see cref="Stream"/>. Good for many binary types like images and so forth.</description>
    /// </item>
    /// <item>
    /// <description>Look for a public static or instance Load method taking a single argument of type <see cref="Stream"/> or <see cref="StreamReader"/>, e.g. <see cref="System.Xml.Linq.XDocument"/>.</description>
    /// </item>
    /// <item>
    /// <description>Look for a public static Parse method taking a single argument of type <see cref="string"/>, e.g. <c>Newtonsoft.Json.Linq.JObject</c>.</description>
    /// </item>
    /// <item>
    /// <description>Search registered plugins for a plugin that supports the <see cref="System.Type"/> of the field or property.</description>
    /// </item>
    /// <item>
    /// <description>Throw an exception.</description>
    /// </item>
    /// </list>
    /// </remarks>
    public static class ResourceLoader
    {
        /// <summary>
        /// Constructors and methods that would be able to use to load an embedded resource.
        /// Instance methods imply availability of a default public constructor for the type.
        /// </summary>
        private static readonly List<ResourceLoaderMethod> MethodsToCheck = new List<ResourceLoaderMethod>
                                                                                {
                                                                                    new ConstructorLoadedResource
                                                                                        {
                                                                                            ArgumentType =
                                                                                                typeof(Stream)
                                                                                        },
                                                                                    new StaticMethodLoadedResource
                                                                                        {
                                                                                            MethodName = "Load",
                                                                                            ArgumentType =
                                                                                                typeof(Stream)
                                                                                        },
                                                                                    new StaticMethodLoadedResource
                                                                                        {
                                                                                            MethodName = "Load",
                                                                                            ArgumentType =
                                                                                                typeof(StreamReader)
                                                                                        },
                                                                                    new InstanceMethodLoadedResource
                                                                                        {
                                                                                            MethodName = "Load",
                                                                                            ArgumentType =
                                                                                                typeof(Stream)
                                                                                        },
                                                                                    new InstanceMethodLoadedResource
                                                                                        {
                                                                                            MethodName = "Load",
                                                                                            ArgumentType =
                                                                                                typeof(StreamReader)
                                                                                        },
                                                                                    new StaticMethodLoadedResource
                                                                                        {
                                                                                            MethodName = "Parse",
                                                                                            ArgumentType =
                                                                                                typeof(string)
                                                                                        }
                                                                                };

        /// <summary>
        /// List of registered plugins
        /// </summary>
        private static readonly List<IResourceLoaderPlugin> RegisteredPlugins = new List<IResourceLoaderPlugin>();

        /// <summary>
        /// Gets the manifest resource stream for the given resource.
        /// </summary>
        /// <param name="partialResourcePath">Partial (trailing) path to embedded resource.</param>
        /// <param name="containingAssembly">
        /// Assembly that contains the resource to load.
        /// If <c>null</c> then the assembly containing the code that calls this method is searched.
        /// </param>
        /// <returns>A stream from which to load the resource data.</returns>
        /// <exception cref="ResourceLoaderInvalidPathException">Resource path was incorrect, or you did not set the build action to Embedded Resource</exception>
        public static Stream GetResourceStream(string partialResourcePath, Assembly containingAssembly)
        {
            if (containingAssembly == null)
            {
                containingAssembly = Assembly.GetCallingAssembly();
            }

            var availableResources = containingAssembly.GetManifestResourceNames()
                .Where(r => r.EndsWith(partialResourcePath, StringComparison.OrdinalIgnoreCase)).ToArray();

            if (!availableResources.Any())
            {
                throw new ResourceLoaderInvalidPathException(
                    new EmbeddedResourceAttribute(partialResourcePath, containingAssembly));
            }

            if (availableResources.Length > 1)
            {
                throw new ResourceLoaderAmbiguousPathException(
                    new EmbeddedResourceAttribute(partialResourcePath, containingAssembly));
            }

            var resourceStream = containingAssembly.GetManifestResourceStream(availableResources.First());

            if (resourceStream == null)
            {
                throw new ResourceLoaderInvalidPathException(
                    new EmbeddedResourceAttribute(partialResourcePath, containingAssembly));
            }

            return resourceStream;
        }

        /// <summary>
        /// Reads the resource stream into a string.
        /// </summary>
        /// <param name="resourceStream">The resource stream returned by <see cref="ResourceLoader.GetResourceStream(string, Assembly)"/>.</param>
        /// <returns>String containing resource data.</returns>
        public static object GetStringResource(Stream resourceStream)
        {
            using (var sr = new StreamReader(resourceStream))
            {
                return sr.ReadToEnd();
            }
        }

        /// <summary>
        /// This method examines the object passed to it for members decorated with the <see cref="EmbeddedResourceAttribute"/> attribute and loads any indicated resources.
        /// </summary>
        /// <param name="typeOrInstance">Either a <see cref="Type"/>, or an instance of some object.</param>
        /// <exception cref="ResourceLoaderInvalidTypeException">The type of the attributed member does not support resource loading.</exception>
        /// <exception cref="ResourceLoaderReadOnlyException">Property does not have a set accessor.</exception>
        /// <exception cref="ResourceLoaderInvalidPathException">The resource could not be found. Either the path is incorrect, or the resource wasn't set to Embedded Resource in the Solution Explorer properties.</exception>
        /// <exception cref="ResourceLoaderAmbiguousPathException">The partial resource path specified matches more than one resource in the assembly manifest.</exception>
        /// <remarks>
        /// <para>
        /// In the case where <paramref name="typeOrInstance"/> is a <see cref="System.Type"/>, 
        /// the type will be examined only for static members decorated with the attribute.
        /// This is useful for static classes, or instance classes for which you only want to initialize the static members. 
        /// </para>
        /// <para>
        /// If <paramref name="typeOrInstance"/> is an instance of an object, decorated static members will be initialized 
        /// along with all decorated instance members.
        /// </para>
        /// <para>
        /// In all cases members are only initialized with embedded resource data if the member is still null at the time this method is called.
        /// </para>
        /// <para>
        /// Note that value types are always initialized with their default value if no actual initialization to some other value is present
        /// and as such are initialized, therefore the <see cref="EmbeddedResourceAttribute"/> is simply ignored. </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// using System.Xml.Linq;
        /// using Firefly.EmbeddedResourceLoader;
        /// 
        /// namespace TestNamespace
        /// {
        ///     static class StaticClass
        ///     {
        ///         [EmbeddedResource("TestNamespace.Resources.TestResource2.xml")]
        ///         public static XDocument xdoc = null;
        ///     }
        ///     
        ///     class Program
        ///     {
        ///         static void Main(string[] args)
        ///         {
        ///             // Initialise members of a static class
        ///             ResourceLoader.LoadResources(typeof(StaticClass));
        ///         }
        ///     }
        /// }
        /// </code>
        /// <code>
        /// using System.Xml.Linq;
        /// using Firefly.EmbeddedResourceLoader;
        ///
        /// namespace TestNamespace
        /// {
        ///     class TestClass
        ///     {
        ///         [EmbeddedResource("TestNamespace.Resources.TestResource1.txt")]
        ///         public string testResource1;
        ///
        ///         [EmbeddedResource("TestNamespace.Resources.TestResource2.xml")]
        ///         private XDocument testResource2;
        ///
        ///         public TestClass()
        ///         {
        ///             // Initialise instance and any uninitialized static members.
        ///             ResourceLoader.LoadResources(this);
        ///         }
        ///     }
        /// }
        /// </code>
        /// <code>
        /// using System.Xml.Linq;
        /// using Firefly.EmbeddedResourceLoader;
        ///
        /// namespace TestNamespace
        /// {
        ///     class TestClass
        ///     {
        ///         // Partial resource paths must be unambiguous
        ///         [EmbeddedResource("TestResource1.txt")]
        ///         public string testResource1;
        ///
        ///         [EmbeddedResource("TestResource2.xml", "MyAssembly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
        ///         private XDocument testResource2;
        ///
        ///         public TestClass()
        ///         {
        ///             // Initialise instance and any uninitialized static members.
        ///             ResourceLoader.LoadResources(this);
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        public static void LoadResources(object typeOrInstance)
        {
            var bindings = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;
            Type t;

            if (typeOrInstance is Type type)
            {
                // We have been passed typeof(someclass)
                t = type;
            }
            else
            {
                // The object is an instance of someclass, so we need to include instance members in the search
                t = typeOrInstance.GetType();
                bindings |= BindingFlags.Instance;
            }

            // Enumerate all fields and properties
            foreach (var memberInfo in t.GetFields(bindings).Union(t.GetProperties(bindings).Cast<MemberInfo>()))
            {
                var attr = GetEmbeddedResourceAttribute(memberInfo);

                if (attr != null)
                {
                    if (attr.ContainingAssembly == null)
                    {
                        // Determine where the attribute was declared from the instance passed in
                        attr.ContainingAssembly = t.Assembly;
                    }

                    var member = GetMember(memberInfo, typeOrInstance);

                    if (member.TargetType.IsArray)
                    {
                        throw new ResourceLoaderInvalidTypeException(member.TargetType);
                    }

                    if (member.IsReadOnly)
                    {
                        throw new ResourceLoaderReadOnlyException(
                            string.Format(CultureInfo.CurrentCulture, "{0} does not have a set accessor", member));
                    }

                    // Only load fields if uninitialized (null)
                    if (member.IsInitialized)
                    {
                        return;
                    }

                    var resourceData = GetResource(member.TargetType, attr);
                    member.SetValue(resourceData);
                }
            }
        }

        /// <summary>
        /// Register a type or types from a plug-in
        /// </summary>
        /// <param name="typesToRegister">The types to register.</param>
        /// <exception cref="ArgumentNullException">typesToRegister is null</exception>
        public static void RegisterPlugin(IEnumerable<IResourceLoaderPlugin> typesToRegister)
        {
            if (typesToRegister == null)
            {
                throw new ArgumentNullException(nameof(typesToRegister));
            }

            RegisteredPlugins.AddRange(typesToRegister);
        }

        /// <summary>
        /// Gets the name of the member as a PowerShell/MSBuild declaration string, used for messages and debugging.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns>Member name</returns>
        internal static string GetMemberName(MemberInfo member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            if (member.DeclaringType == null)
            {
                throw new ArgumentException($"Member {member.Name} has no declaring type", nameof(member));
            }

            return "[" + member.DeclaringType.FullName + "]::" + member.Name;
        }

        /// <summary>
        /// Gets the embedded resource attribute.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns>Instance of the attribute, or NULL if it was not present on the member.</returns>
        private static EmbeddedResourceAttribute GetEmbeddedResourceAttribute(ICustomAttributeProvider member)
        {
            var attrs = member.GetCustomAttributes(typeof(EmbeddedResourceAttribute), false);

            if (attrs.Length > 0)
            {
                return (EmbeddedResourceAttribute)attrs[0];
            }

            return null;
        }

        /// <summary>
        /// Gets an interface through which we can manipulate property or field equally.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="instance">The instance.</param>
        /// <returns>The interface to the member</returns>
        /// <exception cref="ResourceLoaderInvalidTypeException">Internal error. {0} is neither field nor property.</exception>
        private static IMember GetMember(MemberInfo member, object instance)
        {
            var field = member as FieldInfo;
            var prop = member as PropertyInfo;

            if (field != null)
            {
                return new FieldMember(field, instance);
            }

            if (prop != null)
            {
                return new PropertyMember(prop, instance);
            }

            // Should not get here unless the bindings were changed
            throw new ResourceLoaderInvalidTypeException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "Internal error. {0} is neither field nor property.",
                    GetMemberName(member)));
        }

        /// <summary>
        /// Loads the resource from embedded resources and returns it cast to the type of the member that the attribute decorates.
        /// </summary>
        /// <param name="targetMemberType">Type of the class member that is decorated by the attribute.</param>
        /// <param name="resourceAttribute">The attribute which describes where to find the resource.</param>
        /// <returns>Object of the requested type loaded with the data from the resource.</returns>
        /// <exception cref="ResourceLoaderInvalidTypeException">The member is of a type not supported by this library.</exception>
        /// <exception cref="EmbeddedResourceLoader.ResourceLoaderException">The requested resource could not be found. Either incorrect path or not marked as embedded resource.</exception>
        private static object GetResource(Type targetMemberType, EmbeddedResourceAttribute resourceAttribute)
        {
            var resourceStream = GetResourceStream(resourceAttribute);

            // If decorated member is a string, then return the resource as string directly.
            if (targetMemberType == typeof(string))
            {
                return GetStringResource(resourceStream);
            }

            // Check for types that can be initialized by a constructor or a given method
            foreach (var resourceLoadMethod in MethodsToCheck)
            {
                MethodBase methodToInvoke = null;
                object instanceOfType = null;

                switch (resourceLoadMethod)
                {
                    case ConstructorLoadedResource _:

                        methodToInvoke = targetMemberType.GetConstructor(
                            BindingFlags.Instance | BindingFlags.Public,
                            new ResourceLoadBinder(),
                            new[] { resourceLoadMethod.ArgumentType },
                            null);

                        break;

                    case StaticMethodLoadedResource _:

                        methodToInvoke = targetMemberType.GetMethod(
                            resourceLoadMethod.MethodName,
                            BindingFlags.Static | BindingFlags.Public,
                            new ResourceLoadBinder(),
                            new[] { resourceLoadMethod.ArgumentType },
                            null);

                        break;

                    case InstanceMethodLoadedResource _:

                        // Get default constructor
                        var ctor = targetMemberType.GetConstructor(
                            BindingFlags.Instance | BindingFlags.Public,
                            null,
                            Type.EmptyTypes,
                            null);

                        if (ctor != null)
                        {
                            // Create instance on which we will invoke the method and ultimately return this instance
                            instanceOfType = ctor.Invoke(null);

                            methodToInvoke = targetMemberType.GetMethod(
                                resourceLoadMethod.MethodName,
                                BindingFlags.Instance | BindingFlags.Public,
                                new ResourceLoadBinder(),
                                new[] { resourceLoadMethod.ArgumentType },
                                null);
                        }

                        break;
                }

                if (methodToInvoke != null)
                {
                    object invocationResult = null;

                    if (resourceLoadMethod.ArgumentType == typeof(Stream))
                    {
                        var args = new object[] { resourceStream };

                        invocationResult = methodToInvoke is ConstructorInfo ctor
                                               ? ctor.Invoke(args)
                                               : methodToInvoke.Invoke(instanceOfType, args);
                    }
                    else if (resourceLoadMethod.ArgumentType == typeof(StreamReader))
                    {
                        using (var sr = new StreamReader(resourceStream))
                        {
                            var args = new object[] { sr };

                            invocationResult = methodToInvoke is ConstructorInfo ctor
                                                   ? ctor.Invoke(args)
                                                   : methodToInvoke.Invoke(instanceOfType, args);
                        }
                    }
                    else if (resourceLoadMethod.ArgumentType == typeof(string))
                    {
                        var args = new[] { GetStringResource(resourceStream) };

                        invocationResult = methodToInvoke is ConstructorInfo ctor
                                               ? ctor.Invoke(args)
                                               : methodToInvoke.Invoke(instanceOfType, args);
                    }

                    return resourceLoadMethod is InstanceMethodLoadedResource ? instanceOfType : invocationResult;
                }
            }

            // Check registered
            var plugin = RegisteredPlugins.FirstOrDefault(p => p.Type == targetMemberType);

            if (plugin != null)
            {
                return plugin.ResourceFormat == ResourceFormat.Stream
                           ? plugin.GetObject(resourceStream)
                           : plugin.GetObject(GetStringResource(resourceStream));
            }

            // Nothing could be found on the target type that we can populate with resource data
            throw new ResourceLoaderInvalidTypeException(targetMemberType);
        }

        /// <summary>
        /// Gets the resource stream.
        /// </summary>
        /// <param name="resourceLocation">The resource location.</param>
        /// <returns>A stream from which to load the resource data.</returns>
        /// <exception cref="ResourceLoaderInvalidPathException">Resource path was incorrect, or you did not set the build action to Embedded Resource</exception>
        private static Stream GetResourceStream(EmbeddedResourceAttribute resourceLocation)
        {
            return GetResourceStream(resourceLocation.ResourcePath, resourceLocation.ContainingAssembly);
        }

        /// <summary>
        /// Describes objects that can load a resource via a constructor parameter.
        /// </summary>
        /// <seealso cref="Firefly.EmbeddedResourceLoader.ResourceLoader.ResourceLoaderMethod" />
        [DebuggerDisplay(".ctor({ArgumentType.FullName})")]
        private class ConstructorLoadedResource : ResourceLoaderMethod
        {
        }

        /// <summary>
        /// Describes an object that has an instance method that can load a resource.
        /// Such a method is expected to initialize the class with the resource data,
        /// e.g. <c>YamlStream.Load(StreamReader)</c> in <c>YamlDotNet</c>.
        /// </summary>
        /// <seealso cref="Firefly.EmbeddedResourceLoader.ResourceLoader.ResourceLoaderMethod" />
        [DebuggerDisplay("{MethodName}({ArgumentType.FullName})")]
        private class InstanceMethodLoadedResource : ResourceLoaderMethod
        {
        }

        /// <summary>
        /// Describes the methods by which a class can load a resource
        /// </summary>
        private class ResourceLoaderMethod
        {
            /// <summary>
            /// Gets or sets the type of the argument that the method expects.
            /// </summary>
            /// <value>
            /// The type of the argument.
            /// </value>
            public Type ArgumentType { get; set; }

            /// <summary>
            /// Gets or sets the name of the method to call to load the resource data.
            /// </summary>
            /// <value>
            /// The name of the method.
            /// </value>
            public string MethodName { get; set; }
        }

        /// <summary>
        /// Describes an object that has a static method that can load a resource.
        /// Such a method is expected to return an instance of the class.
        /// e.g. <c>JObject.Parse(string)</c> in <c>Newtonsoft.Json</c>.
        /// </summary>
        /// <seealso cref="Firefly.EmbeddedResourceLoader.ResourceLoader.ResourceLoaderMethod" />
        [DebuggerDisplay("static {MethodName}({ArgumentType.FullName})")]
        private class StaticMethodLoadedResource : ResourceLoaderMethod
        {
        }
    }
}