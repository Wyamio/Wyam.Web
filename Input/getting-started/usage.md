Title: Usage
Description: Describes how to run Wyam and the available options.
Order: 2
---
You typically run Wyam using the command line application `Wyam.exe`. If you don't specify any arguments, the root folder will be set to the current folder and if a file named `config.wyam` is found, it will be used as the configuration file. You can also specify a root folder that's different than the current folder after the `Wyam.exe` command. For example:

```
Wyam.exe
```

Or:

```
Wyam.exe C:\MySite
```

There are also a number of arguments that allow you to control execution:

* `--config file`
  
  Configuration file other than the default `config.wyam`.
  
* `--input path`

  The path to input files, can be absolute or relative to the current folder.
  
* `--output path`

  The path to output files, can be absolute or relative to the current folder.
  
* `--noclean`

  By default the output folder is cleaned on every execution and *all* files in the folder and all subfolders are deleted. This argument lets you prevent the cleaning and will keep all files in the output folder. Keep in mind that generated files will still get overwritten on every execution.
  
* `--nocache`

  This turns off the caching mechanism for all modules. It results in a lower memory footprint at the expense of much slower build times.
  
* `--update-packages`

  By default, local packages will be used if available and if they satisfy the version specification given in the config file. Use this flag to check the NuGet server for more recent versions of each package and update them if applicable.

* `--watch`

  Watch the input folder for any changes. If any are found, the content is regenerated. Because of the flexible nature of Wyam modules it is impossible to tell which input files impact which output files, so all pipelines have to be rerun from scratch. Specifying this argument will keep Wyam running until a key is pressed.
  
* `--preview [port] [force-ext]`

  Starts the embedded preview web server on the specified port (or on port 5080 if no port is specified). Be default, the web server has a routing rule for extensionless URLs (that is, if you go to `/about` and a file `/about.html` or `/about.htm` exists, it will be served). To turn this behavior off and force the use of file extensions, specify `force-ext`. Note that to match the extensionless routing behavior on your web server, some additional configuration may be required. Specifying this argument will keep Wyam running until a key is pressed.
  
* `--output-scripts`

  Outputs the config scripts after they've been processed for further debugging or to see exactly what gets evaluated when your configuration runs.
  
* `--log [logfile]`

  Log all trace messages to a log file. You can specify the log file with `logfile` or the file `wyam-[datetime].txt` will be used if one isn't specified.
  
* `--verbose`

  Turn on "verbose mode" and will generate additional trace message useful for debugging.
  
* `--pause`

  Pause execution at the start of the program until a key is pressed and is useful for attaching a debugger or synchronizing with other applications.
  
* `--help`

  Prints out a list of the available command line arguments. 
