using System.ComponentModel.DataAnnotations;

namespace FSA_3S.Models.Requests
{
    public class PersonInfo
    {
        [Required(ErrorMessage = "FullName is required.")]
        [StringLength(50, ErrorMessage = "FullName cannot exceed 50 characters.")]
        public required string FullName { get; set; }

        [Required(ErrorMessage = "CCCD is required.")]
        [StringLength(12, ErrorMessage = "IdentityNumber must be 12 characters.")]
        public required string CCCD { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required.")]
        [RegularExpression(@"^(0[3|5|7|8|9])+([0-9]{8})$", ErrorMessage = "Invalid Vietnamese phone number format.")]
        [StringLength(13, ErrorMessage = "PhoneNumber cannot exceed 13 characters.")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(250, ErrorMessage = "Address cannot exceed 250 characters.")]
        public required string Address { get; set; }
    }
}