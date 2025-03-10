import React from "react";
import { Table } from "antd";

const TaskList = ({ selectedYear, tasks }) => {
  const columns = [
    { title: "Ngày", dataIndex: "date", key: "date" },
    { title: "Công việc", dataIndex: "task", key: "task" },
    { title: "Trạng thái", dataIndex: "status", key: "status" },
    { title: "Người thực hiện", dataIndex: "person", key: "person" },
    { title: "Giờ", dataIndex: "time", key: "time" },
    { title: "Ghi chú", dataIndex: "comment", key: "comment" }
  ];

  const filteredTasks = tasks.filter((task) => task.date.startsWith(`${selectedYear}`));

  return <Table columns={columns} dataSource={filteredTasks} pagination={{ pageSize:5 }} />;
};

export default TaskList;
