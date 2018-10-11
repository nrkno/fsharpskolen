type Vector = private { x : float; y : float }

module Vector = 
    
    let create x y = { x = x; y = y }
    
    let x v = v.x

    let y v = v.y
    
    let add v1 v2 = 
      { x = v1.x + v2.x
        y = v1.y + v2.y }
        
    let negate v = 
      { x = -v.x 
        y = -v.y }
        
    let subtract v1 v2 = 
      { x = v1.x - v2.x
        y = v1.y - v2.y }
        
    let length v = 
      sqrt (v.x * v.x + v.y * v.y)
      