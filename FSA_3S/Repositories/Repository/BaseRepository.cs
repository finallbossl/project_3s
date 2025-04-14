using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FSA_3S.Data;
using FSA_3S.Repositories.Interface;
using FSA_3S.Models;

namespace FSA_3S.Repositories.Repository
{
    public class BaseRepository(AppDbContext context) : IBaseRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddEntitiesAsync<T>(List<T> entities) where T : class
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEntitiesAsync<T>(List<T> entities) where T : class
        {
            _context.Set<T>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}