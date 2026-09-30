# Showlio

Showlio is an ASP.NET Core Web API for creating and managing personal portfolios.

Users can build their portfolio with personal information, skills, projects, experience, education, certificates, services, and custom contact items. Portfolios can be associated with templates and published through a public username-based URL.

## Tech Stack

* **.NET 10**
* **ASP.NET Core Web API**
* **Entity Framework Core 10**
* **SQL Server**
* **ASP.NET Core Identity**
* **JWT Authentication**
* **FluentValidation**
* **Swagger / OpenAPI**

## Features

* User registration and authentication
* JWT-based authentication and role-based authorization
* Portfolio creation and management
* Portfolio section management
* Skills, projects, experience, education, certificates, services, and contact items
* Portfolio templates
* Admin template management
* Portfolio publishing and unpublishing
* Public portfolio access by username
* Portfolio ownership and authorization checks
* DTO validation with FluentValidation
* Entity Framework Core Code First with migrations

## Architecture

Showlio follows a layered architecture:

```text
Controller
    ↓
Service
    ↓
Repository
    ↓
EF Core / Database
```

### Controllers

Handle HTTP requests, responses, routing, authorization, and validation.

### Services

Contain application and business logic and coordinate between controllers and repositories.

### Repositories

Handle data access through Entity Framework Core.

The project uses a combination of **generic and specific repositories**, allowing shared CRUD operations while supporting resource-specific queries where needed.

### DTOs

Separate API request and response models from database entities.

### Mappers

Handle conversion between entities and DTOs.

### Validators

Contain FluentValidation rules for request DTOs.

### Current User & Authorization

`ICurrentUserService` retrieves the authenticated user's ID from the JWT claims.

`IPortfolioAuthorizationService` is used to verify that a portfolio belongs to the authenticated user before performing protected operations.

## Authentication and Authorization

Showlio uses **ASP.NET Core Identity** with **JWT authentication**.

The application supports two roles:

* `User`
* `Admin`

Users are assigned the `User` role during registration.

JWT tokens contain:

* User ID
* Username
* Role

Role-based authorization is used to protect admin-only operations.

For example:

```csharp
[Authorize(Roles = "Admin")]
```

JWT configuration is stored securely using **User Secrets** rather than committing sensitive configuration to the public repository.

## Account

The `AccountController` handles user account operations such as registration and authentication.

Authentication is based on ASP.NET Core Identity and JWT bearer tokens.

## Portfolio

Each user can create and manage their portfolio.

A portfolio contains:

* Personal information
* Job title
* Description
* Profile image reference
* Selected template
* Publishing status
* Section visibility settings

The current application enforces **one portfolio per user at the service layer**, while the database relationship is designed without making the user-to-portfolio relationship inherently single-record.

Portfolio ownership is checked before protected portfolio operations.

## Portfolio Sections

A portfolio can contain the following sections:

### Skills

Skills can be created, updated, retrieved, and deleted within a portfolio.

### Projects

Projects contain information such as:

* Title
* Description
* Image
* Project link
* Display order

### Experience

Experience entries contain:

* Company
* Position
* Description
* Start and end dates
* Current employment status
* Display order

### Education

Education entries contain:

* Institution
* Degree
* Field
* Start and end dates
* Display order

### Certificates

Certificates contain:

* Title
* Description
* Image
* Certificate link
* Display order

### Services

Services contain:

* Title
* Description
* Optional link
* Display order

### Contact Items

Contact items allow users to define custom contact information using a label and value.

Examples include GitHub, LinkedIn, location, or other contact information.

Contact items are always included in the public portfolio response because they are used as part of the portfolio footer.

## Templates

Portfolios can use templates.

Templates contain:

* Title
* Genre
* Description
* Color theme
* Active status

Template titles are unique.

### User Access

Authenticated users can retrieve active templates.

The `IsActive` property is not exposed through the normal user template DTO.

### Admin Access

Administrators can:

* Create templates
* Update templates
* Activate templates
* Deactivate templates
* Delete templates
* Retrieve all templates, including inactive templates

Deleting a template that is currently used by a portfolio is prevented.

## Public Portfolio API

Published portfolios can be accessed without authentication using the owner's username:

```text
GET /api/public/{username}
```

The public endpoint:

* Looks up the portfolio using the username
* Only returns published portfolios
* Returns `404 Not Found` if the username does not exist or the portfolio is unpublished
* Includes the selected `TemplateId`
* Does not expose the portfolio's publishing status
* Includes section visibility settings
* Returns an empty collection for disabled sections
* Returns section data when the section is enabled
* Always includes contact items

The endpoint does not expose private user information.

## API Endpoints

The API is organized into controllers based on application areas:

* **Account** — authentication and account operations
* **Portfolio** — portfolio management
* **Skill** — portfolio skills
* **Project** — portfolio projects
* **Experience** — portfolio experience
* **Education** — portfolio education
* **Certificate** — portfolio certificates
* **Service** — portfolio services
* **Contact Item** — custom contact information
* **Template** — template management
* **Public Portfolio** — publicly accessible published portfolios

The exact routes and HTTP methods are defined by the corresponding controllers.

## Validation

Request DTOs are validated using **FluentValidation**.

Validation rules are organized by resource, including:

* Portfolio
* Skill
* Project
* Experience
* Education
* Certificate
* Service
* Contact Item
* Template

Validation is performed before the corresponding service operation is executed.

## Database

Showlio uses **Entity Framework Core Code First** with SQL Server.

The database contains:

* ASP.NET Core Identity tables
* Portfolio data
* Portfolio section entities
* Template data

Entity relationships include:

* User → Portfolios
* Portfolio → Template
* Portfolio → Skills
* Portfolio → Projects
* Portfolio → Experiences
* Portfolio → Educations
* Portfolio → Certificates
* Portfolio → Services
* Portfolio → Contact Items

Child portfolio entities are configured to be deleted with their parent portfolio where appropriate.

Template deletion is restricted when a template is currently referenced by a portfolio.

Entity Framework migrations are stored in the `Migrations` directory.

To apply migrations:

```bash
dotnet ef database update
```

## Project Structure

```text
Showlio.api/
├── Controllers/
├── Data/
├── Dtos/
├── Enums/
├── Globals/
├── Identity/
├── Interfaces/
│   ├── IRepository/
│   └── IServices/
├── Mappers/
├── Migrations/
├── Models/
├── Repositories/
├── Results/
├── Services/
├── Validators/
├── appsettings.json
├── Program.cs
└── ServicesRegistration.cs
```

### Folder Responsibilities

| Folder         | Responsibility                                                 |
| -------------- | -------------------------------------------------------------- |
| `Controllers`  | HTTP endpoints, request handling, responses, and authorization |
| `Data`         | Entity Framework Core database context and configuration       |
| `Dtos`         | API request and response models                                |
| `Enums`        | Application enumerations                                       |
| `Globals`      | Shared application constants                                   |
| `Identity`     | ASP.NET Core Identity classes and configuration                |
| `Interfaces`   | Repository and service contracts                               |
| `Mappers`      | Entity and DTO mapping                                         |
| `Migrations`   | Entity Framework Core migrations                               |
| `Models`       | Database/domain entities                                       |
| `Repositories` | Data-access implementations                                    |
| `Results`      | Service operation result/status types                          |
| `Services`     | Application and business logic                                 |
| `Validators`   | FluentValidation validators                                    |

## Development and Technical Decisions

### Repository Pattern

The project uses generic and resource-specific repositories.

Resource-specific repositories provide portfolio-scoped queries such as retrieving an entity by both its ID and portfolio ID. This keeps ownership boundaries explicit and supports the portfolio-based data model.

### Service Layer

Business logic is kept in services instead of placing it directly inside controllers.

Controllers are responsible mainly for HTTP concerns, while services handle application rules and coordinate data access.

### Dependency Injection

Dependency injection registrations are organized through `ServicesRegistration` extension methods.

Related repositories, services, and validators are registered together by application area, keeping `Program.cs` focused on application startup configuration.

### Portfolio Ownership

Protected portfolio operations use the authenticated user's identity and portfolio authorization checks rather than relying only on resource IDs.

This prevents users from accessing or modifying portfolio resources that do not belong to them.

### DTO Mapping

Entity-to-DTO and DTO-to-entity/update mapping is handled through mapper methods, keeping mapping logic separate from controllers and services.

## Running the Project

### Prerequisites

* .NET 10 SDK
* SQL Server
* Entity Framework Core CLI tools

### Configuration

Configure the required database connection and JWT settings through local configuration/User Secrets.

Sensitive values should not be committed to the repository.

### Database Setup

Run:

```bash
dotnet ef database update
```

### Run the API

```bash
dotnet run
```

Swagger/OpenAPI can then be used to explore and test the API during development.

## Project Status

Showlio is a functional backend project implementing portfolio management, authentication, authorization, templates, validation, and a public portfolio API using ASP.NET Core and Entity Framework Core.
