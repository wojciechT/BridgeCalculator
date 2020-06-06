namespace BridgeCalculator

[<AutoOpen>]
module Types =
    type Player = 
        | North 
        | South
        | East
        | West

    type Suit = C | D | H | S | NT

    type Double = N | X | XX

    //type ImmediateBonus
   
    type Contract =
        {
            Declarer : Player
            DeclaredSuit : Suit
            DeclaredTricks : int
            Vulnerable : bool
            Doubled : Double
        }

    type HandResult =
        {
            DeclaredContract : Contract
            Tricks : int
        }


