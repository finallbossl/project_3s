
import Menu from "../componnent/menu";
import Header from "../componnent/header";
import { Layout } from "antd";
import { Outlet } from "react-router-dom";
const { Sider, Content } = Layout;

const Home = () => {
  return (
    <Layout >
      <div ><Header/></div>
      <Layout>
        <Sider><Menu/></Sider>
        <Content style={{ margin: "16px", padding: "10px", background: "#fff", marginLeft:"42px" }}>
          <Outlet />
        </Content>
      </Layout>
    </Layout>
  );
};

export default Home;
