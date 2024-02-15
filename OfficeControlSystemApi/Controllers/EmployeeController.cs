using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Services;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAccessCardService _accessCardService;
        private readonly IVisitHistoryService _visitHistoryService;
        private readonly IScopedService _scoped;

        public EmployeeController(
            IEmployeeService employeeService, 
            IAccessCardService accessCardService, 
            IVisitHistoryService visitHistoryService,
            IScopedService scoped
            )
        {
            _employeeService = employeeService;
            _accessCardService = accessCardService;
            _visitHistoryService = visitHistoryService;

            _scoped = scoped;
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
