# Dag 3

## Program API

### Oppg 1: Validere programId
I endepunktet `programs/{progId}` så ønsker vi kanskje sjekke at progId er en rimelig id (feks 4 bokstaver og 8 tall) og lager en egen type for programid som vi brukere videre for å hente program i `getProgram`
* Lag en ny type `ProgramId`i domain som holder på en string
* Oppdater `Program`typen som er der med at `Id`er av type `ProgramId` i stedet for string
* Lag en funksjon i `Domain`for å validere programId, den kan godt returnere en option, `None` hvis den ikke er gyldig eller `Some ProgramId` om den finnes. Man kan bruke string-funksjoner eller regex.
* Kall den før `getProgram` i `programHandler`, og returner feks bad request fra apiet om programiden ikke er på fornuftig format. Hvis den er godkjent, fortsett videre med å hente programmet med `getProgram`, denne funksjonen må endres litt til å godta 

### Oppg 2: Bytte fra Option til Result
Ble det litt kjedelig mye pattern mathcing i `programhandler` ved å bruke option i forrige oppgave? Det er fordi option ikke er uttrykksfull nok for det vi trenger, det hadde f.eks vært fint om å kunne skille på om `None`skyldes ugyldig input eller at programmet ikke finnes. `Resultat` kan være akkurat det vi trenger.

### Oppg 3: Hente programdata fra et annet api

## Tests

### Xunit

* Assert med presision for floats og datetimes
* Parametriserte tester med inlineData eller memberData

### Expecto
https://github.com/haf/expecto

* Run as console app
* Tests and testlists
* Focused tests and lists
* Setup and tear down
