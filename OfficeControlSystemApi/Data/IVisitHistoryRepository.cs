using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.Interface;

namespace OfficeControlSystemApi.Data
{
    public interface IVisitHistoryRepository
    {
        VisitHistory GetById(long id);
        IEnumerable<VisitHistory> GetAll();
        Task AddAsync(VisitHistory entity);
        void Update(VisitHistory entity);
        void Delete(VisitHistory entity);
    }
}
