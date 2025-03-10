import React, { useState, useEffect } from "react";
import { Table, Tag } from "antd";

const mockData = [
  {
    id: 1,
    user: "Nguyễn Văn A",
    action: "Thêm bất động sản",
    objectType: "Bất động sản",
    timestamp: "2024-02-18 10:00:00",
  },
  {
    id: 2,
    user: "Trần Thị B",
    action: "Sửa hợp đồng",
    objectType: "Hợp đồng",
    timestamp: "2024-02-18 11:30:00",
  },
  {
    id: 3,
    user: "Lê Văn C",
    action: "Xóa bất động sản",
    objectType: "Bất động sản",
    timestamp: "2024-02-18 13:15:00",
  },
  {
    id: 4,
    user: "Phạm Thị D",
    action: "Tạo lịch hẹn",
    objectType: "Lịch hẹn",
    timestamp: "2024-02-18 14:00:00",
  },
];

const HistoryPage = () => {
  const [data, setData] = useState([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    fetchHistory();
  }, []);

  const fetchHistory = () => {
    setLoading(true);
    setTimeout(() => {
      setData(mockData);
      setLoading(false);
    }, 1000);
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
        const color = action.includes("Thêm") || action.includes("Tạo")
          ? "green"
          : action.includes("Sửa")
          ? "blue"
          : action.includes("Xóa")
          ? "red"
          : "default";
        return <Tag color={color}>{action}</Tag>;
      },
    },
    {
      title: "Loại đối tượng",
      dataIndex: "objectType",
      key: "objectType",
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
      <Table columns={columns} dataSource={data} loading={loading} rowKey="id" pagination={{ pageSize: 10 }} />
    </div>
  );
};

export default HistoryPage;
