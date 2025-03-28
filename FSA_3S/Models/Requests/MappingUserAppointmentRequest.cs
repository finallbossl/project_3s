public class MappingUserAppointmentRequest
{
    public int UserId { get; set; }

    // Nếu AppointmentId có giá trị => sử dụng Appointment đã có
    // Nếu bằng 0 hoặc không gửi lên => Tạo Appointment mới
    public int? AppointmentId { get; set; }

    // Thông tin để tạo Appointment nếu cần
    public int CustomerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateOnly? AppointmentDate { get; set; }
    public string? Status { get; set; }
    public string? Address { get; set; }
    public DateTime? CreatedBy { get; set; }
}
