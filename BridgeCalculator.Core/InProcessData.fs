
namespace BridgeCalculator.Core

open System
open System.Collections.Generic;

module InProcessData = 
    let oldEntries = new ResizeArray<FlatTravellerEntry>()
    let entries = new Dictionary<int*int, FlatTravellerEntry>()

    let addOrUpdateEntry entry =
        match entries.TryGetValue (fst entry) with
        | false, _ -> entries.Add entry
        | true, _ -> entries.[fst entry] <- snd entry
