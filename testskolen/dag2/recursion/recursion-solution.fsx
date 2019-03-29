// Skriv en funksjon som gitt et tall n finner n fakultet, dvs 1 * 2 * ... * n.
// Feks er factorial 5 = 1 * 2 * 3 * 4 * 5 == 120
let rec factorial n =
    if n <= 1 then 1
    else n * factorial (n - 1)
    
printfn "Factorial 5 = 120: %d" (factorial 5)

// halerekursiv variant
let rec tailFactorial n acc =
    if n < 1 then acc
    else tailFactorial (n - 1) (acc * n)
    

// skriv en funksjon som retunrerer det n-te fibonacci-tallet
// Fibonacci tallene starter med 0 og 1, og det n-te tallet er summet av de to foregående, så sevensen blir 0 1 1 2 3 5 8 13 21 34 ..
// Det nullet tallet er 0, og feks er det sjette fibonacci tallet 8
// Hva bør skje om du prøver å kalle fibonacci med et negativt tall?
let rec fibonacci n =
    if n = 0 then 0
    else if n = 1 then 1
    else fibonacci (n - 1) + fibonacci (n - 2)
    

printfn "fibonacci 0 = 0: %d" (fibonacci 0)    
printfn "fibonacci 1 = 1: %d" (fibonacci 1)  
printfn "fibonacci 6 = 8: %d" (fibonacci 6)

    
// Se om du kan lage en funksjon som returner en liste med det n-te fibonacci tallet, og alle de foregående tallene
// Dvs lista [0; 1; 1; 2; 3; 5; 8; 13; ...]    
let fibonaccis n =
    let rec acc n res =
        if n = 0 then [0]
        else if n = 1 then res
        else
            acc (n - 1) (((List.item 0 res) + (List.item 1 res)) :: res)
    List.rev (acc n [1; 0])

let fibonaccis2 n =
     let rec recur n x y lst =
         if n <= 1 then lst
         else recur (n - 1) (x + 1) (y + 1) (lst @ [ x+ y])
     
     if n = 0 then [0]
     else if n = 1 then [0; 1]
     else recur n 0 1 [0; 1];

let rec fibonaccis3 n =
    if n = 0 then [0]
    else if n = 1 then [0; 1]
    else (fibonaccis3 (n - 1)) @ [ fibonacci n]
    
printfn "fibonaccis 0 = [0]: %A" (fibonaccis 0)
printfn "fibonaccis 1 = [0; 1]: %A" (fibonaccis 1)
printfn "fibonaccis 6 = [0; 1; 1; 2; 3; 5; 8]: %A" (fibonaccis 6)

    
