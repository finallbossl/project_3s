import React, { useState } from "react";
import { Layout, Avatar, Button, Typography, Badge, Popover, List } from "antd";
import { Link } from "react-router-dom";
import { BellOutlined } from "@ant-design/icons";

const { Header } = Layout;
const { Text } = Typography;

const notifications = [
  "Thông báo 1",
  "Thông báo 2",
  "Thông báo 3",
  "Thông báo 4",
  "Thông báo 5"
];

const CustomHeader = () => {
  const [visible, setVisible] = useState(false);

  return (
    <Header
      style={{
        background: "#fff",
        padding: "8px 16px",
        display: "flex",
        alignItems: "center",
        justifyContent: "space-between",
        boxShadow: "0px 2px 8px rgba(0, 0, 0, 0.1)",
        marginTop:"10px"
      }}
    >
      {/* Phần bên trái */}
      <div style={{ display: "flex", alignItems: "center" }}>
        
          <Avatar
            size={48}
            src="https://purwosari.magetan.go.id/image/admin.png"
          />
        <Text style={{ marginLeft: 12, fontSize: 16, fontWeight: 500 }}>
          Xin chào, Admin
        </Text>
      </div>

      {/* Logo ở giữa */}
      <div style={{display:"flex",justifyContent:"center",alignItems: "center"}}> 
        <img
          src="https://upload.wikimedia.org/wikipedia/commons/a/a7/React-icon.svg"
          alt="Logo"
          style={{ height: 40 }}
        />
        <h1>Hệ Thống Quản Lý 3S</h1>
      </div>

      {/* Phần bên phải */}
      <div style={{ display: "flex", alignItems: "center", gap: 16 }}>
        <Popover
          content={
            <List
              dataSource={notifications}
              renderItem={(item) => <List.Item>{item}</List.Item>}
            />
          }
          title="Thông báo"
          trigger="click"
          visible={visible}
          onVisibleChange={setVisible}
        >
          <Badge count={5}>
            <BellOutlined style={{ fontSize: 22, color: "#1890ff", cursor: "pointer" }} />
          </Badge>
        </Popover>
        <Link to="thongtincanhan">
        <Button type="primary" shape="round" size="large">
          Quản trị viên
        </Button>
        </Link>
      </div>
    </Header>
  );
};

export default CustomHeader;

