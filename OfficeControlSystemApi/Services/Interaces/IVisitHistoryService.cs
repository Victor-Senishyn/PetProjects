using OfficeControlSystemApi.Models;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface IVisitHistoryService
    {
        Task<VisitHistory> CreateVisitHistoryAsync(AccessCard accessCard);
        Task<VisitHistory> UpdateExitDateTime(long visitHistoryId);
    }
}
