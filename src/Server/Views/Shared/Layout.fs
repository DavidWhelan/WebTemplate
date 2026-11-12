module Views.Shared.Layout

open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Hosting
open Falco
open Falco.Markup
open Vite.AspNetCore

#if DEBUG
let debugScript =
    [ _script [ _type_ "module"; _src_ "https://localhost:5173/@vite/client" ] [] ]
#else
let debugScript = []
#endif

let styles = [ _link [ _href_ "assets/app.css"; _rel_ "stylesheet" ] ]

let scripts =
    [ _script [ _type_ "module"; _src_ "js/datastar.js" ] []
      _script [ _type_ "module"; _src_ "Components/ClickCounter.fs?import" ] [] ]
    @ debugScript

let template (content: XmlNode list) =

    _html [ _lang_ "en" ] [ _head [] styles; _body [] (content @ scripts) ]
