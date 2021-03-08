module JSON

open System.Text.Json
open System.Text.Json.Serialization

let options = JsonSerializerOptions()
// options.Encoder <- JavaScriptEncoder.Create(UnicodeRanges.All)
// options.WriteIndented <- false
// options.IgnoreNullValues <- false
// options.DefaultIgnoreCondition <- JsonIgnoreCondition.Always

let serialize obj = 
    JsonSerializer.Serialize(obj)
let deserialize<'a> (jsonString: string): 'a = 
    JsonSerializer.Deserialize<'a>(jsonString, options)