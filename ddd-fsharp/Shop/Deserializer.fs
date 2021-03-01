module Deserializer 
open Order

open Newtonsoft.Json

let deserialize<'a> str : Result<'a, OrderLineError> =
    try
        JsonConvert.DeserializeObject<'a> str
        |> Ok
    with
        ex -> Error (JsonParseException (ParseException ex))