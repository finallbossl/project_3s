using FSA_3S.Enum;

public class MappingUserAppointmentResponse
{
    public int MappingUserAppointmentId { get; set; }
    public int UserId { get; set; }
    public string? FullName { get; set; }
    public int AppointmentId { get; set; }
    public string? AppointmentTitle { get; set; } 
    public string? CustomerName { get; set; } 
    public string? Status { get; set; }
    public ApprovalStatusEnum ApprovalStatus { get; set; }
    public DateOnly AppointmentDate { get; set; }
    public int CreatedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}