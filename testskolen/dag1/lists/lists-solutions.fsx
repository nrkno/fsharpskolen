// Her kommer vi til å leke med funksjoner med lister som finnes i List-modulen

let numbers = [1 .. 10]

// Reverser lista
let reverse lst = List.rev lst

printfn "Reversed of %A is %A" numbers (reverse numbers)

// Finn første element i lista
let first lst = List.head lst

printfn "First element of %A is %A" numbers (first numbers)

// Finn siste element i lista
let last lst = first (reverse lst)

printfn "Last element of %A is %A" numbers (last numbers) 


// Finn partallene i lista
let evenNumbers lst = List.filter (fun x -> x % 2 = 0) lst

printfn "Even elements of %A is %A" numbers (evenNumbers numbers)


// Finn summen av elementene lista
let sum lst = List.sum lst

printfn "The sum of the elements of %A is %d" numbers (sum numbers)


// Finn summen av kvadratene i lista: 1*1 + 2*2 + 3*3 etc
let sumOfSquares lst =
    lst
    |> List.map (fun x -> x * x)
    |> List.sum

printfn "The sum of the square of elements of %A is %d: %b" numbers (sumOfSquares numbers) ((sumOfSquares numbers) = 385)