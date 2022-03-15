namespace BridgeCalculator.Core.Tests

open System
open Xunit

open BridgeCalculator.Core.Types
open BridgeCalculator.Core.ScoreContract
open BridgeCalculator.Core.ParseContract

module ParseContractTests =
    [<Fact>]
    let ``4S should be parsed as a 4 spades contract`` () =
        let contractString = "4S"

        let expectedContract = {Declarer = North; DeclaredTricks = 4; DeclaredSuit = S; Doubled = N; Vulnerable = false}

        let result = parse contractString

        Assert.Equal(expectedContract, result)
