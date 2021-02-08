module Order

type ProductId = ProductId of int

type Quantity =
    | Weight of int
    | Units of int

type UncheckedOrderLine = { pid: ProductId; quantity: Quantity }

type CheckedOrderLine =
    { pid: ProductId
      name: string
      quantity: Quantity }

type UncheckedAddress = UncheckedAddress of string
type UncheckedOrder = UncheckedOrderLine list
type CheckedOrder = CheckedOrderLine list
type ValidationError = ValidationErrorMessage of string

type StockError =
    | NotEnoughInStockError of string
    | UnitMismatch of string

type OrderLineError =
    | ValidationError of ValidationError
    | StockError of StockError

//type Order =
//    | Checked of CheckedOrder
//    | Unchecked of UncheckedOrder
let catalog (pid: ProductId): Async<string option> =
    let products =
        [ (ProductId 173, "halmbukk")
          (ProductId 7348, "rødkål")
          (ProductId 3748, "juletre")
          (ProductId 14, "stjerne")
          (ProductId 757, "grøt")
          (ProductId 99834, "pepperkaker") ]

    async.Return (
            products
            |> List.tryPick (fun (i, name) -> if i = pid then Some name else None))

let stock pid: Quantity =
    let stocks =
        [ (ProductId 173, Units 10)
          (ProductId 7348, Weight 5)
          (ProductId 3748, Units 0)
          (ProductId 14, Units 8)
          (ProductId 757, Weight 0)
          (ProductId 99834, Weight 0) ]

    stocks |> List.find (fun (i, _) -> i = pid) |> snd

let isEnoughInStock (line: CheckedOrderLine): Result<Unit, StockError> =
    let qtyInStock = stock line.pid

    match line.quantity, qtyInStock with
    | Weight wOrder, Weight wStock ->
        if wOrder <= wStock then
            Ok()
        else
            Error <| NotEnoughInStockError "Har ikke nok"
    | Units uOrder, Units uStock ->
        if uOrder <= uStock then
            Ok()
        else
            Error
            <| NotEnoughInStockError "Har ikke mange nok"
    | _ -> Error <| UnitMismatch "Blandet vekt og antall"

let passthru (deadEndFunc: ('a -> Result<Unit, 'err>)) (el: 'a): Result<'a, 'err> =
    match deadEndFunc el with
    | Ok _ -> Ok el
    | Error m -> Error m

module Async =
    let map f computation =
        async.Bind(computation, f >> async.Return)

let checkOrderLine
    (catalog: ProductId -> Async<string option>)
    ({ pid = pid; quantity = quantity }: UncheckedOrderLine)
    : Async<Result<CheckedOrderLine, ValidationError>> =
    let computation = catalog pid

    let f pid quantity name =
        match name with
        | None ->
            Error
            <| ValidationErrorMessage(sprintf "Fant ikke produktet med id %A" pid)
        | Some name ->
            Ok
                { pid = pid
                  name = name
                  quantity = quantity }

    Async.map (f pid quantity) computation

let listOfResultToResultOfList (listOfResult: Result<'a, 'e> list): Result<'a list, 'e> =
    let initState: Result<'a list, 'e> = Ok []

    let folder (result: Result<'a, 'e>) (currentState: Result<'a list, 'e>) =
        match currentState with
        | Error e -> Error e
        | Ok results ->
            match result with
            | Error e -> Error e
            | Ok thing -> Ok(thing :: results)

    List.foldBack folder listOfResult initState

let validateOrderLines (catalog: ProductId -> Async<string option>) order =
    let orderCheck line =
        checkOrderLine catalog line
        |> Async.map (Result.mapError ValidationError)

    let stockCheck line =
        passthru isEnoughInStock line
        |> Result.mapError StockError

    order
    |> List.map (orderCheck >> (Async.map (Result.bind stockCheck)))

// Keep all ok lines.
let getAllOkOrderLines
    (catalog: ProductId -> Async<string option>)
    (order: UncheckedOrder)
    : Async<Result<CheckedOrder, OrderLineError>> =
    let result: Async<Result<CheckedOrderLine, OrderLineError>> list = validateOrderLines catalog order

    // [Async<Result<CheckedOrderLine, OrderLineError>>;Async<Result<CheckedOrderLine, OrderLineError>>;Async<Result<CheckedOrderLine, OrderLineError>>]

    let okLines =
        result
        |> Async.Parallel
        |> Async.map Array.toList 
        |> Async.map (List.filter
            (fun line ->
                match line with
                | Ok _ -> true
                | Error _ -> false))

    let temp linesOk =
        if List.isEmpty linesOk then
            ValidationErrorMessage "Ingen gyldige linjer"
            |> ValidationError
            |> Error
        else
            listOfResultToResultOfList linesOk
    
    okLines |> Async.map temp

// All lines must be ok.
let checkAllOrderLinesOk
    (catalog: ProductId -> Async<string option>)
    (order: UncheckedOrder)
    : Async<Result<CheckedOrder, OrderLineError>> =
    order
    |> validateOrderLines catalog
    |> Async.Parallel
    |> Async.map Array.toList
    |> Async.map listOfResultToResultOfList
