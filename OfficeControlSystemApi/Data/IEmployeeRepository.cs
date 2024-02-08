using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.Interface;

namespace OfficeControlSystemApi.Data
{
    public interface IEmployeeRepository
    {
        Employee GetById(long id);
        IEnumerable<Employee> GetAll();
        void Add(Employee entity);
        void Update(Employee entity);
        void Delete(Employee entity);
    }
}
