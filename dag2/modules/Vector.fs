module Vector = 

    type T = private { x : float; y : float }
    
    let create x y = { x = x; y = y }
    
    let x (v : T) = v.x

    let y (v : T) = v.y

    let pair (v : T) = (v.x, v.y)
    
    let add (v1 : T) (v2 : T) = 
      { x = v1.x + v2.x
        y = v1.y + v2.y }
        
    let negate (v : T) = 
      { x = -v.x 
        y = -v.y }
        
    let subtract (v1 : T) (v2 : T) = 
      { x = v1.x - v2.x
        y = v1.y - v2.y }
        
    let length (v : T) = 
      sqrt (v.x * v.x + v.y * v.y)
      