# Week 6 — Day 1: Sprint 1 Planning & Project Database Design

## Overview

Day 1 focused on Sprint 1 planning and reviewing the complete database design of the existing **Cardiac Patient Monitoring System API**.

Because the project baseline had already been implemented during the previous training work, the Day 1 exercise focused on reviewing and finalizing the existing database schema, documenting the project entities and relationships, preparing an updated ERD, reviewing the existing migrations and core API routes, and organizing the Sprint 1 work in a backlog.

A Sprint 1 planning page was also prepared in Notion and exported as PDF for documentation.

---

## Learning Objectives

The objectives of this exercise were to:

- Understand the purpose of Sprint Planning.
- Define a clear Sprint 1 goal.
- Identify the complete set of entities used by the project.
- Review the existing database schema and relationships.
- Apply the normalization principles introduced in Week 3.
- Finalize and document the project ERD.
- Review the existing EF Core entity configurations and migrations.
- Review the existing core API routes.
- Break Sprint 1 work into clear and trackable backlog tasks.

---

## Sprint Planning

Sprint Planning defines what should be considered complete by the end of the sprint.

For Sprint 1, the main focus is:

```text
Database Schema
        ↓
ERD
        ↓
EF Core Migrations
        ↓
Core API Routes
        ↓
Working Sprint Baseline
```

Since the Cardiac Patient Monitoring System already contained an implemented baseline, Sprint 1 planning was used to review and document the existing work instead of rebuilding the same functionality.

---

## Sprint 1 Goal

The Sprint 1 goal was defined as:

> Review and finalize the complete database schema for the Cardiac Patient Monitoring System, verify the existing EF Core migrations, and confirm the core API routes for the primary patient-monitoring workflow.

This goal represents the expected Sprint 1 baseline for the project.

---

## Project Entities

The Cardiac Patient Monitoring System currently uses the following main domain entities:

```text
Patient
VitalSign
Medication
Appointment
```

The project also uses ASP.NET Core Identity for authentication and user management through:

```text
IdentityUser
AspNetUsers
```

The main entity relationships are:

```text
IdentityUser 1 → 1 Patient

Patient 1 → Many VitalSigns
Patient 1 → Many Medications
Patient 1 → Many Appointments
```

---

## Database Schema

The existing database schema was reviewed and documented.

### Patient

```text
Id
→ Primary Key

UserId
→ Foreign Key → AspNetUsers.Id

FullName
DateOfBirth
Gender
PhoneNumber
BloodType
```

`Patient.UserId` has a unique index, which supports the one-to-one relationship between an Identity user and a Patient.

---

### VitalSign

```text
Id
→ Primary Key

PatientId
→ Foreign Key → Patient.Id

HeartRate
SystolicBloodPressure
DiastolicBloodPressure
OxygenSaturation
RecordedAt
```

Relationship:

```text
Patient 1 → Many VitalSigns
```

---

### Medication

```text
Id
→ Primary Key

PatientId
→ Foreign Key → Patient.Id

Name
Dosage
Frequency
StartDate
EndDate
```

Relationship:

```text
Patient 1 → Many Medications
```

---

### Appointment

```text
Id
→ Primary Key

PatientId
→ Foreign Key → Patient.Id

AppointmentDate
Reason
Notes
```

Relationship:

```text
Patient 1 → Many Appointments
```

---

## Identity Relationship

The application database context inherits from:

```csharp
IdentityDbContext<IdentityUser>
```

The main Identity relationship used by the domain is:

```text
AspNetUsers.Id
      ↓
Patient.UserId
```

This relationship is configured as:

```text
One-to-One
```

The `UserId` property also has a unique index to ensure that one Identity user cannot be linked to multiple Patient records.

---

## EF Core Relationship Configuration

The existing `ApplicationDbContext` was reviewed to verify that the entity relationships match the documented schema.

The following relationships are configured using Fluent API:

```text
Patient
→ HasMany VitalSigns
→ WithOne Patient
→ Foreign Key: PatientId
```

```text
Patient
→ HasMany Medications
→ WithOne Patient
→ Foreign Key: PatientId
```

```text
Patient
→ HasMany Appointments
→ WithOne Patient
→ Foreign Key: PatientId
```

The Patient and IdentityUser relationship is configured as one-to-one using:

```text
Patient.UserId
→ AspNetUsers.Id
```

---

## Database Normalization Review

The schema was reviewed using the normalization principles introduced in Week 3.

The project separates related data into dedicated entities instead of repeatedly storing patient information inside other records.

For example:

```text
Patient
→ Stores patient information
```

while:

```text
VitalSign
→ Stores patient vital-sign measurements

Medication
→ Stores patient medication information

Appointment
→ Stores patient appointment information
```

The related entities reference the Patient using:

```text
PatientId
```

This reduces unnecessary data duplication and keeps each table focused on its own responsibility.

---

## ERD

The finalized Entity Relationship Diagram documents the main project entities and their relationships.

The ERD includes:

```text
AspNetUsers
Patient
VitalSign
Medication
Appointment
```

The relationships shown in the ERD are:

```text
AspNetUsers 1 → 1 Patient

Patient 1 → Many VitalSigns

Patient 1 → Many Medications

Patient 1 → Many Appointments
```

The ERD was reviewed against the current entity models and `ApplicationDbContext` configuration to ensure that the documentation matches the implemented database structure.

---

## Sprint Backlog

A Sprint Backlog was created in Notion to organize the Sprint 1 scope.

The following backlog items were documented:

```text
Finalize complete database schema and relationships
Finalize ERD
Review existing EF Core entity configurations
Verify existing migrations and database schema
Review core API routes
Confirm Sprint 1 baseline is working
```

Because these parts of the project had already been implemented or reviewed during previous training work, the backlog items were recorded as completed.

```text
Status
→ Done
```

---

## Task Sizing

Sprint backlog items should be small enough to track clearly instead of representing large and vague pieces of work.

For example, instead of using a task such as:

```text
Build the entire backend
```

the Sprint 1 work was separated into focused tasks for:

```text
Database Schema
ERD
EF Core Configuration
Migrations
Core API Routes
Sprint Baseline Review
```

This makes sprint progress easier to understand and review.

---

## Sprint 1 Baseline Review

The existing Cardiac Patient Monitoring System baseline was reviewed as part of the Sprint 1 planning work.

The project already contains:

```text
Domain Entities
Database Relationships
Entity Framework Core
ASP.NET Core Identity
Existing Migrations
Core API Routes
```

Therefore, Day 1 focused on validating, organizing, and documenting the existing baseline rather than recreating functionality that was already implemented.

---

## Hands-On Lab Completed

The Day 1 hands-on work was completed as follows:

1. Defined a one-sentence Sprint 1 goal.
2. Created a Sprint 1 planning page in Notion.
3. Created a Sprint Backlog.
4. Identified the project's main entities:
   - `Patient`
   - `VitalSign`
   - `Medication`
   - `Appointment`
5. Reviewed the ASP.NET Core Identity relationship with `Patient`.
6. Documented the complete existing database schema.
7. Reviewed the database relationships.
8. Reviewed the schema using normalization principles.
9. Finalized the project ERD.
10. Reviewed the existing EF Core entity configurations.
11. Reviewed the existing migrations and database schema.
12. Reviewed the existing core API routes.
13. Confirmed that the Sprint 1 baseline is already working.
14. Organized the Sprint 1 scope into clear backlog tasks.
15. Exported the Sprint 1 planning documentation from Notion as PDF.

---

## Project Structure Reviewed

The main project files reviewed during this exercise were:

```text
CardiacPatientMonitoringSystem.API
│
├── Data
│   └── ApplicationDbContext.cs
│
└── Models
    ├── Patient.cs
    ├── VitalSign.cs
    ├── Medication.cs
    └── Appointment.cs
```

The existing project database design can be summarized as:

```text
AspNetUsers
     │
     │ 1 : 1
     ↓
 Patient
   │
   ├── 1 : Many → VitalSigns
   ├── 1 : Many → Medications
   └── 1 : Many → Appointments
```

---

## Tools Used

- C#
- ASP.NET Core Web API
- ASP.NET Core Identity
- Entity Framework Core
- SQL Server
- Fluent API
- `ApplicationDbContext`
- `IdentityDbContext<IdentityUser>`
- dbdiagram.io
- Notion
- Visual Studio
- Git
- GitHub