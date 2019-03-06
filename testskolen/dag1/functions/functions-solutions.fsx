// Funksjonen square regner ut kvadratet av et tall
let square x = x * x

// Funksjonen  mult ganger x med n
let mult n x = n * x

// areaOfDoubled finner arelet av et kvadrat som har sidelengde 2 * x 
// prøv å gjenbruke funksjonene over
let areaOfDoubled x = square (mult 2 x)
// kan også skrives med pipes: x |> mult 2 |> square

printfn "Doubled area: 4 * %d = %d" (square 2) (areaOfDoubled 2)

// Og så litt streng-moro! Vi skal lage en leetifyer som erstatter vokaler med tall som ligner
// https://no.wikipedia.org/wiki/Leet
// Følgende bokstaver vil vi erstatte:
// a -> 4
// e -> 3
// i -> 1
// o -> 0
// Kan være nyttig å bruke Replace-metoden som fins på en string
let leetify (str : string) = str.Replace("a", "4").Replace("e", "3").Replace("i", "1").Replace("o", "0");

let printLeetify str = 
    printfn "%s %s" str (leetify str)

printLeetify "To be, or not to be: that is the question"
printLeetify "Romeo, Romeo! wherefore art thou Romeo?"
printLeetify "What’s in a name? A rose by any name would smell as sweet"
