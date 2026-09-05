# Cardiac Patient Monitoring System API

## Overview

The Cardiac Patient Monitoring System API is a standalone ASP.NET Core REST API developed as an individual backend project.

The system provides backend functionality for managing cardiac patients, doctors, vital-sign measurements, medications, and appointments.

The project demonstrates ASP.NET Core Web API development, Entity Framework Core with SQL Server, asynchronous CRUD operations, LINQ, ASP.NET Core Identity, JWT authentication, role-based and resource-based authorization, domain-specific JWT claims, Doctor and Patient account linking, input validation, filtering, sorting, pagination, DTO projection, repository abstraction, transactional write operations, centralized exception handling, custom request-timing middleware, unit testing with xUnit and Moq, Swagger, and Postman.

---

## Features

- Patient profile management.
- Vital-sign measurement management.
- Medication management.
- Appointment management.
- ASP.NET Core Identity registration and login.
- JWT-based authentication.
- Role-based authorization using `Admin`, `Patient`, and `Doctor` roles.
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
- Repository abstraction for patient, doctor, vital-sign, medication, appointment, and authentication transaction data access.
- Transactional Patient registration that creates the Identity user, assigns the Patient role, and creates the linked Patient record using commit and rollback behavior.
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
- Automatic Patient profile creation during public registration.
- Domain-specific `PatientId` claim included in Patient JWT tokens.
- Resource ownership protection for Patient appointment access.
- Cross-patient appointment access protection using `404 Not Found`.
- Custom request-timing middleware using `Stopwatch` and `ILogger`.
- Centralized logging of HTTP method, request path, response status code, and elapsed execution time.
- Doctor profile management.
- Admin-managed Doctor account creation.
- Domain-specific `DoctorId` claim included in Doctor JWT tokens.
- Doctor-to-Patient relationship established through appointments.
- Doctor access to their own appointments.
- Doctor access to Vital Signs for Patients linked through appointments.
- Doctor access to Medications for Patients linked through appointments.
- Doctor access protection using appointment-based relationship checks.
- Safe Doctor deletion using `409 Conflict` when existing appointments are linked.
- Transactional Doctor account creation with Identity user creation, Doctor role assignment, and linked Doctor profile creation.
- Appointment creation and update validation using `DoctorId`.

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
    │   │   ├── ExceptionHandlingMiddleware.cs
    │   │   └── RequestTimingMiddleware.cs
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

### Doctors

Stores doctor profile information including full name and phone number. Each doctor profile is linked to an ASP.NET Core Identity user with the `Doctor` role.

Doctors are linked to Patients through Appointments and can access the Vital Signs and Medications of Patients associated with their appointments.

### Vital Signs
Stores heart rate, systolic and diastolic blood pressure, oxygen saturation, and recorded date/time.

### Medications
Stores medication name, dosage, frequency, start date, and optional end date.

### Appointments

Stores appointment date, reason, optional notes, Patient ID, and Doctor ID.

Each appointment links one Patient with one Doctor.

---

## Database

The application uses SQL Server with Entity Framework Core.

Main entities:

```text
Patient
Doctor
VitalSign
Medication
Appointment
```

Relationships:

```text
IdentityUser
    │
    ├── Patient
    │     │
    │     ├── VitalSigns
    │     ├── Medications
    │     └── Appointments
    │
    └── Doctor
          │
          └── Appointments
```

Each Patient is linked to one ASP.NET Core Identity user, and each Doctor is also linked to one Identity user.

A Patient can have multiple Vital Signs, Medications, and Appointments. A Doctor can also have multiple Appointments.

Each Appointment links one Patient with one Doctor.

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

IDoctorRepository
DoctorRepository

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

DoctorService
→ IDoctorRepository

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

The seed process creates the `Admin`, `Patient`, and `Doctor` roles, Admin, Patient, and Doctor Identity users, a test patient profile, a test doctor profile, vital-sign records, a medication record, and an appointment linked to both the Patient and Doctor.

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

### Seed Doctor Account
```text
Email: doctor@cardiac.com
Password: Doctor@123
```

---

## Authentication

The API uses ASP.NET Core Identity for user management and password handling. JWT Bearer tokens authenticate protected requests.

### Register

```http
POST /api/Auth/register
```

```json
{
  "email": "patient2@cardiac.com",
  "password": "Patient@123",
  "fullName": "Test Patient Two",
  "dateOfBirth": "2001-03-18",
  "gender": "Male",
  "phoneNumber": "0599876543",
  "bloodType": "A+"
}
```

New registered users receive the `Patient` role, and a linked Patient profile is created automatically during registration.

Successful Patient login returns a JWT containing the Identity user information, the `Patient` role, and the domain-specific `PatientId` claim.

Successful Doctor login returns a JWT containing the Identity user information, the `Doctor` role, and the domain-specific `DoctorId` claim.

### Login

```http
POST /api/Auth/login
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

### Doctor Login
```json
{
  "email": "doctor@cardiac.com",
  "password": "Doctor@123"
}
```

Successful Doctor login returns a JWT that includes:

```text
Role = Doctor
DoctorId = linked Doctor ID
```

---

## Transactional Registration

Patient registration is implemented as a multi-step transactional operation.

```text
Begin Transaction
        ↓
Create IdentityUser
        ↓
Assign Patient Role
        ↓
Create linked Patient Record
        ↓
Commit Transaction
```

The created `Patient` record stores the new Identity user's ID in:

```text
Patient.UserId
```

If any step fails:

```text
Failure
    ↓
Rollback Transaction
```

This provides all-or-nothing behavior and prevents partially completed registrations such as an Identity user without a corresponding Patient profile.

Transaction and Patient data access are abstracted through:

```text
AuthService
    ↓
IAuthRepository
    ↓
AuthRepository
    ↓
ApplicationDbContext
```

`AuthService` continues to use `UserManager<IdentityUser>` for ASP.NET Core Identity operations while avoiding direct `ApplicationDbContext` access.

Successful transaction behavior was verified by registering a Patient and confirming the linked records in both `AspNetUsers` and `Patients`.

Rollback behavior was also manually verified by intentionally forcing role assignment to fail and confirming that the created user was not persisted in `AspNetUsers`.

### Transactional Doctor Creation

Doctor accounts are created by the Admin as a multi-step transactional operation.

```text
Begin Transaction
        ↓
Create IdentityUser
        ↓
Assign Doctor Role
        ↓
Create linked Doctor Record
        ↓
Commit Transaction
```

If any step fails:

```text
Failure
    ↓
Rollback Transaction
```

This prevents partially created Doctor accounts such as an Identity user without a linked Doctor profile.

Doctor creation follows this dependency flow:

```text
DoctorsController
        ↓
DoctorService
        ↓
IDoctorRepository
        ↓
DoctorRepository
        ↓
ApplicationDbContext
```

`DoctorService` uses `UserManager<IdentityUser>` for Identity operations while database access and transaction management are handled through `IDoctorRepository`.

---

## Authorization

The API uses both role-based authorization and resource ownership checks.

The available roles are:
```text
Admin
Patient
Doctor
```

Public registration automatically assigns the `Patient` role.

### Admin Permissions

The Admin can also create, view, update, and delete Doctor profiles and accounts.

Doctor deletion is blocked with `409 Conflict` when the Doctor has existing appointments.

The Admin can view, update, and delete patients, vital signs, medications, and appointments.

The Admin can also filter medications and filter, sort, and paginate appointments.

An Admin can access any appointment by ID.

### Patient Permissions

A registered Patient receives a linked Patient profile automatically during registration.

Patients can create vital-sign records, medication records, and appointments associated with their account.

Patients can also retrieve an individual appointment only when the appointment belongs to their own `PatientId`.

### Doctor Permissions

Doctors are created and managed by the Admin.

A Doctor can:

- Login using a Doctor account.
- Receive a JWT containing the `Doctor` role and `DoctorId` claim.
- View their own appointments.
- View Vital Signs for Patients linked to them through appointments.
- View Medications for Patients linked to them through appointments.

Doctors cannot access Admin-only endpoints.

### Doctor-Patient Access Protection

Doctor access to Patient Vital Signs and Medications is based on the appointment relationship.

The API reads the `DoctorId` claim from the JWT and checks whether an appointment exists between the Doctor and the requested Patient.

```text
Doctor linked to Patient through Appointment
→ 200 OK

Doctor not linked to Patient
→ 403 Forbidden
```

This prevents Doctors from accessing data belonging to unrelated Patients.

### Appointment Ownership Protection

The individual appointment endpoint allows both `Admin` and `Patient` roles.

For a Patient request, the API reads the `PatientId` claim from the JWT and compares it with the `PatientId` of the requested appointment.

```text
Admin
→ Can access any appointment

Patient
→ Can access own appointment
→ Cannot access another Patient's appointment
```

If a Patient attempts to access an appointment belonging to another Patient, the API returns:

```text
404 Not Found
```

This prevents cross-patient resource access without exposing whether another Patient's appointment exists.

---

## API Endpoints

### Authentication

| Method | Endpoint | Authorization | Description |
| --- | --- | --- | --- |
| POST | `/api/Auth/register` | Public | Register a Patient account |
| POST | `/api/Auth/login` | Public | Login and receive JWT token |

### Patients

| Method | Endpoint | Authorization | Description |
| --- | --- | --- | --- |
| GET | `/api/Patients` | Admin | Get all patients |
| GET | `/api/Patients/{id}` | Admin | Get patient by ID |
| POST | `/api/Patients` | Patient | Create patient profile |
| PUT | `/api/Patients/{id}` | Admin | Update patient |
| DELETE | `/api/Patients/{id}` | Admin | Delete patient |

### Doctors

| Method | Endpoint            | Authorization | Description                          |
| ------ | ------------------- | ------------- | ------------------------------------ |
| GET    | `/api/Doctors`      | Admin         | Get all doctors                      |
| GET    | `/api/Doctors/{id}` | Admin         | Get doctor by ID                     |
| POST   | `/api/Doctors`      | Admin         | Create Doctor account and profile    |
| PUT    | `/api/Doctors/{id}` | Admin         | Update doctor profile                |
| DELETE | `/api/Doctors/{id}` | Admin         | Delete doctor if no appointments exist |

### Vital Signs

| Method | Endpoint | Authorization | Description |
| --- | --- | --- | --- |
| GET | `/api/VitalSigns` | Admin | Get all vital signs |
| GET | `/api/VitalSigns/{id}` | Admin | Get vital sign by ID |
| GET | `/api/VitalSigns/patient/{patientId}/doctor` | Doctor | Get Vital Signs for a linked Patient |
| POST | `/api/VitalSigns` | Patient | Create vital sign |
| PUT | `/api/VitalSigns/{id}` | Admin | Update vital sign |
| DELETE | `/api/VitalSigns/{id}` | Admin | Delete vital sign |

### Medications

| Method | Endpoint | Authorization | Description |
| --- | --- | --- | --- |
| GET | `/api/Medications` | Admin | Get all medications |
| GET | `/api/Medications/{id}` | Admin | Get medication by ID |
| GET | `/api/Medications?name={name}` | Admin | Filter medications by name |
| GET | `/api/Medications/patient/{patientId}/doctor` | Doctor | Get Medications for a linked Patient |
| POST | `/api/Medications` | Patient | Create medication |
| PUT | `/api/Medications/{id}` | Admin | Update medication |
| DELETE | `/api/Medications/{id}` | Admin | Delete medication |

### Appointments

| Method | Endpoint | Authorization | Description |
| --- | --- | --- | --- |
| GET | `/api/Appointments` | Admin | Get paginated appointments |
| GET | `/api/Appointments/{id}` | Admin / Patient | Get appointment by ID with Patient ownership protection |
| GET | `/api/Appointments?reason={reason}` | Admin | Filter appointments by reason |
| GET | `/api/Appointments?patientId={id}` | Admin | Filter appointments by patient ID |
| GET | `/api/Appointments?sort=date_asc` | Admin | Sort appointments by date ascending |
| GET | `/api/Appointments?sort=date_desc` | Admin | Sort appointments by date descending |
| GET | `/api/Appointments/doctor` | Doctor | Get appointments for the authenticated Doctor |
| POST | `/api/Appointments` | Patient | Create appointment with a selected Doctor |
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
      "doctorId": 1,
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
  "instance": "/api/Auth/register"
}
```

---

## Request Timing Middleware

The project includes a custom `RequestTimingMiddleware` for measuring HTTP request execution time as a cross-cutting concern.

The middleware uses `Stopwatch` and `ILogger` to log:

```text
HTTP Method
Request Path
Response Status Code
Elapsed Time
```

Example log:

```text
HTTP GET /api/Appointments/5 responded 200 in 180 ms
```

The middleware is registered in the ASP.NET Core request pipeline after `ExceptionHandlingMiddleware`.

```text
ExceptionHandlingMiddleware
        ↓
RequestTimingMiddleware
        ↓
HTTPS Redirection
        ↓
Authentication
        ↓
Authorization
        ↓
Controllers
```

This keeps request timing logic centralized instead of duplicating it across individual controllers.

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
AuthController
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

### Authorization and Ownership Verification

Authorization and ownership behavior was manually verified using Postman.

The verified scenarios include:

```text
Patient registration → 201 Created

Patient login → 200 OK + JWT

JWT contains:
Role = Patient
PatientId = linked Patient ID

Patient accesses own appointment
→ 200 OK

Patient accesses another Patient's appointment
→ 404 Not Found

Patient accesses Admin-only endpoint
→ 403 Forbidden

Doctor login → 200 OK + JWT

JWT contains:
Role = Doctor
DoctorId = linked Doctor ID

Doctor accesses own appointments
→ 200 OK

Doctor accesses linked Patient Vital Signs
→ 200 OK

Doctor accesses linked Patient Medications
→ 200 OK

Doctor accesses unrelated Patient data
→ 403 Forbidden

Doctor accesses Admin-only endpoint
→ 403 Forbidden
```

These checks confirm role-based authorization, Patient appointment ownership protection, and Doctor access control based on appointment-linked Patient relationships.

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
| `409 Conflict` | Request conflicts with the current resource state |
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
doctorToken
```

Store the returned JWT token in the appropriate collection variable after login.

Use `adminToken` for Admin requests, `patientToken` for Patient requests, and `doctorToken` for Doctor requests.

---

## Demo

A compressed recorded API demonstration is included in:

```text
demo/Cardiac API Demo.zip
```

The demo shows API testing, Patient registration with automatic profile creation, JWT authentication, role-based authorization, appointment ownership protection, validation, filtering, CRUD operations, and SQL Server database evidence.

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
- Admin, Patient, and Doctor role authorization.
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
- Patient registration creating both the `IdentityUser` and linked `Patient` record.
- `PatientId` JWT claim generation and verification.
- Patient access to an Admin-only endpoint rejected with `403 Forbidden`.
- Patient accessing their own appointment with `200 OK`.
- Cross-patient appointment access rejected with `404 Not Found`.
- Appointment resource ownership protection.
- Custom request timing using `RequestTimingMiddleware`.
- HTTP method, request path, response status code, and elapsed-time logging.
- Doctor role creation and seeding.
- Doctor Identity account linked to a Doctor profile.
- Doctor login with `DoctorId` claim in the JWT.
- Doctor CRUD using Admin authorization.
- Doctor access to their own appointments.
- Doctor access to linked Patient Vital Signs.
- Doctor access to linked Patient Medications.
- Doctor access to an unrelated Patient rejected with `403 Forbidden`.
- Doctor token rejected from Admin-only Doctor endpoints with `403 Forbidden`.
- Patient appointment creation with a valid `DoctorId`.
- Appointment creation rejected when the selected Doctor does not exist.
- Doctor deletion blocked with `409 Conflict` when existing appointments are linked.
- Doctor profile and linked Identity user deleted together when no appointments exist.
- Full automated test suite completed successfully after Doctor-related changes with 75 passed tests and 0 failures.

---

## Sample Seed Data

```text
Patient ID: 1
Patient Name: Test Patient
Blood Type: O+
```

Doctor:
```text
Doctor ID: 1
Doctor Name: Test Doctor
Phone Number: 0599111111
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
Patient ID: 1
Doctor ID: 1
Reason: Routine cardiac follow-up
```

---

## Notes

- The project uses synthetic data only and does not contain real patient information.
- JWT tokens are not stored in the Postman collection.
- Delete requests use non-seeded IDs by default to avoid accidentally deleting seed data.
- The recorded API demo is included in the `demo` folder.
- Doctor accounts are created and managed by the Admin; public registration remains available only for Patients.
- Doctors can access only the Vital Signs and Medications of Patients linked to them through appointments.
