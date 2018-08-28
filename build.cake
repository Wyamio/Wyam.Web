// The following environment variables need to be set for Publish target:
// WYAM_GITHUB_TOKEN

#tool "nuget:https://api.nuget.org/v3/index.json?package=Wyam&version=1.5.1"
#addin "nuget:https://api.nuget.org/v3/index.json?package=Cake.Wyam&version=1.5.1"
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
        StartProcess("../Wyam/src/clients/Wyam/bin/Debug/net462/wyam.exe",
            "-a \"../Wyam/tests/integration/Wyam.Examples.Tests/bin/Debug/net462/**/*.dll\" -r \"docs -i\" -t \"../Wyam/themes/Docs/Samson\" -p");
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
