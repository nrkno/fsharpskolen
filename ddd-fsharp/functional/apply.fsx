// fra https://fsharpforfunandprofit.com/posts/elevated-world/#the-return-function 
let add y x = x + y
let add10 x = add 10 x
let add20 x = add 20 x

module Option =

    // The apply function for Options
    let apply fOpt xOpt = 
        match fOpt,xOpt with
        | Some f, Some x -> Some (f x)
        | _ -> None

    let foo = Some add10 // Some is return here and lifts/returns the function to Some-land 
    let bar = Some 20  // And we can lift/return 20 to Some-land too
    apply foo bar // Then we can apply stuff in some-land

module List =

    // The apply function for lists
    // [f;g] apply [x;y] becomes [f x; f y; g x; g y]
    let apply (fList: ('a->'b) list) (xList: 'a list)  = 
        [ for f in fList do
          for x in xList do
              yield f x ]

    let foo = [add10; add20] // We can lift/return both these, but not sure this is really lift when we do it with both?
    let bar = [ 10; 20] // and this too... Not really sure if it is lift/return in the same way
    apply foo bar

