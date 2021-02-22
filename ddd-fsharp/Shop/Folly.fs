module Folly
let retryStrategy (tries: int) (fragileFunc : ('a->'b)) : ('a -> 'b) =
    let rec retry_int tries x = 
        if tries <= 0 then failwith "Tom for forsøk"
        else
            try
                fragileFunc x
            with
            | _ ->
                printfn "Remaining retries: %A" tries
                retry_int (tries - 1) x
    retry_int tries

//let timeOutAndRetryStrategy (tries: int) (fragileFunc : ('a->Async<'b>)) : ('a -> 'b) =
    // venter litt mellom hver retry