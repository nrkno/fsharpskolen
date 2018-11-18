module ProgramApi.Json 

open Newtonsoft.Json

let serialize obj = 
    JsonConvert.SerializeObject obj

let deserialize<'a> str = 
    JsonConvert.DeserializeObject<'a> str