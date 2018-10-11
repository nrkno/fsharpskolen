# Hvordan fungerer funksjoner i F#?

Her er noen funksjonsdefinisjoner, som alle er ekvivalente: 

```fsharp 
let add x y = x + y
let add x = fun y -> x + y 
let add = fun x -> fun y -> x + y 
let add = (+)
```

I F# har alle funksjoner én inputparameter og én returverdi. Dette er annerledes enn i C#, der metoder har en parameter-liste som kan inneholde `0` til `n` parametre. 

Alle variantene av `add` over tar en inputparameter av typen `int` og returnerer en funksjon av typen `int -> int`. Syntaksen som lar oss skrive `let add x y = x + y` er bare en konsis måte å skrive det ekvivalente `let add = fun x -> fun y -> x + y`. Den "sukkerfrie" måten å skrive det på viser at vi egentlig gjør to ting: vi skriver et uttrykk som angir en funksjonsverdi (`fun x -> fun y -> x + y`) og vi binder et navn (`add`) til det uttrykket. 

# Partial application 

Man kan tenke på det som at `add` "logisk sett" har to parametre, men at man kan gi parametrene en og en. Noen ganger er det hensiktsmessig å gi inn bare den første verdien til `add`. Dette kalles "partial application", fordi vi ikke gjør en "full" evaluering av `add`. Vi er liksom ikke "ferdige". 

Hvis vi gir ett heltall (og ikke to) til `add` får vi ikke en heltallsverdi men en ny funksjon som vi kan gjøre hva vi vil med. Vi kan evaluere den (hvis vi gir den et heltall til), men vi kan også navngi den, sende den til andre funksjoner osv osv. 

Her er et eksempel på at vi navngir den:

```fsharp
let inc = add 1 
```

Dette gir oss altså en ny funksjon `inc` med type `int -> int`. Hvis vi gir `inc` et heltall får vi som svar et tall som er `1` større enn heltallet vi ga inn. 

Definisjonen av `inc` som bruker partial application av `add` er helt ekvivalent med følgende kode:

```fsharp
let inc n = add 1 n 
```

Det finnes et lite "triks" du kan bruke dersom du lurer på om du kan bruke partial application i en funksjonsdefinisjon der du kaller en annen funksjon. Tenk på det som at du kan "stryke" parametre som er like på slutten av venstre og høyre side av likhetstegnet i funksjonsdefinisjonen din. I tilfellet over finner vi `n` på slutten av `inc n` og `add 1 n` og kan dermed sløyfes. Det gir oss definisjonen med partial application over. 

## Partial application i C#

Er det mulig å gjøre partial application i C#? Ja, alt går an hvis man er tålmodig. 

Vi bruker som regel Func'er for å angi typen til en lambda i C#.

Funksjoner som tar én parameter kalles "curried", etter matematikeren Haskell Curry. For å gjøre partial application i C# må man altså operere på lambdaer som er "curried". Det betyr at vi bare kan bruke Func'er av typen `Func<T1, T2>` - `Func<T1, T2, T3>` osv er ikke "curried".

Hvordan ser en "curried" `add` ut i C#? 

```csharp
void Main()
{
	Func<int, Func<int, int>> add = x => y => x + y;
	var inc = add(1);
	var result = inc(5);
	Console.WriteLine(result);
}
```

Definisjonen av `Func<int, Func<int, int>> add = x => y => x + y` ser kanskje ikke så pen ut på grunn av de nøstede Func'ene, men ellers er den til forveksling lik den usukrede F#-varianten, `let add = fun x -> fun y -> x + y`.

Men hva om du ikke har en fin og "curried" lambda av typen `Func<T1, T2>`? Det går an å lage extension-metoder for så mangt! 

```csharp
void Main()
{
	Func<int, int, int> add = (x, y) => x + y;
	var curriedAdd = add.Curry();
	var inc = curriedAdd(1);
	var result = inc(5);
	Console.WriteLine(result);
}

public static class CurryExtensions
{
	public static Func<T1, Func<T2, TR>> Curry<T1, T2, TR>(this Func<T1, T2, TR> f)
	{
		return a => b => f(a, b);
	}
}
```

Her har vi altså en mer tradisjonell `add` i C#-land, som må curries før vi kan gjøre partial application. `Curry`-metoden lager en `Func<int, Func<int, int>>` av en `Func<int, int, int>`-lambdaen vår.

Hva om du har en Func med enda flere type-parametre sier du? Heldigvis finnes det meste på Internett. En fullstendig variant finnes her: https://gist.github.com/einarwh/5f4768fe720cfa22ac0a9a1d0b59926e