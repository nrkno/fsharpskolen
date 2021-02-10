module Option = 

    // The bind function for Options
    let bind f xOpt = 
        match xOpt with
        | Some x -> f x
        | _ -> None
    // has type : ('a -> 'b option) -> 'a option -> 'b option

module List = 

    // The bind function for lists
    let bindList (f: 'a->'b list) (xList: 'a list)  = 
        [ for x in xList do 
          for y in f x do 
              yield y ]
    // has type : ('a -> 'b list) -> 'a list -> 'b list

// og asynk