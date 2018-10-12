// Denne oppgaven handler om funksjoner som komponeres med seg selv! 

// Den er en ganske hard test på hvor komfortabel du er med to konsepter: 
// rekursjon og funksjonskomposisjon. Det er helt OK dersom svaret er 
// "ikke spesielt komfortabel"! Dersom du er usikker kan det være lurt 
// å sjekke ut 'men-si-meg'-artikkelen om funksjonskomposisjon fra dag 1.

// To funksjoner f og g kan komponeres til en ny funksjon med >> dersom f 
// returnerer samme type som g forventer som inputparameter.

// Et morsomt case er funksjoner som returnerer samme type som de konsumerer, 
// det vil si funksjoner av typen 'a -> 'a. Hvorfor er sånne funksjoner 
// morsomme? Fordi de kan komponeres med seg selv!

// Et par enkle eksempler på "selv-komponerbare" funksjoner:

let inc x = 1 + x
let leadingDot s = "." + s

// Du kommer sikkert på flere selv!

// Det spesielle med disse funksjonene er altså at de kan komponeres med seg selv.
// For eksempel vil komposisjonen av inc og inc gi en funksjon inkrementerer 
// et heltall med to. Men det virkelig morsomme er at det ikke stopper der.
// Funksjonen (inc >> inc) har samme type som inc, og kan følgelig komponeres
// med inc! Så vi kan lage funksjoner som dette:

let add5 = inc >> inc >> inc >> inc >> inc 
let leadingEllipsis = leadingDot >> leadingDot >> leadingDot 

7 |> add5 |> printfn "%d"
"hello" |> leadingEllipsis |> printfn "%s"

// Som du ser kan vi gjøre dette så mange ganger vi bare vil!

// Så her kommer (omsider!) oppgaven: 
// Lag en funksjon 'times' som forventer et heltall n og en funksjon fn 
// og lager en ny funksjon som har samme effekt som å kalle fn n ganger.
// Tips: komponer funksjonen med seg selv n-1 ganger. 

let times (n : int) (fn : 'a -> 'a) : 'a -> 'a = fn

// Merk at funksjonen (times 1 fn) skal ha samme oppførsel som fn.
// For eksempel bør dette gi oss true:

let inc' = times 1 inc
inc 5 = inc' 5 |> printfn "%b"

// Noe å gruble på: hva slags oppførsel bør (times 0 fn) ha?
   
let add5' = times 5 inc  
let leadingEllipsis' = leadingDot |> times 3

7 |> add5' |> printfn "%d"
"hello" |> leadingEllipsis' |> printfn "%s"

7 |> times 0 inc |> printfn "%d"
"hello" |> times 0 leadingDot |> printfn "%s"

// Vil du ha vondt i hodet? Klart du vil!
// Funksjonen 'times' har type int -> ('a -> 'a) -> ('a -> 'a)
// La oss definere en ny funksjon 'thrice':

let thrice<'a> : ('a -> 'a) -> ('a -> 'a) = times 3

// Jeg har lagt på en generisk typeparameter <'a> for å si til 
// F#-kompilatoren at det er riktig at thrice er en generisk funksjon.
// Uten denne parameteren får jeg en plagsom "value restriction"-feil.

// Som du ser er 'thrice' en funksjon som forventer en parameter av type
// 'a -> 'a (en funksjon) og returnerer en verdi av samme type 'a -> 'a.
// Det betyr at thrice kan komponeres med seg selv!

let fn1 = (thrice >> thrice >> thrice) inc 
let fn2 = inc |> times 3 thrice 
let fn3 = inc |> thrice thrice

// Hva gjør disse funksjonene? 
// Vel, de tar et heltall og returnerer et heltall i hvert fall!
// Og de gjør akkurat det samme!

7 |> fn1 |> printfn "%d"
7 |> fn2 |> printfn "%d"
7 |> fn3 |> printfn "%d"
