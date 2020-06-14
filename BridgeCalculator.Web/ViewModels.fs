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
