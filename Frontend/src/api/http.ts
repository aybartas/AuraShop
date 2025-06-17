import axios from "axios";

const http = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

console.log("env", import.meta.env.VITE_API_URL);

http.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

// Optional: Global error handler
http.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      // e.g. redirect to login page
      console.warn("Unauthorized");
    }
    return Promise.reject(error);
  }
);

export default http;
