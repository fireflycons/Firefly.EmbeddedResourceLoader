# Firefly.EmbeddedResourceLoader


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

There is also a plugin system discussed in more detail in the documentation that allows you to define how an object of any type not covered by the above can be loaded with embedded resource data.
