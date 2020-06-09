namespace BridgeCalculator

module ScoreTraveller = 

    let private compensateForSide (declarer, score) =
        match declarer with
        | North | South -> score
        | East  | West  -> (-1 * score)

    let getInterScore travellerEntry = 
        let pointResult = ScoreContract.score travellerEntry.Result
        
        let nsScore =
            compensateForSide (travellerEntry.Result.DeclaredContract.Declarer, pointResult)

        let ewScore = -1 * nsScore // Scores are always mirrored

        [{Pair = travellerEntry.NSPair; Position = North; Score = nsScore}; {Pair = travellerEntry.EWPair; Position = East; Score = ewScore}]

    let private getAvg scores = 
        scores
        |> List.averageBy (float)
        |> (fun s -> s / 10.0)
        |> round
        |> int
        |> (*) 10

    let private getAverage (interScores) =
        interScores
        |> List.filter (fun ins -> ins.Position = North || ins.Position = South)
        |> List.map (fun ins -> ins.Score)
        |> getAvg;

    let private getFinalScores average interScores =
        interScores
        |> List.map ( fun inter -> 
                         match inter.Position with
                         | North | South -> inter.Pair, inter.Score - average
                         | East  | West  -> inter.Pair, inter.Score + average ) // Scores are always mirrored

    let score traveller =
        let interScores = traveller.Entries |> List.collect(getInterScore)
        let average = interScores |> getAverage

        getFinalScores average interScores
