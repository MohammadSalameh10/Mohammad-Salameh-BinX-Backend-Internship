# Week 5 — Unit Testing with xUnit

## Overview

Week 5 started the introduction of automated testing by applying unit testing concepts to the existing **Cardiac Patient Monitoring System API** project.

The first day focused on creating a separate xUnit test project, connecting it to the API project through a project reference, and testing service-layer business logic using `[Fact]`, `[Theory]`, `[InlineData]`, and the Arrange-Act-Assert pattern.

## Daily Work

| Day | Topic | Project / Documentation |
|---|---|---|
| Day 1 | Unit Testing with xUnit | [View Day 1](./Day%201) |

## Week 5 Highlights

### xUnit Testing

- Created a dedicated xUnit test project.
- Added a project reference to the existing API project.
- Created unit tests for the `VitalSignService`.
- Added `GetHeartRateStatus(int heartRate)` to `IVitalSignService`.
- Implemented heart rate status evaluation logic inside `VitalSignService`.
- Wrote three `[Fact]` unit tests.
- Wrote one `[Theory]` test with three `[InlineData]` cases.

### Arrange-Act-Assert

- Structured unit tests using the Arrange-Act-Assert pattern.
- Prepared test data during the Arrange phase.
- Executed the service method during the Act phase.
- Verified expected results during the Assert phase.

### Test Scenarios

The tests covered different heart rate scenarios:

- Low heart rate.
- Normal heart rate.
- High heart rate.

The parameterized `[Theory]` test verified multiple heart rate values using `[InlineData]`.

### Test Execution

- Ran unit tests using Visual Studio Test Explorer.
- Added and executed tests progressively.
- Verified the final result:

```text
Tests:   6
Passed:  6
Failed:  0
Skipped: 0
```

## Tools Used

- C#
- .NET
- ASP.NET Core Web API
- xUnit
- Visual Studio
- Visual Studio Test Explorer
- Git
- GitHub