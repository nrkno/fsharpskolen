module Fimmy
let randomGenerator = System.Random()

let introduceError (percentageFail: int) (fragileFunc : ('a->'b)) : ('a -> 'b) =
    let crashingFunc x = 
        let randomNum = randomGenerator.Next(100)
        if randomNum > percentageFail then
            failwith "Network error"
        else fragileFunc x
    crashingFunc

let introduceLatency (chanceOfLatency: int) (latencyInMs: int) (origninalFunc: ('a->'b)) : ('a -> Async<'b>) =
    let slowFunc x = 
        let randomNum = randomGenerator.Next(100)
        async {
            if randomNum > chanceOfLatency then
                do! Async.Sleep latencyInMs
                return origninalFunc x
            else 
                return origninalFunc x
        }
    slowFunc
