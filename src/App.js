import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import LoginPage from "./page/LoginPage";
import Home from "./page/home";
import BatDongSan from "./page/batDongSan";
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

function App() {
  return (
    <Router>
      <Routes>
        {/* Route trang login */}
        <Route path="/" element={<LoginPage />} />

        {/* Route cha có Outlet */}
        <Route path="/app" element={<Home />}>
          <Route path="trangchu" element={<TrangChu/>} />
          <Route path="batdongsan" element={<BatDongSan/>} />
          <Route path="thongtincanhan" element={<UseInfo/>} />
          <Route path="nhanvien" element={<NhanVien/>} />
          <Route path="lichhen" element={<LichHen/>} />
          <Route path="hopdong" element={<HopDong/>} />
          <Route path="khachhang" element={<KhachHang/>} />
          <Route path="pheduyet" element={<PheDuyet/>} />
          <Route path="LichSu" element={<LichSu/>} />
          <Route path="hopdong/themhopdong" element={<ThemHopDong/>} />
          <Route path="nhanvien/themnhanvien" element={<ThemNhanVien/>} />
          <Route path="batdongsan/thembatdongsan" element={<ThemBds/>}/>
        </Route>
      </Routes>
    </Router>
  );
}

export default App;
