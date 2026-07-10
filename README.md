# Helm

Helm is an equipment management system built with ASP.NET Core and React. The project is a playground for Clean Architecture, CQRS (MediatR), EF Core and JWT authentication via ADFS.

The project contains an ASP.NET Core API, a PostgreSQL-backed persistence layer, and a TypeScript SPA frontend. It is built as a pet project for experimenting with backend architecture, authentication, validation, and a simple admin UI.

> Status: work in progress. The frontend is currently React-based, with a planned migration to Vue.

## Features

- JWT-protected API authentication through ADFS/OIDC
- User management:
  - list users
  - create users
  - update user data
  - enable or disable users
  - update user passwords
  - delete users
- Role management:
  - list roles
  - create roles
  - update roles
  - delete roles
- User-role assignment and removal
- PostgreSQL persistence with Entity Framework Core migrations
- CQRS-style request handling with MediatR
- Request validation with FluentValidation
- SPA frontend with MSAL authentication and data tables

## Tech Stack

### Backend

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL / Npgsql
- MediatR
- FluentValidation
- AutoMapper
- JWT Bearer authentication
- ADFS / OIDC

### Frontend

Current implementation:

- TypeScript
- Vite
- React
- MSAL Browser / MSAL React
- MobX
- TanStack Query
- TanStack Router
- TanStack Table
- Tailwind CSS
- shadcn-style UI components

Planned frontend direction:

- Vue 3
- Vue Router
- Vue reactivity instead of MobX
- MSAL Browser without React-specific wrappers

## Repository Structure

```text
Helm/
  Helm.Api/       ASP.NET Core API, controllers, authentication, API pipeline
  Helm.Core/      Application logic, domain entities, infrastructure, migrations
  Helm.Tests/     Backend tests
  Helm.Web/       SPA frontend
  Helm.slnx       Solution file

```
## Requirements

- .NET 10 SDK
- Node.js and npm
- PostgreSQL
- ADFS or another compatible OIDC identity provider
- MediatR license key, if required by the installed package version

## Backend Configuration

The API expects an appsettings.json file with a Settings section.

Create Helm.Api/appsettings.json:
```text
{
  "Settings": {
    "AllowedOrigins": "https://localhost",
    "ConnectionString": "Host=localhost;Port=5432;Database=helm;Username=postgres;Password=postgres",
    "MediatRLicense": "your-mediatr-license-key",
    "ADFS": {
      "ADFSDomain": "https://your-adfs-domain/adfs/",
      "ADFSAudience": "your-api-audience",
      "ADFSIssuer": "https://your-adfs-domain/adfs/services/trust"
    }
  }
}
```
Do not commit real secrets, production connection strings, certificates, or identity provider settings.

## Frontend Configuration

Create Helm.Web/.env.local:

```text
VITE_ADFS_CLIENT_ID=your-client-id
VITE_ADFS_KNOWN_AUTHORITY=your-adfs-host
VITE_ADFS_AUTHORITY=https://your-adfs-domain/adfs/
VITE_ADFS_REDIRECT_URL=https://localhost
VITE_ADFS_PROTOCOL_MODE=OIDC
VITE_ADFS_AUTHORITY_METADATA=

VITE_AUTH_API_URL=https://localhost:5001/api/authentication
VITE_USERS_API_URL=https://localhost:5001/api/v1/users
VITE_USER_ROLES_API_URL=https://localhost:5001/api/v1/userroles
```
The current Vite configuration expects a local HTTPS certificate file named cert_localhost.pfx inside Helm.Web.

Either place the certificate there or adjust Helm.Web/vite.config.ts to use your own local HTTPS setup.

## Database Setup

Apply Entity Framework Core migrations:
```text
dotnet ef database update --project ./Helm.Core --startup-project ./Helm.Api 
```

If the EF CLI is not installed:
```text
dotnet tool install --global dotnet-ef
```

## Running the Backend
From the repository root:
```text
dotnet restore Helm.slnx
dotnet build Helm.slnx
```
Then run the API:
```text
cd Helm.Api
dotnet run
```

## Running the Frontend
```text
cd Helm.Web
npm install
npm run dev
```

## Running Tests
Backend tests:
```text
dotnet test Helm.Tests/Helm.Tests.csproj
```
Frontend checks:
```text
cd Helm.Web
npm run lint
npm run build
```
## API Overview
Authentication:
```text
POST /api/authentication
```
Users:
```text
GET    /api/v1/users
POST   /api/v1/users
PUT    /api/v1/users
PUT    /api/v1/users/{id}/status
put    /api/v1/users/role
PUT    /api/v1/users/{userId}/role/{roleId}
DELETE /api/v1/users/{userId}/role/{roleId}
DELETE /api/v1/users/{id}
```
User roles:
```text
GET    /api/v1/userroles
POST   /api/v1/userroles
PUT    /api/v1/userroles
DELETE /api/v1/userroles/{id}
```
## Current Limitations
- The frontend is still React-based and is planned to be migrated to Vue.
- Local development requires manual ADFS/OIDC configuration.
- The project does not yet include Docker Compose for PostgreSQL.
- CI/CD is not configured yet.
- Some API error responses and frontend HTTP status handling still need cleanup.

## Roadmap
- Migrate the frontend from React to Vue 3
- Replace MobX with Vue reactivity or Pinia where needed
- Use @azure/msal-browser directly without framework-specific wrappers
- Add Docker Compose for local PostgreSQL
- Add GitHub Actions for build, lint, and tests
- Add better API error responses
- Add .env.example and appsettings.example.json
- Improve project documentation with screenshots

## License

MIT