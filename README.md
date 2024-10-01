### Database

1. Step 1: add package

- intro Nuget
- link: https://www.nuget.org/

- dotnet add package Microsoft.EntityFrameworkCore.Design
- dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

- check csproj file

2. Step 2: Database file

- DatabaseContext

3. Step 3: Program.cs

- postgreslocal - property
- get connection string value

- appsetting.json

"ConnectionStrings": {
"Local": "Host=localhost;Database=demotest;Username=postgres;Password=andreaSDA"
},

- gitignore

4. Repository

- to talk to database

5. Category

6. Repo

- create category entity

7. DTO: create category

8. Mapper
   dotnet add package AutoMapper

   create Mapper profile

9. Services

10. Migration
    When: update or change the structure of your database to reflect changes made to your entity models in code

- **Initial Database Creation** (dotnet ef migrations add InitialCreate)
- Modifying Entity Models
- Schema Evolvement: add new table
- Fixing Issues or Optimizing the Database: add constraint

- dotnet tool install --global dotnet-ef

### Note

1.  Common

- Add database context (e.g., AddDbContext)
- Add repositories, services, and other application services
- Add controllers (e.g., AddControllers)

Asynchronous operations are generally more scalable, allowing the server to handle more requests concurrently without getting blocked by long-running tasks.

### Link

- http://localhost:5000/api/v1/categorys?offset=0&limit=10&search=c
