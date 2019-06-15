# Dag 3

## Program API

### Oppg 1: Validere programId
I endepunktet `programs/{progId}` så ønsker vi kanskje sjekke at progId er en rimelig id (feks 4 bokstaver og 8 tall) og lager en egen type for programid som vi brukere videre for å hente program i `getProgram`
* Lag en ny type `ProgramId`i domain som holder på en string
* Lag en funksjon i `Domain`for å validere programId, den kan godt returnere en option, `None` hvis den ikke er gyldig eller `Some ProgramId` om den finnes. Man kan bruke string-funksjoner eller regex.
* Kall den før `getProgram` i `programHandler`, og returner feks bad request fra apiet om programiden ikke er på fornuftig format. Hvis den er godkjent, fortsett videre med å hente programmet med `getProgram`, denne funksjonen må endres litt for å godta den nye typen.
* Ved at `getPrograms`nå må ha et argument av typen `ProgramId`, og den eneste funksjonen som gir en programId er valideringsfunksjonen sikrer vi at man ikke går rett til å hente programmet uten å først ha validert inputverdien. Men det er ikke helt bombesikkert før `ProgramId` er privat, så det bare er i `Domain` man kan lage programIder. Prøv gjerne å lage den private, og lag en funksjon i `Domain`som lar deg hente ut verdien i `ProgramId` når du trenger den.

### Oppg 2: Bytte fra Option til Result
Ble det litt kjedelig mye pattern mathcing i `programhandler` ved å bruke option i forrige oppgave? Det er fordi option ikke er uttrykksfull nok for det vi trenger, det hadde vært fint om å kunne skille på om `None`skyldes ugyldig input eller at programmet ikke finnes. `Resultat` kan være akkurat det vi trenger.
* Vi trenger å lage en egen feiltype som er en discriminated union av de to mulige feilsituasjonene ugyldig program id og at program ikke finnes, lag en type for det i `Domain`.
* Endre valideringsfunksjonen fra forrige oppgave og `getPrograms` til å returnere `Result` i stedet for `Option`
* Oppdater `programHandler`, nå kan man starte med iden som kommer inn til funksjonen og pipe den gjennom validering og henting av program. og man kan klare seg med en match til slutt av resultatet.

### Oppg 3: Hente programdata fra et annet api
Det hadde jo vært fint `getProgram` kunne gitt programmer med mer realistisk data som vi slapp å finne på selv, så la oss prøve å hente informasjon om programmer fra et annet api, feks fra [program endepunktet](http://psapi3-webapp-prod-we.azurewebsites.net/swagger/ui/index#/Program) i PsApi. 
* Sikkert greit å la en ny modul i en ny fil for å kalle apiet og returnere en dto-type med verdiene vi vil hente fra apiet
  * Lag en type ProgramDto som inneholder de feltene vi ønsker å hente ut fra json responsen i endepunktet. La oss si at vi er interessert i å hente ut sourceMedium, title og shortDescription. Hvis dto er en record type der feltene har samme navn som feltene har i responsen fra endepunktet kan vi enkelt deserialisere til dtoen
  * Deserialisering kan vi gjøre med `NewtonSoft.Json`
  ```JsonConvert.DeserializeObject<ProgramDto>(json)```
  * For å gjøres selve kallet kan vi bruke httpklienten som er innebygd i `System.Net.Http`:
  ```
  let httpClient = new HttpClient()
  httpClient.BaseAddress <- Uri "http://psapi3-webapp-prod-we.azurewebsites.net"
  ```
  * For å gjøre selve kallet trenger man litt "greier", det kan for eksempel se sånn her ut
  ```
  let executeRequest id =
    async {
        let request = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, (sprintf "programs/%s" id))
        let! response = httpClient.SendAsync(request) |> Async.AwaitTask
        let! stream = response.Content.ReadAsStreamAsync() |> Async.AwaitTask
        let reader = new StreamReader(stream, Encoding.UTF8)
        let content = reader.ReadToEnd()
        return (content, response.StatusCode)
    }
    |> Async.RunSynchronously
  ```
  * Til slutt trenger vi funksjonen som vi vil kalle fra `getPrograms`, denne funksjonen kan ta inn programid og returnere en `Result` med dtoen hvis kallet gikk bra, og med feil hvis kallet feilet. Kanskje du vil håndtere not found annerledes enn om apiet er nede eller returnerer andre typer feil? I såfall må du kanskje legge til flere varianter i feiltypen vil laget tidligere
* Deretter kan vi i `getPrograms` bruke den nye funksjonen

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
