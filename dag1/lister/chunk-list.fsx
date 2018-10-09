// Lag en funksjon 'chunk' med signaturen under.
// Funksjonen skal lage en liste av lister med de opprinnelige elementene.
// Hver subliste bortsett fra eventuelt den siste skal inneholde n elementer. 
// Den siste sublisten skal inneholde minst ett element, og maks n elementer.
// Enkle eksempler:
// chunk 3 [1 .. 9]  -> [ [1;2;3] ; [4;5;6] ; [7;8;9] ]
// chunk 3 [1 .. 10] -> [ [1;2;3] ; [4;5;6] ; [7;8;9] ; [10]]
// Hva er rimelig oppførsel dersom n < 1? Dersom n > antall elementer i listen?

let chunk (n : int) (things : 'a list) : 'a list list = []

chunk 3 [1 .. 9] |> printfn "%A"
chunk 3 [1 .. 10] |> printfn "%A"
chunk 2 ['a' .. 'e'] |> printfn "%A"
chunk 0 [1 .. 10] |> printfn "%A"
chunk 2 [1] |> printfn "%A"
chunk 1 [] |> printfn "%A"
chunk 1 [[[[]]]] |> printfn "%A"
