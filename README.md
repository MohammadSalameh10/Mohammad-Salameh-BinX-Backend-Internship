# BinX Backend Internship

This repository contains my daily work, exercises, documentation, and projects completed during the BinX Backend Internship program.

## Internship Progress

| Week   | Focus                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     | Documentation                               |
| ------ | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- | ------------------------------------------- |
| Week&nbsp;1 | .NET setup, C# fundamentals, OOP, collections, LINQ basics, async/await, exception handling, and Git workflow                                                                                                                                                                                                                                                                                                                                                                                            | [View Week 1](./BinX%20Internship/Week%201) |
| Week&nbsp;2 | Generics, advanced collections, advanced LINQ, asynchronous programming, concurrency, ASP.NET Core Web APIs, routing, middleware, and dependency injection                                                                                                                                                                                                                                                                                                                                                | [View Week 2](./BinX%20Internship/Week%202) |
| Week&nbsp;3 | REST API design, SQL Server schema design, database normalization, Entity Framework Core Code-First development, migrations, asynchronous CRUD operations, request validation, service layers, Postman environments, automated API testing, and documentation                                                                                                                                                                                                                                               | [View Week 3](./BinX%20Internship/Week%203) |
| Week&nbsp;4 | ASP.NET Core Identity, user registration, password hashing and validation, JWT authentication, token issuance, protected routes, role-based access control, claims-based and policy-based authorization, Postman token reuse, FluentValidation, business validation rules, structured validation errors, rate limiting, CORS, HTTPS redirection, HSTS, and SQL injection prevention practices | [View Week 4](./BinX%20Internship/Week%204) |
| Week&nbsp;5 | Phase 3 capstone project selection, E-Commerce Backend scope, xUnit unit testing, `[Fact]`, `[Theory]`, `[InlineData]`, Arrange-Act-Assert, and Visual Studio Test Explorer | [View Week 5](./BinX%20Internship/Week%205) |

## Repository Structure

```text
BinX Internship/
├── Week 1/
│   ├── README.md
│   ├── Day 1/
│   ├── Day 2/
│   ├── Day 3/
│   ├── Day 4/
│   └── Day 5/
├── Week 2/
│   ├── README.md
│   ├── Day 1/
│   ├── Day 2/
│   ├── Day 3/
│   ├── Day 4/
│   └── Day 5/
├── Week 3/
│   ├── README.md
│   ├── Day 1/
│   ├── Day 2/
│   ├── Day 3/
│   ├── Day 4/
│   └── Day 5/
├── Week 4/
│   ├── README.md
│   ├── Day 1/
│   ├── Day 2/
│   ├── Day 3/
│   ├── Day 4/
│   └── Day 5/
└── Week 5/
    ├── README.md
    └── Day 1/
```

Each week contains a summary README, and each completed day contains its own task documentation and project files when implementation is required.

## Current Learning Areas

### C# and .NET

* C# and .NET fundamentals
* Object-oriented programming
* Collections and LINQ
* Generic programming
* Generic constraints and collection interfaces
* Deferred and immediate LINQ execution
* Advanced LINQ operations with `GroupBy`, `Join`, and `SelectMany`
* Asynchronous programming with `async` and `await`
* Sequential and concurrent execution
* Running independent operations with `Task.WhenAll`
* Cancelling asynchronous operations with `CancellationToken`

### ASP.NET Core

* ASP.NET Core Web API development
* The minimal hosting model in `Program.cs`
* Controllers and Minimal APIs
* Routes, route parameters, and HTTP verbs
* Middleware pipeline and execution ordering
* Custom request-logging middleware
* Dependency Injection and service lifetimes
* Constructor injection using interfaces
* ASP.NET Core built-in Rate Limiting
* Fixed-window rate limiting policies
* Applying endpoint rate-limit policies with `EnableRateLimiting`
* CORS configuration using named policies
* HTTPS redirection
* HSTS configuration
* Security hardening in the middleware pipeline

### REST APIs

* REST API design principles
* Resource-based API modeling
* RESTful resource naming conventions
* Nested resource relationships
* Correct HTTP status-code usage
* URL-based API versioning

### SQL Server and Database Design

* SQL Server database design
* Database normalization using `1NF`, `2NF`, and `3NF`
* Primary keys and foreign keys
* One-to-many database relationships
* SQL Server column-type selection
* Entity Relationship Diagrams
* Database design using `dbdiagram.io`
* Database implementation using SQL Server Management Studio

### Entity Framework Core

* Entity Framework Core with SQL Server
* Entity models and navigation properties
* `DbContext` and `DbSet<T>`
* Entity configuration using Fluent API
* Connection strings and database configuration
* Code-First migrations
* Generating migrations with `Add-Migration`
* Applying migrations with `Update-Database`
* Verifying generated tables using SQL Server Object Explorer
* Asynchronous CRUD operations
* Creating records with `Add` and `SaveChangesAsync`
* Read-only queries using `AsNoTracking`
* Asynchronous queries using `ToListAsync` and `FirstOrDefaultAsync`
* Entity Framework Core change tracking
* Updating tracked entities
* Deleting entities with `Remove`
* EF Core query parameterization
* SQL injection prevention with LINQ and parameterized queries
* Reviewing raw SQL usage for security risks

### API Architecture and Validation

* Request models for create and update operations
* Request validation using Data Annotations
* Input validation using FluentValidation
* Dedicated validator classes using `AbstractValidator<T>`
* Defining validation rules using `RuleFor`
* Creating custom validation messages using `WithMessage`
* Business-oriented validation rules
* Conditional validation using `When`
* Separating validation logic from request models
* Automatic request validation before controller execution
* Structured `400 Bad Request` validation responses
* Service interfaces and service implementations
* Separating database operations from API controllers
* Registering application services using Dependency Injection
* Returning `200 OK`, `201 Created`, `204 No Content`, `400 Bad Request`, `401 Unauthorized`, `403 Forbidden`, `404 Not Found`, and `429 Too Many Requests`
* Returning resource locations using `CreatedAtAction`
* Managing delete order for related database records

### ASP.NET Core Identity

* ASP.NET Core Identity integration
* `IdentityUser` and `IdentityRole`
* `IdentityDbContext<IdentityUser>`
* Identity integration with Entity Framework Core
* Identity database migrations
* Identity service registration
* `UserManager<IdentityUser>`
* `RoleManager<IdentityRole>`
* `SignInManager<IdentityUser>`
* User registration using `UserManager.CreateAsync`
* Credential verification using `CheckPasswordSignInAsync`
* Role creation using `RoleManager`
* Assigning users to roles using `UserManager.AddToRoleAsync`
* Retrieving user roles using `UserManager.GetRolesAsync`
* Password hashing using ASP.NET Core Identity
* PBKDF2 password hashing
* Unique password salts
* Built-in password validation
* Authentication and authorization middleware

### JWT Authentication

* JWT structure: Header, Payload, and Signature
* JWT claims
* User ID and email claims
* Role claims
* Custom permission claims
* Login using ASP.NET Core Identity
* JWT token generation
* JWT signing using HMAC SHA-256
* JWT issuer and audience configuration
* Short-lived access tokens
* 15-minute token expiration
* JWT Bearer Authentication
* JWT issuer validation
* JWT audience validation
* JWT lifetime validation
* JWT signing-key validation
* Protecting API endpoints using `[Authorize]`
* Sending JWTs using the Bearer authentication scheme
* Returning `401 Unauthorized` for invalid credentials
* Rejecting missing or expired JWTs
* Decoding JWTs and verifying claims

### Authorization

* Protecting controllers and endpoints using `[Authorize]`
* Understanding authentication vs authorization
* Role-based access control
* Restricting endpoints using `[Authorize(Roles = "Admin")]`
* Understanding `401 Unauthorized` vs `403 Forbidden`
* Claims-based authorization
* Custom permission claims
* Policy-based authorization
* Named authorization policies
* Requiring claims using `RequireClaim`
* Applying policies using `[Authorize(Policy = "...")]`
* Combining JWT authentication with authorization rules

### FluentValidation

* Comparing DataAnnotations and FluentValidation
* Creating dedicated validator classes
* `AbstractValidator<T>`
* `RuleFor`
* `NotEmpty`
* `MaximumLength`
* `GreaterThan`
* `When`
* `WithMessage`
* Validating `CreateTaskRequest`
* Validating `UpdateTaskRequest`
* Validating positive user IDs
* Validating future due dates
* Registering validators using assembly scanning
* Automatic FluentValidation integration
* Structured validation error responses
* Preventing invalid requests from reaching controller actions

### API Security Hardening

* Rate limiting to reduce excessive request patterns
* Stricter rate limiting for login endpoints
* Returning `429 Too Many Requests` when limits are exceeded
* Named CORS policies
* Restricting allowed frontend origins
* Testing allowed and disallowed origins
* Understanding browser-enforced CORS behavior
* HTTPS redirection
* Testing `307 Temporary Redirect`
* HSTS outside the Development environment
* Understanding Content-Security-Policy as a security concept
* SQL injection prevention
* EF Core automatic query parameterization
* Reviewing `FromSqlRaw`, `ExecuteSqlRaw`, `FromSqlInterpolated`, and manually written SQL usage

### Postman and API Testing

* API design and testing with Postman
* Testing successful and invalid API requests
* Organizing Postman collections by API resource
* Testing success paths and realistic error paths
* Writing automated Postman tests using `pm.test`
* Asserting HTTP status codes and response properties
* Creating Postman environments
* Using the `baseUrl` environment variable
* Exporting and sharing Postman collections
* Testing JWT login and token issuance
* Sending Bearer tokens to protected endpoints
* Testing expired JWT rejection
* Storing JWTs in environment variables
* Capturing login tokens using Post-response scripts
* Reusing `{{token}}` automatically in protected requests
* Testing role-based authorization
* Testing policy-based authorization
* Testing FluentValidation rules individually
* Verifying field-specific validation messages
* Verifying structured `400 Bad Request` responses
* Testing rate-limit rejection with `429 Too Many Requests`
* Testing allowed and disallowed CORS origins
* Inspecting `Access-Control-Allow-Origin`
* Disabling automatic redirect following to test HTTPS redirection
* Verifying `307 Temporary Redirect` and the HTTPS `Location` header

### Phase 3 Capstone Project

* Selected an E-Commerce Backend as the Phase 3 capstone project
* Scoped the project around product catalog management, shopping cart operations, and order processing
* Defined a realistic scope for completion by Week 9
* Reviewed the required professional baseline for the final backend project
* Planned to reuse authentication, authorization, validation, database, security, testing, deployment, and documentation patterns from previous weeks

### Unit Testing with xUnit

* Unit testing small units of application logic independently
* Creating a dedicated xUnit test project
* Referencing the main ASP.NET Core API project from the test project
* Writing tests using `[Fact]`
* Writing parameterized tests using `[Theory]`
* Providing multiple test cases using `[InlineData]`
* Organizing tests using the Arrange-Act-Assert pattern
* Testing pure service methods without external dependencies
* Using descriptive test naming based on method, scenario, and expected result
* Running tests using Visual Studio Test Explorer
* Running individual tests and all tests
* Reviewing passed, failed, and skipped test results

### Development Tools and Workflow

* Swagger and OpenAPI documentation
* Visual Studio
* Visual Studio Test Explorer
* SQL Server Management Studio
* Visual Studio Package Manager Console
* Postman
* jwt.io
* xUnit
* Git and GitHub workflows

## Author

Mohammad Salameh
