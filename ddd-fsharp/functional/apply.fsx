// fra https://fsharpforfunandprofit.com/posts/elevated-world/#the-return-function 

// This little page will introduce apply and pure and combine them to map
// For Options, Lists and Async

// Just some normal functions to start with
let add y x = x + y
let add10 x = add 10 x
let add20 x = add 20 x

// Sums the char code of the letters in a string
let sumOfString (s:string) = Array.sumBy int (System.Text.Encoding.ASCII.GetBytes(s))

// It helps thinking of these things as partial applications, small steps...
// For me it clicked a bit thinking of partial application of map instead of "using the map on a list" 
// Here map takes a function from (int -> int) and makes a List<int> -> List<int>
let foo = List.map add20

// Here map takes a function from (string-> int) and makes a List<string> -> List<int>
let bar = List.map sumOfString 

// That is an example of (a->b) being transformed to E<a> to E<b>
// where E is a list and a is string or int in the examples and b is an int

// Let us see more examples of how we do this with pure and apply
// And build map from these primitives

module Option =
    // a -> E<a>, lifts a single value to the elevated world
    // In this case
    // a -> Some<a>, lifts a single value to the option world
    let pure' x = Some x 
    let notpure x = None // this will not hold the applicative laws, but it has the same signature 


    // Because functions also are values, they too can be lifted with pure
    // (a->b) -> E<(a->b)>
    // Pure lifts the function int -> int to a (int -> int) option
    // Bar is E<(int->int)>
    let bar = (pure' add10) 
    // Foo is E<(int->string)>
    let foo = (pure' sumOfString ) 
  
    // Apply: E<(a->b)> -> E<a> -> E<b>
    // In the option case here
    // (a->b) option -> a option -> b option 
    // The apply function for Options
    let apply fOpt xOpt = 
        match fOpt,xOpt with
        | Some f, Some x -> Some (f x)
        | _ -> None

    apply (pure' id) (Some 5) = Some 5  // Holds the law
    apply (notpure id) (Some 6) = Some 5 // Fails the law

    // The applied function takes the
    // E<(int->int)> -> E<int> -> E<int>, which in this case is
    // (int->int) option to a int option -> int option
    let add10Applied = apply (pure' add10) 
    add10Applied (pure' 20) 

    let appliedStringLength = apply (pure' String.length) 
    appliedStringLength (pure' "foo")
    appliedStringLength None
    let appliedNoneStringop = apply (None: (string -> int) option)
    appliedNoneStringop (pure' "foo") 
    appliedNoneStringop None 

    let appliedSumOfString = apply (pure' sumOfString) // E<string> -> E<int>
    appliedSumOfString (pure' "Hei")

    let map' fn = apply (pure' fn)

// We could probably do this for a lot of collections like array and set and I am not sure how many...
// They would also most likely be very similar, so we pick lists
module List =
    // a -> E<a>, lifts a single value to the elevated world
    // In this case
    // a -> List<a>, lifts a single value to the list world
    let pure' x = [x]
    let notpure x = [x; x] 

    // The apply function for lists
    // [f;g] apply [x;y] becomes [f x; f y; g x; g y]
    // E<(a->b)> -> E<a> -> E<b>
    let apply (fList: ('a->'b) list) (xList: 'a list)  = 
        [ for f in fList do
          for x in xList do
              yield f x ]
    
    apply (pure' id) [5] = [5]
    apply (notpure id) [5] = [5]

    apply [add10; add20] [3; 7]

    // Lifts the (int->int) to List world with pure to be a List<(int->int)>
    // Then applies it and it turns into a List<int> -> List<int>
    let add10Applied = apply (pure' add10) // E<int> -> E<int>

    add10Applied (pure' 20) 
    add10Applied [1; 2; 3] // But this is map! 

    // So let us make map with apply and pure
    let map' fn = apply (pure' fn)

    (map' add10) [2;3;4]


module Async = 
    // a -> E<a>, lifts a single value to the elevated world
    // In this case
    // a -> Async<a>, lifts a single value to the async world
    let pure' (value : 'a) : Async<'a> = async.Return value
    let notpure (value : 'a) : Async<'a> = async { do! Async.Sleep(10)
                                                   return value}

    //Apply is Borrowed from https://github.com/fsprojects/FSharpPlus/blob/master/src/FSharpPlus/Extensions/Async.fs#L47
    let apply f x = async.Bind (f, fun x1 -> async.Bind (x, fun x2 -> async {return x1 x2}))

    // Set up the timeout is 1ms to help explain why they are not similar, even it they at some point in time will be
    let runSyncWithTimeout task = Async.RunSynchronously(task, 1)
    (apply (pure' id) (async.Return 5) |> runSyncWithTimeout) = ((async.Return 5) |> runSyncWithTimeout) 
    apply (notpure id) (async.Return 5) |> runSyncWithTimeout = ((async.Return 5) |> runSyncWithTimeout) 

    // alternative apply fra einar
    // let bind fn value = async.Bind(value, fn)
    // let apply wrappedFn wrappedValue = wrappedFn |> bind (fun fn -> wrappedValue |> bind (fun value -> pure (fn value)))

    let add10Applied = apply (pure' add10) 
    add10Applied (pure' 20) |> Async.RunSynchronously

    let map' fn = apply (pure' fn)

    // Map lifts a function (a->b) to E<a> -> E<b> 
    // In this case we take (string->int) to Async<string> -> Async<int>
    // Map sumOfString from (string -> int) to Async<string> -> Async<int>
    let asyncSum = map' sumOfString

    let getPing = async {
        let httpClient = new System.Net.Http.HttpClient()
        let! response = httpClient.GetAsync("https://psapi.nrk.no/ping") |> Async.AwaitTask
        response.EnsureSuccessStatusCode () |> ignore
        let! content = response.Content.ReadAsStringAsync() |> Async.AwaitTask
        return content
    } 

    getPing |> asyncSum |> Async.RunSynchronously 