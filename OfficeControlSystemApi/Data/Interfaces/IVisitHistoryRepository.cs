using OfficeControlSystemApi.Data.Filters;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.Interface;

namespace OfficeControlSystemApi.Data.Interfaces
{
    public interface IVisitHistoryRepository
    {
        Task<IQueryable<VisitHistory>> GetAsync(VisitHistoryFilter visitHistoryFilter);
        Task AddAsync(VisitHistory entity);
        Task UpdateAsync(VisitHistory entity);
        Task DeleteAsync(VisitHistory entity);
    }
}
