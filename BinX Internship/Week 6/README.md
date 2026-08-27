# Week 6 — Sprint 1 Planning & Database Design

## Overview

Week 6 marked the beginning and completion of Phase 3 Sprint 1 for the **Cardiac Patient Monitoring System API**.

The week focused on Sprint planning, reviewing and validating the existing database design and EF Core model, improving read operations with pagination, filtering, sorting, and DTO projection, strengthening the patient registration flow with business logic and EF Core transaction handling, and closing Sprint 1 through a Postman demo, backlog review, and Sprint Retrospective.

## Daily Work

| Day   | Topic                                                     | Project / Documentation |
| ----- | --------------------------------------------------------- | ----------------------- |
| Day 1 | Sprint 1 Planning & Project Database Design               | [View Day 1](./Day%201) |
| Day 2 | Building the EF Core Data Model & Migrations              | [View Day 2](./Day%202) |
| Day 3 | Implementing Core Routes I — Catalog & Read Operations    | [View Day 3](./Day%203) |
| Day 4 | Implementing Core Routes II — Write Operations & Business Logic; Mentor Code Review | [View Day 4](./Day%204) |
| Day 5 | Sprint Review, Postman Demo & Retrospective | [View Day 5](./Day%205) |

## Week 6 Highlights

### Sprint 1 Planning

- Defined a clear Sprint 1 goal.
- Created a Sprint Backlog in Notion.
- Organized the Sprint 1 scope into clear and trackable tasks.
- Reviewed the existing project baseline instead of rebuilding previously completed functionality.

### Project Database Design

- Reviewed the main project entities:
  - `Patient`
  - `VitalSign`
  - `Medication`
  - `Appointment`
- Reviewed the ASP.NET Core Identity relationship with `Patient`.
- Documented the complete existing database schema.
- Reviewed primary keys, foreign keys, and entity relationships.
- Applied normalization principles from Week 3.

### ERD & EF Core Review

- Finalized the Entity Relationship Diagram.
- Verified that the ERD matches the current project models.
- Reviewed the existing `ApplicationDbContext`.
- Verified the configured one-to-one and one-to-many relationships.
- Reviewed the existing core API routes.

### EF Core Data Model & Relationships

- Reviewed all entity classes represented in the Day 1 ERD.
- Verified foreign key properties and navigation properties.
- Reviewed explicit relationship configuration using Fluent API.
- Reviewed the one-to-many relationships between `Patient` and:
  - `VitalSign`
  - `Medication`
  - `Appointment`
- Reviewed the one-to-one relationship between `IdentityUser` and `Patient`.
- Reviewed the explicit `DeleteBehavior.Cascade` decision for the Identity-to-Patient relationship.
- Reviewed the cascade delete behavior generated for the patient-related entities.

### Seed Data Review

- Reviewed the purpose of EF Core seed data and `HasData`.
- Confirmed that the current domain entities represent operational data rather than fixed reference data.
- Avoided adding artificial reference entities solely for seeding.
- Reviewed the existing startup seeding approach used by the project.

### Migration Review

- Reviewed the existing EF Core migrations:
  - `InitialCreate`
  - `AddIdentity`
  - `AddPatientIdentityRelationship`
- Verified generated tables, columns, primary keys, foreign keys, indexes, nullable constraints, and delete behaviors.
- Confirmed that no new migration was required because the current EF Core model already matched the existing schema.

### SQL Server Schema Verification

- Verified the applied database schema using SQL Server Object Explorer.
- Confirmed the main application tables:
  - `Patients`
  - `VitalSigns`
  - `Medications`
  - `Appointments`
- Confirmed the ASP.NET Core Identity tables.
- Verified the primary keys, foreign keys, indexes, and nullable fields.
- Confirmed the unique index on `Patients.UserId`.
- Confirmed that the SQL Server schema matches the Day 1 ERD.

### Core Read Operations

- Reviewed the existing list endpoints before adding new functionality.
- Selected the `Appointments` resource for the Day 3 implementation.
- Added pagination using `page` and `pageSize`.
- Created a reusable `PaginatedResponse<T>` response model.
- Added `TotalCount` to support client-side pagination.
- Preserved the existing `reason` filter.
- Added a second optional filter using `patientId`.
- Added sorting by `AppointmentDate`.
- Supported `date_asc` and `date_desc` sort options.
- Continued using `AppointmentResponse` instead of exposing EF Core entities.
- Kept DTO projection inside the EF Core query using `Select`.
- Reduced unnecessary over-fetching by selecting only the required response fields.
- Tested pagination, filtering, sorting, and combined query parameters using Postman.

### Write Operations, Business Logic & Transactions

- Reviewed the difference between simple CRUD and business logic.
- Selected patient registration as the Day 4 multi-step write operation.
- Kept registration business logic inside `AuthService`.
- Reviewed the existing user creation and Patient role assignment flow.
- Replaced manual cleanup with an EF Core database transaction.
- Injected `ApplicationDbContext` into `AuthService`.
- Started the transaction using `BeginTransactionAsync`.
- Wrapped user creation and Patient role assignment in a single transaction.
- Committed the transaction only when both steps completed successfully.
- Rolled back the transaction when registration or role assignment failed.
- Verified all-or-nothing transaction behavior.
- Tested successful registration and login using Postman.
- Intentionally forced role assignment to fail to verify the rollback path.
- Confirmed through `AspNetUsers` that the failed registration user was not persisted.
- Restored the correct `Patient` role after rollback testing.
- Built the project successfully in Visual Studio.
- Prepared the work on the `feature/week6-day4-transactions` branch.
- Opened a pull request into `main` for mentor review.
- Merged the Day 4 pull request into `main`.

### Sprint Review, Postman Demo & Retrospective

- Demonstrated the completed Sprint 1 API features using Postman against the running API.
- Verified the `Appointments` endpoint using pagination, filtering, and sorting together.
- Verified successful patient registration with `201 Created`.
- Verified successful login with `200 OK` and JWT token generation.
- Intentionally forced the registration role-assignment step to fail.
- Verified the rollback path using the returned error response.
- Confirmed through `AspNetUsers` that the failed registration user was not persisted.
- Reviewed all Sprint 1 backlog items during the close-out process.
- Confirmed that the current Sprint 1 backlog items were completed.
- Confirmed that no backlog item required moving to Sprint 2 during this review.
- Wrote a Sprint 1 Retrospective covering what went well and what could be improved.
- Defined a concrete Sprint 2 action: complete pull request review before merging Sprint 2 features into `main`.
- Prepared a Sprint 1 summary including ERD review, migration history, API improvements, transaction handling, backlog status, and Pull Request #2.
- Included the Postman collection and demo screenshots with the Day 5 documentation.

### Sprint 1 Backlog

The Sprint 1 backlog currently includes:

- Finalize complete database schema and relationships
- Finalize ERD
- Review existing EF Core entity configurations
- Verify existing migrations and database schema
- Review core API routes
- Confirm Sprint 1 baseline is working

The current backlog items were recorded as completed because the corresponding baseline work had already been implemented or reviewed.

## Tools Used

- C#
- ASP.NET Core Web API
- ASP.NET Core Identity
- Entity Framework Core
- SQL Server
- Fluent API
- SQL Server Object Explorer
- dbdiagram.io
- Notion
- Visual Studio
- LINQ
- DTOs
- Postman
- Git
- GitHub

## Week 6 Summary

Week 6 completed Phase 3 Sprint 1 for the **Cardiac Patient Monitoring System API**.

During the week, the existing project baseline was reviewed and strengthened through database design validation, ERD finalization, EF Core model and migration verification, SQL Server schema review, paginated and filtered read operations, DTO projection, and transaction-based write logic.

The sprint was closed with a Postman demo covering the main implemented API features, a review of the Sprint 1 backlog, verification of transaction rollback behavior, and a Sprint Retrospective with a concrete action for Sprint 2.

All current Sprint 1 backlog items were reviewed and confirmed as completed.

The main improvement action carried into Sprint 2 is to complete pull request review before merging Sprint 2 features into `main`.