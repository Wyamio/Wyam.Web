#tool "nuget:https://api.nuget.org/v3/index.json?package=Wyam&prerelease"
#addin "nuget:https://api.nuget.org/v3/index.json?package=Cake.Wyam&prerelease"
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
    .IsDependentOn("GetSource")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {
            Recipe = "Docs",
            Theme = "Samson",
            UpdatePackages = true
        });        
    });
    
Task("Preview")
    .Does(() =>
    {
        // Use this to launch local build of Wyam
        //StartProcess("../Wyam/src/clients/Wyam/bin/Debug/wyam.exe",
        //    "-a \"../Wyam/src/**/bin/Debug/*.dll\" -r \"docs -i\" -t \"../Wyam/themes/Docs/Samson\" -p --attach");

        Wyam(new WyamSettings
        {
            Recipe = "Docs",
            Theme = "Samson",
            UpdatePackages = true,
            Preview = true
        });
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
