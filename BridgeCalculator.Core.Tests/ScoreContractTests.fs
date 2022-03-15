namespace BridgeCalculator.Core.Tests

open System
open Xunit

open BridgeCalculator.Core.Types
open BridgeCalculator.Core.ScoreContract

module ScoreContractTests =
    [<Fact>]
    let ``0H+0 should be scored 0`` () =
        let contract = {Declarer = North; DeclaredSuit = H; DeclaredTricks = 0; Vulnerable = false; Doubled = N }
        let handResult = {DeclaredContract = contract; Tricks = -1}

        let result = score handResult

        Assert.Equal(0, result)

    
    [<Fact>]
    let ``2H+1 should be scored 140`` () =
        let contract = {Declarer = North; DeclaredSuit = H; DeclaredTricks = 2; Vulnerable = false; Doubled = N }
        let handResult = {DeclaredContract = contract; Tricks = 1}

        let result = score handResult

        Assert.Equal(140, result)

    [<Fact>]
    let ``2SX+1 should be scored 570`` () =
        let contract = {Declarer = North; DeclaredSuit = S; DeclaredTricks = 2; Vulnerable = false; Doubled = X }
        let handResult = {DeclaredContract = contract; Tricks = 1}

        let result = score handResult

        Assert.Equal(570, result)

    [<Fact>]
    let ``2NTXX+3 vulnerable should be scored 2080`` () =
        let contract = {Declarer = North; DeclaredSuit = NT; DeclaredTricks = 2; Vulnerable = true; Doubled = XX }
        let handResult = {DeclaredContract = contract; Tricks = 3}

        let result = score handResult

        Assert.Equal(2080, result)

    [<Fact>]
    let ``6C= vulnerable should be scored 1370`` () =
        let contract = {Declarer = North; DeclaredSuit = C; DeclaredTricks = 6; Vulnerable = true; Doubled = N }
        let handResult = {DeclaredContract = contract; Tricks = 0}
        
        let result = score handResult

        Assert.Equal(1370, result)

    [<Fact>]
    let ``7NTXX= vulnerable should be scored 2980`` () =
        let contract = {Declarer = North; DeclaredSuit = NT; DeclaredTricks = 7; Vulnerable = true; Doubled = XX }
        let handResult = {DeclaredContract = contract; Tricks = 0}
    
        let result = score handResult

        Assert.Equal(2980, result)
    
    [<Fact>]
    let ``1NT-1 should be scored -50`` () =
        let contract = {Declarer = North; DeclaredSuit = NT; DeclaredTricks = 1; Vulnerable = false; Doubled = N }
        let handResult = {DeclaredContract = contract; Tricks = -1}

        let result = score handResult

        Assert.Equal(-50, result)
    
    [<Fact>]
    let ``7NT-13 vulnerable should be scored -1300`` () =
        let contract = {Declarer = North; DeclaredSuit = NT; DeclaredTricks = 7; Vulnerable = true; Doubled = N }
        let handResult = {DeclaredContract = contract; Tricks = -13}

        let result = score handResult

        Assert.Equal(-1300, result)

    [<Fact>]
    let ``1NTX-3 should be scored -500`` () =
        let contract = {Declarer = North; DeclaredSuit = NT; DeclaredTricks = 1; Vulnerable = false; Doubled = X }
        let handResult = {DeclaredContract = contract; Tricks = -3}

        let result = score handResult

        Assert.Equal(-500, result)
    
    [<Fact>]
    let ``4NTXX-9 vulnerable should be scored -5200`` () =
        let contract = {Declarer = North; DeclaredSuit = NT; DeclaredTricks = 4; Vulnerable = true; Doubled = XX }
        let handResult = {DeclaredContract = contract; Tricks = -9}

        let result = score handResult

        Assert.Equal(-5200, result)
