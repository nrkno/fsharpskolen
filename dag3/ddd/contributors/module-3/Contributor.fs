open System

type Name = private Name of string 
type Role = private Role of string 
type Contributor = 
    private { role : Role
              name : Name 
              givenName : Name option 
              familyName : Name option }

module Name = 
  let create (s : string) : Name = 
      if isNull s then raise (ArgumentNullException("s"))
      else if s.Length = 0 then raise (ArgumentException("s"))
      else Name s

module Role = 
    let tryCreate (s : string) : Role option = 
        if isNull s then raise (ArgumentNullException("s"))
        else if s.Length = 0 then raise (ArgumentException("s"))
        else if s.Contains("barn") then None 
        else if s.Contains("anonym") then None 
        else Some (Role s)
  
module Contributor = 
    let create (role : Role) (name : Name) (givenName : Name option) (familyName : Name option) : Contributor = 
        { role = role 
          name = name
          givenName = givenName
          familyName = familyName }

let tryCreateContributor (roleStr : string) 
                         (nameStr : string) 
                         (maybeGivenNameStr : string option)
                         (maybeFamilyNameStr : string option) = 
    let createWithRole r = 
       Contributor.create r 
                          (Name.create nameStr)
                          (Option.map Name.create maybeGivenNameStr)
                          (Option.map Name.create maybeFamilyNameStr)
    Role.tryCreate roleStr |> Option.map createWithRole
let maybeContributor1 = 
    tryCreateContributor "Vert" "Vemund" None None 

let maybeContributor2 = 
    tryCreateContributor "Skuespiller" "Sofia Stiansen" (Some "Sofia") (Some "Stiansen")

let maybeContributor3 = 
    tryCreateContributor "Medvirkende barn" "Oda (5)" (Some "Oda") (Some "Olaussen")
      
let maybeContributor4 = 
    tryCreateContributor "anonym" "NN" None None

let printMaybeContributor (mc : Contributor option) = 
  match mc with 
  | Some c -> printfn "%A" c | None -> printfn "--nope--"

maybeContributor1 |> printMaybeContributor
maybeContributor2 |> printMaybeContributor
maybeContributor3 |> printMaybeContributor
maybeContributor4 |> printMaybeContributor

let maybeContributors = 
    [ maybeContributor1; 
      maybeContributor2; 
      maybeContributor3; 
      maybeContributor4 ]
  
let contributors = maybeContributors |> List.choose id 

contributors |> printfn "%A"
