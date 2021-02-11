// koden til einar
module Option = 
    let pure (value : 'a) : 'a option = Some value 
    let apply (wrappedFn : ('a -> 'b) option) (wrappedValue : 'a option) : 'b option = 
        match wrappedFn, wrappedValue with 
        | Some fn, Some value -> pure (fn value)
        | _ -> None    
    let rec traverseA (worldCrossingFn : 'a -> 'b option) (lst : 'a list) : 'b list option = 
        let cons head tail = head :: tail
        match lst with 
        | [] -> pure []
        | head :: tail -> 
            let maybeHead : 'b option = worldCrossingFn head
            let maybeTail : 'b list option = traverseA worldCrossingFn tail 
            let maybeHeadAttacher : ('b list -> 'b list) option = apply (pure cons) maybeHead
            apply maybeHeadAttacher maybeTail
    let rec traverseM (worldCrossingFn : 'a -> 'b option) (lst : 'a list) : 'b list option = 
        match lst with 
        | [] -> pure []
        | head :: tail -> 
            worldCrossingFn head 
            |> Option.bind (fun h -> 
                traverseM worldCrossingFn tail
                |> Option.bind (fun t -> pure (h :: t)))
let inc x = x + 1 
let add x y = x + y 
let inc' = add 1 
let concat x y : string = x + y 
let cons head tail = head :: tail
let maybeV1 = Some 10
let maybeV2 = Some 20 
let maybeV3 = None
match maybeV1, maybeV2 with 
| Some v1, Some v2 -> Some (v1 + v2)
| _ -> None 
let maybeNumber = Option.pure 10
inc |> printfn "%A"
let maybeInc = Option.pure inc 
maybeInc |> printfn "%A"
Option.apply (Option.pure inc) maybeV1 |> printfn "%A"
Option.apply (Option.pure inc) maybeV3 |> printfn "%A"
Option.apply (Option.pure add) maybeV1 |> printfn "%A"
Option.apply (Option.apply (Option.pure add) maybeV1) maybeV3 |> printfn "%A"
Option.apply (Option.pure concat) (Some "foo") |> printfn "%A"
Option.apply (Option.apply (Option.pure concat) (Some "foo")) (Some "bar") |> printfn "%A"
Option.apply (Option.apply (Option.pure cons) (Some 0)) (Some [1;2;3;4]) |> printfn "%A"
let fn x = if x % 2 = 0 then Some x else None
[1;2;3;4] |> Option.traverseA fn |> printfn "%A"
[2;4;6;8] |> Option.traverseA fn |> printfn "%A"