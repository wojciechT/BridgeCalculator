namespace BridgeCalculator.Core

open System
open System.IO

module Program = 

    let getScoreOutOfResultsRow (resultsRow : ResultsRow) =
        resultsRow.Pair, resultsRow.IMPScore
   
    let flow () =
        printfn "%s" (Directory.GetCurrentDirectory())
        printfn "Board, AvgScore, Pair, Position, Player, Contract, Tricks, Score, DiffScore, IMPs" // TODO: Remove this from here
        ParseTravellerEntries.parseTravellerEntries
        |> Seq.collect( ScoreTraveller.score )
        |> (fun s -> CsvOutput.outputDeals s; s)
        |> Seq.map getScoreOutOfResultsRow 
        |> Seq.groupBy(fun (p, _) -> p)
        |> Seq.map (fun (p, rs) -> p, (rs |> Seq.sumBy(fun (_, r) -> r)))
        |> Seq.sortByDescending(fun (_, r) -> r)
        |> (fun s -> CsvOutput.outputResults s; s)
        |> Seq.iter(fun (p, r) -> printfn "%i: %i" p r)

    [<EntryPoint>]
    let main argv =
        flow()
        0 // return an integer exit code
