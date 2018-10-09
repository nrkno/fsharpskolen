
let isPrime x = 
    let rec helper x curr = 
        if curr = x then true 
        else if x % curr = 0 then false 
        else helper x (curr + 1)
    helper x 2
        