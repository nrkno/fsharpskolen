# Hva er funksjonskomposisjon?

Syntaks: 

```fsharp 
let h = f >> g
```

I F# har alle funksjoner en _type_ som ser noe slik ut: `'a -> 'b`, fra en eller annen type `'a` til en eller annen type `'b`.

Hvis du for eksempel har en funksjon `lengde` som gir deg lengden av en tekststreng, så vil typen til funksjonen være `string -> int`. Hvis du har en funksjon `dobbel` som fordobler et heltall, vil typen være `int -> int`. Og hvis du til slutt har en funksjon `mye` som avgjør om et heltall er stort eller ikke, så vil typen være `int -> bool`.

Hvis du har to funksjoner som "passer sammen" typemessig, så kan du sette dem sammen, eller _komponere_ dem til en ny funksjon. Den nye funksjonen vil gi deg samme resultat som om du kaller de to funksjonene etter hverandre. 

Hva vil det si at to funksjoner "passer sammen"? Det betyr at returtypen fra den ene funksjonen er den samme som inputtypen til den andre funksjonen. Funksjonene `lengde` og `dobbel` passer sammen, fordi `lengde` returnerer en `int`, og `dobbel` forventer en `int`. Men mer at rekkefølgen er viktig - `dobbel` og `lengde` passer ikke sammen, for `dobbel` returnerer en `int` mens `lengde` forventer en `string`! Det er på en måte opplagt: du kan kalle `dobbel` på resultatet til `lengde`, men ikke `lengde` på resultatet av `dobbel`.

Hvis du ser på returtyper og inputtyper vil du se at `lengde` og `mye` passer sammen, og det samme gjør `dobbel` og `mye`. Vi kan lage en hel haug av funksjoner, med litt ulik oppførsel:

```fsharp
let lengde (s : string) : int = s.Length
let dobbel (n : int) : int = n + n
let mye (n : int) : bool = n > 20 

let fn1 = lengde >> dobbel 
let fn2 = dobbel >> mye 
let fn3 = lengde >> mye 
let alt = lengde >> dobbel >> mye

alt "hallo" |> printfn "%A"
alt "hallo verden" |> printfn "%A"
```

## Funksjonskomposisjon i C#

I C# brukes ikke funksjonskomposisjon i noen særlig grad. Men det er mulig å få det til. Vi bruker som regel Func'er for å angi typen til en lambda i C#. To lambdaer med type f.eks `Func<string, int>` og `Func<int, bool>` kan komponeres til en tredje lambda med type `Func<string, bool>`. 

Her en variant som bruker en extension-metode definert på `Func<T1, T2>`.

```csharp
void Main()
{
	Func<string, int> lengde = s => s.Length;
	Func<int, int> dobbel = x => x + x;
	Func<int, bool> mye = x => x > 20;
	var alt = lengde.Compose(dobbel).Compose(mye);
	Console.WriteLine(alt("hallo"));
	Console.WriteLine(alt("hallo verden"));
}

public static class FuncExt 
{
	public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T1, T2> f, Func<T2, T3> g) {
		return x => g(f(x));
	}
}
```