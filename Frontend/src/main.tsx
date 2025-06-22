import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import App from "./App.tsx";
import { BrowserRouter } from "react-router-dom";
import { AuthProvider } from "./contexts/AuthContext.tsx";
import { BasketProvider } from "./contexts/BasketContext.tsx";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <BrowserRouter>
      <AuthProvider>
        <BasketProvider>
          <App />
        </BasketProvider>
      </AuthProvider>
    </BrowserRouter>
  </StrictMode>
);
