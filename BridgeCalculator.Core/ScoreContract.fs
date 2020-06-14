namespace BridgeCalculator.Core

module ScoreContract =

    let private countNT suit points =
        match suit with
        | NT -> points + 10
        | _  -> points

    let private countDouble db points =
        match db with
        | N  -> points
        | X  -> points * 2
        | XX -> points * 4

    let private applyVulnerableBonus declaredTricks points =
        match declaredTricks with
        | ot when ot < 6 -> points + 500        // Vulnerable game
        | ot when ot < 7 -> points + 1250       // Vulnerable game and slam
        | _              -> points + 2000       // Vulnerable game and grand slam

    let private applyNonvulnerableBonus declaredTricks points =
        match declaredTricks with
        | ot when ot < 6 -> points + 300        // nonvulnerable game
        | ot when ot < 7 -> points + 800        // nonvulnerable game and slam
        | _              -> points + 1300       // nonvulnerable game and grand slam

    let private applyImmediateBonus vulnerable declaredTricks points =
        match vulnerable, points with
        | _, p when p < 100 -> p + 50
        | v, _ -> match v with
                  | true -> applyVulnerableBonus declaredTricks points
                  | false -> applyNonvulnerableBonus declaredTricks points
    let private applyInsultBonus doubled points =
        match doubled with
        | N  -> points
        | X  -> points + 50
        | XX -> points + 100

    let private addOvertricks multiplier overtricks doubled vulnerable points = 
        match doubled, vulnerable with
        | N, _                -> points + overtricks * multiplier
        | X, false            -> points + overtricks * 100
        | X, true | XX, false -> points + overtricks * 200
        | XX, true            -> points + overtricks * 400
        

    let private countSuccess overtricks contract = 
        let multiplier = 
            match contract.DeclaredSuit with
            | C | D -> 20
            | H | S | NT -> 30
        
        contract.DeclaredTricks * multiplier
        |> countNT contract.DeclaredSuit
        |> countDouble contract.Doubled
        |> applyImmediateBonus contract.Vulnerable contract.DeclaredTricks
        |> applyInsultBonus contract.Doubled
        |> addOvertricks multiplier overtricks contract.Doubled contract.Vulnerable
    
    let private scoreUndoubledFailure undertricks vulnerable =
        undertricks
        |> (*) 50
        |> match vulnerable with
           | false -> (*) 1
           | true -> (*) 2

    let private scoreDoubledNonvulnerableFailure undertricks = 
        match undertricks with
        | x when x > -2  -> -100
        | x when x > -4  -> -100 + (x+1) * 200
        | x              -> -500 + (x+3) * 300

    let private scoreDoubledVulnerableFailure undertricks =
        match undertricks with
        | x when x > -2  -> -200
        | x              -> -200 + (x+1) * 300

    let private scoreDoubledFailure undertricks vulnerable =
        match vulnerable with
        | false -> scoreDoubledNonvulnerableFailure undertricks
        | true -> scoreDoubledVulnerableFailure undertricks

    let private countFailure undertricks contract = 
        match contract.Doubled with
        | N  -> scoreUndoubledFailure undertricks contract.Vulnerable
        | X  -> scoreDoubledFailure undertricks contract.Vulnerable
        | XX -> (scoreDoubledFailure undertricks contract.Vulnerable) * 2

    let score (handResult :  HandResult) = 
        match handResult.Tricks with
        | ot when ot >= 0 -> countSuccess ot handResult.DeclaredContract
        | _ -> countFailure handResult.Tricks handResult.DeclaredContract
