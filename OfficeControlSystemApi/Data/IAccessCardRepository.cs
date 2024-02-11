using OfficeControlSystemApi.Models;

namespace OfficeControlSystemApi.Data
{
    public interface IAccessCardRepository
    {
        Task<IEnumerable<AccessCard>> Get(Func<AccessCard, bool> filterCriteria);
        Task AddAsync(AccessCard entity);
        Task UpdateAsync(AccessCard entity);
        Task DeleteAsync(AccessCard entity);
    }
}
