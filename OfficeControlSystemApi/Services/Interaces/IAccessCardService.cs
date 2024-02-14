using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.DTOs;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface IAccessCardService
    {
        Task<AccessCardDto> CreateAccessCardAsync(EmployeeDto employeeDto);
        Task<AccessCardDto> GetAccessCardByIdAsync(long id);
    }
}
