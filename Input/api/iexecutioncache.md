Title: IExecutionCache
Description: Used by modules to cache data for faster build times.
---
Used by modules to cache data for faster build times. The cache can be keyed by a custom string or by a specified document (in which case the document content is used as the key). The cache value can be any object. Note that only objects that are not potentially dependent on external mutable state like metadata should be cached. For example, a compiled [Razor](/modules/Razor) page class can be cached, but the result of evaluating the Razor code against a given document should not be cached.

# Members
---
  
## Methods
  
  - `bool ContainsKey(IDocument document)`
    
    Returns `true` if the cache contains a value for the specified document key.
      
  - `bool ContainsKey(string key)`  
    
    Returns `true` if the cache contains a value for the specified string key.
    
  - `bool TryGetValue(IDocument document, out object value)`
  
    Returns `true` if the cache contains a value for the specified document key and sets `value` if so.
    
  - `bool TryGetValue(string key, out object value)`  
    
    Returns `true` if the cache contains a value for the specified string key and sets `value` if so.
    
  - `bool TryGetValue<TValue>(IDocument document, out TValue value)`
  
    Returns `true` if the cache contains a value of type `TValue` for the specified document key and sets `value` if so. If the cache contains a value for the specified key but it cannot be cast to the requested type, an `InvalidCastException` is thrown.
    
  - `bool TryGetValue<TValue>(string key, out TValue value)`  
    
    Returns `true` if the cache contains a value of type `TValue` for the specified string key and sets `value` if so. If the cache contains a value for the specified key but it cannot be cast to the requested type, an `InvalidCastException` is thrown.
    
  - `void Set(IDocument document, object value)`
  
    Sets the provided value for the specified document key.
    
  - `void Set(string key, object value)`
  
    Sets the provided value for the specified string key.