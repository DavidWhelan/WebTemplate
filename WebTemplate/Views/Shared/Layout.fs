module Views.Shared.Layout

open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Hosting
open Falco
open Falco.Markup
open Vite.AspNetCore


let devStyles (ctx: HttpContext) =
    [ _link [ _href_ "assets/css/app.css"; _rel_ "stylesheet" ] ]


let prodStyles (ctx: HttpContext) =
    let manifest = ctx.Plug<IViteManifest>()

    [ _link [ _href_ $"""/{manifest["css/app.css"].File}"""; _rel_ "stylesheet" ] ]

let devScripts (ctx: HttpContext) =
    [ _script [ _type_ "module"; _src_ "https://localhost:5173/@vite/client" ] []
      _script [ _type_ "module"; _src_ "assets/js/app.ts" ] [] ]


let prodScripts (ctx: HttpContext) =
    let manifest = ctx.Plug<IViteManifest>()

    [ _script [ _type_ "module"; _src_ $"""/{manifest["js/app.ts"].File}""" ] [] ]

let styles (ctx: HttpContext) =
    let env = ctx.Plug<IHostEnvironment>()

    []
    @ match env.EnvironmentName with
      | "Development" -> devStyles ctx
      | "Production" -> prodStyles ctx
      | _ -> []

let scripts (ctx: HttpContext) =
    let env = ctx.Plug<IHostEnvironment>()

    []
    @ match env.EnvironmentName with
      | "Development" -> devScripts ctx
      | "Production" -> prodScripts ctx
      | _ -> []

let template (content: XmlNode list) (ctx: HttpContext) =

    _html [ _lang_ "en" ] [ _head [] ([] @ styles ctx); _body [] (content @ scripts ctx) ]
