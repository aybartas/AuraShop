# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

AuraShop is a microservices-based e-commerce platform using .NET 8 (backend) and React 18 + TypeScript (frontend), orchestrated with Docker Compose.

## Development Commands

### Running the Full Stack

```bash
docker-compose up -d
```

| Service | URL |
|---|---|
| API Gateway | http://localhost:5000 |
| Keycloak (admin/admin) | http://localhost:8081 |
| RabbitMQ Management | http://localhost:15672 |

### Frontend

```bash
cd Frontend
npm install
npm run dev        # Dev server at http://localhost:3000
npm run build      # TypeScript check + Vite production build
npm run lint       # ESLint (flat config, v9)
```

### Backend

```bash
dotnet build AuraShop.sln                    # Build entire solution
dotnet build Services/Catalog/AuraShop.Catalog/AuraShop.Catalog.csproj  # Build single service
dotnet run --project Services/Catalog/AuraShop.Catalog                   # Run single service
```

Use the "Docker Compose" launch profile in Visual Studio to run/debug all services simultaneously.

Individual service ports: Gateway (5000), Catalog (7070), Discount (7071), Order (7072), Basket (7074), File (7075), Payment (7076).

Swagger UI is available at each service's base URL (e.g., `http://localhost:7070/swagger`).

### Tests

No test projects exist yet — tests are planned as a future milestone.

## Architecture

### Microservices

| Service | Database | Architecture | Role |
|---|---|---|---|
| Gateway | — | YARP proxy | Reverse proxy, Keycloak JWT validation, routes all traffic |
| Catalog | MongoDB | Vertical Slice | Product & category management, data seeding |
| Basket | Redis | Vertical Slice | Shopping cart; calls Discount service for pricing |
| Discount | MongoDB | Vertical Slice | Promotion/discount code management |
| Order | SQL Server | Clean/DDD | Order creation and retrieval |
| Payment | SQL Server | Vertical Slice | Payment processing (Stripe, in progress) |
| File | — | Vertical Slice | Product image upload/download via RabbitMQ events |

### Two Backend Architecture Styles

**Vertical Slice (most services):** Each feature is a self-contained folder with Command/Query, Handler, Validator, Endpoint, and Response. Uses MediatR for CQRS dispatch. Example structure:
```
Features/Product/Create/
  CreateProductCommand.cs        # MediatR IRequest
  CreateProductCommandHandler.cs # IRequestHandler, returns ServiceResult<T>
  CreateProductCommandValidator.cs # FluentValidation
  CreateProductEndpoint.cs       # Minimal API route as extension method
  CreateProductCommandResponse.cs
```

**Clean Architecture (Order service only):** Four-layer structure:
```
Core/AuraShop.Order.Domain/          → Entities (Order, OrderItem, Address)
Core/AuraShop.Order.Application/     → Use cases, DTOs, validators
Infrastructure/AuraShop.Order.Persistence/ → EF Core, generic repo + Unit of Work
Presentation/AuraShop.Order.WebApi/  → Minimal API endpoints
```

### Shared Libraries (`/Shared/`)

- **AuraShop.Shared** — Cross-cutting concerns: `ServiceResult<T>` response wrapper, Keycloak JWT auth extensions, authorization policies (AdminOnly/CustomerOnly/Authenticated), API versioning, validation filter, MongoDB conventions, `IIdentityService` for accessing current user claims.
- **AuraShop.Bus** — MassTransit + RabbitMQ setup. Call `AddCommonMassTransit()` in `Program.cs`. Defines shared message contracts (`Commands/` and `Events/` folders).

### Service Communication

- **Synchronous**: HTTP via Gateway reverse proxy. Basket calls Discount directly over HTTP.
- **Asynchronous**: RabbitMQ via MassTransit (all services register consumers/publishers).
- **Auth**: Keycloak issues JWTs. Gateway validates first; each downstream service re-validates independently.

### Frontend Architecture (`/Frontend/src/`)

- **`/api/http.ts`** — Axios client with automatic Keycloak JWT injection
- **`/api/services/`** — One file per backend service (CatalogService, BasketService, OrderService, PaymentService)
- **`/contexts/`** — `AuthContext` (Keycloak state), `BasketContext` (cart state)
- **`/app/features/`** — Feature-based structure: `catalog`, `cart`, `checkout`, `home`
- **`/app/layout/`** — `Header`, `PageLayout`, `ProtectedRoute`
- **`/styles/theme.css`** — CSS custom property design tokens (colors, radius, shadows) with dark mode support via `.dark` class
- Provider hierarchy: `StrictMode → BrowserRouter → ReactKeycloakProvider → AuthProvider → BasketProvider → App`
- Routes: `/` home, `/catalog` listing, `/catalog/:id` detail, `/cart`, `/checkout`

## Configuration

### Environment Variables (`.env` at root)

Used by Docker Compose. Contains passwords for MongoDB, Redis, SQL Server, Keycloak, and RabbitMQ.

### Per-Service Config

Each service has `appsettings.Development.json` and `appsettings.Production.json` with:
- `DatabaseSettings` (MongoDB) or connection strings (SQL Server)
- `RabbitMQ` (host, port, credentials)
- `Keycloak` (Authority, Realm `aurashop`, Audience `aurashop-api`)

### Frontend

- `VITE_API_URL` — Gateway URL (e.g., `http://localhost:5000`)
- `VITE_KEYCLOAK_URL` — Keycloak URL (e.g., `http://localhost:8081`)

## Key Patterns

### Backend

- All services use **Minimal APIs** with versioned routes (`/api/v{version:apiVersion}/...`).
- All handler responses are wrapped in **`ServiceResult<T>`**. Endpoints convert to HTTP results via `.ToResult()` extension method.
- Service `Program.cs` setup: call `AddCommonServicesWithAuth()` (registers MediatR, FluentValidation, AutoMapper, versioning, Keycloak auth) and `AddCommonMassTransit()` for RabbitMQ.
- Endpoints are defined as static extension methods on `RouteGroupBuilder`, chained together from a group endpoint extensions class (e.g., `ProductEndpointExtensions`).
- Validation runs via `ValidationFilter<T>` added as an endpoint filter.
- MongoDB services call `MongoConvention.AddMongoConventionPack()` at startup.

### Frontend

- Tailwind CSS with CSS variable design tokens — use semantic color names (`primary`, `accent`, `surface`, `text-secondary`) not raw hex values.
- API service objects use static method style: `CatalogService.getProducts()`.
- Backend responses are wrapped in `ServiceResult<T>` — actual data is in `response.data.data`.
- State via React Context + hooks only (no Redux). Auth and basket contexts with `useAuth()` and `useBasket()` hooks.
- No CSS modules or styled-components — all styling is Tailwind utility classes.
