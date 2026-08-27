# Cardiac Patient Monitoring System API

## Overview

The Cardiac Patient Monitoring System API is a standalone ASP.NET Core REST API developed as an individual backend project.

The system provides backend functionality for managing cardiac patients, vital-sign measurements, medications, and appointments.

The project demonstrates ASP.NET Core Web API development, Entity Framework Core with SQL Server, asynchronous CRUD operations, LINQ, ASP.NET Core Identity, JWT authentication, role-based authorization, input validation, filtering/search, repository abstraction, centralized exception handling, unit testing with xUnit and Moq, Swagger, and Postman.

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
- Appointment filtering by reason.
- Repository abstraction for patient, vital-sign, medication, and appointment data access.
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

Stores patient profile information including:

- Full name
- Date of birth
- Gender
- Phone number
- Blood type

Each patient profile is linked to an ASP.NET Core Identity user.

### Vital Signs

Stores cardiac-related measurements including:

- Heart rate
- Systolic blood pressure
- Diastolic blood pressure
- Oxygen saturation
- Recorded date and time

### Medications

Stores medication information including:

- Name
- Dosage
- Frequency
- Start date
- Optional end date

### Appointments

Stores appointment information including:

- Appointment date
- Reason
- Optional notes

---

## Database

The application uses SQL Server with Entity Framework Core.

The main application entities are:

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

Each patient is linked to one Identity user.

A patient can have multiple:

- Vital signs
- Medications
- Appointments

The database schema is created and updated using Entity Framework Core migrations.

---

## Repository Pattern

Repository abstractions are used for the main CRUD modules to separate service-layer business logic from direct Entity Framework Core data access.

The implemented repositories are:

```text
IPatientRepository
PatientRepository

IVitalSignRepository
VitalSignRepository

IMedicationRepository
MedicationRepository

IAppointmentRepository
AppointmentRepository
```

The general dependency flow is:

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

The following services depend on repository interfaces instead of accessing `ApplicationDbContext` directly:

```text
PatientService
→ IPatientRepository

VitalSignService
→ IVitalSignRepository

MedicationService
→ IMedicationRepository

AppointmentService
→ IAppointmentRepository
```

This separation improves maintainability and allows the service layer to be tested in isolation using mocked repository dependencies.

`AuthService` does not use a custom repository because it works through ASP.NET Core Identity abstractions such as `UserManager<IdentityUser>`.

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

Open Visual Studio and then open:

```text
Tools
→ NuGet Package Manager
→ Package Manager Console
```

Make sure the default project is:

```text
CardiacPatientMonitoringSystem.API
```

Then run:

```powershell
Update-Database
```

This creates the SQL Server database and applies all Entity Framework Core migrations.

---

## Seed Data

The application automatically adds synthetic development data when the project starts.

The seed process creates:

- `Admin` role
- `Patient` role
- Admin Identity user
- Patient Identity user
- Test patient profile
- Vital-sign records
- Medication record
- Appointment record

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

The seeded records use synthetic data only.

---

## Authentication

The API uses ASP.NET Core Identity for user management and password handling.

JWT Bearer tokens are used to authenticate protected API requests.

### Register

```http
POST /api/Auths/register
```

Example request:

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

Example:

```json
{
  "email": "admin@cardiac.com",
  "password": "Admin@123"
}
```

Successful login returns a JWT token:

```json
{
  "token": "..."
}
```

The token must be sent in protected requests using:

```http
Authorization: Bearer <token>
```

---

## Authorization

The API uses two roles:

```text
Admin
Patient
```

### Admin Permissions

The Admin role can:

- View all patients.
- View a patient by ID.
- Update patients.
- Delete patients.
- View all vital signs.
- View a vital sign by ID.
- Update vital signs.
- Delete vital signs.
- View medications.
- Search medications.
- Update medications.
- Delete medications.
- View appointments.
- Search appointments.
- Update appointments.
- Delete appointments.

### Patient Permissions

The Patient role can:

- Create a patient profile.
- Create vital-sign records.
- Create medication records.
- Create appointment records.

A Patient must create a patient profile before creating related records.

---

## API Endpoints

### Authentication

| Method | Endpoint              | Authorization | Description                 |
| ------ | --------------------- | ------------- | --------------------------- |
| POST   | `/api/Auths/register` | Public        | Register a Patient account  |
| POST   | `/api/Auths/login`    | Public        | Login and receive JWT token |

### Patients

| Method | Endpoint             | Authorization | Description            |
| ------ | -------------------- | ------------- | ---------------------- |
| GET    | `/api/Patients`      | Admin         | Get all patients       |
| GET    | `/api/Patients/{id}` | Admin         | Get patient by ID      |
| POST   | `/api/Patients`      | Patient       | Create patient profile |
| PUT    | `/api/Patients/{id}` | Admin         | Update patient         |
| DELETE | `/api/Patients/{id}` | Admin         | Delete patient         |

### Vital Signs

| Method | Endpoint               | Authorization | Description          |
| ------ | ---------------------- | ------------- | -------------------- |
| GET    | `/api/VitalSigns`      | Admin         | Get all vital signs  |
| GET    | `/api/VitalSigns/{id}` | Admin         | Get vital sign by ID |
| POST   | `/api/VitalSigns`      | Patient       | Create vital sign    |
| PUT    | `/api/VitalSigns/{id}` | Admin         | Update vital sign    |
| DELETE | `/api/VitalSigns/{id}` | Admin         | Delete vital sign    |

### Medications

| Method | Endpoint                       | Authorization | Description                |
| ------ | ------------------------------ | ------------- | -------------------------- |
| GET    | `/api/Medications`             | Admin         | Get all medications        |
| GET    | `/api/Medications/{id}`        | Admin         | Get medication by ID       |
| GET    | `/api/Medications?name={name}` | Admin         | Filter medications by name |
| POST   | `/api/Medications`             | Patient       | Create medication          |
| PUT    | `/api/Medications/{id}`        | Admin         | Update medication          |
| DELETE | `/api/Medications/{id}`        | Admin         | Delete medication          |

### Appointments

| Method | Endpoint                            | Authorization | Description                   |
| ------ | ----------------------------------- | ------------- | ----------------------------- |
| GET    | `/api/Appointments`                 | Admin         | Get all appointments          |
| GET    | `/api/Appointments/{id}`            | Admin         | Get appointment by ID         |
| GET    | `/api/Appointments?reason={reason}` | Admin         | Filter appointments by reason |
| POST   | `/api/Appointments`                 | Patient       | Create appointment            |
| PUT    | `/api/Appointments/{id}`            | Admin         | Update appointment            |
| DELETE | `/api/Appointments/{id}`            | Admin         | Delete appointment            |

---

## Filtering and Search

### Medication Name Filter

```http
GET /api/Medications?name=Aspirin
```

Returns medications whose names contain the provided value.

If no matching medications are found, the API returns:

```http
200 OK
```

with:

```json
[]
```

### Appointment Reason Filter

```http
GET /api/Appointments?reason=Routine
```

Returns appointments whose reason contains the provided value.

If no matching appointments are found, the API returns:

```http
200 OK
```

with:

```json
[]
```

---

## Validation

Request validation is implemented using FluentValidation.

Validation is applied to:

- Patient create/update requests.
- Vital-sign create/update requests.
- Medication create/update requests.
- Appointment create/update requests.
- Registration requests.
- Login requests.

Examples of validation rules include:

- Required fields cannot be empty.
- Email must be valid.
- Date of birth must be in the past.
- Blood type must be valid.
- Oxygen saturation must be between `0` and `100`.
- Vital-sign recording time cannot be in the future.
- Medication end date cannot be before its start date.
- New appointments must use a future appointment date.

Invalid input returns:

```http
400 Bad Request
```

with structured validation errors.

---

## Centralized Exception Handling

Unexpected exceptions are handled centrally using `ExceptionHandlingMiddleware`.

The middleware:

- Catches unhandled exceptions from the request pipeline.
- Logs exception details using `ILogger<ExceptionHandlingMiddleware>`.
- Uses structured logging with the request path.
- Returns `500 Internal Server Error`.
- Returns a standardized `ProblemDetails` response.
- Does not expose exception messages, stack traces, or internal implementation details to the client.

Example response:

```json
{
  "title": "An unexpected error occurred.",
  "status": 500
}
```

---

## Testing

The project includes unit tests and integration tests using xUnit and Moq.

### Service Unit Tests

The service layer is tested in isolation using mocked repository dependencies.

The following services are covered:

```text
PatientService
VitalSignService
MedicationService
AppointmentService
```

The tests cover important success and failure paths, including:

```text
CreateAsync
├── Success
└── Failure

UpdateAsync
├── Success
└── Failure

DeleteAsync
├── Success
└── Failure
```

`VitalSignService` also includes tests for:

```text
GetHeartRateStatus
├── Low
├── Normal
└── High

GetByIdAsync
├── Success
└── Exception propagation
```

The tests use Moq verification to confirm expected repository interactions such as:

```text
GetByIdAsync
GetPatientByUserIdAsync
AddAsync
Remove
SaveChangesAsync
```

and to verify that write operations are not performed during failure paths.

### Controller Unit Tests

Controller behavior is tested by mocking the service layer.

The following controllers are covered:

```text
PatientsController
VitalSignsController
MedicationsController
AppointmentsController
AuthsController
```

The tests verify important HTTP responses including:

```text
200 OK
201 Created
204 No Content
400 Bad Request
401 Unauthorized
404 Not Found
```

Controller tests also verify that:

- Authenticated user IDs are read correctly from claims.
- Service methods are called when expected.
- Service methods are not called when authentication information is missing.
- Correct response types are returned for success and failure cases.

### Integration Testing

Integration tests are implemented using:

```text
WebApplicationFactory<Program>
Entity Framework Core InMemory Database
HttpClient
JWT Authentication
```

`CustomWebApplicationFactory` starts the API in a `Testing` environment and replaces the SQL Server database with an EF Core InMemory database.

The integration tests currently verify:

```text
GET /api/VitalSigns/{id}

Existing VitalSign
→ 200 OK

Missing VitalSign
→ 404 Not Found

Missing JWT Token
→ 401 Unauthorized
```

The production seed process is skipped while the application is running in the `Testing` environment.

### Test Result

The full test suite was executed successfully:

```text
Total: 75
Passed: 75
Failed: 0
Skipped: 0
```

---

## HTTP Status Codes

The API uses appropriate HTTP response status codes, including:

| Status Code                 | Meaning                                            |
| --------------------------- | -------------------------------------------------- |
| `200 OK`                    | Successful GET or update operation                 |
| `201 Created`               | Resource created successfully                      |
| `204 No Content`            | Resource deleted successfully                      |
| `400 Bad Request`           | Invalid request or invalid application state       |
| `401 Unauthorized`          | Authentication is required                         |
| `403 Forbidden`             | Authenticated user does not have the required role |
| `404 Not Found`             | Requested resource does not exist                  |
| `500 Internal Server Error` | Unexpected server error                            |

---

## Swagger

Swagger/OpenAPI is available while running the application in the Development environment.

Start the API and open the Swagger page from the URL configured by the application launch profile.

Swagger can be used to inspect the API endpoints and request/response models.

---

## Postman Collection

A Postman collection is included in:

```text
postman/Cardiac Patient Monitoring System API.postman_collection.json
```

The collection contains requests for:

- Authentication
- Patients
- Vital signs
- Medications
- Appointments
- Medication filtering
- Appointment filtering

The collection uses the following variables:

```text
baseUrl
adminToken
patientToken
```

After logging in, copy the returned JWT token into either:

```text
adminToken
```

or:

```text
patientToken
```

depending on the account role.

---

## Demo

A compressed recorded API demonstration is included in:

```text
demo/Cardiac API Demo.zip
```

The demo shows:

- API testing using Postman.
- JWT authentication.
- Admin and Patient role authorization.
- `401 Unauthorized` and `403 Forbidden` scenarios.
- Patient registration and patient-profile creation.
- Duplicate patient-profile prevention.
- Vital-sign creation and request validation.
- Medication and appointment filtering.
- CRUD operations.
- Database evidence showing that API operations are reflected in SQL Server.

---

## Running the Project

### 1. Open the Project

Navigate to:

```text
BinX Project/Cardiac Patient Monitoring System
```

Then open:

```text
Cardiac Patient Monitoring System.slnx
```

using Visual Studio.

### 2. Configure SQL Server

Update the `DefaultConnection` connection string in:

```text
Cardiac Patient Monitoring System/CardiacPatientMonitoringSystem.API/appsettings.json
```

if necessary.

### 3. Create the Database

Open Visual Studio and go to:

```text
Tools
→ NuGet Package Manager
→ Package Manager Console
```

Make sure the default project is:

```text
CardiacPatientMonitoringSystem.API
```

Then run:

```powershell
Update-Database
```

This creates the SQL Server database and applies all Entity Framework Core migrations.

### 4. Run the Application

Run:

```text
CardiacPatientMonitoringSystem.API
```

from Visual Studio.

The synthetic seed data is added automatically when the application starts.

### 5. Test the API

The API can be tested independently using:

- Swagger
- Postman

Import:

```text
postman/Cardiac Patient Monitoring System API.postman_collection.json
```

into Postman.

Set the collection variable:

```text
baseUrl
```

to the URL used by the running API.

Then log in and store the returned JWT token in:

```text
adminToken
```

or:

```text
patientToken
```

depending on the account role.

### 6. Run Tests

Open Test Explorer in Visual Studio and run the tests in:

```text
CardiacPatientMonitoringSystem.Tests
```

The test project includes service unit tests, controller unit tests, and integration tests.

The current test suite contains:

```text
Total: 75
Passed: 75
Failed: 0
Skipped: 0
```

---

## Verification

The following scenarios were manually verified using Postman:

- Admin login.
- Patient registration and login.
- JWT authentication.
- Admin and Patient role authorization.
- `401 Unauthorized` responses.
- `403 Forbidden` responses.
- Patient CRUD.
- Vital-sign CRUD.
- Medication CRUD.
- Appointment CRUD.
- Request validation failures.
- Duplicate patient-profile prevention.
- Missing patient-profile handling.
- `404 Not Found` responses.
- Medication filtering.
- Appointment filtering.
- Empty filtering results returning `200 OK` with `[]`.
- Database recreation using Entity Framework Core migrations.
- Synthetic seed data creation.
- Centralized exception handling.
- `500 Internal Server Error` with a standardized `ProblemDetails` response.
- Service unit tests using xUnit and Moq.
- Controller unit tests using mocked service dependencies.
- Integration tests using WebApplicationFactory and an EF Core InMemory database.
- Full test suite completed successfully with 75 passed tests and 0 failures.

---

## Sample Seed Data

After creating a fresh database and running the application, the seeded application data includes:

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
- Delete requests in the Postman collection use non-seeded IDs by default to avoid accidentally deleting the provided seed data.
- The recorded API demo is included in the `demo` folder.