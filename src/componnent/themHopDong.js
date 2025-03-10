import { Button, Card, Col, DatePicker, Form, Input, Row, Select, Space } from "antd";

const ContractForm = () => {
  return (
    <Card title={<Space><a href="#">Hợp Đồng</a><Button type="primary">Thêm mới</Button></Space>}>
      <Form layout="vertical">
        <Row gutter={16}>
          <Col span={12}>
            <Form.Item label="Id Hợp Đồng">
              <Input placeholder="Search..." />
            </Form.Item>
            <Form.Item label="Địa chỉ">
              <Input />
            </Form.Item>
            <Form.Item label="Ngày bắt đầu">
              <DatePicker style={{ width: "100%" }} />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="Bất động sản">
              <Select placeholder="Chọn bất động sản" />
            </Form.Item>
            <Form.Item label="Trạng thái hợp đồng">
              <Select defaultValue="Hoàn Thành">
                <Select.Option value="Hoàn Thành">Đã Hoàn Thành</Select.Option>
                <Select.Option value="Chờ Xác Nhận">Chờ Xác Nhận</Select.Option>
              </Select>
            </Form.Item>
          </Col>
        </Row>

        <Row gutter={16}>
          <Col span={12}>
            <h3>Khách Hàng</h3>
            <Form.Item label="Tên khách hàng">
              <Input />
            </Form.Item>
            <Form.Item label="Số điện thoại">
              <Input />
            </Form.Item>
            <Form.Item label="Địa chỉ">
              <Input />
            </Form.Item>
            <Form.Item label="Ngày sinh">
              <DatePicker style={{ width: "100%" }} />
            </Form.Item>
            <Form.Item label="Điều khoản bên A">
              <Input.TextArea rows={3} />
            </Form.Item>
          </Col>
          <Col span={12}>
            <h3>Người bán/thuê</h3>
            <Form.Item label="Tên khách hàng">
              <Input />
            </Form.Item>
            <Form.Item label="Số điện thoại">
              <Input />
            </Form.Item>
            <Form.Item label="Địa chỉ">
              <Input />
            </Form.Item>
            <Form.Item label="Ngày sinh">
              <DatePicker style={{ width: "100%" }} />
            </Form.Item>
            <Form.Item label="Điều khoản bên A">
              <Input.TextArea rows={3} />
            </Form.Item>
          </Col>
        </Row>

        <Row gutter={16}>
          <Col span={12}>
            <Form.Item label="Giá trị">
              <Input />
            </Form.Item>
            <Form.Item label="Nhân viên thực hiện">
              <Input />
            </Form.Item>
          </Col>
          <Col span={12}>
            <Form.Item label="Tình trạng thanh toán">
              <Input />
            </Form.Item>
          </Col>
        </Row>

        <Row justify="end">
          <Space>
            <Button>Hủy</Button>
            <Button type="primary">Đăng bài</Button>
          </Space>
        </Row>
      </Form>
    </Card>
  );
};

export default ContractForm;
