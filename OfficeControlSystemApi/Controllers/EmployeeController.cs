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
        private readonly AppDbContext _dbContext;


        public EmployeeController(
            IEmployeeService employeeService, 
            IAccessCardService accessCardService, 
            IVisitHistoryService visitHistoryService,
            AppDbContext dbContext
            )
        {
            _employeeService = employeeService;
            _accessCardService = accessCardService;
            _visitHistoryService = visitHistoryService;
            _dbContext = dbContext;
        }

        [HttpPost("employee")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeInput, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var employee = await _employeeService.CreateEmployeeAsync(employeeInput, cancellationToken);
                var accessCard = await _accessCardService.CreateAccessCardAsync(employeeInput, cancellationToken);
                var visitHistory = await _visitHistoryService.CreateVisitHistoryAsync(accessCard, cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                return Ok(employee);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) when (ex is OperationCanceledException or TaskCanceledException)
            {
                await transaction.RollbackAsync(cancellationToken);
                return BadRequest("Request canceled due to user action or timeout.");
            }
        }
    }
}
