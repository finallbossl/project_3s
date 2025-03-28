using FSA_3S.DTOs;
using FSA_3S.Services.Service;
using Microsoft.AspNetCore.Mvc;
using FSA_3S.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using FSA_3S.Enum;

namespace FSA_3S.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize]
    public class StaffController(IStaffService staffService) : ControllerBase
    {
        public readonly IStaffService _staffService = staffService;

        [HttpGet("GET")]
        public async Task<IActionResult> GetStaff()
        {
            var result = await _staffService.GetStaffByRoleAsync("Staff");
            return Ok(result);
        }

        /// <summary>
        /// Unable Staff
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPut("PUT (Unable)")]
        public async Task<IActionResult> ChangeAccountStatus(int userId)
        {
            var result = await _staffService.ToggleAccountStatusAsync(userId);

            if (!result.Success)
                return BadRequest("Không tìm thấy tài khoản hoặc tài khoản không phải Staff");

            return Ok(new
            {
                Message = $"Tài khoản với ID {userId} đã được chuyển sang trạng thái {result.Status}"
            });
        }
    }
}