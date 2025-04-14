public class MappingUserAppointmentRequest
{
    public int UserId { get; set; }

   
    public int? AppointmentId { get; set; }

    public int CustomerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateOnly? AppointmentDate { get; set; }
    public string? Status { get; set; }
    public string? Address { get; set; }
}
