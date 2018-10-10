open System

type UsageRightRegion = 
  | NRK
  | Norway
  | Nordics
  | World
  
type UsageRightInterval = 
  { From : DateTimeOffset 
    Until : DateTimeOffset }
  
type UsageRightDuration = 
  | Perpetual 
  | Since of DateTimeOffset
  | Interval of UsageRightInterval 
  
type UsageRight = 
  { Region : UsageRightRegion 
    Duration : UsageRightDuration }
    
let evaluateRightsAtTime (instant : DateTimeOffset) (usageRight : UsageRight) : UsageRightRegion option = 
  match usageRight with 
  | { Region = region; Duration = duration } -> 
    match duration with 
    | Perpetual -> Some region 
    | Since from ->
      if from <= instant then Some region else None
    | Interval { From = from; Until = until } ->
      if from <= instant && instant <= until then Some region else None

let instant year month day = 
  new DateTimeOffset(year, month, day, 00, 00, 00, TimeSpan.Zero)
      
{ Region = Norway; Duration = Perpetual } 
|> evaluateRightsAtTime (instant 2018 01 01)
|> printfn "%A"

{ Region = World
  Duration = Interval { From = instant 2018 01 01; Until = instant 2018 12 31 } }
|> evaluateRightsAtTime (instant 2018 03 10)
|> printfn "%A"

{ Region = World
  Duration = Interval { From = instant 2018 01 01; Until = instant 2018 12 31 } }
|> evaluateRightsAtTime (instant 2019 01 01)
|> printfn "%A"
