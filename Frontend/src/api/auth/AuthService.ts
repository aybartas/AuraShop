import http from "../http";

interface LoginRequest {
  email: string;
  password: string;
}

interface RegisterRequest {
  email: string;
  password: string;
}

interface LoginResponse {
  accessToken: string;
}

export const AuthService = {
  login: (data: LoginRequest) => http.post<LoginResponse>("/auth/login", data),
  register: (data: RegisterRequest) => http.post("/auth/register", data),
  getProfile: () => http.get("/auth/profile"),
};
