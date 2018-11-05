   <!-- class: center, middle -->

# Fsharpskolen
## Dag 3

---

# En slags agenda
* Kort intro til DDD
* Bygge domenet fra enkle typer
* Moduleksempel
* ProgId [progId](progid)
* Hva er et program, i hvilken kontekst?
* Et ekte domene - [oppgaver](ddd)
* Et F#-prosjekt

---

# DDD
* Ubiquitous language
* Bounded context
* Context maps
* Context vs domene
* Value objects, entities og aggregater

---

# Bygge domenet fra enkle typer
Eksempel fra boka

```fsharp
type PhoneNumber = PhoneNumber of int
type CardNumber = CardNumber of string
```

```fsharp
type CardType = Visa | MasterCard
```

```fsharp
type CreditCardInfo = {
    CardType: CardType
    CardNumber: CardNumber
}
```

---

# Bygge domenet del 2

```fsharp
type PaymentMethod = 
| Cash
| Vipps of PhoneNumber
| Card of CreditCardInfo
```

```fsharp
type PaymentAmount = PaymentAmount of decimal
type Currency = NOK | EUR
```

```fsharp
type Payment = {
    Amount: PaymentAmount
    Currency: Currency
    Method: PaymentMethod
}
```

```fsharp
type PayInvoice = UnpaidInvoice -> Payment -> PaidInvoice
```

---

# Se mønstre i domenemodellen

* Enkle verdier
* Kombinasjoner av verdier med records
* Valg av verdier med discriminated unions
* Workflows

---

# Moduleksempelet 
## Smart constructor approach

```fsharp
type Antall = private Antall of int

module Antall = 
   let create antall = 
      if antall < 0 then 
         raise (ArgumentException "Ugyldig antall")
      else Antall antall
```

---

# ProgId

* Strenger er sekvenser Seq
* Char.IsDigit/IsLetter

---

# Hva er vel et program?

---
