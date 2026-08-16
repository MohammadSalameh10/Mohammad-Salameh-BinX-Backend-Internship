# Week 5 — Phase 3 Project & Unit Testing

## Overview

Week 5 started the transition into Phase 3 by selecting the capstone project and introducing automated testing with xUnit.

The selected project is an E-Commerce Backend, and the first day focused on defining a realistic project scope, reviewing the professional baseline required by Week 9, and implementing unit tests using `[Fact]`, `[Theory]`, `[InlineData]`, and the Arrange-Act-Assert pattern.

## Daily Work

| Day | Topic | Project / Documentation |
|---|---|---|
| Day 1 | Choosing the Phase 3 Project & Unit Testing with xUnit | [View Day 1](./Day%201) |

## Week 5 Highlights

### Phase 3 Project Selection

- Selected an E-Commerce Backend as the Phase 3 capstone project.
- Defined the core project scope around product catalog management, shopping cart operations, and order processing.
- Reviewed the professional baseline required for completion by Week 9.

### xUnit Testing

- Created a dedicated xUnit test project.
- Added a project reference to the existing API project.
- Created a simple `OrderCalculator` service with pure calculation logic.
- Wrote three `[Fact]` unit tests.
- Wrote one `[Theory]` test with three `[InlineData]` cases.

### Arrange-Act-Assert

- Structured unit tests using the Arrange-Act-Assert pattern.
- Used clear test naming based on method, scenario, and expected result.
- Tested normal values, zero quantity, and decimal prices.

### Test Execution

- Ran unit tests using Visual Studio Test Explorer.
- Added and executed the tests progressively.
- Verified a final result of 6 passed tests with 0 failures and 0 skipped tests.

## Tools Used

- C#
- .NET
- ASP.NET Core Web API
- xUnit
- Visual Studio
- Visual Studio Test Explorer
- Git
- GitHub