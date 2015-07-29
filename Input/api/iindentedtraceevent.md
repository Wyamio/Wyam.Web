Title: IIndentedTraceEvent
Description: Provides an easy way to indent trace messages.
---
Provides an easy way to indent trace messages. You get an instance from `ITrace.WithIndent()` and typically call it from inside a `using` statement. When the `IDisposable` returned by one of the `IIndentedTraceEvent` methods is disposed, the indent is removed from the trace.

```
using(trace.WithIndent().Information("Starting some process..."))
{
  // Do some stuff
}
```

# Members
---
  
## Methods
  
  - `IDisposable Critical(string messageOrFormat, params object[] args)`
  
    Traces a critical message. You can supply arguments similar to `string.Format(...)`.
  
  - `IDisposable Error(string messageOrFormat, params object[] args)`
  
    Traces an error message. You can supply arguments similar to `string.Format(...)`.
  
  - `IDisposable Warning(string messageOrFormat, params object[] args)`
  
    Traces a warning message. You can supply arguments similar to `string.Format(...)`.
  
  - `IDisposable Information(string messageOrFormat, params object[] args)`
  
    Traces an informational message. You can supply arguments similar to `string.Format(...)`.
  
  - `IDisposable Verbose(string messageOrFormat, params object[] args)`
  
    Traces a verbose message. You can supply arguments similar to `string.Format(...)`.
  
  - `IDisposable TraceEvent(TraceEventType eventType, string messageOrFormat, params object[] args)`
  
    Traces a message using `TraceEvent` to specify the type. You can supply arguments similar to `string.Format(...)`.