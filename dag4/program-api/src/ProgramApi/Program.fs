module ProgramApi.Main

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe

open ProgramApi.Dto

let tryProgramHandler id : Result<string, string> = 
    Error "not implemented yet, should be some kind of lookup"
    //let program = { 
    //    ProgId = id 
    //    Title = 
    //}
    //json program

type dummyResult = { result : string }

let programHandler id  = 
    match tryProgramHandler id with 
    | Ok _ -> json <| { result = "ok!" }
    | Error e -> json <| { result = e }

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
