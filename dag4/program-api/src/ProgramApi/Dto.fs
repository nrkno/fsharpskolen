module ProgramApi.Dto

type Program = {
    ProgId: string
}

module Program = 
    let fromDomain ( program : Domain.Program ) = 
        match program.ProgId with
        | Domain.ProgId progId -> { ProgId = progId }