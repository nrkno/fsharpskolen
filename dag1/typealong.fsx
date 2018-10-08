// Simple values 

1 

"hello"

true

1 + 2

5 / 2

let x = 3 + 4

// Functions 

let add x y = x + y

let add' (x, y) = x + y

let concat x y : string = x + y

// Operators are 

// Partial application

let inc = add 1 

// Currying (a parenthesis)

let curry' fn = fun a b -> fn (a, b)  

let curry fn a b = fn (a, b)

let uncurry' fn = fun (a, b) -> fn a b 

let uncurry fn (a, b) = fn a b 

// Lists 

[] // parametric polymorphism

[1]

1 :: []

[1; 2; 3]

1 :: 2 :: 3 :: []

[1 .. 10]

[1 .. 3 .. 10]


// Pattern matching

// Finne veldig godt eksempel på rekursjon.

let rec squared lst = 
  match lst with 
  | [] -> []
  | h :: t -> h * h :: squared t 

type MyList<'a> = Nil | Successor of ('a * MyList<'a>)
 
let rec pairwisesum lst = 
  match lst with 
  | [] -> []
  | [a] -> [a]
  | a::b::t -> (a + b) :: pairwisesum t 

let everyother lst = 
    let rec help toggle lst = 
        match lst with 
        | [] -> []
        | h::t ->
          let rest = help (not toggle) t 
          if toggle then h :: rest else rest 
    help true lst

// Rekursjon med eller uten hale?

// Higher order functions ('functions as data')

// Sammenligning med LINQ?
// List<int> numbers = new List<int> {1, 2, 3, 4, 5};
// var squared = numbers.Select(it => it * it).ToList();

let rec map fn lst = 
    match lst with 
    | [] -> []
    | x :: t -> (fn x) :: map fn t

let rec filter predicate lst = 
    match lst with  
    | [] -> []
    | x :: t -> if predicate x then x :: filter predicate t else filter predicate t 

let squarelist = List.map (fun x -> x * x)

let hmmlist lst = lst |> List.filter (fun x -> x % 2 = 0) |> squarelist    

// Function composition - build larger functions out of smaller functions. 

let double x = inc (inc x)

let doublecomp = inc >> inc

(+) 5 5

[1 .. 10] |> List.fold (+) 0 


