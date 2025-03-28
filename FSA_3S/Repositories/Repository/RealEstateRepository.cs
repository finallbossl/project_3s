using FSA_3S.Models.Entities;
using FSA_3S.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using FSA_3S.Models;
using FSA_3S.DTOs;

namespace FSA_3S.Repositories.Repository
{
    public class RealEstateRepository(AppDbContext context) : IRealEstateRepository
    {
        private readonly AppDbContext _context = context;

        /// <summary>
        /// API Post for RealEstate
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<RealEstateEntity> CreateAsync(RealEstateEntity realEstate)
        {
            _context.RealEstates.Add(realEstate);
            await _context.SaveChangesAsync();
            return realEstate;
        }

        public async Task<RealEstateEntity?> GetByIdAsync(int id)
        {
            return await _context.RealEstates
                .Include(r => r.CreatedBy)
                .Include(r => r.UpdatedBy)
                .FirstOrDefaultAsync(r => r.RealEstateId == id);
        }
        /// <summary>
        /// API Get for RealEstate (All)
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RealEstateEntity>> GetAllAsync()
        {
            return await _context.RealEstates
                .Include(r => r.Creator)
                .Include(r => r.Updater)
                .ToListAsync();
        }
        /// <summary>
        /// API Put RealEstate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RealEstateEntity?> GetByIdForPutAsync(int id)
        {
            var entity = await _context.RealEstates.FindAsync(id);
            if (entity != null)
            {
                // Explicit loading cho các navigation properties
                await _context.Entry(entity).Reference(r => r.Creator).LoadAsync();
                await _context.Entry(entity).Reference(r => r.Updater).LoadAsync();
            }
            return entity;
        }

        public async Task UpdateAsync(RealEstateEntity realEstate)
        {
            _context.RealEstates.Update(realEstate);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// API Delete RealEstate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(int id)
        {
            var realEstate = await _context.RealEstates.FindAsync(id);
            if (realEstate == null) return false;

            _context.RealEstates.Remove(realEstate);
            await _context.SaveChangesAsync();
            return true;
        }
        /// <summary>
        /// Check owner for Contract
        /// </summary>
        /// <returns></returns>
        public async Task<List<RealEstateBasicInfoDto>> GetRealEstateBasicInfoAsync()
        {
            return await _context.RealEstates
                .Select(r => new RealEstateBasicInfoDto
                {
                    RealEstateId = r.RealEstateId,
                    Seller = r.Seller
                })
                .ToListAsync();
        }
    }
}