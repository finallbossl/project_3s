using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace FSA_3S.DTOs
{
    public class AuditDTO
    {
        public int? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? EntityType { get; set; }
        public string? ActionType { get; set; }
        public int? ContractId { get; set; }
        public int? RealEstateId { get; set; }
        public string RealEstateName { get; set; }
        public string CreatedByFullName { get; set; }
        public string UpdatedByFullName { get; set; }

        public bool? IsDeleted => ActionType == "Deleted";
    }
}