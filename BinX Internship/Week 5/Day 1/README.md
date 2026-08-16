# Week 5 — Day 1: Unit Testing with xUnit

## Overview

Day 1 focused on introducing unit testing with xUnit using the existing **Cardiac Patient Monitoring System API** project.

The hands-on work applied unit testing concepts to the service layer by creating a separate xUnit test project and connecting it to the API project through a project reference.

The exercise introduced xUnit unit testing concepts, including `[Fact]`, `[Theory]`, `[InlineData]`, and the Arrange-Act-Assert pattern.

---

## Learning Objectives

The objectives of this exercise were to:

- Understand the purpose of unit testing.
- Create an xUnit test project.
- Reference the existing ASP.NET Core API project from the test project.
- Test service-layer business logic independently.
- Write unit tests using `[Fact]`.
- Write parameterized tests using `[Theory]` and `[InlineData]`.
- Structure unit tests using the Arrange-Act-Assert pattern.
- Run tests using Visual Studio Test Explorer and verify the results.

---

## Unit Testing with xUnit

Unit testing verifies small units of application logic independently from the rest of the application.

For this exercise, **xUnit** was used as the .NET testing framework.

A separate test project was created:

```text
CardiacPatientMonitoringSystem.Tests
```

The test project references the existing API project:

```text
CardiacPatientMonitoringSystem.Tests
        ↓
Project Reference
        ↓
CardiacPatientMonitoringSystem.API
```

This allows the test project to access and test classes from the main application while keeping production code and test code separated.

### xUnit Test Project Setup

![xUnit Test Project Setup](./xunit-project-setup.png)

The test project was created separately from the main API project and the tests were executed through Visual Studio Test Explorer.

---

## xUnit `Fact` and `Theory`

xUnit provides attributes that identify methods as tests.

### `[Fact]`

`[Fact]` is used when a test represents one specific scenario with a fixed set of values.

For example:

```text
Heart Rate = 50
Expected Status = Low
```

A `[Fact]` test runs once and verifies that specific scenario.

### `[Theory]`

`[Theory]` is used when the same test logic should be executed with multiple sets of input data.

Test data can be supplied using `[InlineData]`:

```csharp
[Theory]
[InlineData(40, "Low")]
[InlineData(80, "Normal")]
[InlineData(150, "High")]
```

In this example, xUnit executes the same test method three times, once for each `[InlineData]` set.

This avoids creating multiple nearly identical test methods when only the input values and expected results change.

---

## Arrange-Act-Assert Pattern

The unit tests were organized using the **Arrange-Act-Assert (AAA)** pattern.

```text
Arrange
→ Prepare the object and input data.

Act
→ Execute the method being tested.

Assert
→ Verify that the actual result matches the expected result.
```

For example:

```csharp
[Fact]
public void GetHeartRateStatus_WithLowHeartRate_ReturnsLow()
{
    // Arrange
    var service = new VitalSignService(null!);
    int heartRate = 50;

    // Act
    string result =
        service.GetHeartRateStatus(heartRate);

    // Assert
    Assert.Equal("Low", result);
}
```

Using these three sections keeps each test easy to read and makes it clear what behavior is being verified.

---

## VitalSignService Implementation

The unit tests focus on testing business logic added to the vital sign service.

The interface was updated in:

```text
IVitalSignService.cs
```

with:

```csharp
string GetHeartRateStatus(int heartRate);
```

The implementation was added inside:

```text
VitalSignService.cs
```

```csharp
public string GetHeartRateStatus(int heartRate)
{
    if (heartRate < 60)
        return "Low";

    if (heartRate > 100)
        return "High";

    return "Normal";
}
```

The `GetHeartRateStatus` method receives:

```text
heartRate
→ The patient's measured heart rate value.
```

It evaluates the heart rate using the following rules:

```text
Below 60
→ Low

60 - 100
→ Normal

Above 100
→ High
```

The method is suitable for unit testing because its result depends only on the input value and it does not interact with the database, Entity Framework Core, HTTP requests, or external services.

---

## Fact Tests

Three `[Fact]` tests were created for `GetHeartRateStatus`.

Each test covers one specific scenario and follows the Arrange-Act-Assert pattern.

---

### Low Heart Rate

The first test verifies that a heart rate below 60 returns the expected status:

```csharp
[Fact]
public void GetHeartRateStatus_WithLowHeartRate_ReturnsLow()
{
    // Arrange
    var service = new VitalSignService(null!);
    int heartRate = 50;

    // Act
    string result =
        service.GetHeartRateStatus(heartRate);

    // Assert
    Assert.Equal("Low", result);
}
```

The expected result is:

```text
50 → Low
```

![First Fact Test Passed](./fact-test-1-passed.png)

---

### Normal Heart Rate

The second test verifies that a normal heart rate returns the expected status:

```csharp
[Fact]
public void GetHeartRateStatus_WithNormalHeartRate_ReturnsNormal()
{
    // Arrange
    var service = new VitalSignService(null!);
    int heartRate = 75;

    // Act
    string result =
        service.GetHeartRateStatus(heartRate);

    // Assert
    Assert.Equal("Normal", result);
}
```

The expected result is:

```text
75 → Normal
```

![Two Fact Tests Passed](./fact-tests-2-passed.png)

---

### High Heart Rate

The third test verifies that a heart rate above 100 returns the expected status:

```csharp
[Fact]
public void GetHeartRateStatus_WithHighHeartRate_ReturnsHigh()
{
    // Arrange
    var service = new VitalSignService(null!);
    int heartRate = 120;

    // Act
    string result =
        service.GetHeartRateStatus(heartRate);

    // Assert
    Assert.Equal("High", result);
}
```

The expected result is:

```text
120 → High
```

![Three Fact Tests Passed](./fact-tests-3-passed.png)

---

## Theory Test

A `[Theory]` test was added to verify the same `GetHeartRateStatus` logic using multiple sets of input values.

```csharp
[Theory]
[InlineData(40, "Low")]
[InlineData(80, "Normal")]
[InlineData(150, "High")]
public void GetHeartRateStatus_WithDifferentInputs_ReturnsCorrectStatus(
    int heartRate,
    string expectedStatus)
{
    // Arrange
    var service = new VitalSignService(null!);

    // Act
    string result =
        service.GetHeartRateStatus(heartRate);

    // Assert
    Assert.Equal(expectedStatus, result);
}
```

Each `[InlineData]` attribute supplies a different set of values to the same test method:

```text
40 → Expected Low

80 → Expected Normal

150 → Expected High
```

Instead of creating three separate test methods with nearly identical logic, `[Theory]` allows the same test to be executed repeatedly with different inputs.

This makes parameterized tests useful when several related input cases need to verify the same behavior.

---

## Fact vs Theory

The exercise demonstrated the main difference between `[Fact]` and `[Theory]`:

```text
[Fact]
→ Tests one specific scenario.
→ Uses fixed values inside the test.
→ Runs once.


[Theory]
→ Tests the same behavior with multiple input sets.
→ Receives test data through parameters.
→ Runs once for each supplied data set.
```

`[InlineData]` provides the individual input values used by the `[Theory]`.

---

## Running Tests with Test Explorer

The unit tests were executed using Visual Studio Test Explorer.

To run the tests:

1. Open **Test Explorer** from Visual Studio.
2. Build the solution if needed.
3. Select the test project or individual tests.
4. Click **Run All Tests**.
5. Review the results in Test Explorer.

Test Explorer displays the total number of tests together with the number of passed, failed, and skipped tests.

During the exercise, the tests were added and executed progressively:

```text
First Fact Test
→ 1 Passed


Second Fact Test
→ 2 Passed


Third Fact Test
→ 3 Passed


Theory with 3 InlineData cases
→ Total 6 Passed
```

This made it possible to verify each stage of the implementation before moving to the next test.

---

## Test Results

All unit tests were executed using Visual Studio Test Explorer.

The final result was:

```text
Tests:   6
Passed:  6
Failed:  0
Skipped: 0
```

Although the test class contains three `[Fact]` methods and one `[Theory]` method, the `[Theory]` contains three `[InlineData]` cases.

Therefore, xUnit executed:

```text
3 Fact tests
+
3 Theory data cases
=
6 executed tests
```

All six test executions passed successfully.

### Unit Test Result

![All Unit Tests Passed](./unit-tests-6-passed.png)

---

## Hands-On Lab Completed

The following tasks were completed:

1. Created an xUnit test project.
2. Added a project reference from the test project to the existing API project.
3. Updated `IVitalSignService` by adding `GetHeartRateStatus(int heartRate)`.
4. Implemented `GetHeartRateStatus` inside `VitalSignService`.
5. Created three `[Fact]` unit tests.
6. Organized the tests using the Arrange-Act-Assert pattern.
7. Created one `[Theory]` test.
8. Added three test cases using `[InlineData]`.
9. Executed the tests using Visual Studio Test Explorer.
10. Confirmed that all six test executions passed successfully.

---

## Project Changes

The main files involved in this exercise were:

```text
CardiacPatientMonitoringSystem.API
└── Services
    ├── Interfaces
    │   └── IVitalSignService.cs
    │
    └── Classes
        └── VitalSignService.cs


CardiacPatientMonitoringSystem.Tests
└── VitalSignServiceTests.cs
```

The solution now contains both the application project and a dedicated unit test project:

```text
CardiacPatientMonitoringSystem.API
→ Application code


CardiacPatientMonitoringSystem.Tests
→ Unit test code
→ References CardiacPatientMonitoringSystem.API
```

This separation allows application logic to be tested independently while keeping test code outside the production API project.

---

## Tools Used

- C#
- .NET
- ASP.NET Core Web API
- xUnit
- `[Fact]`
- `[Theory]`
- `[InlineData]`
- Arrange-Act-Assert
- Visual Studio
- Visual Studio Test Explorer