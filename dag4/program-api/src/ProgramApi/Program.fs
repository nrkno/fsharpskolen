module ProgramApi.Main

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe

open ProgramApi.Dto

let programHandler id = 
    let program = { ProgId = id }
    json program

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
