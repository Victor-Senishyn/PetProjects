using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.Interface;

namespace OfficeControlSystemApi.Data
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAsync(Func<Employee, bool> filterCriteria);
        Task AddAsync(Employee entity);
        Task UpdateAsync(Employee entity);
        Task DeleteAsync(Employee entity);
    }
}
