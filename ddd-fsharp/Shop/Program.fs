open System
open Order

[<EntryPoint>]
let main argv =
    let uncheckedOrderLines =
        [ { pid = ProductId 7348
            quantity = Weight 2 }
          { pid = ProductId 99834
            quantity = Units 11 }
          { pid = ProductId 5555
            quantity = Weight 1 } ]

    let result1 =
        uncheckedOrderLines |> getAllOkOrderLines catalog

    match result1 with
    | Ok validOrder -> printfn "Dette gikk fint. Du har %d godkjente elementer" (List.length validOrder)
    | Error (ErrorMessage msg) -> printfn "Dette skjedde %s" msg

    let result2 =
        uncheckedOrderLines
        |> checkAllOrderLinesOk catalog

    match result2 with
    | Ok validOrder -> printfn "Dette gikk fint. Du har %d godkjente elementer" (List.length validOrder)
    | Error (ErrorMessage msg) -> printfn "Dette skjedde %s" msg

    0 // return an integer exit code
