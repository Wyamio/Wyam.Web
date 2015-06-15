Title: FrontMatter
Description: Extracts the first part of content for each document and sends it to a child module for processing.
---
 Extracts the first part of content for each document and sends it to a child module for processing. This module is typically used in conjunction with the [Yaml](/modules/yaml) module to enable putting YAML front matter in a file. First, the content of each input document is scanned for a line that consists entirely of the delimiter character or (`-` by default) or the delimiter string. Once found, the content before the delimiter is passed to the specified child modules. Any metadata from the child module output document(s) is added to the input document. Note that if the child modules result in more than one output document, multiple clones of the input document will be made for each one. The output document content is set to the original content without the front matter.
 
 # Usage
 ---
 
   - `FrontMatter(params IModule[] modules)`
   
     Uses the default delimiter character and passes any front matter to the specified child modules for processing.
   
   - `FrontMatter(string delimiter, params IModule[] modules)`
   
     Uses the specified delimiter string and passes any front matter to the specified child modules for processing.
   
   - `FrontMatter(char delimiter, params IModule[] modules)`
   
     Uses the specified delimiter character and passes any front matter to the specified child modules for processing.