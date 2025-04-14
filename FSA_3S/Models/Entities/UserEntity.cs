using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FSA_3S.Enum;

namespace FSA_3S.Models.Entities
{
    [Table("user")]
    public class UserEntity
    {
        [Key]
        [Column("userId")]
        public int UserId { get; set; }

        [Required]
        [Column("email")]
        [StringLength(50)]
        public required string Email { get; set; }

        [Required]
        [Column("password")]
        [StringLength(100)]
        public required string Password { get; set; }

        [Required]
        [Column("fullname")]
        [StringLength(50)]
        public required string FullName { get; set; }

        [Column("phonenumber")]
        [StringLength(13)]
        public string? PhoneNumber { get; set; }

        [Column("gender")]
        [StringLength(10)]
        public string? Gender { get; set; }

        [Column("birthdate")]
        public DateTime? BirthDate { get; set; }

        [Column("cccd")]
        [StringLength(13)]
        public string? CCCD { get; set; }

        [Required]
        [Column("role")]
        [StringLength(10)]
        public required string Role { get; set; }

        [Column("timeofwork")]
        [StringLength(50)]
        public string? TimeOfWork { get; set; }

        [Column("typeofstaff")]
        [StringLength(50)]
        public string? TypeOfStaff { get; set; }
        [Column("CreateDate")]
        [Required]
        public DateTime CreateDate { get; set; }
        [Column("status")]
        [StringLength(10)]
        public UserStatusEnum Status { get; set; }

        [InverseProperty("Creator")]
        public List<AppointmentEntity>? AppointmentsCreated { get; set; }

        [InverseProperty("Updater")]
        public List<AppointmentEntity>? AppointmentsUpdated { get; set; }
        public ICollection<WorkEntity> Works { get; set; } = [];

        [InverseProperty("Creator")]
        public List<ContractEntity>? ContractsCreated { get; set; }

        [InverseProperty("Updater")]
        public List<ContractEntity>? ContractsUpdated { get; set; }

        [InverseProperty("Creator")]
        public List<RealEstateEntity>? RealEstatesCreated { get; set; }

        [InverseProperty("Updater")]
        public List<RealEstateEntity>? RealEstatesUpdated { get; set; }

        public ICollection<MappingUserAppointmentEntity> MappingUserAppointments { get; set; } = new List<MappingUserAppointmentEntity>();

        public List<MappingUserNotificationEntity>? MappingUserNotifications { get; set; }
    }
}