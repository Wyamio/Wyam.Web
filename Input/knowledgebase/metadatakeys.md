Title: Metadata Keys
Description: Describes the MetadataKeys class and how to use it to refer to metadata without strings.
---
The `Wyam.Abstractions` assembly (which is automatically brought into scope) contains a static `MetadataKeys` class. This class contains `const` strings for all of the metadata keys that get set throughout the core Wyam modules. You can use these instead of magic strings when getting metadata values. For example, in a [Razor page](/modules/razor) you would write:
```
Metadata.Get<string>(MetadataKeys.RelativeFilePath)
```
instead of:
```
Metadata.Get<string>("RelativeFilePath")
``` 