# Static Classes

Wholly static classes can also be populated with resource content. We do this by passing the type of the class to the resource loader.

```csharp
public static class StaticResourceClass
{
    [EmbeddedResource("MyResource.txt")]
    public static MyResource { get; set; }
}
```

```csharp
    ResourceLoader.LoadResources(typeof(StaticResourceClass));
```
