module SerializationFsharp.PersonDtoBuilder

open System
open Dto

let create : PersonDto =
    {
        First = "Firstname" 
        Last = "Firstname" 
        Birthdate = DateTime.Parse "27.08.1993"
    }

let withFirstname (name:string) (dto: PersonDto): PersonDto =
    { dto with First=name }
    
let withLastname (name:string) (dto: PersonDto) : PersonDto =
    { dto with Last=name }

let withBirthdate (date:string) (dto: PersonDto) : PersonDto =
    { dto with Birthdate=(DateTime.Parse date) }

let serialized dto =
    dto
    |> Json.serialize