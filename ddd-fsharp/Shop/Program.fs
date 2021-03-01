open System
open Order
open Deserializer

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
        uncheckedOrderLines 
        |> getAllOkOrderLines catalog
        |> Async.RunSynchronously

    match result1 with
    | Ok validOrder -> printfn "Dette gikk fint. Du har %d godkjente elementer" (List.length validOrder)
    | Error err ->
        match err with
        | ValidationError (ValidationErrorMessage msg) -> printfn "Valideringen gikk dårlig %s" msg
        | StockError stockErr ->
            match stockErr with
            | NotEnoughInStockError msg -> printfn "Hadde ikke nok %s" msg
            | UnitMismatch msg -> printfn "Helt på trynet %s" msg

    let result2 =
        uncheckedOrderLines
        |> checkAllOrderLinesOk catalog
        |> Async.RunSynchronously

    match result2 with
    | Ok validOrder -> printfn "Dette gikk fint. Du har %d godkjente elementer" (List.length validOrder)
    | Error err ->
        match err with
        | ValidationError (ValidationErrorMessage msg) -> printfn "Valideringen gikk dårlig %s" msg
        | StockError stockErr ->
            match stockErr with
            | NotEnoughInStockError msg -> printfn "Hadde ikke nok %s" msg
            | UnitMismatch msg -> printfn "Helt på trynet %s" msg

   // let foo = """ {"lines" : [ { "pid": "100", "quantity": { "unit": "number", amount: 10}}], "address": { "name" : "bjartwolf", "addressLine1": "skogen 123", "addressLine2": null, "postalCode": 1239}} """ 
    let foo = """ {"lines" : [ { "pid": "173", "quantity": { "unit": "number", amount: 5}}], "address": { "name" : "bjartwolf", "addressLine1": "skogen 123", "addressLine2": null, "postalCode": 1239}} """ 

    let result3 =
        foo
        |> deserialize<OrderDto.OrderDto>
        |> Result.bind OrderDto.toDomain 
        |> Result.map (getAllOkOrderLines catalog)
        |> Result.map Async.RunSynchronously
        |> Result.bind id

    match result3 with
    | Ok validOrder -> printfn "Dette gikk fint. Du har %d godkjente elementer" (List.length validOrder)
    | Error err ->
        match err with
        | ValidationError (ValidationErrorMessage msg) -> printfn "Valideringen gikk dårlig %s" msg
        | JsonParseException jpex -> printfn "Helt på trynet input %A" jpex 
        | StockError stockErr ->
            match stockErr with
            | NotEnoughInStockError msg -> printfn "Hadde ikke nok %s" msg
            | UnitMismatch msg -> printfn "Helt på trynet %s" msg


    0 // return an integer exit code
