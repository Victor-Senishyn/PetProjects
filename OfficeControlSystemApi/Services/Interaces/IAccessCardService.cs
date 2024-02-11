using OfficeControlSystemApi.Models;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface IAccessCardService
    {
        Task<AccessCard> CreateAccessCardAsync(Employee employee);
        Task<AccessCard> GetAccessCardById(long id);
    }
}
