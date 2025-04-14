using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("appointment")]
    public class AppointmentEntity
    {
        [Key]
        [Column("appointmentId")]
        public int AppointmentId { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;

        [Column("title")]
        [StringLength(50)]
        public required string Title { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("appointmentDate")]
        public DateOnly AppointmentDate { get; set; }

        [Column("status")]
        [StringLength(10)]
        public string? Status { get; set; }

        [Column("address")]
        [StringLength(255)]
        public string? Address { get; set; }

        [Required]
        [ForeignKey(nameof(Creator))]
        [Column("createdBy")]
        public int CreatedBy { get; set; }
        public UserEntity? Creator { get; set; }

        [ForeignKey(nameof(Updater))]
        [Column("updatedBy")]
        public int? UpdatedBy { get; set; }
        public UserEntity? Updater { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime? UpdatedAt { get; set; }

        public ICollection<MappingUserAppointmentEntity> MappingUserAppointments { get; set; } = new List<MappingUserAppointmentEntity>();

       
    }

}