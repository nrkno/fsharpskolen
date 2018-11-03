   <!-- class: center, middle -->

# Fsharpskolen
## Dag 3

---

# En slags agenda
* Discriminated unions - [oppgaver](discriminated-unions)

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

