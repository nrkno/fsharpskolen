// Kortstokk
// Vi skal spille poker, men først trenger vi kortene i en kortstokk

// Et kort består av en farge (suit) og en verdi (face), hvordan skal disse typene se ut?
type Suit = Diamonds | Hearts | Clubs | Spades

type Face = Two | Three | Four | Five | Six | Seven | Eight | Nine | Ten | Jack | Queen | King | Ace

type Card = Card of Suit * Face

let aceOfSpades = Card (Spades, Ace)

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

let royalFlushHand = [Card(Spades, Ace); Card(Spades, Ten); Card(Spades, Queen); Card(Spades, Jack); Card(Spades, King)]
let straightFlushHand = [Card(Hearts, Ten); Card(Hearts, Seven); Card(Hearts, Eight); Card(Hearts, Six); Card(Hearts, Nine)]
let fourOfAKindHand = [Card(Diamonds, Five); Card(Hearts, Five); Card(Spades, Five); Card(Spades, Three); Card(Clubs, Five)]


// La oss se om vi kan skrive noen funksjoner som kan hjelpe oss med å verifisere at hendene vi har laget oppfyller reglene.

// Vi vil trenge å vite om kortene er i sekvens, kanksje det er lurt med en funksjon som mapper mellom face og en tallverdi?
let cardToValue (Card (_, face)) =
    match face with
    | Two -> 2
    | Three -> 3
    | Four -> 4
    | Five -> 5
    | Six -> 6
    | Seven -> 7
    | Eight -> 8
    | Nine -> 9
    | Ten -> 10
    | Jack -> 11
    | Queen -> 12
    | King -> 13
    | Ace -> 14
    
// Vi må vite om kortene i en hånd har samme farge
let isSameSuit hand = 
    hand
    |> List.map (fun (Card(suit, _)) -> suit)
    |> List.distinct
    |> List.length = 1

let isSequence hand = 
    let sorted = List.sortBy cardToValue hand
    let lowestCard = List.item 0 sorted
    let highestCard = List.item 4 sorted
    cardToValue highestCard - cardToValue lowestCard = 4


// Da kan vi starte med å sjekke om en hånd er royal flush
let isRoyalFlush hand = 
    let sorted = List.sortBy cardToValue hand
    let (Card (_, lowFace)) = List.item 0 sorted
    let (Card (_, highFace)) = List.item 4 sorted
    lowFace = Ten && highFace = Ace && isSameSuit hand

// straight flush burde være easy peasy nå
let isStraightFlush hand =
    isSameSuit hand && isSequence hand

// I fourOfAKind kan du kanskje få bruk for List.groupBy?
let isFourOfAKind hand = 
    hand
    |> List.groupBy (fun (Card (_, face)) -> face)
    |> List.exists (fun (_, cards) -> List.length cards = 4)


printfn "royalFlushHand is royal flush: %b" (isRoyalFlush royalFlushHand)
printfn "straightFlushHand is straight flush: %b" (isStraightFlush straightFlushHand)
printfn "straightFlushHand is royal flush: %b" (isRoyalFlush straightFlushHand)
printfn "fourOfAKindHand has four of a kind: %b" (isFourOfAKind fourOfAKindHand)
printfn "fourOfAKindHand is royal flush: %b" (isRoyalFlush fourOfAKindHand)
printfn "fourOfAKindHand is straight flush: %b" (isStraightFlush fourOfAKindHand)