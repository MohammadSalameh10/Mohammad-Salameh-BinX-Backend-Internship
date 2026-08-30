# Week 7 — Sprint 2: Identity & Authorization Integration

## Overview

Week 7 begins Phase 3 Sprint 2 for the **Cardiac Patient Monitoring System API**.

The week starts with Sprint 2 planning and reviewing the existing ASP.NET Core Identity integration, Identity-related migrations, role structure, authorization requirements, and authentication wiring in the existing project.

Because Identity and role-based authorization had already been implemented during previous training work, Day 1 focused on reviewing and verifying the existing implementation instead of recreating the same functionality.

## Daily Work

| Day | Topic | Project / Documentation |
|---|---|---|
| Day 1 | Sprint 2 Planning & Wiring Identity into the Capstone | [View Day 1](./Day%201) |

## Week 7 Highlights

### Sprint 2 Planning

- Defined a clear Sprint 2 goal.
- Created a Sprint 2 backlog in Notion.
- Carried forward the Sprint 1 retrospective improvement action.
- Organized the Sprint 2 Identity and authorization work into clear backlog tasks.

### Identity Integration Review

- Reviewed the existing ASP.NET Core Identity integration.
- Verified that `ApplicationDbContext` inherits from `IdentityDbContext<IdentityUser>`.
- Confirmed that Identity uses the existing EF Core database context.
- Reviewed the one-to-one relationship between `IdentityUser` and `Patient`.
- Verified the unique index on `Patient.UserId`.
- Reviewed the existing cascade delete behavior.

### Identity Migration Review

- Reviewed the existing `AddIdentity` migration.
- Confirmed that the Identity migration adds the required ASP.NET Core Identity tables and indexes.
- Verified that no destructive changes were made to the existing project tables.
- Reviewed the `AddPatientIdentityRelationship` migration.
- Verified the foreign key between `Patients.UserId` and `AspNetUsers.Id`.
- Confirmed that no new migration was required because Identity had already been integrated.

### Database Verification

- Verified the Identity tables using SQL Server Object Explorer.
- Confirmed the presence of:
  - `AspNetUsers`
  - `AspNetRoles`
  - `AspNetUserRoles`
  - `AspNetUserClaims`
  - `AspNetRoleClaims`
  - `AspNetUserLogins`
  - `AspNetUserTokens`
- Confirmed that the existing application tables remained intact.

### Roles and Authorization Planning

- Confirmed the project roles as:
  - `Admin`
  - `Patient`
- Reviewed the responsibilities of each role.
- Documented which endpoints require `Admin`, `Patient`, or public access.
- Reviewed the existing `[Authorize]` attributes across the project controllers.
- Confirmed that the current authorization structure matches the planned role model.

### Authentication and Authorization Wiring

- Reviewed ASP.NET Core Identity registration in `Program.cs`.
- Verified `AddIdentity<IdentityUser, IdentityRole>()`.
- Verified `AddEntityFrameworkStores<ApplicationDbContext>()`.
- Reviewed the existing JWT Bearer authentication configuration.
- Verified validation of issuer, audience, lifetime, and signing key.
- Confirmed the middleware order:
  - `UseAuthentication`
  - `UseAuthorization`
  - `MapControllers`

### Role Seeding

- Reviewed the existing role seeding logic in `DbSeeder`.
- Confirmed that `Admin` and `Patient` roles are created when missing.
- Verified that seeded users are assigned to the correct roles.
- Reviewed role checks using `IsInRoleAsync`.
- Reviewed role assignment using `AddToRoleAsync`.

## Sprint 2 Backlog

The Sprint 2 backlog currently includes:

- Review existing ASP.NET Core Identity integration
- Verify `ApplicationDbContext` Identity configuration
- Review existing Identity migrations
- Verify Identity tables in SQL Server
- Verify `Admin` and `Patient` roles
- Document role permissions for project endpoints
- Review existing authorization attributes
- Verify authentication and authorization wiring
- Apply the Sprint 1 retrospective action before merging Sprint 2 changes

The existing Identity-related review tasks were completed during Day 1.

The Sprint 1 retrospective action remains active throughout Sprint 2:

`Complete the pull request review before merging any Sprint 2 feature into main.`

## Tools Used

- C#
- ASP.NET Core Web API
- ASP.NET Core Identity
- Entity Framework Core
- SQL Server
- SQL Server Object Explorer
- JWT Authentication
- Visual Studio
- Notion
- Git
- GitHub