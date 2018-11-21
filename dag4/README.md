<!-- class: center, middle -->

# Fsharpskolen
## Dag 4

---

# Funksjoner

* Funksjoner er over alt
* Som input, parametre eller output til andre funksjoner
* Composisjon
* Currying/partial application
* Totale funksjoner

---

# Repetisjon

* Høyere ordens funksjoner, feks List.map
* Result bind og map

---

# Teknikker fra boka

* Funksjonsadaptere
* Håndtere avhengigheter - composition root
* Kjede Result<'t, 'error>
* Serialisering av domeneobjekter

# Oppgaver
* Bygg og kjør prosjektet og se at du får opp en webserver som svarer på `http://localhost:5000`
* Datafilene under files/metadata inneholder informasjon om medvirkende (`Contributors`), men de kommer ikke ut i responsen. Gjør de nødvendige endringene til å få med medvirkende ut i responsen.
* Oppdater koden så transmissions leses fra repository og kobles inn i data som leveres med program-endepunktet
* Hvis man ser på json man får for et program kan man se at deserialisering av discriminated union ser rar ut, se om du kan fikse dette
* Hente manifest med http 
  * legge til avhengighet for en http-klient
  * ta i bruk webserveren i prosjektet `manifest-server`
* Legg på validering på `ProgId`og `Role`i domain, her kan du kanskje gjenbruke kode du skrev sist
* Legg på validering på `Contributor` så bare gyldige contributors kommer med i lista (ikke barn eller anonyme)
* Lag tester på mapping fra datamodell til domenemodell i testprosjektet

## Mer avanserte oppgaver
* Lag en compositional expression for `Result<'a, 'err>` så man slipper å kjede så mange `bind` sammen
* Skriv om funksjonen `combine` slik at den blir enklere. Er det mulig å lage en slags `map2`?

-----
