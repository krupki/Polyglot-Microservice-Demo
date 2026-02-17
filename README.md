# Polyglot Microservice Demo

A small polyglot microservice demo where:

- ASP.NET Core (.NET 10) exposes a REST API
- PostgreSQL stores persons
- A Go gRPC service sorts person data by age

The API fetches data from Postgres and, for the sorted endpoint, delegates sorting to the Go service over gRPC.

## Architecture

- `PersonsApi` (C#): REST orchestrator
- `sorting-service` (Go): gRPC sorter on port `50051`
- `postgres` (Docker): relational persistence

## Prerequisites

- .NET SDK 10
- Docker Desktop (or Docker Engine + Compose)

## Run the Project

### 1) Start infrastructure (Postgres + Go sorter)

```bash
docker-compose up --build
```

This starts:

- Postgres on `localhost:5432`
- Go sorter on `localhost:50051`

### 2) Run the API

```bash
dotnet run
```

By default (from `launchSettings.json`), the API runs on:

- `http://localhost:5104`
- `https://localhost:7230`

Swagger/OpenAPI is enabled in Development.

## Configuration

Configuration lives in `appsettings.json`.

- Connection string:
	- `ConnectionStrings:PostgresConnection`
- Go gRPC endpoint:
	- `SortingService:GrpcUrl`

Current default is:

```json
"SortingService": {
	"GrpcUrl": "http://go-sorter:50051"
}
```

If you run the API directly on your machine (not in Docker), set this to:

```json
"SortingService": {
	"GrpcUrl": "http://localhost:50051"
}
```

## API Endpoints

Base route: `/api/personen`

### GET `/api/personen`

Returns all persons from Postgres.

### POST `/api/personen`

Creates a person.

Request body:

```json
{
	"name": "Anna",
	"age": 29
}
```

Response: `201 Created`

### GET `/api/personen/sorted`

Returns persons sorted by age using the Go gRPC service.
If the gRPC call fails, the API returns unsorted data as a fallback.

## Quick Test (curl)

```bash
curl -X POST http://localhost:5104/api/personen \
	-H "Content-Type: application/json" \
	-d '{"name":"Max","age":34}'

curl http://localhost:5104/api/personen
curl http://localhost:5104/api/personen/sorted
```

## Troubleshooting

- `Connection refused` to Postgres:
	- ensure `docker-compose` is running and Postgres is healthy
- Sorting endpoint not sorted:
	- verify `SortingService:GrpcUrl` points to a reachable address
- TLS/HTTP2 gRPC issues:
	- this project enables unencrypted HTTP/2 for local gRPC communication
