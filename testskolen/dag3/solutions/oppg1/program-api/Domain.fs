module ProgramApi.Domain
open System.Text.RegularExpressions

type ProgramId = ProgramId of string

let (|ProgIdRegex|) input =
    let m = Regex.IsMatch(input, @"^\p{L}{4}\d{8}")
    if m then Some <| ProgramId (input.ToUpper())
    else None

let validate input =
    match input with
    | ProgIdRegex progId -> progId
    

// todo: lag en bedre programtype!
type Program = { Id: string}