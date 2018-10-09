let everyother (lst : 'a list) : 'a list = 
    let rec help toggle lst = 
        match lst with 
        | [] -> []
        | h::t ->
          let rest = help (not toggle) t 
          if toggle then h :: rest else rest 
    help true lst

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
