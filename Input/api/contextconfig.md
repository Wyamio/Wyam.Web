Title: ContextConfig
Description: A delegate used for specifying configuration data that requires the IExecutionContext.
---
This is a delegate used for specifying configuration data that requires the IExecutionContext.

```
delegate object ContextConfig(IExecutionContext ctx)
```

Note that the delegate returns a raw `object` to make using it from the configuration file easier (otherwise the configuration would have to contain casts or other typing information). The return value should get converted to the required type at runtime using the `Invoke<T>` method described below. 

You can use `@@ctx` in your configuration file as a shorthand for writing an entire `Func<>` or delegate.

## Extension Methods

  - `T Invoke&lt;T&gt;(this ContextConfig config, IExecutionContext context)`
  
    This extension invokes the delegate and attempts to convert the result to the specified type `T` using the same flexible type conversion process used by metadata.