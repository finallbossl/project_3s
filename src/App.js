import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import LoginPage from "./page/LoginPage";
import Home from "./page/home";
import BatDongSan from "./page/batDongSan";
import ProtectedRoute from "./componnent/ProtectedRoute";
import NhanVien from "./page/nhanVien";
import LichHen from "./page/LichHen";
import HopDong from "./page/hopdong";
import KhachHang from "./page/KhachHang";
import ThemBds from "./componnent/ThemBds";
import ThemHopDong from "./componnent/themHopDong";
import ThemNhanVien from "./componnent/ThemNhanVien";
import UseInfo from "./page/UserInfo";
import PheDuyet from "./page/toDo";
import TrangChu from "./page/trangchu";
import LichSu from "./page/lichsu";
import TaiKhoan from "./page/taiKhoan";

function App() {
  return (
    <Router>
      <Routes>
        {/* Route trang login */}
        <Route path="/" element={<LoginPage />} />

        <Route
          path="/app"
          element={
            <ProtectedRoute>
              <Home />
            </ProtectedRoute>
          }
        >
          <Route path="trangchu" element={<TrangChu />} />
          <Route path="batdongsan" element={<BatDongSan />} />
          <Route path="thongtincanhan" element={<UseInfo />} />
          <Route path="nhanvien" element={<NhanVien />} />
          <Route path="lichhen" element={<LichHen />} />
          <Route path="hopdong" element={<HopDong />} />
          <Route path="khachhang" element={<KhachHang />} />
          <Route
            path="pheduyet"
            element={
              <ProtectedRoute requiredRole="Admin">
                <PheDuyet />
              </ProtectedRoute>
            }
          />
          <Route path="LichSu" element={<LichSu />} />
          <Route
            path="TaiKhoan"
            element={
              <ProtectedRoute requiredRole="Admin">
                <TaiKhoan />
              </ProtectedRoute>
            }
          />
          <Route path="hopdong/themhopdong" element={<ThemHopDong />} />
          <Route path="nhanvien/themnhanvien" element={<ThemNhanVien />} />
          <Route path="batdongsan/thembatdongsan" element={<ThemBds />} />
        </Route>
      </Routes>
    </Router>
  );
}

export default App;
