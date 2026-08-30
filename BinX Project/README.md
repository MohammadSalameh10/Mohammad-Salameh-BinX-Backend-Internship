# Cardiac Patient Monitoring System API

## Overview

The Cardiac Patient Monitoring System API is a standalone ASP.NET Core REST API developed as an individual backend project.

The system provides backend functionality for managing cardiac patients, vital-sign measurements, medications, and appointments.

The project demonstrates ASP.NET Core Web API development, Entity Framework Core with SQL Server, asynchronous CRUD operations, LINQ, ASP.NET Core Identity, JWT authentication, role-based authorization, input validation, filtering, sorting, pagination, DTO projection, repository abstraction, transactional write operations, centralized exception handling, unit testing with xUnit and Moq, Swagger, and Postman.

---

## Features

- Patient profile management.
- Vital-sign measurement management.
- Medication management.
- Appointment management.
- ASP.NET Core Identity registration and login.
- JWT-based authentication.
- Role-based authorization using `Admin` and `Patient` roles.
- FluentValidation request validation.
- Asynchronous CRUD operations using Entity Framework Core.
- SQL Server database with Entity Framework Core migrations.
- Synthetic seed data for development and testing.
- Medication filtering by name.
- Appointment filtering by reason and patient ID.
- Appointment sorting by appointment date.
- Appointment pagination using `page` and `pageSize`.
- Reusable generic `PaginatedResponse<T>` for paginated API responses.
- Query-level DTO projection for appointment list operations.
- Repository abstraction for patient, vital-sign, medication, appointment, and authentication transaction data access.
- Transactional patient registration using commit and rollback behavior.
- Centralized exception handling using custom middleware.
- Standardized `ProblemDetails` responses for unexpected server errors.
- Structured error logging using `ILogger`.
- Unit testing using xUnit and Moq.
- Service unit testing with mocked repository dependencies.
- Controller unit testing with mocked service dependencies.
- Integration testing using `WebApplicationFactory` and an EF Core InMemory database.
- Authentication and authorization testing for protected API endpoints.
- Swagger/OpenAPI documentation.
- Postman collection for API testing.
- Recorded API demo with database evidence.

---

## Technologies Used

- .NET 10
- C#
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- JWT Authentication
- FluentValidation
- LINQ
- xUnit
- Moq
- Swagger / OpenAPI
- Postman
- Git
- GitHub
- Visual Studio

---

## Requirements

Before running the project, make sure the following are installed:

- .NET 10 SDK
- SQL Server
- Visual Studio
- Postman

---

## Project Structure

```text
BinX Project/
│
├── README.md
├── demo/
│   └── Cardiac API Demo.zip
├── docs/
│   └── Cardiac PatientMonitoringSystem_ERD.png
├── postman/
│   └── Cardiac Patient Monitoring System API.postman_collection.json
└── Cardiac Patient Monitoring System/
    ├── CardiacPatientMonitoringSystem.API/
    │   ├── Controllers/
    │   ├── Data/
    │   ├── DTOs/
    │   │   ├── Requests/
    │   │   └── Responses/
    │   ├── Middleware/
    │   │   └── ExceptionHandlingMiddleware.cs
    │   ├── Migrations/
    │   ├── Models/
    │   ├── Repositories/
    │   │   ├── Classes/
    │   │   └── Interfaces/
    │   ├── Services/
    │   │   ├── Classes/
    │   │   └── Interfaces/
    │   ├── Validators/
    │   ├── appsettings.json
    │   └── Program.cs
    ├── CardiacPatientMonitoringSystem.Tests/
    │   ├── Controllers/
    │   ├── Integration/
    │   └── Services/
    └── Cardiac Patient Monitoring System.slnx
```

---

## Core Modules

### Patients
Stores patient profile information including full name, date of birth, gender, phone number, and blood type. Each patient profile is linked to an ASP.NET Core Identity user.

### Vital Signs
Stores heart rate, systolic and diastolic blood pressure, oxygen saturation, and recorded date/time.

### Medications
Stores medication name, dosage, frequency, start date, and optional end date.

### Appointments
Stores appointment date, reason, and optional notes.

---

## Database

The application uses SQL Server with Entity Framework Core.

Main entities:

```text
Patient
VitalSign
Medication
Appointment
```

Relationships:

```text
IdentityUser
    │
    └── Patient
          │
          ├── VitalSigns
          ├── Medications
          └── Appointments
```

Each patient is linked to one Identity user. A patient can have multiple vital signs, medications, and appointments.

The database schema is created and updated using Entity Framework Core migrations.

---

## Repository Pattern

Repository abstractions separate service-layer business logic from direct Entity Framework Core data access and transaction management.

Implemented repositories:

```text
IPatientRepository
PatientRepository

IVitalSignRepository
VitalSignRepository

IMedicationRepository
MedicationRepository

IAppointmentRepository
AppointmentRepository

IAuthRepository
AuthRepository
```

General dependency flow:

```text
Controller
    ↓
Service
    ↓
Repository Interface
    ↓
Repository
    ↓
ApplicationDbContext
    ↓
SQL Server
```

Service dependencies:

```text
PatientService
→ IPatientRepository

VitalSignService
→ IVitalSignRepository

MedicationService
→ IMedicationRepository

AppointmentService
→ IAppointmentRepository

AuthService
→ IAuthRepository
```

`AuthService` continues to use ASP.NET Core Identity abstractions such as `UserManager<IdentityUser>` for user management, while transaction operations are abstracted through `IAuthRepository`.

`AuthRepository` uses `ApplicationDbContext` to manage EF Core database transactions, keeping direct `ApplicationDbContext` access out of `AuthService`.

---

## Database Setup

Update the connection string in:

```text
Cardiac Patient Monitoring System/CardiacPatientMonitoringSystem.API/appsettings.json
```

Example:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=CardiacPatientMonitoringSystemDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

In Visual Studio open:

```text
Tools
→ NuGet Package Manager
→ Package Manager Console
```

Set the default project to:

```text
CardiacPatientMonitoringSystem.API
```

Then run:

```powershell
Update-Database
```

---

## Seed Data

The application automatically adds synthetic development data when the project starts.

The seed process creates the `Admin` and `Patient` roles, Admin and Patient Identity users, a test patient profile, vital-sign records, a medication record, and an appointment record.

### Seed Admin Account

```text
Email: admin@cardiac.com
Password: Admin@123
```

### Seed Patient Account

```text
Email: patient@cardiac.com
Password: Patient@123
```

---

## Authentication

The API uses ASP.NET Core Identity for user management and password handling. JWT Bearer tokens authenticate protected requests.

### Register

```http
POST /api/Auths/register
```

```json
{
  "email": "patient2@cardiac.com",
  "password": "Patient@123"
}
```

New registered users receive the `Patient` role.

### Login

```http
POST /api/Auths/login
```

```json
{
  "email": "admin@cardiac.com",
  "password": "Admin@123"
}
```

Successful login returns a JWT token, which is sent using:

```http
Authorization: Bearer <token>
```

---

## Transactional Registration

Patient registration is a multi-step write operation.

```text
Begin Transaction
        ↓
Create Identity User
        ↓
Assign Patient Role
        ↓
Commit
```

If user creation or role assignment fails:

```text
Failure
    ↓
Rollback
```

This provides all-or-nothing behavior and prevents partially completed registrations from remaining in the database.

Transaction management is abstracted through:

```text
AuthService
    ↓
IAuthRepository
    ↓
AuthRepository
    ↓
ApplicationDbContext
```

Rollback behavior was manually verified by intentionally forcing role assignment to fail and confirming that the created user was not persisted in `AspNetUsers`.

---

## Authorization

The API uses `Admin` and `Patient` roles.

### Admin Permissions

The Admin can view, update, and delete patients, vital signs, medications, and appointments. The Admin can also filter medications and filter, sort, and paginate appointments.

### Patient Permissions

The Patient can create a patient profile, vital-sign records, medication records, and appointment records. A Patient must create a patient profile before creating related records.

---

## API Endpoints

### Authentication

| Method | Endpoint | Authorization | Description |
| --- | --- | --- | --- |
| POST | `/api/Auths/register` | Public | Register a Patient account |
| POST | `/api/Auths/login` | Public | Login and receive JWT token |

### Patients

| Method | Endpoint | Authorization | Description |
| --- | --- | --- | --- |
| GET | `/api/Patients` | Admin | Get all patients |
| GET | `/api/Patients/{id}` | Admin | Get patient by ID |
| POST | `/api/Patients` | Patient | Create patient profile |
| PUT | `/api/Patients/{id}` | Admin | Update patient |
| DELETE | `/api/Patients/{id}` | Admin | Delete patient |

### Vital Signs

| Method | Endpoint | Authorization | Description |
| --- | --- | --- | --- |
| GET | `/api/VitalSigns` | Admin | Get all vital signs |
| GET | `/api/VitalSigns/{id}` | Admin | Get vital sign by ID |
| POST | `/api/VitalSigns` | Patient | Create vital sign |
| PUT | `/api/VitalSigns/{id}` | Admin | Update vital sign |
| DELETE | `/api/VitalSigns/{id}` | Admin | Delete vital sign |

### Medications

| Method | Endpoint | Authorization | Description |
| --- | --- | --- | --- |
| GET | `/api/Medications` | Admin | Get all medications |
| GET | `/api/Medications/{id}` | Admin | Get medication by ID |
| GET | `/api/Medications?name={name}` | Admin | Filter medications by name |
| POST | `/api/Medications` | Patient | Create medication |
| PUT | `/api/Medications/{id}` | Admin | Update medication |
| DELETE | `/api/Medications/{id}` | Admin | Delete medication |

### Appointments

| Method | Endpoint | Authorization | Description |
| --- | --- | --- | --- |
| GET | `/api/Appointments` | Admin | Get paginated appointments |
| GET | `/api/Appointments/{id}` | Admin | Get appointment by ID |
| GET | `/api/Appointments?reason={reason}` | Admin | Filter appointments by reason |
| GET | `/api/Appointments?patientId={id}` | Admin | Filter appointments by patient ID |
| GET | `/api/Appointments?sort=date_asc` | Admin | Sort appointments by date ascending |
| GET | `/api/Appointments?sort=date_desc` | Admin | Sort appointments by date descending |
| POST | `/api/Appointments` | Patient | Create appointment |
| PUT | `/api/Appointments/{id}` | Admin | Update appointment |
| DELETE | `/api/Appointments/{id}` | Admin | Delete appointment |

---

## Filtering, Sorting, and Pagination

### Medication Name Filter

```http
GET /api/Medications?name=Aspirin
```

If no matching medications are found, the API returns `200 OK` with:

```json
[]
```

### Appointment Filtering

```http
GET /api/Appointments?reason=Routine
```

```http
GET /api/Appointments?patientId=1
```

### Appointment Sorting

```http
GET /api/Appointments?sort=date_asc
```

```http
GET /api/Appointments?sort=date_desc
```

The default ordering is ascending by appointment date.

### Appointment Pagination

```http
GET /api/Appointments?page=1&pageSize=10
```

The response uses the reusable `PaginatedResponse<T>` model:

```json
{
  "page": 1,
  "pageSize": 10,
  "totalCount": 2,
  "items": [
    {
      "id": 1,
      "patientId": 1,
      "appointmentDate": "2026-09-01T10:00:00",
      "reason": "Routine cardiac follow-up",
      "notes": "Synthetic test appointment"
    }
  ]
}
```

If no matching appointments exist:

```json
{
  "page": 1,
  "pageSize": 10,
  "totalCount": 0,
  "items": []
}
```

### Combined Appointment Query

```http
GET /api/Appointments?patientId=1&sort=date_desc&page=1&pageSize=1
```

Appointment list queries project directly to `AppointmentResponse` DTOs using `Select` before query execution, reducing unnecessary data over-fetching.

---

## Validation

Request validation is implemented using FluentValidation for patient, vital-sign, medication, appointment, registration, and login requests.

Examples include required fields, valid email, past date of birth, valid blood type, oxygen saturation between `0` and `100`, non-future vital-sign recording time, valid medication date ranges, and future appointment dates.

Invalid input returns `400 Bad Request` with structured validation errors.

---

## Centralized Exception Handling

Unexpected exceptions are handled centrally using `ExceptionHandlingMiddleware`.

The middleware catches unhandled exceptions, logs them using `ILogger<ExceptionHandlingMiddleware>`, uses structured request-path logging, returns `500 Internal Server Error`, and returns standardized `ProblemDetails` without exposing internal exception details.

Example:

```json
{
  "title": "An unexpected error occurred.",
  "status": 500,
  "instance": "/api/Auths/register"
}
```

---

## Testing

The project includes unit and integration tests using xUnit and Moq.

### Service Unit Tests

The service layer is tested in isolation with mocked repository dependencies for:

```text
PatientService
VitalSignService
MedicationService
AppointmentService
```

Tests cover important success and failure paths for create, update, and delete operations. `VitalSignService` also includes heart-rate status and GetById tests.

### Controller Unit Tests

Controller tests cover:

```text
PatientsController
VitalSignsController
MedicationsController
AppointmentsController
AuthsController
```

They verify responses including `200 OK`, `201 Created`, `204 No Content`, `400 Bad Request`, `401 Unauthorized`, and `404 Not Found`.

### Integration Testing

Integration tests use:

```text
WebApplicationFactory<Program>
Entity Framework Core InMemory Database
HttpClient
JWT Authentication
```

Current integration scenarios verify:

```text
GET /api/VitalSigns/{id}

Existing VitalSign → 200 OK
Missing VitalSign → 404 Not Found
Missing JWT Token → 401 Unauthorized
```

Production seed data is skipped in the `Testing` environment.

### Test Result

```text
Total: 75
Passed: 75
Failed: 0
Skipped: 0
```

---

## HTTP Status Codes

| Status Code | Meaning |
| --- | --- |
| `200 OK` | Successful GET or update operation |
| `201 Created` | Resource created successfully |
| `204 No Content` | Resource deleted successfully |
| `400 Bad Request` | Invalid request or invalid application state |
| `401 Unauthorized` | Authentication is required |
| `403 Forbidden` | Authenticated user does not have the required role |
| `404 Not Found` | Requested resource does not exist |
| `500 Internal Server Error` | Unexpected server error |

---

## Swagger

Swagger/OpenAPI is available while running the application in the Development environment and can be used to inspect endpoints and request/response models.

---

## Postman Collection

A Postman collection is included in:

```text
postman/Cardiac Patient Monitoring System API.postman_collection.json
```

The collection uses:

```text
baseUrl
adminToken
patientToken
```

Store the returned JWT token in the appropriate collection variable after login.

---

## Demo

A compressed recorded API demonstration is included in:

```text
demo/Cardiac API Demo.zip
```

The demo shows API testing, JWT authentication, role authorization, patient registration/profile creation, validation, filtering, CRUD operations, and SQL Server database evidence.

---

## Running the Project

### 1. Open the Project

Open:

```text
BinX Project/Cardiac Patient Monitoring System/Cardiac Patient Monitoring System.slnx
```

using Visual Studio.

### 2. Configure SQL Server

Update `DefaultConnection` in:

```text
Cardiac Patient Monitoring System/CardiacPatientMonitoringSystem.API/appsettings.json
```

if necessary.

### 3. Create the Database

In Package Manager Console, with `CardiacPatientMonitoringSystem.API` selected:

```powershell
Update-Database
```

### 4. Run the Application

Run `CardiacPatientMonitoringSystem.API` from Visual Studio.

### 5. Test the API

Use Swagger or import:

```text
postman/Cardiac Patient Monitoring System API.postman_collection.json
```

into Postman.

### 6. Run Tests

Run `CardiacPatientMonitoringSystem.Tests` from Visual Studio Test Explorer.

Current result:

```text
Total: 75
Passed: 75
Failed: 0
Skipped: 0
```

---

## Verification

The following scenarios were verified:

- Admin login.
- Patient registration and login.
- JWT authentication.
- Admin and Patient role authorization.
- `401 Unauthorized` and `403 Forbidden`.
- Patient CRUD.
- Vital-sign CRUD.
- Medication CRUD.
- Appointment CRUD.
- Request validation failures.
- Duplicate patient-profile prevention.
- Missing patient-profile handling.
- `404 Not Found`.
- Medication filtering.
- Appointment filtering by reason.
- Appointment filtering by patient ID.
- Appointment pagination using `page` and `pageSize`.
- Appointment sorting by date ascending and descending.
- Combined appointment filtering, sorting, and pagination.
- Empty medication filtering results returning `200 OK` with `[]`.
- Empty paginated appointment results returning `200 OK` with an empty `items` collection.
- Database recreation using Entity Framework Core migrations.
- Synthetic seed data creation.
- Transactional patient registration.
- Successful transaction commit verified through registration and login.
- Transaction rollback verified using a forced role-assignment failure.
- Failed registration confirmed as not persisted in `AspNetUsers`.
- Centralized exception handling.
- Standardized `ProblemDetails` response for unexpected errors.
- Service and controller unit tests.
- Integration tests using `WebApplicationFactory` and EF Core InMemory.
- Full automated test suite completed successfully with 75 passed tests and 0 failures.

---

## Sample Seed Data

```text
Patient ID: 1
Patient Name: Test Patient
Blood Type: O+
```

Vital signs:

```text
ID: 1
Heart Rate: 72
Blood Pressure: 120/80
Oxygen Saturation: 98%

ID: 2
Heart Rate: 76
Blood Pressure: 118/78
Oxygen Saturation: 97%
```

Medication:

```text
ID: 1
Name: Aspirin
Dosage: 81 mg
Frequency: Once daily
```

Appointment:

```text
ID: 1
Reason: Routine cardiac follow-up
```

---

## Notes

- The project uses synthetic data only and does not contain real patient information.
- JWT tokens are not stored in the Postman collection.
- Delete requests use non-seeded IDs by default to avoid accidentally deleting seed data.
- The recorded API demo is included in the `demo` folder.
