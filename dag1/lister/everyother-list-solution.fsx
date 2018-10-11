let rec everyother (lst : 'a list) : 'a list = 
  match lst with 
  | [] -> []
  | [x] -> [x]
  | x::_::t -> x :: everyother t

let everyotter lst = 
  match lst with 
  | [] -> []
  | _ :: t -> everyother t

everyother [1 .. 10] |> printfn "%A"
everyother ['a' .. 'e'] |> printfn "%A"
everyother ["hello"; "world"] |> printfn "%A"
everyother [] |> printfn "%A"

everyotter [1 .. 10] |> printfn "%A"
everyotter ['a' .. 'e'] |> printfn "%A"
everyotter ["hello"; "world"] |> printfn "%A"
everyotter [] |> printfn "%A"
