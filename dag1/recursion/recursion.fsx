// Et primtall er et tall > 1 som bare er delelig med 1 og seg selv. Altså er 5 primtall fordi 5 bare kan deles på 1 og 5, mens 6 ikke er et primtall.
// En strategi for å finne ut om en tall er et primtall er å forsøke å dele det på alle lavere tall og se at det ikke gir 0 i rest.
// Skriv en funksjon som sjekker om et gitt tall er et primtall eller ikke
let isPrime x = false

// Nå som vi vet om et tall er primtall eller ikke er det selvsagt fristende å kunne faktorisere et vilkårlig tall i primtallsfaktorer
// Et måte å faktorisere oddetall på er å bruke Fermats metode https://en.wikipedia.org/wiki/Fermat%27s_factorization_method
// Før vi kan implementere algoritmen trenger vi to hjelpefunksjoner, 
// en for å si om et tall er et kvadrat eller ikke, og en for å estimere kvadratroten

let isSquare = false

// squareEstimate av n er kvadratroten om n er et kvadrat, hvis ikke er det kvadratroten rundet opp til nærmeste heltall (ceil (sqrt n))
let squareEstimate = 0

// Med de to hjelpefunksjonene på plass kan vi implementere Fermats metode
let fermatFactorization n = 0


// test faktoriseringa med ulike tall, feks 1227, 13275
let rec printFermatFactorization n = 
    let a = fermatFactorization n
    let b =  n / a
    if isPrime a then printfn "%d" a
    else if a > 1 then printFermatFactorization a
    if isPrime b then printfn "%d" b
    else if b > 1 then printFermatFactorization b