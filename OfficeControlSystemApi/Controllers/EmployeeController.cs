using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Services;

namespace OfficeControlSystemApi.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;
        private readonly AccessCardService _accessCardService;
        private readonly VisitHistoryService _visitHistoryService;

        public EmployeeController(AppDbContext context)
        {
            _employeeService = new EmployeeService(context);
            _accessCardService = new AccessCardService(context);
            _visitHistoryService = new VisitHistoryService(context);
        }

        [HttpPost("employee")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeInput, CancellationToken cancellationToken)
        {
            try
            {
                var newEmployee = await _employeeService.CreateEmployeeAsync(employeeInput, cancellationToken);
                var newAccessCard = await _accessCardService.CreateAccessCardAsync(employeeInput, cancellationToken);
                var newVisitHistory = await _visitHistoryService.CreateVisitHistoryAsync(newAccessCard, cancellationToken);

                return Ok(newEmployee);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) when (ex is OperationCanceledException or TaskCanceledException)
            {
                return BadRequest("Request canceled due to user action or timeout.");
            }
        }
    }
}
