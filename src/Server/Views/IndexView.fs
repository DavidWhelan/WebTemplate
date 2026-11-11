module Views.IndexView

open Microsoft.AspNetCore.Http
open Falco.Markup
open Falco.Markup.Components
open Falco.Datastar
open Views.Shared
open Routes

let page: HttpContext -> XmlNode =
    Layout.template
        [ _h1 [] [ _text "Example: Click Counter" ]
          _button [ _id_ "click-counter"; Ds.onClick (Ds.get IndexRoutes.clickRoute) ] [ _text "Click Me" ] ]

let count = Signal.browser<int> "count"

let clickResponse =
    _div
        [ _id_ "click-counter" ]
        [ _clickCounter
              [ _label_ "Liked"
                Ds.attr' ("count", (Expr.read count))
                Ds.onEvent ("count-changed", Stmt.set count (Expr.unsafeRaw "evt.detail")) ]
              [] ]
