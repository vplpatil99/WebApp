import axios from "axios";

const api = axios.create({
  baseURL: "http://10.208.183.20:2221/api", // change port if needed
});

api.interceptors.request.use(config => {
  const token = localStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default api;
