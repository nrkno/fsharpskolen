
let stringLength (str  : string option) = 
    match str with
    | Some s -> s.Length
    | None -> 0

let stringLength2 str = 
    Option.map (fun (s  : string) -> s.Length) str

stringLength2 (Some "hei")

let stringLength3 (str : string option) = 
    Option.fold (fun _ (s : string) -> s.Length) 0 str


let toNaturalNumber x = 
    if x > 0 then Some (x) else None

let addition x y = 
    match x, y with
    | Some a, Some b -> Some (a + b)
    | None, _ -> None
    | _, None -> None

let add2 x y = 
    Option.map2 (+) x y