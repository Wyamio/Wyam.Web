Title: Command Line
Description: Describes how to run Wyam from the command line and the available options.
Order: 2
RedirectFrom: getting-started/usage
---
To execute Wyam you need to run either `wyam` (if you installed it as a global tool) or `dotnet /path/to/wyam/Wyam.dll` (if you downloaded the ZIP archive) followed by any Wyam arguments. If you don't specify any arguments, the root folder will be set to the current folder and if a file named `config.wyam` is found, it will be used as the configuration file. You can also specify a root folder that's different than the current folder after the `wyam` command. For example:

```
wyam
```

Or:

```
wyam C:\MySite
```

There are several different available commands (use `--help` or `-?` to see them):

```
>wyam --help            

usage:  <command> [<args>]                                                
                                                                          
    build      Runs the build process (this is the default command).      
    new        Scaffolds the given recipe into a specified path.          
    preview    Runs the preview server without generating anything.       
    help       Displays various help messages.                            
```

Each command has its own set of options. The main command that gets executed
by default is `build` which builds the site given the options you specify:

```
>wyam build --help

usage:  build [-v] [--attach] [-w] [-p [arg]] [--force-ext]                     
        [--virtual-dir <arg>] [--preview-root <arg>] [-i <arg>...]              
        [-o <arg>] [-c <arg>] [-u] [--use-local-packages]                       
        [--use-global-sources] [--packages-path <arg>] [--output-script]        
        [--verify-config] [--noclean] [--nocache] [-l [arg]]                    
        [-g <arg>...] [--initial <arg>...] [--ns <arg>...] [-r <arg>]           
        [-a <arg>...] [-t <arg>] [-n <arg>...] [--] <root>                      
                                                                                
    -v, --verbose                    Turns on verbose output showing            
                                     additional trace message useful for        
                                     debugging.                                 
    --attach                         Pause execution at the start of the        
                                     program until a debugger is                
                                     attached.                                  
    -w, --watch                      Watches the input folder for any           
                                     changes.                                   
    -p, --preview [arg]              Start the preview web server on the        
                                     specified port (default is 5080).          
    --force-ext                      Force the use of extensions in the         
                                     preview web server (by default,            
                                     extensionless URLs may be used).           
    --virtual-dir <arg>              Serve files in the preview web             
                                     server under the specified virtual         
                                     directory.                                 
    --preview-root <arg>             The path to the root of the preview        
                                     server, if not the output folder.          
    -i, --input <arg>...             The path(s) of input files, can be         
                                     absolute or relative to the current        
                                     folder.                                    
    -o, --output <arg>               The path to output files, can be           
                                     absolute or relative to the current        
                                     folder.                                    
    -c, --config <arg>               Configuration file (by default,            
                                     config.wyam is used).                      
    -u, --update-packages            Check the NuGet server for more            
                                     recent versions of each package and        
                                     update them if applicable.                 
    --use-local-packages             Toggles the use of a local NuGet           
                                     packages folder.                           
    --use-global-sources             Toggles the use of the global NuGet        
                                     sources (default is false).                
    --packages-path <arg>            The packages path to use (only if          
                                     use-local is true).                        
    --output-script                  Outputs the config script after            
                                     it's been processed for further            
                                     debugging.                                 
    --verify-config                  Compile the configuration but do           
                                     not execute.                               
    --noclean                        Prevents cleaning of the output            
                                     path on each execution.                    
    --nocache                        Prevents caching information during        
                                     execution (less memory usage but           
                                     slower execution).                         
    -l, --log [arg]                  Log all trace messages to the              
                                     specified log file (by default,            
                                     wyam-[datetime].txt).                      
    -g, --global <arg>...            Specifies global metadata as a             
                                     sequence of key=value pairs. Use           
                                     the syntax [x,y] to specify array          
                                     values.                                    
    --initial <arg>...               Specifies initial document metadata        
                                     as a sequence of key=value pairs.          
                                     Use the syntax [x,y] to specify            
                                     array values.                              
    --ns, --nuget-source <arg>...    Specifies an additional package            
                                     source to use.                             
    -r, --recipe <arg>               Specifies a recipe to use. See             
                                     below for syntax details.                  
    -a, --assembly <arg>...          Adds an assembly reference by name,        
                                     file name, or globbing pattern.            
    -t, --theme <arg>                Specifies a theme to use. See below        
                                     for syntax details.                        
    -n, --nuget <arg>...             Adds a NuGet package (downloading          
                                     and installing it if needed). See          
                                     below for syntax details.                  
    <root>                           The folder (or config file) to use.        
                                                                                
--recipe usage:                                                                 
                                                                                
    -i, --ignore-known-packages    Ignores (does not add) packages for          
                                   known recipes.                               
    <recipe>                       The recipe to use.                           
                                                                                
--theme usage:                                                                  
                                                                                
    -i, --ignore-known-packages    Ignores (does not add) packages for          
                                   known themes.                                
    <theme>                        The theme to use.                            
                                                                                
--nuget usage:                                                                  
                                                                                
    -p, --prerelease         Specifies that prerelease packages are             
                             allowed.                                           
    -u, --unlisted           Specifies that unlisted packages are               
                             allowed.                                           
    -v, --version <arg>      Specifies the version range of the package         
                             to use.                                            
    -l, --latest             Specifies that the latest available version        
                             of the package should be used (this will           
                             always trigger a request to the sources).          
    -s, --source <arg>...    Specifies the package source(s) to get the         
                             package from.                                      
    -e, --exclusive          Indicates that only the specified package          
                             source(s) should be used to find the               
                             package.                                           
    <package>                The package to install.                              
```

# Nested Arguments

Note that some of the arguments such as `--nuget` are "nested" and must be contained in quotes if they contain options of their own (and any inner quotes must be escaped). For example, the following command will load a Nuget package named `Foo.Bar`:

```
wyam --nuget Foo.Bar 
```

However, if you want to specify any additional options for the `--nuget` option such as to accept pre-release packages, you need to surround the whole value in quotes:

```
wyam --nuget "Foo.Bar -p" 
```

# Embedded Web server

Wyam includes an embedded web server you can use to test your site locally. It includes support for ignoring file extensions and implicitly delivering a `index.html` page if one exists. This is similar to the convention a lot of static site hosts such as GitHub Pages uses.

To activate the preview server, use the `-p` or `--preview` argument. This will make the site available on a URL like `http://localhost:5080`. If you prefer a different port, add that after the preview argument. This will cause the Wyam process to remain open in your console until you hit a key to exit.

You can change where the preview server pulls files from with the `--preview-root` argument.

You can serve files under a virtual directory with the `--virtual-dir` option. This will make all of files on disk appear as if they were under the specified path on the web server. Requesting any resources or pages outside of this virtual directory will result in 404 Not Found errors.

# File Watching

You can turn on file watching with the `-w` or `--watch` argument. When file watching is enabled, the generation will be re-run any time a file in one of the input folders changes. Some modules can cache information from one generation to the next, so re-generations after a watched file changes should be somewhat faster. Note that because Wyam makes no assumptions about dependencies between files, modules, and pipelines, it is not possible to only regenerate the effects of the file that changed. We don't know what modules that file may have impacted indirectly and therefore need to regenerate everything (it's possible that in the future this could get smarter, but it's a very hard problem). Also note that some modules like [Less](/modules/less) may not reflect changes to their underlying files due to the mechanics of the module.

# LiveReload

> Requires Wyam 0.17.0 or greater.

Wyam comes with [LiveReload](http://livereload.com) support built-in and it is automatically enabled when the preview server is used while file watching. LiveReload works by injecting JavaScript into HTML pages when served using the embedded preview server, no changes to your content need to be made. When any input files are re-generated, Wyam will refresh all connected browsers automagically.

The LiveReload server listens on port `35729` by default. When using LiveReload in conjunction with the preview server, ensure that any relevant HTML files have a closing `<body/>` tag as this is required for `<script>` injection.

# Paths

The paths that Wyam uses can be specified on the command line. This includes the input path(s) specified by `--input` where Wyam will look for files, the output path specified by `--output` where Wyam will place the output of modules like [WriteFiles](/modules/writefiles), the config file path specified by `--config` where Wyam will look for a configuration file, and the root path specified as the final argument that Wyam considers the base path for all other relative paths. By default, the root path is set to the path from where Wyam is executed. You may want to specify a different base path (for example, if running from a build script or as part of a larger process). In this case, just supply an alternate absolute path at the end of the command line. This new root path will be the base for all other relative paths including input and output paths. This is a good way to ensure consistency regardless of which path Wyam is run from.

Note that these paths can also be specified from within [your configuration script](/docs/usage/configuration). From there you can set [`FileSystem.OutputPath`](/api/Wyam.Common.IO/IFileSystem/6DB77CDF), [`FileSystem.RootPath`](/api/Wyam.Common.IO/IFileSystem/3D1098EC), or call [`FileSystem.InputPaths.Add()`](/api/Wyam.Common.IO/IFileSystem/540AF0EB).