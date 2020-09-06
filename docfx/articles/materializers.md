# Materializers

Materializers are classes in this library that materialize resources to files. You might want to do this for a set of unit tests that operate on files rather than embedded resources directly. There are two classes for this, both implement [IDisposable](https://docs.microsoft.com/dotnet/api/system.idisposable) to enable cleanup of temporary files after use.

Both the temp file name and the temp directory name are composed of a generated guid with a `.tmp` extension.

## TempFile

The [TempFile](xref:Firefly.EmbeddedResourceLoader.Materialization.TempFile) class materializes a single resource to a file in the user's temp directory (Windows) or `/tmp` on other systems.

You can also use the `TempFile` constructor to directly create a temporary file from any valid `Stream`.

```csharp
public class MyResourceClass : IDisposable
{
    [EmbeddedResource("MyResource.txt")]
    public TempFile FileResource { get; set; }

    public void Dispose()
    {
        this.FileResource?.Dispose();
    }
}
```

## TempDirectory

The [TempDirectory](xref:Firefly.EmbeddedResourceLoader.Materialization.TempDirectory) class materializes a project folder full of resources to a directory created  in the user's temp directory (Windows) or `/tmp` on other systems.

Here, the path passed to the [EmbeddedResource](xref:Firefly.EmbeddedResourceLoader.EmbeddedResourceAttribute) attribute is expected to unambiguously resolve to a folder in the project that contains one or more embedded resources. The file names and any subfolders within the selected folder are replicated to the temporary directory tree.

The [TempDirectory](xref:Firefly.EmbeddedResourceLoader.Materialization.TempDirectory) also contains a default constructor that enables you to create a temporary directory for any purpose.

```csharp
public class MyResourceClass : IDisposable
{
    [EmbeddedResource("Resources")]
    public TempDirectory Resources { get; set; }

    public void Dispose()
    {
        this.Resources?.Dispose();
    }
}
```

### Caveats with TempDirectory

The compiler has a nasty trick when embedding folders into the resource manifest. It converts hyphens in directory names to underscores meaning that when the directory structure is materialized you don't get what you expect. I expect this is to do with what consitutes valid characters for a .NET namespace name, thus any invalid character in a resource folder name is going to be replaced.

To work around this, there is a property `DirectoryRenames` on [EmbeddedResource](xref:Firefly.EmbeddedResourceLoader.EmbeddedResourceAttribute) that allows you to specify renames for directories that are being materialized.

Say you have this structure in your project

```
TempDirResource
+ sub-dir
  + file.txt
```

This will by default come out like this in the filesystem

```
TempDirResource
+ sub_dir
  + file.txt
```

To get the correct directory, do this

```csharp
public class MyResourceClass : IDisposable
{
    [EmbeddedResource("Resources", DirectoryRenames = new[] { "sub_dir", "sub-dir"} )]
    public TempDirectory Resources { get; set; }

    public void Dispose()
    {
        this.Resources?.Dispose();
    }
}
```
