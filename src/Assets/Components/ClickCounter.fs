module ClickCounter

open Fable.Core
open Browser.Types
open Firelight
open type Firelight.Lit
open Firelight.Html

let steps = [ 1; 10; 100 ]

[<AttachMembers>]
type ClickCounter() =
    inherit LitElement()

    // Changing a reactive property re-renders the component.
    static member properties =
        PropertyDeclarations.create [ "count", PropertyDeclaration<int>(); "label", PropertyDeclaration<string>() ]

    // Styles live in the component's shadow DOM, so they apply only to this component.
    static member styles =
        css
            $$"""
                :host { display: inline-flex; align-items: center; gap: 0.5rem; }
                button {
                    font: inherit; padding: 0.5rem 1rem; border: none; border-radius: 2rem; cursor: pointer;
                    color: white; background: var(--counter-color, rebeccapurple);
                }
            """

    member val count = 0 with get, set
    member val label = "Clicked" with get, set

    member this.Add(step: int) =
        this.dispatch (Event.customEvent ("count-changed", this.count + step))

    override this.render() =
        let stepButton step =
            template {
                button {
                    onClick (fun _ -> this.Add step)
                    $"+{step}"
                }
            }

        view {
            span { $"{this.label} {this.count}" }

            for step in steps do
                stepButton step
        }

defineElement<ClickCounter> "click-counter"
