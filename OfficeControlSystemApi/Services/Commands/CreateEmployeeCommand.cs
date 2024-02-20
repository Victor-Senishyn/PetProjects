using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Services.Commands
{
    public class CreateEmployeeCommand : ICreateEmployeeCommand
    {
        private readonly AppDbContext _dbContext;
        private readonly IEmployeeRepository _employeeRepository;


        public CreateEmployeeCommand(
            AppDbContext dbContext,
            IEmployeeRepository employeeRepository
            )
        {
            _dbContext = dbContext;
            _employeeRepository = employeeRepository;
        }

        public async Task<IActionResult> ExecuteAsync(EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var employee = new Employee()
                {
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName,
                };

                var accessCard = new AccessCard
                {
                    AccessLevel = AccessLevel.Low,
                    EmployeeId = employee.Id
                };

                var visitHistory = new VisitHistory
                {
                    AccessCardId = accessCard.Id,
                    VisitDateTime = DateTimeOffset.UtcNow
                };

                accessCard.VisitHistories.Add( visitHistory );
                employee.AccessCards.Add( accessCard );

                await _employeeRepository.AddAsync(employee);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync(cancellationToken);
                employeeDto.Id = employee.Id;

                return new OkObjectResult(employeeDto);
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
