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

There are also a number of arguments that allow you to control execution (use `--help` or `-?` to see them):

```
usage:  [-w] [-p [arg]] [--force-ext] [--preview-root <arg>] [-i <arg>]
        [--verify-config] [-o <arg>] [-c <arg>] [-u] [--output-scripts]
        [--noclean] [--nocache] [-v] [--pause] [-l [arg]] [--] <root>

    -w, --watch              Watches the input folder for any changes.
    -p, --preview [arg]      Start the preview web server on the
                             specified port (default is 5080).
    --force-ext              Force the use of extensions in the preview
                             web server (by default, extensionless URLs
                             may be used).
    --preview-root <arg>     The path to the root of the preview server,
                             if not the output folder.
    -i, --input <arg>        The path of input files, can be absolute or
                             relative to the current folder.
    --verify-config          Compile the configuration but do not
                             execute.
    -o, --output <arg>       The path to output files, can be absolute
                             or relative to the current folder.
    -c, --config <arg>       Configuration file (by default, config.wyam
                             is used).
    -u, --update-packages    Check the NuGet server for more recent
                             versions of each package and update them if
                             applicable.
    --output-scripts         Outputs the config scripts after they've
                             been processed for further debugging.
    --noclean                Prevents cleaning of the output path on
                             each execution.
    --nocache                Prevents caching information during
                             execution (less memory usage but slower
                             execution).
    -v, --verbose            Turns on verbose output showing additional
                             trace message useful for debugging.
    --pause                  Pause execution at the start of the program
                             until a key is pressed (useful for
                             attaching a debugger).
    -l, --log [arg]          Log all trace messages to the specified log
                             file (by default, wyam-[datetime].txt).
    <root>                   The folder (or config file) to use.
```