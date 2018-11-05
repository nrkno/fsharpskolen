module Program

open System

// ProgId er en id for et program, og består av 12 tegn, de fire første tegnene er bokstaver, de 8 siste er tall
// Definer typen ProgId, og bruk "modul-strategien" til å gjøre det umulig å lage en ugyldig progid
// Gyldig progId: MSPO40820417, MSUI13008317
// Ugyldig progId: MSUI1300831712, MSUI130O8317, M5UI13008317

type ProgId = private ProgId of string

module ProgId = 
    let create str =
        let onlyDigits = Seq.forall Char.IsDigit
        let onlyLetters = Seq.forall Char.IsLetter
        if (String.length str = 12 && str.Substring(0, 4) |> onlyLetters && str.Substring(4, 8) |> onlyDigits) 
        then
            ProgId str
        else raise (ArgumentException "Illegal progId")


printfn "%A" (ProgId.create "MSPO40820417")
printfn "%A" (ProgId.create "MSUI130O8317")
