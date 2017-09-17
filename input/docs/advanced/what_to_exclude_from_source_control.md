Title: What To Exclude From Source Control
Description: Files and folders that Wyam generates and should not be included in source control.
---
Wyam generates output files and folders that should not be included in source control as well as writes some caching information to disk. Here are the files and folders that should be ignored:

* `output` - the folder that contains the generated output of running Wyam.
* `config.wyam.dll` - a cached compilation of the configuration file.
* `config.wyam.hash` - a hash of the configuration file used to determine if it needs to be recompiled.
* `packages.xml` - a cache of all packages and their entire dependency tree to improve NuGet dependency resolution performance.