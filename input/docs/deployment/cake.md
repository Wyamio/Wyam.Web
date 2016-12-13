Title: Using Cake
Description: Instrument your build and deployment steps with Cake.
Order: 10
---
Wyam includes an official [Cake Build](http://cakebuild.net/) addin called [Cake.Wyam](https://www.nuget.org/packages/Cake.Wyam). This allows you to integrate Wyam into a more general Cake-based build and deployment process. To use it, you must include the appropriate `#tool` and `#addin` directive at the top of your Cake script (to load the Wyam engine and the Cake addin respectively). Note that you'll need to use the special URI syntax below since Wyam packages are still prerelease (due to having dependencies on other prerelease packages).

```
#tool nuget:?package=Wyam&prerelease
#addin nuget:?package=Cake.Wyam&prerelease

// ...

Task("Build")
    .Does(() =>
    {
        Wyam();        
    });
    
Task("Preview")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {
            Preview = true,
            Watch = true
        });        
    });
```

The Cake addin supports [all available command line options](/docs/usage/command-line) and is kept up to date with each Wyam release.

If you want to use the development feed to get the latest Wyam packages, change the directives at the top of the Cake build script to:

```
#tool nuget:https://www.myget.org/F/wyam/api/v2?package=Wyam&prerelease
#addin nuget:https://www.myget.org/F/wyam/api/v2?package=Cake.Wyam&prerelease
```

Note that you'll also need to delete the existing "tools\Wyam" and "tools\Addins\Cake.Wyam" folders if they exist (otherwise Cake will just use the existing packages and won't download the new packages).