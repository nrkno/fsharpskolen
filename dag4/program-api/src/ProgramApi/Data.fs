module ProgramApi.Data

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