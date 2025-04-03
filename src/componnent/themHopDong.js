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
      clauseIds: values.clauseIds || [],
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
    console.log("Dữ liệu gửi lên API:", payload);
    try {
      await axios.post("/api/contracts", payload, {
        headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
      });
    

      message.success("Hợp đồng đã được tạo thành công!");
      form.resetFields();
    } catch (error) {
      console.error("Lỗi API:", error.response?.data);
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
            <Form.Item name="contractStatus" label="Trạng thái hợp đồng"> 
              <Select>
                <Select.Option value={1}>Đã hoàn thành</Select.Option>
                <Select.Option value={2}>Chờ xác nhận</Select.Option>
              </Select>
            </Form.Item>
            <Form.Item name="statusPayment" label="Tình trạng thanh toán"> 
              <Select>
                <Select.Option value={1}>Đã thanh toán</Select.Option>
                <Select.Option value={2}>Chưa thanh toán</Select.Option>
              </Select>
            </Form.Item>
            <Form.Item name="clauseIds" label="Điều khoản hợp đồng">
               <Select mode="multiple" placeholder="Chọn điều khoản">
               <Select.Option value={1}>Điều khoản 1</Select.Option>
                <Select.Option value={2}>Điều khoản 2</Select.Option>
               </Select>
              </Form.Item>

            <Row gutter={16}>
              <Col span={12}>
                <Form.Item name="startDate" label="Ngày bắt đầu">
                  <DatePicker style={{ width: "100%" }} />
                </Form.Item>
              </Col>
              <Col span={12}>
                <Form.Item name="endDate" label="Ngày kết thúc">
                  <DatePicker style={{ width: "100%" }} />
                </Form.Item>
              </Col>
            </Row>
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
                <Form.Item name="buyerPhoneNumber" label="Số điện thoại">
                  <Input />
                </Form.Item>
                <Form.Item name="buyerAddress" label="Địa chỉ">
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