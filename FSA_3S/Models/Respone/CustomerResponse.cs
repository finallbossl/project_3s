using FSA_3S.Enum;

namespace FSA_3S.Models.Respone
{
    public class CustomerResponse
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? CCCD { get; set; }
        public CustomerTypeEnum CustomerType { get; set; }
        //public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
