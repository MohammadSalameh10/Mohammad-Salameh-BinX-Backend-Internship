# Week 5 — Unit Testing with xUnit

## Overview

Week 5 started the introduction of automated testing by applying unit testing concepts to the existing **Cardiac Patient Monitoring System API** project.

The first day focused on creating a separate xUnit test project, connecting it to the API project through a project reference, and testing service-layer business logic using `[Fact]`, `[Theory]`, `[InlineData]`, and the Arrange-Act-Assert pattern.

The second day focused on mocking dependencies with Moq, introducing a repository abstraction for vital sign data access, isolating `VitalSignService` from real database dependencies, configuring mocked return values and exceptions, and verifying repository interactions.

## Daily Work

| Day | Topic | Project / Documentation |
|---|---|---|
| Day 1 | Unit Testing with xUnit | [View Day 1](./Day%201) |
| Day 2 | Mocking Dependencies with Moq | [View Day 2](./Day%202) |

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

### Test Execution

- Ran unit tests using Visual Studio Test Explorer.
- Added and executed tests progressively.
- Verified the final result after Day 2:

```text
Tests:   8
Passed:  8
Failed:  0
Skipped: 0
```

## Tools Used

- C#
- .NET
- ASP.NET Core Web API
- xUnit
- Moq
- `[Fact]`
- `[Theory]`
- `[InlineData]`
- Arrange-Act-Assert
- Repository Pattern
- Dependency Injection
- Visual Studio
- Visual Studio Test Explorer
- Git
- GitHub