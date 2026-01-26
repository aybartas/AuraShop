import Keycloak from "keycloak-js";

const keycloak = new Keycloak({
  url: import.meta.env.VITE_KEYCLOAK_URL,
  realm: "aurashop",
  clientId: "aurashop-client",
});

export default keycloak;

export interface AuthContextType {
  keycloak: Keycloak | null;
  initialized: boolean;
}
