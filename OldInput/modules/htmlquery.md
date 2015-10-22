Title: HtmlQuery
Description: Powerful querying of HTML content that allows setting new content or metadata based on query results.
Category: Metadata
---
This module provides a powerful querying capability for HTML content. Once you provide a DOM query selector, the module creates new output documents for each query result and allows you to set the new document content and/or set new metadata based on the query result.

**This module uses [AngleSharp](https://github.com/FlorianRappl/AngleSharp).**

# Usage
---

  - `HtmlQuery(string querySelector)`

    Creates the module with the specified query selector.
  
## Fluent Methods

Chain these methods together after the constructor to modify behavior.

  - `WithQuerySelector(string querySelector)`
  
    Allows you to specify the query selector. 

  - `First(bool first = true)`
  
    Specifies that only the first query result should be processed.
    
  - `SetContent(bool outerHtml = true)`
  
    Sets the content of the result document(s) to the content of the corresponding query result, optionally specifying whether inner or outer HTML content should be used.
    
  - `GetOuterHtml(string metadataKey = "OuterHtml")`
  
    Gets the outer HTML of each query result and sets it in the metadata of the corresponding result document(s) with the specified key.
    
  - `GetInnerHtml(string metadataKey = "InnerHtml")`
  
    Gets the inner HTML of each query result and sets it in the metadata of the corresponding result document(s) with the specified key.
    
  - `GetTextContent(string metadataKey = "TextContent")`
  
    Gets the text content of each query result and sets it in the metadata of the corresponding result document(s) with the specified key.
    
  - `GetAttributeValue(string attributeName, string metadataKey = null)`
  
    Gets the specified attribute value of each query result and sets it in the metadata of the corresponding result document(s). If the attribute is not found for a given query result, no metadata is set. If `metadataKey` is null, the attribute name will be used as the metadata key, otherwise the specified metadata key will be used.
    
  - `GetAttributeValues()`
  
    Gets the values for all attributes of each query result and sets them in the metadata of the corresponding result document(s) with keys names equal to the attribute local name.
    
  - `GetAll()`
  
    Gets all information for each query result and sets the metadata of the corresponding result document(s). This is equivalent to calling `GetOuterHtml()`, `GetInnerHtml()`, `GetTextContent()`, and `GetAttributeValues()` with default arguments.
    
 # Metadata
---

The following metadata is added to each document if requested by calling the corresponding fluent method(s). Additional metadata may also be added (such as attribute values) depending on the configuration.

  - `OuterHtml`
  
    Contains the outer HTML of the query result (unless an alternate metadata key is specified).
    
  - `InnerHtml`
  
    Contains the inner HTML of the query result (unless an alternate metadata key is specified).
    
  - `TextContent`
  
    Contains the text content of the query result (unless an alternate metadata key is specified).
  
  