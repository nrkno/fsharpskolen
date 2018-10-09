open System

let rec zipStrict xs ys = 
    match (xs, ys) with 
    | ([], []) -> [] 
    | (x :: xs', y :: ys') -> (x, y) :: zipStrict xs' ys' 
    | _ -> raise (ArgumentException("The lists had different lengths."))
    
let rec zipLenient (xs : 'a list) (ys : 'b list) : ('a * 'b) list = 
    match (xs, ys) with 
    | ([], _)
    | (_, []) -> []
    | (x :: xs', y :: ys') -> (x, y) :: zipLenient xs' ys' 

let rec tryZip (xs : 'a list) (ys : 'b list) : ('a * 'b) list option = 
    match (xs, ys) with 
    | ([], []) -> Some [] 
    | (x :: xs', y :: ys') -> tryZip xs' ys' |> Option.map (fun lst -> (x, y) :: lst)
    | _ -> None

try 
    List.zip ['a' .. 'f'] [1..10] |> printfn "%A"
with
    | ex -> ex |> printfn "%A"

try 
    zipStrict ['a' .. 'f'] [1..10] |> printfn "%A"// Eks
with
    | ex -> ex |> printfn "%A"

zipLenient ['a' .. 'f'] [1..10] |> printfn "%A"

tryZip ['a' .. 'f'] [1..6] |> printfn "%A"
tryZip ['a' .. 'f'] [1..5] |> printfn "%A"
