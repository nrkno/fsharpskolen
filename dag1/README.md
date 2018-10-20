<!-- class: center, middle -->

# Fsharpskolen
## Dag 1

---

# En slags agenda
* Funksjoner - [oppgaver](functions)
* Rekursjon
* Lister - [oppgaver](lister)
* Options
* Discriminated unions
* Records

---

# Hei Fsharp!
* Multiparadigmespråk; funksjonelt og objektorientert
* Startet som en implementasjon av OCaml
* Sterkt typet, typeinferens
* Bruker innrykk til å gruppere kodelinjer

---

# Primitive datatyper
```fsharp
1 + 2
```

```fsharp
false
```

```fsharp
"Hei F#"
```

```fsharp
'f'
```

```fsharp
()
```
---

# Definisjoner
```fsharp
let i = 1
```

```fsharp
let tekst = "Hei"
```

```fsharp
let f x y = x + y
```

```fsharp
let x, y = (1, 2)
```
---

# If

```fsharp
if x = y then "equals"
else if x < y then "is less than"
else "is greater than"
```
Sammenligning:

```fsharp
=, <, <=, >, >=, <>
```

Bolske operatorer: 
```fsharp
&&, ||, not
```
---

# Funksjoner
<table>
    <tr>
        <td>let add x y = x + y</td><td>int -> int -> int</td>
        </tr>
        <tr>
        <td>let addt (x, y) = x + y</td><td>int * int -> int</td>
        </tr>
        <tr>
        <td>let add1 x = 1 + x</td><td>int -> int</td>
        </tr>
        <tr>
        <td>let add1p = (+) 1</td><td>int -> int</td>
        </tr>
        <tr>
        <td>x |> add 1</td><td>int -> int</td>
        </tr>
        <tr>
        <td>add 1 >> (*) 2</td><td>int -> int</td>
    </tr>
</table>

---

# Rekursjon

```fsharp
let rec factorial n =
   if n = 0 then 1
   else n * factorial (n - 1)
```

```fsharp
let tailFactorial n =
   let rec factorial n acc =
       if n = 0 then acc
       else factorial (n - 1) (acc * n)
   factorial n 1
```
---

# Lister

```fsharp
[1; 2; 3; 4]
```

```fsharp
1 :: [2; 3; 4]
```

```fsharp
[1; 2] @ [3; 4]
```

```fsharp
[ for i in [1; 2; 3] do
    for j in [4; 5; 6] do
      yield i + j ]
```
---

# Høyere ordens funksjoner

## C# Linq
```csharp
new int[] { 1, 2, 3, 4 }
.Select(n => n * n)
.Where(n => n > 3);
```

## F#
```fsharp
[1; 2; 3; 4]
|> List.map (fun n -> n * n) 	
|> List.filter (fun n -> n > 3)
```
---

# Options
Some(verdi) eller None


```fsharp
let exists (x : int option) =
    match x with
    | Some(x) -> true
    | None -> false
```
I stedet for null!

---

# Records og tupler

```fsharp
type Point = { X: float; Y: float; Z: float }
```

```fsharp
let point = { X = 1.5; Y = 0.2; Z = 3.4}
```

```fsharp
let newPoint = { point with Z = 1.0}
```
---

# Discriminated union
```fsharp
type ProgId = ProgId of string
```
```fsharp
type Fruit = 
| Apple
| Banana
| Pear 
```
```fsharp
type Contact = Email of string | Phone of int
```
```fsharp
type Shape =
| Rectangle of width : float * length : float
| Circle of radius : float
| Prism of width : float * float * height : float
```
---

# Pattern matching

```fsharp
match shape with
| Rectangle (width, height) -> width * height
| Circle (radius) -> Math.PI * radius * radius
```

```fsharp
type Point3D = { X: float; Y: float; Z: float }

let evaluatePoint (point: Point3D) =
    match point with
    | { X = 0.0; Y = 0.0; Z = 0.0 } -> printfn "Point is at the origin."
    | { X = xVal; Y = 0.0; Z = 0.0 } -> printfn "Point is on the x-axis. Value is %f." xVal
```

```fsharp
let a, b = (1, 2)
```

```fsharp
let print (ProgId(id)) = printfn "ProgId %s" id
```
---

# Objekter og interfacer
```fsharp
type IAnimal =
   abstract member Sound : string
   abstract member Legs : int

type Dog(name) =
   member __.Name = name

   interface IAnimal with
       member __.Sound = "Voff"
       member __.Legs = 4

let fido = Dog("Fido")
```
