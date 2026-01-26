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
    config.headers.Authorization = `Bearer ${keycloak.token}`;
  }
  return config;
});

export default http;
