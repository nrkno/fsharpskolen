

// fra https://fsharpforfunandprofit.com/posts/elevated-world/#the-return-function 
let add y x = x + y
let add10 x = add 10 x
let add20 x = add 20 x


// it helps thinking of these things as partial applications, small steps...
let foo = List.map add20
let sumOfString (s:string) = Array.sumBy int (System.Text.Encoding.ASCII.GetBytes(s))

module Option =

    // The apply function for Options
    let apply fOpt xOpt = 
        match fOpt,xOpt with
        | Some f, Some x -> Some (f x)
        | _ -> None

    let pure' x = Some x 
    let pure'' x = None // this will not hold the applicative laws 

    apply (pure' id) (Some 5) = Some 5  // Holds
    apply (pure'' id) (Some 6) = Some 5 // Fails

    let add10Applied = apply (pure' add10) // E<int> -> E<int>
    add10Applied (pure' 20) 

    let appliedStringLength = apply (pure' String.length) 
    appliedStringLength (pure' "foo")
    appliedStringLength None
    let appliedNoneStringop = apply (None: (string -> int) option)
    appliedNoneStringop (pure' "foo") 
    appliedNoneStringop None 

    let appliedSumOfString = apply (pure' sumOfString) // E<string> -> E<int>
    appliedSumOfString (pure' "Hei")


// We could probably do this for a lot of collections like array and set and I am not sure how many...
// They would also most likely be very similar
module List =
    // The apply function for lists
    // [f;g] apply [x;y] becomes [f x; f y; g x; g y]
    let apply (fList: ('a->'b) list) (xList: 'a list)  = 
        [ for f in fList do
          for x in xList do
              yield f x ]
    
    let pure' x = [x]
    let pure'' x = [x; x] 

    apply (pure' id) [5] = [5]
    apply (pure'' id) [5] = [5]

    let map' fn = apply (pure' fn)

    apply [add10; add20] [3; 7]

    let add10Applied = apply (pure' add10) // E<int> -> E<int>
    add10Applied (pure' 20) 

module Async = 
//Borrowed from https://github.com/fsprojects/FSharpPlus/blob/master/src/FSharpPlus/Extensions/Async.fs#L47
    let apply f x = async.Bind (f, fun x1 -> async.Bind (x, fun x2 -> async {return x1 x2}))

    let pure' (value : 'a) : Async<'a> = async.Return value
    let pure'' (value : 'a) : Async<'a> = async { do! Async.Sleep(10) 
                                                  return value } 

    // This is most likely cheating, because they should be compared before they are 
    // run and they are most certainly not equal at some point in time when one has completed and the other has not
    let timeoutStart task = Async.RunSynchronously(task, 1)
    (apply (pure' id) (async.Return 5) |> timeoutStart) = ((async.Return 5) |> timeoutStart) 
    apply (pure'' id) (async.Return 5) |> timeoutStart = ((async.Return 5) |> timeoutStart) 

    // alternative apply fra einar
    // let bind fn value = async.Bind(value, fn)
    //    let apply wrappedFn wrappedValue = wrappedFn |> bind (fun fn -> wrappedValue |> bind (fun value -> pure (fn value)))
    let add10Applied = apply (pure' add10) 
    add10Applied (pure' 20) |> Async.RunSynchronously