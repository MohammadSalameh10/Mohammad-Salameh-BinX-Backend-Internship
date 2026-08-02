# Week 3 — Backend Internship

## Overview

Week 3 begins with REST API design principles and resource modeling.

The first exercise focused on designing a RESTful API before implementing it, including resource naming, HTTP methods, status codes, nested resources, and API versioning.

This README will be updated as the remaining Week 3 exercises are completed.

## Completed Days

### Day 1 — REST API Design Principles & Resource Modeling

Designed a REST resource map for a Task Tracker API.

The exercise included:

- Identifying the core API resources.
- Naming resources using plural nouns.
- Designing CRUD endpoints for the `tasks` resource.
- Using the correct HTTP methods.
- Assigning success and error status codes.
- Creating a nested resource endpoint.
- Selecting URL-based API versioning.
- Organizing the resource map in Postman.
- Documenting the API design in Notion and GitHub.

Core resources:

```text
users
tasks
comments
```

Designed endpoints:

```http
GET    /api/v1/tasks
GET    /api/v1/tasks/{id}
POST   /api/v1/tasks
PUT    /api/v1/tasks/{id}
DELETE /api/v1/tasks/{id}
GET    /api/v1/users/{userId}/tasks
```

[View Day 1 Documentation](./Day%201/README.md)

## Tools Used

- Postman
- Notion
- Git
- GitHub

## Repository Structure

```text
Week 3/
├── README.md
└── Day 1/
    └── README.md
```

## Progress

| Day | Topic | Status |
|---|---|---|
| Day 1 | REST API Design Principles & Resource Modeling | Completed |
| Day 2 | To be added | Not started |
| Day 3 | To be added | Not started |
| Day 4 | To be added | Not started |
| Day 5 | To be added | Not started |