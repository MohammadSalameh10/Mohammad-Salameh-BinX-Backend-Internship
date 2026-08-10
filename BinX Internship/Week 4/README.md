# Week 4 — Authentication & Security

## Overview

Week 4 focuses on authentication and security in ASP.NET Core applications.

The existing Task Tracker API from Week 3 is extended with ASP.NET Core Identity and JWT authentication to support secure user registration, login, token issuance, and protected API endpoints.

## Daily Work

| Day   | Topic                                     | Project / Documentation |
| ----- | ----------------------------------------- | ----------------------- |
| Day 1 | ASP.NET Core Identity & User Registration | [View Day 1](./Day%201) |
| Day 2 | JWT Authentication & Token Issuance       | [View Day 2](./Day%202) |

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

A registration endpoint was implemented using ASP.NET Core Identity.

The endpoint is:

```http
POST /api/Auths/register
```

The registration request contains:

```json
{
  "email": "mohammad@gmail.com",
  "password": "Mm@123123"
}
```

User creation is handled using:

```csharp
_userManager.CreateAsync(user, request.Password)
```

`UserManager` handles password hashing and stores the Identity user in the database.

### Password Hashing & Validation

ASP.NET Core Identity handles password hashing automatically using its built-in password hasher.

No custom password hashing logic was implemented.

Identity also validates passwords during registration.

A deliberately weak password was tested:

```json
{
  "email": "mohammad1@gmail.com",
  "password": "123"
}
```

Identity returned validation errors such as:

```text
PasswordTooShort
PasswordRequiresNonAlphanumeric
PasswordRequiresLower
```

### JWT Authentication

JWT authentication was added to authenticate registered users after login.

A JWT contains three main parts:

```text
Header.Payload.Signature
```

The generated token contains claims representing the authenticated user, including:

- User ID
- Email

The JWT payload is encoded rather than encrypted, so sensitive information should not be stored inside token claims.

### Login & Token Issuance

A login endpoint was implemented:

```http
POST /api/Auths/login
```

The login request contains:

```json
{
  "email": "mohammad@gmail.com",
  "password": "Mm@123123"
}
```

The user is retrieved using `UserManager`, and the submitted password is verified using:

```csharp
_signInManager.CheckPasswordSignInAsync(
    user,
    request.Password,
    false)
```

Invalid credentials return:

```text
401 Unauthorized
```

After successful authentication, a signed JWT is generated and returned to the client.

The token contains the following user claims:

```text
sub
email
```

### JWT Signing & Expiration

The JWT is signed using:

```text
HMAC SHA-256
```

The signing key, issuer, and audience are read from the application's JWT configuration.

The access token is configured with a lifetime of:

```text
15 minutes
```

A short-lived access token limits how long an expired or compromised token can remain usable.

### JWT Bearer Authentication

JWT Bearer Authentication was configured in `Program.cs`.

Incoming tokens are validated for:

- Issuer
- Audience
- Lifetime
- Signing key

The authentication middleware validates the Bearer token before protected endpoint code executes.

### Protected Endpoints

The existing task endpoint was protected using:

```csharp
[Authorize]
```

The protected endpoint is:

```http
GET /api/Tasks
```

The client must send a valid JWT using:

```text
Authorization: Bearer <token>
```

Requests without a valid token are rejected with:

```text
401 Unauthorized
```

### Token Validation & Expiration Testing

The generated JWT was decoded to verify its claims.

The decoded token confirmed the expected:

```text
sub
email
exp
iss
aud
```

The token lifetime was also temporarily reduced to test expiration.

After the token expired, a request was sent to the protected endpoint and the API returned:

```text
401 Unauthorized
```

The final token lifetime was then restored to 15 minutes.

## Projects

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

### Task Tracker API — JWT Authentication

The Task Tracker API was extended with login and JWT-based authentication.

The Day 2 implementation includes:

- Login request model
- Authentication service
- `UserManager`
- `SignInManager`
- Credential verification
- JWT generation
- User ID and email claims
- JWT signing using HMAC SHA-256
- 15-minute access-token expiration
- JWT Bearer Authentication
- Issuer, audience, lifetime, and signing-key validation
- Protected endpoint using `[Authorize]`
- Expired-token testing

The login endpoint returns:

```text
200 OK           → Successful login with JWT
401 Unauthorized → Invalid credentials
```

The protected task endpoint also returns `401 Unauthorized` when a valid Bearer token is not provided.

The generated token was decoded to verify its claims, and token expiration was tested against the protected endpoint.

[View the Day 2 project and documentation](./Day%202)

## Technologies and Tools

- C#
- ASP.NET Core Web API
- ASP.NET Core Identity
- Entity Framework Core
- SQL Server
- `IdentityUser`
- `IdentityRole`
- `UserManager`
- `SignInManager`
- JWT
- JWT Bearer Authentication
- System.IdentityModel.Tokens.Jwt
- HMAC SHA-256
- Claims
- Authorization
- Dependency Injection
- Visual Studio
- Visual Studio Package Manager Console
- Postman
- jwt.io
- Git
- GitHub