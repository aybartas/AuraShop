import { createContext, useEffect, useMemo, useState } from "react";
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
  const navigate = useNavigate();

  const token = localStorage.getItem("token");

  useEffect(() => {
    if (token) {
      AuthService.getProfile()
        .then((res) => setUser(res.data))
        .catch(() => localStorage.removeItem("token"));
    } else {
      setUser(null);
    }
  }, [token]);

  const login = (token: string) => {
    localStorage.setItem("token", token);
    AuthService.getProfile().then((res) => {
      setUser(res.data);
      navigate("/catalog");
    });
  };

  const logout = () => {
    localStorage.removeItem("token");
    setUser(null);
    navigate("/catalog");
  };

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};
