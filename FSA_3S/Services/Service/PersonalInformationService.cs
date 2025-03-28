using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FSA_3S.Services.Interface;
using FSA_3S.DTOs;
using FSA_3S.Repositories.Interface;

namespace FSA_3S.Services.Service
{
    public class PersonalInformationService(IUserRepository userRepository) : IPersonalInformationService
    {
        private readonly IUserRepository _userRepository =userRepository;

        public async Task<(bool Success, string Message)> ChangePasswordAsync(int userId, ChangePasswordDTO changePasswordDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("Người dùng không tồn tại.");

            if (!BCrypt.Net.BCrypt.Verify(changePasswordDto.CurrentPassword, user.Password))
                throw new UnauthorizedAccessException("Mật khẩu hiện tại không đúng.");

            user.Password = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
            await _userRepository.UpdateAsync(user);
            return (true, "Mật khẩu đã cập nhật thành công rồi.");
        }

        public async Task<PersonalInfoDTO?> GetPersonalInfoAsync(int userId)
        {
            var user =await _userRepository.GetByIdAsync(userId);
            return user == null ? null : new PersonalInfoDTO(user);
        }

        public async Task<bool> UpdatePersonalInfoAsync(int userId, UpdatePersonalInfoDTO updateDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) throw new KeyNotFoundException($"Không tìm thấy thông tin của người dùng nay với Id {userId} \n Vui lòng kiểm tra lại ");
            user.Email = updateDto.Email;
            user.FullName = updateDto.FullName;
            user.PhoneNumber = updateDto.PhoneNumber;
            user.Gender = updateDto.Gender;
            user.BirthDate = updateDto.BirthDate;
            user.CCCD = updateDto.CCCD;
            user.TypeOfStaff = updateDto.TypeOfStaff;
            await _userRepository.UpdateAsync(user);
            return true;
        }

    }
}
