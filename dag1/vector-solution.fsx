type Vector = 
    { x : float
      y : float }

let add { x = x1; y = y1 } { x = x2; y = y2 } = 
    { x = x1 + x2
      y = y1 + y2 } 

let negate { x = x; y = y } = 
    { x = -x; y = -y }

let subtract { x = x1; y = y1 } { x = x2; y = y2 } = 
    { x = x1 + x2
      y = y1 + y2 } 

let scale f { x = x; y = y } = 
    { x = f * x
      y = f * y }
      
let length { x = x; y = y } = 
  x * x + y * y |> sqrt
      
{ x = 2.0; y = 3.0 } 
|> negate 
|> add { x = 1.0; y = 2.0 }
|> length 
|> printfn "%A"
