import React, { useEffect, useState } from "react";
import { Form, Input, Select, Button, Row, Col, Card, List } from "antd";

const { Option } = Select;

const ScheduleForm = ({ initialData }) => {
  const [form] = Form.useForm();
  const [submittedData, setSubmittedData] = useState([]);

  useEffect(() => {
    if (initialData) {
      form.setFieldsValue({
        task: initialData.task,
        customer: initialData.person,
        employees: [initialData.person],
        content: initialData.comment,
        ...initialData.weeklyHours,
      });
    }
  }, [initialData, form]);

  const handleSubmit = (values) => {
    console.log("Submitted: ", values);
    setSubmittedData([...submittedData, { ...values, status: "Đang duyệt" }]);
  };

  return (
    <div style={{ overflow: "auto", padding: -10 ,maxHeight:"440px",scrollbarWidth: "none",  msOverflowStyle: "none" }}>
      <Form form={form} layout="vertical" onFinish={handleSubmit}>
        <Row gutter={16}>
          <Col span={6}>
            <Form.Item name="task" label="Công việc" rules={[{ required: true }]}>            
              <Select placeholder="Chọn công việc">
                <Option value="ky-hop-dong">Ký hợp đồng</Option>
                <Option value="bao-gia">Báo giá</Option>
              </Select>
            </Form.Item>
          </Col>
          <Col span={6}>
            <Form.Item name="customer" label="Khách hàng" rules={[{ required: true }]}>            
              <Input placeholder="Nhập tên khách hàng" />
            </Form.Item>
          </Col>
          <Col span={6}>
            <Form.Item name="employees" label="Nhân viên thực hiện" rules={[{ required: true }]}>            
              <Select mode="multiple" placeholder="Chọn nhân viên">
                <Option value="nhan-vien-1">Nhân viên 1</Option>
                <Option value="nhan-vien-2">Nhân viên 2</Option>
              </Select>
            </Form.Item>
          </Col>
        </Row>
        <Row>
          <Col span={12}>
            <Form.Item name="content" label="Nội dung">            
              <Input.TextArea placeholder="Nhập nội dung công việc" />
            </Form.Item>
          </Col>
        </Row>
        <Row gutter={8}>
          {["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"].map((day, index) => (
            <Col key={day} span={3}>
              <Form.Item name={day.toLowerCase()} label={`${15 + index} / ${day}`}>
                <Input placeholder="0.00" />
              </Form.Item>
            </Col>
          ))}
        </Row>
        <Row>
          <Col span={6}>
            <Button type="primary" htmlType="submit">Lưu</Button>
            <Button danger style={{ marginLeft: 10 }}>Xóa</Button>
          </Col>
        </Row>
      </Form>
      <Card title="Danh sách công việc đã lưu" style={{ marginTop: 10 }}>
        {submittedData.length > 0 ? (
          <List
            dataSource={submittedData}
            renderItem={(item, index) => (
              <List.Item>
                <p><strong>#{index + 1} Công việc:</strong> {item.task}</p>
                <p><strong>Khách hàng:</strong> {item.customer}</p>
                <p><strong>Nhân viên:</strong> {item.employees.join(", ")}</p>
                <p><strong>Nội dung:</strong> {item.content}</p>
                <p><strong>Trạng thái:</strong> {item.status}</p>
              </List.Item>
            )}
          />
        ) : (
          <p>Không có dữ liệu</p>
        )}
      </Card>
    </div>
  );
};

export default ScheduleForm;
