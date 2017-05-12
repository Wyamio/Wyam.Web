// The following environment variables need to be set for Publish target:
// WYAM_GITHUB_TOKEN

#tool "nuget:https://api.nuget.org/v3/index.json?package=Wyam"
#addin "nuget:https://api.nuget.org/v3/index.json?package=Cake.Wyam"
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
        Wyam(new WyamSettings
        {
            Recipe = "Docs",
            Theme = "Samson",
            UpdatePackages = true,
            Preview = true
        });
    });

Task("Debug")
    .Does(() =>
    {
        StartProcess("../Wyam/src/clients/Wyam/bin/Debug/wyam.exe",
            "-a \"../Wyam/src/**/bin/Debug/*.dll\" -r \"docs -i\" -t \"../Wyam/themes/Docs/Samson\" -p");
    });

Task("Deploy")
    .Does(() =>
    {
        string token = EnvironmentVariable("NETLIFY_WYAM");
        if(string.IsNullOrEmpty(token))
        {
            throw new Exception("Could not get NETLIFY_WYAM environment variable");
        }
        
        // This uses the Netlify CLI, but it hits the 200/min API rate limit
        // To use this, also need #addin "Cake.Npm"
        // Npm.Install(x => x.Package("netlify-cli"));
        // StartProcess(
        //    MakeAbsolute(File("./node_modules/.bin/netlify.cmd")), 
        //    "deploy -p output -s wyam -t " + token);

        // Upload via curl and zip instead
        Zip("./output", "output.zip", "./output/**/*");
        StartProcess("curl", "--header \"Content-Type: application/zip\" --header \"Authorization: Bearer " + token + "\" --data-binary \"@output.zip\" --url https://api.netlify.com/api/v1/sites/wyam.netlify.com/deploys");
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build");
    
Task("AppVeyor")
    .IsDependentOn("Build")
    .IsDependentOn("Deploy");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
