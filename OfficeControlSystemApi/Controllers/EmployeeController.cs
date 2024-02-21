using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Services;
using OfficeControlSystemApi.Services.Commands;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ICreateEmployeeCommand _createEmployeeCommand;

        public EmployeeController(
            ICreateEmployeeCommand createEmployeeCommand)
        {
            _createEmployeeCommand = createEmployeeCommand;
        }

        [HttpPost("employee")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeInput, CancellationToken cancellationToken)
        {
            try
            {
                return new OkObjectResult(await _createEmployeeCommand.ExecuteAsync(employeeInput, cancellationToken));
            }
            catch (ArgumentException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
