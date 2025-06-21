import { createContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthService } from "../api/auth/AuthService";
import { User } from "../types/User";

interface AuthContextProps {
  user: User | null;
  login: (token: string) => void;
  logout: () => void;
}

export const AuthContext = createContext<AuthContextProps | undefined>(
  undefined
);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [user, setUser] = useState<User | null>(null);
  const [token, setToken] = useState<string | null>(() =>
    localStorage.getItem("token")
  );

  const navigate = useNavigate();

  useEffect(() => {
    if (token) {
      AuthService.getProfile()
        .then((res) => setUser(res.data))
        .catch((err) => {
          console.log("profile err", err);
          localStorage.removeItem("token");
          setToken(null);
          setUser(null);
        });
    } else {
      setUser(null);
    }
  }, [token]);

  const login = (newToken: string) => {
    localStorage.setItem("token", newToken);
    setToken(newToken);
    navigate("/catalog");
  };

  const logout = () => {
    localStorage.removeItem("token");
    setToken(null);
    setUser(null);
    navigate("/catalog");
  };

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
