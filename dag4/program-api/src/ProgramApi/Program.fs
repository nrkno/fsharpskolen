module ProgramApi.Main

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe

open ProgramApi.Dto
open ProgramApi.ProgramRepository

type errorResult = { wrong : string }

let programHandler id  = 
    let program = findProgram id 
    let result = program |> Result.map Program.fromDomainProgram
    match result with 
    | Ok p -> json p 
    | Error e -> json { wrong = e }

let webApp =
    choose [
        GET >=> choose [
            routef "/program/%s" programHandler
        ]
        route "/ping"   >=> text "pong"
        route "/"       >=> text "Hei!" ]

let configureApp (app : IApplicationBuilder) =
    // Add Giraffe to the ASP.NET Core pipeline
    app.UseGiraffe webApp

let configureServices (services : IServiceCollection) =
    // Add Giraffe dependencies
    services.AddGiraffe() |> ignore

[<EntryPoint>]
let main _ =
    WebHostBuilder()
        .UseKestrel()
        .Configure(Action<IApplicationBuilder> configureApp)
        .ConfigureServices(configureServices)
        .Build()
        .Run()
    0
