using Microsoft.AspNetCore.Mvc;
using FSA_3S.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/Appointment")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _service;

    public AppointmentController(IAppointmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppointmentResponse>>> GetAppointments()
    {
        var appointments = await _service.GetAllAsync();

        if (appointments == null || !appointments.Any())
        {
            return NotFound("No appointments found.");
        }

        foreach (var appointment in appointments)
        {
            Console.WriteLine($"AppointmentId: {appointment.AppointmentId}, Title: {appointment.Title}, Customer: {appointment.Customer?.FullName}");
        }

        var response = appointments.Select(a => new AppointmentResponse
        {
            AppointmentId = a.AppointmentId,
            Title = a.Title,
            Description = a.Description,
            AppointmentDate = a.AppointmentDate,
            Status = a.Status,
            Address = a.Address,
            CreatedAt = a.CreatedAt,
            UpdatedAt = (DateTime)a.UpdatedAt,
            CustomerName = a.Customer?.FullName ?? "Unknown" // Kiểm tra null
        }).ToList();

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentResponse>> GetAppointment(int id)
    {
        var appointment = await _service.GetByIdAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }

        var response = new AppointmentResponse
        {
            AppointmentId = appointment.AppointmentId,
            Title = appointment.Title,
            Description = appointment.Description,
            AppointmentDate = appointment.AppointmentDate,
            Status = appointment.Status,
            Address = appointment.Address,
            CreatedAt = appointment.CreatedAt,
            UpdatedAt = (DateTime)appointment.UpdatedAt
        };

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<AppointmentResponse>> PostAppointment(AppointmentRequest request)
    {
        var appointment = new AppointmentEntity
        {
            Title = request.Title,
            Description = request.Description,
            AppointmentDate = request.AppointmentDate,
            Status = request.Status,
            Address = request.Address,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CustomerId = request.CustomerId 
        };

        var createdAppointment = await _service.CreateAsync(appointment);

        var response = new AppointmentResponse
        {
            AppointmentId = createdAppointment.AppointmentId,
            Title = createdAppointment.Title,
            Description = createdAppointment.Description,
            AppointmentDate = createdAppointment.AppointmentDate,
            Status = createdAppointment.Status,
            Address = createdAppointment.Address,
            CreatedAt = createdAppointment.CreatedAt,
            UpdatedAt = (DateTime)createdAppointment.UpdatedAt
        };

        return CreatedAtAction(nameof(GetAppointment), new { id = response.AppointmentId }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAppointment(int id, AppointmentRequest request)
    {
        var appointment = await _service.GetByIdAsync(id);
        if (appointment == null)
        {
            return NotFound();
        }

        appointment.Title = request.Title;
        appointment.Description = request.Description;
        appointment.AppointmentDate = request.AppointmentDate;
        appointment.Status = request.Status;
        appointment.Address = request.Address;
        appointment.UpdatedAt = DateTime.UtcNow;

        await _service.UpdateAsync(appointment);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}