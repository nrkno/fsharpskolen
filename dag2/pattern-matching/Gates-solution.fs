// Logiske porter kan implementeres på mange måter. 
// Vi kunne brukt booleans og &&, ||, not osv, men det blir veldig kjedelig.
// Så hva skal vi finne på da?
// Oppførselen til en logisk port blir gjerne definert via sannhetstabeller.
// Ved hjelp av pattern matching kan vi implementere logiske porter i F#
// nærmest ved å skrive sannhetstabellene direkte. Hvordan?

type Signal = Lo | Hi

module Gates = 
  
    let ``and`` x y = 
        match (x, y) with 
        | (Hi, Hi) -> Hi
        | _ -> Lo

    let ``or`` x y = 
        match (x, y) with  
        | (Lo, Lo) -> Lo 
        | _ -> Hi

    let nand x y = 
        match (x, y) with 
        | (Hi, Hi) -> Lo
        | _ -> Hi

    let nor x y = 
        match (x, y) with  
        | (Lo, Lo) -> Hi
        | _ -> Lo

    let xor x y = 
        match (x, y) with  
        | (x, y) when x = y -> Lo
        | _ -> Hi

    let xnor x y =
        match (x, y) with  
        | (x, y) when x = y -> Hi
        | _ -> Lo

    let not = function 
        | Hi -> Lo 
        | Lo -> Hi 

let testUnaryGate name port (lo, hi) = 
    let run (input, expected) = 
        let output = port input 
        if output = expected then None else Some <| sprintf "[%s %A]: expected %A but got %A." name input expected output
    let setup = [ (Lo, lo); (Hi, hi) ]
    let errors = List.map run setup |> List.choose id
    if List.isEmpty errors then printfn "Gate %s works as specified." name else printfn "Gate '%s' is broken.\n%s" name (String.concat "\n" errors)

let testBinaryGate name port (lolo, lohi, hilo, hihi) = 
    let run ((a, b), expected) = 
        let output = port a b 
        if output = expected then None else Some <| sprintf "[%s %A %A]: expected %A but got %A." name a b expected output
    let setup = [ ((Lo, Lo), lolo); ((Lo, Hi), lohi); ((Hi, Lo), hilo); ((Hi, Hi), hihi) ]
    let errors = List.map run setup |> List.choose id
    if List.isEmpty errors then printfn "Gate %s works as specified." name else printfn "Gate '%s' is broken.\n%s" name (String.concat "\n" errors)

testBinaryGate "and" Gates.``and`` (Lo, Lo, Lo, Hi)
testBinaryGate "or" Gates.``or`` (Lo, Hi, Hi, Hi)
testBinaryGate "nand" Gates.``nand`` (Hi, Hi, Hi, Lo)
testBinaryGate "nor" Gates.``nor`` (Hi, Lo, Lo, Lo)
testBinaryGate "xor" Gates.``xor`` (Lo, Hi, Hi, Lo)
testBinaryGate "xnor" Gates.``xnor`` (Hi, Lo, Lo, Hi)
testUnaryGate "not" Gates.not (Hi, Lo)
