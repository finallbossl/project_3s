using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using FSA_3S.Models;
using FSA_3S.Models.Entities;
using FSA_3S.Enum;

namespace FSA_3S.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController(IConfiguration configuration, AppDbContext appDbContext) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly AppDbContext _appDbContext = appDbContext;

        // DTO nhận dữ liệu đăng nhập
        public class LoginRequest
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        [HttpPost("dangnhap")]
        public async Task<IActionResult> UserLogin([FromBody] LoginRequest request)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user == null)
            {
                return Unauthorized(new { message = "Tài khoản không tồn tại!" });
            }
            if (user.Status == UserStatusEnum.Inactive)
            {
                return Unauthorized(new { message = "Tài khoản của bạn đã bị vô hiệu hóa. Vui lòng liên hệ với quản trị viên." });
            }
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                {
                    return BadRequest(new { message = "Email và mật khẩu không được để trống!" });
                }

                
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
                if (!isPasswordValid)
                {
                    return Unauthorized(new { message = "Mật khẩu không chính xác!" });
                }

                
                Console.WriteLine("Mat khau  DB: " + user.Password);
                Console.WriteLine("Mat khau nhap vao : " + request.Password);

                var token = GenerateJwtToken(user);
                return Ok(new { message = "Đăng nhập thành công!", userId = user.UserId ,role = user.Role, token });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, new
                {
                    message = "Lỗi server!",
                    error = ex.Message,
                    stack = ex.StackTrace
                });
            }
        }

        private string GenerateJwtToken(UserEntity user)
        {
           
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            
            Console.WriteLine("JWT Key: " + key);
            Console.WriteLine("JWT Issuer: " + issuer);
            Console.WriteLine("JWT Audience: " + audience);

            if (string.IsNullOrEmpty(key))
                throw new InvalidOperationException("Chưa có khởi tạo cấu hình JWT Key trong appsettings.json");
            if (string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
                throw new InvalidOperationException("Chưa có khởi tạo cấu hình JWT Issuer hoặc Audience trong appsettings.json");

            var keyBytes = Encoding.UTF8.GetBytes(key);

            
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(ClaimTypes.Role, user.Role ?? string.Empty),
        new Claim("UserId", user.UserId.ToString())
    };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256)
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}