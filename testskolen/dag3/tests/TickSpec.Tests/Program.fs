module TickSpec.Tests.Program

open System.Reflection
open TickSpec

do  let ass = Assembly.GetExecutingAssembly()
    let definitions = new StepDefinitions(ass)

    [@"TickSpec.Tests.spec.feature"]
    |> Seq.iter (fun source ->
        let s = ass.GetManifestResourceStream(source)
        definitions.Execute(source,s)
    )
