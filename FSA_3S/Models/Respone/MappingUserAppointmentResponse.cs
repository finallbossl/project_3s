public class MappingUserAppointmentResponse
{
    public int MappingUserAppointmentId { get; set; }
    public int UserId { get; set; }
    public string? FullName { get; set; } // Tên người dùng
    public int AppointmentId { get; set; }
    public string? AppointmentTitle { get; set; } // Tiêu đề của cuộc hẹn
    public string? CustomerName { get; set; } // Tên của khách hàng
}