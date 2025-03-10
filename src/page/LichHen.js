import React, { useState } from "react";
import { Layout, Card, Typography, Select } from "antd";
import {
  CalendarOutlined,
  SearchOutlined,
  PlusOutlined
} from "@ant-design/icons";
import TaskList from "../componnent/TaskList";
import TaskCalendar from "../componnent/TaskCalendar";
import ScheduleForm from "../componnent/ScheduleForm";

const { Content } = Layout;
const { Option } = Select;

const Dashboard = () => {
  const [activeComponent, setActiveComponent] = useState("calendar"); 
  const [selectedYear, setSelectedYear] = useState(new Date().getFullYear());
  
  // Dữ liệu công việc chung với giá trị mặc định
  const tasks = Array.from({ length: 20 }, (_, i) => ({
    key: `${i + 1}`,
    date: `2025-02-${(i % 28) + 1}`,
    task: `Công việc ${i + 1}`,
    status: i % 2 === 0 ? "Hoàn thành" : "Chưa hoàn thành",
    person: `Nhân viên ${i + 1}`,
    time: `${8 + (i % 10)}:00`,
    comment: `Ghi chú công việc ${i + 1}`
  }));

  return (
    <Content style={{ margin: "10px", background: "#fff", padding: 20 }}>
      <div style={{ display: "flex", gap: 10 }}>
        <Card 
          style={{ 
            flex: 1, 
            textAlign: "center", 
            background: activeComponent === "calendar" ? "#cce5ff" : "#e3f2fd", 
            cursor: "pointer" 
          }}
          onClick={() => setActiveComponent("calendar")}
        >
          <CalendarOutlined style={{ fontSize: 30, color: "#007bff" }} />
          <p>Lịch Hẹn</p>
        </Card>

        <Card 
          style={{ 
            flex: 1, 
            textAlign: "center", 
            background: activeComponent === "taskList" ? "#cce5ff" : "#e3f2fd", 
            cursor: "pointer" 
          }} 
          onClick={() => setActiveComponent("taskList")}
        > 
          <SearchOutlined style={{ fontSize: 30 }} />
          <p>Danh sách công việc</p>
        </Card>

        <Card 
          style={{ 
            flex: 1, 
            textAlign: "center", 
            background: activeComponent === "scheduleForm" ? "#cce5ff" : "#e3f2fd", 
            cursor: "pointer" 
          }} 
          onClick={() => setActiveComponent("scheduleForm")}
        >
          <PlusOutlined style={{ fontSize: 30 }} />
          <p>Tạo lịch hẹn</p>
        </Card>
      </div>

      {/* Chọn năm */}
      <div style={{ marginTop: 10, textAlign: "right" }}>
        <Select value={selectedYear} onChange={setSelectedYear} style={{ width: 120 }}>
          {[...Array(5)].map((_, index) => {
            const year = new Date().getFullYear() - 2 + index;
            return <Option key={year} value={year}>{year}</Option>;
          })}
        </Select>
      </div>

      {activeComponent === "calendar" && <TaskCalendar selectedYear={selectedYear} tasks={tasks || []} />}
      {activeComponent === "taskList" && <TaskList selectedYear={selectedYear} tasks={tasks || []} />}
      {activeComponent === "scheduleForm" && <ScheduleForm selectedYear={selectedYear} tasks={tasks || []} />}
    </Content>
  );
};  

export default Dashboard;