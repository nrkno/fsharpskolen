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
    let create (roleStr : string) (nameStr : string) (givenNameStr : string option) (familyNameStr : string option) : Contributor = 
        match Role.create roleStr with 
        | AnonymousRole -> Anonymous 
        | ChildRole -> Child 
        | OtherRole r -> 
          let info = { role = r 
                       name = Name.create nameStr
                       givenName = Option.map Name.create givenNameStr
                       familyName = Option.map Name.create familyNameStr }
          Contributor info 
      
let contributor1 = Contributor.create "Vert" "Vemund" None None 

let contributor2 = Contributor.create "Skuespiller" "Sofia Stiansen" (Some "Sofia") (Some "Stiansen")

let contributor3 = Contributor.create "Medvirkende barn" "Oda (5)" (Some "Oda") (Some "Olaussen")
      
let contributor4 = Contributor.create "anonym" "NN" None None

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
