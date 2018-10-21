// Lag en funksjon 'intersperse' med signaturen under.
// Funksjonen skal ta et element 'value' og en liste av elementer av samme type
// og produsere en ny liste med 'value'-elementet spredd innimellom elementene i den opprinnelige listen.
// Et par eksempler er kanskje bedre enn ord:
// intersperse 0 [1 .. 5] -> [1;0;2;0;3;0;4;0;5]
// intersperse "foo" [ "hello"; "dear"; "friends" ] -> [ "hello"; "foo"; "dear"; "foo"; "friends" ]
// intersperse [] [ []; []; [] ] -> [ []; []; []; []; [] ]

let intersperse (value : 'a) (things : 'a list) : 'a list = []

intersperse 0 [1 .. 5] |> printfn "%A"
intersperse "foo" [ "hello"; "dear"; "friends" ] |> printfn "%A"
intersperse [] [[];[];[]] |> printfn "%A"