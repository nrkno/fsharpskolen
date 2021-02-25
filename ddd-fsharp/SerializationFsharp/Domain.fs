module SerializationFsharp.Domain

open System

type String50 = String50 of string
type Birthdate = Birthdate of DateTime

module String50 =
    let value (String50 s) = s
    let create(fieldName: string) (value: string) : Result<String50, string> =
        if String.IsNullOrEmpty value then
            Error (fieldName + " must be non-empty")
        elif value.Length > 50 then
            Error (fieldName + " must be less than 50 chars")
        else
            Ok (String50 value)
            
module Birthdate =
    let value (Birthdate s) = s
    let create (fieldName: string) (value: DateTime) : Result<Birthdate, string> =
        let lowerBound = DateTime.Parse "1-1-1900"
        let upperBound = DateTime.Now
        if (value.CompareTo lowerBound) < 0 then
            Error (fieldName + " can not be earlier than 1-1-1900")
        elif (value.CompareTo upperBound) > 0 then
            Error (fieldName + " must be earlier than today's date")
        else
            Ok (Birthdate value)

type Person = {
    First: String50
    Last: String50
    Birthdate: Birthdate
}
    