// Simple values 

1 

"hello"

'a'

true

true && false
not false


1 + 2

5 / 2

0.5

let x = 3 + 4

let y = 2

x + y

if x = y then "equals"
else if x < y then "is less than"
else "is greater than"

// Functions

let add x y = x + y

let concat x y : string = x + y


// Lists 

[] // parametric polymorphism

[1]

1 :: []

[1; 2; 3]

1 :: 2 :: 3 :: []

[1; 2] @ [3; 4]

[1 .. 10]

[1 .. 3 .. 10]

[ for i in [1; 2; 3] do
    for j in [4; 5; 6] do
      yield i + j ]

[1; 2; 3; 4]
|> List.map (fun n -> n * n)
|> List.filter (fun n -> n > 3)


// Optional - no null!

Some "world"
None

let greet name =
    match name with
    | Some str -> printfn "Hello %s!" str
    | None -> printfn "Hello!"

greet (Some "world")
greet None

// Discriminated union
type Mat = Fisk | Brød | Banan

type Matvare = 
    | Fisk of int
    | Brød of int
    
 
 let pris vare =
    match vare with
    | Fisk f -> 70 * f
    | Brød b -> 10 * b
    

type Antall = Antall of int
type Kilo = Kilo of int

type Matvare' =
    | Fisk of Kilo
    | Brød of Antall

let matvarer = [ Fisk (Kilo 7) 
                 Brød (Antall 2) 
                 Brød (Antall 3) 
                 Fisk (Kilo 3) 
                 Brød (Antall 1)
                 Fisk (Kilo 5) 
                 Fisk (Kilo 2) 
                 Brød (Antall 2) 
                 Brød (Antall 1) 
                 Brød (Antall 5) ]

let antallBrød = matvarer |> List.map (fun x -> match x with | Fisk _ -> 0 | Brød (Antall n) -> n)
                          |> List.sum

let antallBrød' = matvarer |> List.choose (fun x -> match x with | Fisk _ -> None | Brød (Antall n) -> Some n)
                           |> List.sum
              
