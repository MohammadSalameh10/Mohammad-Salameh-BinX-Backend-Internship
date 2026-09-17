# Week 9 — Sprint 4: Testing, Documentation & Deployment

## Overview

Week 9 begins Phase 3 Sprint 4 for the **Cardiac Patient Monitoring System API**.

The week started with Sprint 4 planning and a focus on strengthening automated test coverage, completing project documentation, and preparing the application for deployment.

Day 1 focused on auditing integration-test coverage across all 23 API endpoints, identifying happy-path and error-path coverage gaps, and prioritizing the highest-risk routes.

Authentication, registration, patient ownership, and role-based authorization were selected as the highest-priority gaps.

Eight new integration tests were added across the Auths, Appointments, and Patients APIs.

The complete test suite finished with:

```text
25 Tests
25 Passed
0 Failed
0 Skipped
```

The Sprint 3 improvement action to protect the optimized VitalSigns query against future N+1 regressions was also carried forward into the Sprint 4 backlog.

Day 2 focused on finalizing the API documentation using Swagger/OpenAPI and Postman.

XML documentation output was enabled and connected to Swagger so meaningful endpoint summaries, parameter descriptions, and documented response codes could be displayed directly in the Swagger UI.

Realistic request examples were added for login, registration, and appointment creation, and the login endpoint was documented with a realistic JWT response example.

The Postman collection was then reviewed and finalized by organizing the requests, verifying the required endpoints, using collection variables consistently, automatically storing Admin and Patient JWT tokens after login, and adding at least one expected status-code test script to every request.

The existing project README was also reviewed against the professional documentation requirements and confirmed to already contain the required setup, migration, technology stack, API documentation, Postman, testing, and local-run instructions.

Day 3 focused on building and verifying a CI pipeline with GitHub Actions.

A workflow was created to run automatically on `push` and `pull_request`, using `.NET 10` to restore dependencies, build the solution, and run the complete automated test suite.

The pipeline was first verified with a successful run, then one test was deliberately broken to confirm that GitHub Actions correctly marks the workflow as failed. After restoring the original assertion, the pipeline returned to a passing state. A workflow status badge was also added to the Day 3 README.
Day 4 focused on deploying the Cardiac Patient Monitoring System API to Railway and extending the existing GitHub Actions workflow into a complete CI/CD pipeline.

The production environment was configured with a remote SQL Server database, Redis, JWT environment variables, and EF Core migrations. The live API was verified through Postman using the Railway public URL.

After the manual deployment was confirmed, the GitHub Actions workflow was extended with a Railway deployment job that runs only after the build and automated tests succeed on a push to the `main` branch. The automated deployment was verified successfully in both GitHub Actions and Railway.

## Daily Work

| Day   | Topic                                                | Project / Documentation |
| ----- | ---------------------------------------------------- | ----------------------- |
| Day 1 | Sprint 4 Planning & Closing Test Coverage Gaps       | [View Day 1](./Day%201) |
| Day 2 | Finalizing API Documentation (Swagger/OpenAPI & Postman) | [View Day 2](./Day%202) |
| Day 3 | Building the CI/CD Pipeline with GitHub Actions      | [View Day 3](./Day%203) |
| Day 4 | Deploying to Railway & Extending the CI/CD Pipeline | [View Day 4](./Day%204) |

## Week 9 Highlights

### Sprint 4 Planning

- Defined the Sprint 4 goal around automated testing, documentation, and deployment readiness.
- Reviewed the Sprint 4 backlog.
- Carried forward the Sprint 3 N+1 regression-test improvement action.
- Planned to audit integration-test coverage across all API endpoints.
- Prioritized high-risk coverage gaps before lower-risk endpoints.

### Test Coverage Audit

The complete API endpoint list was reviewed to identify current integration-test coverage.

A total of `23` endpoints were audited.

Each endpoint was classified based on whether it had:

- Happy-path coverage
- Error-path coverage
- Both
- Neither

Before the new Day 1 work, the main endpoint with existing integration coverage was:

```http
GET /api/VitalSigns/{id}
```

Its existing integration tests covered:

- `200 OK`
- `404 Not Found`
- `401 Unauthorized`

Service-level unit tests were reviewed separately and were not counted as endpoint-level integration coverage.

### High-Risk Coverage Prioritization

The remaining endpoint coverage gaps were prioritized based on risk rather than treating every route equally.

The highest-priority Day 1 gaps were:

- `POST /api/Auths/login`
- `POST /api/Auths/register`
- `GET /api/Appointments/{id}`
- `GET /api/Patients`

These routes were prioritized because they involve:

- Authentication
- Identity and user creation
- Patient ownership
- Role-based authorization
- Protected access rules

Lower-risk read-only endpoints were left for later Sprint 4 work.

### Authentication Integration Tests

Integration tests were added for the authentication endpoints.

For:

```http
POST /api/Auths/login
```

the following scenarios were covered:

- Valid credentials return `200 OK`.
- A JWT token is returned for successful login.
- Invalid credentials return `401 Unauthorized`.

For:

```http
POST /api/Auths/register
```

the following scenarios were covered:

- A valid registration request returns `201 Created`.
- Registering with an existing email returns `400 Bad Request`.

The `Patient` role was created inside the test environment when needed so the registration flow could run successfully during integration testing.

### Appointment Ownership Integration Tests

Integration tests were added for:

```http
GET /api/Appointments/{id}
```

The following scenarios were covered:

- A Patient can access their own appointment and receives `200 OK`.
- A Patient cannot access another Patient's appointment and receives `404 Not Found`.

These tests verify the patient-ownership rule by comparing the JWT `PatientId` claim with the appointment's `PatientId`.

### Role-Based Authorization Integration Tests

Integration tests were added for:

```http
GET /api/Patients
```

The following scenarios were covered:

- An Admin user can access the endpoint and receives `200 OK`.
- A Patient user is denied access and receives `403 Forbidden`.

These tests confirm that the Admin-only authorization rule is enforced correctly.

### Full Test Suite

After adding the new Day 1 integration tests, the complete automated test suite was executed.

The final result was:

```text
Total Tests: 25
Passed: 25
Failed: 0
Skipped: 0
```

The suite currently includes:

- `14` unit tests for `VitalSignService`
- `11` integration tests across the API

The new Day 1 work added `8` integration tests covering authentication, registration, appointment ownership, and role-based authorization.

### Sprint 3 Improvement Action

The following improvement action was carried forward from Sprint 3:

```text
Add an automated regression test to verify that the optimized VitalSigns query does not return to an N+1 query pattern after future code changes.
```

This action remains part of the Sprint 4 backlog.

It was not completed during Day 1 because the Day 1 implementation focused first on the highest-risk uncovered integration-test gaps involving authentication, registration, ownership, and role-based authorization.

### Swagger XML Documentation

- Enabled XML documentation output in the API project.
- Connected the generated XML documentation file to Swagger using `IncludeXmlComments`.
- Added meaningful XML comments to five important endpoints.
- Documented endpoint summaries, request parameters, and expected response status codes.
- Verified that the XML comments appear correctly in Swagger UI.

### Swagger Request and Response Examples

- Added realistic request examples for:
  - `POST /api/Auths/login`
  - `POST /api/Auths/register`
  - `POST /api/Appointments`
- Replaced default placeholder values with realistic request data.
- Added a documented `200 OK` response model for the login endpoint.
- Added a realistic JWT token example to the login response documentation.

### Postman Collection Finalization

- Reviewed the complete Postman collection.
- Confirmed that the required API endpoints are present.
- Organized requests into logical folders:
  - `Auths`
  - `Patients`
  - `VitalSigns`
  - `Medications`
  - `Appointments`
- Used collection variables consistently:
  - `{{baseUrl}}`
  - `{{adminToken}}`
  - `{{patientToken}}`
- Configured Admin login to automatically store the JWT in `adminToken`.
- Configured Patient login to automatically store the JWT in `patientToken`.
- Added at least one expected status-code test script to every request.
- Exported the finalized Week 9 Day 2 Postman collection.

### Project README Review

- Reviewed the existing project README against the Day 2 documentation requirements.
- Confirmed that the README already includes:
  - Project overview
  - Technology stack
  - Setup requirements
  - Database configuration
  - EF Core migration instructions
  - Swagger documentation
  - Postman collection information
  - Automated testing information
  - Local project run instructions
- No full README rewrite was required because the existing documentation already covered the required setup and project information.

### Sprint 4 Backlog

The current Sprint 4 backlog includes:

| Backlog Item | Status |
| ------------ | ------ |
| Define Sprint 4 goal and backlog | Done |
| Audit integration-test coverage across all 23 API endpoints | Done |
| Identify happy-path and error-path coverage gaps | Done |
| Prioritize authentication, authorization, ownership, and protected-route gaps | Done |
| Add integration tests for `POST /api/Auths/login` | Done |
| Add integration tests for `POST /api/Auths/register` | Done |
| Add ownership tests for `GET /api/Appointments/{id}` | Done |
| Add role-authorization tests for `GET /api/Patients` | Done |
| Run the complete automated test suite | Done |
| Keep all tests passing after the new coverage work | Done |
| Finalize Swagger/OpenAPI documentation with XML comments | Done |
| Add realistic Swagger request and response examples | Done |
| Finalize the Postman collection and add status-code tests | Done |
| Review the existing project README against professional documentation needs | Done |
| Complete remaining Sprint 4 project documentation | Done |
| Prepare the application for deployment | Done |
| Create GitHub Actions CI workflow | Done |
| Trigger the CI pipeline on `push` and `pull_request` | Done |
| Automatically restore, build, and test the project in GitHub Actions | Done |
| Verify that the pipeline fails when a test fails | Done |
| Fix the test and confirm the pipeline returns to passing | Done |
| Add a CI workflow status badge to the Day 3 README | Done |
| Deploy the capstone API to Railway | Done |
| Configure production SQL Server, Redis, and JWT environment variables | Done |
| Apply EF Core migrations to the production database | Done |
| Verify the live API through the public Railway URL | Done |
| Extend GitHub Actions with a Railway deploy job | Done |
| Gate deployment on successful build and automated tests | Done |
| Restrict automated deployment to pushes on `main` | Done |
| Verify the complete automated CI/CD deployment flow | Done |

### GitHub Actions CI Pipeline

- Created a GitHub Actions workflow in `.github/workflows/build-and-test.yml`.
- Configured the workflow to run on:
  - `push`
  - `pull_request`
- Configured the pipeline to use `.NET 10`.
- Automated:
  - `dotnet restore`
  - `dotnet build --no-restore`
  - `dotnet test --no-build`
- Verified that the complete test suite passes successfully in GitHub Actions.
- Deliberately broke one test to confirm that the workflow correctly fails when a test fails.
- Restored the test and confirmed that the workflow returned to a passing state.
- Added a workflow status badge to the Day 3 README.

### Railway Deployment & CI/CD Automation

- Deployed the Cardiac Patient Monitoring System API to Railway.
- Added a `Dockerfile` so Railway could build and run the `.NET 10` API.
- Configured the Railway service root directory for the capstone project.
- Created a production SQL Server database and enabled remote access.
- Configured production environment variables for:
  - `ConnectionStrings__DefaultConnection`
  - `ConnectionStrings__Redis`
  - `Jwt__Issuer`
  - `Jwt__Audience`
  - `Jwt__Key`
- Created and linked a Redis service in Railway.
- Applied EF Core migrations to the production SQL Server database.
- Verified the live API through Postman using the Railway public URL.
- Confirmed `POST /api/Auths/login` returns `200 OK` from the live deployment.
- Extended the GitHub Actions workflow with a Railway deployment job.
- Stored the Railway project token securely in GitHub Secrets as `RAILWAY_TOKEN`.
- Configured deployment to run only after the build and automated tests succeed.
- Restricted automated deployment to pushes on the `main` branch.
- Verified the complete build, test, and Railway deployment flow successfully.

## Tools Used

- C#
- ASP.NET Core Web API
- xUnit
- Moq
- `WebApplicationFactory<Program>`
- `CustomWebApplicationFactory`
- EF Core InMemory
- ASP.NET Core Identity
- `UserManager<IdentityUser>`
- `RoleManager<IdentityRole>`
- JWT Bearer Authentication
- `HttpClient`
- Swashbuckle.AspNetCore
- Swagger / OpenAPI
- XML Documentation Comments
- `GenerateDocumentationFile`
- `IncludeXmlComments`
- `ProducesResponseType`
- Postman Test Scripts
- Postman Collection Variables
- Visual Studio Test Explorer
- GitHub Actions
- Railway
- Docker
- MonsterASP.NET
- SQL Server Production Database
- Redis
- GitHub Secrets
- Railway CLI
- Postman
- Visual Studio
- Git
- GitHub
