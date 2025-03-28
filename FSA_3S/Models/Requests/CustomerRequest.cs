using System.ComponentModel.DataAnnotations;
using FSA_3S.Enum;

namespace FSA_3S.Models.Requests
{
    public class CustomerRequest
    {
        [Required]
        public required string FullName { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? CCCD { get; set; }
        public CustomerTypeEnum CustomerType { get; set; }
        //public string? Notes { get; set; }
    }
}
