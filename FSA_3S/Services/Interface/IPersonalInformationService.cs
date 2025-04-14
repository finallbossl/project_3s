using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FSA_3S.Services.Interface;
using FSA_3S.DTOs;


namespace FSA_3S.Services.Interface
{
    public interface IPersonalInformationService
    {
        Task<PersonalInfoDTO?> GetPersonalInfoAsync(int userId);
        Task<bool> UpdatePersonalInfoAsync(int userId, UpdatePersonalInfoDTO updateDto);
        Task<(bool Success, string Message)> ChangePasswordAsync(int userId, ChangePasswordDTO changePasswordDto);
    }
}
