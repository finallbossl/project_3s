import React, { useState } from "react";
import { List, Tag, Button } from "antd";
import { PlusOutlined } from "@ant-design/icons";
import { Link } from "react-router-dom";
const NhanVien = () => {
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 9; // Số lượng nhân viên trên mỗi trang

  const duLieu = [
    { key: 1, ten: "Nguyễn Đức Luân", gioLamViec: "7:00 - 11:00", trangThai: "Hoạt động" },
    { key: 2, ten: "Phan Duy Hùng", gioLamViec: "7:00 - 15:00", trangThai: "Không hoạt động" },
    { key: 3, ten: "Đỗ Tấn Vũ", gioLamViec: "7:00 - 15:00", trangThai: "Không hoạt động" },
    { key: 4, ten: "Nguyễn Hữu Lộc", gioLamViec: "7:00 - 11:00", trangThai: "Hoạt động" },
    { key: 5, ten: "Nguyễn Văn A", gioLamViec: "11:00 - 19:00", trangThai: "Hoạt động" },
    { key: 6, ten: "Nguyễn Đức Luân", gioLamViec: "7:00 - 11:00", trangThai: "Hoạt động" },
    { key: 7, ten: "Phan Duy Hùng", gioLamViec: "7:00 - 15:00", trangThai: "Không hoạt động" },
    { key: 8, ten: "Đỗ Tấn Vũ", gioLamViec: "7:00 - 15:00", trangThai: "Không hoạt động" },
    { key: 9, ten: "Nguyễn Đức Luân", gioLamViec: "7:00 - 11:00", trangThai: "Hoạt động" },
    { key: 10, ten: "Phan Duy Hùng", gioLamViec: "7:00 - 15:00", trangThai: "Không hoạt động" },
    { key: 11, ten: "Đỗ Tấn Vũ", gioLamViec: "7:00 - 15:00", trangThai: "Không hoạt động" },
    { key: 12, ten: "Nguyễn Hữu Lộc", gioLamViec: "7:00 - 11:00", trangThai: "Hoạt động" },
    { key: 13, ten: "Nguyễn Văn A", gioLamViec: "11:00 - 19:00", trangThai: "Hoạt động" },
    { key: 14, ten: "Nguyễn Đức Luân", gioLamViec: "7:00 - 11:00", trangThai: "Hoạt động" },
    { key: 15, ten: "Phan Duy Hùng", gioLamViec: "7:00 - 15:00", trangThai: "Không hoạt động" },
    { key: 16, ten: "Đỗ Tấn Vũ", gioLamViec: "7:00 - 15:00", trangThai: "Không hoạt động" },
  ];

  return (
    <div style={{ padding: 20 }}>
      <Button type="primary" icon={<PlusOutlined />} style={{ marginBottom: 16,backgroundColor:"#2c5282" }}>
         <Link to="themnhanvien">Thêm Nhân Viên</Link>
      </Button>

      {/* Tiêu đề bảng */}
      <div style={{
        display: "flex",
        fontWeight: "bold",
        backgroundColor: "#f0f2f5",
        padding: "10px",
        borderRadius: "5px",
      }}>
        <div style={{ flex: 2 }}>Họ và Tên</div>
        <div style={{ flex: 2 }}>Giờ làm việc</div>
        <div style={{ flex: 1 }}>Trạng thái</div>
      </div>

      {/* Danh sách nhân viên */}
      <List
        dataSource={duLieu.slice((currentPage - 1) * pageSize, currentPage * pageSize)}
        renderItem={(item) => (
          <List.Item
            style={{
              display: "flex",
              padding: "10px",
              marginTop: "5px",
              backgroundColor: item.trangThai === "Hoạt động" ? "#e6fffb" : "#fff5f5",
            }}
          >
            <div style={{ flex: 2 }}>{item.ten}</div>
            <div style={{ flex: 2 }}>{item.gioLamViec}</div>
            <div style={{ flex: 1 }}>
              <Tag color={item.trangThai === "Hoạt động" ? "green" : "red"}>
                {item.trangThai}
              </Tag>
            </div>
          </List.Item>
        )}
        pagination={{
          current: currentPage,
          pageSize,
          total: duLieu.length,
          onChange: (page) => setCurrentPage(page),
        }}
      />
    </div>
  );
};

export default NhanVien;
