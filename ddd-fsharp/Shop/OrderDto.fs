module OrderDto

open Order
open System

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
    lines: OrderLineDto array
    address: AddressDto
}

let processQuantity (quantityDto : QuantityDto) : Result<Quantity, string> =
    match quantityDto.unit with
    | "kg" -> Ok (Weight (int quantityDto.amount)) //TODO: make weight float
    | "number" -> Ok (Units (int quantityDto.amount))
    | _ -> Error "Unsupported unit"

let processLine (line: OrderLineDto): Result<UncheckedOrderLine, string> =
    match processQuantity line.quantity  with
    | Ok q -> Ok { pid = ProductId line.pid; quantity = q}
    | Error e -> Error e

let toDomain (orderDto: OrderDto) : Result<UncheckedOrder, string> =
    let orderLines =
        orderDto.lines
        |> Array.map processLine
        |> List.ofArray
        |> sequence

    let addrDto = orderDto.address
    let address: UncheckedAddress = {
        name = addrDto.name
        addressLine1 = addrDto.addressLine1
        addressLine2 = addrDto.addressLine2
        postalCode = addrDto.postalCode
    }

    orderLines 
    |> Result.bind (fun orderLines -> Ok {
        lines = orderLines
        address = address
    }) 

let toQuantityDto (quantity : Quantity) : QuantityDto =
    match quantity with
    | Weight amount ->
        {
            unit = "kg"
            amount = double amount
        }
    | Units amount ->
        {
            unit = "number"
            amount = double amount
        }

let toOrderLineDto (line:UncheckedOrderLine ): OrderLineDto =
    {
        pid = ProductId.value line.pid
        quantity = toQuantityDto line.quantity
    }

let toUncheckedOrderDto (order : UncheckedOrder) : OrderDto = 
    {
        lines =
            order.lines
            |> List.map toOrderLineDto
            |> Array.ofList
        address = {
            name = order.address.name
            addressLine1 = order.address.addressLine1
            addressLine2 = order.address.addressLine2
            postalCode = order.address.postalCode
        }
    }
