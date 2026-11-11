module ClickCounter

open Fable.Core
open Firelight
open type Firelight.Lit
open Firelight.Html
open FormattableExplore
open System

[<AttachMembers>]
type ClickCounter() =
    inherit LitElement()

    // Changing a reactive property re-renders the component.
    static member properties = PropertyDeclarations.create [ "count", PropertyDeclaration<int>() ]

    // Styles live in the component's shadow DOM, so they apply only to this component.
    static member styles =
        css
            $$"""
                button { font: inherit; padding: 0.5rem 1rem; border-radius: 0.5rem; cursor: pointer; }
            """

    member val count = 0 with get, set

    override this.render() =
        let s1: FormattableString =
            $"""<button @click={fun _ -> this.count <- this.count + 1}>"""

        let s2: FormattableString = $"""Clicked {this.count} times"""
        let s3: FormattableString = $"""</button>"""
        let cs1 = combineFormatStrings s1 s2
        let cs2 = combineFormatStrings cs1 s3
        html cs2

//view {
//    button {
//        onClick (fun _ -> this.count <- this.count + 1)
//        $"Clicked {this.count} times"
//    }
//}

defineElement<ClickCounter> "click-counter"
