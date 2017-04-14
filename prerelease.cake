#tool "nuget:https://www.myget.org/F/wyam?package=Wyam&prerelease"
#addin "nuget:https://www.myget.org/F/wyam?package=Cake.Wyam&prerelease"
#addin "nuget:https://api.nuget.org/v3/index.json?package=Octokit"

using Octokit;

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
var addinDir = releaseDir + Directory("addins");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("CleanSource")
    .Does(() =>
{
    if(DirectoryExists(sourceDir))
    {
        DeleteDirectory(sourceDir, true);
    }    
});

Task("GetSource")
    .IsDependentOn("CleanSource")
    .Does(() =>
    {
        GitHubClient github = new GitHubClient(new ProductHeaderValue("WyamDocs"));
	    // The GitHub releases API returns Not Found if all are pre-release, so need workaround below
        //Release release = github.Repository.Release.GetLatest("Wyamio", "Wyam").Result;        
	    Release release = github.Repository.Release.GetAll("Wyamio", "Wyam").Result.First();
	    FilePath releaseZip = DownloadFile(release.ZipballUrl);
        Unzip(releaseZip, releaseDir);
        
        // Need to rename the container directory in the zip file to something consistent
        var containerDir = GetDirectories(releaseDir.Path.FullPath + "/*").First(x => x.GetDirectoryName().StartsWith("Wyamio"));
        MoveDirectory(containerDir, sourceDir);
    });

Task("CleanAddinPackages")
    .Does(() =>
{
    CleanDirectory(addinDir);
});

Task("GetAddinPackages")
    .IsDependentOn("CleanAddinPackages")
    .Does(() => 
{
    string[] addinLines = System.IO.File.ReadAllLines(GetFiles("./addins.txt").First().FullPath);
    foreach(string addinLine in addinLines)
    {
        string[] addinColumns = addinLine.Split(' ');
        NuGetInstall(addinColumns[0],
            new NuGetInstallSettings
            {
                OutputDirectory = addinDir,
                Prerelease = true,
                Verbosity = NuGetVerbosity.Quiet,
                Source = new [] { "https://api.nuget.org/v3/index.json" },
                NoCache = true
            });
    }
});
    
Task("Preview")
    .IsDependentOn("GetSource")
    .IsDependentOn("GetAddinPackages")
    .Does(() =>
    {
        var addinAssemblies = System.IO.File.ReadAllLines(GetFiles("./addins.txt").First().FullPath)
            .SelectMany(x => x.Split(' ').Skip(1))
            .Select(x => "../release/addins/**/" + x);
        Wyam(new WyamSettings
        {
            Recipe = "Docs -i",
            Theme = "Samson -i",
            NuGetPackages = new []
            {
                "Wyam.Docs",
                "Wyam.Docs.Samson",
                "Wyam.Markdown",
                "Wyam.Razor",
                "Wyam.Yaml",
                "Wyam.CodeAnalysis",
                "Wyam.Less",
                "Wyam.Html"
            }.Select(x => new NuGetSettings
            {
                Prerelease = true,
                Source = new [] { "https://www.myget.org/F/wyam/api/v3/index.json" },
                Package = x
            }),
            UpdatePackages = true,
            Preview = true,
            Watch = true,
            Settings = new Dictionary<string, object>
            {
                { "AssemblyFiles",  addinAssemblies }
            }
        });
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Preview");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
