module SerializationFsharp.TextJson
open System.Text.Json
open System.Text.Unicode
open System.Text.Encodings.Web

let options = JsonSerializerOptions()
options.Encoder <- JavaScriptEncoder.Create(UnicodeRanges.All)
options.WriteIndented <- false 

let serialize obj =
    JsonSerializer.Serialize(obj, options)
let deserialize<'a> (str: string) =
    try
        JsonSerializer.Deserialize<'a> str 
        |> Result.Ok
    with
        ex -> Result.Error ex