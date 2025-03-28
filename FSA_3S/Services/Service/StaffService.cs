    using FSA_3S.DTOs;
    using FSA_3S.Enum;
    using FSA_3S.Models.Entities;
    using FSA_3S.Repositories.Interface;
    using FSA_3S.Services.Interface;

    namespace FSA_3S.Services.Service
    {
        public class StaffService(IStaffRepository staffRepository) : IStaffService
        {
            private readonly IStaffRepository _staffRepository = staffRepository;

            /// <summary>
            /// API GET User role Staff
            /// </summary>
            /// <param name="role"></param>
            /// <returns></returns>
            public async Task<IEnumerable<StaffDTO>> GetStaffByRoleAsync(string role)
            {
                var users = await _staffRepository.GetUsersByRoleAsync(role);

                return users.Select(u => new StaffDTO
                {
                    UserId = u.UserId,
                    Email = u.Email,
                    FullName = u.FullName,
                    PhoneNumber = u.PhoneNumber,
                    Gender = u.Gender,
                    BirthDate = u.BirthDate,
                    CCCD = u.CCCD,
                    Role = u.Role,
                    CreateDate = u.CreateDate
                }).ToList();
            }

            /// <summary>
            /// API PUT Unable User role Staff
            /// </summary>
            /// <param name="userId"></param>
            /// <returns></returns>
            public async Task<UnableStaffResponse> ToggleAccountStatusAsync(int userId)
            {
                return await _staffRepository.ToggleAccountStatusAsync(userId);
            }
        }
    }