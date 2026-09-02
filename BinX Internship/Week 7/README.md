# Week 7 — Sprint 2: Identity & Authorization Integration

## Overview

Week 7 begins Phase 3 Sprint 2 for the **Cardiac Patient Monitoring System API**.

The week started with Sprint 2 planning and a review of the existing ASP.NET Core Identity integration, Identity-related migrations, role structure, authorization requirements, and authentication wiring.

Because Identity and role-based authorization had already been implemented during previous training work, Day 1 focused on reviewing and verifying the existing implementation instead of recreating the same functionality.

Day 2 extended the existing authentication flow by creating the linked `Patient` domain record during registration and adding a domain-specific `PatientId` claim to the JWT returned during login.

Day 3 focused on applying role-based access control across the API and adding resource ownership checks so that Patients can access only their own appointment data while Admin users retain broader access.

Day 4 focused on identifying a genuine cross-cutting concern and implementing a custom `RequestTimingMiddleware` to measure request execution time consistently across the API without duplicating logic inside individual controllers.

## Daily Work

| Day | Topic | Project / Documentation |
|---|---|---|
| Day 1 | Sprint 2 Planning & Wiring Identity into the Capstone | [View Day 1](./Day%201) |
| Day 2 | JWT Login & Registration for the Capstone Project | [View Day 2](./Day%202) |
| Day 3 | Role-Based Access Control and Ownership Checks | [View Day 3](./Day%203) |
| Day 4 | Custom Middleware & Cross-Cutting Concerns | [View Day 4](./Day%204) |

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

### Role-Based Access Control Review

- Reviewed role assignment for `Admin` and `Patient`.
- Confirmed that public registration assigns the `Patient` role only.
- Confirmed that the initial Admin account is created through database seeding.
- Reviewed the access requirement for every main API endpoint.
- Confirmed which endpoints are public, Patient-only, Admin-only, or shared between Admin and Patient.

### Appointment Ownership Checks

- Updated `GET /api/Appointments/{id}` to allow both `Admin` and `Patient`.
- Added an ownership check for Patient access to specific appointments.
- Read the `PatientId` claim from the authenticated user's JWT.
- Compared the JWT `PatientId` with the requested appointment's `PatientId`.
- Allowed Patients to access their own appointments.
- Returned `404 Not Found` when a Patient attempted to access another Patient's appointment.
- Kept Admin access unrestricted for individual appointment retrieval.

### RBAC and Ownership Testing

- Tested a Patient token against `GET /api/Patients`.
- Confirmed `403 Forbidden` for the Admin-only Patients endpoint.
- Tested a Patient token against `GET /api/VitalSigns`.
- Confirmed `403 Forbidden` for the Admin-only VitalSigns endpoint.
- Verified appointment ownership test data in SQL Server.
- Tested a Patient accessing their own appointment and confirmed `200 OK`.
- Tested the same Patient accessing another Patient's appointment and confirmed `404 Not Found`.
- Verified both role-based authorization and resource ownership protection.

### Cross-Cutting Concern Identification

- Identified request timing as a genuine cross-cutting concern.
- Confirmed that request timing applies broadly across multiple API endpoints.
- Avoided duplicating timing logic inside individual controllers.
- Selected custom middleware as the appropriate implementation approach.
- Confirmed that the concern was separate from the existing global exception-handling middleware.

### Request Timing Middleware

- Implemented a custom `RequestTimingMiddleware`.
- Used `Stopwatch` to measure HTTP request execution time.
- Logged the HTTP method.
- Logged the request path.
- Logged the response status code.
- Logged the elapsed execution time.
- Used `RequestDelegate` to continue the ASP.NET Core request pipeline.
- Used `ILogger<RequestTimingMiddleware>` for structured request timing logs.

### Middleware Pipeline Integration

- Registered `RequestTimingMiddleware` in `Program.cs`.
- Positioned it after the existing `ExceptionHandlingMiddleware`.
- Confirmed that the middleware runs centrally without modifying individual controllers.
- Reviewed the difference between middleware and action filters.
- Confirmed that middleware is appropriate for concerns that apply broadly across requests.

### Request Timing Testing

- Tested `GET /api/Patients`.
- Confirmed `200 OK`.
- Verified the request timing log for the Patients endpoint.
- Tested `GET /api/Appointments/1`.
- Confirmed `200 OK`.
- Verified the request timing log for the Appointments endpoint.
- Confirmed that the middleware applies consistently across multiple endpoints without per-endpoint changes.

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
- Review role assignment and endpoint access requirements
- Apply RBAC across the main API endpoints
- Add ownership protection for patient-specific appointment access
- Test Patient access against at least two Admin-only endpoints
- Test own-resource and cross-patient appointment access
- Identify a genuine cross-cutting concern
- Implement custom request timing middleware
- Register the custom middleware in the ASP.NET Core pipeline
- Test middleware behavior across multiple endpoints
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
- Custom Middleware
- `RequestDelegate`
- `HttpContext`
- `ILogger`
- `Stopwatch`
- Visual Studio
- Notion
- Postman
- jwt.io
- Git
- GitHub