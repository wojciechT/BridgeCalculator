namespace BridgeCalculator.Cli

open BridgeCalculator.Core.Types

module PrintScores =
    let private printScore (pair : Pair, score) =
        printfn "%s: %i" pair.PlayerNames score

    let private sumScores (scores : (Pair * int) seq) =
        scores
        |> Seq.sumBy(fun (_, s) -> s)

    let print (scores : (Pair * int) seq) =
        scores
        |> Seq.groupBy fst
        |> Seq.map(fun (p, ps) -> (p, sumScores ps))
        |> Seq.iter printScore
