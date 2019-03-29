
type Mat = Brød of int | Fisk of int

Brød 5

type KanskjeMat = Mat option

None

Some (Brød 5)

(1, 2)

type Par = int * int

('a', 2, "hei")

type RarTing = char * int * string

type Person = {
    Navn: string
    Alder: int
}

let p1 = { Navn = "Jens"; Alder = 27 }
let p2 = { Navn = "Frida"; Alder = 42 }
let p3 = { Navn = "Harry"; Alder = 32 }

// nye record
let p4 = p3
let p5 = {p3 with Alder = 22}
let p6 =  {p3 with Alder = 32}

let persons = [p1; p2; p3]

List.filter (fun p -> p.Alder > 30) persons

persons
|> List.map (fun p -> p.Alder)
|> List.sum 