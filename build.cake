#module nuget:?package=Cake.DotNetTool.Module&version=0.4.0
#addin "nuget:?package=Cake.Coverlet&version=2.5.4"
#tool dotnet:?package=dotnet-reportgenerator-globaltool&version=4.6.7

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var coverletDirectory = "./coverlet";
var nugetFeed = "https://nuget.pkg.github.com/StirlingLabs/index.json";
var artifactDir = Argument ("artifactDir", "./artifacts");
FilePath filePath = "./coverlet/results-XKCP.NET.Tests.xml";
///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////
Task ("Clean")
    .Does (() => {
        var settings = new DeleteDirectorySettings {
            Recursive = true,
            Force = true
        };
        if(DirectoryExists(coverletDirectory))
        {
            CleanDirectory(coverletDirectory);
        }
        var binDirs = GetDirectories ("./src/**/bin");
        var objDirs = GetDirectories ("./src/**/obj");
        CleanDirectories (binDirs);
        CleanDirectories (objDirs);
        DeleteDirectories (binDirs, settings);
        DeleteDirectories (objDirs, settings);
        DotNetCoreClean (".");
    });

Task ("Restore")
    .Does (() => {
        DotNetCoreRestore (".");
    });

Task ("Build")
    .IsDependentOn ("Clean")
    .IsDependentOn ("Restore")
    .Does (() => {
        var settings = new DotNetCoreBuildSettings {
        Configuration = configuration
        };
        DotNetCoreBuild (".", settings);
    });
Task("Run-Tests")
    .Does(() =>
{
    var settings = new DotNetCoreTestSettings
    {
        Configuration = configuration,
        NoBuild=true,
		//Logger = "GitHubActions"
    }; 
    if(GitHubActions.IsRunningOnGitHubActions)
    {
        settings.Logger = "GitHubActions";
    }
    
    var coverletSettings = new CoverletSettings();
    coverletSettings.CollectCoverage =true;
    coverletSettings.CoverletOutputFormat= CoverletOutputFormat.opencover;
    coverletSettings.CoverletOutputDirectory = "./coverlet";
    var files = GetFiles("./test/**/*.csproj");
    Information($"Found {files.Count} test project! ");

    var reportFileName = string.Empty;
    foreach(var file in files){
        reportFileName= $"results-{file.GetFilenameWithoutExtension()}.xml";
        Information($"Generate report file {reportFileName}");
        coverletSettings.CoverletOutputName = reportFileName;
        DotNetCoreTest(file.FullPath,settings,coverletSettings);
    }
    var generatorSettings = new ReportGeneratorSettings();
    generatorSettings.ReportTypes= new ReportGeneratorReportType[]{ReportGeneratorReportType.lcov};
    ReportGenerator(filePath,coverletDirectory,generatorSettings);    
});
Task ("Pack")
	.Does (() => {
		var projectPath = "./src/XKCP.NET/XKCP.NET.csproj";
		var settings = new DotNetCorePackSettings {
			Configuration = configuration,
			OutputDirectory = artifactDir,
			NoBuild =true,
			ArgumentCustomization = args => {				
				return args;
			}
		};
		DotNetCorePack(projectPath, settings);
	});
Task("Push")
.Does(()=>{
	var apiKey=	EnvironmentVariable("GITHUB_TOKEN");
	 var settings = new DotNetCoreNuGetPushSettings
     {
         Source = nugetFeed,
         ApiKey =apiKey,
     };
	 var path =  GetFiles ("./XKCP.NET.*.nupkg").First ();
     DotNetCoreNuGetPush(path.FullPath, settings);
});

RunTarget(target);