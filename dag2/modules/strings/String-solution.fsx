module String = 

    let substring start length (s : string) = 
        s.Substring(start, length)
  
    let crop maxlength s = 
        if String.length s > maxlength then substring 0 maxlength s else s

    let undup : string -> string = 
        let rec unduplist lst = 
            match lst with 
            | [] -> []
            | [c] -> [c]
            | x :: y :: t -> 
                if x = y then unduplist (y :: t) 
                else x :: unduplist (y :: t)
        Seq.toList >> unduplist >> List.toArray >> System.String
         
    let reverse (s : string) = 
        Seq.fold (fun result c -> string(c) + result) "" s

"###f###" |> String.substring 3 2 |> printfn "%s"
"kokkelue" |> String.crop 4 |> printfn "%s"
"lappeteppe" |> String.undup |> printfn "%s"
"langøre" |> String.reverse |> printfn "%s"
"langøre" |> (String.reverse >> String.reverse) |> printfn "%s"

"hello world" 
|> String.substring 1 9     // ello worl
|> String.crop 4            // ello
|> String.undup             // elo
|> String.reverse           // ole
|> printfn "%s"

"hestesko"
|> String.reverse
|> String.crop 4
|> printfn "%s"

"hestesko"
|> String.substring 4 4 
|> String.reverse
|> printfn "%s"
