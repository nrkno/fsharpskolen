module TicksSpec.Tests.Steps

open System.Diagnostics
open TickSpec
open TickSpec.Tests.Domain


let [<Given>] ``I have (.*) apples in the basket`` (n: int) = 
    AppleBasket.create n
    
let [<When>] ``Lars gives me (.*) apples`` (n: int) (appleBasket: AppleBasket)  =
    AppleBasket.add appleBasket n
    
let [<Then>] ``I should have (.*) apples in the basket`` (n: int) (appleBasket: AppleBasket) =
    let passed = (AppleBasket.value appleBasket = n)
    Debug.Assert(passed)