# Et ganske ordentlig prosjekt

Solutionen her består av to prosjekter, ConsoleApp og testprosjektet Tests.

Vi har lyst til å skrive tester, men trenger først å legge til avhengighet til `Xunit` i Tests-prosjektet. Bruk `paket` til å legge til denne avhengigheten
```
.paket\paket.exe add xunit --version 2.4.1 --project Tests
```

Gå til fila `ContributorTests.fs`, her skal vi etterhvert skrive tester for Contributors, men aller først skal vi bare teste at Xunit virker og at vi klarer å skrive og kjøre vår første test.

Testfunksjonene må annoteres med `[<Fact>]`og være på formen
```
[<Fact>]
let ``En pluss en er to``() =
  Assert.Equal (2, 1 + 1)
```
Skriv en test i testfila, og se at du kan kjøre en fra IDEen din. Endre på asserten så testen feiler, og prøv å kjøre den på nytt.

Nå kan du kopiere over koden du skrev i stad for Contributors over til ConsoleApp-prosjektet, og fortsette med å skrive noen tester for å verifsere at koden fungerer som forventet. Prøv å bruke ulike Assert-typer, feks å asserte at funksjonen kaster exception.


