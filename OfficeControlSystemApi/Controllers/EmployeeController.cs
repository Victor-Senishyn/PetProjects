using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Services;
using OfficeControlSystemApi.Services.Commands;
using OfficeControlSystemApi.Services.Interaces;
using System.ComponentModel.DataAnnotations;

namespace OfficeControlSystemApi.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly ICreateEmployeeCommand _createEmployeeCommand;

        public EmployeeController(
            ICreateEmployeeCommand createEmployeeCommand)
        {
            _createEmployeeCommand = createEmployeeCommand;
        }

        [HttpPost("employee/{accessLevel}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CreateEmployee(
            [FromBody] EmployeeDto employeeInput, 
            int accessLevel, 
            CancellationToken cancellationToken)
        {
            try
            {
                return new OkObjectResult(await _createEmployeeCommand.ExecuteAsync(employeeInput, accessLevel, cancellationToken));
            }
            catch (ArgumentException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
