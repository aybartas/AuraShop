import { createContext, useContext, ReactNode } from "react";
import { useKeycloak } from "@react-keycloak/web";
import { AuthContextType } from "../api/auth/keycloak";

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const { keycloak, initialized } = useKeycloak();
  return (
    <AuthContext.Provider value={{ keycloak, initialized }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (!context) throw new Error("useAuth must be used within an AuthProvider");
  return context;
};
