import React, { useState, useEffect } from "react";
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

  const fetchPendingListings = async () => {
    setLoading(true);
    setTimeout(() => {
      setData([
        { id: 1, title: "Căn hộ cao cấp Quận 1", status: "pending" },
        { id: 2, title: "Nhà phố Quận 7", status: "pending" },
      ]);
      setLoading(false);
    }, 1000);
  };

  const fetchPendingAppointments = async () => {
    setLoading(true);
    setTimeout(() => {
      setAppointments([
        { id: 1, customer: "Nguyễn Văn A", date: "2024-02-20", status: "pending" },
        { id: 2, customer: "Trần Thị B", date: "2024-02-22", status: "pending" },
      ]);
      setLoading(false);
    }, 1000);
  };

  const handleApprove = (id, type) => {
    Modal.confirm({
      title: "Xác nhận phê duyệt?",
      onOk: () => {
        if (type === "listing") {
          setData(data.filter((item) => item.id !== id));
        } else {
          setAppointments(appointments.filter((item) => item.id !== id));
        }
        message.success("Đã phê duyệt!");
      },
    });
  };

  const handleReject = (id, type) => {
    Modal.confirm({
      title: "Xác nhận từ chối?",
      onOk: () => {
        if (type === "listing") {
          setData(data.filter((item) => item.id !== id));
        } else {
          setAppointments(appointments.filter((item) => item.id !== id));
        }
        message.error("Đã từ chối!");
      },
    });
  };

  const columns = [
    { title: "Tiêu đề", dataIndex: "title", key: "title" },
    {
      title: "Trạng thái",
      dataIndex: "status",
      key: "status",
      render: () => <Tag color="orange">Chờ duyệt</Tag>,
    },
    {
      title: "Hành động",
      key: "actions",
      render: (_, record) => (
        <Space>
          <Button type="primary" icon={<CheckCircleOutlined />} onClick={() => handleApprove(record.id, "listing")}>
            Duyệt
          </Button>
          <Button danger icon={<CloseCircleOutlined />} onClick={() => handleReject(record.id, "listing")}>
            Từ chối
          </Button>
        </Space>
      ),
    },
  ];

  const appointmentColumns = [
    { title: "Khách hàng", dataIndex: "customer", key: "customer" },
    { title: "Ngày hẹn", dataIndex: "date", key: "date" },
    {
      title: "Trạng thái",
      dataIndex: "status",
      key: "status",
      render: () => <Tag color="orange">Chờ duyệt</Tag>,
    },
    {
      title: "Hành động",
      key: "actions",
      render: (_, record) => (
        <Space>
          <Button type="primary" icon={<CheckCircleOutlined />} onClick={() => handleApprove(record.id, "appointment")}>
            Duyệt
          </Button>
          <Button danger icon={<CloseCircleOutlined />} onClick={() => handleReject(record.id, "appointment")}>
            Từ chối
          </Button>
        </Space>
      ),
    },
  ];

  return (
    <div style={{ padding: 10 ,overflow: "auto",maxHeight:"88vh",    scrollbarWidth: "none",  msOverflowStyle: "none" }}>
      <Card>
        <Title level={2}>Phê duyệt bài đăng bất động sản</Title>
        <Table columns={columns} dataSource={data} rowKey="id" loading={loading} pagination={{ pageSize: 5 }} />
      </Card>
      <Card style={{ marginTop: 5 }}>
        <Title level={2}>Phê duyệt lịch hẹn</Title>
        <Table columns={appointmentColumns} dataSource={appointments} rowKey="id" loading={loading} pagination={{ pageSize: 5 }} />
      </Card>
    </div>
  );
};

export default ApprovalPage;
