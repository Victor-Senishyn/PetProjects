using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.Interface;

namespace OfficeControlSystemApi.Data
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByIdAsync(long id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task AddAsync(Employee entity);
        Task UpdateAsync(Employee entity);
        Task DeleteAsync(Employee entity);
    }
}
