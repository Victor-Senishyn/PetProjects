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
        [InlineData("John", "Doe", 1)]
        [InlineData("Jane", "Smith", 2)]
        public async Task ExecuteAsync_WithValidInputs_ReturnsEmployeeDto(string firstName, string lastName, int accessLevel)
        {
            // Arrange
            var createEmployeeCommand = new CreateEmployeeCommand(_employeeRepository);
            var employeeDto = new EmployeeDto { FirstName = firstName, LastName = lastName };

            // Act
            var result = await createEmployeeCommand.ExecuteAsync(employeeDto, accessLevel, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.FirstName.Should().Be(firstName);
            result.LastName.Should().Be(lastName);
        }

        [Theory]
        [InlineData("John", "Doe", -1)]
        [InlineData("Jane", "Smith", 4)]
        public async Task ExecuteAsync_WithInvalidAccessLevel_ThrowsArgumentException(string firstName, string lastName, int accessLevel)
        {
            // Arrange
            var createEmployeeCommand = new CreateEmployeeCommand(_employeeRepository);
            var employeeDto = new EmployeeDto { FirstName = firstName, LastName = lastName };

            // Act
            Func<Task> act = async () => await createEmployeeCommand.ExecuteAsync(employeeDto, accessLevel, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>().WithMessage("Wrong access level");
        }
    }
}
