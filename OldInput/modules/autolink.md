Title: AutoLink
Description: Replaces occurrences of specified strings with HTML links.
Category: Content
---
Replaces occurrences of specified strings with HTML links. This module is smart enough to only look in specified HTML elements (`p` by default). You can supply an alternate query selector to narrow the search scope to different container elements or to those elements that contain (or don't contain) a CSS class, etc. It also won't generate an HTML link if the replacement text is already found in another link.

**This module uses [AngleSharp](https://github.com/FlorianRappl/AngleSharp).**

# Usage
---

  - `AutoLink()`

    Creates the module without any initial mappings. Use `AddLink(...)` to add mappings with fluent methods.
  
  - `AutoLink(IDictionary<string, string> links)`
  
    Specifies a dictionary of link mappings. The keys specify strings to search for in the HTML content and the values specify what should be placed in the `href` attribute. This uses the same link mappings for all input documents.
    
  - `AutoLink(ContextConfig links)`
  
    Specifies a dictionary of link mappings given an `IExecutionContext`. The return value is expected to be a `IDictionary<string, string>`. The keys specify strings to search for in the HTML content and the values specify what should be placed in the `href` attribute. This uses the same link mappings for all input documents.
    
  - `AutoLink(DocumentConfig links)`
  
    Specifies a dictionary of link mappings given an `IDocument` and `IExecutionContext`. The return value is expected to be a `IDictionary<string, string>`. The keys specify strings to search for in the HTML content and the values specify what should be placed in the `href` attribute. This allows you to specify a different mapping for each input document.
  
## Fluent Methods

Chain these methods together after the constructor to modify behavior.

  - `WithQuerySelector(string querySelector)`
  
    Allows you to specify an alternate query selector. 

  - `WithLink(string text, string link)`
  
    Adds an additional link to the mapping. This can be used whether or not you specify a mapping in the constructor.

  - `WithMatchOnlyWholeWord()`
  
    If used, AutoLink no longer matches substrings in a word. Only whole words will be matched. A word is considered a substring of a string if both following conditions are met:
    * The character before the first character of the word is not a digit and not a letter
    * The character following the last character of the word is not a digit and not a letter

    Any character before the first or after the last character of the string is regarded to be not a digit and not a letter.
