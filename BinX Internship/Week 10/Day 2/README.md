# Week 10 - Day 2: Preparing the Case Study & CV/LinkedIn Update

## Overview

In this day, I prepared a technical case study for the team-based Trip Planning Platform and documented the project from a technical hiring perspective.

The case study focuses on the project problem, scope, system architecture, key technical decisions, engineering challenges, testing evidence, and the current project outcome.

The backend test suite was also executed to collect real project metrics instead of using estimated values.

A CV bullet was prepared using the actual project stack and verified testing results, and a LinkedIn post draft was created for use after the project is fully completed.

The team project is still under active development, so the case study and final LinkedIn post will be updated again when the complete system and final project results are available.

---

## Backend Case Study

### Problem & Project Scope

The project is a team-based **Trip Planning Platform** designed to help users plan complete trips based on their available time, budget, and personal interests.

The platform supports two main scenarios:

- A user who does not yet know where they want to travel can provide information such as budget, trip duration, and interests such as art, culture, nature, adventure, or food. Based on these preferences, the platform can recommend a suitable destination.
- A user who already knows their destination can skip the destination recommendation step and receive a personalized itinerary for the selected location.

The platform goes beyond recommending only a country or city. It generates a structured day-by-day travel plan that suggests places to visit and organizes them in a logical order across the available days.

The generated itinerary is also influenced by the user's interests so that a larger portion of the recommended places matches their preferences.

The project is developed collaboratively by a multidisciplinary team covering Backend, Mobile/Frontend, AI/Planning, Data, Testing, and Security responsibilities.

### Architecture & Key Technical Decisions

The project uses a multi-component architecture where different parts of the system handle different responsibilities.

The Backend is built using **ASP.NET Core**, while a separate **FastAPI planning service** is used as part of the trip-planning workflow.

The backend is responsible for exposing API endpoints, validating requests and responses, coordinating with the planning service, and supporting the integration between the different system components.

The project also includes Mobile/Frontend, Data, Testing, and Security work, with each area contributing to the final integrated platform.

Key technical decisions include:

- Using ASP.NET Core for the main backend API.
- Using FastAPI for the planning-related service.
- Separating backend and planning responsibilities into different services.
- Validating the integration between services through automated testing and staged QA.
- Keeping the project organized as a collaborative team repository with separate responsibilities across multiple technical areas.

### Biggest Engineering Challenge

One of the biggest engineering challenges was integrating the ASP.NET Core backend with the separate FastAPI planning service in a reliable way.

The challenge was not only sending requests between the two services, but also ensuring that both sides agreed on the expected request and response structure.

The team addressed this by:

- Defining and validating the integration contract between the backend and FastAPI.
- Verifying request and response models.
- Rejecting invalid planning responses instead of allowing incorrect data to continue through the system.
- Adding automated tests for the FastAPI planning client.
- Running real integration tests against the planning service.
- Using staged QA and security review before treating the integration as complete.

This helped make the communication between the services more predictable and reduced the risk of invalid planning data reaching the final application.

### Performance / Testing Evidence

The project was validated through multiple layers of testing and quality assurance rather than relying only on manual verification.

The backend work included:

- Automated tests for the FastAPI planning client.
- Real integration tests against the FastAPI planning service.
- Validation tests for planning results.
- Controller-level tests for trip-planning endpoints.
- Middleware tests for planning-related exception handling.
- Staged QA verification for integrated outputs.
- Security review of the API boundaries and integration flow.

The repository history also shows dedicated QA and evaluation work across multiple stages of the project.

The current ASP.NET Core backend exposes **1 controller** with **1 primary backend endpoint**:

`POST /trip-plans/preview`

At this stage, only verified measurements and test results are included in the case study.

The backend automated test suite currently contains **57 tests**, with the latest test run completing with:

- **57 passed**
- **0 failed**
- **0 skipped**

The project build also completed successfully during the test run.

### Final Outcome

The project has progressed into an integrated team platform with a working backend and planning-service integration.

The current outcome includes:

- An ASP.NET Core backend for the main API.
- Integration with a FastAPI planning service.
- Trip-planning flows based on user preferences and destination data.
- Automated tests for important backend and integration scenarios.
- Staged QA and security review across the project.
- Collaborative development across Backend, Mobile/Frontend, AI/Planning, Data, Testing, and Security responsibilities.

The project is still under active team development.

The final case study will be updated after the complete system is finalized so the final deployed state, complete feature set, and any verified performance or testing metrics can be documented accurately.

---

## CV Bullet

Built an ASP.NET Core backend integrated with a FastAPI planning service for a team-based trip planning platform, validating the integration through 57 automated tests with 57 passed, 0 failed, and 0 skipped.

---

## LinkedIn Post Draft

Currently working on a team-based **Trip Planning Platform** that helps users generate personalized travel plans based on their budget, trip duration, interests, and selected destination.

My work focuses on the **ASP.NET Core backend** and its integration with a separate **FastAPI planning service**.

The current backend integration is validated through **57 automated tests**, with the latest test run completing with **57 passed, 0 failed, and 0 skipped**.

The project also includes staged QA, security review, and collaboration across Backend, Mobile/Frontend, AI/Planning, Data, Testing, and Security responsibilities.

GitHub Repository:

`https://github.com/group6-project-team/gr6_project`

The final post will be updated after the complete project is finalized.

---

## Tools Used

- ASP.NET Core
- FastAPI
- xUnit
- .NET CLI
- Visual Studio
- PowerShell
- Git
- GitHub