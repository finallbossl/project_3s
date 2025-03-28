using CloudinaryDotNet.Actions;
using FSA_3S.Helpers;
using FSA_3S.Models.Entities;
using FSA_3S.Models.Requests;
using FSA_3S.Models.Respone;
using FSA_3S.Repositories.Interface;
using FSA_3S.Repositories.Repository;
using FSA_3S.Services.Interface;
using Microsoft.AspNetCore.Http;
using System.Diagnostics.Contracts;

namespace FSA_3S.Services.Service
{
    public class RealEstateService(IRealEstateRepository _realestaterepository, IHttpContextAccessor _httpContextAccessor, CloudinaryService _cloudinaryService) : IRealEstateService
    {
        /// <summary>
        /// API Post RealEstate
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public async Task<RealEstateRespone> CreateAsync(RealEstateRequest request)
        {
            int createdBy = UserIdHelper.GetUserId(_httpContextAccessor)
                             ?? throw new UnauthorizedAccessException("Invalid or missing user ID.");

            string? imageUrl = null;
            if (request.ImagePath != null)
            {
                imageUrl = await _cloudinaryService.UploadImageAsync(request.ImagePath);
            }

            var realEstate = new RealEstateEntity
            {
                RealEstateName = request.RealEstateName,
                RealEstateType = request.RealEstateType,
                RealEstateStatus = Enum.RealEstateStatusEnum.Waiting_for_approval,
                Price = request.Price,
                Seller = request.Seller,
                SaleDate = request.SaleDate,
                Coordinate = request.Coordinate,
                ImagePath = imageUrl,
                Address = request.Address,
                Description = request.Description,
                CreatedBy = createdBy,
                UpdatedBy = null,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null,
            };

            try
            {
                var result = await _realestaterepository.CreateAsync(realEstate);

                return new RealEstateRespone
                {
                    RealEstateId = result.RealEstateId,
                    RealEstateName = result.RealEstateName,
                    RealEstateType = result.RealEstateType,
                    RealEstateStatus = Enum.RealEstateStatusEnum.Waiting_for_approval,
                    Price = result.Price,
                    Seller = result.Seller,
                    Coordinate = result.Coordinate,
                    SaleDate = result.SaleDate,
                    ImagePath = result.ImagePath,
                    Address = result.Address,
                    Description = result.Description,
                    CreatedBy = result.CreatedBy,
                    UpdatedBy = result.UpdatedBy,
                    CreatedAt = result.CreatedAt,
                    UpdatedAt = result.UpdatedAt
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"[CreateAsync] Failed to create real estate: {ex.InnerException?.Message ?? ex.Message}", ex);
            }
        }
        public async Task<RealEstateEntity?> GetByIdAsync(int id)
        {
            return await _realestaterepository.GetByIdAsync(id);
        }
        /// <summary>
        /// API Get for RealEstate (All)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RealEstateRespone>> GetAllRealEstateAsync()
        {
            var result = await _realestaterepository.GetAllAsync();

            return result.Select(r => new RealEstateRespone
            {
                RealEstateId = r.RealEstateId,
                RealEstateName = r.RealEstateName,
                RealEstateType = r.RealEstateType,
                RealEstateStatus = r.RealEstateStatus,
                Price = r.Price,
                Seller = r.Seller,
                Coordinate = r.Coordinate,
                SaleDate = r.SaleDate,
                ImagePath = r.ImagePath,
                Address = r.Address,
                Description = r.Description,
                CreatedBy = r.CreatedBy,
                UpdatedBy = r.UpdatedBy,
                CreatedAt = r.CreatedAt,
                UpdatedAt = r.UpdatedAt
            }).ToList();
        }
        /// <summary>
        /// API Put RealEstate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<RealEstateRespone> UpdateRealEstateAsync(int id, RealEstateRequest request)
        {
            // Dùng FindAsync + Explicit Loading
            var realEstate = await _realestaterepository.GetByIdForPutAsync(id);
            if (realEstate == null) return null;

            realEstate.RealEstateName = request.RealEstateName;
            realEstate.RealEstateType = request.RealEstateType;
            realEstate.RealEstateStatus = request.RealEstateStatus;
            realEstate.Price = request.Price;
            realEstate.Seller = request.Seller;
            realEstate.SaleDate = request.SaleDate;
            realEstate.Coordinate = request.Coordinate;
            realEstate.Address = request.Address;
            realEstate.Description = request.Description;

            if (request.ImagePath != null)
            {
                realEstate.ImagePath = await _cloudinaryService.UploadImageAsync(request.ImagePath);
            }

            await _realestaterepository.UpdateAsync(realEstate);

            var response = new RealEstateRespone
            {
                RealEstateId = realEstate.RealEstateId,
                RealEstateName = realEstate.RealEstateName,
                RealEstateType = realEstate.RealEstateType,
                RealEstateStatus = realEstate.RealEstateStatus,
                Price = realEstate.Price,
                Seller = realEstate.Seller,
                SaleDate = realEstate.SaleDate,
                Coordinate = realEstate.Coordinate,
                Address = realEstate.Address,
                Description = realEstate.Description,
                ImagePath = realEstate.ImagePath,
                CreatedBy = realEstate.CreatedBy,
                UpdatedBy = realEstate.UpdatedBy,
                CreatedAt = realEstate.CreatedAt,
                UpdatedAt = realEstate.UpdatedAt
            };

            return response;
        }
        /// <summary>
        /// API Delete RealEstate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRealEstateAsync(int id)
        {
            var isDeleted = await _realestaterepository.DeleteAsync(id);
            return isDeleted;
        }
    }
}