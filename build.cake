#addin "Cake.Incubator"
#tool "xunit.runner.console"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var binDir = "";       // Destination Binary File Directory name i.e. bin
var projJson = "";     // Path to the project.json
var projDir = "";      //  Project Directory
var solutionFile = "LockOnCode.VirtualMachine.sln"; // Solution file if needed
var outputDir = Directory("Build") + Directory(configuration);  // The output directory the build artifacts saved too

var buildSettings = new DotNetCoreBuildSettings
     {
         Framework = "netcoreapp1.1",
         Configuration = "Release",
         OutputDirectory = outputDir
     };


Task("Clean")
    .Does(() =>
{
    if (DirectoryExists(outputDir))
        {
            DeleteDirectory(outputDir, recursive:true);
        }
});
 
Task("Restore")
    .Does(() => {
        DotNetCoreRestore(solutionFile);
    });
 
Task("Build")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .Does(() => {
	    
		DotNetCoreBuild(solutionFile, buildSettings);
    
    });

	Task("UnitTest")
	.IsDependentOn("Build")
	.Does(() => {
		Information("Start Running Tests");
		var testSettings = new DotNetCoreTestSettings
		{
			NoBuild = true,
			DiagnosticOutput = true,
            Configuration = "Debug",
            Logger = "trx;LogFileName=Build\unit_tests.xml" 

		};
		
        var directoryToScanForTests = "./"+outputDir+"/*Tests.csproj";
        Information("Scanning directory for tests: " + directoryToScanForTests);
        
		var testProjects = GetFiles(directoryToScanForTests);
		foreach(var testProject in testProjects)
		{
			Information("Found Test Project: " + testProject);
			DotNetCoreTest(testProject.ToString(), testSettings);
		}
		//XUnit2(testAssemblies);*/
		
		//DotNetCoreTest(testProject);
		});
 
Task("Package")
    .IsDependentOn("Build")
    .Does(() => {
        var packSettings = new DotNetCorePackSettings
        {
            OutputDirectory = outputDir,
            NoBuild = true
        };
 
         DotNetCorePack(projJson, packSettings);
 
    });
 
 
//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////
 
Task("Default")
    .IsDependentOn("UnitTest");
 
//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////
 
RunTarget(target);