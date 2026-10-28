open Microsoft.AspNetCore.Builder
open Falco
open Vite.AspNetCore
open Controllers

let endpoints = IndexController.endpoints

let bldr = WebApplication.CreateBuilder()

bldr.Services.AddViteServices() |> ignore

let wapp = bldr.Build()

let isDevelopment = wapp.Environment.EnvironmentName = "Development"

wapp
    .UseRouting()
    .UseIf(isDevelopment, DeveloperExceptionPageExtensions.UseDeveloperExceptionPage)
    .UseIf(not (isDevelopment), FalcoExtensions.UseFalcoExceptionHandler ErrorController.serverException)
    .UseIf(isDevelopment, WebSocketMiddlewareExtensions.UseWebSockets)
    .UseIf(isDevelopment, fun (appl: IApplicationBuilder) -> appl.UseViteDevelopmentServer(true))
    .Use(DefaultFilesExtensions.UseDefaultFiles)
    .Use(StaticFileExtensions.UseStaticFiles)
    .UseFalco(endpoints)
    .UseFalcoNotFound(ErrorController.notFound)
    .Run()

    //TODO vite development server is not getting fable components properly
    //maybe write a specific middleware to intercept .fs requests

    //TODO add fsharp lint to whole solution or to the components project

    //TODO add web components

    //TODO add fss to web components

    //TODO change layout file more condusive to importing components

    //TODO Best way to reference components in Falco.Markup