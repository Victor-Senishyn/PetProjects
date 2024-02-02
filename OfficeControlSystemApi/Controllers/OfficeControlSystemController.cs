using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data;

namespace OfficeControlSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeControlSystemController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public OfficeControlSystemController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("addEmployee")]
        public IActionResult AddEmployee([FromBody] Employee employeeInput)
        {
            if (employeeInput == null)
                return BadRequest("Invalid input data");

            var newEmployee = new Employee
            {
                FirstName = employeeInput.FirstName,
                LastName = employeeInput.LastName,
                AccessLevel = (AccessLevel)employeeInput.AccessLevel
            };

            var newAccessCard = new AccessCard
            {
                IssuedDate = DateTimeOffset.UtcNow,
            };

            newEmployee.AccessCards.Add(newAccessCard);

            _dbContext.Employees.Add(newEmployee);
            _dbContext.AccessCards.Add(newAccessCard);
            _dbContext.SaveChanges();

            var newVisitHistory = new VisitHistory
            {
                EmployeeId = newEmployee.Id,
                VisitDateTime = DateTimeOffset.UtcNow
            };

            _dbContext.VisitHistories.Add(newVisitHistory);
            _dbContext.SaveChanges();

            return Ok(newEmployee);
        }

        [HttpPut("updateExitTime/{accessHistoryId}")]
        public IActionResult UpdateExitDateTime(int visitHistoryId)
        {
            var visitHistory = _dbContext.VisitHistories.FirstOrDefault(ah => ah.Id == visitHistoryId);

            if (visitHistory == null)
                return NotFound($"AccessHistory with id {visitHistoryId} not found");

            visitHistory.ExitDateTime = DateTimeOffset.UtcNow;

            _dbContext.SaveChanges();

            return Ok(visitHistory);
        }

        [HttpPost("addVisitHistory/{employeeId}")]
        public IActionResult AddAccessHistory(int employeeId)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == employeeId);

            if (employee == null)
                return NotFound($"Employee with id {employeeId} not found");


            var newVisitHistory = new VisitHistory
            {
                EmployeeId = employee.Id,
                VisitDateTime = DateTimeOffset.UtcNow
            };

            _dbContext.VisitHistories.Add(newVisitHistory);
            _dbContext.SaveChanges();

            return Ok(newVisitHistory);
        }
    }
}
