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
type ValidationError = ErrorMessage of string

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

let checkOrderLine
    (catalog: ProductId -> string option)
    ({ pid = pid; quantity = quantity }: UncheckedOrderLine)
    : Result<CheckedOrderLine, ValidationError> =
    match catalog pid with
    | None ->
        Error
        <| ErrorMessage(sprintf "Fant ikke produktet med id %A" pid)
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
let checkOrder1 (catalog: ProductId -> string option) (order: UncheckedOrder): Result<CheckedOrder, ValidationError> =
    let result: CheckedOrderLine list =
        order
        |> List.choose
            (fun line ->
                match checkOrderLine catalog line with
                | Ok checkedOrderLine -> Some checkedOrderLine
                | Error _ -> None)

    if List.isEmpty result then
        Error <| ErrorMessage "Dette rota du til"
    else
        Ok result

// All lines must be ok.
let checkOrder2 (catalog: ProductId -> string option) (order: UncheckedOrder): Result<CheckedOrder, ValidationError> =
    order
    |> List.map (checkOrderLine catalog)
    |> listOfResultToResultOfList
