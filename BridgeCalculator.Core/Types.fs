namespace BridgeCalculator.Core

[<AutoOpen>]
module Types =
    type Position = 
        | North 
        | South
        | East
        | West


    type VulnerablePosition = 
        | None 
        | NS 
        | EW 
        | Both       

    
    type Suit = 
        | C 
        | D 
        | H 
        | S 
        | NT


    type Double =
        | N 
        | X 
        | XX

  
    type Contract =
        {
            Declarer : Position
            DeclaredTricks : int
            DeclaredSuit : Suit
            Doubled : Double
            Vulnerable : bool
        }
        static member Empty() = {Declarer = North; DeclaredTricks = 0; DeclaredSuit = C; Doubled = N; Vulnerable = false} 

    type HandResult =
        {
            DeclaredContract : Contract
            Tricks : int
        }

    type Pair = 
        {
            Id : int
            PlayerNames : string
        }

    type TravellerEntry =
        {
            NSPair : int
            EWPair : int
            Result : HandResult
        }

    type FlatTravellerEntry =
        {
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
        }

    type FullTravellerEntry =
        {
            BoardNumber : int
            Vulnerability : VulnerablePosition
            NSPair : int
            EWPair : int
            Result : HandResult
        }
    type Traveller = 
        {
            BoardNumber : int
            Vulnerability: VulnerablePosition
            Entries: FullTravellerEntry list
        }


    type InterScore =
        {
            BoardNumber : int
            Pair : int
            Position : Position
            Contract : Contract
            Tricks: int
            Score : int
        }

    type Tournament =
        {
            Id : string
            Travellers : Traveller list
        }
    type ResultsRow = 
        {
            BoardNumber : int
            AverageScore : int
            Pair : int
            Position : Position
            Contract : Contract
            Tricks : int
            RawScore : int
            DiffScore : int
            IMPScore : int
        }
