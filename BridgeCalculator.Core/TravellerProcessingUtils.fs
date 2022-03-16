namespace BridgeCalculator.Core

open BridgeCalculator.Core.Types

module TravellerProcessingUtils = 
    
    let checkVulnerability declarer vulnerable =
        match vulnerable, declarer with
        | None, _   -> false
        | Both, _   -> true
        | NS, North -> true
        | NS, South -> true
        | EW, East  -> true
        | EW, West  -> true
        | _         -> false

    let groupFullTravellerEntriesToTravellers (entries : FullTravellerEntry seq) =
        entries
        |> Seq.groupBy(fun e -> e.BoardNumber, e.Vulnerability)
        |> Seq.map (fun bpe -> 
            let bp, fullEntries = bpe
            let entries = 
                fullEntries
                |> Seq.toList
            {BoardNumber = fst bp; Vulnerability = snd bp; Entries = entries})
