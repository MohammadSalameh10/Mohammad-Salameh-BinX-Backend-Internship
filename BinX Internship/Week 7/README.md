# Week 7 — Sprint 2: Identity & Authorization Integration

## Overview

Week 7 begins Phase 3 Sprint 2 for the **Cardiac Patient Monitoring System API**.

The week started with Sprint 2 planning and a review of the existing ASP.NET Core Identity integration, Identity-related migrations, role structure, authorization requirements, and authentication wiring.

Because Identity and role-based authorization had already been implemented during previous training work, Day 1 focused on reviewing and verifying the existing implementation instead of recreating the same functionality.

Day 2 extended the existing authentication flow by creating the linked `Patient` domain record during registration and adding a domain-specific `PatientId` claim to the JWT returned during login.

## Daily Work

| Day | Topic | Project / Documentation |
|---|---|---|
| Day 1 | Sprint 2 Planning & Wiring Identity into the Capstone | [View Day 1](./Day%201) |
| Day 2 | JWT Login & Registration for the Capstone Project | [View Day 2](./Day%202) |

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

### Patient Registration and Domain Linking

- Extended the registration request to include the patient domain information required by the `Patient` entity.
- Updated the registration flow to create both the `IdentityUser` and the linked `Patient` record.
- Linked the new Patient record using `Patient.UserId` and `IdentityUser.Id`.
- Kept Identity user creation, role assignment, and Patient creation inside the same EF Core transaction.
- Saved the linked Patient record using Entity Framework Core.
- Preserved rollback behavior to prevent partially completed registrations.

### Domain-Specific JWT Claims

- Extended the login flow to retrieve the Patient linked to the authenticated Identity user.
- Added a domain-specific `PatientId` claim to the generated JWT.
- Kept the existing Identity claims for user ID, email, and role.
- Used the `Patient.UserId` relationship to resolve the linked Patient record.
- Enabled authenticated requests to identify the related Patient directly from the JWT.

### Registration-to-Login Flow Testing

- Registered a new Patient account using Postman.
- Confirmed `201 Created` from the registration endpoint.
- Verified the new account in the `AspNetUsers` table.
- Verified the linked Patient record in the `Patients` table.
- Confirmed that `Patient.UserId` matches the corresponding Identity user's ID.
- Logged in using the newly registered account.
- Confirmed `200 OK` and successful JWT generation.
- Decoded the JWT and verified the `PatientId` claim.
- Confirmed that the `PatientId` claim matches the Patient record stored in SQL Server.
- Verified the complete registration-to-login flow end-to-end.

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
- Extend registration to create the linked `Patient` record
- Add the domain-specific `PatientId` claim to JWT login
- Test the complete registration-to-login flow
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
- Postman
- jwt.io
- Git
- GitHub