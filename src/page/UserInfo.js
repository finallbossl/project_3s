import React, { useEffect, useState } from "react";
import {
  Layout,
  Card,
  Avatar,
  Button,
  Table,
  Tag,
  Input,
  Select,
  Form,
  Typography,
  Space,
  message,
  Modal,
  Row,
  Col,
} from "antd";
import { EditOutlined, LockOutlined } from "@ant-design/icons";
import api from "../Api";

const { Content } = Layout;
const { Title, Text } = Typography;
const { Option } = Select;

const Profile = () => {
  const [isEditing, setIsEditing] = useState(false);
  const [isChangingPassword, setIsChangingPassword] = useState(false);
  const [userData, setUserData] = useState(null);
  const [workData, setWorkData] = useState([]);
  const [filteredData, setFilteredData] = useState([]);
  const [filter, setFilter] = useState("");
  const [form] = Form.useForm();
  const userId = localStorage.getItem("userId");

  useEffect(() => {
    if (!userId) {
      message.error("Không tìm thấy mã người dùng. Vui lòng đăng nhập lại!");
      return;
    }
    api
      .get(`/api/personal_Info/${userId}`)
      .then((res) => setUserData(res.data))
      .catch(() => message.error("Không thể lấy thông tin cá nhân!"));
  }, [userId]);

  const handleSave = () => {
    api
      .put(`/api/personal_Info/${userId}`, userData)
      .then(() => {
        setIsEditing(false);
        message.success("Cập nhật thông tin thành công!");
      })
      .catch(() => message.error("Cập nhật thất bại!"));
  };

  const handlePasswordChange = () => {
    form.validateFields().then((values) => {
      api
        .put(`/api/personal_Info/change-password/${userId}`, {
          currentPassword: values.oldPassword,
          newPassword: values.newPassword,
        })
        .then(() => {
          setIsChangingPassword(false);
          form.resetFields();
          message.success("Đổi mật khẩu thành công!");
        })
        .catch((error) =>
          message.error(error.response?.data?.title || "Đổi mật khẩu thất bại!")
        );
    });
  };

  const handleFilterChange = (value) => {
    setFilter(value);
    setFilteredData(
      value ? workData.filter((item) => item.project.includes(value)) : workData
    );
  };

  if (!userData) return <p>Đang tải dữ liệu...</p>;

  return (
    <Layout style={{ padding: 5, maxHeight: "88vh", overflowX: "hidden", overflowY: "auto", backgroundColor:"#ffff" }}>
      <Content>
        <Row gutter={[20, 20]}>
          {/* Thông tin cá nhân */}
          <Col xs={24} md={8}>
            <Card
              className="shadow-sm p-4"
              style={{
                textAlign: "center",
                borderRadius: 10,
                backgroundImage: `url('https://canland.vn/media/uploads/uploads/23171032-marketing-bat-dong-san.jpeg')`,
                backgroundSize: "cover",
                backgroundPosition: "center",
                backgroundRepeat: "no-repeat",
              }}
            >
              <Avatar
                size={120}
                src={
                  userData.avatarUrl ||
                  "https://static.vecteezy.com/system/resources/previews/020/429/953/non_2x/admin-icon-vector.jpg"
                }
                n
                style={{ marginBottom: 10 }}
              />
              <Title level={3}>
                {userData.fullName || "Chưa cập nhật tên"}
              </Title>
              <Text type="secondary">
                {userData.role ? `Chức vụ: ${userData.role}` : "Thông Tin"}
              </Text>
              <div style={{ marginTop: 10 }}>
                <Button
                  type="primary"
                  icon={<LockOutlined />}
                  onClick={() => setIsChangingPassword(true)}
                >
                  Đổi mật khẩu
                </Button>
              </div>
            </Card>
          </Col>

          {/* Cập nhật thông tin */}
          <Col xs={24} md={16}>
            <Card className="shadow-sm p-4" style={{ borderRadius: 10 }}>
              <Title level={4}>
                Thông tin cá nhân
                <EditOutlined
                  onClick={() => setIsEditing(!isEditing)}
                  style={{
                    marginLeft: 10,
                    cursor: "pointer",
                    color: "#1890ff",
                  }}
                />
              </Title>
              <Form layout="vertical">
                {Object.entries(userData).map(([key, value]) =>
                  key !== "avatarUrl" ? (
                    <Form.Item
                      key={key}
                      label={<strong>{getLabel(key)}</strong>}
                    >
                      <Input
                        value={value}
                        onChange={(e) =>
                          setUserData({ ...userData, [key]: e.target.value })
                        }
                        disabled={!isEditing}
                      />
                    </Form.Item>
                  ) : null
                )}
                {isEditing && (
                  <Button type="primary" onClick={handleSave}>
                    Lưu thay đổi
                  </Button>
                )}
              </Form>
            </Card>
          </Col>
        </Row>
      </Content>

      <Modal
        title="Đổi mật khẩu"
        open={isChangingPassword}
        onCancel={() => setIsChangingPassword(false)}
        onOk={handlePasswordChange}
      >
        <Form form={form} layout="vertical">
          <Form.Item
            name="oldPassword"
            label="Mật khẩu cũ"
            rules={[{ required: true, message: "Vui lòng nhập mật khẩu cũ" }]}
          >
            <Input.Password />
          </Form.Item>
          <Form.Item
            name="newPassword"
            label="Mật khẩu mới"
            rules={[{ required: true, message: "Vui lòng nhập mật khẩu mới" }]}
          >
            <Input.Password />
          </Form.Item>
        </Form>
      </Modal>
    </Layout>
  );
};

const getLabel = (key) =>
  ({
    fullName: "Họ và tên",
    email: "Email",
    phoneNumber: "Số điện thoại",
    role: "Chức vụ",
    department: "Phòng ban",
    address: "Địa chỉ",
    birthDate: "Ngày sinh",
    gender: "Giới tính",
    status: "Trạng thái",
  }[key] || key);

export default Profile;
