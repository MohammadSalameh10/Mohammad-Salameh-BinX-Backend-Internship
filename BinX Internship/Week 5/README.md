# Week 5 — Testing with xUnit, Moq & Integration Testing

## Overview

Week 5 introduced automated testing by applying unit testing and integration testing concepts to the existing **Cardiac Patient Monitoring System API** project.

The first day focused on creating a separate xUnit test project, connecting it to the API project through a project reference, and testing service-layer business logic using `[Fact]`, `[Theory]`, `[InlineData]`, and the Arrange-Act-Assert pattern.

The second day focused on mocking dependencies with Moq, introducing a repository abstraction for vital sign data access, isolating `VitalSignService` from real database dependencies, configuring mocked return values and exceptions, and verifying repository interactions.

The third day focused on integration testing with `WebApplicationFactory`, hosting the API in-memory, using an Entity Framework Core InMemory test database, sending real HTTP requests with `HttpClient`, and testing protected endpoints using JWT authentication.

The fourth day focused on centralized error handling using custom global exception middleware, standardized `ProblemDetails` responses, structured logging with `ILogger`, safe handling of unhandled exceptions, and preventing internal exception details from being exposed to API clients.

The fifth day focused on applying testing practices based on risk and complexity. Three high-risk `VitalSignService` operations were selected for additional unit testing: `CreateAsync`, `UpdateAsync`, and `DeleteAsync`. Both successful and failure paths were tested. The existing integration tests for `GET /api/VitalSigns/{id}` were also reviewed, and the complete test suite was executed using both Visual Studio Test Explorer and `dotnet test`.

The final test suite result was:

```text
Tests:   17
Passed:  17
Failed:  0
Skipped: 0
```

## Daily Work

| Day   | Topic                                                    | Project / Documentation |
| ----- | -------------------------------------------------------- | ----------------------- |
| Day 1 | Unit Testing with xUnit                                  | [View Day 1](./Day%201) |
| Day 2 | Mocking Dependencies with Moq                            | [View Day 2](./Day%202) |
| Day 3 | Integration Testing with WebApplicationFactory           | [View Day 3](./Day%203) |
| Day 4 | Centralized Error Handling & Global Exception Middleware | [View Day 4](./Day%204) |
| Day 5 | Applying Testing to the Cardiac Patient Monitoring System | [View Day 5](./Day%205) |

## Week 5 Highlights

### xUnit Testing

- Created a dedicated xUnit test project.
- Added a project reference to the existing API project.
- Created unit tests for the `VitalSignService`.
- Added `GetHeartRateStatus(int heartRate)` to `IVitalSignService`.
- Implemented heart rate status evaluation logic inside `VitalSignService`.
- Wrote three `[Fact]` unit tests.
- Wrote one `[Theory]` test with three `[InlineData]` cases.
- Extended the test suite with Moq-based dependency tests.
- Added integration tests for real HTTP endpoints.

### Arrange-Act-Assert

- Structured unit tests using the Arrange-Act-Assert pattern.
- Prepared test data during the Arrange phase.
- Executed the service method during the Act phase.
- Verified expected results during the Assert phase.

### Mocking Dependencies with Moq

- Added Moq to the xUnit test project.
- Introduced `IVitalSignRepository` as a repository abstraction.
- Implemented `VitalSignRepository` using `ApplicationDbContext`.
- Updated `VitalSignService` to depend on `IVitalSignRepository`.
- Registered `IVitalSignRepository` and `VitalSignRepository` using Dependency Injection.
- Created `Mock<IVitalSignRepository>` for isolated service testing.
- Configured mocked return values using `Setup` and `ReturnsAsync`.
- Simulated repository failures using `ThrowsAsync`.
- Verified repository interactions using `Verify` and `Times.Once`.
- Tested `VitalSignService.GetByIdAsync` without connecting to the real database.

### Integration Testing with WebApplicationFactory

- Added `Microsoft.AspNetCore.Mvc.Testing` to the test project.
- Created `CustomWebApplicationFactory` using `WebApplicationFactory<Program>`.
- Hosted the ASP.NET Core API in-memory during integration testing.
- Configured a dedicated `Testing` environment.
- Replaced the development SQL Server database configuration with Entity Framework Core InMemory.
- Disabled normal database seeding while running integration tests.
- Created an `HttpClient` using `factory.CreateClient()`.
- Seeded controlled `VitalSign` test data into the InMemory database.
- Generated a valid test JWT containing the `Admin` role.
- Attached JWTs using the Bearer authentication scheme.
- Tested real HTTP requests against `VitalSignsController`.

### Centralized Error Handling

- Implemented custom global exception-handling middleware.
- Registered the middleware early in the ASP.NET Core request pipeline.
- Centralized handling of unexpected exceptions across the API.
- Returned standardized error responses using `ProblemDetails`.
- Configured unhandled exceptions to return `500 Internal Server Error`.
- Prevented exception messages and stack traces from being exposed to API clients.
- Added structured logging using `ILogger`.
- Logged the request path as structured logging context.
- Logged complete exception details and stack traces on the server.
- Created a dedicated endpoint to deliberately trigger an unhandled exception.
- Verified centralized error handling using Postman.
- Reviewed existing controllers for redundant `try/catch` blocks.
- Confirmed that no redundant general-purpose `try/catch` blocks needed to be removed.

### Risk-Based Testing

- Prioritized testing based on business logic, conditional paths, and repository interactions.
- Identified three high-risk `VitalSignService` operations:
  - `CreateAsync`
  - `UpdateAsync`
  - `DeleteAsync`
- Added two unit tests for `CreateAsync`.
- Added two unit tests for `UpdateAsync`.
- Added two unit tests for `DeleteAsync`.
- Tested both successful and failure paths for each operation.
- Verified repository interactions using Moq.
- Verified that database save operations occur only when expected.
- Verified that invalid or missing entities do not trigger unnecessary repository operations.

### Integration Test Review

- Reviewed the existing integration tests for `GET /api/VitalSigns/{id}`.
- Confirmed three existing integration tests were available.
- Verified the successful `200 OK` scenario.
- Verified the `404 Not Found` scenario.
- Verified the `401 Unauthorized` scenario.
- Confirmed that the existing three tests exceeded the Day 5 requirement of at least two integration tests.
- No additional integration tests were required on Day 5.

### Full Test Suite Execution

- Ran the complete test suite using Visual Studio Test Explorer.
- Ran the complete test suite using `dotnet test`.
- Confirmed that all existing and newly added tests passed successfully.
- Final test result:

```text
Tests:   17
Passed:  17
Failed:  0
Skipped: 0
```

- Confirmed that the new Day 5 tests did not introduce regressions.
- Established a risk-based testing approach for the next phase of the project.

### Test Scenarios

The tests covered different heart rate scenarios:

- Low heart rate.
- Normal heart rate.
- High heart rate.

The parameterized `[Theory]` test verified multiple heart rate values using `[InlineData]`.

The Moq-based tests also covered:

- Returning a predefined `VitalSign` from a mocked repository.
- Processing mocked repository data inside `VitalSignService`.
- Simulating an `InvalidOperationException`.
- Verifying that `GetByIdAsync(1)` was called exactly once.

The integration tests covered:

- `200 OK` for an existing `VitalSign` using a valid `Admin` JWT.
- Verification of the complete `VitalSignResponse` body.
- `404 Not Found` for a non-existing `VitalSign`.
- `401 Unauthorized` when the protected endpoint was called without a JWT.

The global exception-handling test also verified:

- `500 Internal Server Error` for a deliberately triggered unhandled exception.
- A standardized `ProblemDetails` response.
- The request path returned through the `instance` property.
- No exception message exposed to the API client.
- No stack trace exposed to the API client.
- Full exception details logged on the server.
- Structured logging of the request path.

The Day 5 unit tests also covered:

- Creating a VitalSign when the patient exists.
- Preventing creation when the patient does not exist.
- Updating an existing VitalSign successfully.
- Returning `false` when the VitalSign does not exist during update.
- Deleting an existing VitalSign successfully.
- Returning `false` when the VitalSign does not exist during deletion.
- Verifying `AddAsync`, `Remove`, and `SaveChangesAsync` repository interactions.
- Verifying that unnecessary database operations are not performed for failure paths.

## Tools Used

- C#
- .NET
- ASP.NET Core Web API
- ASP.NET Core Middleware
- xUnit
- Moq
- `Microsoft.AspNetCore.Mvc.Testing`
- `WebApplicationFactory<Program>`
- `HttpClient`
- Entity Framework Core InMemory
- JWT Authentication
- Bearer Tokens
- Role-Based Authorization
- Global Exception Handling
- `ProblemDetails`
- `ILogger`
- Structured Logging
- `HttpContext`
- `RequestDelegate`
- `[Fact]`
- `[Theory]`
- `[InlineData]`
- Arrange-Act-Assert
- Repository Pattern
- Dependency Injection
- Risk-Based Testing
- Postman
- Visual Studio
- Visual Studio Test Explorer
- Terminal
- `dotnet test`
- Git
- GitHub