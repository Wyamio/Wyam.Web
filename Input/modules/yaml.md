Title: Yaml
Description: Parses YAML content and stores the result in document metadata.
Category: Metadata
---
Parses YAML content for each input document and stores the result in it's metadata.

This module uses [YamlDotNet](http://aaubry.net/pages/yamldotnet.html).

# Usage
---
  - `Yaml()`
  
    The content of the input document is parsed as YAML. All root-level scalars are added to the input document's metadata. Any more complex YAML structures are ignored. This is best for simple key-value YAML documents.
	
  - `Yaml(string key, bool flatten = false)`
  
    The content of the input document is parsed as YAML. A dynamic object representing the first YAML document is set as the value for the given metadata key. See [YamlDotNet.Dynamic](https://github.com/aaubry/YamlDotNet.Dynamic) for more details about the dynamic YAML object. If `flatten` is `true`, all root-level scalars are also added to the input document's metadata.