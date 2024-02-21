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

        public async Task<EmployeeDto> ExecuteAsync(EmployeeDto employeeDto, CancellationToken cancellationToken)
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

            await _employeeRepository.CommitAsync();
            employeeDto.Id = employee.Id;

            return employeeDto;
        }
    }

}
