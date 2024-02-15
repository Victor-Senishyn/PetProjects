using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Data.Repositorys;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.DTOs;
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

        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto employeeDto, CancellationToken cancellationToken = default)
        {
            if (employeeDto == null)
                throw new ArgumentException("Invalid input data");

            var employee = new Employee()
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
            };

            await _employeeRepository.AddAsync(employee);

            employeeDto.Id = employee.Id;

            return employeeDto;
        }
    }
}
