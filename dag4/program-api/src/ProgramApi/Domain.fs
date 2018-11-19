module ProgramApi.Domain

open System
open NodaTime

type ProgId = ProgId of string

type ProgramTitle = ProgramTitle of string 

type ProgramDescription = ProgramDescription of string 

type Contributor = 
    {
        Role : string 
        Name : string 
    }

type Rating = 
    | RatingA 
    | Rating6
    | Rating9
    | Rating12
    | Rating15
    | Rating18

type LegalAge = 
    | Exempt 
    | Rated of Rating 

type ProgramMetadata = 
    {
        Title : ProgramTitle 
        Description : ProgramDescription 
        Contributors : Contributor list 
        LegalAge : LegalAge 
        PlannedDuration : Duration option 
    }

type ChannelId = ChannelId of string 

type Transmission = 
    {
        ChannelId : ChannelId
        EpgDate : LocalDate
        ActualStartTime : Instant
        ActualEndTime : Instant
    }

type MediaFormat = 
    | HLS 
    | HDS 
    | Dash 

type Manifest = 
    {
        Url : Uri 
        Format : MediaFormat
    }

type SubtitlesFormat = 
    | TTML 
    | WebVTT

type LanguageCode = 
    | No 
    | En

type SubtitlesFile = 
    {
        Url : Uri 
        Format : SubtitlesFormat 
        Language : LanguageCode 
    }

type Playable = 
    {
        Manifest : Manifest 
        Subtitles : SubtitlesFile 
    }

type UsageRightsRegion = 
    | Norway 
    | World

type UsageRightsInterval = 
    {
        RightsStart : LocalDate 
        RightsExpire : LocalDate
    }

type UsageRightsDuration = 
    | Perpetual 
    | Since of LocalDate 
    | Until of LocalDate
    | Interval of UsageRightsInterval 

type UsageRights = 
    {
        Region : UsageRightsRegion 
        Duration : UsageRightsDuration
    }

type IndexPointId = IndexPointId of int 

type IndexPointTitle = IndexPointTitle of string 

type IndexPointDescription = IndexPointDescription of string

type IndexPoint = 
    {
        Id : IndexPointId 
        Title : IndexPointTitle 
        Description : IndexPointDescription 
        Contributors : Contributor list 
        StartPoint : Duration 
    }

type PlaybackElement = 
    {
        Playable : Playable 
        UsageRights : UsageRights
        ActualDuration : Duration 
        IndexPoints : IndexPoint list 
    }

type Program = 
    {
        ProgId : ProgId
        Metadata : ProgramMetadata
        Transmissions : Transmission list 
        PlaybackElement : PlaybackElement
    }
