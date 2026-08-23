# Week 6 — Sprint 1 Planning & Database Design

## Overview

Week 6 begins Phase 3 Sprint 1 for the **Cardiac Patient Monitoring System API**.

The first day focused on Sprint Planning, reviewing and finalizing the existing database schema, documenting the project entities and relationships, preparing the ERD, reviewing the existing EF Core migrations and core API routes, and organizing the Sprint 1 scope in a backlog.

## Daily Work

| Day | Topic | Project / Documentation |
|---|---|---|
| Day 1 | Sprint 1 Planning & Project Database Design | [View Day 1](./Day%201) |

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
- Reviewed the existing EF Core migrations and database schema.
- Reviewed the existing core API routes.

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
- dbdiagram.io
- Notion
- Visual Studio
- Git
- GitHub