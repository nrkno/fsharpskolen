module ProgramApi.ExternalApi
open System
open System.IO
open System.Net.Http
open System.Text
open Newtonsoft.Json
open ProgramApi.Domain

type ProgramDto = {
    Id: string
    Title: string
    SourceMedium: int
    ShortDescription: string
}

let deserialize json =
    JsonConvert.DeserializeObject<ProgramDto>(json)

let httpClient = new HttpClient()
httpClient.BaseAddress <- Uri "http://psapi3-webapp-prod-we.azurewebsites.net"

let executeRequest id =
    async {
        let request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, (sprintf "programs/%s" id))
        let! response = httpClient.SendAsync(request) |> Async.AwaitTask
        let! stream = response.Content.ReadAsStreamAsync() |> Async.AwaitTask
        let reader = new StreamReader(stream, Encoding.UTF8)
        let content = reader.ReadToEnd()
        return (content, response.StatusCode)
    }
    |> Async.RunSynchronously

let fetchProgram (ProgramId id) =
    let (content, status) = executeRequest id
    match int status with
    | 200 -> Ok (deserialize content)
    | 404 -> Error <| NonExistingprogram
    | _ -> Error <| CouldNotFetchProgram
    

