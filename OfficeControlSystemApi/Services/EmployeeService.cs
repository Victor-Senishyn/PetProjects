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
    }
}
