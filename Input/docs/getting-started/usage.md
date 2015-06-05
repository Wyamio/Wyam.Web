Title: Usage
Order: 3
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
  
  This allows you specify a configuration file that's different from the default `config.wyam`.
  
* `--no-clean`

  By default the output folder is cleaned on every execution and *all* files in the folder and all subfolders are deleted. This argument lets you prevent the cleaning and will keep all files in the output folder. Keep in mind that generated files will still get overwritten on every execution.
  
* `--skip-packages`

  By default NuGet packages are checked every time the application is started. This can be time consuming, particularly if a lot of NuGet packages are specified and/or the NuGet servers are slow in responding. You can turn off NuGet installation with this argument. Local packages will still get used, but any packages not already installed will be ignored.

* `--watch`

  This tells Wyam to watch the input folder for any changes. If any are found, the content is regenerated. Because of the flexible nature of Wyam modules it is impossible to tell which input files impact which output files, so all pipelines have to be rerun from scratch. Specifying this argument will keep Wyam running until a key is pressed.
  
* `--preview [port] [force-ext]`

  This will start the embedded preview web server on the specified port (or on port 5080 if no port is specified). Be default, the web server has a routing rule for extensionless URLs (that is, if you go to `/about` and a file `/about.html` or `/about.htm` exists, it will be served). To turn this behavior off and force the use of file extensions, specify `force-ext`. Note that to match the extensionless routing behavior on your web server, some additional configuration may be required. Specifying this argument will keep Wyam running until a key is pressed.
  
* `--log [logfile]`

  This will log all trace messages to a log file. You can specify the log file with `logfile` or the file `wyam-[datetime].txt` will be used if one isn't specified.
  
* `--verbose`

  This will turn on "verbose mode" and will generate additional trace message useful for debugging.
  
* `--pause`

  This will pause execution at the start of the program until a key is pressed and is useful for attaching a debugger or synchronizing with other applications.
  
* `--help`

  This prints out a list of the available command line arguments. 
