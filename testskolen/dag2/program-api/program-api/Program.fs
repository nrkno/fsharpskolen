module ProgramApi.Program


open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open ProgramRepository


type ErrorResult = { Wrong : string }

let programHandler id  = 
    match getProgram id with
    Some program -> json program
    | None -> json { Wrong = (sprintf "Fant ikke program med id %s" id) }

let programListHandler () = 
    json (getPrograms ())

let webApp =
    choose [
        GET >=> choose [
            routef "/programs/%s" programHandler
            route "/programs" >=> programListHandler ()
        ]
        route "/ping"   >=> text "pong"
        route "/"       >=> text "Hei!" ]

let configureApp (app : IApplicationBuilder) =
    app.UseGiraffe webApp

let configureServices (services : IServiceCollection) =
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

