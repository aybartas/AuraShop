# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

See also the root `../CLAUDE.md` for full-stack architecture, backend services, and Docker Compose setup.

## Development Commands

```bash
npm install          # Install dependencies
npm run dev          # Dev server (Vite) at http://localhost:3000
npm run build        # TypeScript check + Vite production build
npm run lint         # ESLint (flat config, v9)
npm run preview      # Preview production build locally
```

No test framework is configured yet.

## Tech Stack

- **React 18** with TypeScript, Vite 6, and React Router v7
- **Tailwind CSS 3** for styling (PostCSS + autoprefixer)
- **Keycloak** for auth via `@react-keycloak/web` and `keycloak-js`
- **Axios** for HTTP (no other data fetching library)
- **react-hook-form** for form handling
- **@heroicons/react** and **react-icons** for icons
- **vite-plugin-svgr** for importing SVGs as React components

## Architecture

### Provider Hierarchy (main.tsx)

```
StrictMode → BrowserRouter → ReactKeycloakProvider → AuthProvider → BasketProvider → App
```

Keycloak must initialize before any authenticated API call. The `ReactKeycloakProvider` wraps everything.

### API Layer (`src/api/`)

- `http.ts` — Shared Axios instance. Base URL from `VITE_API_URL`. Automatically attaches Keycloak JWT via request interceptor. All service files import this.
- `auth/keycloak.ts` — Keycloak singleton. Realm: `aurashop`, client: `aurashop-client`. Also exports `AuthContextType`.
- `services/` — One file per backend service (CatalogService, BasketService, OrderService, PaymentService). Each exports an object with methods that call the Axios instance. API routes go through the Gateway (no direct service URLs).

### State Management

No Redux or external state library. State is managed via:
- `AuthContext` — wraps `useKeycloak()`, exposes `useAuth()` hook
- `BasketContext` — cart state with `refreshBasket()`, consumed via `useBasket()` hook in `src/hooks/`

### Routing (App.tsx)

All routes are flat in `App.tsx`: `/`, `/catalog`, `/catalog/:id`, `/cart`, `/checkout`. Use `ProtectedRoute` component to guard routes requiring auth (supports `requiredRoles` prop for realm/resource role checks).

### Feature Structure (`src/app/features/`)

Feature-based organization: `home/`, `catalog/`, `cart/`, `checkout/`. Each feature folder contains page components and feature-specific sub-components.

- `src/app/components/` — Shared components (e.g., `ProductCard`)
- `src/app/layout/` — Layout components (`Header`, `PageLayout`, `ProtectedRoute`)
- `src/types/` — TypeScript interfaces (`Product`, `Basket`, `BasketItem`, `Address`, `User`, `Comment`)

## Environment Variables

Prefixed with `VITE_` (Vite requirement):
- `VITE_API_URL` — Gateway URL (e.g., `http://localhost:5000`)
- `VITE_KEYCLOAK_URL` — Keycloak URL (e.g., `http://localhost:8081`)

## Key Patterns

- API service objects use static method style: `CatalogService.getProducts()`, `BasketService.addItemToCart(data)`.
- Backend responses are wrapped in `ServiceResult<T>` — the actual data is in `response.data.data` for list endpoints.
- Context providers follow the pattern: create context, export provider component, export custom hook with null-check.
- No CSS modules or styled-components — all styling is Tailwind utility classes.
