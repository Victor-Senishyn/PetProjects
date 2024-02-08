using OfficeControlSystemApi.Models;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface IVisitHistoryService
    {
        VisitHistory CreateVisitHistory(long accessCardId);
        VisitHistory UpdateExitDateTime(long visitHistoryId);
    }
}
