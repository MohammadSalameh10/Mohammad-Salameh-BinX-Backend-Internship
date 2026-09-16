# Week 9 - Day 4: Deploying to Railway and Extending the CI/CD Pipeline

## Overview

In this day, I deployed the Cardiac Patient Monitoring System API to Railway and configured the production environment required for the live application.

The deployment included a production SQL Server database, Redis, JWT configuration, and secure environment variables. I also applied the EF Core migrations to the production database and verified that the live API was reachable through its public Railway URL.

After confirming the manual deployment, I extended the existing GitHub Actions workflow with a deployment job. The pipeline now builds and tests the project first, then deploys to Railway only after the build job succeeds on a push to the `main` branch.

## Manual Deployment to Railway

The Cardiac Patient Monitoring System API was deployed manually to Railway before automating the deployment process.

Because the project targets `.NET 10` and Railway could not build the application directly with Railpack, I added a `Dockerfile` to the project and configured Railway to use the capstone project directory as the service root.

The Railway service was then connected to the GitHub repository and deployed successfully.

The live API is available at:

`https://mohammad-salameh-binx-backend-internship-production.up.railway.app`

The root URL returns `404 Not Found` because the API does not define an endpoint for `/`, but the application is reachable and real API endpoints can be accessed through the public URL.

## Production Environment Configuration

The production environment was configured using Railway service variables instead of storing sensitive values in the repository.

The following production configuration values were added:

- `ConnectionStrings__DefaultConnection`
- `ConnectionStrings__Redis`
- `Jwt__Issuer`
- `Jwt__Audience`
- `Jwt__Key`

A production SQL Server database was created on MonsterASP.NET and remote access was enabled so the Railway-hosted API could connect to it.

A Redis service was also created on Railway and linked to the API through a Railway reference variable.

The EF Core migrations were applied to the production SQL Server database before restarting the application so the required database schema, including ASP.NET Core Identity tables, was available.

## Verifying the Live API

After the production environment was configured and the database migrations were applied, the Railway service started successfully.

The live API was tested through Postman using the Railway public URL as the collection `baseUrl`.

The login endpoint was tested successfully:

`POST /api/Auths/login`

The request returned:

`200 OK`

and a valid JWT token, confirming that the deployed API could successfully use the production database, ASP.NET Core Identity, and JWT authentication.

## Extending the CI/CD Pipeline

The existing GitHub Actions workflow was extended with a deployment job for Railway.

A Railway project token was created and stored securely in GitHub as:

`RAILWAY_TOKEN`

The deployment job depends on the build job:

```yaml
needs: build
```

This ensures that deployment only starts after the build and automated tests complete successfully.

The deployment is also restricted to pushes on the `main` branch:

```yaml
if: github.ref == 'refs/heads/main' && github.event_name == 'push'
```

The Railway CLI is installed inside the deployment job and used to deploy the application:

```yaml
- run: npm install -g @railway/cli

- run: railway up --service "Mohammad-Salameh-BinX-Backend-Internship" --detach
  env:
    RAILWAY_TOKEN: ${{ secrets.RAILWAY_TOKEN }}
```

After correcting the deployment source path, the full pipeline completed successfully and Railway confirmed the automated deployment as active.

## CI/CD Verification

The updated GitHub Actions workflow was tested by pushing a change to the `main` branch.

The pipeline completed the following sequence successfully:

1. Restore dependencies
2. Build the application
3. Run the automated test suite
4. Run the Railway deployment job

The deployment job was configured with:

```yaml
needs: build
```

so the application is not deployed unless the build and tests succeed first.

After fixing the Railway deployment source path, the GitHub Actions workflow completed successfully and Railway confirmed the new deployment as active.

This verified that the project now has a working CI/CD pipeline where a successful push to `main` automatically builds, tests, and deploys the live API.

## Tools Used

- Railway
- GitHub Actions
- GitHub Secrets
- Docker
- MonsterASP.NET
- SQL Server
- Redis
- Postman
- Visual Studio
