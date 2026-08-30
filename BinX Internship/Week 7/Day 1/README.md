# Week 7 — Day 1: Sprint 2 Planning & Identity Integration

## Overview

Day 1 focused on Sprint 2 planning and reviewing the existing ASP.NET Core Identity integration in the Cardiac Patient Monitoring System API.

Because Identity, JWT authentication, role-based authorization, and the related migrations had already been implemented during previous training work, the Day 1 exercise focused on verifying the existing configuration instead of recreating it.

The work included defining the Sprint 2 goal, creating the Sprint 2 backlog, reviewing `ApplicationDbContext`, reviewing the existing Identity migrations, verifying the Identity tables in SQL Server, confirming the `Admin` and `Patient` roles, and documenting the role requirements for the project endpoints.

The Sprint 1 retrospective action was also carried forward into Sprint 2 planning.

## Learning Objectives

The objectives of this exercise were to:

- Define a clear Sprint 2 goal.
- Carry forward the Sprint 1 retrospective improvement action.
- Review the existing ASP.NET Core Identity integration.
- Verify that `ApplicationDbContext` inherits from `IdentityDbContext<IdentityUser>`.
- Review the existing Identity-related migrations.
- Verify that the Identity tables are applied to the existing SQL Server database.
- Confirm the roles required by the project domain.
- Document which endpoints require `Admin`, `Patient`, or public access.
- Organize the Sprint 2 scope into clear backlog tasks.

## Sprint 2 Planning

Sprint 2 started by defining a clear goal for authentication and authorization work in the existing Cardiac Patient Monitoring System API.

The Sprint 2 goal was defined as:

> Integrate and verify authentication and authorization in the Cardiac Patient Monitoring System API, define the required Admin and Patient roles, and ensure Sprint 2 changes are reviewed before merging into `main`.

The Sprint 1 retrospective action was carried forward into the new sprint:

`Complete the pull request review before merging any Sprint 2 feature into main.`

A Sprint 2 backlog was created in Notion to organize the planned work.

The backlog included:

- Review existing ASP.NET Core Identity integration
- Verify `ApplicationDbContext` Identity configuration
- Review existing Identity migrations
- Verify Identity tables in SQL Server
- Verify `Admin` and `Patient` roles
- Document role permissions for project endpoints
- Review existing authorization attributes
- Verify authentication and authorization wiring
- Apply the Sprint 1 retrospective action before merging Sprint 2 changes

The existing Identity and authorization work was reviewed before making any new code changes to avoid duplicating functionality that was already implemented.

## Identity Integration Review

The existing ASP.NET Core Identity integration was reviewed in the project.

`ApplicationDbContext` already inherits from:

```csharp
IdentityDbContext<IdentityUser>
```

The context also contains the main project entities:

```text
Patients
VitalSigns
Medications
Appointments
```

The existing relationship configuration between `Patient` and `IdentityUser` was also reviewed.

The relationship uses:

```text
Patient.UserId
→ AspNetUsers.Id
```

and is configured as a one-to-one relationship with:

```csharp
DeleteBehavior.Cascade
```

A unique index on `Patient.UserId` ensures that one Identity user cannot be linked to multiple Patient records.

Because Identity was already integrated into the existing `ApplicationDbContext`, no DbContext inheritance change was required during Day 1.

## Identity Migration Review

The existing Identity-related migrations were reviewed instead of generating a new migration, because ASP.NET Core Identity had already been added to the project previously.

The reviewed migrations were:

```text
AddIdentity
AddPatientIdentityRelationship
```

### AddIdentity

The `AddIdentity` migration adds the ASP.NET Core Identity tables, including:

```text
AspNetUsers
AspNetRoles
AspNetUserRoles
AspNetUserClaims
AspNetRoleClaims
AspNetUserLogins
AspNetUserTokens
```

The migration was reviewed to confirm that it adds the required Identity tables and indexes without making destructive changes to the existing project tables.

### AddPatientIdentityRelationship

The `AddPatientIdentityRelationship` migration adds the foreign key:

```text
Patients.UserId
→ AspNetUsers.Id
```

The relationship uses cascade delete behavior.

The migration was reviewed to confirm that it only adds the required relationship and does not remove or modify existing project data destructively.

Because the existing migrations already contained the required Identity schema changes, no new migration was generated during Day 1.

## Database Verification

The existing SQL Server database was reviewed using SQL Server Object Explorer to confirm that the Identity migrations had already been applied successfully.

The following ASP.NET Core Identity tables were verified:

```text
AspNetUsers
AspNetRoles
AspNetUserRoles
AspNetUserClaims
AspNetRoleClaims
AspNetUserLogins
AspNetUserTokens
```

The existing application tables were also confirmed to still be present:

```text
Patients
VitalSigns
Medications
Appointments
```

This confirmed that ASP.NET Core Identity was integrated into the existing database without removing the original project tables.

The Identity schema and the application schema are both present in the same SQL Server database.

## Roles and Authorization Planning

The project domain requires two main roles:

```text
Admin
Patient
```

These roles were already implemented and were reviewed during Day 1.

### Admin Role

The `Admin` role is used for endpoints that manage and review patient-related data.

The Admin can:

- View all patients.
- View a patient by ID.
- Update and delete patients.
- View all vital signs.
- View a vital sign by ID.
- Update and delete vital signs.
- View medications.
- Update and delete medications.
- View appointments.
- Update and delete appointments.

### Patient Role

The `Patient` role is used for creating patient-owned data.

The Patient can:

- Create a patient profile.
- Create vital-sign records.
- Create medication records.
- Create appointment records.

Authentication endpoints remain public so users can register and log in before receiving a JWT token.

### Endpoint Role Structure

| Endpoint | Required Access |
|---|---|
| `POST /api/Auths/register` | Public |
| `POST /api/Auths/login` | Public |
| `GET /api/Patients` | Admin |
| `GET /api/Patients/{id}` | Admin |
| `POST /api/Patients` | Patient |
| `PUT /api/Patients/{id}` | Admin |
| `DELETE /api/Patients/{id}` | Admin |
| `GET /api/VitalSigns` | Admin |
| `GET /api/VitalSigns/{id}` | Admin |
| `POST /api/VitalSigns` | Patient |
| `PUT /api/VitalSigns/{id}` | Admin |
| `DELETE /api/VitalSigns/{id}` | Admin |
| `GET /api/Medications` | Admin |
| `GET /api/Medications/{id}` | Admin |
| `POST /api/Medications` | Patient |
| `PUT /api/Medications/{id}` | Admin |
| `DELETE /api/Medications/{id}` | Admin |
| `GET /api/Appointments` | Admin |
| `GET /api/Appointments/{id}` | Admin |
| `POST /api/Appointments` | Patient |
| `PUT /api/Appointments/{id}` | Admin |
| `DELETE /api/Appointments/{id}` | Admin |

The existing authorization attributes were reviewed and confirmed to match this planned role structure.

## Identity and Authorization Wiring Review

The existing authentication and authorization configuration was reviewed in `Program.cs`.

ASP.NET Core Identity is registered using:

```csharp
builder.Services
    .AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
```

This confirms that Identity users and roles use the existing `ApplicationDbContext`.

JWT authentication is also configured using:

```text
JwtBearer
```

with validation for:

```text
Issuer
Audience
Lifetime
Signing Key
```

The middleware order was reviewed and confirmed as:

```text
UseAuthentication
        ↓
UseAuthorization
        ↓
MapControllers
```

The existing authorization attributes were also reviewed across the project controllers.

Role-based authorization uses:

```csharp
[Authorize(Roles = "Admin")]
```

and:

```csharp
[Authorize(Roles = "Patient")]
```

The authentication endpoints remain public.

The existing configuration was confirmed to be correctly wired, so no new authentication or authorization configuration was required during Day 1.

## Role Seeding Review

The existing role seeding logic was reviewed in `DbSeeder`.

The project defines the following roles:

```text
Admin
Patient
```

The seeder first checks whether each role already exists.

If a role does not exist, it is created using:

```csharp
roleManager.CreateAsync(new IdentityRole(role))
```

The seed process also verifies that the predefined users are assigned to the correct roles:

```text
Admin user
→ Admin role

Patient user
→ Patient role
```

Role membership is checked using:

```csharp
IsInRoleAsync
```

and missing role assignments are added using:

```csharp
AddToRoleAsync
```

This confirmed that the roles used by the authorization attributes are not only planned in the API, but are also created and assigned in the database during application startup.

## Hands-On Lab Review

The Day 1 hands-on work was completed as follows:

1. Defined the Sprint 2 goal.
2. Created a Sprint 2 backlog in Notion.
3. Carried forward the Sprint 1 retrospective action.
4. Reviewed the existing ASP.NET Core Identity integration.
5. Verified that `ApplicationDbContext` already inherits from `IdentityDbContext<IdentityUser>`.
6. Reviewed the existing Identity-related migrations.
7. Confirmed that the `AddIdentity` migration adds the required Identity tables without destructive changes to the existing project tables.
8. Reviewed the `AddPatientIdentityRelationship` migration.
9. Verified the relationship between `Patients.UserId` and `AspNetUsers.Id`.
10. Verified the Identity tables in SQL Server Object Explorer.
11. Confirmed that the existing application tables remained intact.
12. Confirmed the project roles as `Admin` and `Patient`.
13. Reviewed and documented the required role for each protected endpoint.
14. Reviewed the existing authorization attributes across the project controllers.
15. Reviewed the Identity, JWT authentication, and authorization configuration in `Program.cs`.
16. Verified the authentication and authorization middleware order.
17. Reviewed the existing role seeding logic in `DbSeeder`.
18. Confirmed that `Admin` and `Patient` roles are created and assigned correctly.
19. Exported the Sprint 2 planning documentation from Notion as PDF.

## Tools Used

- C#
- ASP.NET Core Identity
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- SQL Server Object Explorer
- JWT Authentication
- Visual Studio
- Notion
- Git
- GitHub