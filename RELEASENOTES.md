# 0.1.4

* Enhancement - Provide TempFile resources as a method so can be used in using blocks.

# 0.1.3

* Enhancement - TempFile and TempDirectory should have string casts, so they can be used directly in mathods that take a string path (e.g.System.IO).
* Enhancement - TempFile should have option to preserve file extension.

# 0.1.2

* Fix issue with directory materialization for `TempDirectory` resource. Characters in the resource folder names that are not valid for .NET namespaces are replaced by the compiler with underscores. A property has been added to `EmbeddedResource` attribute to permit renaming of these back to what they should be upon materialization.

# 0.1.1

* Fix bug that occurs if you have a TempDirectory resource and all files in resource directory have the same filename but different extensions, then the files created have only the .ext as the filename in the temporary directory.

# 0.1.0

Initial Release