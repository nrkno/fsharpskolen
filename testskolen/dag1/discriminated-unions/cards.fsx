// Kortstokk
// Vi skal spille poker, men først trenger vi kortene i en kortstokk

// Et kort består av en farge (suit) og en verdi (face), hvordan skal disse typene se ut?
type Suit = ChangeMe

type Face = ChangeMe

type Card = ChangeMe


// Nå har vi typen for kort, hva er spar ess?
let aceOfSpades = "change me"


// En pokerhånd på 5 kort er rangert etter følgende liste
// 1 Royal flush A, K, Q, J, 10, all the same suit. 
// 2 Straight flush - Five cards in a sequence, all in the same suit
// 3 Four of a kind - All four cards of the same rank. 
// 4 Full house - Three of a kind with a pair. 
// 5 Flush - Any five cards of the same suit, but not in a sequence. 
// 6 Straight - Five cards in a sequence, but not of the same suit. 
// 7 Three of a kind - Three cards of the same rank. 
// 8 Two pair - Two different pairs. 
// 9 Pair - Two cards of the same rank. 
// 10 High card - When you haven't made any of the hands above, the highest card plays. 

// Prøv å lage noen hender, feks royal flush, straight flush og four of a kind
// For enkelthets skyld  lar vi det være en liste som inneholder fem elementer

let royalFlushHand = []
let straightFlushHand = []
let fourOfAKindHand = []


// La oss se om vi kan skrive noen funksjoner som kan hjelpe oss med å verifisere at hendene vi har laget oppfyller reglene.

// Vi vil trenge å vite om kortene er i sekvens, kanksje det er lurt med en funksjon som mapper mellom face og en tallverdi?
let cardToValue card = 0
    
// Vi må vite om kortene i en hånd har samme farge
let isSameSuit hand = false

// og om de er i sekvens
let isSequence hand = false


// Da kan vi starte med å sjekke om en hånd er royal flush
let isRoyalFlush hand = false

// straight flush burde være easy peasy nå
let isStraightFlush hand = false

// I fourOfAKind kan du kanskje få bruk for List.groupBy?
let isFourOfAKind hand = false


printfn "royalFlushHand is royal flush: %b" (isRoyalFlush royalFlushHand)
printfn "straightFlushHand is straight flush: %b" (isStraightFlush straightFlushHand)
printfn "straightFlushHand is royal flush: %b" (isRoyalFlush straightFlushHand)
printfn "fourOfAKindHand has four of a kind: %b" (isFourOfAKind fourOfAKindHand)
printfn "fourOfAKindHand is royal flush: %b" (isRoyalFlush fourOfAKindHand)
printfn "fourOfAKindHand is straight flush: %b" (isStraightFlush fourOfAKindHand)
