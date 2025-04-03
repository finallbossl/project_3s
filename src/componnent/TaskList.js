import React, { useEffect, useState } from "react";
import { Table, Typography, Badge, Spin, message } from "antd";
import axios from "../Api";

const TaskList = () => {
  const [mapAppointments, setMapAppointments] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const fetchMappings = async () => {
    try {
      const response = await axios.get("/api/MappingUserAppointment");
      console.log("Fetched appointments:", response.data); // Kiểm tra dữ liệu
      setMapAppointments(response.data);
    } catch (error) {
      console.error("Error fetching mappings:", error);
      setError("Có lỗi xảy ra khi tải dữ liệu.");
      message.error("Có lỗi xảy ra khi tải dữ liệu."); // Thông báo lỗi cho người dùng
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchMappings();
  }, []);

  const columns = [
    {
      title: "Công việc",
      dataIndex: "appointmentTitle", // Sửa lại cho đúng với API response
      key: "appointmentTitle",
      render: (text) => <Typography.Text strong>{text}</Typography.Text>,
    },
    {
      title: "Khách hàng",
      dataIndex: "customerName", // Kiểm tra lại xem API có trường này không
      key: "customerName",
    },
    {
      title: "Người thực hiện",
      dataIndex: "fullName", // Sửa lại cho đúng với API response
      key: "fullName",
    },
    {
      title: "Trạng thái",
      dataIndex: "approvalStatus", // Kiểm tra lại xem API có trường này không
      key: "approvalStatus",
      render: (approvalStatus) => (
        <Badge
          status={
            approvalStatus === 1
              ? "success"
              : approvalStatus === 0
              ? "error"
              : "warning"
          }
          text={
            approvalStatus === 1
              ? "Thành công"
              : approvalStatus === 0
              ? "Chưa Hoàn Thành"
              : "Không xác định"
          }
        />
      ),
    },
  ];

  if (error) {
    return <Typography.Text type="danger">{error}</Typography.Text>;
  }

  return (
    <Spin spinning={loading}>
      <Table
        columns={columns}
        dataSource={mapAppointments} // Truyền dữ liệu vào Table
        rowKey="MappingUserAppointmentId" // Đảm bảo mỗi dòng có key duy nhất
        pagination={{ pageSize: 5 }}
        bordered
        title={() => (
          <Typography.Title level={4}>Danh Sách Cuộc Hẹn</Typography.Title>
        )}
      />
    </Spin>
  );
};

export default TaskList;
