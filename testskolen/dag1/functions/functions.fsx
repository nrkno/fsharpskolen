// Funksjonen square regner ut kvadratet av et tall
let square x = 0

// Funksjonen  mult ganger x med n
let mult n x = 0

// areaOfDoubled finner arelet av et kvadrat som har sidelengde 2 * x 
// prøv å gjenbruke funksjonene over
let areaOfDoubled x = 0

printfn "Doubled area: 4 * %d = %d" (square 2) (areaOfDoubled 2)

// Og så litt streng-moro! Vi skal lage en leetifyer som erstatter vokaler med tall som ligner
// https://no.wikipedia.org/wiki/Leet
// Følgende bokstaver vil vi erstatte:
// a -> 4
// e -> 3
// i -> 1
// o -> 0
// Kan være nyttig å bruke Replace-metoden som fins på en string
let leetify (str : string) = str

let printLeetify str = 
    printfn "%s %s" str (leetify str)

printLeetify "To be, or not to be: that is the question"
printLeetify "Romeo, Romeo! wherefore art thou Romeo?"
printLeetify "What’s in a name? A rose by any name would smell as sweet"



