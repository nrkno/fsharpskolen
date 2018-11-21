module ProgramApi.Data

open System
open System.IO
open System.Reflection

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
    Duration : string
    UsageRights : UsageRightsData 
}

type TransmissionData = {
    Channel : string 
    EpgDate : string 
    StartTime : string 
    EndTime : string
}

type TransmissionsData = {
    Id : string 
    ProgramTransmissions : TransmissionData list 
}

type MetadataData = {  // :-)
    Id : string 
    Title : string 
    Description : string 
    Rated : string 
    Duration : string
}

let assemblyDirectory = 
    let codebase = Assembly.GetExecutingAssembly().CodeBase
    let uriBuilder = UriBuilder(codebase)
    let path = Uri.UnescapeDataString(uriBuilder.Path)
    Path.GetDirectoryName(path)

let combinePath path1 path2 = 
    Path.Combine(path1, path2)

let filesPath = combinePath assemblyDirectory "files"

module ManifestRepository = 

    let getManifest (id : string) : Result<ManifestData, string> = 
        let manifestsPath = combinePath filesPath "manifests"
        let filePath = id |> sprintf "%s-program-manifest.json" |> combinePath manifestsPath
        if File.Exists(filePath) then 
            let fileContent = File.ReadAllText(filePath)
            let data = Json.deserialize<ManifestData>(fileContent)
            Ok data 
        else 
            Error (sprintf "Not found: %s [%s]" id filePath)

module MetadataRepository = 

    let getMetadata (id : string) : Result<MetadataData, string> = 
        let metadataPath = combinePath filesPath "metadata"
        let filePath = id |> sprintf "%s-program-metadata.json" |> combinePath metadataPath 
        if File.Exists(filePath) then 
            let fileContent = File.ReadAllText(filePath)
            let data = Json.deserialize<MetadataData>(fileContent)
            Ok data 
        else 
            Error (sprintf "Not found: %s [%s]" id filePath)

module TransmissionsRepository = 

    let getTransmissions (id : string) : Result<TransmissionsData, string> = 
        let transmissionsPath = combinePath filesPath "transmissions"
        let filePath = id |> sprintf "%s-program-transmissions.json" |> combinePath transmissionsPath 
        if File.Exists(filePath) then 
            let fileContent = File.ReadAllText(filePath)
            let data = Json.deserialize<TransmissionsData>(fileContent)
            Ok data 
        else 
            Error (sprintf "Not found: %s [%s]" id filePath)
