
let square x = x * x

let double x = 2 * x

let areaOfDouble = double >> square

let areaOfDoubleSquare2  x = x |> double |> square

let hypotenuseSquared a b = square a + square b

//let euclideanNumbers a b = 


let isPythagoreanTriple a b c = 
    if a > b && a > c then square a = square b + square c
    else if b > a && b > c then square b = square a + square c
    else square c = square a + square b



