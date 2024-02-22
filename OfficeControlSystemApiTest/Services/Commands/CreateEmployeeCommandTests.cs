using FakeItEasy;
using FluentAssertions;
using OfficeControlSystemApi.Data.Interfaces;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Services.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace OfficeControlSystemApiTest.Services.Commands
{
    public class CreateEmployeeCommandTests
    {
        private readonly IEmployeeRepository _employeeRepository;

        public CreateEmployeeCommandTests()
        {
            _employeeRepository = A.Fake<IEmployeeRepository>();
        }

        [Theory]
        [InlineData("FirstName", "SecondName", 1)]
        [InlineData("FirstName", "SecondName", 2)]
        [InlineData("FirstName", "SecondName", 3)]
        public async Task CreateEmployeeCommand_ExecuteAsync_ReturnsEmployeeDto(string firstName, string lastName, int accessLevel)
        {
            // Arrange
            var createEmployeeCommand = new CreateEmployeeCommand(_employeeRepository);
            var employeeDto = new EmployeeDto { FirstName = firstName, LastName = lastName };

            // Act
            var result = await createEmployeeCommand.ExecuteAsync(employeeDto, accessLevel, CancellationToken.None);

            // Assert
            accessLevel.Should().BeGreaterThan(0);
            accessLevel.Should().BeLessThan(4);
            result.Should().NotBeNull();
            result.FirstName.Should().Be(firstName);
            result.LastName.Should().Be(lastName);
        }
    }
}
