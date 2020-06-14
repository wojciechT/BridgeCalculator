namespace BridgeCalculator.Core

[<AutoOpen>]
module Types =
    type Position = 
        | North 
        | South
        | East
        | West

    type VulnerablePosition = None | NS | EW | Both

    type Suit = C | D | H | S | NT

    type Double = N | X | XX
  
    type Contract =
        {
            Declarer : Position
            DeclaredTricks : int
            DeclaredSuit : Suit
            Doubled : Double
            Vulnerable : bool
        }

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
            NSPair : Pair
            EWPair : Pair
            Result : HandResult
        }

    type Traveller = 
        {
            BoardNumber : int
            Vulnerability: VulnerablePosition
            Entries: TravellerEntry list
        }

    type InterScore =
        {
            Pair : Pair
            Position : Position
            Score : int
        }
