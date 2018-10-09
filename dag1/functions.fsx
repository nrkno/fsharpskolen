
let areaOfSquare x = x * x

let doubleSquareSide x = 2 * x

let areaOfDoubleSquare = doubleSquareSide >> areaOfSquare

let areaOfDoubleSquare2  x = x |> doubleSquareSide |> areaOfSquare