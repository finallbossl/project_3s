import React, { useState } from "react";
import { Input, List, Pagination, Button, Form, Upload, Modal } from "antd";
import { SearchOutlined ,EditOutlined, UploadOutlined } from "@ant-design/icons";
import { Link } from "react-router-dom";
const properties = [
  {
    id: 1,
    title: "Chung cư Đề Thám",
    uploaded: "3 months ago",
    image: "https://tse4.mm.bing.net/th?id=OIP.fXm4iCMgfoY_tAxXA0WVDgHaEK&pid=Api&P=0&h=180",
  },
  {
    id: 2,
    title: "Căn hộ The Grand Manhattan",
    uploaded: "2 months ago",
    image: "https://tse4.mm.bing.net/th?id=OIP.fXm4iCMgfoY_tAxXA0WVDgHaEK&pid=Api&P=0&h=180",
  },
  {
    id: 3,
    title: "Căn hộ chung cư Soho Residence",
    uploaded: "25 days ago",
    image: "https://tse4.mm.bing.net/th?id=OIP.fXm4iCMgfoY_tAxXA0WVDgHaEK&pid=Api&P=0&h=180",
  },
  {
    id: 4,
    title: "Căn hộ Vinhomes Ba Son",
    uploaded: "20 days ago",
    image: "https://tse4.mm.bing.net/th?id=OIP.fXm4iCMgfoY_tAxXA0WVDgHaEK&pid=Api&P=0&h=180",
  },
  {
    id: 5,
    title: "Vinhome Golden River",
    uploaded: "12 days ago",
    image: "https://tse4.mm.bing.net/th?id=OIP.fXm4iCMgfoY_tAxXA0WVDgHaEK&pid=Api&P=0&h=180",
  },
  {
    id: 6,
    title: "Vinhome Golden River",
    uploaded: "12 days ago",
    image: "https://tse4.mm.bing.net/th?id=OIP.fXm4iCMgfoY_tAxXA0WVDgHaEK&pid=Api&P=0&h=180",
  },
  {
    id: 7,
    title: "Vinhome Golden River",
    uploaded: "12 days ago",
    image: "https://tse4.mm.bing.net/th?id=OIP.fXm4iCMgfoY_tAxXA0WVDgHaEK&pid=Api&P=0&h=180",
  },
  {
    id: 5,
    title: "Vinhome Golden River",
    uploaded: "12 days ago",
    image: "https://tse4.mm.bing.net/th?id=OIP.fXm4iCMgfoY_tAxXA0WVDgHaEK&pid=Api&P=0&h=180",
  },
  {
    id: 5,
    title: "Vinhome Golden River",
    uploaded: "12 days ago",
    image: "https://tse4.mm.bing.net/th?id=OIP.fXm4iCMgfoY_tAxXA0WVDgHaEK&pid=Api&P=0&h=180",
  },
  {
    id: 5,
    title: "Vinhome Golden River",
    uploaded: "12 days ago",
    image: "https://tse4.mm.bing.net/th?id=OIP.fXm4iCMgfoY_tAxXA0WVDgHaEK&pid=Api&P=0&h=180",
  },
  
];

const ITEMS_PER_PAGE = 6;

const BatDongSan = () => {
  const [currentPage, setCurrentPage] = useState(1);
  const [searchTerm, setSearchTerm] = useState("");
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingProperty, setEditingProperty] = useState(null);
  const [form] = Form.useForm();

  const filteredProperties = properties.filter((property) =>
    property.title.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const paginatedProperties = filteredProperties.slice(
    (currentPage - 1) * ITEMS_PER_PAGE,
    currentPage * ITEMS_PER_PAGE
  );

  const handleEdit = (property) => {
    setEditingProperty(property);
    form.setFieldsValue({ ...property });
    setIsModalOpen(true);
  };

  const handleSave = (values) => {
    setCurrentPage((prev) =>
      prev.map((prop) => (prop.id === editingProperty.id ? { ...prop, ...values } : prop))
    );
    setIsModalOpen(false);
  };


  return (
    <div style={{ maxWidth: 1000, margin: "auto", padding: "20px" }}>
      <div style={{ display:"flex",flexDirection:"row"}}>
      <Input
        placeholder="Tìm kiếm"
        prefix={<SearchOutlined />}
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.target.value)}
        style={{ marginBottom: 16 }}
      />
      <Button type="primary" style={{marginLeft:"10px",backgroundColor:"#2c5282"}}>  <Link to="thembatdongsan">Thêm Mới</Link> </Button>
      </div>
      <List 
        itemLayout="horizontal"
        dataSource={paginatedProperties}
        renderItem={(item) => (
          <List.Item key={item.id} style={{backgroundColor:"#f7fafc", marginBottom:"10px",padding:"10px"}}>
            <List.Item.Meta
              avatar={
                <img
                  src={item.image}
                  alt={item.title}
                  style={{
                    width: 80,
                    borderRadius: 8,
                    border: "1px solid #ddd",
                  }}
                />
              }
              title={<a href="#">{item.title}</a>}
              description={`Cập nhật ${item.uploaded}`}
            />
              <Button icon={<EditOutlined />} onClick={() => handleEdit(item)} />
          </List.Item>
        )}
      />

     
      <Pagination 
      
        current={currentPage}
        pageSize={ITEMS_PER_PAGE}
        total={filteredProperties.length}
        onChange={(page) => setCurrentPage(page)}
        style={{ textAlign: "center", marginTop: 16, justifyContent:"center", }}
      />
       <Modal
        title="Chỉnh sửa bất động sản"
        open={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        onOk={() => form.submit()}
      >
        <Form form={form} onFinish={handleSave} layout="vertical">
          <Form.Item name="title" label="Tên" rules={[{ required: true, message: "Nhập tên!" }]}>
            <Input />
          </Form.Item>
          <Form.Item name="uploaded" label="Ngày cập nhật">
            <Input />
          </Form.Item>
          <Form.Item name="image" label="Hình ảnh">
            <Upload
              listType="picture"
              beforeUpload={() => false}
              maxCount={1}
            >
              <Button icon={<UploadOutlined />}>Tải ảnh lên</Button>
            </Upload>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default BatDongSan;
