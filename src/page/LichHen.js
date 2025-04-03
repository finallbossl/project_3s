import React, { useState, useEffect } from "react";
import { Layout, Card, Select } from "antd";
import { CalendarOutlined, SearchOutlined, PlusOutlined } from "@ant-design/icons";
import TaskList from "../componnent/TaskList";
import TaskCalendar from "../componnent/TaskCalendar";
import ScheduleForm from "../componnent/ScheduleForm";
import api from "../Api";

const { Content } = Layout;
const { Option } = Select;

const Dashboard = () => {
  const [activeComponent, setActiveComponent] = useState("calendar");
  const [selectedYear, setSelectedYear] = useState(new Date().getFullYear());
  const [mapAppointments, setMapAppointments] = useState([]); // Khởi tạo là mảng rỗng

  const fetchAppointments = async () => {
    try {
      const response = await api.get("/api/MappingUserAppointment");
      console.log("Response status:", response.status); // Kiểm tra mã trạng thái
      console.log("Fetched appointments:", response.data); // Kiểm tra dữ liệu
      setMapAppointments(response.data);
    } catch (error) {
      console.error("Error fetching appointments:", error);
    }
  };

  useEffect(() => {
    fetchAppointments();
  }, []);

  return (
    <Content style={{ margin: "10px", background: "#fff", padding: 20 }}>
      <div style={{ display: "flex", gap: 10 }}>
        <Card
          style={{ flex: 1, textAlign: "center", cursor: "pointer", background: activeComponent === "calendar" ? "#cce5ff" : "#e3f2fd" }}
          onClick={() => setActiveComponent("calendar")}
        >
          <CalendarOutlined style={{ fontSize: 30 }} />
          <p>Lịch Hẹn</p>
        </Card>

        <Card
          style={{ flex: 1, textAlign: "center", cursor: "pointer", background: activeComponent === "taskList" ? "#cce5ff" : "#e3f2fd" }}
          onClick={() => setActiveComponent("taskList")}
        >
          <SearchOutlined style={{ fontSize: 30 }} />
          <p>Danh sách công việc</p>
        </Card>

        <Card
          style={{ flex: 1, textAlign: "center", cursor: "pointer", background: activeComponent === "scheduleForm" ? "#cce5ff" : "#e3f2fd" }}
          onClick={() => setActiveComponent("scheduleForm")}
        >
          <PlusOutlined style={{ fontSize: 30 }} />
          <p>Tạo lịch hẹn</p>
        </Card>
      </div>

      <div style={{ marginTop: 10, textAlign: "right" }}>
        <Select value={selectedYear} onChange={setSelectedYear} style={{ width: 120 }}>
          {[...Array(5)].map((_, index) => {
            const year = new Date().getFullYear() - 2 + index;
            return <Option key={year} value={year}>{year}</Option>;
          })}
        </Select>
      </div>

      {activeComponent === "calendar" && <TaskCalendar selectedYear={selectedYear} appointments={mapAppointments} />}
      {activeComponent === "taskList" && <TaskList appointments={mapAppointments} />} {/* Truyền mapAppointments vào TaskList */}
      {activeComponent === "scheduleForm" && <ScheduleForm />}
    </Content>
  );
};

export default Dashboard;