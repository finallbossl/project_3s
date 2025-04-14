using FSA_3S.DTOs;
using FSA_3S.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FSA_3S.Controllers
{
    [ApiController]
    [Route("api/personal_Info")]
    public class PersonalInformationController(IPersonalInformationService personalInformationService) : ControllerBase
    {
        private readonly IPersonalInformationService _personalInformationService = personalInformationService;
        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetPersonalInfo(int userId)
        {
            Console.WriteLine($" Đang lấy thông tin user với ID: {userId}");
            var userInfo = await _personalInformationService.GetPersonalInfoAsync(userId);
            if (userInfo == null) return NotFound("Thông tin không tồn tại ");
            return Ok(userInfo);

        }
        [HttpPut("{userId}")]
        [Authorize]
        public async Task<IActionResult> UpdatePersonalInfo(int userId, [FromBody] UpdatePersonalInfoDTO updateDto)
        {
            var success = await _personalInformationService.UpdatePersonalInfoAsync(userId, updateDto);
            if (!success) return BadRequest("Cập nhật thất bại.");
            return Ok("Cập nhật thành công.");
        }
        [HttpPut("change-password/{userId}")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(int userId, [FromBody] ChangePasswordDTO changePasswordDto)
        {
            var result = await _personalInformationService.ChangePasswordAsync(userId, changePasswordDto);
            if (!result.Success) return BadRequest(result.Message);
            return Ok("Đổi mật khẩu thành công.");
        }
    }
}
