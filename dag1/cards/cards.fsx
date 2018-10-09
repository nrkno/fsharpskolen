// Kortstokk
// Vi skal spille poker, men først trenger vi kortene i en kortstokk

// Et kort består av en farge (suit) og en verdi (face), hvordan skal disse typene se ut?
type Suit = ChangeMe

type Face = ChangeMe

type Card = ChangeMe


// Nå har vi typen for kort, hva er spar ess?
let aceOfSpades = "change me"


// hvordan kan vi lage en liste med hele kortstokken?
let deck = []

// En poker hånd på 5 kort er rangert etter følgende liste
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


// Hva er typen til en hånd med 5 kort? Kanskje noe med en liste?
// Vi må vite om kortene i en hånd har samme farge
let rec isSameSuit hand = true

// Og vi må vite om kortene er i sekvens. 
// Kanskje du må lage en funksjon som mapper mellom face og en tallverdi?
let cardToValue card = 0

let isSequence hand = true


// Da kan vi starte på den den rangerte lista over pokerhender
let isRoyalFlush hand = true

// straight flush burde være easy peasy nå
let isStraightFlush hand = true

// I fourOfAKind kan du kanskje få bruk for List.groupBy?
let isFourOfAKind hand = true

// kanskje på tide å teste om funksjonene vi har til nå funker som de skal?
// lag hender som skal gi true for royal flush, straight fluh og four of a kind, og en som gir false.

