using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.Interface;

namespace OfficeControlSystemApi.Data
{
    public interface IVisitHistoryRepository
    {
        Task<IEnumerable<VisitHistory>> GetAsync(Func<VisitHistory, bool> filterCriteria);
        Task AddAsync(VisitHistory entity);
        Task UpdateAsync(VisitHistory entity);
        Task DeleteAsync(VisitHistory entity);
    }
}
