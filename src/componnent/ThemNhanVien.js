import { Button, Card, Col, DatePicker, Form, Input, Row, Select, Space } from "antd";

const EmployeeForm = () => {
  return (
    <Card title={<Space><a href="#">Nhân Viên</a><Button type="primary">Thêm mới</Button></Space>}>
      <Form layout="vertical">
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item label="Mã Nhân Viên">
              <Input placeholder="Nhập mã nhân viên" />
            </Form.Item>
            <Form.Item label="Tên Nhân Viên">
              <Input placeholder="Nhập tên nhân viên" />
            </Form.Item>
            <Form.Item label="Ngày Sinh">
              <DatePicker style={{ width: "100%" }} />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="Chức Vụ">
              <Select placeholder="Chọn chức vụ">
                <Select.Option value="Nhân viên">Nhân viên</Select.Option>
                <Select.Option value="Quản lý">Quản lý</Select.Option>
              </Select>
            </Form.Item>
            <Form.Item label="Số Điện Thoại">
              <Input placeholder="Nhập số điện thoại" />
            </Form.Item>
            <Form.Item label="Địa Chỉ">
              <Input placeholder="Nhập địa chỉ" />
            </Form.Item>
          </Col>
        </Row>

        <Row justify="end">
          <Space>
            <Button>Hủy</Button>
            <Button type="primary">Lưu</Button>
          </Space>
        </Row>
      </Form>
    </Card>
  );
};

export default EmployeeForm;

