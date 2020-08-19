# Exceptions

There are several types of exception that may be raised by the resource loading system

## ResourceLoaderAmbiguousPathException

Will be raised when a resource name passed to the [EmbeddedResource](xref:Firefly.EmbeddedResourceLoader.EmbeddedResourceAttribute) attribute matches more than one resource. See the discussion on this [here](how-it-works.md)

## ResourceLoaderInvalidDirectoryException

Will be raised if you have attempted to initialize a [TempDirectory](xref:Firefly.EmbeddedResourceLoader.Materialization.TempDirectory) object and the resource path does not resolve to a project folder containing embedded resources.

## ResourceLoaderInvalidPathException

Will be raised when a resource name passed to the [EmbeddedResource](xref:Firefly.EmbeddedResourceLoader.EmbeddedResourceAttribute) attribute yeilds no resource. This could be because the file does exist, but it was not set to `Embedded Resource` in the file properties.

## ResourceLoaderInvalidTypeException

Will be raised if the decorated member is a type not supported by this library and no [plugin](plugins.md) is registered to support that type.

## ResourceLoaderReadOnlyException

Will be raised for a readonly field or a property with no `get` accessor.
