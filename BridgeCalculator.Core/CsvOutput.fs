namespace BridgeCalculator.Core

open System
open System.IO

module CsvOutput =

  [<Literal>]
  let formatString = "yyyy-MM-dd"

  let private getFileNameSuffix () = DateTimeOffset.Now.ToString(formatString)

  // EXCEL dialect of csv, to be changed
  let private logResult (result : ResultsRow) =     
    sprintf "%i; %i; %i; %O; %O; %i%O%O; %i; %i; %i; %i" result.BoardNumber result.AverageScore result.Pair result.Position result.Contract.Declarer result.Contract.DeclaredTricks result.Contract.DeclaredSuit result.Contract.Doubled result.Tricks result.RawScore result.DiffScore result.IMPScore

  let outputDeals (scores : ResultsRow seq) = 
    let fileName = sprintf "deals-%s.csv" (getFileNameSuffix ())
    use file = File.CreateText(fileName)
    file.WriteLine "Board; AvgScore; Pair; Position; Player; Contract; Tricks; Score; DiffScore; IMPs"
    scores |> Seq.iter ( logResult >> file.WriteLine )

  let outputResults (results : (int * int) seq) = 
    let fileName = sprintf "results-%s.csv" (getFileNameSuffix ())
    use file = File.CreateText(fileName)
    file.WriteLine "Pair; Score"
    results |> Seq.iter ( fun (p,r) -> sprintf "%i; %i" p r |> file.WriteLine )
