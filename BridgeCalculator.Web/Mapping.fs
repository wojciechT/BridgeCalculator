namespace BridgeCalculator.Web

open BridgeCalculator.Core.Types
open BridgeCalculator.Web.ViewModels

module Mapping =
    let mapPairToPairViewModel (pair : Pair) : PairViewModel =
        { Id = pair.Id; PlayerNames = pair.PlayerNames}

    let mapPairViewModelToPair (pairViewModel : PairViewModel) : Pair =
        { Id = pairViewModel.Id; PlayerNames = pairViewModel.PlayerNames}
