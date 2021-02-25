open System
open SerializationFsharp
open SerializationFsharp.Domain
open SerializationFsharp.Dto

type DtoError =
    | ValidationError of string
    | DeserializationException of Exception

let jsonFromDomain (person:Person) =
    person
    |> Mapper.fromDomain
    |> Json.serialize
    
let jsonToDomain (jsonString: string) : Result<Person,DtoError> =
    let deserializedValue =
        jsonString
        |> Json.deserialize<PersonDto>
    
    match deserializedValue with
    | Ok s ->
         s
         |> Mapper.toDomain
         |> Result.mapError ValidationError
    | Error e ->  Error e |> Result.mapError DeserializationException

[<EntryPoint>]
let main argv =
    let serialized = PersonDomainBuilder.create
    serialized |> printfn "%A"
    
    let jsonPerson =
        PersonDtoBuilder.create
        |> PersonDtoBuilder.withFirstname "Anders"
        |> PersonDtoBuilder.withLastname "Åsebø"
        |> PersonDtoBuilder.withBirthdate "27.08.1899"
        |> PersonDtoBuilder.serialized
    
    jsonPerson |> printfn "%A"
    
    let domain = jsonPerson |> jsonToDomain
    domain |> printfn "%A"
    0 