module ProgramApi.Lookup

open System
open ProgramApi.Domain
open ProgramApi.ProgramRepository
open NodaTime 
open NodaTime.Text

let toInstant (s : string) : Instant = 
    let parseResult = InstantPattern.ExtendedIso.Parse(s)
    parseResult.GetValueOrThrow()

let toLocalDate (s : string) : LocalDate = 
    let instant = toInstant s 
    instant.InUtc().LocalDateTime.Date

let program1 = {
    ProgId = ProgId "DKMR50000413"
    Metadata = {
        Title = ProgramTitle "Der ingen skulle tru at nokon kunne bu"
        Description = ProgramDescription "Norsk dokumentarserie. \rFor 30 år sidan kom Gaby frå Færøyane og Rupert frå Tyskland til Tinn med hest og prærievogn. Sidan har dei drive det brattlendte småbruket Skifterud utan mekaniserte hjelpemiddel, og målet er å vere sjølvforsynt. Dessutan reiser trubaduren Derben på turné - med hest og vogn. Sesong 13 (4:6)"
        Contributors = [
            { Role = Role "Medvirkende"; Name = Name "Gaby Larsen" }
            { Role = Role "Medvirkende"; Name = Name "Rubert Derben" }
        ]
        LegalAge = Rated RatingA 
        PlannedDuration = Some <| Duration.FromTimeSpan(TimeSpan(00, 29, 07)) 
    }
    Transmissions = [
        {   ChannelId = ChannelId "nrk1"
            EpgDate = LocalDate(2014, 11, 16)
            ActualStartTime = toInstant "2014-11-16T19:15:56Z"
            ActualEndTime = toInstant "2014-11-16T19:46:27Z" }
    ] 
    PlaybackElement = {
        Playable = {
            Manifest = {
                Url = Uri("https://dummy.cdn.no/hls/dkmr50000413/playlist.m3u8")
                Format = HLS
            }
            Subtitles = {
                Url = Uri("https://blablabla.text.no/TTV/DKMR50000413.vtt")
                Format = WebVTT
                Language = No 
            }
        }
        UsageRights = {
            Region = World 
            Duration = UsageRightsSince <| toLocalDate "2014-11-16T19:15:00Z" 
        }
        ActualDuration = Duration.FromTimeSpan(TimeSpan(00, 29, 11)) 
        IndexPoints = []
    }
}

let programs = [
    program1
]

let find (id : string) : Result<Program, string> = 
    match List.tryFind (fun (p : Program) -> p.ProgId = ProgId id) programs with 
    | None -> Error (sprintf "Program %s not found." id)
    | Some p -> Ok p
