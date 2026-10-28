module Views.IndexView

open Microsoft.AspNetCore.Http
open Falco.Markup
open Falco.Markup.Components
open Falco.Datastar
open Views.Shared
open Routes

let page: HttpContext -> XmlNode =
    Layout.template
        [ _h1 [] [ _text "Example: Hello World" ]
          _button [ _id_ "hello"; Ds.onClick (Ds.get IndexRoutes.clickRoute) ] [ _text "Click Me" ] ]

let clickResponse = _h2 [ _id_ "hello" ] [ _clickCounter [] []]
