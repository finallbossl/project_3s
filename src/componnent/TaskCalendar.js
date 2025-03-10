import React from "react";
import { Calendar, Badge } from "antd";

const TaskCalendar = ({ selectedYear, tasks }) => {
  const dateCellRender = (value) => {
    const dateStr = value.format("YYYY-MM-DD");
    const dailyTasks = tasks.filter((task) => task.date === dateStr);

    return (
      <ul style={{ listStyle: "none", padding: 0, margin: 0 }}>
        {dailyTasks.map((task) => (
          <li key={task.key}>
            <Badge 
              status={task.status === "Hoàn thành" ? "success" : "error"} 
              text={task.task} 
            />
          </li>
        ))}
      </ul>
    );
  };

  return (
    <div style={{ overflow: "auto", padding: 10,marginTop:"5px", border: "1px solid #ddd", borderRadius: 8 ,scrollbarWidth: "none",  msOverflowStyle: "none" }}>
      <Calendar 
        fullscreen={true} 
        dateCellRender={dateCellRender} 
        style={{overflow: "auto",maxHeight:"54vh",scrollbarWidth: "none",  msOverflowStyle: "none" }}
      />
    </div>
  );
};

export default TaskCalendar;
