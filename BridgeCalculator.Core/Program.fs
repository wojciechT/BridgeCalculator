namespace BridgeCalculator

open System

module Program = 
    let getTravellerScore () =
        ReadTraveller.read ()
        |> ScoreTraveller.score
        //|> fun r -> [r]
        |> PrintScores.print

    let loop =
        Seq.initInfinite( fun _ -> getTravellerScore () )
    
    [<EntryPoint>]
    let main argv =
        loop |> Seq.toArray |> ignore
        0 // return an integer exit code
