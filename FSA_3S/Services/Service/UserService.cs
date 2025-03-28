using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using BCrypt.Net;
using FSA_3S.Models;
using FSA_3S.Models.Entities;

namespace FSA_3S.Services.Service
{
    public class UserService
    {
        private readonly AppDbContext _appDbContext;
        private readonly EmailService _emailService;

        public UserService(AppDbContext appDbContext, EmailService emailService)
        {
            _appDbContext = appDbContext;
            _emailService = emailService;
        }

        public async Task<string> CreateEmployeeAsync(string email, string role, string fullName)
        {
            if (await _appDbContext.Users.AnyAsync(u => u.Email == email))
            {
                return "Email đã có trong hệ thống!";
            }

            string password = GenerateRandomPassword();


            string mhPassword = HashPassword(password);

            var user = new UserEntity
            {
                Email = email,
                Password = mhPassword,
                FullName = fullName,
                Role = role,
                CreateDate = DateTime.UtcNow
            };

            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();

            string subject = "Thông tin tài khoản của hệ thống 3S";
            string message = $"3S, Xin chào,\n\nTài khoản của bạn đã được tạo:\nEmail: {email}\nMật khẩu: {password}\n\nVui lòng đăng nhập và đổi mật khẩu ngay lập tức.";

            await _emailService.SendEmailUser(email, subject, message);
            return "Tạo tài khoản thành công!";
        }
              public async Task<List<UserDto>> GetAllEmployeesAsync()
        {
            return await _appDbContext.Users
                .Select(user => new UserDto
                {
                    Id = user.UserId,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role,
                    CreatedAt = user.CreateDate
                })
                .ToListAsync();
        }
        public class UserDto
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
            public DateTime CreatedAt { get; set; }
        }


        private string GenerateRandomPassword()
        {
            return Guid.NewGuid().ToString(
                "N").Substring(0, 8);
        }


        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}