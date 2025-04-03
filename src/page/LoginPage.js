import { useState } from "react"; 
import { useNavigate } from "react-router-dom"; 
import { Form, Input, Button, Card, Typography, message } from "antd"; 
import { UserOutlined, LockOutlined } from "@ant-design/icons"; 
import Api from "../Api"; 
import Background from "../img/img.png";  // Import hình ảnh
import { color } from "framer-motion";

const { Title } = Typography; 

const LoginPage = () => {   
  const navigate = useNavigate();   
  const [loading, setLoading] = useState(false);    

  const handleLogin = async (values) => {     
    setLoading(true);     
    try {       
      const response = await Api.post("/api/dangnhap", values);              
      localStorage.setItem("token", response.data.token);       
      localStorage.setItem("role", response.data.role);       
      localStorage.setItem("userId", response.data.userId);        

      message.success("Đăng nhập thành công!");       
      navigate("/app/trangchu");     
    } catch (error) {       
      message.error(error.response?.data?.message || "Đăng nhập thất bại!");     
    } finally {       
      setLoading(false);     
    }   
  };    

  return (     
    <div style={styles.container }>       
      <Card title={<Title level={3} style={styles.title}>Hệ Thống 3S</Title>} style={{ width: 400 ,marginRight: "38px",}}>         
        <Form onFinish={handleLogin}>           
          <Form.Item name="email" rules={[{ required: true, message: "Nhập tên đăng nhập!" }]}>             
            <Input prefix={<UserOutlined />} placeholder="Tên đăng nhập" size="large" />           
          </Form.Item>           
          <Form.Item name="password" rules={[{ required: true, message: "Nhập mật khẩu!" }]}>             
            <Input.Password prefix={<LockOutlined />} placeholder="Mật khẩu" size="large" />           
          </Form.Item>           
          <Form.Item>             
            <Button type="primary" htmlType="submit" block size="large" style={styles.button} loading={loading}>               
              Đăng nhập             
            </Button>           
          </Form.Item>         
        </Form>       
      </Card>     
    </div>   
  ); 
};  

const styles = {   
  container: {     
    display: "flex",     
    justifyContent: "end",     
    alignItems: "center",     
    height: "100vh",     
    backgroundColor: "#f0f2f5",   
    backgroundImage: `url(${Background})`,
    backgroundSize: "cover", 
    backgroundPosition: "center",     
    backgroundSize: "100% 100%", 
    backgroundPosition: "center",
    backgroundRepeat: "no-repeat", 
  },   
  title: {     
    textAlign: "center", 
    color: "#666668",
    
      
  },   
  button: {     
    width: "100%",   
  }, 
  Card:{
    backgroundColor: "#3478",
  }
  
};

export default LoginPage;
