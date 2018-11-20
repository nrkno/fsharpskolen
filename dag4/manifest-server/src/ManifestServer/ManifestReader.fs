module ManifestServer.ManifestReader

open System.IO

open Newtonsoft.Json

let getFilePath (filename : string) : string = 
    Path.Combine("files", "manifests",  filename)

type UsageRightsData = {
    GeoBlocked : bool 
    FromUtc : string 
    ToUtc : string 
}

type ManifestData = {
    Id : string 
    Manifest : string 
    StreamFormat : string 
    Subtitles : string 
    SubtitlesFormat : string 
    Language : string 
    UsageRights : UsageRightsData 
}

type ManifestError = NotFound | ServerError

let getManifest (id : string) : Result<ManifestData, ManifestError> = 
    let filePath = id |> sprintf "%s-program-manifest.json" |> getFilePath 
    try
        if File.Exists(filePath) then 
            let fileContent = File.ReadAllText(filePath)
            Ok (JsonConvert.DeserializeObject<ManifestData> fileContent)
        else 
            Error NotFound
    with _ ->
        Error ServerError