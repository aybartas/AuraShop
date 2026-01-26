import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./App.tsx";
import { BrowserRouter } from "react-router-dom";
import { AuthProvider } from "./contexts/AuthContext.tsx";
import { BasketProvider } from "./contexts/BasketContext.tsx";
import { ReactKeycloakProvider } from "@react-keycloak/web";
import keycloak from "./api/auth/keycloak.ts";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <BrowserRouter>
      <ReactKeycloakProvider authClient={keycloak}>
        <AuthProvider>
          <BasketProvider>
            <App />
          </BasketProvider>
        </AuthProvider>
      </ReactKeycloakProvider>
    </BrowserRouter>
  </StrictMode>,
);
