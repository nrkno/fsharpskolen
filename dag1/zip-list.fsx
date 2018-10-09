// Denne oppgaven handler om zipping. 
//
// .NET-rammeverket har en extension-metode som heter Zip,
// https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip
// Zip tar to enumerables og en Func som parvis slår sammen verdier fra dem.
// Det er litt krøkkete å forklare, bedre å vise med eksempler:
// var lst1 = new List<int> { 1, 2, 3, 4 };
// var lst2 = new List<int> { 2, 3, 4, 5 };
// Enumerable.Zip(lst1, lst2, (a, b) => a + b); -> { 3, 5, 7, 9 }
// Enumerable.Zip(lst1, lst1, (a, b) => a * b); -> { 1, 4, 9, 16 }
// Det er et par ting å merke seg med Zip. 
// For det første: 
// Når man bruker Enumerable.Zip må man alltid spesifisere hva hvert par skal bli til.
// Dvs man må spesifisere Func'en som gir en mapper de to parvise verdiene til en tredje verdi.
// For det andre:
// Dersom listene er av ulik lengde gjør Zip sitt beste, og lager så mange par som mulig.
// Dvs:
// var lst1 = new List<int> { 1, 2 };
// var lst2 = new List<int> { 2, 3, 4, 5 };
// Enumerable.Zip(lst1, lst2, (a, b) => a + b); -> { 3, 5 }
// 
// List-modulen i F# har også en zip, en funksjon som produserer en liste av par.
// Den oppfører seg litt annerledes. 
// For det første:
// List.zip produserer bare par som tupler og gir ingen mapping.
// Dersom du vil mappe om til noe annet vil du typisk kalle List.map etter List.zip.
// For det andre:
// Listene må være like lange. List.zip kaster ArgumentException dersom listene er av ulik lengde.
// let lst1 = [1; 2; 3; 4];
// let lst2 = [2; 3; 4; 5];
// List.zip lst1 lst2 -> [(1,2); (2,3); (3,4); (4,5)]
// let lst0 = [1; 2];
// List.zip lst0 lst2 -> ArgumentException
// 
// Oppgaven er å implementere begge to varianter av zip i F#:
//
// Funksjonen zipLenient skal tillate lister av ulik lengde, og gjøre det beste ut av det.
// let zipLenient (xs : 'a list) (ys : 'b list) : ('a * 'b) list = []
//
// Eksempler:
// zipLenient ['a' .. 'c'] [1..3] |> printfn "%A" -> [('a', 1); ('b', 2); ('c', 3)]
// zipLenient ['a' .. 'c'] [1..5] |> printfn "%A" -> [('a', 1); ('b', 2); ('c', 3)]
//
// Funksjonen zipStrict skal kaste ArgumentException dersom listene er av ulik lengde.
// let zipStrict (xs : 'a list) (ys : 'b list) : ('a * 'b) list = []
//
// Eksempler:
// zipStrict ['a' .. 'c'] [1..3] |> printfn "%A" -> [('a', 1); ('b', 2); ('c', 3)]
// zipStrict ['a' .. 'c'] [1..5] |> printfn "%A" -> ArgumentException
//
//
// Om du vil kan du lage en tredje variant, tryZip, med følgende signatur:
// let tryZip (xs : 'a list) (ys : 'b list) : ('a * 'b) list option = []
//
// Den er litt mer fiklete å få til, i hvert fall hvis du ikke jukser og kaller zipStrict.
// Eksempler:
// tryZip ['a' .. 'c'] [1..3] |> printfn "%A" -> Some [('a', 1); ('b', 2); ('c', 3)]
// tryZip ['a' .. 'c'] [1..5] |> printfn "%A" -> None
