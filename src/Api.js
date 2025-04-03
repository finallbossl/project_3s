import axios from "axios";

const API_URL = "http://localhost:5145/"; 

const api = axios.create({
  baseURL: API_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

// 🛠 Interceptor: Thêm token vào request
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    console.log("Token trước khi gửi:", token); // 🛠 Kiểm tra token

    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    
    return config;
  },
  (error) => Promise.reject(error)
);

// 🚀 Interceptor: Xử lý lỗi response (401, 403, 500...)
api.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error("Lỗi API:", error.response);

    if (error.response?.status === 401) {
      localStorage.removeItem("token"); // Xóa token nếu bị 401
      alert("Phiên đăng nhập hết hạn. Vui lòng đăng nhập lại!"); // Thông báo lỗi
      window.location.href = "/login"; // Chuyển hướng về trang đăng nhập
    }

    return Promise.reject(error);
  }
);

export default api;
