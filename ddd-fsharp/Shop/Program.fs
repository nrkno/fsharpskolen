open System
open Order

[<EntryPoint>]
let main argv =
    let uncheckedOrderLines = 
        [
            { pid = ProductId 7348
              quantity = Weight 2 }
            { pid = ProductId 99834
              quantity = Units 11 }
            { pid = ProductId 5555
              quantity = Weight 1 }
        ]
    let result1 = uncheckedOrderLines |> checkOrder1 catalog
    match result1 with 
    | Some validOrder -> 
        printfn "Dette gikk fint."
    | None -> 
        printfn "Ooops"
    let result2 = uncheckedOrderLines |> checkOrder2 catalog
    match result2 with 
    | Some validOrder -> 
        printfn "Dette gikk fint (2)."
    | None -> 
        printfn "Ooops (2)"
    0 // return an integer exit code