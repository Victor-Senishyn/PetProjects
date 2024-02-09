using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.Interface;

namespace OfficeControlSystemApi.Data
{
    public interface IVisitHistoryRepository
    {
        Task<VisitHistory> GetByIdAsync(long id);
        Task<IEnumerable<VisitHistory>> GetAllAsync();
        Task AddAsync(VisitHistory entity);
        Task UpdateAsync(VisitHistory entity);
        Task DeleteAsync(VisitHistory entity);
    }
}
