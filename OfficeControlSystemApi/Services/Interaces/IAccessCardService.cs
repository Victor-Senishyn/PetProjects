using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.DTOs;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface IAccessCardService
    {
        Task<AccessCardDto> CreateAccessCardDtoAsync(Employee employee);
        Task<AccessCardDto> GetAccessCardByIdAsync(long id);
    }
}
