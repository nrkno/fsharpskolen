// https://fsharpforfunandprofit.com/posts/elevated-world-4/#traverse

module Result =
    let pure' (value : 'a) : Result<'a, 'e> = Ok value

    let apply1 (wrappedFn: Result<'a -> 'b, 'e>) (wrappedValue: Result<'a, 'e>) : Result<'b, 'e> =
        match wrappedFn, wrappedValue with
        | Ok fn, Ok value -> pure' (fn value)
        | Error e1, _ -> Error e1
        | _, Error e2 -> Error e2

    let createApply (errorAggregator: 'e -> 'e -> 'e): Result<'a -> 'b, 'e> -> Result<'a, 'e> -> Result<'b, 'e> = 
        fun (wrappedFn: Result<'a -> 'b, 'e>) (wrappedValue: Result<'a, 'e>) ->
            match wrappedFn, wrappedValue with
            | Ok fn, Ok value -> pure' (fn value)
            | Error e1, Error e2 -> Error (errorAggregator e1 e2)
            | Error e1, _ -> Error e1
            | _, Error e2 -> Error e2
    
    //let apply2 = createApply +

    let map (f : 'a -> 'b) (wrappedValue : Result<'a, 'e>) : Result<'b, 'e> =
        apply1 (pure' f) wrappedValue

    let map' (f : 'a -> 'b) : Result<'a, 'e> -> Result<'b, 'e> =
        apply1 (pure' f)

    let rec traverse (worldCrossingFunction: 'c -> Result<'d, 'e>) (lst: 'c list) : Result<'d list, 'e> =
        let cons h t = h :: t
        match lst with
        | [] -> pure' []
        | h :: t -> 
            let headResult: Result<'d, 'e> = worldCrossingFunction h
            let tailResult: Result<'d list, 'e> = traverse worldCrossingFunction t
            apply1 (apply1 (pure' cons) headResult) tailResult

    let appendString s1 s2 = s1 + s2
    let apply2 (wrappedFn: Result<'a -> 'b, 'e>) (wrappedValue: Result<'a, 'e>) : Result<'b, 'e> = createApply appendString 
    let rec traverse2 (worldCrossingFunction: 'c -> Result<'d, 'e>) (lst: 'c list) : Result<'d list, 'e> =
        let cons h t = h :: t
        match lst with
        | [] -> pure' []
        | h :: t -> 
            let headResult: Result<'d, 'e> = worldCrossingFunction h
            let tailResult: Result<'d list, 'e> = traverse2 worldCrossingFunction t
            apply2 (apply2 (pure' cons) headResult) tailResult
             // match (headResult, tailResult) with
            // | Ok newHead, Ok newTail ->
            //     pure' (cons newHead newTail)
            // | Error e1, _ -> Error e1
            // | _, Error e2 -> Error e2
    

let howDoYouFeel person : Result<bool,string> =
    match person with
    | 1 -> Error "hva sa du"
    | 2 -> Ok true
    | 3 -> Ok true
    | 4 -> Ok false
    | 5 -> Error "muted"

 

[1;2;3;4;5] |> Result.traverse howDoYouFeel |> printfn "%A"
[2;3;4;5] |> Result.traverse howDoYouFeel |> printfn "%A"
[2;3;4] |> Result.traverse howDoYouFeel |> printfn "%A"

[1;2;3;4;5] |> Result.traverse2 howDoYouFeel |> printfn "%A"
[2;3;4;5] |> Result.traverse2 howDoYouFeel |> printfn "%A"
[2;3;4] |> Result.traverse2 howDoYouFeel |> printfn "%A"


Result.pure' 5 |> printfn "%A"
Result.apply1 (Result.pure' id) (Ok 5) = Ok 5 |> printfn "%A"
Result.apply1 (Result.pure' (fun x -> x + 5)) (Ok 5) |> printfn "%A"


module Option = 
    let pure (value : 'a) : 'a option = Some value 
    
    let apply (wrappedFn : ('a -> 'b) option) (wrappedValue : 'a option) : 'b option = 
        match wrappedFn, wrappedValue with 
        | Some fn, Some value -> pure (fn value)
        | _ -> None 

    // Eksempel: worldCrossingFn x = match x % 2 with | 0 -> Some true | _ -> None
    //           lst = [2;3;4;5]
    //           'c = int
    //           'd = bool
    let rec traverseA (worldCrossingFn : 'c -> 'd option) (lst : 'c list) : 'd list option = 
        let cons (head: 'd) (tail: 'd list) : 'd list = head :: tail // Eksempel for bool: cons true [false; true; true] -> [true; false; true; true]
        match lst with 
        | [] -> pure [] // Some []
        | (head: 'c) :: (tail: 'c list) -> // [2;3;4;5]
            let maybeHead : 'd option = worldCrossingFn head // worldCrossingFn 2 -> Some true
            let maybeTail : 'd list option = traverseA worldCrossingFn tail // worldCrossingFn 3 -> None, worldCrossingFn 4 -> Some true, worldCrossingFn 5 -> None, traverseA worldCrossingFn tail = [None, Some true, None]
            let maybeHeadAttacher : ('d list -> 'd list) option = apply (pure cons) maybeHead // pure cons -> Some cons -> Some ('d -> 'd list -> 'd list), i kontekst av apply er 'a = 'd, og 'b = 'd list -> 'd list
                                                                                              // apply (pure cons) = 'd option -> ('d list -> 'd list) option
                                                                                              // apply (pure cons) maybeHead = ('d list -> 'd list) option, med maybeHead bakt inn som elementet som legges til
                                                                                              // maybeHeadAttacher = apply (pure cons) maybeHead = ('d list -> 'd list) option, med maybeHead bakt inn som elementet som legges til
            apply maybeHeadAttacher maybeTail // maybeHeadAttacher = ('d list -> 'd list) option, med maybeHead bakt inn som elementet som legges til
                                              // i kontekst av apply er 'a = 'd list, 'b = 'd list og wrappedFn = ('d list -> 'd list) option
                                              // Dvs. at (apply maybeHeadAttacher) = 'd list option -> 'd list option
                                              // Videre er (apply maybeHeadAttacher maybeTail) å kalle (apply maybeAttacher) med maybeTail ('d list option) som argument
                                              // Det fører til at vi baker inn den indre verdien fra maybeHead ('d) i den indre listen til maybeTail ('d list) og hever det resultatet i en option slik at vi får en ('d list option)