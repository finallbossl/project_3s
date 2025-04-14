using FSA_3S.Enum;
using FSA_3S.Models.Entities;

namespace FSA_3S.DTOs
{
    public class PersonalInfoDTO
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? CCCD { get; set; }
        public string? TypeOfStaff { get; set; }
        public UserStatusEnum Status { get; set; }

        public PersonalInfoDTO(UserEntity user)
        {
            UserId = user.UserId;
            Email = user.Email;
            FullName = user.FullName;
            PhoneNumber = user.PhoneNumber;
            Gender = user.Gender;
            BirthDate = user.BirthDate;
            CCCD = user.CCCD;
            TypeOfStaff = user.TypeOfStaff;
            Status = user.Status;
        }
    }

}
