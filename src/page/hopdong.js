import React from "react";
import { List, Tag, Button, Pagination } from "antd";
import { UserOutlined, PlusOutlined, MoreOutlined } from "@ant-design/icons";
import "antd/dist/reset.css";
import { Link } from "react-router-dom";
import { useState } from "react";

const pageSize = 4;
const dataSource = Array(20).fill({}).map((_, index) => ({
  key: index,
  name: `Hợp đồng #${index + 1}`,
  updated: `Cập nhật ${Math.floor(Math.random() * 30) + 1} ngày trước`,
  status: index % 2 === 0 ? "Hoàn Thành" : "Đang Xử Lý",
  user: `Khách hàng ${index + 1}`,
  role: index % 3 === 0 ? "Admin" : "Nhân viên",
}));

const ContractList = () => {
  const [currentPage, setCurrentPage] = useState(1);
  const startIndex = (currentPage - 1) * pageSize;
  const currentData = dataSource.slice(startIndex, startIndex + pageSize);

  return (
    <div style={{ padding: 20 }}>
      <Button type="primary" icon={<PlusOutlined />} style={{ marginBottom: 16, backgroundColor: "#2c5282" }}>
        <Link to="themhopdong" style={{ color: "white" }}>Thêm Mới</Link>
      </Button>
      <List
        dataSource={currentData}
        renderItem={(item) => (
          <List.Item style={{ borderBottom: "1px solid #ddd", padding: "12px 16px" }}>
            <List.Item.Meta
              title={<strong>{item.name}</strong>}
              description={<>
                <p><strong>Ngày tạo:</strong> {item.updated}</p>
                <p><strong>Người thực hiện:</strong> {item.user}</p>
              </>}
            />
            <Tag color={item.status === "Hoàn Thành" ? "green" : "orange"}>{item.status}</Tag>
            <Button icon={<UserOutlined />} size="small" style={{ marginLeft: 8 }}>{item.role}</Button>
            <Button icon={<MoreOutlined />} size="small" style={{ marginLeft: 8 }} />
          </List.Item>
        )}
        bordered
      />
      <Pagination
        current={currentPage}
        pageSize={pageSize}
        total={dataSource.length}
        onChange={(page) => setCurrentPage(page)}
        style={{ marginTop: 16, textAlign: "center" }}
      />
    </div>
  );
};

export default ContractList;