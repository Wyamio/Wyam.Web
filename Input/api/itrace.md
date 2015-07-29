Title: ITrace
Description: An interface for outputting trace and log messages.
---
An interface for outputting trace and log messages.

# Members
---

## Properties

  - `int IndentLevel { get; set; }`
  
    Gets or sets the current indent level.
  
## Methods

  - `void SetLevel(SourceLevels level)`
  
    Sets the source level for the trace.
  
  - `void AddListener(TraceListener listener)`
  
    Adds a listener to the trace.
  
  - `void RemoveListener(TraceListener listener)`
  
    Removes a listener from the trace.
  
  - `void Critical(string messageOrFormat, params object[] args)`
  
    Traces a critical message. You can supply arguments similar to `string.Format(...)`.
  
  - `void Error(string messageOrFormat, params object[] args)`
  
    Traces an error message. You can supply arguments similar to `string.Format(...)`.
  
  - `void Warning(string messageOrFormat, params object[] args)`
  
    Traces a warning message. You can supply arguments similar to `string.Format(...)`.
  
  - `void Information(string messageOrFormat, params object[] args)`
  
    Traces an informational message. You can supply arguments similar to `string.Format(...)`.
  
  - `void Verbose(string messageOrFormat, params object[] args)`
  
    Traces a verbose message. You can supply arguments similar to `string.Format(...)`.
  
  - `void TraceEvent(TraceEventType eventType, string messageOrFormat, params object[] args)`
  
    Traces a message using `TraceEvent` to specify the type. You can supply arguments similar to `string.Format(...)`.
    
  - `int Indent()`
  
    Indents the trace and returns the current amount of indentation before indenting.
    
  - `IIndentedTraceEvent WithIndent()`
  
    Returns an `IIndentedTraceEvent` object suitable for indenting using the disposable pattern.