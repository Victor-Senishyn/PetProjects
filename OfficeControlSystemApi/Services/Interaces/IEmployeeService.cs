using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Models;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployeeAsync(Employee employee);
    }
}
