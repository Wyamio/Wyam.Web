Title: Deployment
Description: What to do after you've built your site.
Order: 6
---
One of the many benefits of static generation is that the end result is a collection of final files. By default, Wyam places the result of your build in the `output` folder. You can manually deploy your site by simply uploading these files to your host.

# Using Cake
---
For more advanced deployment scenarios Wyam includes an official [Cake Build](http://cakebuild.net/) addin called [Cake.Wyam](https://www.nuget.org/packages/Cake.Wyam). This allows you to integrate Wyam into a more general Cake-based build and deployment process. To use it, yuo must include the appropriate `#tool` and `#addin` directive at the top of your Cake script (to load the Wyam engine and the Cake addin respectively). Note that you'll need to use the special URI syntax below since Wyam packages are still prerelease (due to having dependencies on other prerelease packages).

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

The Cake addin supports [all available command line options](/getting-started/usage) and is kept up to date with each Wyam release.

# Using AppVeyor
---
While the Cake script discussed above could certainly be used on AppVeyor, you can also control the build and deployment process directly using AppVeyor's own scripting capability. More information on this [can be found in the knowledgebase](/knowledgebase/continuous-integration).