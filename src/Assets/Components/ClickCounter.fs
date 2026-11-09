module ClickCounter

open Fable.Core
open Firelight
open type Firelight.Lit
open Firelight.Html

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
        view {
            button {
                onClick (fun _ -> this.count <- this.count + 1)
                $"Clicked {this.count} times"
            }
        }

defineElement<ClickCounter> "click-counter"
