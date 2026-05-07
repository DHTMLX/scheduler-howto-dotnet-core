# DHTMLX Scheduler with ASP.NET Core

Backend example for [DHTMLX Scheduler](https://dhtmlx.com/docs/products/dhtmlxScheduler/) implemented with ASP.NET Core 8 and Entity Framework Core.

**Related tutorial**:
[https://docs.dhtmlx.com/scheduler/integrations/dotnet/howtostart-dotnet-core/](https://docs.dhtmlx.com/scheduler/integrations/dotnet/howtostart-dotnet-core/)

## Features

- Basic CRUD with REST/JSON data processor
- Dynamic loading by date range
- Recurring events using the rrule-based engine (Scheduler 7.1+)
- Error-handling middleware
- XSS protection on the event `text` field

## Requirements

- Visual Studio 2022
- .NET 8 SDK
- SQL Server LocalDB (or any SQL Server instance reachable via the connection string)

## How to run

Clone the repository:

```
git clone https://github.com/DHTMLX/scheduler-howto-dotnet-core.git
cd scheduler-howto-dotnet-core
```

Open `SchedulerApp.sln` in Visual Studio 2022, restore NuGet packages and run the project (F5).

If you prefer the command line:

```
dotnet restore
dotnet run --project SchedulerApp/SchedulerApp.csproj
```

By default the app uses the LocalDB connection string from `SchedulerApp/appsettings.json` and recreates the database on every start. Update the connection string if you want to point it at a different SQL Server instance.

## Routes

- `/basic.html` - basic CRUD sample
- `/recurring.html` - recurring events sample (rrule-based)

## Tutorial

A complete tutorial is available at https://docs.dhtmlx.com/scheduler/howtostart_dotnet_core.html.

## License

Source code in this repository is released under the **MIT License**.

DHTMLX Scheduler is a commercial library - use under a valid [DHTMLX license](https://dhtmlx.com/docs/products/licenses.shtml) or evaluation agreement.

## Useful links

- [DHTMLX Scheduler product page](https://dhtmlx.com/docs/products/dhtmlxScheduler/)
- [Documentation](https://docs.dhtmlx.com/scheduler/)
- [Blog](https://dhtmlx.com/blog/)
- [Forum](https://forum.dhtmlx.com/)
