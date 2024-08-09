# About the Project

Simple project that helps users to manage project and tasks 

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
5.Run the solution

# Production

use docker compose file
```docker compose up```

