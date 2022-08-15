# Ali ZEROUALI
# Weather.Forecast.Cli 

Console Application which allows you to retrieve the weather forecast for all the cities provided by TUI Musement.

# Technologies

- [Microsoft ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-6.0?view=aspnetcore-6.0)
- [Serilog](https://serilog.net/)
- [Unit Testing Using XUnit](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test)

# How To Install

- You need to install [Visual Studio 2022](https://visualstudio.microsoft.com/fr/vs/#:~:text=Visual%20Studio%202022%20est%20le,plus%20fluide%20et%20plus%20r%C3%A9actif.)
- Install [Dot Net 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- Check if Dot Net 6 is well installed, for that you have to open you command line and tap the following command :
```
    dotnet --list-sdks
```
- Install [Git](https://git-scm.com/downloads) 
- Go to GitHub and get the solution from GitHub to your Local using `git clone`
- Once you have to application locally, go to `Weather.Forecast.Cli.sln` and double click
- Once you application is opened in Visual Studio, make sure that the Layer `Weather.Forecast.Cli` is set to `Set as Statup project`.
- Run you application

# Code organization : Clean Architecture 

The project is based on the architectural model ``Clean Architecture`` whose documentation is available on the Microsoft website : [Architect Modern Web Applications with ASP.NET Core and Azure](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/)

The organization of the code is based on the model of [Jason Taylor Clean Architecture](https://github.com/jasontaylordev/CleanArchitecture).

## Layer : Weather.Forecast.Cli

This layer is an entry point of a console application based on ASP .NET Core 6. This layer depends on both Application and Infrastructure layers, however, dependency on Infrastructure is only to support dependency injection . Therefore, only Program.cs should reference Infrastructure.

## Layer : Weather.Forecast.Domain

Layer that contains all entities, enumerations, exceptions, interfaces, types, and logic specific to the domain layer.

## Layer : Weather.Forecast.Application

Layer that contains all application logic. It depends on the domain layer, but has no dependency on any other layer or project. This layer defines interfaces that are implemented by external layers. For example, if the application needs to access a notification service, a new interface would be added to the application and an implementation would be created within the framework.

## Layer : Weather.Forecast.Infrastructure

Layer that contains classes for accessing external resources such as file systems, web services, smtp, etc. These classes must be based on interfaces defined within the application layer.

# Folder Structure

```
├── ForecastWeather.sln
├── README.md
├── src
│   ├── Weather.Forecast.Cli
│   ├── Weather.Forecast.Application
│   ├── Weather.Forecast.Domain
│   └── Weather.Forecast.Infrastructure
└── test
    ├── Weather.Forecast.Cli.UnitTests
    ├── Weather.Forecast.Application.UnitTests
    └── Weather.Forecast.Infrastructure.UnitTests
```
- **src** : product source code
- **test** : test projects
