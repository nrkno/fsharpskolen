module SerializationFsharp.Dto

open System

type PersonDto = {
    First: string
    Last: string
    Birthdate: DateTime
}