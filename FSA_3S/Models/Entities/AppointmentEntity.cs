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

        [ForeignKey("CreatedBy")]
        public DateTime CreatedBy { get; set; }
        public UserEntity? Creator { get; set; }

        [ForeignKey("UpdatedBy")]
        public int? UpdatedBy { get; set; }
        public UserEntity? Updater { get; set; }

        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedAt")]
        public DateTime UpdatedAt { get; set; }
        public ICollection<MappingUserAppointmentEntity> MappingUserAppointments { get; set; } = new List<MappingUserAppointmentEntity>();

       
    }

}