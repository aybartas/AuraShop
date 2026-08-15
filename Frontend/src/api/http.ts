import axios from "axios";
import keycloak from "./auth/keycloak";

const http = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: {
    "Content-Type": "application/json",
  },
  withCredentials: true,
});

http.interceptors.request.use(async (config) => {
  if (keycloak.authenticated) {
    try {
      await keycloak.updateToken(30);
    } catch {
      keycloak.login();
      return Promise.reject(new Error("Token refresh failed"));
    }
    config.headers.Authorization = `Bearer ${keycloak.token}`;
  }
  return config;
});

http.interceptors.response.use(
  (response) => response,
  async (error) => {
    if (error.response?.status === 401) {
      try {
        await keycloak.updateToken(0);
        error.config.headers.Authorization = `Bearer ${keycloak.token}`;
        return http.request(error.config);
      } catch {
        keycloak.login();
      }
    }
    return Promise.reject(error);
  },
);

export default http;
