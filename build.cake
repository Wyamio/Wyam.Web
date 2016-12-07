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

Task("Build")
    .Does(() =>
    {
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
                "Wyam.Html",
                "Wyam.Feeds",
                "Wyam.SearchIndex"
            }.Select(x => new NuGetSettings
            {
                Prerelease = true,
                Source = new [] { "https://www.myget.org/F/wyam/api/v3/index.json" },
                Package = x
            }),
            UpdatePackages = true
        });        
    });
    
Task("Preview")
    .Does(() =>
    {
        // Use this to launch local build of Wyam
        StartProcess("../Wyam/src/clients/Wyam/bin/Debug/wyam.exe",
            "-a \"../Wyam/src/**/bin/Debug/*.dll\" -r \"docs -i\" -t \"../Wyam/themes/Docs/Samson\" -p --attach");
/*
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
                "Wyam.Html",
                "Wyam.Feeds",
                "Wyam.SearchIndex"
            }.Select(x => new NuGetSettings
            {
                Prerelease = true,
                Source = new [] { "https://www.myget.org/F/wyam/api/v3/index.json" },
                Package = x
            }),
            UpdatePackages = true
            Preview = true
        });     */
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");

Task("GetArtifacts")
    .IsDependentOn("GetSource");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
