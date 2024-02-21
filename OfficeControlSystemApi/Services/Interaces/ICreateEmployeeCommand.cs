using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Models.DTOs;

namespace OfficeControlSystemApi.Services.Interaces
{
    public interface ICreateEmployeeCommand
    {
        Task<EmployeeDto> ExecuteAsync(EmployeeDto employeeDto, CancellationToken cancellationToken);
    }
}
