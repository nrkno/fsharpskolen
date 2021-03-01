module Deserializer 

open Newtonsoft.Json

let deserialize<'a> str =
    try
        JsonConvert.DeserializeObject<'a> str
        |> Ok
    with
        ex -> Error ex