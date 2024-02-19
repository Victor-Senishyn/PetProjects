using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Services.Commands
{
    public class CreateEmployeeCommand : Command
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAccessCardService _accessCardService;
        private readonly IVisitHistoryService _visitHistoryService;
        private readonly AppDbContext _dbContext;

        public CreateEmployeeCommand(
            IEmployeeService employeeService,
            IAccessCardService accessCardService,
            IVisitHistoryService visitHistoryService,
            AppDbContext dbContext)
        {
            _employeeService = employeeService;
            _accessCardService = accessCardService;
            _visitHistoryService = visitHistoryService;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> ExecuteAsync(EmployeeDto employeeInput, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var employee = await _employeeService.CreateEmployeeAsync(employeeInput, cancellationToken);
                var accessCard = await _accessCardService.CreateAccessCardAsync(employeeInput, cancellationToken);
                var visitHistory = await _visitHistoryService.CreateVisitHistoryAsync(accessCard, cancellationToken);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync(cancellationToken);
                return new OkObjectResult(employee);
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
