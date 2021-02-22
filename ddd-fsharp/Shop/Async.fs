module Async 
let map f computation =
    async.Bind(computation, f >> async.Return)