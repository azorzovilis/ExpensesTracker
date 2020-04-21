# Expenses tracker

Expenses tracker is a simple CRUD application written in .Net Core 3.1, EF Core and Angular 9 used to log expenses.

## Running the app

In order to view the app you will need to have both the server and the client instances running.

### Expenses Tracker API
In order to run the API, open the ExpensesTrackerAPI.sln solution with Visual Studio 2019 and run the migrations using `dotnet ef database update`. 
You might have to install `dotnet tool install --global dotnet-ef` first. The originial migrations are created using `dotnet ef migrations add InitialCreate`
It is using a local SQL Server DB. After checking that the DB has been created, run the ExpensesTrackerAPI project to load the API. The API is configured
to run under `https://localhost:44330/`.

### Expenses Tracker Client

You need to have Node.js and Angular CLI installed in order to run the app. 
For more installation info check [here](https://angular.io/guide/setup-local)

In order to view the client app, run `ng serve -o` for a dev server using a terminal under ExpensesTracker/ExpensesTrackerClient. 
This will open `http://localhost:4200/`.