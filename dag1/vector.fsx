// Denne oppgaven handler om vektorer, men aller mest om record types.
// Typen Vector er en record med to felter, x og y.
// Implementer funksjonene add, negate, subtract, scale og length. 

type Vector = 
    { x : float
      y : float }

let add (v1 : Vector) (v2 : Vector) : Vector = v1

// Eksempel:
// add { x = 1.0; y = 2.0 } { x = 2.0; y = 1.0 } -> { x = 3.0; y = 3.0 }

let negate (v : Vector) : Vector = v 

// Eksempel:
// negate { x = 1.0; y = 2.0 } -> { x = -1.0; y = -2.0 }

let subtract (v1 : Vector) (v2 : Vector) : Vector = v1

// Eksempel:
// subtract { x = 3.5; y = 2.5 } { x = 2.0; y = 1.4 } -> { x = 1.5; y = 1.1 }

let scale (f : float) (v : Vector) : Vector = v

// Eksempler:
// scale 2 { x = 1.0; y = 3.0 } -> { x = 2.0; y = 6.0 } 
// scale 0.5 { x = 1.0; y = 3.0 } -> { x = 0.5; y = 1.5 } 
// scale 1 { x = 1.0; y = 3.0 } -> { x = 1.0; y = 3.0 } 

let length (v : Vector) : float = 0.0

// Eksempler: 
// length { x = 1.0; y = 0.0 } -> 1.0
// length { x = 3.0; y = 4.0 } -> 5.0 
