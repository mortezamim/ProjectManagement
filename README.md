# About the Project

Simple project that helps users to manage project and tasks 

I chose to use a Domain-Driven Design (DDD) approach for this application because it helps in creating a clear separation of concerns and allows for a better organization of the codebase. By structuring the code around domain concepts, DDD makes it easier to understand and maintain the application.

The use of a mediator pattern in this application enables a decoupled communication between different components. This promotes better code reusability, modularity, and the ability to easily add new features without tightly coupling different parts of the system.
# Technologies

- ASP.NET Core
- Entity Framework Core
- MediatR
- Fluent Validation
- Postgres

# Develop setup
To get started, follow the below steps:

1. Run database ```docker run --env=POSTGRES_DB=opm --env=POSTGRES_USER=postgres --env=POSTGRES_PASSWORD=postgres --volume=/var/lib/postgresql/data -p 5432:5432 --restart=no -d postgres```
2. Install .NET 7 SDK
3. Clone the Solution into your Local Directory
4. Navigate to the Web.API directory
5. Run the solution

# Production

use docker compose file
```docker compose up```

