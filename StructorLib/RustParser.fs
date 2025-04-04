module StructorLib.RustParser

open RustParserCs

type RustVisitor() =
  inherit RustParserBaseVisitor<string * string>()
  
  override this.VisitFunction_ (context: RustParser.Function_Context)= "", ""