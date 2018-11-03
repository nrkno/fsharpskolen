   <!-- class: center, middle -->

# Fsharpskolen
## Dag 2

---

# En slags agenda
* Lister - [oppgaver](lister)
* Discriminated unions and Pattern matching - [oppgaver](pattern-matching)
* Records - [oppgaver](records)
* Moduler og innkapsling - [eksempler](modules)
* DDD

---
# Oppvarming
## Oppgaven intersperse-list
* Lag funksjonen 'intersperse
* Funksjonen skal ta et element 'value' og en liste av elementer av samme type
* Funksjonen skal produsere en ny liste med 'value'-elementet spredd innimellom elementene i den opprinnelige listen.

```fsharp
intersperse 0 [1 .. 5] -> [1;0;2;0;3;0;4;0;5]
```

```fsharp
intersperse "foo" [ "hello"; "dear"; "friends" ] -> [ "hello"; "foo"; "dear"; "foo"; "friends" ]
```

```fsharp
intersperse [] [ []; []; [] ] -> [ []; []; []; []; [] ]
```

---

# Discriminated unions og pattern matching

```fsharp
type Shape =
| Rectangle of float * float
| Circle of float

let area shape = 
    match shape with
    | Rectangle (length, height) -> length * height
    | Circle radius  -> Math.PI * radius * radius
```

```fsharp
type Result<'a, 'err> = 
| Ok of 'a
| Error of 'err

type Response = { Status: int; Content: string }

let validate response = 
    if response.Status = 200 
    then Ok response.Content
    else Error "Henting av data feilet"
```
---

# Records

```fsharp
type Circle = {
    Center: float * float
    Radius: float
}
```

```fsharp
let { Center = (c1, c2); Radius = r } = circle 
```

```fsharp
let isUnitCircle circle =
    match circle with
    | { Radius = 1.0 } -> true
    | _ -> false
```

---

# Moduler
* Dele programmet i komponenter
* Lage gode abstraksjoner
* Skjule implementasjonsdetaljer

```fsharp
module MittProgram
   module EnFinModul = 
      let add1 = (+) 1
```
