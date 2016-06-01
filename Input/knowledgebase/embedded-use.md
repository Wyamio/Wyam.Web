Title: Embedded Use
Description: Wyam can be embedded into your own application.
---
While Wyam is usually executed from the command line application, this is just a thin wrapper around a core library that you can include in your own applications. The core Wyam library is available on NuGet as [Wyam.Core](https://www.nuget.org/packages/Wyam.Core). Once you've included it in your application, you will need to create an instance of the `Wyam.Core.Execution.Engine` class.

To configure the engine, you use the properties of the `Engine` class. Any modules that take a dependencies on other libraries are typically contained in NuGet packages and are not part of the Wyam.Core library. This is to keep the core library light and allow you to include only those modules that you will need. [Search NuGet for "wyam"](https://www.nuget.org/packages?q=wyam) in order to find additional module libraries. They are all named with the `Wyam.XYZ` convention.

Once the engine is configured, execute it with a call to `Engine.Execute()`. This will start evaluation of the pipelines and any output messages will be sent to the configured trace endpoints.
