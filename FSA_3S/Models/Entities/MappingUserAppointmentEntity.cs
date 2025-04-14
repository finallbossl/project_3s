using FSA_3S.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSA_3S.Models.Entities
{
    [Table("mappinguserappointment")]
    public class MappingUserAppointmentEntity
    {
        [Key]
        [Column("mappingUserAppointmentId")]
        public int MappingUserAppointmentId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
        public string Status { get; set; }

        [ForeignKey("AppointmentId")]
        public int AppointmentId { get; set; }
        public AppointmentEntity? Appointment { get; set; }
        
        [Column("approval")]
        [StringLength(20)]
        public ApprovalStatusEnum ApprovalStatus { get; set; }

        [ForeignKey(nameof(Creator))]
        public int CreatedBy { get; set; }  
        public UserEntity? Creator { get; set; } 

        [ForeignKey(nameof(Updater))]
        public int? UpdatedBy { get; set; }  
        public UserEntity? Updater { get; set; } 

        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }  

        [Column("updatedAt")]
        public DateTime UpdatedAt { get; set; }  


    }
}