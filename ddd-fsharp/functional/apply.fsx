// fra https://fsharpforfunandprofit.com/posts/elevated-world/#the-return-function 
module Option =

    // The apply function for Options
    let apply fOpt xOpt = 
        match fOpt,xOpt with
        | Some f, Some x -> Some (f x)
        | _ -> None

module List =

    // The apply function for lists
    // [f;g] apply [x;y] becomes [f x; f y; g x; g y]
    let apply (fList: ('a->'b) list) (xList: 'a list)  = 
        [ for f in fList do
          for x in xList do
              yield f x ]

    let add y x = x + y
    let add10 x = add 10 x
    let add20 x = add 20 x
    let foo = [add10; add20]
    let bar = [ 10; 20]
    apply foo bar

