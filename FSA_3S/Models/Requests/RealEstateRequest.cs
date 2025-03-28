using FSA_3S.Enum;
using System.ComponentModel.DataAnnotations;

namespace FSA_3S.Models.Requests
{
    public class RealEstateRequest
    {
        /// <summary>
        /// Tên của bất động sản (bắt buộc, tối đa 50 ký tự)
        /// </summary>
        //[Required(ErrorMessage = "Tên bất động sản là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Tên bất động sản không được vượt quá 50 ký tự.")]
        public required string RealEstateName { get; set; }

        /// <summary>
        /// Loại bất động sản (bắt buộc)
        /// </summary>
        //[Required(ErrorMessage = "Loại bất động sản là bắt buộc.")]
        [EnumDataType(typeof(RealEstateTypeEnum), ErrorMessage = "Loại bất động sản không hợp lệ.")]
        public RealEstateTypeEnum RealEstateType { get; set; }

        /// <summary>
        /// Trạng thái của bất động sản (bắt buộc)
        /// </summary>
        //[Required(ErrorMessage = "Trạng thái là bắt buộc.")]
        [EnumDataType(typeof(RealEstateStatusEnum), ErrorMessage = "Trạng thái không hợp lệ.")]
        public RealEstateStatusEnum RealEstateStatus { get; set; }

        /// <summary>
        /// Giá bất động sản (bắt buộc, giá trị dương)
        /// </summary>
        //[Required(ErrorMessage = "Giá là bắt buộc.")]
        [Range(1, double.MaxValue, ErrorMessage = "Giá phải là một số dương.")]
        public decimal Price { get; set; }

        /// <summary>
        /// Số phòng ngủ (tuỳ chọn, từ 0 đến 100)
        /// </summary>
        [Required(ErrorMessage = "Người bán không được để trống.")]
        public int Seller { get; set; }

        /// <summary>
        /// Số phòng tắm (tuỳ chọn, từ 0 đến 100)
        /// </summary>
        [Required(ErrorMessage = "Ngày bán đang trống.")]
        public DateTime SaleDate { get; set; }

        public string? Coordinate { get; set; }

        /// <summary>
        /// Đường dẫn ảnh (tuỳ chọn, tối đa 255 ký tự)
        /// </summary>
        //[Required(ErrorMessage = "Cần có ảnh.")]
        public IFormFile? ImagePath { get; set; }

        /// <summary>
        /// Địa chỉ bất động sản (bắt buộc, tối đa 250 ký tự)
        /// </summary>
        //[Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        [StringLength(250, ErrorMessage = "Địa chỉ không được vượt quá 250 ký tự.")]
        public string? Address { get; set; }

        /// <summary>
        /// Mô tả chi tiết về bất động sản (tuỳ chọn, tối đa 1000 ký tự)
        /// </summary>
        [StringLength(1000, ErrorMessage = "Mô tả không được vượt quá 1000 ký tự.")]
        public string? Description { get; set; }
    }
}