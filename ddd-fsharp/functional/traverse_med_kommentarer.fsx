module Option = 
    let pure (value : 'a) : 'a option = Some value 
    
    let apply (wrappedFn : ('a -> 'b) option) (wrappedValue : 'a option) : 'b option = 
        match wrappedFn, wrappedValue with 
        | Some fn, Some value -> pure (fn value)
        | _ -> None 

    // Eksempel: worldCrossingFn x = match x % 2 with | 0 -> Some true | _ -> None
    //           lst = [2;3;4;5]
    //           'c = int
    //           'd = bool
    let rec traverseA (worldCrossingFn : 'c -> 'd option) (lst : 'c list) : 'd list option = 
        let cons (head: 'd) (tail: 'd list) : 'd list = head :: tail // Eksempel for bool: cons true [false; true; true] -> [true; false; true; true]
        match lst with 
        | [] -> pure [] // Some []
        | (head: 'c) :: (tail: 'c list) -> // [2;3;4;5]
            let maybeHead : 'd option = worldCrossingFn head // worldCrossingFn 2 -> Some true
            let maybeTail : 'd list option = traverseA worldCrossingFn tail // worldCrossingFn 3 -> None, worldCrossingFn 4 -> Some true, worldCrossingFn 5 -> None, traverseA worldCrossingFn tail = [None, Some true, None]
            let maybeHeadAttacher : ('d list -> 'd list) option = apply (pure cons) maybeHead // pure cons -> Some cons -> Some ('d -> 'd list -> 'd list), i kontekst av apply er 'a = 'd, og 'b = 'd list -> 'd list
                                                                                              // apply (pure cons) = 'd option -> ('d list -> 'd list) option
                                                                                              // apply (pure cons) maybeHead = ('d list -> 'd list) option, med maybeHead bakt inn som elementet som legges til
                                                                                              // maybeHeadAttacher = apply (pure cons) maybeHead = ('d list -> 'd list) option, med maybeHead bakt inn som elementet som legges til
            apply maybeHeadAttacher maybeTail // maybeHeadAttacher = ('d list -> 'd list) option, med maybeHead bakt inn som elementet som legges til
                                              // i kontekst av apply er 'a = 'd list, 'b = 'd list og wrappedFn = ('d list -> 'd list) option
                                              // Dvs. at (apply maybeHeadAttacher) = 'd list option -> 'd list option
                                              // Videre er (apply maybeHeadAttacher maybeTail) å kalle (apply maybeAttacher) med maybeTail ('d list option) som argument
                                              // Det fører til at vi baker inn den indre verdien fra maybeHead ('d) i den indre listen til maybeTail ('d list) og hever det resultatet i en option slik at vi får en ('d list option)