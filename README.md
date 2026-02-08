# Project: WITS-Test



This project is a .NET Core 6 Web API microservice built using .NET Core 6.0 and C#, following clean architecture principles. The service uses Entity Framework Core (Code First) with SQLite as the database and implements the Repository Pattern for data access. Database schema changes are handled using EF Core Migrations, and some seed data is added for testing purposes.



**##Tech Stack**

.NET Core 6.0

C#

ASP.NET Core Web API

EF Core Code First with Migrations

SQLite Database (for local development)

Repository Pattern



**##Architecture \& Design**

The application follows a layered architecture:

**Controller Layer**

Handles HTTP requests and responses.



**Service / Business Logic Layer**

Validates username rules and enforces business constraints.



**Repository Layer**

Abstracts database operations and uses EF Core internally.



**Data Layer**

Uses EF Core Code First approach for schema definition and migrations.

This separation improves testability, readability, and long-term maintainability.



**## API Endpoints**



**GET** accountusers

&nbsp;- This endpoint gets all the existing users in the database



**GET** accountusers/validate

&nbsp;- This endpoint is to validate the given name whether it complies to the requirement (isValid), and checks if the user name is not already present (isAvailable)



**POST** accountusers

&nbsp;- This endpoint is to insert the username if it is valid and not already present or updates an existing user name with a new name if valid





**##Running the Application**

&nbsp;- Clone the repository

 - Restore NuGet packages

 - Run the application

 - Database is created automatically on first run

 - Migrations are applied

 - API available at configured localhost URL

 - Swagger available at /swagger

