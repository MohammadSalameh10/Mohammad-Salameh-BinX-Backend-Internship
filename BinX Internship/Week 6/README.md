# Week 6 — Sprint 1 Planning & Database Design

## Overview

Week 6 begins Phase 3 Sprint 1 for the **Cardiac Patient Monitoring System API**.

The first two days focused on Sprint Planning, reviewing and finalizing the existing database design, validating the EF Core domain model, reviewing Fluent API relationship configuration, reviewing the existing migrations, and confirming that the applied SQL Server schema matches the Day 1 ERD.

## Daily Work

| Day   | Topic                                          | Project / Documentation |
| ----- | ---------------------------------------------- | ----------------------- |
| Day 1 | Sprint 1 Planning & Project Database Design    | [View Day 1](./Day%201) |
| Day 2 | Building the EF Core Data Model & Migrations   | [View Day 2](./Day%202) |

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
- Git
- GitHub