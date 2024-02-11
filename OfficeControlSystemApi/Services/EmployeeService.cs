using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Data.Repositorys;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;
        public EmployeeService(AppDbContext context)
        {
            _employeeRepository = new EmployeeRepository(context);
        }

        public Employee CreateEmployee(string firstname, string lastname)
        {
            return new Employee { FirstName = firstname, LastName = lastname };
        }//maybe will delete

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            if (employee == null)
                throw new ArgumentException("Invalid input data");

            await _employeeRepository.AddAsync(employee);
            return employee;
        }

    }
}
