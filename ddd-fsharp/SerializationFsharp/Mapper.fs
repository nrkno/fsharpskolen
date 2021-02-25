module SerializationFsharp.Mapper
open Dto
open Domain

let fromDomain (person:Person): PersonDto =
        let first = person.First |> String50.value
        let last = person.Last |> String50.value
        let birthdate = person.Birthdate |> Birthdate.value
        { First = first; Last = last; Birthdate = birthdate }
    
let toDomain (dto:PersonDto) : Result<Person, string> =
    let first = dto.First |> String50.create (nameof dto.First)
    let last = dto.Last |> String50.create (nameof dto.First)
    let birthdate = dto.Birthdate |> Birthdate.create (nameof dto.Birthdate)
    
    match first, last, birthdate with
    | Ok f, Ok l, Ok b ->
        Ok {
            First = f
            Last = l
            Birthdate = b
        }
    | Error e, _, _ -> Error e
    | _, Error e, _ -> Error e
    | _, _, Error e -> Error e
    