import React, { useEffect, useState } from "react";
import { Table, Button, Modal, Form, Input, Select, message, Layout, Card } from "antd";
import { PlusOutlined } from "@ant-design/icons";
import Api from "../Api"; 


const { Option } = Select;
const { Content } = Layout;

const EmployeeManagement = () => {
  const [employees, setEmployees] = useState([]);
  const [loading, setLoading] = useState(false);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [form] = Form.useForm();

  const [currentUserRole, setCurrentUserRole] = useState(null);

useEffect(() => {
  const role = localStorage.getItem("role");
  setCurrentUserRole(role);
    fetchEmployees();
  }, []);

  const fetchEmployees = async () => {
    setLoading(true);
    try {
        const response = await Api.get(`api/admin/danhsachtaikhoan`, {
          headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
        });
        setEmployees(response.data);
      } catch (error) {
        console.error("Lỗi API:", error);
        console.error("Chi tiết lỗi:", error.response?.data || error.message);
      } finally {
        setLoading(false);
      }
  };

  // 🛠 Xử lý tạo tài khoản mới
  const handleCreateEmployee = async (values) => {
    try {
      await Api.post(`/api/admin/taotaikhoan`, values);
      message.success("Tạo tài khoản thành công!");
      setIsModalOpen(false);
      form.resetFields();
      fetchEmployees(); // Load lại danh sách nhân viên
    } catch (error) {
      message.error("Tạo tài khoản thất bại!");
    }
  };

  // 🎨 Cấu hình cột bảng nhân viên
  const columns = [
    { title: "ID", dataIndex: "id", key: "id" },
    { title: "Họ và Tên", dataIndex: "fullName", key: "fullName" },
    { title: "Email", dataIndex: "email", key: "email" },
    { title: "Vai trò", dataIndex: "role", key: "role" },
    { title: "Ngày tạo", dataIndex: "createdAt", key: "createdAt" },
  ];

  return (
    <Layout style={{ }}>
      <Content>
        <Card title="Quản lý nhân viên" extra={
            currentUserRole === "Admin" && (
              <Button
                type="primary"
                icon={<PlusOutlined />}
                onClick={() => setIsModalOpen(true)}
              >
                Thêm tài khoản
              </Button>
            )
        }>
          <Table dataSource={employees} columns={columns} loading={loading} rowKey="id" />
        </Card>
      </Content>

      {/* 🛠 Modal Tạo tài khoản */}
      <Modal
        title="Tạo tài khoản mới"
        open={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        footer={null}
      >
        <Form form={form} layout="vertical" onFinish={handleCreateEmployee}>
          <Form.Item name="fullName" label="Họ và Tên" rules={[{ required: true, message: "Nhập họ và tên!" }]}>
            <Input placeholder="Nhập họ và tên" />
          </Form.Item>
          <Form.Item name="email" label="Email" rules={[{ required: true, type: "email", message: "Nhập email hợp lệ!" }]}>
            <Input placeholder="Nhập email" />
          </Form.Item>
          <Form.Item name="role" label="Vai trò" rules={[{ required: true, message: "Chọn vai trò!" }]}>
            <Select placeholder="Chọn vai trò">
              <Option value="Admin">Admin</Option>
              <Option value="Nhân viên">Nhân viên</Option>
            </Select>
          </Form.Item>
          <Form.Item>
            <Button type="primary" htmlType="submit">Tạo tài khoản</Button>
          </Form.Item>
        </Form>
      </Modal>
    </Layout>
  );
};

export default EmployeeManagement;
