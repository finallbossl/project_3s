import React, { useEffect, useState } from "react";
import { List, Tag, Button, Pagination, Spin, message, Modal, Form, Select } from "antd";
import { PlusOutlined, MoreOutlined, DeleteOutlined, EditOutlined, ExclamationCircleOutlined } from "@ant-design/icons";
import { Link } from "react-router-dom";
import api from "../Api";

const { confirm } = Modal;
const { Option } = Select;
const pageSize = 4;

const ContractList = () => {
  const [contracts, setContracts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingContract, setEditingContract] = useState(null);
  const [form] = Form.useForm();

  useEffect(() => {
    fetchContracts();
  }, []);

  const fetchContracts = async () => {
    try {
      setLoading(true);
      const response = await api.get("/api/contracts/get-all-contracts");
      setContracts(response.data);
    } catch (error) {
      message.error("Không thể tải danh sách hợp đồng!");
    } finally {
      setLoading(false);
    }
  };

  const handleDeleteContract = async (contractId) => {
    confirm({
      title: "Bạn có chắc chắn muốn xóa hợp đồng này?",
      icon: <ExclamationCircleOutlined />, 
      content: "Hành động này không thể hoàn tác!",
      okText: "Xóa",
      okType: "danger",
      cancelText: "Hủy",
      async onOk() {
        try {
          await api.delete(`/api/contracts/${contractId}`);
          message.success("Hợp đồng đã được xoá!");
          fetchContracts();
        } catch (error) {
          message.error("Không thể xoá hợp đồng, vui lòng thử lại!");
        }
      },
    });
  };

  const showEditModal = (contract) => {
    setEditingContract(contract);
    form.setFieldsValue({
      contractStatus: contract.contractStatus,
      contractType: contract.contractType,
      statusPayment: contract.statusPayment,
      clauseIds: contract.clauseIds,
    });
    setIsModalOpen(true);
  };

  const handleUpdateContract = async (values) => {
    const payload = {
      contractStatus: values.contractStatus,
      contractType: values.contractType,
      statusPayment: values.statusPayment,
      clauseIds: values.clauseIds,
    };

    try {
      await api.put(`/api/contracts/${editingContract.contractId}`, payload, {
              headers: { Authorization: `Bearer ${localStorage.getItem("token")}` }
            });
            message.success("Hợp đồng đã được tạo thành công!");
            form.resetFields();
          } catch (error) {
            message.error(`Lỗi: ${error.response?.data?.title || "Có lỗi xảy ra"}`);
          }
        };

  const startIndex = (currentPage - 1) * pageSize;
  const currentData = contracts.slice(startIndex, startIndex + pageSize);

  return (
    <div style={{ padding: 20 }}>
      <Button type="primary" icon={<PlusOutlined />} style={{ marginBottom: 16, backgroundColor: "#2c5282" }}>
        <Link to="themhopdong" style={{ color: "white" }}>Thêm Mới</Link>
      </Button>

      {loading ? (
        <Spin tip="Đang tải dữ liệu..." style={{ display: "block", textAlign: "center", marginTop: 50 }} />
      ) : (
        <>
          <List
            dataSource={currentData}
            renderItem={(item) => (
              <List.Item style={{ borderBottom: "1px solid #ddd", padding: "12px 16px" }}>
                <List.Item.Meta
                  title={<strong>Hợp đồng #{item.contractId}</strong>}
                  description={
                    <>
                      <p><strong>Ngày tạo:</strong> {new Date(item.createdAt).toLocaleDateString()}</p>
                      <p><strong>Người tạo:</strong> {item.createdBy}</p>
                      <p><strong>Loại hợp đồng:</strong> {item.contractType}</p>
                      <p><strong>Trạng thái:</strong> {item.contractStatus}</p>
                    </>
                  }
                />
                <Tag color={item.contractStatus === "Completed" ? "green" : "orange"}>
                  {item.contractStatus === "Completed" ? "Hoàn Thành" : "Đang Xử Lý"}
                </Tag>
                <Button icon={<EditOutlined />} size="small" style={{ marginLeft: 8 }} onClick={() => showEditModal(item)} />
                <Button icon={<MoreOutlined />} size="small" style={{ marginLeft: 8 }} />
                <Button
                  icon={<DeleteOutlined />}
                  size="small"
                  danger
                  style={{ marginLeft: 8 }}
                  onClick={() => handleDeleteContract(item.contractId)}
                />
              </List.Item>
            )}
            bordered
          />

          <Pagination
            current={currentPage}
            pageSize={pageSize}
            total={contracts.length}
            onChange={(page) => setCurrentPage(page)}
            style={{ marginTop: 16, textAlign: "center" }}
          />
        </>
      )}

      <Modal
        title="Chỉnh sửa hợp đồng"
        open={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        onOk={() => form.submit()}
      >
        <Form form={form} layout="vertical" onFinish={handleUpdateContract}>
          <Form.Item name="contractStatus" label="Trạng thái hợp đồng">
            <Select>
              <Option value={1}>Đang chờ</Option>
              <Option value={2}>Hoàn thành</Option>
            </Select>
          </Form.Item>
          <Form.Item name="contractType" label="Loại hợp đồng">
            <Select>
              <Option value={1}>Mua</Option>
              <Option value={2}>Thuê</Option>
            </Select>
          </Form.Item>
          <Form.Item name="statusPayment" label="Trạng thái thanh toán">
            <Select>
              <Option value={0}>Chưa thanh toán</Option>
              <Option value={1}>Đã thanh toán</Option>
            </Select>
          </Form.Item>
          <Form.Item name="clauseIds" label="Điều khoản hợp đồng">
            <Select mode="multiple" placeholder="Chọn điều khoản">
              <Option value={1}>Điều khoản 1</Option>
              <Option value={2}>Điều khoản 2</Option>
            </Select>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default ContractList;
