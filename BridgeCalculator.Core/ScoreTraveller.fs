namespace BridgeCalculator.Core

open System

module ScoreTraveller = 

    let private compensateForSide (declarer, score) =
        match declarer with
        | North | South -> score
        | East  | West  -> (-1 * score)

    let getInterScore (travellerEntry : FullTravellerEntry) = 
        let pointResult = ScoreContract.score travellerEntry.Result
        
        let nsScore =
            compensateForSide (travellerEntry.Result.DeclaredContract.Declarer, pointResult)

        let ewScore = -1 * nsScore // Scores are always mirrored

        [ { BoardNumber = travellerEntry.BoardNumber;
            Pair = travellerEntry.NSPair;
            Position = North;
            Contract = travellerEntry.Result.DeclaredContract;
            Tricks = travellerEntry.Result.Tricks;
            Score = nsScore };
          
          { BoardNumber = travellerEntry.BoardNumber;
            Pair = travellerEntry.EWPair;
            Position = East;
            Contract = travellerEntry.Result.DeclaredContract;
            Tricks = travellerEntry.Result.Tricks;
            Score = ewScore} ]

    let private getAvg scores = 
        scores
        |> List.averageBy (float)
        |> (fun s -> s / 10.0)
        |> round
        |> int
        |> (*) 10

    let private getAverage (interScores : InterScore list) =
        interScores
        |> List.filter (fun ins -> ins.Position = North || ins.Position = South)
        |> List.map (fun ins -> ins.Score)
        |> getAvg;

    let private convertResultToImps (result : int) =
        (Math.Sign result) * CalculateImps.matchWithImpValue (Math.Abs result)
    
    let private convertInterScoreToResultsRow average (interScore : InterScore) : ResultsRow =
        let score = match interScore.Position with
                    | North | South -> interScore.Score - average
                    | East  | West  -> interScore.Score + average
        {
            BoardNumber = interScore.BoardNumber;
            AverageScore = average;
            Pair = interScore.Pair;
            Position = interScore.Position; 
            Contract = interScore.Contract;
            Tricks = interScore.Tricks;
            RawScore = interScore.Score;
            DiffScore = score;
            IMPScore = convertResultToImps score;
        }  

    let private getFinalScores average (interScores : InterScore list) =
        interScores
        |> List.map (convertInterScoreToResultsRow average)

    let private logResult (result : ResultsRow) =     
        printfn "%i, %i, %i, %O, %O, %i%O%O, %i, %i, %i, %i" result.BoardNumber result.AverageScore result.Pair result.Position result.Contract.Declarer result.Contract.DeclaredTricks result.Contract.DeclaredSuit result.Contract.Doubled result.Tricks result.RawScore result.DiffScore result.IMPScore

    let private logResults results =
        List.iter logResult results

    let score traveller =
        let interScores = traveller.Entries |> List.collect(getInterScore)
        let average = interScores |> getAverage
        let results = getFinalScores average interScores
        logResults results
        results                     // TODO: Obvious tee candidate
