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
type ValidationError = ValidationError of string

type StockError =
    | NotEnoughInStockError of string
    | UnitMismatch of string

// type ErrorTypes =
//     | ValidationError
//     | NotEnoughInStockError

//type Order =
//    | Checked of CheckedOrder
//    | Unchecked of UncheckedOrder
let catalog (pid: ProductId): string option =
    let products =
        [ (ProductId 173, "halmbukk")
          (ProductId 7348, "rødkål")
          (ProductId 3748, "juletre")
          (ProductId 14, "stjerne")
          (ProductId 757, "grøt")
          (ProductId 99834, "pepperkaker") ]

    products
    |> List.tryPick (fun (i, name) -> if i = pid then Some name else None)

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


let checkOrderLine
    (catalog: ProductId -> string option)
    ({ pid = pid; quantity = quantity }: UncheckedOrderLine)
    : Result<CheckedOrderLine, ValidationError> =
    match catalog pid with
    | None ->
        Error
        <| ValidationError(sprintf "Fant ikke produktet med id %A" pid)
    | Some name ->
        Ok
            { pid = pid
              name = name
              quantity = quantity }

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

// Keep all ok lines.
let getAllOkOrderLines
    (catalog: ProductId -> string option)
    (order: UncheckedOrder)
    : Result<CheckedOrder, ValidationError> =
    let result: CheckedOrderLine list =
        order
        |> List.choose
            (fun line ->
                match checkOrderLine catalog line with
                | Ok checkedOrderLine ->
                    match isEnoughInStock checkedOrderLine with
                    | Ok _ -> Some checkedOrderLine
                    | Error _ -> None
                | Error _ -> None)

    if List.isEmpty result then
        Error <| ValidationError "Dette rota du til"
    else
        Ok result

// All lines must be ok.
let checkAllOrderLinesOk
    (catalog: ProductId -> string option)
    (order: UncheckedOrder)
    : Result<CheckedOrder, ValidationError> =
    order
    |> List.map (checkOrderLine catalog)
    |> listOfResultToResultOfList
