using Microsoft.AspNetCore.Mvc;
using FSA_3S.Models;
using FSA_3S.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FSA_3S.Models.Requests;
using Microsoft.AspNetCore.Authorization;

[Route("api/MappingUserAppointment")]
[ApiController]
public class MappingUserAppointmentController : ControllerBase
{
    private readonly IMappingUserAppointmentService _service;
   
    public MappingUserAppointmentController( IMappingUserAppointmentService service)
    {
        _service = service;
       
    }

    
    [HttpGet("get-all-Approve")]
    public async Task<ActionResult<IEnumerable<MappingUserAppointmentResponse>>> GetMappingsApprovalStatus()
    {
        var mappings = await _service.GetAllMappingsApprovalStatusAsync();
        return Ok(mappings);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MappingUserAppointmentResponse>>> GetMappings()
    {
        var mappings = await _service.GetAllMappingsAsync();
        return Ok(mappings);
    }
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

    [HttpPost]
    public async Task<IActionResult> CreateMapping([FromBody] MappingUserAppointmentRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
            return Unauthorized();

        try
        {
            var res = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetMapping), new { id = res.MappingUserAppointmentId }, res);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new { message = "Error saving data.", details = ex.InnerException?.Message ?? ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An unexpected error occurred.", details = ex.Message });
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMapping(int id, [FromBody] MappingUserAppointmentRequest request)
    {
        try
        {
            var updatedMapping = await _service.UpdateStatusAsync(id, request.Status);

            return Ok(new
            {
                message = "Cập nhật trạng thái thành công!",
                data = updatedMapping
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Cập nhật thất bại: {ex.Message}" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMapping(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
    [HttpPut("appointment/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Approveappointment(int id, [FromBody] ApprovalStatusRequest request)
    {
        var result = await _service.UpdateApprovalStatusAsync(id, request);
        if (result == null)
        {
            return NotFound(new { message = "Không tìm thấy bất động sản!" });
        }

        return Ok(new { message = "Cập nhật trạng thái thành công!" });
    }
}