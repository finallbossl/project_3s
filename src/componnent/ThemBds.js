import React, { useState } from "react";
import { Input, Select, Button, Upload, Form, DatePicker } from "antd";
import { UploadOutlined } from "@ant-design/icons";

const { Option } = Select;

const BatDongSanUpdate = () => {
  const [form] = Form.useForm();
  const [image, setImage] = useState(null);

  const handleImageUpload = (info) => {
    if (info.file.status === 'done') {
      setImage(URL.createObjectURL(info.file.originFileObj));
    }
  };

  const handleSubmit = (values) => {
    console.log("Submitted Data:", values);
  };

  return (
    <div style={{ maxWidth: 1000, margin: "auto", padding: 20 }}>
      <Button type="primary" style={{ marginBottom: 16 ,backgroundColor:"#2c5282"}}>Cập nhật mới</Button>
      <Form form={form} onFinish={handleSubmit} layout="vertical">
        <div style={{ display: "flex", gap: 20 }}>
          <div style={{ flex: 1 }}>
            <Form.Item name="title" label="Tên bất động sản">
              <Input placeholder="Thông Tin" style={{ backgroundColor:"#f1f1f1" }}/>
            </Form.Item>
            <Form.Item name="category" label="Thể loại">
              <Input placeholder="Thông Tin" style={{ backgroundColor:"#f1f1f1" }}/>
            </Form.Item>
            <Form.Item name="description" label="Mô tả">
              <Input placeholder="Thông Tin" style={{ backgroundColor:"#f1f1f1" }}/>
            </Form.Item>
            <Form.Item name="price" label="Giá">
              <Input placeholder="Nhập giá" style={{ backgroundColor:"#f1f1f1" }}/>
            </Form.Item>
            <Form.Item name="area" label="Khu vực" >
            <Input placeholder="Thông Tin" style={{ backgroundColor:"#f1f1f1" }}/>
            </Form.Item>
            <Form.Item name="agent" label="Người bán">
              <Input placeholder="Thông Tin" style={{ backgroundColor:"#f1f1f1" }}/>
               
            </Form.Item>
          </div>
          <div style={{ flex: 1 }}>
            <Form.Item name="location" label="Vị trí">
            <Input placeholder="Thông Tin" style={{ backgroundColor:"#f1f1f1" }}/>
            </Form.Item>
            <Form.Item name="status" label="Trạng Thái">
              <Select placeholder="Chọn trạng thái" style={{ backgroundColor:"#f1f1f1" }}>
                <Option value="available">Đang bán</Option>
                <Option value="sold">Đã bán</Option>
              </Select>
            </Form.Item>
            <Form.Item name="postedDate" label="Ngày đăng bán">
              <DatePicker style={{ width: "100%" }} />
            </Form.Item>
            <div style={{ textAlign: "center", border: "1px solid #ddd", padding: 10, borderRadius: 8, display: "inline-block",marginLeft:120 }}>
              {image ? (
                <img src={image} alt="Uploaded" style={{ width: 200, height: 150, objectFit: "cover", borderRadius: 8, marginBottom: 10 }} />
              ) : (
                <div style={{ width: 200, height: 150, backgroundColor: "#f7f7f7", display: "flex", alignItems: "center", justifyContent: "center", borderRadius: 8 }}>Chưa có ảnh</div>
              )}
              <Upload beforeUpload={() => false} onChange={handleImageUpload} showUploadList={false}>
                <Button icon={<UploadOutlined />}>Thêm image</Button>
              </Upload>
            </div>
          </div>
        </div>
        <div style={{ display: "flex", justifyContent: "space-between", marginTop: 20 }}>
          <Button > Xóa </Button>
          <Button type="primary" htmlType="submit"  style={{ backgroundColor:"#2c5282"}}> Đăng bài </Button>
        </div>
      </Form>
    </div>
  );
};

export default BatDongSanUpdate;
