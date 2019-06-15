module ProgramApi.Domain
open System.Text.RegularExpressions

type ProgramId = ProgramId of string

type Error =
    | InvalidprogramId
    | NonExistingprogram

let (|ProgIdRegex|) input =
    let m = Regex.IsMatch(input, @"^\p{L}{4}\d{8}")
    if m then Ok <| ProgramId (input.ToUpper())
    else Error InvalidprogramId

let validate input =
    match input with
    | ProgIdRegex progId -> progId
    

// todo: lag en bedre programtype!
type Program = { Id: string}