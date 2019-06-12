module Expecto.Tests

open Expecto
open System

let random = Random()

let invoke (setup: unit -> 'a) (tearDown: 'a -> unit ) (test : 'a -> Test) = 
    let id = setup ()
    try
        printfn "Running test for id %d" id
        test id
    finally
        tearDown id


let tests = testList "A list of tests" [
  test "A simple test" {
    let subject = "Hello World"
    Expect.equal subject "Hello World" "The strings should equal"
  }
  test "One plus one is two" {
    Expect.equal (1 + 1) 2 "One plus one is two"
  }]

let otherTests =
  let setup () =
    let id = random.Next(0, 10000)
    printfn "Setup id %d before test" id
    id

  let tearDown id = 
    printfn "Tidy up things with id %d after test" id
  
  let invoke = invoke setup tearDown

  testList "Another list of tests" [
  invoke (fun x -> test "x + x = 2 * x" {
      Expect.equal (x + x) (2 * x) "x + x = 2 * x"
  })
  invoke (fun x -> test "sqrt x * x = x" {
    let sqrt = x |> float |> (fun x -> Math.Sqrt (x * x)) |> int
    Expect.equal sqrt x "sqrt x * x = x"
  })
]

[<EntryPoint>]
let main args =
  runTestsWithArgs defaultConfig args <| testList "All the tests" [tests; otherTests]
