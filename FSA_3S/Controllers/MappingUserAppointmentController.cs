using Microsoft.AspNetCore.Mvc;
using FSA_3S.Models;
using FSA_3S.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

[Route("api/MappingUserAppointment")]
[ApiController]
public class MappingUserAppointmentController : ControllerBase
{
    private readonly IMappingUserAppointmentService _service;
    private readonly IAppointmentService _appointmentService;
    public MappingUserAppointmentController(
      IMappingUserAppointmentService service,
      IAppointmentService appointmentService)
    {
        _service = service;
        _appointmentService = appointmentService;
    }

    // GET: api/mappinguserappointment
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MappingUserAppointmentResponse>>> GetMappings()
    {
        var mappings = await _service.GetAllMappingsAsync();
        return Ok(mappings);
    }
    // GET: api/mappinguserappointment/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<MappingUserAppointmentResponse>> GetMapping(int id)
    {
        var mapping = await _service.GetByIdAsync(id);
        if (mapping == null)
        {
            return NotFound();
        }

        var response = new MappingUserAppointmentResponse
        {
            MappingUserAppointmentId = mapping.MappingUserAppointmentId,
            UserId = mapping.UserId,
            AppointmentId = mapping.AppointmentId
        };

        return Ok(response);
    }

    // POST: api/mappinguserappointment
    [HttpPost]
    [HttpPost]
    public async Task<ActionResult<MappingUserAppointmentResponse>> PostMapping(MappingUserAppointmentRequest request)
    {
        // Tạo một cuộc hẹn mới (Appointment)
        var newAppointment = new AppointmentEntity
        {
            CustomerId = request.CustomerId, // Cần có CustomerId trong request
            Title = request.Title ?? "Default Title",
            Description = request.Description,
            AppointmentDate = request.AppointmentDate ?? DateOnly.FromDateTime(DateTime.UtcNow),
            Status = request.Status ?? "Pending",
            Address = request.Address,
            CreatedBy = request.CreatedBy ?? DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var createdAppointment = await _appointmentService.CreateAsync(newAppointment);

        // Tạo MappingUserAppointment với Appointment vừa tạo
        var mapping = new MappingUserAppointmentEntity
        {
            UserId = request.UserId,
            AppointmentId = createdAppointment.AppointmentId // Lấy ID của Appointment vừa tạo
        };

        var createdMapping = await _service.CreateAsync(mapping);

        var response = new MappingUserAppointmentResponse
        {
            MappingUserAppointmentId = createdMapping.MappingUserAppointmentId,
            UserId = createdMapping.UserId,
            AppointmentId = createdMapping.AppointmentId
        };

        return CreatedAtAction(nameof(GetMapping), new { id = response.MappingUserAppointmentId }, response);
    }
    // PUT: api/mappinguserappointment/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMapping(int id, MappingUserAppointmentRequest request)
    {
        var mapping = await _service.GetByIdAsync(id);
        if (mapping == null)
        {
            return NotFound();
        }

        mapping.UserId = request.UserId;
        //mapping.AppointmentId = request.AppointmentId;

        await _service.UpdateAsync(mapping);
        return NoContent();
    }

    // DELETE: api/mappinguserappointment/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMapping(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}