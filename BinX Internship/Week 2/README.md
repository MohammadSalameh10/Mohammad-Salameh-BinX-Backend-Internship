# Week 2 — Advanced C# Concepts

## Overview

Week 2 focuses on advanced C# concepts and building reusable, type-safe application components.

Day 1 covered generics, generic constraints, collection interfaces, predicates, and the implementation of a reusable generic repository.

## Daily Work

| Day | Topic | Project / Documentation |
|---|---|---|
| Day 1 | Generics & Advanced Collections | [View Day 1](./Day%201) |

## Week 2 Highlights

### Generics

- Learned why generics are used in C#.
- Used type parameters to write reusable and type-safe code.
- Applied the `where T : class` generic constraint.
- Used the same generic class with different domain-model types.

### Generic Repository

- Created a reusable `Repository<T>` class.
- Added `Add`, `GetAll`, and `Find` operations.
- Stored items internally using `List<T>`.
- Searched for items using `Predicate<T>` and lambda expressions.
- Used the repository with `Product` and `Customer`.

### Collection Interfaces

- Reviewed `IEnumerable<T>`, `IReadOnlyList<T>`, and `IList<T>`.
- Returned `IReadOnlyList<T>` from `GetAll()`.
- Confirmed that callers cannot add or remove items from the returned collection directly.

## Tools Used

- C#
- .NET
- Visual Studio
- Console Application
- PowerShell
- Git
- GitHub

## Summary

Week 2 began with an introduction to generics and advanced collection interfaces in C#.

A reusable generic repository was created and tested with two different domain-model types. The repository maintains type safety, supports adding and searching for items, and protects its internal collection by returning an `IReadOnlyList<T>`.