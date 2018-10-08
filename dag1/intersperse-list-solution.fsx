let rec intersperse (value : 'a) (things : 'a list) : 'a list = 
  match things with 
  | [] -> []
  | [a] -> [a]
  | a::t -> a :: value :: intersperse value t
    
intersperse 0 [1 .. 5] |> printfn "%A"
intersperse "foo" [ "hello"; "dear"; "friends" ] |> printfn "%A"
intersperse [] [[];[];[]] |> printfn "%A"
