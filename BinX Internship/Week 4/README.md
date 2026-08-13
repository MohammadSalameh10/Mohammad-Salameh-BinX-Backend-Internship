# Week 4 — Authentication & Security

## Overview

Week 4 focused on securing the existing Task Tracker API using authentication, authorization, input validation, and API security hardening techniques in ASP.NET Core.

The week extended the Week 3 project with ASP.NET Core Identity, JWT authentication, role and policy-based authorization, FluentValidation, rate limiting, CORS, HTTPS redirection, HSTS, and SQL injection prevention practices.

## Daily Work

| Day | Topic | Project / Documentation |
|---|---|---|
| Day 1 | ASP.NET Core Identity & User Registration | [View Day 1](./Day%201) |
| Day 2 | JWT Authentication & Token Issuance | [View Day 2](./Day%202) |
| Day 3 | Protecting Routes, Roles & Policy-Based Authorization | [View Day 3](./Day%203) |
| Day 4 | Input Validation with FluentValidation | [View Day 4](./Day%204) |
| Day 5 | Rate Limiting, CORS & Security Hardening | [View Day 5](./Day%205) |

## Week 4 Highlights

### Identity & Registration

- Integrated ASP.NET Core Identity with Entity Framework Core.
- Added the Identity database schema using migrations.
- Implemented user registration with built-in password hashing and validation.

### JWT Authentication

- Implemented login and JWT token generation.
- Added user ID and email claims.
- Protected API endpoints using `[Authorize]`.
- Tested successful login and expired-token rejection.

### Authorization

- Created `User` and `Admin` roles.
- Added role claims to JWTs.
- Restricted endpoints using role-based authorization.
- Implemented the `CanCreateTasks` policy using a custom permission claim.

### FluentValidation

- Created dedicated validators for Create and Update Task requests.
- Added validation rules for `Title`, `UserId`, and `DueDate`.
- Integrated automatic validation into the ASP.NET Core request pipeline.
- Tested structured `400 Bad Request` validation responses.

### API Security Hardening

- Added general and stricter login rate limits.
- Configured a restricted CORS policy for the frontend origin.
- Enabled HTTPS redirection and configured HSTS.
- Reviewed the project for unsafe raw SQL usage and continued using EF Core and LINQ parameterized queries.

## Tools Used

- C#
- ASP.NET Core Web API
- ASP.NET Core Identity
- Entity Framework Core
- SQL Server
- JWT
- FluentValidation
- ASP.NET Core Rate Limiting
- CORS
- Postman
- Visual Studio
- Git
- GitHub

## Summary

During Week 4, I extended the Task Tracker API with authentication, authorization, request validation, and additional security protections.

I implemented Identity-based registration, JWT authentication, role and policy-based authorization, FluentValidation, rate limiting, CORS restrictions, HTTPS redirection, HSTS, and SQL injection prevention practices.
