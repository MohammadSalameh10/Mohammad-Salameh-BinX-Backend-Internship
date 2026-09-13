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

## Daily Work

| Day   | Topic                                               | Project / Documentation |
| ----- | --------------------------------------------------- | ----------------------- |
| Day 1 | Sprint 4 Planning & Closing Test Coverage Gaps      | [View Day 1](./Day%201) |

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
| Add automated N+1 regression protection for the optimized VitalSigns query | Pending |
| Continue closing remaining high-value endpoint coverage gaps | Pending |
| Complete Sprint 4 project documentation | Pending |
| Prepare the application for deployment | Pending |

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
- Visual Studio Test Explorer
- Postman
- Visual Studio
- Git
- GitHub
