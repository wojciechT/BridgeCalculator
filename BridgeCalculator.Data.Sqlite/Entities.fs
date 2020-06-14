namespace BridgeCalculator.Data.Sqlite

[<AutoOpen>]
module Entities =
    type [<CLIMutable>] PairEntity = { Id : int; PlayerNames : string }

    type [<CLIMutable>] ContractEntity = 
        {
            Id : int;
            Declarer : string;
            DeclaredTricks : int;
            DeclaredSuit : string;
            Doubled : string;
            Vulnerable : bool;
        }

    type [<CLIMutable>] HandResultEntity =
        {
            Id : int;
            DeclaredContract : ContractEntity;
            Tricks : int;
        }

    type [<CLIMutable>] TravellerEntryEntity =
        {
            Id : int;
            NSPair : PairEntity;
            EWPair : PairEntity;
            Result : HandResultEntity;
        }

    type [<CLIMutable>] TravellerEntity =
        {
            Id : int;
            Vulnerability : string;
            Entries : TravellerEntryEntity [];
        }
