open System


type Contributor = 
    { role : string
      name : string 
      givenName : string option 
      familyName : string option }

let ensureValidName (s : string) : string = 
    if isNull s then raise (ArgumentNullException("s"))
    else if s.Length = 0 then raise (ArgumentException("s"))
    else s

let ensureValidRole (s : string) = 
    if isNull s then raise (ArgumentNullException("s"))
    else if s.Length = 0 then raise (ArgumentException("s"))
    else s

let createContributor role name givenName familyName = 
  ()

let contributor1 = 
    { role = ensureValidRole "Vert" 
      name = ensureValidName "Vemund" 
      givenName = None 
      familyName = None }

let contributor2 = 
    { role = ensureValidRole "Skuespiller" 
      name = ensureValidName "Sofia Stiansen" 
      givenName = Some <| ensureValidName "Sofia"
      familyName = Some <| ensureValidName "Stiansen" }

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
    | Role roleStr -> 
      roleStr.Contains("barn") || roleStr.Contains("anonym")

contributor1 |> printfn "%A"
contributor2 |> printfn "%A"
contributor3 |> printfn "%A"
contributor4 |> printfn "%A"

let contributors = [ contributor1; contributor2; contributor3; contributor4 ]

let validContributors = contributors |> List.filter (blockContributor >> not)

validContributors |> printfn "%A"
