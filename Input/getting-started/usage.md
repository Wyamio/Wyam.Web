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

# Paths

The paths that Wyam uses can be specified on the command line. This includes the `--input` path(s) where Wyam will look for files, the `--output` path where Wyam will place the output of modules like [WriteFiles](/modules/writefiles), the `--config` file path where Wyam will look for a configuration file, and the root path that Wyam considers the base path for all other relative paths. By default, the root path is set to the path from where Wyam is executed. You may want to specify a different base path (for example, if running from a build script or as part of a larger process). In this case, just supply an alternate absolute path at the end of the command line. This new root path will be the base for all other relative paths including input and output paths. This is a good way to ensure consistency regardless of which path Wyam is run from.

Note that these paths can also be specified from within the [setup portion of your configuration script](/getting-started/configuration#setup). From there you can set `FileSystem.OutputPath` or `FileSystem.RootPath` or call `FileSystem.InputPaths.Add()`.