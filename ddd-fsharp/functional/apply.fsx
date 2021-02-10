

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

    let foo = Some add10 // Some is return here and lifts/returns the function to Some-land 
    let bar = Some 20  // And we can lift/return 20 to Some-land too
    let unpackedFoo = apply foo// Then we can apply stuff in some-land
    unpackedFoo bar 

    let a1 = apply (Some String.length) 
    a1 (Some "foo")
    a1 None
    let a2 = apply (None: (string -> int) option)
    a2 (Some "foo") 
    a2 None 

    let pure' x = Some x 
    let foo2 = pure' sumOfString
    let unpackedFoo2 = apply foo2
    unpackedFoo2 (pure' "Hei")


module List =

    // The apply function for lists
    // [f;g] apply [x;y] becomes [f x; f y; g x; g y]
    let apply (fList: ('a->'b) list) (xList: 'a list)  = 
        [ for f in fList do
          for x in xList do
              yield f x ]
    
    let pure' x = [x]

    let map' fn = apply (pure' fn)

    let foo = [add10; add20] // We can liftboth these, but not sure this is really lift when we do it with both?
    let bar = [ 10; 20] // and this too... Not really sure if it is lift/return in the same way
    
    let unpackedFoo = apply foo// Then we can apply stuff in some-land
    unpackedFoo bar 
    
    let unpacked2 = apply [String.length; sumOfString]
    unpacked2 ["hei"; "fooooo"] 
    unpacked2 (pure' "Hei")

module Async = 
//Borrowed from https://github.com/fsprojects/FSharpPlus/blob/master/src/FSharpPlus/Extensions/Async.fs#L47
    let apply f x = async.Bind (f, fun x1 -> async.Bind (x, fun x2 -> async {return x1 x2}))

    let pure' (value : 'a) : Async<'a> = async.Return value

    // alternative apply fra einar
    // let bind fn value = async.Bind(value, fn)
    //    let apply wrappedFn wrappedValue = wrappedFn |> bind (fun fn -> wrappedValue |> bind (fun value -> pure (fn value)))
    let foo = pure' add10
    let unpackedFoo = apply foo 

    let foo2 = pure' sumOfString
    let unpackedFoo2 = apply foo2
    unpackedFoo2 (pure' "Hei")
