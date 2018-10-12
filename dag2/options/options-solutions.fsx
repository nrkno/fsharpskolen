// Tenk at du trenger å konvertere int til et ikke-negativt tall
// En måte å gjøre det på (særlig før vi har lært så mye om andre typer) er å lage en funksjon som returnerer en option, 
// med None hvis tallet du sender inn er negativt 
let nonNegative x = 
    if x >= 0 then Some x else None

// Hvordan skal vi addere sammen to nonNegative tall?
// Lag en funksjon som bruker pattern matching
let add x y = 
    match x, y with
    | Some a, Some b -> Some (a + b)
    | _ -> None

// Option har også en egen høyereordens funksjon, Option.map2, som kan gjøre matchingen for oss
// Lag add2 med denne funksjonen istedet
let add2 x y = 
    Option.map2 (+) x y

// Test at funksjonene virker som forventet
printfn "add 2 og 3: %A" (add (nonNegative 2) (nonNegative 3))
printfn "add None og 4: %A" (add (nonNegative -2) (nonNegative 4))
printfn "add 2 og 3: %A" (add2 (nonNegative 2) (nonNegative 3))
printfn "add None og 4: %A" (add2 (nonNegative -2) (nonNegative 4))

// La oss leke litt med strenger også
// Lag ferdig funksjonen stringLength som tar inn en string option og returnerer en int option   
let stringLength (str : string option) = 
   Option.map String.length str
    
// Det går også an å hente ut intverdien direkte med feks Option.fold
let stringLengthValue  (str : string option) = 
    str
    |>Option.map String.length
    |> Option.defaultValue 0
     //Option.fold (fun _ (s : string) -> s.Length) 0 str
     

printfn "string length option: %A" (stringLength (Some "Hei"))
printfn "String length int: %d" (stringLengthValue (Some "Hei"))


// Hva om vi har to string options og vil sette de sammen til en streng?
// Hva er det fornuftig å returnere om den ene optionen er None?
let concat (str1 : string option) (str2 : string option) = 
    match str1, str2 with 
    | Some s, Some t -> Some (s + t)
    | _ -> None

let concat2 (str1 : string option) (str2 : string option) = 
    Option.map2 (+) str1 str2        
    
// En fin ting med string.Length er at den er en ikke-negativ int, så la oss kombinere funksjoner vi har laget til 
// å finne lengden av konkatering av to strenger, og her kan man gå to veier, 
// enten finne lengden av hver av strengene og så legge sammen
// eller konkatenere strengene og så finne lengden
// de to veiene bør gi samme resultat
// Funksjonene kan implementeres med bare å bruke stringLenght, add og concat    
let addTwoLengths  (str1 : string option) (str2 : string option) = 
    add (stringLength str1) (stringLength str2) 

let lenghtOfConcat (str1 : string option) (str2 : string option) = 
    stringLength (concat str1 str2)