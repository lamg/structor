open structor
// This attribute is required and needs to match the correct context type!

[<EntryPoint>]
let main args =
  match args with
  | [| source; dest |] ->
    FileGraph.visitSource source dest
    0
  | _ ->
    eprintfn $"unrecognized arguments {args}"
    1
