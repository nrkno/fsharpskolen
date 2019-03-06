// Her kommer vi til å leke med funksjoner med lister som finnes i List-modulen

let numbers = [1 .. 10]

// Reverser lista
let reverse lst = lst

printfn "Reversed of %A is %A" numbers (reverse numbers)

// Finn første element i lista
let first lst = lst

printfn "First element of %A is %A" numbers (first numbers)

// Finn siste element i lista
let last lst = lst

printfn "Last element of %A is %A" numbers (last numbers) 


// Finn partallene i lista
let evenNumbers lst = lst

printfn "Even elements of %A is %A" numbers (evenNumbers numbers)


// Finn summen av elementene lista
let sum lst = 0

printfn "The sum of the elements of %A is %d" numbers (sum numbers)


// Finn summen av kvadratene i lista: 1*1 + 2*2 + 3*3 etc
let sumOfSquares lst = 0

printfn "The sum of the square of elements of %A is %d: %b" numbers (sumOfSquares numbers) ((sumOfSquares numbers) = 385)