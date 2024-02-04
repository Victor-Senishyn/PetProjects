using OfficeControlSystemApi.Models;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface IVisitHistoryService
    {
        VisitHistory AddVisitHistory(long accessCardId);
        VisitHistory UpdateExitDateTime(long visitHistoryId);
    }
}
