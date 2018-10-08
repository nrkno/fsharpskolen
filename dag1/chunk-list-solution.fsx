let chunk (n : int) (things : 'a list) : 'a list list = 
    let help acc it = 
        match acc with 
        | [] -> [ [ it ] ]
        | h :: t -> 
            if n > List.length h then 
                (it :: h) :: t 
            else
                [ it ] :: h :: t
    List.fold help [] things
    |> List.rev
    |> List.map List.rev

chunk 3 [1 .. 9] |> printfn "%A"
chunk 3 [1 .. 10] |> printfn "%A"
chunk 2 ['a' .. 'e'] |> printfn "%A"
chunk 0 [1 .. 10] |> printfn "%A"
chunk 2 [1] |> printfn "%A"
chunk 1 [] |> printfn "%A"
chunk 1 [[[[]]]] |> printfn "%A"
