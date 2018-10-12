let rec times (n : int) (fn : 'a -> 'a) : 'a -> 'a = 
    if n <= 0 then id else fn >> times (n - 1) fn

let inc = (+) 1 
let leadingDot = (+) "."

let add5 = times 5 inc  
let leadingEllipsis = leadingDot |> times 3

7 |> add5 |> printfn "%d"
"hello" |> leadingEllipsis |> printfn "%s"

7 |> times 0 inc |> printfn "%d"
"hello" |> times 0 leadingDot |> printfn "%s"

let thrice<'a> : ('a -> 'a) -> ('a -> 'a) = times 3

let fn1 = (thrice >> thrice >> thrice) inc 
let fn2 = inc |> times 3 thrice 
let fn3 = inc |> thrice thrice

7 |> fn1 |> printfn "%d"
7 |> fn2 |> printfn "%d"
7 |> fn3 |> printfn "%d"
