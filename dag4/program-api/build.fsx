
// --------------------------------------------------------------------------------------
// FAKE build script
// --------------------------------------------------------------------------------------

#r "./packages/build/FAKE/tools/FakeLib.dll"

open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet
open Fake.IO
open Fake.IO.Globbing.Operators
open System

// --------------------------------------------------------------------------------------
// Build variables
// --------------------------------------------------------------------------------------

let buildDir  = "./build/"
let appReferences = !! "**/*.fsproj"
let testReferences = !! "**/tests/**/*.fsproj"
let dotnetcliVersion = "2.1.403"
let mutable dotnetExePath = "dotnet"

// --------------------------------------------------------------------------------------
// Helpers
// --------------------------------------------------------------------------------------

let run' timeout cmd args dir =
    let result =
        Process.execSimple (fun info ->
            { info with
                FileName = cmd
                Arguments = args
                WorkingDirectory = if String.isNullOrWhiteSpace dir then info.WorkingDirectory else dir }
        ) timeout
    if result <> 0 then failwithf "Error while running '%s' with args: %s" cmd args

let run = run' System.TimeSpan.MaxValue

let runDotnet workingDir args =
    let result =
        Process.execSimple (fun info ->
            { info with
                FileName = dotnetExePath
                WorkingDirectory = workingDir
                Arguments = args }
        ) TimeSpan.MaxValue
    if result <> 0 then failwithf "dotnet %s failed" args

// --------------------------------------------------------------------------------------
// Targets
// --------------------------------------------------------------------------------------

Target.create "Clean" (fun _ ->
    Shell.cleanDirs [buildDir]
)

Target.create "InstallDotNetCLI" (fun _ ->
    let setParams (options : DotNet.CliInstallOptions) =
        { options with
            Version = DotNet.Version dotnetcliVersion }
    DotNet.install setParams |> ignore
    )

Target.create "Restore" (fun _ ->
    appReferences
    |> Seq.iter (fun p ->
        let dir = System.IO.Path.GetDirectoryName p
        runDotnet dir "restore"
    )
)

Target.create "Build" (fun _ ->
    appReferences
    |> Seq.iter (fun p ->
        let dir = System.IO.Path.GetDirectoryName p
        runDotnet dir "build"
    )
)

Target.create "Test" (fun _ ->
    testReferences
    |> Seq.iter (fun p ->
        let dir = System.IO.Path.GetDirectoryName p
        runDotnet dir "test"
    )
)

// --------------------------------------------------------------------------------------
// Build order
// --------------------------------------------------------------------------------------

"Clean"
  ==> "InstallDotNetCLI"
  ==> "Restore"
  ==> "Build"
  ==> "Test"

Target.runOrDefault "Build"
