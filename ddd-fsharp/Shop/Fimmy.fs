module Fimmy
let randomGenerator = System.Random()

let introduceError (percentageFail: int) (fragileFunc : ('a->'b)) : ('a -> 'b) =
    let crashingFunc x = 
        let randomNum = randomGenerator.Next(100)
        if randomNum > percentageFail then
            failwith "Network error"
        else fragileFunc x
    crashingFunc
