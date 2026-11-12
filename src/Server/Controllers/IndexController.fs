module Controllers.IndexController

open Views.IndexView
open Routes.IndexRoutes
open Microsoft.AspNetCore.Http
open Falco
open Falco.Routing

let pageHandler = Response.ofHtml page

let clickHandler: HttpHandler = Response.ofHtml clickResponse

let endpoints = [ get pageRoute pageHandler; get clickRoute clickHandler ]
