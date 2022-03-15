namespace BridgeCalculator.Core

open FSharp.Data

open BridgeCalculator.Core.Types

module ParseTravellerEntries =

    type TravellerEntries = CsvProvider<"traveller.csv", ";">  //Resign from csv provider?
    
    let private getSuit (suitName : char) =
        match suitName with
        | 'c'  -> C
        | 'd'  -> D
        | 'h'  -> H
        | 's'  -> S
        | 'n' -> NT
        | x    -> failwithf "Unrecognized suit name: %c" x

    let private contractFolder (next : char) (contract : Contract)  =
        match next with
        | 'p' -> contract
        | 'x' -> match contract.Doubled with
                 | N  -> {contract with Doubled = X}
                 | X  -> {contract with Doubled = XX}
                 | XX -> failwith "Too many doubled signs"
        | 'c' | 'd' | 'h' | 's' | 'n' -> {contract with DeclaredSuit = getSuit next}
        | vl -> match System.Int32.TryParse(vl.ToString()) with
                | true, x  -> {contract with DeclaredTricks = x}
                | false, _ -> failwith "Expected number"

    let parseContract (contractString : string) =
        contractString.ToLowerInvariant()
        |> (fun x -> Seq.foldBack contractFolder x (Contract.Empty()))

    let private parseVulnerability (vulnerabilityString : string) =
        match vulnerabilityString.ToLowerInvariant() with
        | "n" -> None
        | "ns" -> NS
        | "ew" -> EW
        | "b"  -> Both
        | x    -> failwithf "Unrecognized vulnerable position: %s" x

    let private parseDeclarer (declarerString : string) =
        match declarerString.ToLowerInvariant() with
        | "n" -> North
        | "s" -> South
        | "e" -> East
        | "w" -> West
        | x   -> failwithf "Unrecognized declarer: %s" x

    let private checkVulnerability declarer vulnerable =
        match vulnerable, declarer with
        | None, _   -> false
        | Both, _   -> true
        | NS, North -> true
        | NS, South -> true
        | EW, East  -> true
        | EW, West  -> true
        | _         -> false

    let private groupFullTravellerEntriesToTravellers (entries : FullTravellerEntry seq) =
        entries
        |> Seq.groupBy(fun e -> e.BoardNumber, e.Vulnerability)
        |> Seq.map (fun bpe -> 
            let bp, fullEntries = bpe
            let entries = 
                fullEntries
                |> Seq.toList
            {BoardNumber = fst bp; Vulnerability = snd bp; Entries = entries})

    let parseTravellerEntries =
        let path = System.IO.Directory.GetCurrentDirectory();
        let entries = TravellerEntries.Load(sprintf "%s/traveller.csv" path)
        
        entries.Rows
        |> Seq.map(fun r -> 
            let vulnerability = parseVulnerability r.Vulnerable
            let contract = parseContract (r.Contract)
            let declarer = parseDeclarer (r.Declarer)
            {
                BoardNumber = r.Board;
                Vulnerability = vulnerability;
                NSPair = r.NSPair;
                EWPair = r.EWPair;
                Result = {
                    DeclaredContract = {contract with Declarer = declarer; Vulnerable = (checkVulnerability declarer vulnerability)};
                    Tricks = r.Result
                }
            })
        |> groupFullTravellerEntriesToTravellers
