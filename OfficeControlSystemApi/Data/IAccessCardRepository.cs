using OfficeControlSystemApi.Models;

namespace OfficeControlSystemApi.Data
{
    public interface IAccessCardRepository
    {
        Task<AccessCard> GetByIdAsync(long id);
        Task<IEnumerable<AccessCard>> GetAllAsync();
        Task AddAsync(AccessCard entity);
        Task UpdateAsync(AccessCard entity);
        Task DeleteAsync(AccessCard entity);
    }
}
