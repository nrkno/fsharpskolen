open System
open SerializationFsharp
open SerializationFsharp.Domain
open SerializationFsharp.Dto
open TextJson 

type DtoError =
    | ValidationError of string
    | DeserializationException of Exception

let jsonFromDomain (person:Person) =
    person
    |> Mapper.fromDomain
    |> serialize
    
let jsonToDomain (jsonString: string) : Result<Person,DtoError> =
    let deserializedValue =
        jsonString
        |> deserialize
    
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
        |> PersonDtoBuilder.withBirthdate "27.08.1999"
        |> PersonDtoBuilder.serialized
    
    jsonPerson |> printfn "%A"
    
    let domain = jsonPerson |> jsonToDomain
    domain |> printfn "%A"
    0 