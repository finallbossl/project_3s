namespace FSA_3S.DTOs
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? CCCD { get; set; }
        public string? CustomerType { get; set; }
        public string? Notes { get; set; }
        public DateOnly CreatedAt { get; set; }
    }
}
