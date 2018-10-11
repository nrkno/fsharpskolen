type Vector = private Pair of (float * float)

module Vector = 
    
    let create x y = Pair (x, y)
    
    let x v = match v with Pair(x, _) -> x
    let y v = match v with Pair(_, y) -> y
    
    let add v1 v2 = 
      match (v1, v2) with 
      | Pair (x1, y1), Pair (x2, y2) -> 
        Pair (x1 + x2, y1 + y2)
        
    let negate v = match v with Pair (x, y) -> Pair (-x, -y)
        
    let subtract v1 v2 = 
      match (v1, v2) with 
      | Pair (x1, y1), Pair (x2, y2) -> 
        Pair (x1 - x2, y1 - y2)
        
    let length v = match v with Pair (x, y) -> sqrt (x * x + y * y)
 