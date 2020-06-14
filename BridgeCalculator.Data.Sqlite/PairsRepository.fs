namespace BridgeCalculator.Data.Sqlite

open BridgeCalculator.Data.Sqlite.DataContext

module PairsRepository =
    
    let getPairs (context : DataContext) =
        query {
            for pair in context.Pairs do
            select pair
        }

    let addPair (context : DataContext) pair =
        context.Pairs.Add(pair) |> ignore
        context.SaveChanges true |> ignore
