import React, { useEffect, useState } from "react";
import { Form, Input, Select, Button, DatePicker, Row, Col, Card, List, message } from "antd";
import axios from "../Api";

const { Option } = Select;

const ScheduleForm = () => {
  const [form] = Form.useForm();
  const [submittedData, setSubmittedData] = useState([]);
  const [loading, setLoading] = useState(false);
  const [employees, setEmployees] = useState([]);
  const [appointments, setAppointments] = useState([]);
  const [customers, setCustomers] = useState([]);
  const [createNewAppointment, setCreateNewAppointment] = useState(false);

  useEffect(() => {
    fetchAppointments();
    fetchEmployees();
    fetchCustomers();
    fetchMappings();
  }, []);

  const fetchAppointments = async () => {
    try {
      const response = await axios.get(`/api/Appointment`, {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
      });
      setAppointments(response.data);
    } catch (error) {
      console.error("Lỗi API:", error);
      console.error("Chi tiết lỗi:", error.response?.data || error.message);
    }
  };

  const fetchEmployees = async () => {
    try {
      const response = await axios.get(`/api/admin/danhsachtaikhoan`, {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
      });
      setEmployees(response.data);
    } catch (error) {
      console.error("Lỗi API:", error);
      console.error("Chi tiết lỗi:", error.response?.data || error.message);
    }
  };

  const fetchCustomers = async () => {
    try {
      const response = await axios.get(`/api/Customer/GetAll`, {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
      });
      setCustomers(response.data);
    } catch (error) {
      console.error("Lỗi API:", error);
      console.error("Chi tiết lỗi:", error.response?.data || error.message);
    }
  };

  const fetchMappings = async () => {
    try {
      const response = await axios.get(`/api/MappingUserAppointment`);
      setSubmittedData(response.data);
    } catch (error) {
      message.error("Lỗi khi tải danh sách công việc");
    }
  };

  const handleSubmit = async (values) => {
    console.log("Dữ liệu customerId gửi đi:", values.customer); // Kiểm tra customerId
    
    if (!values.customer) {
      message.error("Vui lòng chọn khách hàng hợp lệ!");
      return;
    }
    
    try {
      setLoading(true);
      const requestData = {
        userId: values.employee,
        title: createNewAppointment ? values.newAppointmentTitle : "",
        customerId: values.customer, // Kiểm tra giá trị này
        description: createNewAppointment ? values.newAppointmentDescription : "",
        appointmentDate: createNewAppointment ? values.newAppointmentDate?.format("YYYY-MM-DD") : null,
        address: createNewAppointment ? values.newAppointmentAddress : "",
      };
  
      console.log("Dữ liệu gửi lên API:", requestData); // Kiểm tra dữ liệu cuối cùng
  
      const response = await axios.post(`/api/MappingUserAppointment`, requestData, {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
      });
  
      setSubmittedData([...submittedData, response.data]);
      form.resetFields();
      setCreateNewAppointment(false);
      message.success("Lưu thành công");
    } catch (error) {
      console.error("Lỗi khi lưu dữ liệu:", error);
      console.error("Chi tiết lỗi:", error.response?.data || error.message);
      message.error("Lỗi khi lưu dữ liệu: " + (error.response?.data?.message || error.message));
    } finally {
      setLoading(false);
    }
  };
  
 

  return (
    <div style={{ maxHeight: "500px", overflow: "auto" }}>
      <Form form={form} layout="vertical" onFinish={handleSubmit}>
        <Row gutter={16}>
          <Col span={8}>
            <Form.Item name="appointment" label="Lịch hẹn" rules={[{ required: true }]}>
              <Select placeholder="Chọn lịch hẹn hoặc tạo mới" onChange={(value) => setCreateNewAppointment(value === "new")}>
                <Option value="new">Tạo lịch hẹn mới</Option>
                {appointments.map((appt) => (
                  <Option key={appt.id} value={appt.title}>{appt.title}</Option>
                ))}
              </Select>
            </Form.Item>
          </Col>

          <Col span={8}>
            <Form.Item name="customer" label="Khách hàng" rules={[{ required: true }]}>
              <Select placeholder="Chọn khách hàng">
                {customers.map((cust) => (
                  <Option key={cust.customerId} value={cust.customerId}>{cust.fullName}</Option>
                ))}
              </Select>
            </Form.Item>
          </Col>

          <Col span={8}>
            <Form.Item name="employee" label="Nhân viên thực hiện" rules={[{ required: true }]}>
              <Select placeholder="Chọn nhân viên">
                {employees.map((emp) => (
                  <Option key={emp.id} value={emp.id}>{emp.fullName}</Option>
                ))}
              </Select>
            </Form.Item>
          </Col>
        </Row>

        {createNewAppointment && (
          <>
            <Row>
              <Col span={12}>
                <Form.Item name="newAppointmentTitle" label="Tiêu đề lịch hẹn" rules={[{ required: true }]}>
                  <Input placeholder="Nhập tiêu đề lịch hẹn" />
                </Form.Item>
              </Col>
            </Row>

            <Row>
              <Col span={12}>
                <Form.Item name="newAppointmentDescription" label="Mô tả">
                  <Input.TextArea placeholder="Nhập mô tả lịch hẹn" />
                </Form.Item>
              </Col>
            </Row>

            <Row>
              <Col span={12}>
                <Form.Item name="newAppointmentDate" label="Ngày hẹn" rules={[{ required: true }]}>
                  <DatePicker format="YYYY-MM-DD" style={{ width: "100%" }} />
                </Form.Item>
              </Col>
            </Row>

            <Row>
              <Col span={12}>
                <Form.Item name="newAppointmentAddress" label="Địa chỉ">
                  <Input placeholder="Nhập địa chỉ lịch hẹn" />
                </Form.Item>
              </Col>
            </Row>
          </>
        )}

        <Row>
          <Col span={6}>
            <Button type="primary" htmlType="submit" loading={loading}>Lưu</Button>
          </Col>
        </Row>
      </Form>

      <Card title="Danh sách công việc đã lưu" style={{ marginTop: 10 }}>
        {submittedData.length > 0 ? (
          <List
            dataSource={submittedData}
            renderItem={(item, index) => (
              <List.Item>
                <p><strong>{index + 1}. Công việc:</strong> {item.title}</p>
                <p><strong>Khách hàng:</strong> {item.customerName}</p>
                <p><strong>Nhân viên:</strong> {item.fullName}</p>
                <p><strong>Ngày hẹn:</strong> {item.appointmentDate}</p>
              </List.Item>
            )}
          />
        ) : <p>Không có dữ liệu</p>}
      </Card>
    </div>
  );
};

export default ScheduleForm;
