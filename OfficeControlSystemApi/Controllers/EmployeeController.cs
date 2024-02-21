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
        private readonly AppDbContext _dbContext;
        public EmployeeController(
            ICreateEmployeeCommand createEmployeeCommand,
            AppDbContext dbContext)
        {
            _createEmployeeCommand = createEmployeeCommand;
            _dbContext = dbContext;
        }

        [HttpPost("employee")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeInput, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await transaction.CommitAsync(cancellationToken);
                return new OkObjectResult(await _createEmployeeCommand.ExecuteAsync(employeeInput, cancellationToken));
            }
            catch (ArgumentException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            catch (Exception ex) when (ex is OperationCanceledException or TaskCanceledException)
            {
                await transaction.RollbackAsync(cancellationToken);
                return new BadRequestObjectResult("Request canceled due to user action or timeout.");
            }
        }
    }
}
