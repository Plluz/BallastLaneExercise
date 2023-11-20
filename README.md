# BallastLane Technical Assignment

This repository contains the solution to the technical assignment provided by BallastLane as part of the Software Engineer hiring process.

## Tech Overview

- **Clean Architecture:** To ensure separation of concerns, maintainability, and scalability.
- **.NET 8:** Harnessing the latest features and improvements in the framework.
- **Docker:** Containerized the Web API for portability and easy deployment.
- **PostgreSQL:** The database to store and manage data.
- **ADO.NET:** For database connectivity within the application.
- **JWT Tokens:** To help with authentication.
- **Postman:** For testing API endpoints.

## User Story and Thought Process

- Check out a [simulated user story (TODO)](#) that provides a narrative overview of the project.
- Additionally, explore a [quick presentation (TODO)](#) for technical insights and design decisions made during development.

## Postman Files

Access the [Postman Collection (TODO)](#) and [Postman Environment file (TODO)](#) to explore and test the API endpoints.

## Usage

To run the application, navigate to the root folder (BallastLaneExercise folder) and execute the following command in the terminal:

```bash
docker-compose up
```

This will start the Application and PostgreSQL.
Once the container is running, you can access the Swagger documentation at https://localhost:8081/swagger (or the Postman provided above) to test the API endpoints.

Ports used: 8080/8081 for HTTP/HTTPS and 5432 for Postgres
