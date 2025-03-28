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

        [ForeignKey("AppointmentId")]
        public int AppointmentId { get; set; }
        public AppointmentEntity? Appointment { get; set; }
        
    }
}