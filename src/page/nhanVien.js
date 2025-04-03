import React, { useState, useEffect } from "react";
import { Table, Button, Modal, Form, Input, TimePicker, message } from "antd";
import { PlusOutlined, EditOutlined, DeleteOutlined } from "@ant-design/icons";
import dayjs from "dayjs";
import axios from "../Api";

const CongViecNhanVien = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [form] = Form.useForm();
  const [selectedWorkId, setSelectedWorkId] = useState(null);
  const [duLieu, setDuLieu] = useState([]);
const [currentUserRole, setCurrentUserRole] = useState(null);

useEffect(() => {
  const role = localStorage.getItem("role"); // Lấy role từ localStorage
  setCurrentUserRole(role);
    const fetchWorks = async () => {
      try {
        const response = await axios.get("/api/work");
  
        // Chuyển đổi dữ liệu API về đúng định dạng
        const formattedData = response.data.map((item) => ({
          workId: item.workId,
          userId: item.userId,
          monday: { task: item.monday, time: item.mondayTime },
          tuesday: { task: item.tuesday, time: item.tuesdayTime },
          wednesday: { task: item.wednesday, time: item.wednesdayTime },
          thursday: { task: item.thursday, time: item.thursdayTime },
          friday: { task: item.friday, time: item.fridayTime },
          saturday: { task: item.saturday, time: item.saturdayTime },
          sunday: { task: item.sunday, time: item.sundayTime },
        }));
  
        setDuLieu(formattedData);
      } catch (error) {
        console.error("Error fetching data: ", error);
        message.error("Không thể tải dữ liệu.");
      }
    };
  
    fetchWorks();
  }, []);
  

  // Handle create work
  const handleCreateWork = async (values) => {
    const newWork = {
      userId: values.userId,
      monday: values.monday || "",  // Đảm bảo không có giá trị undefined hoặc null
      mondayTime: values.mondayTime ? values.mondayTime.format("HH:mm") : "",
      tuesday: values.tuesday || "",
      tuesdayTime: values.tuesdayTime ? values.tuesdayTime.format("HH:mm") : "",
      wednesday: values.wednesday || "",
      wednesdayTime: values.wednesdayTime ? values.wednesdayTime.format("HH:mm") : "",
      thursday: values.thursday || "",
      thursdayTime: values.thursdayTime ? values.thursdayTime.format("HH:mm") : "",
      friday: values.friday || "",
      fridayTime: values.fridayTime ? values.fridayTime.format("HH:mm") : "",
      saturday: values.saturday || "",
      saturdayTime: values.saturdayTime ? values.saturdayTime.format("HH:mm") : "",
      sunday: values.sunday || "",
      sundayTime: values.sundayTime ? values.sundayTime.format("HH:mm") : "",
    };
  
    console.log("Dữ liệu gửi lên API:", newWork);
  
    try {
      const response = await axios.post("/api/work", newWork);
      setDuLieu([...duLieu, response.data]);
      setIsModalOpen(false);
      form.resetFields();
      message.success("Công việc đã được tạo thành công!");
    } catch (error) {
      console.error("Lỗi API111:", error.response?.data || error);
      message.error("Không thể tạo công việc.");
    }
  };
  
  
  
  // Handle update work
  const handleEditWork = async (values) => {
   
    const updatedWork = {
      userId: values.userId,
      monday: values.monday || "",
      mondayTime: values.mondayTime ? values.mondayTime.format("HH:mm") : "",
      tuesday: values.tuesday || "",
      tuesdayTime: values.tuesdayTime ? values.tuesdayTime.format("HH:mm") : "",
      wednesday: values.wednesday || "",
      wednesdayTime: values.wednesdayTime ? values.wednesdayTime.format("HH:mm") : "",
      thursday: values.thursday || "",
      thursdayTime: values.thursdayTime ? values.thursdayTime.format("HH:mm") : "",
      friday: values.friday || "",
      fridayTime: values.fridayTime ? values.fridayTime.format("HH:mm") : "",
      saturday: values.saturday || "",
      saturdayTime: values.saturdayTime ? values.saturdayTime.format("HH:mm") : "",
      sunday: values.sunday || "",
      sundayTime: values.sundayTime ? values.sundayTime.format("HH:mm") : "",
    };
  
    try {
      // Gửi PUT request với workId trong URL và dữ liệu cập nhật trong body
      const response = await axios.put(`/api/work/${selectedWorkId}`, updatedWork);
      setDuLieu(
        duLieu.map((work) =>
          work.workId === selectedWorkId ? { ...work, ...response.data } : work
        )
      );
      setIsEditModalOpen(false);
      form.resetFields();
      message.success("Công việc đã được cập nhật thành công!");
    } catch (error) {
      console.error("Error updating work:", error);
      if (error.response && error.response.status === 404) {
        message.error("Công việc không tồn tại.");
      } else {
        message.error("Không thể cập nhật công việc.");
      }
    }
  };
  

  // Handle delete work
  const handleDeleteWork = async (workId) => {
    try {
      await axios.delete(`/api/work/${workId}`); // Replace with your API endpoint
      setDuLieu(duLieu.filter((work) => work.workId !== workId));
      message.success("Công việc đã được xóa thành công!");
    } catch (error) {
      console.error("Error deleting work: ", error);
      message.error("Không thể xóa công việc.");
    }
  };

  const columns = [
    { title: "ID Nhân Viên", dataIndex: "userId", key: "userId" },
    { title: "Thứ 2", dataIndex: "monday", key: "monday", render: (text) => formatCell(text) },
    { title: "Thứ 3", dataIndex: "tuesday", key: "tuesday", render: (text) => formatCell(text) },
    { title: "Thứ 4", dataIndex: "wednesday", key: "wednesday", render: (text) => formatCell(text) },
    { title: "Thứ 5", dataIndex: "thursday", key: "thursday", render: (text) => formatCell(text) },
    { title: "Thứ 6", dataIndex: "friday", key: "friday", render: (text) => formatCell(text) },
    { title: "Thứ 7", dataIndex: "saturday", key: "saturday", render: (text) => formatCell(text) },
    { title: "Chủ Nhật", dataIndex: "sunday", key: "sunday", render: (text) => formatCell(text) },
    {
      title: "Hành Động", key: "action", render: (_, record) => (
        <span>
  {currentUserRole === "Admin" && ( // Chỉ cần một cặp `{}` 
    <>
      <Button 
        icon={<EditOutlined />} 
        onClick={() => editWork(record)} 
        style={{ marginRight: 8 }} 
      />
      <Button 
        icon={<DeleteOutlined />} 
        onClick={() => handleDeleteWork(record.workId)} 
      />
    </>
  )}
</span>
      ),
    },
  ];

  const formatCell = (data) => {
    if (typeof data === "string") {
      return data ? (
        <span style={{ backgroundColor: "#e6f7ff", padding: "5px", borderRadius: "4px" }}>
          {data}
        </span>
      ) : (
        ""
      );
    } else if (typeof data === "object" && data !== null) {
      return data.task ? (
        <span style={{ backgroundColor: "#e6f7ff", padding: "5px", borderRadius: "4px" }}>
          {data.task} ({data.time})
        </span>
      ) : (
        ""
      );
    }
    return "";
  };
  

  const editWork = (work) => {
    if (!work) {
      console.error("Công việc không hợp lệ", work);
      return;
    }
  
    setSelectedWorkId(work.workId); 
    form.setFieldsValue({
      userId: work.userId,
      monday: work.monday.task,
      mondayTime: dayjs(work.monday.time, "HH:mm"),
      tuesday: work.tuesday.task,
      tuesdayTime: dayjs(work.tuesday.time, "HH:mm"),
      wednesday: work.wednesday.task,
      wednesdayTime: dayjs(work.wednesday.time, "HH:mm"),
      thursday: work.thursday.task,
      thursdayTime: dayjs(work.thursday.time, "HH:mm"),
      friday: work.friday.task,
      fridayTime: dayjs(work.friday.time, "HH:mm"),
      saturday: work.saturday.task,
      saturdayTime: dayjs(work.saturday.time, "HH:mm"),
      sunday: work.sunday.task,
      sundayTime: dayjs(work.sunday.time, "HH:mm"),
    });
    setIsEditModalOpen(true);
  };

  return (
    <div style={{ padding: 20 }}>
      <Button type="primary" icon={<PlusOutlined />} onClick={() => setIsModalOpen(true)}>
        Tạo Công Việc
      </Button>
      <Table dataSource={duLieu} columns={columns} rowKey="userId" pagination={false} bordered />

      {/* Modal tạo công việc */}
      {currentUserRole === "Admin" && (
  <Modal title="Tạo Công Việc Mới" open={isModalOpen} onCancel={() => setIsModalOpen(false)} footer={null}>
    <Form form={form} onFinish={handleCreateWork} layout="vertical">
      <Form.Item name="userId" label="ID Nhân Viên" rules={[{ required: true }]}> 
        <Input type="number" />
      </Form.Item>

      {["monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"].map(day => (
        <React.Fragment key={day}>
          <Form.Item name={`${day}`} label={`Công việc ${day.charAt(0).toUpperCase() + day.slice(1)}`}>
            <Input />
          </Form.Item>

          <Form.Item name={`${day}Time`} label={`Giờ làm ${day.charAt(0).toUpperCase() + day.slice(1)}`}>
            <TimePicker format="HH:mm" />
          </Form.Item>
        </React.Fragment>
      ))}

      <Form.Item>
        <Button type="primary" htmlType="submit">Tạo</Button>
      </Form.Item>
    </Form>
  </Modal>
)}



      {/* Modal chỉnh sửa công việc */}
      <Modal title="Chỉnh Sửa Công Việc" open={isEditModalOpen} onCancel={() => setIsEditModalOpen(false)} footer={null}>
        <Form form={form} onFinish={handleEditWork} layout="vertical">
          <Form.Item name="userId" label="ID Nhân Viên" rules={[{ required: true }]}> 
            <Input type="number" disabled />
          </Form.Item>
          {["monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday"].map(day => (
            <>
              <Form.Item name={day} label={`Công việc ${day.charAt(0).toUpperCase() + day.slice(1)}`}> 
                <Input />
              </Form.Item>
              <Form.Item name={`${day}Time`} label={`Giờ làm ${day.charAt(0).toUpperCase() + day.slice(1)}`}> 
                <TimePicker format="HH:mm" />
              </Form.Item>
            </>
          ))}
          <Form.Item>
            <Button type="primary" htmlType="submit">Cập nhật</Button>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default CongViecNhanVien;
