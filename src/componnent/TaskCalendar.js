// src/components/TaskCalendar.js
import React from "react";
import { Calendar, Badge } from "antd";

const TaskCalendar = ({ selectedYear, appointments }) => {
  console.log("Appointments in TaskCalendar:", appointments); // Kiểm tra dữ liệu

  const cellRender = (value) => {
    const dateStr = value.format("YYYY-MM-DD");
    const dailyTasks = Array.isArray(appointments) ? appointments.filter((task) => task.appointmentDate === dateStr) : [];

    return (
      <ul style={{ listStyle: "none", padding: 0, margin: 0 }}>
        {dailyTasks.map((task) => (
          <li key={task.mappingUserAppointmentId}>
            <Badge status={task.approvalStatus === "Hoàn thành" ? "success" : "error"} text={`${task.appointmentTitle} - ${task.customerName}`} />
          </li>
        ))}
      </ul>
    );
  };

  return (
    <div style={{ overflow: "auto", padding: 10, marginTop: "5px", border: "1px solid #ddd", borderRadius: 8 }}>
      <Calendar cellRender={cellRender} style={{ overflow: "auto", maxHeight: "54vh" }} />
    </div>
  );
};

export default TaskCalendar;