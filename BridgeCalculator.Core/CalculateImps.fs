namespace BridgeCalculator.Core

module CalculateImps =
    let matchWithImpValue points=
        match points with
        | x when x < 20   -> 0
        | x when x < 50   -> 1
        | x when x < 90   -> 2
        | x when x < 130  -> 3
        | x when x < 170  -> 4
        | x when x < 220  -> 5
        | x when x < 270  -> 6
        | x when x < 320  -> 7
        | x when x < 370  -> 8
        | x when x < 430  -> 9
        | x when x < 500  -> 10
        | x when x < 600  -> 11
        | x when x < 750  -> 12
        | x when x < 900  -> 13
        | x when x < 1100 -> 14
        | x when x < 1300 -> 15
        | x when x < 1500 -> 16
        | x when x < 1750 -> 17
        | x when x < 2000 -> 18
        | x when x < 2250 -> 19
        | x when x < 2500 -> 20
        | x when x < 3000 -> 21
        | x when x < 3500 -> 22
        | x when x < 4000 -> 23
        | _               -> 24


