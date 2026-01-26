import { FC, ReactNode } from "react";
import { useAuth } from "../../contexts/AuthContext";

interface ProtectedRouteProps {
  children: ReactNode;
  requiredRoles?: string[];
}

const ProtectedRoute: FC<ProtectedRouteProps> = ({
  children,
  requiredRoles,
}) => {
  const { keycloak, initialized } = useAuth();

  if (!initialized) return null;
  if (!keycloak?.authenticated) {
    keycloak?.login();
    return null;
  }
  if (
    requiredRoles &&
    !requiredRoles.some(
      (role) => keycloak.hasRealmRole(role) || keycloak.hasResourceRole(role),
    )
  ) {
    return <div>Unauthorized</div>;
  }
  return <>{children}</>;
};

export default ProtectedRoute;
