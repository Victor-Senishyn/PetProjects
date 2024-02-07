using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Models;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface IEmployeeService
    {
        Employee AddEmployee(Employee employeeInput);
    }
}
