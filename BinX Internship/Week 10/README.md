# Week 10 — Final Project Preparation & Presentation

## Overview

Week 10 focuses on final project preparation, repository polish, portfolio readiness, technical case-study writing, and preparing the final project presentation.

Day 1 focused on reviewing and cleaning the team project repository, checking the commit history, pinning the team repository on GitHub, and preparing an outline for the final project presentation.

The repository was reviewed for leftover debug code, TODO and FIXME comments, commented-out experiments, old local branches, and uncommitted changes.

The commit history was also reviewed to confirm that it provides a clear and professional record of the team's development work.

The team project repository was pinned to my GitHub profile, and a presentation deck outline was prepared for the final project discussion after the project is fully completed.

Day 2 focused on preparing a technical case study for the team-based Trip Planning Platform and writing about the project for a technical hiring audience.

The case study documented the project problem, scope, architecture, technical decisions, biggest engineering challenge, testing evidence, and current outcome.

The backend automated test suite was executed to collect real project metrics, confirming 57 tests passed with 0 failed and 0 skipped.

A CV bullet was also prepared using the actual project stack and verified testing results, and a LinkedIn post draft was created for use after the project is fully completed.

## Daily Work

| Day   | Topic                                                     | Documentation           |
| ----- | --------------------------------------------------------- | ----------------------- |
| Day 1 | Finalizing the GitHub Repository & Portfolio Presentation | [View Day 1](./Day%201) |
| Day 2 | Preparing the Case Study & CV/LinkedIn Update             | [View Day 2](./Day%202) |

## Week 10 Highlights

### Repository Polish

- Reviewed the team repository for leftover debug code.
- Confirmed there were no `Console.WriteLine` statements.
- Confirmed there were no `TODO` comments.
- Confirmed there were no `FIXME` comments.
- Reviewed comments for abandoned experimental code.
- Removed merged local branches that were no longer needed.
- Kept unmerged remote branches unchanged because they may still be used by other team members.
- Confirmed the working tree was clean.

### Commit History Review

- Reviewed the complete Git commit history.
- Confirmed that the history provides a clear record of team development work.
- Verified that commit messages are generally descriptive and professional.
- Reviewed the use of prefixes such as `feat:`, `fix:`, and `docs:`.
- Kept the existing collaborative commit history unchanged.

### GitHub Portfolio Preparation

- Pinned the team project repository to my GitHub profile.
- Reviewed the existing GitHub profile README.
- Kept the current profile README unchanged.

### Presentation Preparation

- Prepared the structure of the final presentation deck.
- Planned sections for the project problem, architecture, technical decisions, biggest challenge, performance work, testing, final outcome, and conclusion.
- Decided to build the full presentation after the team project is fully completed.

### Technical Case Study

- Prepared a technical case study for the team-based Trip Planning Platform.
- Documented the project problem and scope.
- Documented the ASP.NET Core and FastAPI architecture.
- Described the main technical decisions behind the backend and planning-service integration.
- Documented the backend-to-FastAPI integration as a major engineering challenge.
- Documented automated testing, staged QA, and security-review evidence.
- Kept the case study limited to verified project information and measurements.

### Testing Metrics

- Executed the backend automated test suite using `dotnet test`.
- Confirmed a total of `57` automated tests.
- Confirmed `57` tests passed.
- Confirmed `0` tests failed.
- Confirmed `0` tests were skipped.
- Confirmed the project build completed successfully during the test run.

### CV & LinkedIn Preparation

- Prepared a CV bullet using the actual project stack and measurable testing results.
- Included ASP.NET Core and FastAPI integration in the CV description.
- Used the verified 57-test result as a concrete metric.
- Prepared a LinkedIn post draft for the team project.
- Deferred publishing the final project announcement until the project is fully completed.

## Tools Used

- GitHub
- Git
- Visual Studio
- PowerShell
- ASP.NET Core
- FastAPI
- xUnit
- .NET CLI