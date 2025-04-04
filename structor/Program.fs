open System
open Structor
open Argu

type Args =
  | [<AltCommandLine("-s")>] Source of string
  | [<AltCommandLine("-o")>] Output of string

  interface IArgParserTemplate with
    member s.Usage =
      match s with
      | Source _ -> "Path to the file to be inspected"
      | Output _ -> "Output file for the Graphviz graph"

[<EntryPoint>]
let main args =
  let errorHandler =
    ProcessExiter(
      colorizer =
        function
        | ErrorCode.HelpText -> None
        | _ -> Some ConsoleColor.Red
    )

  let parser =
    ArgumentParser.Create<Args>(programName = "structor", errorHandler = errorHandler)

  let results = parser.ParseCommandLine(inputs = args, raiseOnUsage = true)

  match results.TryGetResult Source, results.TryGetResult Output with
  | Some src, None ->
    let res = FileGraphFs.visitSource src
    printfn $"{res}"
    0
  | Some src, Some dest ->
    let res = FileGraphFs.visitSource src
    IO.File.WriteAllText(dest, res)
    0
  | _ ->
    eprintfn $"{parser.PrintUsage()}"
    1
