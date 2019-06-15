module ProgramApi.Program
open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open ProgramApi
open ProgramRepository
open Domain


type ErrorResult = { Wrong : string }

let programHandler id  =
    let result =
        id
        |> Domain.validate
        |> Result.bind getProgram
    match result with
    | Ok program -> json program
    | Error Error.InvalidprogramId -> RequestErrors.badRequest <| json { Wrong = (sprintf "Ugyldig format på id %s" id) }
    | Error Error.NonExistingprogram -> RequestErrors.notFound <| json { Wrong = (sprintf "Fant ikke program med id %s" id) }
    
let currentTime () =
   json DateTime.Now

let programListHandler () = 
    json (getPrograms ())

let webApp =
    choose [
        GET >=> choose [
            routef "/programs/%s" programHandler
            route "/programs" >=> programListHandler ()
        ]
        route "/ping"   >=> text "pong"
        route "/"       >=> text "Hei!"
        route "/date" >=> currentTime ()
        route "/date2" >=> warbler (fun _ -> currentTime ()) ]

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

