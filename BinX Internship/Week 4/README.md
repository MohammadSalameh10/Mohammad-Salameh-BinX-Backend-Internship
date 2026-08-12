# Week 4 — Authentication & Security

## Overview

Week 4 focuses on authentication, authorization, security, and input validation in ASP.NET Core applications.

The existing Task Tracker API from Week 3 is extended with ASP.NET Core Identity, JWT authentication, protected routes, role-based access control, claims, authorization policies, and FluentValidation for structured request validation.

## Daily Work

| Day   | Topic                                                 | Project / Documentation |
| ----- | ----------------------------------------------------- | ----------------------- |
| Day 1 | ASP.NET Core Identity & User Registration             | [View Day 1](./Day%201) |
| Day 2 | JWT Authentication & Token Issuance                   | [View Day 2](./Day%202) |
| Day 3 | Protecting Routes, Roles & Policy-Based Authorization | [View Day 3](./Day%203) |
| Day 4 | Input Validation with FluentValidation                | [View Day 4](./Day%204) |

## Topics Covered

### ASP.NET Core Identity

* Integrated ASP.NET Core Identity into the existing Task Tracker API.
* Used `IdentityUser` for Identity users.
* Used `IdentityRole` for Identity roles.
* Integrated Identity with Entity Framework Core.
* Used Identity instead of implementing custom password storage and hashing logic.

### Identity & Entity Framework Core

The existing `AppDbContext` was extended using:

```csharp
IdentityDbContext<IdentityUser>
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

A registration endpoint was implemented:

```http
POST /api/Auths/register
```

User creation is handled using:

```csharp
_userManager.CreateAsync(user, request.Password)
```

`UserManager` handles password validation, password hashing, and storing the Identity user.

The registration endpoint was tested using both valid credentials and a deliberately weak password.

### Password Hashing & Validation

ASP.NET Core Identity automatically handles password hashing and validation.

No custom password hashing logic was implemented.

A weak password test confirmed that Identity returns validation errors such as:

```text
PasswordTooShort
PasswordRequiresNonAlphanumeric
PasswordRequiresLower
```

### JWT Authentication

JWT authentication was added to authenticate registered users after login.

A JSON Web Token contains:

```text
Header.Payload.Signature
```

The generated JWT initially contains claims representing:

```text
User ID
Email
```

The JWT payload is encoded rather than encrypted, so sensitive information should not be stored inside claims.

### Login & Token Issuance

A login endpoint was implemented:

```http
POST /api/Auths/login
```

The submitted credentials are verified using ASP.NET Core Identity.

Password verification uses:

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

Successful authentication returns:

```text
200 OK
```

with a signed JWT.

### JWT Signing & Expiration

The JWT is signed using:

```text
HMAC SHA-256
```

The token uses configured values for:

```text
Issuer
Audience
Signing Key
```

The final access-token lifetime is:

```text
15 minutes
```

Token expiration was tested by temporarily reducing the lifetime and confirming that an expired token is rejected.

### JWT Bearer Authentication

JWT Bearer Authentication was configured to validate incoming tokens.

The API validates:

* Issuer
* Audience
* Lifetime
* Signing key

The Bearer token is validated before protected endpoint code executes.

### Protected Routes

The existing `TasksController` was protected using:

```csharp
[Authorize]
```

Placing `[Authorize]` on the controller protects all Task CRUD endpoints.

A request without a valid JWT was tested and returned:

```text
401 Unauthorized
```

### Role-Based Access Control

Two ASP.NET Core Identity roles were created:

```text
User
Admin
```

`RoleManager<IdentityRole>` was used to create the roles when the application starts.

Before creating each role, the application checks whether it already exists using:

```csharp
RoleExistsAsync()
```

Users were assigned to roles using:

```csharp
_userManager.AddToRoleAsync(user, role)
```

Two test users were configured:

```text
mohammad@gmail.com → User
ahmad@gmail.com    → Admin
```

### Role Claims in JWT

The authenticated user's roles are retrieved during login using:

```csharp
_userManager.GetRolesAsync(user)
```

Each role is then added to the JWT:

```csharp
claims.Add(new Claim(ClaimTypes.Role, role));
```

This allows ASP.NET Core Authorization to evaluate role requirements directly from the authenticated user's JWT.

### Admin-Only Endpoint

The Delete Task endpoint was restricted using:

```csharp
[Authorize(Roles = "Admin")]
```

A valid JWT belonging to the `User` role returned:

```text
403 Forbidden
```

A valid JWT belonging to the `Admin` role successfully executed the Delete endpoint and returned:

```text
204 No Content
```

### `401 Unauthorized` vs `403 Forbidden`

The authorization tests demonstrated the difference between these responses:

```text
401 Unauthorized
→ Authentication failed or no valid authenticated user exists.

403 Forbidden
→ Authentication succeeded, but the user does not have the required permission.
```

### Claims-Based Authorization

Authorization was extended beyond simple role checks using a custom claim:

```text
permission = create_task
```

For the training exercise, this claim is added to the JWT when the authenticated user has the `Admin` role.

### Policy-Based Authorization

A named authorization policy was created:

```text
CanCreateTasks
```

The policy requires:

```text
permission = create_task
```

It was configured in `Program.cs`:

```csharp
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanCreateTasks", policy =>
    {
        policy.RequireClaim(
            "permission",
            "create_task");
    });
});
```

The Create Task endpoint was protected using:

```csharp
[Authorize(Policy = "CanCreateTasks")]
```

A `User` token without the required permission returned:

```text
403 Forbidden
```

while an `Admin` token containing the permission successfully created a task and returned:

```text
201 Created
```

### Postman Environment & Token Reuse

A Postman Environment was configured with:

```text
baseUrl
token
```

A Post-response script was added to the login request:

```javascript
const response = pm.response.json();

pm.environment.set("token", response.token);
```

Protected requests can then use:

```text
{{token}}
```

as their Bearer Token instead of manually copying and pasting the JWT.

### DataAnnotations vs FluentValidation

Task request validation was moved from DataAnnotations to FluentValidation.

DataAnnotations are useful for simple validation rules placed directly on request-model properties.

FluentValidation keeps validation logic in separate validator classes and provides more flexibility for expressing business-oriented rules.

The Task create and update request models were cleaned so their validation responsibility is handled by dedicated validators.

### FluentValidation Validators

Two validator classes were created:

```text
CreateTaskRequestValidator
UpdateTaskRequestValidator
```

Both inherit from:

```csharp
AbstractValidator<T>
```

Validation rules are defined using:

```csharp
RuleFor(...)
```

with custom messages using:

```csharp
WithMessage(...)
```

### Create Task Validation

The `CreateTaskRequestValidator` validates:

```text
Title
UserId
DueDate
```

The implemented rules include:

```text
Title
→ Must not be empty.
→ Must not exceed 200 characters.

UserId
→ Must be greater than 0.

DueDate
→ Optional.
→ If provided, it must be in the future.
```

### Update Task Validation

A separate `UpdateTaskRequestValidator` was created for Update requests.

It applies the same validation requirements to task updates.

Keeping Create and Update validation in dedicated classes separates validation logic from the request models and makes the rules easier to maintain.

### FluentValidation Integration

Automatic FluentValidation support was registered in `Program.cs`:

```csharp
builder.Services.AddFluentValidationAutoValidation();
```

Validators were registered using assembly scanning:

```csharp
builder.Services
    .AddValidatorsFromAssemblyContaining<
        CreateTaskRequestValidator>();
```

This allows the application to discover the Create and Update validators automatically.

### Automatic Validation Pipeline

Invalid requests are validated before the controller action executes.

```text
Client Request
      ↓
Model Binding
      ↓
FluentValidation
      ↓
Valid?
 ├── No  → 400 Bad Request
 └── Yes → Controller Action
```

This keeps controller actions focused on request processing rather than repeating validation checks.

### Structured Validation Errors

Invalid requests return structured validation errors associated with the property that failed validation.

For example:

```json
{
  "status": 400,
  "errors": {
    "Title": [
      "Title is required."
    ]
  }
}
```

This provides API clients with both the invalid field and the reason validation failed.

### Create Validation Testing

Each Create validation rule was tested individually using Postman.

The following cases returned:

```text
Empty Title
→ 400 Bad Request
→ Title is required.

UserId = 0
→ 400 Bad Request
→ UserId must be greater than 0.

Past DueDate
→ 400 Bad Request
→ DueDate must be in the future.
```

### Update Validation Testing

The Update validator was tested using the same individual validation approach:

```text
Empty Title
→ 400 Bad Request
→ Title is required.

UserId = 0
→ 400 Bad Request
→ UserId must be greater than 0.

Past DueDate
→ 400 Bad Request
→ DueDate must be in the future.
```

Testing each rule separately confirmed that every validation requirement returns its expected structured error message.

## Projects

### Task Tracker API — Identity Integration

The Day 1 implementation includes:

* ASP.NET Core Identity integration
* Identity with Entity Framework Core
* Identity database migration
* `IdentityUser`
* `IdentityRole`
* `UserManager`
* Registration request model
* Registration endpoint
* Password hashing
* Password validation

The registration endpoint returns:

```text
201 Created     → Successful registration
400 Bad Request → Invalid registration
```

[View the Day 1 project and documentation](./Day%201)

### Task Tracker API — JWT Authentication

The Day 2 implementation includes:

* Login request model
* Authentication service
* Credential verification
* JWT generation
* User ID and email claims
* JWT signing using HMAC SHA-256
* 15-minute access-token expiration
* JWT Bearer Authentication
* Protected endpoint using `[Authorize]`
* Expired-token testing

The login endpoint returns:

```text
200 OK           → Successful login with JWT
401 Unauthorized → Invalid credentials
```

[View the Day 2 project and documentation](./Day%202)

### Task Tracker API — Authorization & Role-Based Access Control

The Day 3 implementation includes:

* Controller-level `[Authorize]`
* `User` and `Admin` Identity roles
* Role creation using `RoleManager`
* User-role assignment using `UserManager`
* Role claims inside JWTs
* Admin-only Delete endpoint
* Custom permission claim
* Named authorization policy
* Policy-protected Create Task endpoint
* Postman Environment
* Automatic JWT capture and reuse

Authorization behavior was verified through:

```text
No Token
→ 401 Unauthorized

User → Admin-only Delete
→ 403 Forbidden

Admin → Admin-only Delete
→ 204 No Content

User → CanCreateTasks Policy
→ 403 Forbidden

Admin → CanCreateTasks Policy
→ 201 Created
```

[View the Day 3 project and documentation](./Day%203)

### Task Tracker API — FluentValidation

The Day 4 implementation includes:

* FluentValidation integration
* Dedicated Create validator
* Dedicated Update validator
* Validation logic separated from request models
* `RuleFor` validation rules
* Custom validation messages
* Positive `UserId` validation
* Conditional future `DueDate` validation
* Automatic validation before controller execution
* Structured `400 Bad Request` responses
* Individual Postman testing for each validation rule

Validation behavior was verified through:

```text
Create — Empty Title
→ 400 Bad Request

Create — UserId = 0
→ 400 Bad Request

Create — Past DueDate
→ 400 Bad Request

Update — Empty Title
→ 400 Bad Request

Update — UserId = 0
→ 400 Bad Request

Update — Past DueDate
→ 400 Bad Request
```

[View the Day 4 project and documentation](./Day%204)

## Technologies and Tools

* C#
* ASP.NET Core Web API
* ASP.NET Core Identity
* Entity Framework Core
* SQL Server
* `IdentityUser`
* `IdentityRole`
* `UserManager`
* `RoleManager`
* `SignInManager`
* JWT
* JWT Bearer Authentication
* HMAC SHA-256
* Claims
* Role-Based Authorization
* Claims-Based Authorization
* Policy-Based Authorization
* `[Authorize]`
* FluentValidation
* `AbstractValidator<T>`
* `RuleFor`
* Structured Validation Errors
* Dependency Injection
* Visual Studio
* Visual Studio Package Manager Console
* Postman
* Postman Environments
* jwt.io
* Git
* GitHub
