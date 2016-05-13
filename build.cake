#tool nuget:?package=Wyam&prerelease
#addin nuget:?package=Cake.Wyam&prerelease

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var isLocal = BuildSystem.IsLocalBuild;
var isRunningOnUnix = IsRunningOnUnix();
var isRunningOnWindows = IsRunningOnWindows();
var isRunningOnAppVeyor = AppVeyor.IsRunningOnAppVeyor;
var isPullRequest = AppVeyor.Environment.PullRequest.IsPullRequest;

var sourcePath = isLocal
    ? "../../Wyam/src/Wyam.sln"     // Read from the current branch on disk in the actual repo
    : "../Code/Wyam/src/Wyam.sln";  // Read from the master Wyam branch in the Git submodule

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Build")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {
            GlobalMetadata = new Dictionary<string, string>
            {
                { "SourcePath", sourcePath }
            },
            OutputPath = isRunningOnAppVeyor ? "../Output" : null
        });        
    });
    
Task("Preview")
    .Does(() =>
    {
        Wyam(new WyamSettings
        {
            Preview = true,
            Watch = true,
            GlobalMetadata = new Dictionary<string, string>
            {
                { "SourcePath", sourcePath }
            }
        });        
    });
    
//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Preview");    
    
Task("AppVeyor")
    .IsDependentOn("Build");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
