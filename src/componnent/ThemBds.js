import React, { useState } from "react";
import {
  Input,
  Select,
  Button,
  Upload,
  Form,
  DatePicker,
  message,
  Card,
} from "antd";
import { UploadOutlined } from "@ant-design/icons";
import axios from "../Api";
import { MapContainer, TileLayer, Marker, useMapEvents ,Popup } from "react-leaflet";
import L from "leaflet";
import "leaflet/dist/leaflet.css";


const { Option } = Select;

const LocationPicker = ({ onSelect }) => {
  const [position, setPosition] = useState(null);

  useMapEvents({
    click(e) {
      const { lat, lng } = e.latlng;
      setPosition([lat, lng]);
      onSelect({ lat, lng }); // Gửi đối tượng với lat, lng
    },
  });

  return position ? <Marker position={position}><Popup>Tọa độ: {position[0]}, {position[1]}</Popup></Marker> : null;
};

const BatDongSanCreate = () => {
  const [form] = Form.useForm();
  const [image, setImage] = useState(null);
  const [file, setFile] = useState(null);
  const [coordinate, setCoordinate] = useState("");

  const handleImageUpload = (info) => {
    if (info.file.status !== "uploading") {
      const selectedFile = info.file;
      if (selectedFile instanceof Blob) {
        setFile(selectedFile);
        setImage(URL.createObjectURL(selectedFile));
      } else {
        message.error("Tệp tải lên không hợp lệ!");
      }
    }
  };
  const defaultIcon = new L.Icon({
    iconUrl: "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-icon.png", // Đường dẫn tới biểu tượng mặc định
    iconSize: [25, 41], // Kích thước của biểu tượng
    iconAnchor: [12, 41], // Điểm neo của biểu tượng (phần dưới cùng)
    popupAnchor: [1, -34], // Vị trí của popup so với biểu tượng
  }); 

  const handleSubmit = async (values) => {
    const formData = new FormData();
  
    // Chuyển đổi SaleDate
    if (values.SaleDate) {
      formData.append("SaleDate", values.SaleDate.format("YYYY-MM-DD"));
    }
  
    // Kiểm tra và xử lý tọa độ
    if (coordinate) {
      formData.append("Coordinate", `${coordinate.lat}, ${coordinate.lng}`);
    } else {
      message.error("Vui lòng chọn tọa độ trên bản đồ!");
      return;
    }
  
    // Chuyển đổi các giá trị cần thiết
    Object.keys(values).forEach((key) => {
      if (values[key] && key !== "SaleDate" && key !== "Coordinate") {
        formData.append(
          key,
          typeof values[key] === "number" ? values[key].toString() : values[key]
        );
      }
    });
  
    // Thêm hình ảnh
    if (file) {
      formData.append("ImagePath", file);
    }
  
    try {
      const response = await axios.post("/api/Realestate", formData, {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      });
  
      console.log("API Response:", response.data);
      message.success("Tạo mới bất động sản thành công!");
      form.resetFields();
      setImage(null);
      setFile(null);
    } catch (error) {
      console.error("Lỗi API:", error.response ? error.response.data : error.message);
      message.error(`Lỗi: ${error.response?.data?.message || "Không xác định"}`);
    }
  };
  

  return (
    <div style={{ maxWidth: 1300, padding: 0 ,maxHeight: "88vh", overflow: "auto"}}>
      <Card>
        <Form form={form} onFinish={handleSubmit} layout="vertical">
          <div style={{ display: "flex", gap: 20 }}>
            <div style={{ flex: 1 }}>
              <Form.Item
                name="RealEstateName"
                label="Tên bất động sản"
                rules={[{ required: true }]}
              >
                <Input placeholder="Nhập tên" />
              </Form.Item>

              <Form.Item
                name="RealEstateType"
                label="Loại bất động sản"
                rules={[{ required: true }]}
              >
                <Select placeholder="Chọn loại">
                  <Option value="0">Căn hộ</Option>
                  <Option value="1">Nhà riêng</Option>
                </Select>
              </Form.Item>

              <Form.Item name="Price" label="Giá" rules={[{ required: true }]}>
                <Input type="number" placeholder="Nhập giá" />
              </Form.Item>

              <Form.Item
                name="Seller"
                label="Mã người bán"
                rules={[{ required: true }]}
              >
                <Input type="number" placeholder="Nhập mã người bán" />
              </Form.Item>
              <Form.Item
                name="RealEstateStatus"
                label="Trạng thái"
                rules={[{ required: true }]}
              >
                <Select placeholder="Chọn trạng thái">
                  <Option value="1">Đang bán</Option>
                  <Option value="2">Đã bán</Option>
                </Select>
              </Form.Item>
            </div>

            <div style={{ flex: 1 }}>
              <Form.Item
                name="Address"
                label="Địa chỉ"
                rules={[{ required: true }]}
              >
                <Input placeholder="Nhập địa chỉ" />
              </Form.Item>

              {/* Bản đồ chọn tọa độ */}
              {/* Bản đồ chọn tọa độ */}
              <Form.Item name="Coordinate" label="Tọa độ">
                <Input
                  value={coordinate ? `${coordinate.lat}, ${coordinate.lng}` : ""}
                  placeholder="Nhấp vào bản đồ để chọn"
                  readOnly
                />
              </Form.Item>
              <div
                style={{
                  border: "1px solid #ddd",
                  borderRadius: 8,
                  overflow: "hidden",
                }}
              >
                <MapContainer
                  center={[21.0285, 105.8542]}
                  zoom={13}
                  style={{ height: 300, width: "100%" }}
                >
                  <TileLayer
                    attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                  />
                  <LocationPicker onSelect={setCoordinate} />
                  {coordinate && (
                    <Marker position={coordinate} icon={defaultIcon}>
                      <Popup>
                        Tọa độ: {coordinate.lat}, {coordinate.lng}
                      </Popup>
                    </Marker>
                  )}
                </MapContainer>
              </div>
            </div>
          </div>

          <div style={{ display: "flex", gap: 20, marginTop: 20 }}>
            <div style={{ flex: 1 }}>
              <Form.Item
                name="ApprovalStatus"
                label="Trạng thái phê duyệt"
                rules={[
                  {
                    required: true,
                    message: "Vui lòng chọn trạng thái phê duyệt!",
                  },
                ]}
              >
                <Select placeholder="Chọn trạng thái">
                  <Option value={1}>Chờ duyệt</Option>
                  
                </Select>
              </Form.Item>

              <Form.Item name="SaleDate" label="Ngày đăng bán">
                <DatePicker style={{ width: "100%" }} />
              </Form.Item>

              <Form.Item name="Area" label="Diện tích (m²)">
                <Input type="number" placeholder="Nhập diện tích" />
              </Form.Item>
            </div>

            <div style={{ flex: 1 }}>
              <Form.Item name="Bedrooms" label="Số phòng ngủ">
                <Input type="number" placeholder="Nhập số phòng ngủ" />
              </Form.Item>

              <Form.Item name="Bathrooms" label="Số phòng tắm">
                <Input type="number" placeholder="Nhập số phòng tắm" />
              </Form.Item>

              <Form.Item name="IsFurnished" label="Có nội thất">
                <Select placeholder="Chọn">
                  <Option value="true">Có</Option>
                  <Option value="false">Không</Option>
                </Select>
              </Form.Item>
            </div>
          </div>

          {/* Ảnh đại diện */}
          <div style={{ textAlign: "center", marginTop: 20 }}>
            <div
              style={{
                border: "1px solid #ddd",
                padding: 10,
                borderRadius: 8,
                display: "inline-block",
              }}
            >
              {image ? (
                <img
                  src={image}
                  alt="Uploaded"
                  style={{
                    width: 200,
                    height: 150,
                    objectFit: "cover",
                    borderRadius: 8,
                  }}
                />
              ) : (
                <div
                  style={{
                    width: 200,
                    height: 150,
                    backgroundColor: "#f7f7f7",
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "center",
                    borderRadius: 8,
                  }}
                >
                  Chưa có ảnh
                </div>
              )}
              <Upload
                beforeUpload={() => false}
                onChange={handleImageUpload}
                showUploadList={false}
              >
                <Button icon={<UploadOutlined />}>Thêm hình ảnh</Button>
              </Upload>
            </div>
          </div>

          <div style={{ textAlign: "center", marginTop: 20 }}>
            <Button
              type="primary"
              htmlType="submit"
              style={{
                backgroundColor: "#2c5282",
                fontSize: 16,
                padding: "10px 40px",
              }}
            >
              Tạo mới
            </Button>
          </div>
        </Form>
      </Card>
    </div>
  );
};

export default BatDongSanCreate;
