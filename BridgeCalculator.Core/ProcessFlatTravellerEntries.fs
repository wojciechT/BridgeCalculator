namespace BridgeCalculator.Core

open BridgeCalculator.Core.Types
open BridgeCalculator.Core.TravellerProcessingUtils

module ProcessFlatTravellerEntries = 
    let getContract (flatTraveller : FlatTravellerEntry) : Contract =
        { Declarer = flatTraveller.Declarer;
          DeclaredTricks = flatTraveller.DeclaredTricks;
          DeclaredSuit = flatTraveller.DeclaredSuit;
          Doubled = flatTraveller.Doubled;
          Vulnerable = checkVulnerability flatTraveller.Declarer flatTraveller.Vulnerability }

    let getHandResult (flatTraveller : FlatTravellerEntry) : HandResult =
        { DeclaredContract = getContract flatTraveller;
          Tricks = flatTraveller.Tricks }

    let mapFlatTravellerToFullTraveller (flatTraveller : FlatTravellerEntry) : FullTravellerEntry =
        { BoardNumber = flatTraveller.BoardNumber;
          Vulnerability = flatTraveller.Vulnerability;
          NSPair = flatTraveller.NSPair;
          EWPair = flatTraveller.EWPair;
          Result = getHandResult flatTraveller }

    let processFlatTravellers flatTravellers =
        flatTravellers
        |> Seq.map mapFlatTravellerToFullTraveller
        |> groupFullTravellerEntriesToTravellers
