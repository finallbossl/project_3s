import React, { useState } from "react";
import { Table, Checkbox, Button, Pagination } from "antd";
import { SearchOutlined, UserOutlined } from "@ant-design/icons";
import "antd/dist/reset.css";

const data = Array.from({ length: 50 }, (_, i) => ({
  key: i,
  fullName: `Customer ${i + 1}`,
  location: "Quy nhon",
  purpose: i % 3 === 0 ? "Mua" : i % 3 === 1 ? "Bán" : "Thuê",
  realEstateId: "id: QN341",
  phone: "03452819381",
  startDate: "12.01.2025",
  id: Math.floor(Math.random() * 100000),
}));

const columns = [
  { title: "Full name", dataIndex: "fullName", key: "fullName" },
  { title: "Địa chỉ", dataIndex: "location", key: "location" },
  { title: "Mục đích", dataIndex: "purpose", key: "purpose" },
  { title: "Bất động sản chú ý", dataIndex: "realEstateId", key: "realEstateId" },
  { title: "SĐT", dataIndex: "phone", key: "phone" },
  { title: "Start date", dataIndex: "startDate", key: "startDate" },
  { title: "ID", dataIndex: "id", key: "id" },
];

const CustomerList = () => {
  const [selectedLocation, setSelectedLocation] = useState(["Quy nhon"]);
  const [selectedPurpose, setSelectedPurpose] = useState(["Mua"]);
  const [currentPage, setCurrentPage] = useState(1);
  const pageSize = 8;

  const handleLocationChange = (e) => {
    const value = e.target.value;
    setSelectedLocation((prev) =>
      prev.includes(value) ? prev.filter((loc) => loc !== value) : [...prev, value]
    );
  };

  const handlePurposeChange = (e) => {
    const value = e.target.value;
    setSelectedPurpose((prev) =>
      prev.includes(value) ? prev.filter((purpose) => purpose !== value) : [...prev, value]
    );
  };

  const filteredData = data.filter(
    (item) =>
      selectedLocation.includes(item.location) && selectedPurpose.includes(item.purpose)
  );

  return (
    <div style={{ padding: 20, display: "flex", gap: 20 }}>
      <div style={{ width: 250 }}>
        <div style={{ display: "flex", justifyContent: "space-between", marginBottom: 20 }}>
          <Button icon={<SearchOutlined />} size="large">Lọc</Button>
        </div>
        <p>Vị trí</p>
        <Checkbox value="Quy nhon" checked={selectedLocation.includes("Quy nhon")} onChange={handleLocationChange}>
          Quy nhơn
        </Checkbox>
        <Checkbox value="Phú yên" checked={selectedLocation.includes("Phú yên")} onChange={handleLocationChange}>
          Phú yên
        </Checkbox>
        <Checkbox value="Quảng Ngãi" disabled>
          Quảng Ngãi
        </Checkbox>
        <p>Mục đích</p>
        <Checkbox value="Mua" checked={selectedPurpose.includes("Mua")} onChange={handlePurposeChange}>
          Mua
        </Checkbox>
        <Checkbox value="Bán" disabled>
          Bán
        </Checkbox>
        <Checkbox value="Thuê" disabled>
          Thuê
        </Checkbox>
        <Button block style={{ marginTop: 10 }}>Xóa</Button>
      </div>
      <div style={{ flex: 1 }}>
        <h2>Khách Hàng:</h2>
        <Table
          columns={columns}
          dataSource={filteredData.slice((currentPage - 1) * pageSize, currentPage * pageSize)}
          pagination={false}
        />
        <Pagination
          current={currentPage}
          total={filteredData.length}
          pageSize={pageSize}
          onChange={(page) => setCurrentPage(page)}
          style={{ marginTop: 20, textAlign: "center" }}
          showSizeChanger={false}
        />
      </div>
    </div>
  );
};

export default CustomerList;
