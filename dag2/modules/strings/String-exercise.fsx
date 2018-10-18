// I F# finnes det en del hjelpefunksjoner for å jobbe med tekststrenger i String-modulen.
// (I tillegg kan du naturligvis bruke alle metodene på String-klassen.)
// Men hva gjør du hvis du ønsker deg en hjelpefunksjon som ikke finnes i String-modulen? 
// Du utvider String-modulen! Det gjør du ganske enkelt ved å lage en ny modul som også heter String.

module String = 

    // Funksjonen substring bør fungere tilsvarende som instansmetoden Substring på String-klassen.
    let substring (start : int) (stop : int) (s : string) = s
  
    // Funksjonen crop skal sørge for at en tekststreng ikke er lengre enn en gitt makslengde.
    // F.eks: crop 4 "kokkelue" -> "kokk"
    let crop (maxlength : int) (s : string) = s

    // Funksjonen undup skal fjerne etterfølgende duplikater av tegn.
    // F.eks: undup "lappeteppe" -> "lapetepe"
    let undup (s : string) = s
    
    // Funksjonen reverse skal gi en tekststreng som inneholder tegnene i input-strengen i motsatt rekkefølge.
    // (Merk at reverse >> reverse bør gi den opprinnelige tekststrengen!)
    // F.eks: reverse "langøre" -> "erøgnal"
    let reverse (s : string) = s

// Dette burde gi... hva da?   
"hello world" 
|> String.substring 1 9     // ello worl
|> String.crop 4            // ello
|> String.undup             // elo
|> String.reverse           // ole
|> printfn "%s"

// Hvordan lager du en okse av en hestesko? Finner du mer enn en måte?
