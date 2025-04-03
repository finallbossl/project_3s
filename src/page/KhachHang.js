import React, { useEffect, useState } from "react";
import axios from "../Api"; 
import { Table, Checkbox, Button, Pagination } from "antd";
import { SearchOutlined } from "@ant-design/icons";
import "antd/dist/reset.css";

const pageSize = 8;

const CustomerList = () => {
  const [customers, setCustomers] = useState([]);
  const [selectedLocation, setSelectedLocation] = useState([]);
  const [selectedPurpose, setSelectedPurpose] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);

  useEffect(() => {
    fetchCustomers();
  }, [selectedLocation, selectedPurpose]);

  const fetchCustomers = async () => {
    try {
      const response = await axios.get("/api/Customer/GetAll", {
        params: {
          location: selectedLocation.join(","),
          purpose: selectedPurpose.join(","),
        },
      });
      setCustomers(response.data);
    } catch (error) {
      console.error("Error fetching customers:", error);
    }
  };

  const handleLocationChange = (e) => {
    const value = e.target.value;
    setSelectedLocation((prev) =>
      prev.includes(value) ? prev.filter((loc) => loc !== value) : [...prev, value]
    );
  };

  const handlePurposeChange = (e) => {
    const value = e.target.value;
    setSelectedPurpose((prev) =>
      prev.includes(value) ? prev.filter((p) => p !== value) : [...prev, value]
    );
  };

  const columns = [
    { title: "Full name", dataIndex: "fullName", key: "fullName" },
    { title: "Địa chỉ", dataIndex: "address", key: "address" },
    { title: "Mục đích", dataIndex: "customerType", key: "customerType" },
    { title: "SĐT", dataIndex: "phoneNumber", key: "phoneNumber" },
    { title: "Ngày tạo", dataIndex: "createdAt", key: "createdAt" },
  ];

  return (
    <div style={{ padding: 20, display: "flex", gap: 20 }}>
      <div style={{ width: 250 }}>
        <div style={{ display: "flex", justifyContent: "space-between", marginBottom: 20 }}>
          <Button icon={<SearchOutlined />} size="large" onClick={fetchCustomers}>Lọc</Button>
        </div>
        <p>Vị trí</p>
        <Checkbox value="Quy nhon" onChange={handleLocationChange}>Quy Nhơn</Checkbox>
        <Checkbox value="Phú yên" onChange={handleLocationChange}>Phú Yên</Checkbox>
        <p>Mục đích</p>
        <Checkbox value="Mua" onChange={handlePurposeChange}>Mua</Checkbox>
        <Checkbox value="Bán" onChange={handlePurposeChange}>Bán</Checkbox>
        <Button block style={{ marginTop: 10 }} onClick={() => { setSelectedLocation([]); setSelectedPurpose([]); }}>Xóa</Button>
      </div>
      <div style={{ flex: 1 }}>
        <h2>Khách Hàng:</h2>
        <Table
          columns={columns}
          dataSource={customers.slice((currentPage - 1) * pageSize, currentPage * pageSize)}
          pagination={false}
        />
        <Pagination
          current={currentPage}
          total={customers.length}
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
