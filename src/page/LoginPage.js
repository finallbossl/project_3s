import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { Form, Input, Button, Card, Typography } from "antd";
import { UserOutlined, LockOutlined } from "@ant-design/icons";

const { Title } = Typography;

const LoginPage = () => {
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleLogin = (values) => {
    setLoading(true);
    setTimeout(() => {
      if (values.username === "admin" && values.password === "123456") {
        navigate("/app/trangchu");
      } else {
        alert("Sai tên đăng nhập hoặc mật khẩu!");
      }
      setLoading(false);
    }, 1000);
  };

  const styles = {
    container: {
      display: "flex",
      justifyContent: "center",
      alignItems: "center",
      height: "100vh",
      backgroundColor: "#f0f2f5",
    },
    title: {
      textAlign: "center",
    },
    button: {
      width: "100%",
    },
  };

  return (
    <div style={styles.container}>
      <Card title={<Title level={3} style={styles.title}>Đăng Nhập</Title>} style={{ width: 400 }}>
        <Form onFinish={handleLogin}>
          <Form.Item name="username" rules={[{ required: true, message: "Nhập tên đăng nhập!" }]}>
            <Input prefix={<UserOutlined />} placeholder="Tên đăng nhập" size="large" />
          </Form.Item>
          <Form.Item name="password" rules={[{ required: true, message: "Nhập mật khẩu!" }]}>
            <Input.Password prefix={<LockOutlined />} placeholder="Mật khẩu" size="large" />
          </Form.Item>
          <Form.Item>
            <Button type="primary" htmlType="submit" loading={loading} block size="large" style={styles.button}>
              Đăng nhập
            </Button>
          </Form.Item>
        </Form>
      </Card>
    </div>
  );
};

export default LoginPage;