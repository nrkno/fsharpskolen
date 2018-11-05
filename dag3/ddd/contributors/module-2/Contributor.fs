open System

type Name = private Name of string 
type ValidRole = private ValidRole of string 
type Role = private AnonymousRole | ChildRole | OtherRole of ValidRole 
type ValidContributor = 
    private { role : ValidRole
              name : Name 
              givenName : Name option 
              familyName : Name option }
type Contributor = 
    private Anonymous | Child | Contributor of ValidContributor 

module Name = 
  let create (s : string) : Name = 
      if isNull s then raise (ArgumentNullException("s"))
      else if s.Length = 0 then raise (ArgumentException("s"))
      else Name s

module Role = 
    let create (s : string) : Role = 
        if isNull s then raise (ArgumentNullException("s"))
        else if s.Length = 0 then raise (ArgumentException("s"))
        else if s.Contains("barn") then ChildRole 
        else if s.Contains("anonym") then AnonymousRole 
        else OtherRole (ValidRole s)
  
module Contributor = 
    let create (role : Role) (name : Name) (givenName : Name option) (familyName : Name option) : Contributor = 
        match role with 
        | AnonymousRole -> Anonymous 
        | ChildRole -> Child 
        | OtherRole r -> 
          let info = { role = r 
                       name = name
                       givenName = givenName
                       familyName = familyName }
          Contributor info 
      
let contributor1 = 
    Contributor.create (Role.create "Vert")
                       (Name.create "Vemund") 
                       None 
                       None 

let contributor2 = 
    Contributor.create (Role.create "Skuespiller")
                       (Name.create "Sofia Stiansen")
                       (Some (Name.create "Sofia"))
                       (Some (Name.create "Stiansen"))

let contributor3 = 
    Contributor.create (Role.create "Medvirkende barn")
                       (Name.create "Oda (5)") 
                       (Some (Name.create "Oda"))
                       (Some (Name.create "Olaussen"))
      
let contributor4 = 
    Contributor.create (Role.create "anonym")
                       (Name.create "NN")
                       None 
                       None

contributor1 |> printfn "%A"
contributor2 |> printfn "%A"
contributor3 |> printfn "%A"
contributor4 |> printfn "%A"

let contributors = [ contributor1; contributor2; contributor3; contributor4 ]

let selectValid (contributor : Contributor) : ValidContributor option = 
    match contributor with 
    | Contributor valid -> Some valid 
    | _ -> None
  
let validContributors = contributors |> List.choose selectValid 

validContributors |> printfn "%A"
