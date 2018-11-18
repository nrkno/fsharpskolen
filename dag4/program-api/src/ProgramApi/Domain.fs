module ProgramApi.Domain

type ProgId = ProgId of string

type Program = 
    {
        ProgId: ProgId
    }