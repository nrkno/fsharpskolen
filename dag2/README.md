   <!-- class: center, middle -->

# Fsharpskolen
## Dag 2

---

# En slags agenda
* Lister - [oppgaver](lister)
* Pattern matching - [oppgaver](pattern-matching)
* Discriminated unions - [oppgaver](discriminatedUnions)
* Records - [oppgaver](records)
* Moduler og innkapsling - [eksempler](modules)
* DDD

---

# Oppvarming
## Oppgaven intersperse-list
* Lag funksjonen 'intersperse
* Funksjonen skal ta et element 'value' og en liste av elementer av samme type
* Funksjonen skal produsere en ny liste med 'value'-elementet spredd innimellom elementene i den opprinnelige listen.

```fsharp
intersperse 0 [1 .. 5] -> [1;0;2;0;3;0;4;0;5]
```

```fsharp
intersperse "foo" [ "hello"; "dear"; "friends" ] -> [ "hello"; "foo"; "dear"; "foo"; "friends" ]
```

```fsharp
intersperse [] [ []; []; [] ] -> [ []; []; []; []; [] ]
```

---

# Discriminated unions og pattern matching

---

# Records

---

# Moduler

---

# DDD

---

# Bygge domenet ved å sette sammen typer

kredittkorteksempelet fra Wlaschin

---

# Patterns

* Enkle verdier
* Kombinasjoner av verdier med records
* Valg av verider med DU
* Workflows


```fsharp
```
