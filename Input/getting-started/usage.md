Title: Usage
Description: Describes how to run Wyam and the available options.
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

There are also a number of arguments that allow you to control execution (use `--help` or `-?` to see them):

```
usage:  [--help-directives] [-w] [-p [arg]] [--force-ext]                                 
        [--preview-root <arg>] [-i <arg>...] [-o <arg>] [-c <arg>] [-u]                   
        [--use-local-packages] [--packages-path <arg>] [--output-script]                  
        [--verify-config] [--noclean] [--nocache] [-v] [--pause]                          
        [--meta <arg>...] [-l [arg]] [--assembly-name <arg>...]                           
        [--assembly <arg>...] [--nuget-source <arg>...]                                   
        [--nuget <arg>...] [--] <root>                                                    
                                                                                          
    --help-directives           Displays help for the various                             
                                preprocessor directives.                                  
    -w, --watch                 Watches the input folder for any                          
                                changes.                                                  
    -p, --preview [arg]         Start the preview web server on the                       
                                specified port (default is 5080).                         
    --force-ext                 Force the use of extensions in the                        
                                preview web server (by default,                           
                                extensionless URLs may be used).                          
    --preview-root <arg>        The path to the root of the preview                       
                                server, if not the output folder.                         
    -i, --input <arg>...        The path(s) of input files, can be                        
                                absolute or relative to the current                       
                                folder.                                                   
    -o, --output <arg>          The path to output files, can be                          
                                absolute or relative to the current                       
                                folder.                                                   
    -c, --config <arg>          Configuration file (by default,                           
                                config.wyam is used).                                     
    -u, --update-packages       Check the NuGet server for more recent                    
                                versions of each package and update them                  
                                if applicable.                                            
    --use-local-packages        Toggles the use of a local NuGet                          
                                packages folder.                                          
    --packages-path <arg>       The packages path to use (only if                         
                                use-local is true).                                       
    --output-script             Outputs the config script after it's                      
                                been processed for further debugging.                     
    --verify-config             Compile the configuration but do not                      
                                execute.                                                  
    --noclean                   Prevents cleaning of the output path on                   
                                each execution.                                           
    --nocache                   Prevents caching information during                       
                                execution (less memory usage but slower                   
                                execution).                                               
    -v, --verbose               Turns on verbose output showing                           
                                additional trace message useful for                       
                                debugging.                                                
    --pause                     Pause execution at the start of the                       
                                program until a key is pressed (useful                    
                                for attaching a debugger).                                
    --meta <arg>...             Specifies global metadata which can be                    
                                accessed from the engine or config file                   
                                (--meta key=value).                                       
    -l, --log [arg]             Log all trace messages to the specified                   
                                log file (by default,                                     
                                wyam-[datetime].txt).                                     
    --assembly-name <arg>...    Adds a reference to an assembly by name.                  
                                See below for syntax details.                             
    --assembly <arg>...         Adds a reference to an assembly by file                   
                                name or globbing pattern. See below for                   
                                syntax details.                                           
    --nuget-source <arg>...     Specifies an additional package source                    
                                to use. See below for syntax details.                     
    --nuget <arg>...            Adds a NuGet package (downloading and                     
                                installing it if needed). See below for                   
                                syntax details.                                           
    <root>                      The folder (or config file) to use.                       
                                                                                          
--assembly-name usage:                                                                    
                                                                                          
    <assembly>    The assembly to load by name.                                           
                                                                                          
--assembly usage:                                                                         
                                                                                          
    <assembly>    The assembly to load by file or globbing pattern.                       
                                                                                          
--nuget-source usage:                                                                     
                                                                                          
    <source>    The package source to add.                                                
                                                                                          
--nuget usage:                                                                            
                                                                                          
    -p, --prerelease         Specifies that prerelease packages are                       
                             allowed.                                                     
    -u, --unlisted           Specifies that unlisted packages are                         
                             allowed.                                                     
    -v, --version <arg>      Specifies the version of the package to                      
                             use.                                                         
    -s, --source <arg>...    Specifies the package source(s) to get the                   
                             package from.                                                
    -e, --exclusive          Indicates that only the specified package                    
                             source(s) should be used to find the                         
                             package.                                                     
    <package>                The package to install.
```

Note that some of the options such as `--nuget` are "nested" and must be contained in quotes if they contain options of their own (and any inner quotes must be escaped). For example, the following command will load a Nuget package named `Foo.Bar`:

```
Wyam.exe --nuget Foo.Bar 
```

However, if you want to specify any additional options for the `--nuget` option, you need to surround the whole value in quotes:

```
Wyam.exe --nuget "Foo.Bar -p" 
```

# Paths

The paths that Wyam uses can be specified on the command line. This includes the `--input` path(s) where Wyam will look for files, the `--output` path where Wyam will place the output of modules like [WriteFiles](/modules/writefiles), the `--config` file path where Wyam will look for a configuration file, and the root path that Wyam considers the base path for all other relative paths. By default, the root path is set to the path from where Wyam is executed. You may want to specify a different base path (for example, if running from a build script or as part of a larger process). In this case, just supply an alternate absolute path at the end of the command line. This new root path will be the base for all other relative paths including input and output paths. This is a good way to ensure consistency regardless of which path Wyam is run from.

Note that these paths can also be specified from within [your configuration script](/getting-started/configuration). From there you can set `FileSystem.OutputPath`, `FileSystem.RootPath`, or call `FileSystem.InputPaths.Add()`.

# Embedding

Creating a Wyam engine directly in your own application is also supported. The core Wyam library is available on NuGet as [Wyam.Core](https://www.nuget.org/packages/Wyam.Core). Once you've included it in your application, you will need to create an instance of the `Wyam.Core.Engine` class. See [the knowledgebase article on embedded use](/knowledgebase/embedded-use) for more information.