module FormattableExplore

open Fable.Core
open System
open System.Runtime.CompilerServices

[<Emit("fmt($0, $1)")>]
let makeFormatString (s: string) (values: obj[]) : FormattableString = jsNative

let v1 = 5
let v2 = true

let s1: FormattableString = $"this is {v1}"
let s2: FormattableString = $"this is {v2}"

let mutable index = -1

let cs1 =
    s1.GetStrings()
    |> Array.reduce (fun x1 x2 ->
        index <- index + 1
        x1 + "{" + index.ToString() + "}" + x2)

let cs2 =
    s2.GetStrings()
    |> Array.reduce (fun x1 x2 ->
        index <- index + 1
        x1 + "{" + index.ToString() + "}" + x2)

let cs = cs1 + cs2
let args = Array.append (s1.GetArguments()) (s2.GetArguments())

let combined = makeFormatString cs args

Console.WriteLine(args)

Console.WriteLine(cs1 + cs2)

Console.WriteLine(combined.GetStrings())
Console.WriteLine(combined.GetArguments())
