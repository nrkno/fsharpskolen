module SerializationFsharp.PersonDomainBuilder

open System
open SerializationFsharp.Domain

let create : Person =
    {
        First = String50 "Firstname" 
        Last = String50 "Lastname" 
        Birthdate = Birthdate (DateTime.Parse "27.08.1993" )
    }

let withFirstname (name:string) (domain: Person): Person =
    { domain with First=(String50 name) }
    
let withLastname (name:string) (domain: Person): Person =
    { domain with Last=(String50 name) }

let withBirthdate (date:string) (domain: Person): Person =
    { domain with Birthdate=(Birthdate (DateTime.Parse date)) }