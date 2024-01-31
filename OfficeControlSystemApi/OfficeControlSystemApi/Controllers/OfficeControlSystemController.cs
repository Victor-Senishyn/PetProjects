using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models;

namespace OfficeControlSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeControlSystemController : ControllerBase
    {
        private readonly OfficeControlSystemDbContext _dbContext;

        public OfficeControlSystemController(OfficeControlSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("addEmployee")]
        public IActionResult AddEmployee([FromBody] EmployeeInputModel employeeInput, int accessLevel)
        {
            if (employeeInput == null)
                return BadRequest("Invalid input data");
            

            var newEmployee = new Employee
            {
                EmployeeId = Guid.NewGuid(),
                AccessCardId = Guid.NewGuid(),
                Name = employeeInput.Name,
            };

            var newAccessCard = new AccessCard
            {
                Id = newEmployee.AccessCardId,
                EmployeeId = newEmployee.EmployeeId,
                AccessLevel = accessLevel,
                Employee = newEmployee,
                AccessHistory = new List<AccessHistory>()
            };

            var newAccessHistory = new AccessHistory
            {
                AccessHistoryId = Guid.NewGuid(),
                AccessCardId = newAccessCard.Id,
                AccessCard = newAccessCard,
                EntryTime = DateTime.Now,
            };

            newAccessCard.AccessHistory.Add(newAccessHistory);
            newEmployee.AccessCard = newAccessCard;

            _dbContext.Employees.Add(newEmployee);
            _dbContext.AccessCards.Add(newAccessCard);
            _dbContext.AccessHistories.Add(newAccessHistory);

            _dbContext.SaveChanges();

            return Ok(newEmployee);
        }

        [HttpPut("updateExitTime/{accessHistoryId}")]
        public IActionResult UpdateExitTime(Guid accessHistoryId)
        {
            var accessHistory = _dbContext.AccessHistories.FirstOrDefault(ah => ah.AccessHistoryId == accessHistoryId);

            if (accessHistory == null)
                return NotFound($"AccessHistory with id {accessHistoryId} not found");

            accessHistory.ExitTime = DateTime.Now;

            _dbContext.SaveChanges();

            return Ok(accessHistory);
        }

        [HttpPost("addAccessHistory/{employeeId}")]
        public IActionResult AddAccessHistory(Guid employeeId)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);

            if (employee == null)
                return NotFound($"Employee with id {employeeId} not found");
            

            var newAccessHistory = new AccessHistory
            {
                AccessHistoryId = Guid.NewGuid(),
                AccessCardId = employee.AccessCardId,
                AccessCard = employee.AccessCard,
                EntryTime = DateTime.Now,
            };

            employee.AccessCard.AccessHistory.Add(newAccessHistory);

            _dbContext.AccessHistories.Add(newAccessHistory);
            _dbContext.SaveChanges();

            return Ok(newAccessHistory);
        }
    }
}
