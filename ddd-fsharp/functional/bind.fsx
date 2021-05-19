// In the previous apply section we looked at unit, apply and map to lift functions
// from normal world to elevated worlds

// But what if the functions are crossing the world, that is we have string -> Async<int> ?
// Or more general, a -> E<b>
// With map we could take a (a->b) to a E<a> -> E<b>
// In a similar way we can - using bind - take an a -> E<b> and make a E<a> -> E<b>

// Takes a string and returns a list of its charcodes 
// This is a function from a -> E<b> with a being a string, E the list world and b int
let stringToCharcodes (s:string) = Array.map int (System.Text.Encoding.ASCII.GetBytes(s)) |> Array.toList
stringToCharcodes "foo"


module Lazy
    let bind (f: ('a -> (unit -> 'b))): (unit -> 'a) -> (unit -> 'b) = 
        fun (foo: (unit -> 'a)) -> let a = foo()
                                   f a


    (getPassword 11 |> bind timebomb |> map' add10) ()
    (getPassword 1 |> bind timebomb |> map' add10) ()

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
module Async =
    let bind fn value = async.Bind(value, fn)

