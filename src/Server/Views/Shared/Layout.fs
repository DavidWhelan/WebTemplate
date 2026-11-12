module Views.Shared.Layout

open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Hosting
open Falco
open Falco.Markup
open Vite.AspNetCore


let devStyles =
    [ _link [ _href_ "assets/app.css"; _rel_ "stylesheet" ] ]


let prodStyles =

    [ _link [ _href_ "assets/app.css"; _rel_ "stylesheet" ] ]

let devScripts =
    [ _script [ _type_ "module"; _src_ "https://localhost:5173/@vite/client" ] []
      _script [ _type_ "module"; _src_ "js/datastar.js" ] []
      _script [ _type_ "module"; _src_ "Components/ClickCounter.fs?import" ] [] ]


let prodScripts =

    [ _script [ _type_ "module"; _src_ "js/datastar.js" ] []
      _script [ _type_ "module"; _src_ "Components/ClickCounter.fs" ] [] ]

let styles (ctx: HttpContext) =
    let env = ctx.Plug<IHostEnvironment>()

    match env.EnvironmentName with
    | "Development" -> devStyles
    | "Production" -> prodStyles
    | _ -> []

let scripts (ctx: HttpContext) =
    let env = ctx.Plug<IHostEnvironment>()

    match env.EnvironmentName with
    | "Development" -> devScripts
    | "Production" -> prodScripts
    | _ -> []

let template (content: XmlNode list) (ctx: HttpContext) =

    _html [ _lang_ "en" ] [ _head [] (styles ctx); _body [] (content @ scripts ctx) ]
