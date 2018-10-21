

let isPrime x = 
    let rec helper curr = 
        if curr = x then true 
        else if x % curr = 0 then false 
        else helper (curr + 1)
    helper 2
        
let isSquare m = 
    let rec helper x = 
        if x = m / 2 then false
        else if x * x = m then true
        else helper (x + 1)
    helper 1

let squareEstimate m = 
    let rec helper x = 
        if x * x = m then x
        else if x * x < m && (x + 1) * (x + 1) > m then x + 1
        else helper (x + 1)
    helper 1

let fermatFactorization n = 
    let rec helper a b =
        if isSquare b then a - (squareEstimate b)
        else helper (a + 1) ((a + 1) * (a + 1) - n)
    let a = squareEstimate n
    helper a (a * a - n)

let rec printFermatFactorization n = 
    let a = fermatFactorization n
    let b =  n / a
    if isPrime a then printfn "%d" a
    else if a > 1 then printFermatFactorization a
    if isPrime b then printfn "%d" b
    else if b > 1 then printFermatFactorization b