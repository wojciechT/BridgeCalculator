namespace BridgeCalculator.Data.Sqlite

open Microsoft.EntityFrameworkCore;

module DataContext =
    type DataContext =
        inherit DbContext

        new() = {inherit DbContext()}

        override x.OnConfiguring(options : DbContextOptionsBuilder) =
            options.UseSqlite("Data Source=data.db") |> ignore

        [<DefaultValue>]
        val mutable pairs:DbSet<PairEntity>
        [<DefaultValue>]
        val mutable contracts:DbSet<ContractEntity>
        [<DefaultValue>]
        val mutable handResults:DbSet<HandResultEntity>
        [<DefaultValue>]
        val mutable travellerEntries:DbSet<TravellerEntryEntity>
        [<DefaultValue>]
        val mutable travellers:DbSet<TravellerEntity>
        
        member x.Pairs 
            with get() = x.pairs 
            and set v = x.pairs <- v
        member x.Contracts
            with get() = x.contracts 
            and set v = x.contracts <- v
        member x.HandResults 
            with get() = x.handResults 
            and set v = x.handResults <- v
        member x.TravellerEntries 
            with get() = x.travellerEntries 
            and set v = x.travellerEntries <- v
        member x.Travellers 
            with get() = x.travellers 
            and set v = x.travellers <- v
