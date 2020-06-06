namespace BridgeCalculator

open System

module Program = 
    let getImps () =
        printfn "Points: " 
        Console.ReadLine()
        |> int
        |> CalculateImps.matchWithImpValue
        |> printfn "%i" 
        
    let loop =
        Seq.initInfinite( fun _ -> getImps() )
    
    [<EntryPoint>]
    let main argv =
        loop |> Seq.toArray |> ignore
        0 // return an integer exit code
