//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Build");
var githubRef = Argument("ref", string.Empty); 
var version = Argument("package-version", "8.1.97");
var suffix = Argument("version-suffix", "alpha");
var configuration = Argument("configuration", "Release");
var solution = Argument("solution", "FileOnQ.Prism.Popups.XCT.sln");
var sample = Argument("sample", "./sample/FileOnQ.Prism.Popups.XCT.Sample.sln");
var csproj = Argument("csproj", "./src/FileOnQ.Prism.Popups.XCT/FileOnQ.Prism.Popups.XCT.csproj");

//////////////////////////////////////////////////////////////////////
// MSBuild Settings
//////////////////////////////////////////////////////////////////////
var msbuildSettings = new MSBuildSettings
{
    ToolVersion = MSBuildToolVersion.VS2019,
    Configuration = configuration
};

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectories("./src/**/bin/");
    Information("Cleaning Directory ./src/**/bin/");
    
    CleanDirectories("./src/**/obj/");
    Information("Cleaning Directory ./src/**/obj/");
    
    CleanDirectories("./Sample/**/bin/");
    Information("Cleaning Directory ./Sample/**/bin/");
    
    CleanDirectories("./Sample/**/obj/");
    Information("Cleaning Directory ./Sample/**/obj/");
});

Task("Restore")
    .Does(() =>
{
    NuGetRestore(solution);
});
    

Task("Build")
    .Does(() =>
{
    MSBuild(solution, msbuildSettings);
});

Task("Pack")
    .Does(() =>
{
    if (!string.IsNullOrEmpty(githubRef))
    {
        version = githubRef
            .Split("/")
            .LastOrDefault()
            .TrimStart('v');
          
        if (version.Contains("-dev."))
        {
            var segments = version.Split("-");
            version = segments[0];
            suffix = segments[1];
        }
        else
        {
            suffix = string.Empty;
        }
    }
    
    MSBuild(csproj, msbuildSettings
        .WithTarget("pack")
        .WithProperty("PackageVersion", string.IsNullOrEmpty(suffix) ? version : $"{version}-{suffix}")
        .WithProperty("Version", version)
        .WithProperty("AssemblyVersion", version) 
        .WithProperty("PackageOutputPath", "../../"));
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("CI-Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build");
    
Task("Package-Build")
    .IsDependentOn("CI-Build")
    .IsDependentOn("Pack");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);