using FSA_3S.Enum;

namespace FSA_3S.Models.Respone
{
    public class RealEstateRespone
    {
        public int RealEstateId { get; set; }
        public string? RealEstateName { get; set; }
        public RealEstateTypeEnum RealEstateType { get; set; }
        public RealEstateStatusEnum RealEstateStatus { get; set; }
        public int Seller { get; set; }
        public decimal Price { get; set; }
        public string? Coordinate { get; set; }
        public DateTime SaleDate { get; set; }
        public string? ImagePath { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}