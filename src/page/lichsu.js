import React, { useState, useEffect } from "react";
import { Table, Tag } from "antd";
import Api from "../Api"; // Assuming you have an Axios instance for API calls

const HistoryPage = () => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    fetchHistory();
  }, []);

  const fetchHistory = async () => {
    setLoading(true);
    try {
      const response = await Api.get("/api/audit");
      const result = response.data;
  
      const mappedData = result.map((audit) => ({
        id: audit.id,
      user: audit.actionType === "Updated" ? audit.updatedByFullName : audit.createdByFullName,
      action: audit.actionType,
      objectType:
      audit.entityType === "RealEstateEntity"
      ? "Bất động sản:"
      : audit.entityType === "ContractEntity"
      ? "Hợp đồng bất động sản"
      : audit.entityType,
      timestamp: audit.createdAt,
      }));
  
      setData(mappedData);
    } catch (error) {
      console.error("Error fetching audit data:", error);
    } finally {
      setLoading(false);
    }
  };
  

  const columns = [
    {
      title: "Người thao tác",
      dataIndex: "user",
      key: "user",
    },
    {
      title: "Hành động",
      dataIndex: "action",
      key: "action",
      render: (action) => {
        let actionText = "";
        let color = "default";
  
        if (action === "Created") {
          actionText = "Tạo dữ liệu";
          color = "green";
        } else if (action === "Updated") {
          actionText = "Cập nhật dữ liệu";
          color = "blue";
        } else if (action === "Delete") {
          actionText = "Xóa dữ liệu";
          color = "red";
        }
  
        return <Tag color={color}>{actionText}</Tag>;
      },
    },
    {
      title: "Loại đối tượng",
      dataIndex: "objectType",
      key: "objectType",
      render: (objectType) => {
        // Mapping objectType to a more user-friendly format
        if (objectType === "RealEstateEntity") {
          return "Bất động sản";
        }
        // You can add more mappings here for other object types
        return objectType; // Default return if not mapped
      },
    },
    {
      title: "Thời gian",
      dataIndex: "timestamp",
      key: "timestamp",
    },
  ];

  return (
    <div>
      <h2>Lịch sử thao tác</h2>
      <Table
        columns={columns}
        dataSource={data}
        loading={loading}
        rowKey="id"
        pagination={{ pageSize: 10 }}
      />
    </div>
  );
};

export default HistoryPage;
