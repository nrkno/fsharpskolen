module Newtonsoft
open Newtonsoft.Json

let serialize obj =
    JsonConvert.SerializeObject obj
let deserialize<'a> str =
    try
        JsonConvert.DeserializeObject<'a> str
        |> Result.Ok
    with
        ex -> Result.Error ex