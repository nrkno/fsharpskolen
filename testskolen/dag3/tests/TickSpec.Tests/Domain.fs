module TickSpec.Tests.Domain

type AppleBasket = private AppleBasket of int
    

module AppleBasket =
    let create n =
        if n > 0 then
            AppleBasket n
        else AppleBasket 0
        
    let value (AppleBasket n) = n
    
    let add (AppleBasket n) m = AppleBasket (n + m)
    