using OfficeControlSystemApi.Models;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface IAccessCardService
    {
        Task<AccessCard> CreateNewAccessCardAsync(Employee employee);
        void AddVisitHistory(AccessCard accessCard, VisitHistory visitHistory);
    }
}
