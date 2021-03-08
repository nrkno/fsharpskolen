open System
open Order
open OrderDto
open JSON

[<EntryPoint>]
let main argv =
    let uncheckedOrderJsonString = """
    {
            "lines": [
                {
                    "pid": 7348, 
                    "quantity": { 
                        "unit": "kg", 
                        "amount": 2.0 
                    }
                },
                {
                    "pid": 14, 
                    "quantity": { 
                        "unit": "number", 
                        "amount": 6.0
                    }
                },
                {
                    "pid": 5555, 
                    "quantity": { 
                        "unit": "kg",
                        "amount": 1.0
                    }
                }
            ],
            "address": {
                "name": "Ola Nordmann",
                "addressLine1": "Karl Johans gate 1",
                "addressLine2": null,
                "postalCode": "1234"
            }
    }
    """

    let uncheckedOrderDto = deserialize<OrderDto> uncheckedOrderJsonString
    let uncheckedOrder = toDomain uncheckedOrderDto

    uncheckedOrder
    |> Result.map (toUncheckedOrderDto >> serialize)
    |> printfn "%A"

    let result1 =
        let getOkOrder order = 
            order
            |> getAllOkOrderLines catalog
            |> Async.RunSynchronously

        uncheckedOrder
        |> Result.mapError (ValidationErrorMessage >> ValidationError)
        |> Result.bind getOkOrder

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
        let checkOrder order = 
            order 
            |> checkAllOrderLinesOk catalog 
            |> Async.RunSynchronously

        uncheckedOrder
        |> Result.mapError (ValidationErrorMessage >> ValidationError)
        |> Result.bind checkOrder

    match result2 with
    | Ok validOrder -> printfn "Dette gikk fint. Du har %d godkjente elementer" (List.length validOrder)
    | Error err ->
        match err with
        | ValidationError (ValidationErrorMessage msg) -> printfn "Valideringen gikk dårlig %s" msg
        | StockError stockErr ->
            match stockErr with
            | NotEnoughInStockError msg -> printfn "Hadde ikke nok %s" msg
            | UnitMismatch msg -> printfn "Helt på trynet %s" msg


    0 // return an integer exit code
