import React from "react";
import { Link } from "react-router-dom";
import { Layout, Menu } from "antd";
import { 
  UserOutlined, HomeOutlined, FileOutlined, 
  TeamOutlined, BarChartOutlined, ClockCircleOutlined ,AppstoreOutlined ,HistoryOutlined
} from "@ant-design/icons";




const { Sider } = Layout;

const menuItems = [
  { key: "1", label: <Link to="trangchu">Trang chủ</Link>, icon: <HomeOutlined /> },
  { key: "2", label: <Link to="batdongsan">Quản lý BĐS</Link>, icon: <BarChartOutlined /> },
  { key: "3", label: <Link to="nhanvien">Nhân viên</Link>, icon: <UserOutlined /> },
  { key: "4", label: <Link to="khachhang">Khách hàng</Link>, icon: <TeamOutlined /> },
  { key: "5", label: <Link to="hopdong">Hợp đồng</Link>, icon: <FileOutlined /> },
  { key: "6", label: <Link to="lichhen">Lịch hẹn</Link>, icon: <ClockCircleOutlined /> },
  { key: "7", label: <Link to="lichsu">Lich Sử</Link>, icon: <HistoryOutlined />},
  { key: "8", label: <Link to="pheduyet">Phê Duyệt</Link>, icon: <AppstoreOutlined />},

];

const SidebarMenu = () => (
  <Menu theme="light" defaultSelectedKeys={["2"]} mode="inline" items={menuItems} />
);

const Dashboard = () => {
  return (
    <Layout style={{ minHeight: "100vh" }}>
  <Sider
    width={220}
    style={{
      background: "#f5f5f5",
      marginTop: "16px",
      overflow: "hidden", 
      marginLeft: "10px", 
      marginRight: "10px",
    
    }}
  >
    <SidebarMenu />
  </Sider>
</Layout>
  );
};

export default Dashboard;
