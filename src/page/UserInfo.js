import React, { useState } from "react";
import { Layout, Card, Avatar, Button, Table, Tag, Input, Select, Form } from "antd";
import { EditOutlined, MailOutlined, PhoneOutlined, CalendarOutlined, FileTextOutlined, FilterOutlined } from "@ant-design/icons";
import "bootstrap/dist/css/bootstrap.min.css";

const { Content } = Layout;
const { Option } = Select;

const Profile = () => {
  const [isEditing, setIsEditing] = useState(false);
  const [userData, setUserData] = useState({
    name: "Dominique Charpentier",
    position: "CEO",
    practice: "Finance",
    contact: "Employee",
    gender: "MG3",
    email: "example@gmail.com",
    address: "id/ceo",
    status: "Activated",
    activatedDate: "13/05/2009",
    avatarUrl: "https://randomuser.me/api/portraits/women/44.jpg",
  });

  const [workData, setWorkData] = useState([
    {
      key: "1",
      status: "Active",
      project: "Project 1",
      role: "Quản lý",
      task: "Lịch hẹn vs khách hàng",
      startDate: "11.06.2025",
      capacity: "35%",
    },
    {
      key: "2",
      status: "Active",
      project: "Project 2",
      role: "Quản lý",
      task: "Lịch hẹn vs khách hàng",
      startDate: "12.02.2025",
      capacity: "65%",
    },
  ]);

  const [filteredData, setFilteredData] = useState(workData);
  const [filter, setFilter] = useState("");

  const handleFilterChange = (value) => {
    setFilter(value);
    if (value) {
      setFilteredData(workData.filter((item) => item.project.includes(value)));
    } else {
      setFilteredData(workData);
    }
  };

  const columns = [
    {
      title: "Status",
      dataIndex: "status",
      render: () => <Tag color="green">●</Tag>,
    },
    {
      title: "Project Task",
      dataIndex: "project",
    },
    {
      title: "Vai trò",
      dataIndex: "role",
    },
    {
      title: "Công việc",
      dataIndex: "task",
    },
    {
      title: "Start date",
      dataIndex: "startDate",
    },
    {
      title: "Capacity",
      dataIndex: "capacity",
    },
  ];

  return (
    <Layout style={{ overflow: "auto", maxHeight: "88vh", scrollbarWidth: "none", msOverflowStyle: "none"  }}>
      <Content>
        <div className="d-flex align-items-stretch">
          <Card className="shadow-sm p-4 flex-grow-1" style={{width: "65%" }}>
            <h2 className="mb-4">
              Thông tin <EditOutlined onClick={() => setIsEditing(!isEditing)} className="ms-2 cursor-pointer" />
            </h2>
            <Form layout="vertical">
              <div className="row">
                {Object.entries(userData).map(([key, value]) => (
                  key !== "avatarUrl" && (
                    <div className="col-md-6 mb-3" key={key}>
                      <Form.Item label={key.replace(/([A-Z])/g, ' $1').trim()}>
                        <Input
                          value={value}
                          onChange={(e) => setUserData({ ...userData, [key]: e.target.value })}
                          disabled={!isEditing}
                        />
                      </Form.Item>
                    </div>
                  )
                ))}
              </div>
              {isEditing && <Button type="primary" onClick={() => setIsEditing(false)}>Save</Button>}
            </Form>
          </Card>

          <Card className="p-4 text-center flex-grow-1" style={{ width: "35%", background: "#fff", borderRadius: "12px" }}>
            <Avatar size={100} src={userData.avatarUrl} className="mb-2" />
            <h4 className="fw-bold">Admin</h4>
            <p className="text-muted">{userData.position}</p>
            <div className="d-flex flex-column align-items-center gap-2 mt-3">
  <Button icon={<MailOutlined />} className="btn btn-outline-primary" />
  <Button icon={<PhoneOutlined />} className="btn btn-outline-success" />
</div>

            <div className="mt-3 text-center">
              {[
                { name: "Calendar", icon: <CalendarOutlined /> },
                { name: "Vacations", icon: <FileTextOutlined /> },
                { name: "Timesheet", icon: <FileTextOutlined /> },
                { name: "Corporate CV", icon: <FileTextOutlined /> },
              ].map((item) => (
                <p
                  key={item.name}
                  className="mb-1 text-primary fw-bold"
                  style={{ transition: "0.3s", cursor: "pointer" }}
                  onMouseEnter={(e) => (e.target.style.color = "#0056b3")}
                  onMouseLeave={(e) => (e.target.style.color = "#007bff")}
                  onClick={() => alert(`${item.name} clicked`)}
                >
                  {item.icon} {item.name}
                </p>
              ))}
            </div>
          </Card>
        </div>

        <Card title="Công việc" style={{ marginTop: 20 }}>
          <div className="d-flex justify-content-between mb-3">
            <Select placeholder="Filter" style={{ width: 150 }} onChange={handleFilterChange} value={filter}>
              <Option value="">All</Option>
              {workData.map((item) => (
                <Option key={item.project} value={item.project}>{item.project}</Option>
              ))}
            </Select>
            <FilterOutlined style={{ fontSize: 18 }} />
          </div>
          <Table columns={columns} dataSource={filteredData} pagination={false} />
        </Card>
      </Content>
    </Layout>
  );
};

export default Profile;