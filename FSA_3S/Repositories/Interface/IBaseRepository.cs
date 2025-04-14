using System.Collections.Generic;
using System.Threading.Tasks;

namespace FSA_3S.Repositories.Interface
{
    public interface IBaseRepository
    {
        Task AddEntitiesAsync<T>(List<T> entities) where T : class;
        Task UpdateEntitiesAsync<T>(List<T> entities) where T : class;
    }
}