using System;

public class AppointmentResponse
{
    public int AppointmentId { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateOnly AppointmentDate { get; set; }
    public string? Status { get; set; }
    public string? Address { get; set; }
    public string CustomerName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}