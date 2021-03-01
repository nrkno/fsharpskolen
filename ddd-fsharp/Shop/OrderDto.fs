module OrderDto

open Order

// Bare bruk ting som har en direkte gjenpart i JSON
// Records er greit => JSON object
// Lister er greit => JSON array
// Tall er greit => JSON number
// Boolean er greit => JSON true og JSON false
// String er greit => JSON string
// DateTime er IKKE greit => JSON string
// option er greit => JSON null

type QuantityDto = {
    unit : string
    amount : double
}

type OrderLineDto = {
    pid: int
    quantity: QuantityDto
}

type AddressDto = {
    name: string
    addressLine1: string
    addressLine2: string option
    postalCode: string
}

type OrderDto = {
    lines: OrderLineDto list
    address: AddressDto
}

let processQuantity (quantityDto : QuantityDto) : Result<Quantity, OrderLineError> =
    match quantityDto.unit with
    | "kg" -> Ok (Weight (int quantityDto.amount)) //TODO: make weight float
    | "number" -> Ok (Units (int quantityDto.amount))
    | _ -> Error (JsonParseException (DtoConvertError "Unsupported unit"))

let processLine (line: OrderLineDto): Result<UncheckedOrderLine, OrderLineError> =
    match processQuantity line.quantity  with
    | Ok q -> Ok { pid = ProductId line.pid; quantity = q}
    | Error e -> Error e

let toDomain (orderDto: OrderDto) : Result<UncheckedOrder, OrderLineError> =
    orderDto.lines
    |> List.map processLine
    |> sequence

