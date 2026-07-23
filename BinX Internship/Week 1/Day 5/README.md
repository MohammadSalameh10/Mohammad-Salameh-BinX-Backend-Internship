# Day 5 — Git & GitHub Workflow; Week 1 Synthesis

## Overview

Day 5 focused on practicing a professional Git and GitHub workflow and completing the Week 1 close-out.

I created a dedicated feature branch, prepared the Week 1 documentation, and followed the Git workflow used to stage, commit, and push changes.

The feature branch will be submitted through a pull request into the `main` branch for review.

## Learning Objectives

- Use Git commands to track and manage project changes.
- Understand the Git staging and commit workflow.
- Work on a dedicated feature branch instead of committing directly to `main`.
- Write clear and specific commit messages.
- Push a feature branch to GitHub.
- Open a pull request from a feature branch into `main`.
- Write a clear pull request description.
- Request a mentor as a reviewer.
- Prepare a Week 1 summary in Notion.

## Git Fundamentals

Git stores project changes as a sequence of commits.

Each commit represents a snapshot of the staged changes and includes a message explaining what changed.

The main Git workflow is:

```text
Modify → Stage → Commit → Push
```

### Stage Changes

The `git add` command moves selected changes into the staging area:

```bash
git add <file>
```

Only the files that should be included in the next commit should be staged.

### Create a Commit

The `git commit` command saves the staged changes:

```bash
git commit -m "Add Day 5 Git workflow documentation"
```

A commit message should clearly describe the change.

### Push Changes

The `git push` command uploads local commits to GitHub:

```bash
git push
```

## GitHub Remote

I confirmed that the local repository is connected to GitHub using:

```bash
git remote -v
```

The configured remote is named:

```text
origin
```

It points to the BinX Backend Internship GitHub repository for both fetching and pushing changes.

## Feature-Branch Workflow

Instead of making Day 5 changes directly on `main`, I created a dedicated feature branch:

```bash
git checkout -b feature/week1-close-out
```

The branch name describes the work being completed:

```text
feature/week1-close-out
```

I confirmed the current branch using:

```bash
git branch --show-current
```

The result was:

```text
feature/week1-close-out
```

This workflow keeps the `main` branch stable while work is completed and reviewed separately.

## Writing Good Commit Messages

A good commit message should:

- Clearly describe the change.
- Be specific.
- Use the imperative mood.
- Help readers understand the project history.
- Avoid unclear messages such as `fix stuff` or `changes`.

Examples of clear commit messages include:

```text
Add Day 5 Git workflow documentation
```

```text
Add Week 1 internship summary
```

```text
Update README with Week 1 close-out
```

## Opening a Pull Request

A pull request combines the commits from a feature branch into a reviewable unit.

The pull request for this task will be opened:

```text
From: feature/week1-close-out
Into: main
```

The pull request description should explain:

- What was added.
- Why the changes were made.
- Which Week 1 topics were completed.
- Whether there are any open questions.

The mentor will be requested as a reviewer before the pull request is merged.

## Pull Request Description

The following description can be used for the pull request:

```text
## Summary

This pull request completes the Week 1 close-out for the BinX Backend Internship.

## Work Completed

- Completed the Day 2 C# fundamentals exercise.
- Completed the Day 3 object-oriented programming exercise.
- Completed the Day 4 collections, LINQ, async/await, and exception-handling exercise.
- Added Day 5 Git and GitHub workflow documentation.
- Prepared the Week 1 learning summary in Notion.

## Workflow Practiced

- Created a dedicated feature branch.
- Wrote a clear commit message.
- Pushed the branch to GitHub.
- Opened a pull request into main.
- Requested a mentor review.
```

## Week 1 Synthesis

During Week 1, I completed the following work:

### Day 1

- Prepared the .NET development environment.
- Configured Visual Studio and Visual Studio Code.
- Created and ran the first C# console application.
- Created the internship GitHub repository.

### Day 2

- Practiced value types and reference types.
- Used variables, type inference, and naming conventions.
- Applied control flow using conditions, switch expressions, and loops.
- Handled nullable reference types safely.

### Day 3

- Practiced object-oriented programming.
- Created classes, properties, constructors, and methods.
- Applied encapsulation.
- Used records, interfaces, and polymorphism.
- Created a small order-management domain model.

### Day 4

- Used `List<T>`, `Dictionary<TKey, TValue>`, and `HashSet<T>`.
- Queried collections using LINQ.
- Applied filtering, projection, ordering, and aggregation.
- Used LINQ Method Syntax and Query Syntax.
- Created an asynchronous method using `async`, `await`, and `Task.Delay()`.
- Handled specific exceptions using `try`, `catch`, and `finally`.

### Day 5

- Practiced Git fundamentals.
- Created a feature branch.
- Prepared clear commit messages.
- Pushed the feature branch to GitHub.
- Prepared a pull request for review.
- Completed the Week 1 summary.

## Commands Used

```bash
git status
git remote -v
git checkout -b feature/week1-close-out
git branch --show-current
git add <file>
git commit -m "<clear commit message>"
git push -u origin feature/week1-close-out
```

## Tools Used

- Git
- GitHub
- Notion
- PowerShell
- Visual Studio

## Summary

On Day 5, I practiced the Git and GitHub workflow that will be used during future internship sprints.

I confirmed the GitHub remote connection, created the `feature/week1-close-out` branch, and prepared the Week 1 close-out documentation.

I also reviewed how to stage changes, create clear commits, push a feature branch, open a pull request, and request a mentor review.

This workflow keeps the `main` branch stable and allows changes to be reviewed before they are merged.
