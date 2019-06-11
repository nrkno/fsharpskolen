module Expecto.Tests

open Expecto

let tests = testList "A list of tests" [
  test "A simple test" {
    let subject = "Hello World"
    Expect.equal subject "Hello World" "The strings should equal"
  }
  test "One plus one is two" {
    Expect.equal (1 + 1) 2 "One plus one is two"
  }]

let otherTests = testList "Another list of tests" [
  test "" {
      let text = "Hello"
      Expect.equal (String.length text) 5 "String length should be as expected"
  }
]

[<EntryPoint>]
let main args =
  runTestsWithArgs defaultConfig args <| testList "All the tests" [tests; otherTests]
