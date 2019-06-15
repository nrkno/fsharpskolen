module ProgramApi.ProgramRepository

open Domain
open ExternalApi


let programIds = [
    "MDRE20000117"
    "MUHH26000118"
    "MSPO48151019"
    "MUHU03001219"
    "KMTE60000119"
]

let mapMedium medium =
    match medium with
    | 1 -> "Tv"
    | 2 -> "Radio"
    | _ -> "Ukjent"

let fromDto (dto: ProgramDto) : Program =
    {
        Id = dto.Id
        Title = dto.Title
        Description = dto.ShortDescription
        Medium = mapMedium dto.SourceMedium
    }

let getProgram programId =
    fetchProgram programId
    |> Result.map fromDto


let getPrograms () = 
    programIds
    |> List.map (fun id -> { Id = id; Title = ""; Description = ""; Medium = ""})