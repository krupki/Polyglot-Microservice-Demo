# Polyglot Microservice Demo

A high-performance distributed architecture demonstrating interoperability between .NET 10, Go, and PostgreSQL.

## Tech Stack

Orchestrator: .NET 10 (C#) Web API.

Worker: Go (Golang) for specialized sorting.

Database: PostgreSQL 15 with Identity-Column sync.

Infastructure: Docker & Docker Compose.

## Getting Started

### Infrastructure

```Bash
docker-compose up --build
```

### Endpoints

**GET** /api/personen: Fetch all persons.

**POST** /api/personen: Add person (DB-managed ID).

**GET** /api/personen/sorted: Delegates sorting to the Go Worker via internal Docker DNS.

Internally, the .NET orchestrator communicates with the Go sorter via gRPC.
