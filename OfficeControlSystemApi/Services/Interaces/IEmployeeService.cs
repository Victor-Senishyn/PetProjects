using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.DTOs;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> CreateEmployeeDtoAsync(Employee employee);
    }
}
