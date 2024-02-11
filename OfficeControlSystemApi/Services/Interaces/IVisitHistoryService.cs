using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.DTOs;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface IVisitHistoryService
    {
        Task<VisitHistoryDto> CreateVisitHistoryDtoAsync(AccessCardDto accessCard);
        Task<VisitHistoryDto> UpdateExitDateTime(long visitHistoryId);
    }
}
