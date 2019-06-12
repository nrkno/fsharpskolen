module ProgramApi.ProgramRepository

open Domain


let programIds = [
    "MDRE20000117"
    "MUHH26000118"
    "MSPO48151019"
    "MUHU03001219"
    "KMTE60000119"
]


let getProgram id = 
    programIds
    |> List.tryFind (fun pId -> pId = id)
    |> Option.map (fun id -> { Id = id })


let getPrograms () = 
    programIds
    |> List.map (fun id -> { Id = id})