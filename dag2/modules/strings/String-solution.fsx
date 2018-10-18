module String = 

    let substring start stop (s : string) = 
        s.Substring(start, stop)
  
    let crop maxlength s = 
        if String.length s > maxlength then substring 0 maxlength s else s
    
    let reverse (s : string) = 
        Seq.fold (fun result c -> string(c) + result) "" s
   
"hello world" 
|> String.substring 1 9     // ello worl
|> String.crop 4            // ello
|> String.reverse           // olle
|> printfn "%s"
