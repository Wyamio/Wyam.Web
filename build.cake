// The following environment variables need to be set for Publish target:
// WYAM_GITHUB_TOKEN

#tool "nuget:https://api.nuget.org/v3/index.json?package=Wyam&version=2.1.3"
#addin "nuget:https://api.nuget.org/v3/index.json?package=Cake.Wyam&version=2.1.3"
#addin "nuget:https://api.nuget.org/v3/index.json?package=Octokit"
#addin "NetlifySharp"

using Octokit;
using NetlifySharp;

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var releaseDir = Directory("./release");
var sourceDir = releaseDir + Directory("repo");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("CleanSource")
    .Does(() =>
{
    if(DirectoryExists(sourceDir))
    {
        DeleteDirectory(sourceDir, new DeleteDirectorySettings
        {
            Force = true,
            Recursive = true
        });
    }    
});

Task("GetSource")
    .IsDependentOn("CleanSource")
    .Does(() =>
    {
        var githubToken = EnvironmentVariable("WYAM_GITHUB_TOKEN");
        GitHubClient github = new GitHubClient(new ProductHeaderValue("WyamDocs"))
        {
            Credentials = new Credentials(githubToken)
        };
	    // The GitHub releases API returns Not Found if all are pre-release, so need workaround below
        //Release release = github.Repository.Release.GetLatest("Wyamio", "Wyam").Result;        
	    Release release = github.Repository.Release.GetAll("Wyamio", "Wyam").Result.First();
	    FilePath releaseZip = DownloadFile(release.ZipballUrl);
        Unzip(releaseZip, releaseDir);
        
        // Need to rename the container directory in the zip file to something consistent
        var containerDir = GetDirectories(releaseDir.Path.FullPath + "/*").First(x => x.GetDirectoryName().StartsWith("Wyamio"));
        MoveDirectory(containerDir, sourceDir);
        Information($"Downloaded and unzipped { GetFiles(sourceDir.Path.FullPath + "/**/*").Count } files in { GetSubDirectories(sourceDir).Count } directories");
    });
    
Task("Generate-Themes")
    .Does(() =>
    {
        // Clean the output directory
        var output = Directory("./output");
        if(DirectoryExists(output))
        {
            CleanDirectory(output);
        }

        // Clean/create the scaffold directory
        var scaffold = Directory("./scaffold");
        if(!DirectoryExists(scaffold))
        {
            CreateDirectory(scaffold);
        }

        // Iterate the recipes
        foreach(DirectoryPath recipe in GetDirectories("./input/recipes/*"))
        {
            // Scaffold the recipe into a temporary directory
            CleanDirectory(scaffold);
            Wyam(new WyamSettings
            {
                Recipe = recipe.GetDirectoryName(),
                RootPath = scaffold,
                ArgumentCustomization = args => args.Prepend("new") 
            });

            // Iterate the themes
            foreach(FilePath theme in GetFiles(recipe.FullPath + "/themes/*.md"))
            {
                // See if this is a built-in theme by checking for "Preview" metadata
                if(!Context.FileSystem
                    .GetFile(theme)
                    .ReadLines(Encoding.UTF8)
                    .TakeWhile(x => !x.StartsWith("---"))
                    .Any(x => x.StartsWith("Preview:")))
                {
                    // Build the theme preview
                    string linkRoot = "/recipes/" + recipe.GetDirectoryName() + "/themes/preview/" + theme.GetFilenameWithoutExtension().FullPath;
                    Wyam(new WyamSettings
                    {
                        Recipe = recipe.GetDirectoryName(),
                        RootPath = scaffold,
                        Theme = theme.GetFilenameWithoutExtension().FullPath,
                        OutputPath = MakeAbsolute(Directory("./output" + linkRoot)).FullPath,
                        Settings = new Dictionary<string, object>
                        {
                            { "LinkRoot", linkRoot }
                        }
                    });
                }
            }
        }
        CleanDirectory(scaffold);
        DeleteDirectory(scaffold, new DeleteDirectorySettings
        {
            Force = true,
            Recursive = true
        });
    });

Task("Build")
    .IsDependentOn("GetSource")
    .IsDependentOn("Generate-Themes")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {
            NoClean = true,  // Cleaned in Generate-Themes task
            Recipe = "Docs",
            Theme = "Samson",
            UpdatePackages = true
        });        
    });
    
Task("Preview")
    .IsDependentOn("Generate-Themes")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {
            NoClean = true,  // Cleaned in Generate-Themes task
            Recipe = "Docs",
            Theme = "Samson",
            UpdatePackages = true,
            Preview = true            
        });
    });

Task("Debug")
    .Does(() =>
    {
        DotNetCoreBuild("../Wyam/tests/integration/Wyam.Examples.Tests/Wyam.Examples.Tests.csproj");        
        DotNetCoreExecute("../Wyam/tests/integration/Wyam.Examples.Tests/bin/Debug/netcoreapp2.1/Wyam.dll",
            "-a \"../Wyam/tests/integration/Wyam.Examples.Tests/bin/Debug/netcoreapp2.1/**/*.dll\" -r \"docs -i\" -t \"../Wyam/themes/Docs/Samson\" -p");
    });

Task("Deploy")
    .Does(() =>
    {
        var netlifyToken = EnvironmentVariable("NETLIFY_TOKEN");
        if(string.IsNullOrEmpty(netlifyToken))
        {
            throw new Exception("Could not get Netlify token environment variable");
        }

        Information("Deploying output to Netlify");
        var client = new NetlifyClient(netlifyToken);
        client.UpdateSite($"wyam.netlify.com", MakeAbsolute(Directory("./output")).FullPath).SendAsync().Wait();
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");
    
Task("BuildServer")
    .IsDependentOn("Build")
    .IsDependentOn("Deploy");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
