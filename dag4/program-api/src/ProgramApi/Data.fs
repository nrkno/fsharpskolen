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

let getFilePath (filename : string) : string = 
    Path.Combine(assemblyDirectory, filename)

module ManifestRepository = 

    type ManifestError = ManifestNotFound of string

    let getManifest (id : string) : Result<ManifestData, ManifestError> = 
        let filePath = id |> sprintf "%s-program-manifest.json" |> getFilePath 
        if File.Exists(filePath) then 
            let fileContent = File.ReadAllText(filePath)
            let data = Json.deserialize<ManifestData>(fileContent)
            Ok data 
        else 
            Error (ManifestNotFound id)

module MetadataRepository = 

    type MetadataError = MetadataNotFound of string

    let getManifest (id : string) : Result<ManifestData, MetadataError> = 
        let filePath = id |> sprintf "%s-program-metadata.json" |> getFilePath 
        if File.Exists(filePath) then 
            let fileContent = File.ReadAllText(filePath)
            let data = Json.deserialize<ManifestData>(fileContent)
            Ok data 
        else 
            Error (MetadataNotFound id)

module TransmissionsRepository = 

    type TransmissionsError = TransmissionsNotFound of string

    let getManifest (id : string) : Result<ManifestData, TransmissionsError> = 
        let filePath = id |> sprintf "%s-program-transmissions.json" |> getFilePath 
        if File.Exists(filePath) then 
            let fileContent = File.ReadAllText(filePath)
            let data = Json.deserialize<ManifestData>(fileContent)
            Ok data 
        else 
            Error (TransmissionsNotFound id)
