module Controllers.ErrorController

open Views.Shared
open Falco
open Microsoft.AspNetCore.Http
open Falco.Markup

let notFound (ctx: HttpContext) =
    let markup = Layout.template [ _h1' "Not Found" ] ctx
    ctx |> Response.withStatusCode 404 |> Response.ofHtml markup

let serverException (ctx: HttpContext) =
    let markup = Layout.template [ _h1' "Server Error" ] ctx
    ctx |> Response.withStatusCode 500 |> Response.ofHtml markup
