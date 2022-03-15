namespace BridgeCalculator.Web

open BridgeCalculator.Core.Types
open BridgeCalculator.Web.ViewModels

module Mapping =
    let mapPairToPairViewModel (pair : Pair) : PairViewModel =
        { Id = pair.Id; PlayerNames = pair.PlayerNames}

    let mapPairViewModelToPair (pairViewModel : PairViewModel) : Pair =
        { Id = pairViewModel.Id; PlayerNames = pairViewModel.PlayerNames}

    let mapVulnerabilityStringToVulnerablePosition(vulnerability : string) =
        match vulnerability.ToLowerInvariant() with
        | "n" -> None
        | "ns" -> NS
        | "ew" -> EW
        | "b" -> Both
        | x -> failwithf "Unrecognized position string %s" x

    let mapDeclarerStringToPosition(declarer : string) : Position =
        match declarer.ToLowerInvariant() with
        | "n" -> North
        | "s" -> South
        | "e" -> East
        | "w" -> West
        | x -> failwithf "Unrecognized declarer string %s" x

    let mapTravellerEntryViewModelToFlatTravellerEntry(entry : TravellerEntryViewModel) : FlatTravellerEntry =
        { BoardNumber = entry.boardNumber; 
          Vulnerability = entry.vulnerable |> mapVulnerabilityStringToVulnerablePosition; 
          NSPair = entry.nsPair;
          EWPair = entry.ewPair;
          Declarer = entry.declarer |> mapDeclarerStringToPosition;
          DeclaredTricks = entry}

            BoardNumber : int
            Vulnerability: VulnerablePosition
            NSPair : int
            EWPair : int
            Declarer : Position
            DeclaredTricks : int
            DeclaredSuit : Suit
            Doubled : Double
            Vulnerable : bool
            Tricks : int