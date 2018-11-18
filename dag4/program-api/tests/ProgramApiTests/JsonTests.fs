module JsonTests

open Xunit
open ProgramApi.Json

type Person = {
    Fornavn: string
    Etternavn: string
    Alder: int
}
let person = { Fornavn = "Ola"; Etternavn = "Nordmann"; Alder = 42}

[<Fact>]
let test () = 
    let retrievedPerson = 
        person
        |> serialize
        |> deserialize 

    Assert.Equal(person, retrievedPerson)