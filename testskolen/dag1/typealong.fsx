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
type Direction = North | East | South | West


type Card = Visa | Master
Visa
Master

type PhoneNumber = PhoneNumber of int
PhoneNumber 98765432

type PaymentMethod =
    | Cash
    | Vipps of PhoneNumber
    | Card of Card


Cash
Vipps (PhoneNumber 98765432)
Card Visa

// Records

type PaymentAmount = PaymentAmount of decimal
type Currency = NOK | EUR
type Payment = {
    Amount: PaymentAmount
    Currency: Currency
    Method: PaymentMethod
}





