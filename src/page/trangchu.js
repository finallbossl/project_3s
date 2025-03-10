import React from "react";
import { Layout, Card, Typography, Row, Col } from "antd";
import { PieChart, Pie, Cell, BarChart, Bar, XAxis, YAxis, Tooltip, Legend, LineChart, Line, ResponsiveContainer } from "recharts";
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet";
import L from "leaflet";
import "leaflet/dist/leaflet.css";

const realEstateLocations = [
  { name: "BĐS 1", lat: 13.782, lng: 109.219, price: 3.5, sold: true, image: "https://scontent.fsgn2-6.fna.fbcdn.net/v/t39.30808-6/471974246_1549302423128871_2877890771528881990_n.jpg?stp=cp6_dst-jpg_tt6&_nc_cat=111&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=6K5c2rQxn0sQ7kNvgHs9KpH&_nc_oc=AdgIKxrydgwwsPoYLlOZPHyhBIaoHrIbT900IhjtJQ0IeZsqg3bb6YQ4q6lhmOSkDXk&_nc_zt=23&_nc_ht=scontent.fsgn2-6.fna&_nc_gid=A2FBkng_-Hg3jsq2hSRO_T0&oh=00_AYD-967q5Wdv_0-DG26DCn6wA5bjSW25vyVrmCy-qcvh1Q&oe=67BCA95D" },
  { name: "BĐS 2", lat: 13.769, lng: 109.223, price: 2.8, sold: false, image: "https://scontent.fsgn2-6.fna.fbcdn.net/v/t39.30808-6/471974246_1549302423128871_2877890771528881990_n.jpg?stp=cp6_dst-jpg_tt6&_nc_cat=111&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=6K5c2rQxn0sQ7kNvgHs9KpH&_nc_oc=AdgIKxrydgwwsPoYLlOZPHyhBIaoHrIbT900IhjtJQ0IeZsqg3bb6YQ4q6lhmOSkDXk&_nc_zt=23&_nc_ht=scontent.fsgn2-6.fna&_nc_gid=A2FBkng_-Hg3jsq2hSRO_T0&oh=00_AYD-967q5Wdv_0-DG26DCn6wA5bjSW25vyVrmCy-qcvh1Q&oe=67BCA95D" },
  { name: "BĐS 3", lat: 13.755, lng: 109.230, price: 4.2, sold: true, image: "https://tse2.mm.bing.net/th?id=OIP.RvPElgbukAXcjxuctdBmRwHaEo&pid=Api&P=0&h=180" },
  { name: "BĐS 4", lat: 13.743, lng: 109.215, price: 3.1, sold: false, image: "https://tse2.mm.bing.net/th?id=OIP.RvPElgbukAXcjxuctdBmRwHaEo&pid=Api&P=0&h=180" },
  { name: "BĐS 5", lat: 13.732, lng: 109.240, price: 5.0, sold: true, image: "https://tse2.mm.bing.net/th?id=OIP.RvPElgbukAXcjxuctdBmRwHaEo&pid=Api&P=0&h=180" },
  { name: "BĐS 6", lat: 13.720, lng: 109.250, price: 2.9, sold: false, image: "https://tse2.mm.bing.net/th?id=OIP.RvPElgbukAXcjxuctdBmRwHaEo&pid=Api&P=0&h=180" },
  { name: "BĐS 7", lat: 13.705, lng: 109.235, price: 4.7, sold: true, image: "https://tse2.mm.bing.net/th?id=OIP.RvPElgbukAXcjxuctdBmRwHaEo&pid=Api&P=0&h=180" },
  { name: "BĐS 8", lat: 13.695, lng: 109.220, price: 3.8, sold: false, image: "https://tse2.mm.bing.net/th?id=OIP.RvPElgbukAXcjxuctdBmRwHaEo&pid=Api&P=0&h=180" },
  { name: "BĐS 9", lat: 13.685, lng: 109.205, price: 3.2, sold: true, image: "https://tse2.mm.bing.net/th?id=OIP.RvPElgbukAXcjxuctdBmRwHaEo&pid=Api&P=0&h=180" },
  { name: "BĐS 10", lat: 13.670, lng: 109.195, price: 5.5, sold: false, image: "https://tse2.mm.bing.net/th?id=OIP.RvPElgbukAXcjxuctdBmRwHaEo&pid=Api&P=0&h=180" },
];


const Dashboard = () => {
  const soldCount = realEstateLocations.filter(item => item.sold).length;
  const notSoldCount = realEstateLocations.length - soldCount;

  return (
    <div style={{ margin: "16px", padding: "10px", background: "#fff", marginLeft: "42px", overflow: "auto", maxHeight: "88vh", scrollbarWidth: "none", msOverflowStyle: "none" }}>
      <Row gutter={16}>
        <Col span={12}>
          <Card title="Doanh thu theo tháng">
            <ResponsiveContainer width="100%" height={200}>
              <LineChart data={[{ month: "Jan", revenue: 30 }, { month: "Feb", revenue: 40 }]}>
                <XAxis dataKey="month" />
                <YAxis />
                <Tooltip />
                <Line type="monotone" dataKey="revenue" stroke="#8884d8" />
                <Legend />
              </LineChart>
            </ResponsiveContainer>
          </Card>
        </Col>
        <Col span={12}>
          <Card title="Hợp đồng thành công và đang tiến hành theo tháng">
            <ResponsiveContainer width="100%" height={200}>
              <BarChart data={[{ month: "Jan", successful: 10, ongoing: 5 }, { month: "Feb", successful: 12, ongoing: 8 }]}>
                <XAxis dataKey="month" />
                <YAxis />
                <Tooltip />
                <Legend />
                <Bar dataKey="successful" fill="#82ca9d" name="Thành công" />
                <Bar dataKey="ongoing" fill="#8884d8" name="Đang tiến hành" />
              </BarChart>
            </ResponsiveContainer>
          </Card>
        </Col>
      </Row>

      <Row gutter={16} style={{ marginTop: 24 }}>
        <Col span={12}>
          <Card title="Bất động sản đã bán và chưa bán">
            <ResponsiveContainer width="100%" height={200}>
              <PieChart>
                <Pie data={[{ name: "Đã bán", value: soldCount }, { name: "Chưa bán", value: notSoldCount }]} dataKey="value" cx="50%" cy="50%" outerRadius={80} fill="#8884d8">
                  {["#0088FE", "#FF8042"].map((entry, index) => (
                    <Cell key={`cell-${index}`} fill={entry} />
                  ))}
                </Pie>
              </PieChart>
            </ResponsiveContainer>
          </Card>
        </Col>
        <Col span={12}>
          <Card title="Hợp đồng lịch hẹn theo tháng">
            <ResponsiveContainer width="100%" height={200}>
              <BarChart data={[{ month: "Jan", successful: 10, ongoing: 5 }, { month: "Feb", successful: 12, ongoing: 8 }]}>
                <XAxis dataKey="month" />
                <YAxis />
                <Tooltip />
                <Legend />
                <Bar dataKey="successful" fill="#82ca9d" name="Thành công" />
                <Bar dataKey="ongoing" fill="#8884d8" name="Đang tiến hành" />
              </BarChart>
            </ResponsiveContainer>
          </Card>
        </Col>
      </Row>

      <Card title="Bản đồ bất động sản tại Quy Nhơn, Bình Định" style={{ marginTop: 24 }}>
        <MapContainer center={[13.782, 109.219]} zoom={12} style={{ height: 400, width: "100%" }}>
          <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
          {realEstateLocations.map((location, index) => (
            <Marker 
              key={index} 
              position={[location.lat, location.lng]}
              icon={L.icon({
                iconUrl: location.image,
                iconSize: [32, 32],
                iconAnchor: [16, 32]
              })}
            >
              <Popup>
                <div style={{ textAlign: "center" }}>
                  <img src={location.image} alt={location.name} style={{ width: "150px", borderRadius: "8px", marginBottom: "8px" }} />
                  <p><strong>{location.name}</strong></p>
                  <p>Giá: {location.price} tỷ VNĐ</p>
                </div>
              </Popup>
            </Marker>
          ))}
        </MapContainer>
      </Card>
    </div>
  );
};

export default Dashboard;
