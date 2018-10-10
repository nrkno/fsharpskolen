open System

type Name = Name of string 
type ValidRole = ValidRole of string 
type Role = AnonymousRole | ChildRole | OtherRole of ValidRole 

type ValidContributor = 
    { role : ValidRole
      name : Name 
      givenName : Name option 
      familyName : Name option }
      
type Contributor = 
  | Anonymous
  | Child
  | Contributor of ValidContributor 

let createValidName (s : string) : Name = 
    if isNull s then raise (ArgumentNullException("s"))
    else if s.Length = 0 then raise (ArgumentException("s"))
    else Name s

let createValidNameOption (s : string) : Name option = 
    if isNull s then None else Some <| createValidName s

let createValidRole (s : string) : Role = 
    if isNull s then raise (ArgumentNullException("s"))
    else if s.Length = 0 then raise (ArgumentException("s"))
    else if s.Contains("barn") then ChildRole 
    else if s.Contains("anonym") then AnonymousRole 
    else OtherRole (ValidRole s)
    
let createContributor (roleStr : string) (nameStr : string) (givenNameStr : string option) (familyNameStr : string option) : Contributor = 
    match createValidRole roleStr with 
    | AnonymousRole -> Anonymous 
    | ChildRole -> Child 
    | OtherRole r -> 
      let info = { role = r 
                   name = createValidName nameStr
                   givenName = Option.map createValidName givenNameStr
                   familyName = Option.map createValidName familyNameStr }
      Contributor info 
      
let contributor1 = createContributor "Vert" "Vemund" None None 

let contributor2 = createContributor "Skuespiller" "Sofia Stiansen" (Some "Sofia") (Some "Stiansen")

let contributor3 = createContributor "Medvirkende barn" "Oda (5)" (Some "Oda") (Some "Olaussen")
      
let contributor4 = createContributor "anonym" "NN" None None

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
