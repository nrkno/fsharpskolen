module ProgramApi.Dto

open NodaTime
open NodaTime.Text

type ContributorDto = {
    Role : string 
    Name : string 
}

type LegalAgeDto = {
    Code : string 
    Description : string
    Label : string
}

type TransmissionDto = {
    Channel : string
    EpgDate : string
    StartTime : string
    EndTime : string
}

type PlayableDto = {
    Manifest : string 
    Subtitles : string 
}

type IndexPointDto = {
    StartPoint : string 
    Title : string 
}

type PlaybackElementDto = {
    Playable : PlayableDto  
    Duration : string 
    IndexPoints : IndexPointDto list 
}

type RestrictedPlaybackElementDto = 
    | Unavailable 
    | Available of PlaybackElementDto 
   
type Program = {
    ProgId : string
    Title : string 
    Description : string 
    Contributors : ContributorDto list 
    LegalAge : LegalAgeDto option 
    PlannedDuration : string option 
    Transmissions : TransmissionDto list 
    PlaybackElement : RestrictedPlaybackElementDto 
}

module Program = 

    let toDurationString (duration : Duration) : string = 
        let durationRoundTripPattern = "-H:mm:ss.FFFFFFFFF";
        DurationPattern
            .CreateWithInvariantCulture(durationRoundTripPattern)
            .Format(duration)

    let toLocalDateString (localDate : LocalDate) : string = 
        LocalDatePattern.Iso.Format(localDate)

    let toInstantString (instant : Instant) : string = 
        InstantPattern.ExtendedIso.Format(instant)

    let fromDomainContributor (contributor : Domain.Contributor) : ContributorDto = 
        match contributor with 
        | { Role = Domain.Role r; Name = Domain.Name n } -> { Role = r; Name = n } 

    let fromDomainLegalAge (legalAge : Domain.LegalAge) : LegalAgeDto option = 
        let fromDomainRating r = 
            match r with 
            | Domain.RatingA -> { Code = "A"; Description = "Tillatt for alle"; Label = "A" }
            | Domain.Rating6 -> { Code = "6"; Description = "Anbefalt de over 6 år"; Label = "6+" }
            | Domain.Rating9 -> { Code = "9"; Description = "Anbefalt de over 9 år"; Label = "9+" }
            | Domain.Rating12 -> { Code = "12"; Description = "Anbefalt de over 12 år"; Label = "12+" }
            | Domain.Rating15 -> { Code = "15"; Description = "Anbefalt de over 15 år"; Label = "15+" }
            | Domain.Rating18 -> { Code = "18"; Description = "Anbefalt de over 18 år"; Label = "18+" }
        match legalAge with 
        | Domain.Exempt -> None 
        | Domain.Rated r -> Some <| fromDomainRating r

    let fromDomainTransmission (transmission : Domain.Transmission) : TransmissionDto = 
        match transmission with 
        | { ChannelId = Domain.ChannelId channelId
            EpgDate = epgDate 
            ActualStartTime = startTime 
            ActualEndTime = endTime } -> 
          { Channel = channelId 
            EpgDate = toLocalDateString epgDate 
            StartTime = toInstantString startTime 
            EndTime = toInstantString endTime }

    let hasRightsNow (usageRights : Domain.UsageRights) : bool = 
        let now = SystemClock.Instance.GetCurrentInstant().InUtc().LocalDateTime.Date
        match usageRights with 
        | { Region = _; Duration = usageRightsDuration } ->
          match usageRightsDuration with 
          | Domain.NoUsageRights -> false
          | Domain.PerpetualUsageRights -> true
          | Domain.UsageRightsSince since -> now >= since 
          | Domain.UsageRightsUntil until -> now <= until 
          | Domain.UsageRightsBetween { RightsStart = start; RightsExpire = expire } ->
            start <= now && now <= expire

    let fromDomainPlayable (playable : Domain.Playable) : PlayableDto = 
        match playable with 
        | { Manifest = manifest
            Subtitles = subtitles } -> 
          { Manifest = manifest.Url.OriginalString 
            Subtitles = subtitles.Url.OriginalString }    

    let fromDomainIndexPoint (indexPoint : Domain.IndexPoint) : IndexPointDto = 
        match indexPoint with 
        | { Id = _ 
            Title = Domain.IndexPointTitle title 
            Description = _ 
            Contributors = _ 
            StartPoint = startPoint } -> 
          { StartPoint = toDurationString startPoint
            Title = title }

    let fromDomainPlaybackElement (playbackElement : Domain.PlaybackElement) : RestrictedPlaybackElementDto = 
        match playbackElement with 
        | { Playable = playable 
            UsageRights = usageRights 
            ActualDuration = duration 
            IndexPoints = indexPoints } -> 
          if hasRightsNow usageRights then
            let element = { Playable = fromDomainPlayable playable
                            Duration = toDurationString duration 
                            IndexPoints = List.map fromDomainIndexPoint indexPoints } 
            Available element 
          else 
            Unavailable 

    let fromDomainProgram ( program : Domain.Program ) : Program = 
        match program with
        | { ProgId = Domain.ProgId progId 
            Metadata = { Title = Domain.ProgramTitle title  
                         Description = Domain.ProgramDescription description 
                         Contributors = contributors 
                         LegalAge = legalAge
                         PlannedDuration = plannedDuration }
            Transmissions = transmissions 
            PlaybackElement = playbackElement } -> 
          { ProgId = progId 
            Title = title 
            Description = description 
            Contributors = List.map fromDomainContributor contributors
            LegalAge = fromDomainLegalAge legalAge 
            PlannedDuration = Option.map toDurationString plannedDuration
            Transmissions = List.map fromDomainTransmission transmissions
            PlaybackElement = fromDomainPlaybackElement playbackElement }
