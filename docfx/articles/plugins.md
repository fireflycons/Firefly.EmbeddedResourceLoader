# Resource Loader Plugins

The Embedded Resource Loader supports a simple plugin system that enables you to wire up objects that are not directly supported by the library o be able to be initialized by decoration with the [EmbeddedResource](xref:Firefly.EmbeddedResourceLoader.EmbeddedResourceAttribute) attribute. You can do this for any object be it in your code, or an object in a third party package.

## Creating a Plugin

Although already directly supported by this package, let's take `JObject` from `Newtonsoft.Json` as an example plugin implementation.

```csharp
public class ResourceClass
{
    [EmbeddedResource("MyJson.json")]
    public JObject MyJson { get; set; }
}
```

### Create a Plugin Definition

Create an implementation of `IResourceLoaderPlugin`

```csharp
public class MyJObjectPlugin : IResourceLoaderPlugin
{
    // We want the resource content delivered as a string
    public ResourceFormat ResourceFormat => ResourceFormat.String;

    // Call this plugin when a decorated JObject member is found
    public Type Type => typeof(JObject);

    // Called to construct the object and populate
    public object GetObject(object resourceData)
    {
        return JObject.Parse((string)resourceData);
    }
}
```

### Register the plugin

```csharp
    ResourceLoader.RegisterPlugin(new[] { MyJObjectPlugin });
```

### Populate

```csharp
    var c = new ResourceClass();

    ResourceLoader.LoadResources(c);
```

