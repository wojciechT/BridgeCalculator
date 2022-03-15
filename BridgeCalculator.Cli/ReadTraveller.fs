namespace BridgeCalculator.Cli

open System

open BridgeCalculator.Core.Types

module ReadTraveller =
    
    let private readContract vulnerablePosition =
        printf "Declarer (N/S/E/W): "
        let declarer = 
            Console.ReadLine()
            |> string
            |> fun s -> s.ToLowerInvariant()
            |> function
                | "n" -> North
                | "s" -> South
                | "e" -> East
                | "w" -> West
                | _   -> failwith "Unrecognized position"
    
        printf "Declared tricks: "
        let declaredTricks = Console.ReadLine() |> int
        
        printf "Declared suit (C/D/H/S/NT): "
        let declaredSuit = 
            Console.ReadLine() 
            |> string 
            |> (fun s -> s.ToLowerInvariant()) 
            |> function
                | "c"  -> C
                | "d"  -> D
                | "h"  -> H
                | "s"  -> S
                | "nt" -> NT
                | _    -> failwith "Unrecognized suit"
        
        printf "Doubled? (X/XX): "
        let doubled =
            Console.ReadLine()
            |> string
            |> (fun s -> s.ToLowerInvariant())
            |> function
                | "" | "n" -> N
                | "x"      -> X
                | "xx"     -> XX
                | _        -> failwith "Unrecognized double"

        let vulnerable =
            match vulnerablePosition with
            | None -> false
            | Both -> true
            | NS   -> match declarer with
                      | North | South -> true
                      | East  | West  -> false
            | EW   -> match declarer with
                      | North | South -> false
                      | East  | West  -> true

        {Declarer = declarer; DeclaredTricks = declaredTricks; DeclaredSuit = declaredSuit; Doubled = doubled; Vulnerable = vulnerable}

    let private readHandResult vulnerablePosition = 
        let declaredContract = readContract vulnerablePosition
        
        printf "Result: "
        let tricks = Console.ReadLine() |> int

        {DeclaredContract = declaredContract; Tricks = tricks}

    let private readTravellerEntry pairs vulnerablePosition = 
        printf "NS pair number: "
        let nsPair = Console.ReadLine() |> int |> fun i -> Map.find i pairs
        printf "EW pair number: "
        let ewPair = Console.ReadLine() |> int |> fun i -> Map.find i pairs

        let handResult = readHandResult vulnerablePosition

        {NSPair = nsPair; EWPair = ewPair; Result = handResult}

    let read rounds pairs = 
        printf "Enter board number: "
        let boardNumber = Console.ReadLine() |> int

        printf "Enter vulnerable position (N/NS/EW/B): "
        let vulnerablePosition = 
            Console.ReadLine()
            |> string
            |> fun s -> s.ToLowerInvariant()
            |> function
                | "n"  -> None
                | "ns" -> NS
                | "ew" -> EW
                | "b"  -> Both
                | _    -> failwith "Unrecognized vulnerable position"


        
        let entries = List.init rounds (fun _ -> readTravellerEntry pairs vulnerablePosition)

        {BoardNumber = boardNumber; Vulnerability = vulnerablePosition; Entries = entries}
