import React, { useState, useEffect } from "react";
import {
  Select,
  DatePicker,
  Input,
  List,
  Pagination,
  Button,
  Form,
  Upload,
  Modal,
  message,
  InputNumber,
} from "antd";
import {
  SearchOutlined,
  EditOutlined,
  UploadOutlined,
  EyeOutlined,
} from "@ant-design/icons";
import { Link } from "react-router-dom";
import moment from "moment";
import axios from "../Api";

const ITEMS_PER_PAGE = 6;

const BatDongSan = () => {
  const [properties, setProperties] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [searchTerm, setSearchTerm] = useState("");
  const [fileList, setFileList] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isDetailModalOpen, setIsDetailModalOpen] = useState(false);  // Modal xem chi tiết
  const [editingProperty, setEditingProperty] = useState(null);
  const [viewingProperty, setViewingProperty] = useState(null);  // Lưu bất động sản cần xem
  const [form] = Form.useForm();

  // Fetch danh sách bất động sản
  useEffect(() => {
    const fetchProperties = async () => {
      try {
        const response = await axios.get("/api/Realestate/get-all");
        setProperties(response.data);
      } catch (error) {
        const errorMessage = error.response
          ? error.response.data.message || "Không thể tải dữ liệu bất động sản."
          : "Lỗi mạng hoặc máy chủ.";
        console.error("Lỗi API:", error);
        message.error(errorMessage);
      }
    };
    fetchProperties();
  }, []);

  // Lọc và phân trang danh sách bất động sản
  const filteredProperties = properties.filter((property) =>
    property.realEstateName.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const paginatedProperties = filteredProperties.slice(
    (currentPage - 1) * ITEMS_PER_PAGE,
    currentPage * ITEMS_PER_PAGE
  );

  // Xử lý chỉnh sửa bất động sản
  const handleEdit = (property) => {
    setEditingProperty(property);
    form.setFieldsValue({
      realEstateName: property.realEstateName,
      realEstateType: property.realEstateType,
      realEstateStatus: property.realEstateStatus,
      price: property.price,
      seller: property.seller,
      saleDate: property.saleDate ? moment(property.saleDate) : null,
      coordinate: property.coordinate,
      address: property.address,
      description: property.description,
      area: property.area,  // Thêm area vào đây
      bedrooms: property.bedrooms,  // Thêm bedrooms
      bathrooms: property.bathrooms,  // Thêm bathrooms
      isFurnished: property.isFurnished,  // Thêm isFurnished
    });

    setFileList(
      property.imagePath
        ? [
            {
              uid: "-1",
              name: "image.png",
              status: "done",
              url: property.imagePath,
            },
          ]
        : []
    );

    setIsModalOpen(true);
  };

  // Lưu cập nhật bất động sản
  const handleSave = async (values) => {
    const formattedDate = values.saleDate
      ? values.saleDate.format("YYYY-MM-DD")
      : null;

    const data = {
      RealEstateName: values.realEstateName,
      RealEstateType: values.realEstateType,
      RealEstateStatus: values.realEstateStatus,
      ApprovalStatus: 2,
      Price: values.price,
      Seller: values.seller,
      Coordinate: values.coordinate || "", // Gửi giá trị rỗng nếu không có tọa độ
      Address: values.address,
      Description: values.description || "", // Gửi giá trị rỗng nếu không có mô tả
      SaleDate: formattedDate,
      Area: values.area || 0,  // Lưu diện tích
      Bedrooms: values.bedrooms || 0,  // Lưu số phòng ngủ
      Bathrooms: values.bathrooms || 0,  // Lưu số phòng tắm
      IsFurnished: values.isFurnished || "",  // Lưu thông tin có đồ đạc hay không
    };

    try {
      const formData = new FormData();
      Object.keys(data).forEach((key) => {
        if (data[key]) formData.append(key, data[key]);
      });
    
      if (fileList.length > 0) {
        formData.append("Image", fileList[0].originFileObj);
      }
    
      await axios.put(`/api/Realestate/${editingProperty.realEstateId}`, formData, {
        headers: { "Content-Type": "multipart/form-data" },
      });
    
      message.success("Cập nhật thành công!");
      setIsModalOpen(false);
    } catch (error) {
      console.error("Lỗi cập nhật:", error.response?.data);  // In ra lỗi chi tiết
      message.error(error.response?.data?.message || "Lỗi cập nhật bất động sản.");
    }
  };

  // Xử lý xem chi tiết bất động sản
  const handleViewDetails = (property) => {
    setViewingProperty(property);
    setIsDetailModalOpen(true);
  };

  return (
    <div style={{ maxWidth: 1000, margin: "auto", padding: "20px" }}>
      <div style={{ display: "flex", flexDirection: "row" }}>
        <Input
          placeholder="Tìm kiếm"
          prefix={<SearchOutlined />}
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
          style={{ marginBottom: 16 }}
        />
        <Button
          type="primary"
          style={{ marginLeft: "10px", backgroundColor: "#2c5282" }}
        >
          <Link to="thembatdongsan">Thêm Mới</Link>
        </Button>
      </div>

      <List
        itemLayout="vertical"
        dataSource={paginatedProperties}
        renderItem={(item) => (
          <List.Item
            key={item.realEstateId}
            style={{
              backgroundColor: "#ffffff",
              marginBottom: "12px",
              padding: "12px",
              borderRadius: "10px",
              boxShadow: "0px 4px 6px rgba(0, 0, 0, 0.1)",
              display: "flex",
              alignItems: "center",
              justifyContent: "space-between",
            }}
          >
            <div
              style={{
                display: "flex",
                alignItems: "center",
                flex: 1,
                gap: "15px",
              }}
            >
              <img
                src={item.imagePath || "placeholder-image-url"}
                alt={item.realEstateName}
                style={{
                  width: 100,
                  height: 100,
                  objectFit: "cover",
                  borderRadius: 8,
                  border: "1px solid #ddd",
                  boxShadow: "0px 2px 4px rgba(0, 0, 0, 0.1)",
                }}
              />
              <List.Item.Meta
                title={
                  <aa
                    href="#"
                    style={{
                      fontSize: "16px",
                      fontWeight: "bold",
                      color: "#2c5282",
                    }}
                  >
                    {item.realEstateName}
                  </aa>
                }
                description={
                  <>
                    <p style={{ margin: "5px 0", color: "#4a5568" }}>
                      <strong>Giá:</strong> {item.price?.toLocaleString()} VNĐ
                    </p>
                    <p style={{ margin: "5px 0", color: "#718096" }}>
                      <strong>Cập nhật:</strong>{" "}
                      {new Date(item.createdAt).toLocaleDateString("vi-VN")}
                    </p>
                  </>
                }
              />
            </div>
            <div>
              <Button
                icon={<EditOutlined />}
                onClick={() => handleEdit(item)}
                style={{
                  backgroundColor: "#3182ce",
                  color: "#fff",
                  border: "none",
                  borderRadius: "5px",
                  padding: "5px 10px",
                }}
              />
              <Button
                icon={<EyeOutlined />}
                onClick={() => handleViewDetails(item)}
                style={{
                  backgroundColor: "#38a169",
                  color: "#fff",
                  border: "none",
                  borderRadius: "5px",
                  padding: "5px 10px",
                  marginLeft: "10px",
                }}
              />
            </div>
          </List.Item>
        )}
      />

      <Pagination
        current={currentPage}
        pageSize={ITEMS_PER_PAGE}
        total={filteredProperties.length}
        onChange={(page) => setCurrentPage(page)}
        style={{ textAlign: "center", marginTop: 16 }}
      />

      {/* Modal Chỉnh Sửa */}
      <Modal
        title="Chỉnh sửa bất động sản"
        open={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        onOk={() => form.submit()}
      >
        <Form form={form} onFinish={handleSave} layout="vertical">
          <Form.Item
            name="realEstateName"
            label="Tên"
            rules={[{ required: true, message: "Nhập tên!" }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="realEstateType"
            label="Loại bất động sản"
            rules={[{ required: true, message: "Chọn loại bất động sản!" }]}
          >
            <Select>
              <Select.Option value={1}>Nhà</Select.Option>
              <Select.Option value={2}>Căn hộ</Select.Option>
              <Select.Option value={3}>Đất</Select.Option>
            </Select>
          </Form.Item>
          <Form.Item
            name="realEstateStatus"
            label="Trạng thái"
            rules={[{ required: true, message: "Chọn trạng thái!" }]}
          >
            <Select>
              <Select.Option value={1}>Có sẵn</Select.Option>
              <Select.Option value={2}>Đã bán</Select.Option>
            </Select>
          </Form.Item>
          <Form.Item
            name="price"
            label="Giá"
            rules={[{ required: true, message: "Nhập giá!" }]}
          >
            <InputNumber min={1} style={{ width: "100%" }} />
          </Form.Item>
          <Form.Item
            name="seller"
            label="Người bán"
            rules={[{ required: true, message: "Nhập người bán!" }]}
          >
            <InputNumber min={1} style={{ width: "100%" }} />
          </Form.Item>
          <Form.Item
            name="coordinate"
            label="Tọa độ"
            rules={[{ required: true, message: "Nhập tọa độ!" }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="address"
            label="Địa chỉ"
            rules={[{ required: true, message: "Nhập địa chỉ!" }]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="saleDate"
            label="Ngày bán"
            rules={[{ required: true, message: "Nhập ngày bán!" }]}
          >
            <DatePicker style={{ width: "100%" }} format="YYYY-MM-DD" />
          </Form.Item>
          <Form.Item
            name="area"
            label="Diện tích"
            rules={[{ required: true, message: "Nhập diện tích!" }]}
          >
            <InputNumber min={1} style={{ width: "100%" }} />
          </Form.Item>
          <Form.Item
            name="bedrooms"
            label="Số phòng ngủ"
            rules={[{ required: true, message: "Nhập số phòng ngủ!" }]}
          >
            <InputNumber min={1} style={{ width: "100%" }} />
          </Form.Item>
          <Form.Item
            name="bathrooms"
            label="Số phòng tắm"
            rules={[{ required: true, message: "Nhập số phòng tắm!" }]}
          >
            <InputNumber min={1} style={{ width: "100%" }} />
          </Form.Item>
          <Form.Item
            name="isFurnished"
            label="Đồ đạc"
            rules={[{ required: true, message: "Chọn có đồ đạc hay không!" }]}
          >
            <Select>
              <Select.Option value="Có">Có đồ đạc</Select.Option>
              <Select.Option value="Không">Không có đồ đạc</Select.Option>
            </Select>
          </Form.Item>
          <Form.Item
            name="description"
            label="Mô tả"
            rules={[{ required: true, message: "Nhập mô tả!" }]}
          >
            <Input.TextArea />
          </Form.Item>
          <Form.Item name="image" label="Ảnh" valuePropName="fileList">
            <Upload
              listType="picture"
              beforeUpload={() => false}
              fileList={fileList}
              onChange={({ fileList }) => setFileList(fileList)}
            >
              <Button icon={<UploadOutlined />}>Tải lên ảnh</Button>
            </Upload>
          </Form.Item>
        </Form>
      </Modal>

      {/* Modal Xem Chi Tiết */}
      <Modal
        title="Chi tiết bất động sản"
        open={isDetailModalOpen}
        onCancel={() => setIsDetailModalOpen(false)}
        footer={null}
      >
        {viewingProperty && (
          <div>
            <p>
              <strong>Tên:</strong> {viewingProperty.realEstateName}
            </p>
            <p>
              <strong>Loại:</strong>{" "}
              {viewingProperty.realEstateType === 1
                ? "Nhà"
                : viewingProperty.realEstateType === 2
                ? "Căn hộ"
                : "Đất"}
            </p>
            <p>
              <strong>Trạng thái:</strong>{" "}
              {viewingProperty.realEstateStatus === 1
                ? "Có sẵn"
                : "Đã bán"}
            </p>
            <p>
              <strong>Giá:</strong>{" "}
              {viewingProperty.price?.toLocaleString()} VNĐ
            </p>
            <p>
              <strong>Người bán:</strong> {viewingProperty.seller}
            </p>
            <p>
              <strong>Địa chỉ:</strong> {viewingProperty.address}
            </p>
            <p>
              <strong>Tọa độ:</strong> {viewingProperty.coordinate}
            </p>
            <p>
              <strong>Diện tích:</strong> {viewingProperty.area} m²
            </p>
            <p>
              <strong>Số phòng ngủ:</strong> {viewingProperty.bedrooms}
            </p>
            <p>
              <strong>Số phòng tắm:</strong> {viewingProperty.bathrooms}
            </p>
            <p>
              <strong>Đồ đạc:</strong> {viewingProperty.isFurnished}
            </p>
            <p>
              <strong>Ngày bán:</strong>{" "}
              {new Date(viewingProperty.saleDate).toLocaleDateString("vi-VN")}
            </p>
            <p>
              <strong>Mô tả:</strong> {viewingProperty.description}
            </p>
            <img
              src={viewingProperty.imagePath || "placeholder-image-url"}
              alt={viewingProperty.realEstateName}
              style={{
                width: "100%",
                height: "auto",
                objectFit: "cover",
                borderRadius: "8px",
                marginTop: "10px",
              }}
            />
          </div>
        )}
      </Modal>
    </div>
  );
};

export default BatDongSan;
