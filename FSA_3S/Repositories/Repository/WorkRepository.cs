using FSA_3S.Models.Entities;
using FSA_3S.Repositories.Interface;
using FSA_3S.Models;
using Microsoft.EntityFrameworkCore;

namespace FSA_3S.Repositories.Repository
{
    public class WorkRepository(AppDbContext context) : IWorkRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddWorkAsync(WorkEntity work)
        {
            _context.WorkEntity.Add(work);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// API Get All Work Repository
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<WorkEntity>> GetAllWorksAsync()
        {
            return await _context.WorkEntity.ToListAsync();
        }

        /// <summary>
        /// API Put Work
        /// </summary>
        /// <param name="workId"></param>
        /// <returns></returns>
        public async Task<WorkEntity?> GetWorkByIdAsync(int workId)
        {
            return await _context.WorkEntity.FindAsync(workId);
        }

        public async Task UpdateWorkAsync(WorkEntity work)
        {
            _context.WorkEntity.Update(work);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// API Delete Work
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public async Task DeleteWorkAsync(WorkEntity work)
        {
            _context.WorkEntity.Remove(work);
            await _context.SaveChangesAsync();
        }
    }
}