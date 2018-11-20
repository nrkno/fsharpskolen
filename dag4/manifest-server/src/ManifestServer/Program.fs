module ManifestServer.Main

open System
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe

open ManifestServer.ManifestReader

let manifestHandler id  = 
    let result = getManifest id
    match result with 
    | Ok p -> json p 
    | Error ServerError -> ServerErrors.INTERNAL_ERROR "Det skjedde en feil!"
    | Error NotFound -> RequestErrors.NOT_FOUND (sprintf "Fant ikke manifest for %s" id)

let webApp =
    choose [
        GET >=> choose [
            routef "/manifest/%s" manifestHandler
        ]
        route "/" >=> text "Velkommen til manifestserver!" ]

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
        .UseUrls("http://localhost:7000")
        .Configure(Action<IApplicationBuilder> configureApp)
        .ConfigureServices(configureServices)
        .Build()
        .Run()
    0