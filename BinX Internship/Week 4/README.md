# Week 4 — Authentication & Security

## Overview

Week 4 focuses on authentication and security in ASP.NET Core applications.

The existing Task Tracker API from Week 3 is extended with ASP.NET Core Identity to support secure user management and authentication.

## Daily Work

| Day   | Topic                                             | Project / Documentation |
| ----- | ------------------------------------------------- | ----------------------- |
| Day 1 | ASP.NET Core Identity & User Registration         | [View Day 1](./Day%201) |

## Topics Covered

### ASP.NET Core Identity

- Integrated ASP.NET Core Identity into the existing Task Tracker API.
- Used `IdentityUser` for Identity users.
- Used `IdentityRole` for Identity roles.
- Integrated Identity with Entity Framework Core.
- Used Identity instead of implementing custom password storage and hashing logic.

### Identity & Entity Framework Core

The existing `AppDbContext` was extended using:

```csharp
IdentityDbContext<IdentityUser>
```

The Identity Entity Framework Core package was added:

```text
Microsoft.AspNetCore.Identity.EntityFrameworkCore
```

A new migration was created and applied:

```powershell
Add-Migration AddIdentity
Update-Database
```

This added the ASP.NET Core Identity schema to the existing SQL Server database.

### Identity Configuration

Identity was registered in `Program.cs` using:

```csharp
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();
```

Authentication and authorization middleware were added to the request pipeline:

```csharp
app.UseAuthentication();
app.UseAuthorization();
```

### User Registration

A registration endpoint was implemented using `UserManager<IdentityUser>`.

The endpoint is:

```http
POST /api/Auths/register
```

The registration request contains:

```json
{
  "email": "testuser@example.com",
  "password": "Test@12345"
}
```

User creation is handled using:

```csharp
_userManager.CreateAsync(user, request.Password)
```

`UserManager` handles password hashing and stores the Identity user in the database.

### Password Hashing & Validation

ASP.NET Core Identity handles password hashing automatically using PBKDF2 with a unique salt.

No custom password hashing logic was implemented.

Identity also validates passwords during registration.

A deliberately weak password was tested:

```json
{
  "email": "weakuser@example.com",
  "password": "123"
}
```

Identity returned validation errors such as:

```text
PasswordTooShort
PasswordRequiresNonAlphanumeric
PasswordRequiresLower
```

## Project

### Task Tracker API — Identity Integration

The existing Task Tracker API was extended with ASP.NET Core Identity.

The Day 1 implementation includes:

- ASP.NET Core Identity
- Identity integration with Entity Framework Core
- Identity database migration
- `IdentityUser`
- `IdentityRole`
- `UserManager`
- Registration request model
- Registration endpoint
- Password hashing
- Password validation
- Authentication and authorization middleware

The registration endpoint returns:

```text
201 Created     → Successful registration
400 Bad Request → Invalid registration
```

The endpoint was tested using Postman with both valid credentials and a deliberately weak password.

[View the Day 1 project and documentation](./Day%201)

## Technologies and Tools

- C#
- ASP.NET Core Web API
- ASP.NET Core Identity
- Entity Framework Core
- SQL Server
- Dependency Injection
- Visual Studio
- Visual Studio Package Manager Console
- Postman
- Git
- GitHub
