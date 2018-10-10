open System

type Name = Name of string 
type Role = AnonymousRole | ChildRole | Role of string 

type Contributor = 
    { role : Role
      name : Name 
      givenName : Name option 
      familyName : Name option }

let createValidName (s : string) : Name = 
    if isNull s then raise (ArgumentNullException("s"))
    else if s.Length = 0 then raise (ArgumentException("s"))
    else Name s

let createValidRole (s : string) : Role = 
    if isNull s then raise (ArgumentNullException("s"))
    else if s.Length = 0 then raise (ArgumentException("s"))
    else if s.Contains("barn") then ChildRole 
    else if s.Contains("anonym") then AnonymousRole 
    else Role s
    
let contributor1 = 
    { role = createValidRole "Vert" 
      name = createValidName "Vemund" 
      givenName = None 
      familyName = None }

let contributor2 = 
    { role = createValidRole "Skuespiller" 
      name = createValidName "Sofia Stiansen" 
      givenName = Some <| createValidName "Sofia"
      familyName = Some <| createValidName "Stiansen" }

let contributor3 = 
    { role = createValidRole "Medvirkende barn" 
      name = createValidName "Oda (5)" 
      givenName = Some <| createValidName "Oda"
      familyName = Some <| createValidName "Olaussen" }
      
let contributor4 = 
    { role = createValidRole "anonym" 
      name = createValidName "NN" 
      givenName = None
      familyName = None }
      
let blockContributor (c : Contributor) = 
    match c.role with 
    | AnonymousRole 
    | ChildRole -> true
    | Role _ -> false

contributor1 |> printfn "%A"
contributor2 |> printfn "%A"
contributor3 |> printfn "%A"
contributor4 |> printfn "%A"

let contributors = [ contributor1; contributor2; contributor3; contributor4 ]

let validContributors = contributors |> List.filter (blockContributor >> not)

validContributors |> printfn "%A"
