using OfficeControlSystemApi.Models;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface IVisitHistoryService
    {
        Task<VisitHistory> CreateVisitHistoryAsync(long accessCardId);
        VisitHistory UpdateExitDateTime(long visitHistoryId);
    }
}
