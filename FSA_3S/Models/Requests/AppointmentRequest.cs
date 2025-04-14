using System;
using System.ComponentModel.DataAnnotations;

public class AppointmentRequest
{
    [Required]
    [StringLength(50)]
    public string Title { get; set; }

    public string? Description { get; set; }

    [Required]
    public DateOnly AppointmentDate { get; set; }

    [StringLength(10)]
    public string? Status { get; set; }

    [StringLength(255)]
    public string? Address { get; set; }

    public int CustomerId { get; set; }
}