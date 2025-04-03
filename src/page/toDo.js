import React, { useState, useEffect } from "react";
import axios from "../Api";
import { Table, Tag, Button, Modal, message, Card, Typography, Space } from "antd";
import { CheckCircleOutlined, CloseCircleOutlined } from "@ant-design/icons";

const { Title } = Typography;

const ApprovalPage = () => {
  const [data, setData] = useState([]);
  const [appointments, setAppointments] = useState([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    fetchPendingListings();
    fetchPendingAppointments();
  }, []);

  const statusColors = {
    0: "gray",    // Chưa duyệt
    1: "orange",  // Đang phê duyệt
    2: "green",   // Đã phê duyệt
    3: "red"      // Không cho phép
  };

  const statusText = {
    0: "Chưa duyệt",
    1: "Đang phê duyệt",
    2: "Đã phê duyệt",
    3: "Không cho phép"
  };

  const fetchPendingListings = async () => {
    setLoading(true);
    try {
      const response = await axios.get("/api/Realestate/get-all-Approve");
      setData(response.data);
      console.log("data bat dong san",response.data);
    } catch (error) {
      message.error("Lỗi khi tải danh sách bất động sản!");
    }
    setLoading(false);
  };

  const fetchPendingAppointments = async () => {
    setLoading(true);
    try {
      const response = await axios.get("/api/MappingUserAppointment/get-all-Approve");
      setAppointments(response.data);
    } catch (error) {
      message.error("Lỗi khi tải danh sách lịch hẹn!");
    }
    setLoading(false);
  };

  const handleApproval = async (id, type, status) => {
    Modal.confirm({
      title: `Xác nhận ${status === 2 ? "phê duyệt" : "từ chối"}?`,
      content: "Hành động này sẽ thay đổi trạng thái của mục này.",
      okText: "Xác nhận",
      cancelText: "Hủy",
      onOk: async () => {
        try {
          const url = type === "listing"
            ? `/api/Realestate/approve/${id}`
            : `/api/MappingUserAppointment/appointment/${id}`; 
  
          const requestData = { approvalStatus: status };
  
          await axios.put(url, requestData);
  
          message.success(status === 2 ? "Đã phê duyệt!" : "Đã từ chối!");
          type === "listing" ? fetchPendingListings() : fetchPendingAppointments();
        } catch (error) {
          message.error("Lỗi khi cập nhật trạng thái!");
        }
      },
    });
  };
  

  const columns = [
    { title: "Tên Bất Động Sản", dataIndex: "realEstateName", key: "realEstateName" },
    {
      title: "Trạng thái",
      dataIndex: "approvalStatus",
      key: "approvalStatus",
      render: (status) => <Tag color={statusColors[status]}>{statusText[status]}</Tag>,
    },
    {
      title: "Hành động",
      key: "actions",
      render: (_, record) => (
        <Space>
          <Button type="primary" icon={<CheckCircleOutlined />} onClick={() => handleApproval(record.realEstateId, "listing", 2)}>
            Duyệt
          </Button>
          <Button danger icon={<CloseCircleOutlined />} onClick={() => handleApproval(record.realEstateId, "listing", 3)}>
            Từ chối
          </Button>
        </Space>
      ),
    },
  ];

  const appointmentColumns = [
    { title: "Khách hàng", dataIndex: "customerName", key: "customerName" },
    { title: "Ngày hẹn", dataIndex: "appointmentDate", key: "appointmentDate" },
    {
      title: "Trạng thái",
      dataIndex: "approvalStatus",
      key: "approvalStatus",
      render: (status) => <Tag color={statusColors[status]}>{statusText[status]}</Tag>,
    },
    {
      title: "Hành động",
      key: "actions",
      render: (_, record) => (
        <Space>
          <Button type="primary" icon={<CheckCircleOutlined />} onClick={() => handleApproval(record.mappingUserAppointmentId, "appointment", 2)}>
            Duyệt
          </Button>
          <Button danger icon={<CloseCircleOutlined />} onClick={() => handleApproval(record.mappingUserAppointmentId, "appointment", 3)}>
            Từ chối
          </Button>
        </Space>
      ),
    },
  ];

  return (
    <div style={{ padding: 10, overflow: "auto", maxHeight: "88vh", scrollbarWidth: "none", msOverflowStyle: "none" }}>
      <Card>
        <Title level={2}>Phê duyệt bài đăng bất động sản</Title>
        <Table columns={columns} dataSource={data} rowKey="id" loading={loading} pagination={{ pageSize: 5 }} />
      </Card>
      <Card style={{ marginTop: 5 }}>
        <Title level={2}>Phê duyệt lịch hẹn</Title>
        <Table columns={appointmentColumns} dataSource={appointments} rowKey="mappingUserAppointmentId" loading={loading} pagination={{ pageSize: 5 }} />
      </Card>
    </div>
  );
};

export default ApprovalPage;
