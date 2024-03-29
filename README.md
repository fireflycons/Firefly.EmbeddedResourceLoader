# Firefly.EmbeddedResourceLoader

[![Build status](https://ci.appveyor.com/api/projects/status/9faegqir2x2mtccx/branch/master?svg=true)](https://ci.appveyor.com/project/fireflycons/firefly-embeddedresourceloader/branch/master)

![Nuget](https://img.shields.io/nuget/v/Firefly.EmbeddedResourceLoader)

## About

Yet another library for managing embedded resources? Well yes, however this one does it using attributes.

I find that I use embedded resources a lot when creating unit tests, and I tire of writing the same old boilerplate to locate embedded resources and load their content into test class members.
This library provides a mechanism to simply add an attribute to any field or property of a class that refers to an embedded resource, and either via inheritance from a base class provided here
or a call to a static method in this library, all attributed members will be initialized with embedded resource content.

## Types that can be auto-intialized

Out of the box the following can easily be initialized

* `string` - directly from any text resource
* Stream constructed types - Any object that has a single argument constructor of type `Stream`, e.g. `System.Drawing.Bitmap`
* Stream loaded types - Any object that has a static or instance `Load` method taking a single argument of type `Stream` or `StreamReader`, e.g. `XDocument` or `YamlDotNet.RepresentationModel.YamlStream`
* Parse types - Any object that has a static `Parse` method taking a single argument of type `string`, e.g. `Newtonsoft.Json.Linq.JObject` or `Newtonsoft.Json.Linq.JArray`

There is also a plugin system discussed in more detail in the [documentation](https://fireflycons.github.io/Firefly-EmbeddedResourceLoader/articles/plugins.html) that allows you to define how an object of any type not covered by the above can be loaded with embedded resource data.

## Examples

In all the following examples, the embedded resources are expected to be present in the assembly where the class decorated with the `EmbeddedResource` attribute is declared.
Other constructors of the attribute take an assembly name or an `Assembly` object that specifices the assembly in which to search for the resource.

In this example, all resources are automatically loaded when an instance of the class is created. Static members are only initialized the first time.

```csharp
public class ResourceLoadedClass: AutoResourceLoader
{
	[EmbeddedResource('FirstString.txt')]
	private static string FirstString;

	[EmbeddedResource('XmlDocument.xml')]
	public XDocument Document { get; set; }
}
```

In this example resources are loaded when requested by the code

```csharp
public class ResourceLoadedClass
{
	[EmbeddedResource('FirstString.txt')]
	private static string FirstString;

	[EmbeddedResource('XmlDocument.xml')]
	public XDocument Document { get; set; }
}

...

var c = new ResourceLoadedClass();

ResourceLoader.LoadResources(c);
```

And for entirely static classes...

```csharp
public static class StaticResourceLoadedClass
{
	[EmbeddedResource('FirstString.txt')]
	private static string FirstString;

	[EmbeddedResource('XmlDocument.xml')]
	public static XDocument Document { get; set; }
}

...

ResourceLoader.LoadResources(typeof(StaticResourceLoadedClass));
```

Materializing a file...


```csharp
public class FileLoadingClass: AutoResourceLoader, IDisposable
{
	[EmbeddedResource('XmlDocument.xml')]
	private TempFile myFile { get; set; }

	void SomeMethodThatUsesTheFile()
	{
		var pathToFile = myFile.FullPath;
		...
	}

	public void Dispose()
	{
		// Clean up the file
		myFile?.Dispose();
	}
}
```

Materializing a directory structure

```csharp
public class DirectoryLoadingClass: AutoResourceLoader, IDisposable
{
	[EmbeddedResource('Resources')]		// Get all embedded resources below this project folder.
	private TempDirectory myDir { get; set; }

	void SomeMethodThatUsesTheFiles()
	{
		var pathToFiles = myDir.FullPath;
		...
	}

	public void Dispose()
	{
		// Delete the directory structure
		myDir?.Dispose();
	}
}
```

# Examples in Other Repositories

You can see this library in action in the unit test projects in my other repositories

* [Firefly.CloudFormation](https://github.com/fireflycons/Firefly.CloudFormation/tree/master/tests/Firefly.CloudFormation.Tests.Unit)
* [PSCloudFomation](https://github.com/fireflycons/PSCloudFormation/tree/master/tests/Firefly.PSCloudFormation.Tests.Unit)


# API Documentation and Further Examples

This can be found on the [documentation site](https://fireflycons.github.io/Firefly-EmbeddedResourceLoader)
