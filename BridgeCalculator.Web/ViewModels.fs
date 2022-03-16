namespace BridgeCalculator.Web

module ViewModels =
    type [<CLIMutable>] PairViewModel = { Id : int; PlayerNames: string; }

    type [<CLIMutable>] ContractViewModel =
        {
            Declarer : string;
            DeclaredTricks : int;
            DeclaredSuit : string;
            Doubled : string;
            Vulnerable : bool;
        }

    type [<CLIMutable>] TravellerEntryViewModel =
        {
            boardNumber : int;
            vulnerable : string;
            nsPair : int;
            ewPair : int;
            declaredTricks : int;
            suit : string;
            doubled: string;
            declarer : string;
            tricks : int;
        }
