# Week 4 — Day 1: ASP.NET Core Identity & User Registration

## Overview

This exercise focused on integrating ASP.NET Core Identity into the existing Task Tracker API and implementing user registration.

ASP.NET Core Identity was configured with Entity Framework Core and SQL Server. A registration endpoint was implemented using `UserManager<IdentityUser>` to create users, validate passwords, securely hash them, and store the Identity users in the database.

The registration endpoint was tested in Postman with both valid credentials and a deliberately weak password.

## Learning Objectives

- Explain what ASP.NET Core Identity provides out of the box.
- Configure ASP.NET Core Identity with Entity Framework Core.
- Add the Identity database schema using EF Core migrations.
- Register Identity services using Dependency Injection.
- Implement user registration using `UserManager.CreateAsync`.
- Understand how Identity handles password hashing.
- Understand the purpose of PBKDF2 and password salts.
- Return meaningful validation errors for invalid registration requests.
- Test successful and invalid registration requests using Postman.

## ASP.NET Core Identity

ASP.NET Core Identity is a built-in membership system for managing application users and authentication-related data.

It provides features such as:

- User storage
- Password hashing
- Role management
- Account confirmation

Using ASP.NET Core Identity avoids manually implementing security-critical functionality such as user storage and password hashing.

Instead of creating custom password-handling logic, the application can rely on Identity's built-in and tested implementation.

## NuGet Package

The following NuGet package was added to the project:

```text
Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

### Microsoft.AspNetCore.Identity.EntityFrameworkCore

This package integrates ASP.NET Core Identity with Entity Framework Core.

It allows Identity users, roles, and other Identity-related data to be stored in the application's database using Entity Framework Core.

## Identity & Entity Framework Core

The existing `AppDbContext` was updated to inherit from:

```csharp
IdentityDbContext<IdentityUser>
```

Example:

```csharp
public class AppDbContext : IdentityDbContext<IdentityUser>
{
}
```

### IdentityDbContext

`IdentityDbContext` is a DbContext provided by ASP.NET Core Identity.

By inheriting from it, the application gains the Entity Framework Core configuration required for storing Identity data such as users and roles.

The Task Tracker's existing entities remain in the same context:

```csharp
public DbSet<User> Users => Set<User>();

public DbSet<TaskItem> Tasks => Set<TaskItem>();

public DbSet<Comment> Comments => Set<Comment>();
```

This means the database contains both the existing Task Tracker entities and the ASP.NET Core Identity schema.

The context also calls:

```csharp
base.OnModelCreating(modelBuilder);
```

This allows the Identity base context to configure its model alongside the Task Tracker application's existing entity configuration.

## IdentityUser and IdentityRole

The project uses:

```csharp
IdentityUser
```

to represent users managed by ASP.NET Core Identity.

It also uses:

```csharp
IdentityRole
```

to represent roles that can be assigned to Identity users.

They are configured when registering Identity:

```csharp
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
```

For this exercise, `IdentityUser` is used directly rather than creating a custom Identity user class.

## Identity Database Schema

After `AppDbContext` was changed to use `IdentityDbContext`, Entity Framework Core detected the additional Identity entities.

A new migration was created:

```powershell
Add-Migration AddIdentity
```

The migration was then applied to SQL Server:

```powershell
Update-Database
```

This added the ASP.NET Core Identity schema alongside the existing application tables.

The Identity schema includes tables for data such as:

```text
Users
Roles
UserRoles
```

along with supporting Identity tables.

The existing Task Tracker tables remained in the same database.

## Identity Service Registration

Identity services were registered in `Program.cs`:

```csharp
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
```

This configuration connects:

```text
IdentityUser  → Application users
IdentityRole  → Application roles
AppDbContext  → Identity data storage through Entity Framework Core
```

The authentication and authorization middleware were also added to the request pipeline:

```csharp
app.UseAuthentication();
app.UseAuthorization();
```

Authentication runs before authorization so that the application can first identify the user before checking what that user is allowed to access.

## Registration Request

A separate request model was created for user registration:

```csharp
public class RegisterRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
```

The request model defines the data accepted by the registration endpoint.

`[Required]` ensures that the values are provided, while `[EmailAddress]` validates that the submitted email has a valid email format.

Example request:

```json
{
  "email": "testuser@example.com",
  "password": "Test@12345"
}
```

## UserManager

ASP.NET Core Identity provides:

```csharp
UserManager<IdentityUser>
```

`UserManager` is responsible for operations related to Identity users.

For this exercise, it is used to create a new user through:

```csharp
CreateAsync
```

The application therefore does not manually hash the password or directly insert the Identity user into the database.

## Registration Endpoint

A registration endpoint was added to `AuthsController`:

```http
POST /api/Auths/register
```

The endpoint creates a new `IdentityUser` using the supplied email:

```csharp
var user = new IdentityUser
{
    UserName = request.Email,
    Email = request.Email
};
```

The user is then created using:

```csharp
var result = await _userManager.CreateAsync(
    user,
    request.Password);
```

`UserManager.CreateAsync` handles the registration process, including password validation, password hashing, and persisting the Identity user.

If registration fails, the Identity validation errors are returned:

```csharp
if (!result.Succeeded)
{
    return BadRequest(result.Errors);
}
```

A successful registration returns:

```text
201 Created
```

An invalid registration returns:

```text
400 Bad Request
```

## Registration Flow

```text
Client
   ↓
POST /api/Auths/register
   ↓
AuthsController
   ↓
Create IdentityUser
   ↓
UserManager.CreateAsync
   ↓
Validate Password
   ↓
Hash Password
   ↓
Store Identity User
   ↓
Success → 201 Created
Failure → 400 Bad Request + Identity Errors
```

## Password Hashing

Passwords should never be stored in plain text.

ASP.NET Core Identity automatically hashes passwords before storing them in the database.

Identity uses PBKDF2 by default for password hashing.

### PBKDF2

PBKDF2 is a password-based key derivation algorithm designed to make password guessing more computationally expensive.

This helps make brute-force attempts against stored password hashes more difficult.

Because Identity already handles this process, no custom password hashing implementation was added to the project.

### Password Salt

Identity also uses a unique salt when hashing passwords.

A salt is additional unique data used during the password-hashing process.

Because each password uses a unique salt, two users who choose the same password do not end up with identical stored password hashes.

This also makes precomputed attacks such as rainbow-table attacks less effective.

Password hashing and salting are handled automatically by ASP.NET Core Identity through `UserManager`.

## Password Validation

ASP.NET Core Identity also validates passwords before creating users.

If a password does not satisfy the configured Identity password requirements, `CreateAsync` fails and returns specific validation errors.

These errors can then be returned by the API to explain why registration failed.

## Postman Testing

The registration endpoint was tested in Postman with both successful and invalid registration requests.

### Valid Registration

Request:

```json
{
  "email": "mohammad@gmail.com",
  "password": "Mm@123123"
}
```

Response:

```text
201 Created
```

The successful response confirms that the Identity user was created successfully.

### Successful Registration Test

![Successful Registration](./registration-success.png)

### Weak Password

A deliberately weak password was used to verify Identity's built-in password validation.

Request:

```json
{
  "email": "mohammad1@gmail.com",
  "password": "123"
}
```

Response:

```text
400 Bad Request
```

Identity returned specific password validation errors, including:

```text
PasswordTooShort
PasswordRequiresNonAlphanumeric
PasswordRequiresLower
```

This confirms that password validation is handled automatically by ASP.NET Core Identity.

### Weak Password Validation Test

![Weak Password Validation](./weak-password-validation.png)

## Hands-On Lab Completed

The following tasks were completed:

- Added the ASP.NET Core Identity Entity Framework Core package.
- Extended `AppDbContext` using `IdentityDbContext<IdentityUser>`.
- Created and applied an Identity migration.
- Added the Identity schema to the existing SQL Server database.
- Registered Identity using `IdentityUser` and `IdentityRole`.
- Implemented user registration using `UserManager.CreateAsync`.
- Returned Identity validation errors for invalid registration attempts.
- Tested successful registration using Postman.
- Tested registration with a deliberately weak password using Postman.

## Project Changes

The main additions and changes for this exercise were:

```text
TaskTrackerApi
├── Controllers
│   └── AuthsController.cs
├── Data
│   └── AppDbContext.cs
├── Migrations
│   └── <timestamp>_AddIdentity.cs
├── Requests
│   └── RegisterRequest.cs
└── Program.cs
```

The existing Task Tracker models, controllers, and services from Week 3 remained in the project.

## Tools Used

- ASP.NET Core Web API
- ASP.NET Core Identity
- Entity Framework Core
- SQL Server
- Visual Studio
- Visual Studio Package Manager Console
- Postman
