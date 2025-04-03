import { Button, Card, Col, DatePicker, Form, Input, Row, Select, Space, message, Typography } from "antd";
import { useEffect, useState } from "react";
import axios from "../Api";
import dayjs from "dayjs";

const { Title } = Typography;

const ContractForm = () => {
  const [customers, setCustomers] = useState([]);
  const [properties, setProperties] = useState([]);
  const [form] = Form.useForm();

  useEffect(() => {
    fetchCustomers();
    fetchProperties();
  }, []);

  const fetchCustomers = async () => {
    try {
      const response = await axios.get(`/api/Customer/GetAll`, {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
      });
      setCustomers(response.data);
    } catch (error) {
      console.error("Lỗi API khách hàng:", error.response?.data || error.message);
    }
  };

  const fetchProperties = async () => {
    try {
      const response = await axios.get("/api/Realestate/get-all");
      setProperties(response.data);
    } catch (error) {
      console.error("Lỗi API bất động sản:", error);
    }
  };

  const handleSellerChange = (value) => {
    const selectedSeller = customers.find(c => c.customerId === value);
    if (selectedSeller) {
      form.setFieldsValue({
        sellerFullName: selectedSeller.fullName,
        sellerCCCD: selectedSeller.cccd,
        sellerPhoneNumber: selectedSeller.phoneNumber,
        sellerAddress: selectedSeller.address
      });
    }
  };

  const handleSubmit = async (values) => {
    const payload = {
      realEstateId: values.realEstateId,
      contractType: values.contractType,
      contractStatus: values.contractStatus,
      statusPayment: values.statusPayment,
      startDate: values.startDate ? values.startDate.format("YYYY-MM-DDTHH:mm:ss") : null,
      endDate: values.endDate ? values.endDate.format("YYYY-MM-DDTHH:mm:ss") : null,
      buyer: {
        fullName: values.buyerFullName || "",
        cccd: values.buyerCCCD || "",
        phoneNumber: values.buyerPhoneNumber || "",
        address: values.buyerAddress || "",
      },
      seller: {
        fullName: values.sellerFullName || "",
        cccd: values.sellerCCCD || "",
        phoneNumber: values.sellerPhoneNumber || "",
        address: values.sellerAddress || "",
      }
    };

    try {
      await axios.post("/api/contracts", payload, {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
      });
      message.success("Hợp đồng đã được tạo thành công!");
      form.resetFields();
    } catch (error) {
      message.error(`Lỗi: ${error.response?.data?.title || "Có lỗi xảy ra"}`);
    }
  };

  return (
    <Card style={{ maxHeight: "88vh", overflow: "auto"}} title={<Title level={3}>Quản lý Hợp Đồng</Title>}>
      <Form layout="vertical" form={form} onFinish={handleSubmit}>
        <Row gutter={24}>
          <Col span={8}>
            <Title level={4}>Thông tin hợp đồng</Title>
            <Form.Item name="realEstateId" label="Bất động sản" rules={[{ required: true }]}> 
              <Select placeholder="Chọn bất động sản" options={properties.map(p => ({ value: p.realEstateId, label: p.realEstateName }))} />
            </Form.Item>
            <Form.Item name="contractType" label="Loại hợp đồng" rules={[{ required: true }]}> 
              <Select>
                <Select.Option value={1}>Mua bán</Select.Option>
                <Select.Option value={2}>Thuê</Select.Option>
              </Select>
            </Form.Item>
          </Col>

          <Col span={16}>
            <Row gutter={24}>
              <Col span={12}>
                <Title level={4}>Thông tin người mua</Title>
                <Form.Item name="buyerFullName" label="Họ và tên">
                  <Input />
                </Form.Item>
                <Form.Item name="buyerCCCD" label="CCCD">
                  <Input />
                </Form.Item>
              </Col>
              
              <Col span={12}>
                <Title level={4}>Thông tin người bán</Title>
                <Form.Item name="sellerId" label="Chọn người bán">
                  <Select 
                    placeholder="Chọn người bán" 
                    options={customers.map(c => ({ value: c.customerId, label: c.fullName }))}
                    onChange={handleSellerChange}
                  />
                </Form.Item>
                <Form.Item name="sellerFullName" label="Họ và tên">
                  <Input readOnly />
                </Form.Item>
                <Form.Item name="sellerCCCD" label="CCCD">
                  <Input readOnly />
                </Form.Item>
                <Form.Item name="sellerPhoneNumber" label="Số điện thoại">
                  <Input readOnly />
                </Form.Item>
                <Form.Item name="sellerAddress" label="Địa chỉ">
                  <Input readOnly />
                </Form.Item>
              </Col>
            </Row>
          </Col>
        </Row>

        <Row justify="end">
          <Space>
            <Button htmlType="reset">Hủy</Button>
            <Button type="primary" htmlType="submit">Tạo hợp đồng</Button>
          </Space>
        </Row>
      </Form>
    </Card>
  );
};

export default ContractForm;
