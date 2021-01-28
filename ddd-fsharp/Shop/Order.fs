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

type Error =
    | ValidationError of ValidationError
    | StockError of StockError

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

let wrapper func el =
    match func el with
    | Ok _ -> Ok el
    | Error m -> Error m 

let checkOrderLine
    (catalog: ProductId -> string option)
    ({ pid = pid; quantity = quantity }: UncheckedOrderLine)
    : Result<CheckedOrderLine, ValidationError> =
    match catalog pid with
    | None ->
        Error
        <| ValidationErrorMessage (sprintf "Fant ikke produktet med id %A" pid)
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
    : Result<CheckedOrder, Error> =
    let passthru (deadend : 'a -> Result<unit, 'e>) (element : 'a): Result<'a, 'e> =
        match deadend element with 
        | Ok () -> Ok element 
        | Error e -> Error e
    let checkOrderLine' : UncheckedOrderLine -> Result<CheckedOrderLine, Error> =
        fun uncheckedOrderLine -> checkOrderLine catalog uncheckedOrderLine |> Result.mapError ValidationError
    let isEnoughInStock' : CheckedOrderLine -> Result<CheckedOrderLine, Error> = 
        fun checkedOrderLine -> passthru isEnoughInStock checkedOrderLine |> Result.mapError StockError
    let combinedValidation line =
        checkOrderLine' line |> Result.bind isEnoughInStock'
    let results : Result<CheckedOrderLine, Error> list =
        order |> List.map combinedValidation
    let okLines =
        results
        |> List.choose (fun r -> match r with | Ok line -> Some line | Error _ -> None)
    if List.isEmpty okLines then
        Error (ValidationError (ValidationErrorMessage "Dette rota du til"))
    else
        Ok okLines




// All lines must be ok.
let checkAllOrderLinesOk
    (catalog: ProductId -> string option)
    (order: UncheckedOrder)
    : Result<CheckedOrder, Error> =

    order
    |> List.map (checkOrderLine catalog)
    |> listOfResultToResultOfList
    |> Result.mapError (ValidationError)
