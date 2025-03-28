using FSA_3S.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FSA_3S.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController(UserService userService) : ControllerBase
    {
        private readonly UserService _userService = userService;

        // 🛠 Tạo tài khoản nhân viên
        [HttpPost("taotaikhoan")]
        public async Task<IActionResult> CreateEmployee([FromBody] UserDto userDto)
        {
            try
            {
                var result = await _userService.CreateEmployeeAsync(userDto.Email, userDto.Role, userDto.FullName);
                return Ok(new { message = result });
            }
            catch (DbUpdateException ex)
            {
                var detailedError = ex.InnerException?.Message ?? ex.Message;
                return StatusCode(500, new { message = "Lỗi server", error = detailedError });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", error = ex.Message });
            }
        }

        // 🛠 Lấy danh sách tất cả tài khoản
        [HttpGet("danhsachtaikhoan")]
        [Authorize]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                var users = await _userService.GetAllEmployeesAsync();

                if (users == null || users.Count == 0)
                {
                    return NotFound(new { message = "Không có tài khoản nào trong hệ thống." });
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server", error = ex.Message });
            }
        }

        // DTO cho dữ liệu đầu vào
        public class UserDto
        {
            public string Email { get; set; }
            public string Role { get; set; }
            public string FullName { get; set; }
        }
    }
}
