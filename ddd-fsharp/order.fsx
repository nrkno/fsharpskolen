type ProductId = ProductId of int
type Quantity = | Weight of int | Units of int
type UncheckedOrderLine = {
    pid : ProductId 
    quantity : Quantity 
}
type CheckedOrderLine = {
    pid : ProductId 
    name : string
    quantity : Quantity 
}
type UncheckedOrder = UncheckedOrderLine list
type CheckedOrder = CheckedOrderLine list 
//type Order = 
//    | Checked of CheckedOrder 
//    | Unchecked of UncheckedOrder 
let catalog (pid : ProductId) : string option =
    let products = 
        [ (ProductId 173, "halmbukk")
          (ProductId 7348, "rødkål")
          (ProductId 3748, "juletre")
          (ProductId 14, "stjerne")
          (ProductId 757, "grøt")
          (ProductId 99834, "pepperkaker") ]
    products 
    |> List.tryPick (fun (i, name) -> if i = pid then Some name else None)
let validateOrderLine (catalog : ProductId -> string option) ({ pid = pid; quantity = quantity } : UncheckedOrderLine) 
    : CheckedOrderLine option = 
    match catalog pid with 
    | None -> None
    | Some name -> Some { pid = pid; name = name; quantity = quantity }
// Keep all ok lines.
let validateOrder1 (catalog : ProductId -> string option) (order : UncheckedOrder) 
    : CheckedOrder option = 
    let result : CheckedOrderLine list = 
        order 
        |> List.choose (validateOrderLine catalog)
    if List.isEmpty result then None else Some result
// All lines must be ok.
let validateOrder2 (catalog : ProductId -> string option) (order : UncheckedOrder) = 
    
    let result : CheckedOrderLine list = 
        order 
        |> List.choose (validateOrderLine catalog)

    if List.isEmpty result then 
        None    
    elif List.length result <> List.length order then
        None 
    else 
        Some result
    