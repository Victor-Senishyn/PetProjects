﻿using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Models.Enums;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Services.Commands
{
    public class CreateEmployeeCommand : ICreateEmployeeCommand
    {
        private readonly IEmployeeRepository _employeeRepository;


        public CreateEmployeeCommand(
            IEmployeeRepository employeeRepository
            )
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> ExecuteAsync(EmployeeDto employeeDto, int accessLevel, CancellationToken cancellationToken)
        {
            var employee = new Employee()
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
            };

            if (accessLevel < 0 || accessLevel > 3)
                throw new ArgumentException("Wrong access level");

            var accessCard = new AccessCard
            {
                AccessLevel = (AccessLevel)accessLevel,
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
