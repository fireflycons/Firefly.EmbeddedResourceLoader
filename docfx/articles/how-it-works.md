# How it Works

Basically, reflection.

That aside for now, there are quite a few types that exist both in the standard libraries and server popular third party packages that are ideal for initializing from embedded resouces, taking the resource either as a `Stream` or as a `string`. This library will work as-is without any extra coding to initialise classes with fields or properties that meet the following criteria

* Is not read only (readonly field, or property with no set accessor)
* Is not already assigned a value. Note that for value types, they always are assigned with their default value (e.g. int = 0), so are ignored.
* Is a string
* Has a constructor taking a single argument of type `Stream`, e.g. `Bitmap`
* Has a static `Load` method taking a single argument of type `Stream` or `StreamReader`, e.g. `XDocument`
* Has an instance `Load` method taking a single argument of type `Stream` or `StreamReader`, e.g.  `YamlStream`
* Has a static `Parse` method taking a single argument of type `string`, e.g. `JObject`

Create a class that has one or more fields or properties that you want to initialise from embedded resource data. You can either inherit your class from [AutoResourceLoader](xef:Firefly.EmbeddedResourceLoader.AutoResourceLoader) in which case evey time an instance of your class is created, its resources will auto-load, or call [ResouceLoader.LoadResources](xref:Firefly.EmbeddedResourceLoader.ResourceLoader.LoadResources(System.Object)) on the instance to load on demand.

To associate a field or property with an embedded resource, decorate it with the [EmbeddedResource](xref:Firefly.EmbeddedResourceLoader.EmbeddedResourceAttribute) attribute.

The attribute's constructor takes as it's first argument the name of an embedded resource. You only have to give enough of the resource name to make it unambiguous, i.e. can't be confused with any similarly named resource.

Say the namespace of your resources is `MyCompany.MyLibrary.Resources`, i.e. you created a project called `MyCompany.MyLibrary` and you created a project folder called `Resources` to put your embedded resources in. Within this folder you have a resource file `MyResource.txt`. You could then decorate your class member with

```csharp
    [EmbeddedResource("MyResource.txt")]
    public string MyProperty {get; set; }
```

...and the resource would be found and loaded.

If on the other hand you had two resource folders `Resources1` and `Resources2`, both containing a file `MyResource.txt` the above would throw a [ResourceLoaderAmbiguousPathException](xref:Firefly.EmbeddedResourceLoader.Exceptions.ResourceLoaderAmbiguousPathException). To solve this, you would need to be more specific with the resource name:

```csharp
    [EmbeddedResource("Resources1.MyResource.txt")]
    public string MyProperty {get; set; }
```

# Examples in Other Repositories

You can see this library in action in the unit test projects in my other repositories

* [Firefly.CloudFormation](https://github.com/fireflycons/Firefly.CloudFormation/tree/master/tests/Firefly.CloudFormation.Tests.Unit)
* [PSCloudFomation](https://github.com/fireflycons/PSCloudFormation/tree/master/tests/Firefly.PSCloudFormation.Tests.Unit)

## Remarks

Note that creating classes with members that are initialized using the [EmbeddedResource](xref:Firefly.EmbeddedResourceLoader.EmbeddedResourceAttribute) attribute and are therefore never directly assigned will generate a lot of compiler warnings. You can get rid of most of these by disabling warning `169` and `649` using `#pragme warning disable`

