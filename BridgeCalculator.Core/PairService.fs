namespace BridgeCalculator.Core

open BridgeCalculator.Core.Mapping
open BridgeCalculator.Data.Sqlite

module PairService =
    let getPairs () =
        use context = new DataContext.DataContext()
        context.Database.EnsureCreated() |> ignore // TODO: Remove this later, do proper migrations
        PairsRepository.getPairs context
        |> Seq.map(fun pe -> mapPairEntityToPair pe)
        |> Seq.toArray

    let addPair pair =
        use context = new DataContext.DataContext()
        context.Database.EnsureCreated() |> ignore // TODO: Remove this later, do proper migrations
        pair
        |> mapPairToPairEntity
        |> PairsRepository.addPair context 
