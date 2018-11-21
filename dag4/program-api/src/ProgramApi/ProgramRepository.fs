module ProgramApi.ProgramRepository

open System

open NodaTime
open NodaTime.Text

open ProgramApi.Domain
open ProgramApi.Data
open ProgramApi.Data.ManifestRepository
open ProgramApi.Data.MetadataRepository
open ProgramApi.Data.TransmissionsRepository

let mapParseResult<'a> (parseResult : ParseResult<'a>) = 
    if parseResult.Success then Ok (parseResult.Value)
    else Error (parseResult.Exception.Message)

let toInstant (iso8601DateTimeString : string) : Result<Instant, string> = 
    InstantPattern.ExtendedIso.Parse(iso8601DateTimeString)
    |> mapParseResult 

let toLocalDate (iso8601DateString : string) : Result<LocalDate, string> = 
    LocalDatePattern.Iso.Parse(iso8601DateString)
    |> mapParseResult 

let toDuration (durationRoundTripString : string) : Result<Duration, string> = 
    DurationPattern
        .CreateWithInvariantCulture("-H:mm:ss.FFFFFFFFF")
        .Parse(durationRoundTripString)
    |> mapParseResult

let toDurationOption (durationRoundTripString : string) : Result<Duration option, string> = 
    if isNull durationRoundTripString then 
        Ok None 
    else 
        toDuration durationRoundTripString |> Result.map Some 

let getUsageRightsDuration (fromDate : LocalDate) (toDate : LocalDate)
    : UsageRightsDuration = 
    if fromDate > toDate then 
        NoUsageRights 
    else
        let lowerDateBounds = LocalDate(1900, 1, 1)
        let upperDateBounds = LocalDate(2100, 1, 1)
        if fromDate < lowerDateBounds then 
            // There is no beginning! (From-date is bogus.)
            if toDate > upperDateBounds then
                // There is no end either! (Until-date is bogus too.)
                PerpetualUsageRights
            else 
                // But it does end (Until-date is genuine.)
                UsageRightsUntil toDate 
        else
            // Usage rights had an actual from date. 
            if toDate > upperDateBounds then
                // But there is no end to it (Until-date is bogus.)
                UsageRightsSince fromDate
            else 
                UsageRightsBetween { RightsStart = fromDate; RightsExpire = toDate }

let toUsageRights (data : UsageRightsData) : Result<UsageRights, string> = 
    let readBlocked (d : UsageRightsData) 
        : Result<UsageRightsData * bool, string> = 
        Ok (d, d.GeoBlocked)
    let readFromUtc (d : UsageRightsData, blocked : bool) 
        : Result<UsageRightsData * bool * LocalDate, string> = 
        toInstant d.FromUtc
        |> Result.map (fun instant -> instant.InUtc().LocalDateTime.Date)
        |> Result.map (fun date -> (data, blocked, date))
    let readToUtc (d : UsageRightsData, blocked : bool, fromDate : LocalDate)
        : Result<bool * LocalDate * LocalDate, string> = 
        toInstant d.ToUtc
        |> Result.map (fun instant -> instant.InUtc().LocalDateTime.Date)
        |> Result.map (fun date -> (blocked, fromDate, date))
    let toRights (blocked : bool, fromDate : LocalDate, toDate : LocalDate) 
        : Result<UsageRights, string> = 
        let region = if blocked then Norway else World
        let duration = getUsageRightsDuration fromDate toDate
        Ok { Region = region; Duration = duration }
    data
    |> readBlocked
    |> Result.bind readFromUtc 
    |> Result.bind readToUtc 
    |> Result.bind toRights

let toUri (s : string) : Result<Uri, string> = 
    let (success, uri) = Uri.TryCreate(s, UriKind.RelativeOrAbsolute)
    if success then Ok uri else Error (sprintf "Not a valid url %s" s)

let toMediaFormat (s : string) : Result<MediaFormat, string> = 
    if s = "HLS" then Ok HLS
    else if s = "HDS" then Ok HDS 
    else if s = "Dash" then Ok Dash
    else Error (sprintf "Not a valid media format %s" s)
    
let toManifest (manifestUrl : string) (streamingFormat : string) 
    : Result<Manifest, string> = 

    let addStreamingFormat (url : Uri) : Result<Uri * MediaFormat, string> = 
        toMediaFormat streamingFormat
        |> Result.map (fun f -> (url, f))

    let createManifest (url : Uri, format : MediaFormat) : Result<Manifest, string> = 
        Ok { Url = url; Format = format }

    toUri manifestUrl
    |> Result.bind addStreamingFormat 
    |> Result.bind createManifest

let toSubtitlesFormat (s : string) : Result<SubtitlesFormat, string> = 
    if s = "TTML" then Ok TTML 
    else if s = "WebVTT" then Ok WebVTT 
    else Error (sprintf "Not a valid subtitles format %s" s)

let toLanguageCode (s : string) : Result<LanguageCode, string> = 
    if s = "No" then Ok No
    else if s = "En" then Ok En
    else Error (sprintf "Not a valid language code %s" s)

let toSubtitles (subtitlesUri : string) (subtitlesFormat : string) (languageCode : string)
    : Result<SubtitlesFile, string> =

    let addSubtitlesFormat (url : Uri) : Result<Uri * SubtitlesFormat, string> = 
        toSubtitlesFormat subtitlesFormat
        |> Result.map (fun format -> (url, format))

    let addLanguageCode (url : Uri, format : SubtitlesFormat) 
        : Result<Uri * SubtitlesFormat * LanguageCode, string> = 
        toLanguageCode languageCode 
        |> Result.map (fun code -> (url, format, code))

    let createSubtitlesFile (url : Uri, format : SubtitlesFormat, code : LanguageCode) 
        : Result<SubtitlesFile, string> = 
        Ok { Url = url; Format = format; Language = code }
    
    toUri subtitlesUri
    |> Result.bind addSubtitlesFormat 
    |> Result.bind addLanguageCode 
    |> Result.bind createSubtitlesFile

let toPlayable (data : ManifestData) : Result<Playable, string> = 
    let addSubtitles (uri : string) (format : string) (code : string) (manifest : Manifest) 
        : Result<Manifest * SubtitlesFile, string> = 
        toSubtitles uri format code
        |> Result.map (fun subtitles -> (manifest, subtitles))
    let createPlayable (manifest : Manifest, subtitles : SubtitlesFile)
        : Result<Playable, string> = 
        Ok { Manifest = manifest; Subtitles = subtitles }

    toManifest data.Manifest data.StreamFormat 
    |> Result.bind (addSubtitles data.Subtitles data.SubtitlesFormat data.Language)
    |> Result.bind createPlayable

let toPlaybackElement (manifestData : ManifestData) : Result<PlaybackElement, string> = 
    let addUsageRights (rightsData : UsageRightsData) (playable : Playable)
        : Result<Playable * UsageRights, string> =
        toUsageRights rightsData 
        |> Result.map (fun rights -> (playable, rights))
    let addActualDuration (durationString : string) (playable : Playable, usageRights : UsageRights)
        : Result<Playable * UsageRights * Duration, string> = 
        toDuration durationString 
        |> Result.map (fun duration -> (playable, usageRights, duration))

    let createPlaybackElement (playable : Playable, usageRights : UsageRights, duration : Duration)
        : Result<PlaybackElement, string> = 
        Ok { Playable = playable
             UsageRights = usageRights
             ActualDuration = duration 
             IndexPoints = [] }
        
    
    toPlayable manifestData 
    |> Result.bind (addUsageRights manifestData.UsageRights)
    |> Result.bind (addActualDuration manifestData.Duration)
    |> Result.bind createPlaybackElement 

let toLegalAge (s : string) : Result<LegalAge, string> = 
    let toRating (s : string) : Result<Rating, string> = 
        if s = "A" then Ok RatingA 
        else if s = "6" then Ok Rating6
        else if s = "9" then Ok Rating9
        else if s = "12" then Ok Rating12
        else if s = "15" then Ok Rating15
        else if s = "18" then Ok Rating18
        else Error (sprintf "Invalid legal age rating %s." s)
    if s = "" then Ok Exempt
    else toRating s |> Result.map Rated

let toProgramMetadata (metadataData : MetadataData) : Result<ProgramMetadata, string> = 
    let addPlannedDuration (durationStr : string) (legalAge : LegalAge)
        : Result<LegalAge * Duration option, string> = 
        toDurationOption durationStr 
        |> Result.map (fun d -> (legalAge, d))

    let createProgramMetadata (data : MetadataData) (legalAge : LegalAge, maybeDuration : Duration option)
        : Result<ProgramMetadata, string> = 
        Ok { Title = ProgramTitle data.Title 
             Description = ProgramDescription data.Description 
             Contributors = []
             LegalAge = legalAge 
             PlannedDuration = maybeDuration }

    toLegalAge metadataData.Rated
    |> Result.bind (addPlannedDuration metadataData.Duration)
    |> Result.bind (createProgramMetadata metadataData)

let toTransmission (data : TransmissionData) : Result<Transmission, string> = 
    let addEpgDate (s : string) (cid : ChannelId) 
        : Result<ChannelId * LocalDate, string> = 
        toLocalDate s 
        |> Result.map (fun date -> (cid, date))
    let addStartTime (s : string) (cid : ChannelId, epgDate : LocalDate) 
        : Result<ChannelId * LocalDate * Instant, string> = 
        toInstant s 
        |> Result.map (fun startTime -> (cid, epgDate, startTime))
    let addEndTime (s : string) (cid : ChannelId, epgDate : LocalDate, startTime : Instant)
        : Result<ChannelId * LocalDate * Instant * Instant, string> = 
        toInstant s 
        |> Result.map (fun endTime -> (cid, epgDate, startTime, endTime))
    let createTransmission (cid : ChannelId, epgDate : LocalDate, startTime : Instant, endTime : Instant)
        : Result<Transmission, string> =
        Ok { ChannelId = cid
             EpgDate = epgDate 
             ActualStartTime = startTime 
             ActualEndTime = endTime }

    Ok (ChannelId data.Channel)
    |> Result.bind (addEpgDate data.EpgDate)
    |> Result.bind (addStartTime data.StartTime)
    |> Result.bind (addEndTime data.EndTime)
    |> Result.bind createTransmission

let toProgram (metadataData : MetadataData) (manifestData : ManifestData) (transmissionsData : TransmissionsData) 
    : Result<Program, string> =
    let addMetadata (data : MetadataData) (pid : ProgId)
        : Result<ProgId * ProgramMetadata, string> = 
        toProgramMetadata data
        |> Result.map (fun metadata -> (pid, metadata))
    let addPlayable (data : ManifestData) (pid : ProgId, metadata : ProgramMetadata)
        : Result<ProgId * ProgramMetadata * PlaybackElement, string> =
        toPlaybackElement data 
        |> Result.map (fun playbackElement -> (pid, metadata, playbackElement))
    let addTransmissions (data : TransmissionsData) (pid : ProgId, metadata : ProgramMetadata, playbackElement : PlaybackElement)
        : Result<ProgId * ProgramMetadata * PlaybackElement * Transmission list, string> =
        let rec combine (results : Result<'a, 'e> list)
            : Result<'a list, 'e> = 
            match results with 
            | [] -> Ok []
            | r :: rest -> 
                match r with 
                | Error s -> Error s 
                | Ok t -> combine rest |> Result.bind (fun ts -> Ok (t :: ts))
        data.ProgramTransmissions 
        |> List.map toTransmission
        |> combine
        |> Result.map (fun ts -> (pid, metadata, playbackElement, ts))
    let createProgram (pid : ProgId, metadata : ProgramMetadata, playbackElement : PlaybackElement, transmissions : Transmission list)
        : Result<Program, string> = 
        Ok { ProgId = pid 
             Metadata = metadata 
             PlaybackElement = playbackElement 
             Transmissions = transmissions }

    Ok (ProgId metadataData.Id)
    |> Result.bind (addMetadata metadataData)
    |> Result.bind (addPlayable manifestData)
    |> Result.bind (addTransmissions transmissionsData)
    |> Result.bind createProgram

let findProgram (id : string) : Result<Program, string> = 
    let addManifestStep (id : string) (metadata : MetadataData)
        : Result<MetadataData * ManifestData, string> = 
        getManifest id 
        |> Result.map (fun manifest -> (metadata, manifest))
    let addTransmissionsStep (id : string) (metadata : MetadataData, manifest : ManifestData) 
        : Result<MetadataData * ManifestData * TransmissionsData, string> = 
        getTransmissions id 
        |> Result.map (fun transmissions -> (metadata, manifest, transmissions))
    let toProgramStep (metadata : MetadataData, manifest : ManifestData, transmissions : TransmissionsData) 
        : Result<Program, string> = 
        toProgram metadata manifest transmissions

    getMetadata id
    |> Result.bind (addManifestStep id)
    |> Result.bind (addTransmissionsStep id) 
    |> Result.bind toProgramStep