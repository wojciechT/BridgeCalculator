namespace BridgeCalculator.Web.Controllers

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks

open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging

open BridgeCalculator.Core
open BridgeCalculator.Core.Types
open BridgeCalculator.Web.Mapping
open BridgeCalculator.Web.ViewModels

[<ApiController>]
[<Route("[controller]")>]
type TravellerController (logger : ILogger<TravellerController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member x.Get() : PairViewModel[] =
        PairService.getPairs ()
        |> Seq.map(mapPairToPairViewModel)
        |> Seq.toArray

    [<HttpPost>]
    member x.Post([<FromBody>] entry: TravellerEntryViewModel) =
        printfn "%O" entry
        "ok"
