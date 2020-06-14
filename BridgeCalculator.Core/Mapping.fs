namespace BridgeCalculator.Core

open BridgeCalculator.Data.Sqlite.Entities

module Mapping =
    let mapPairEntityToPair (pairEntity : PairEntity) : Pair = 
        {Id = pairEntity.Id; PlayerNames = pairEntity.PlayerNames}

    let mapPairToPairEntity (pair : Pair) =
        {Id = pair.Id; PlayerNames = pair.PlayerNames}
