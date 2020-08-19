# Firefly.EmbeddedResourceLoader


## About

Yet another library for managing embedded resources? Well yes, however this one does it using attributes.

I find that I use embedded resources a lot when creating unit tests, and I tire of writing the same old boilerplate to locate embedded resources and load their content into test class members.
This library provides a mechanism to simply add an attribute to any field or property of a class that refers to an embedded resource, and either via inheritance from a base class provided here
or a call to a static method in this library, all attributed members will be initialized with embedded resource content.


There is also a plugin system discussed in more detail [in the documentation](articles/plugins.md) that allows you to define how an object of any type not covered by the above can be loaded with embedded resource data.
