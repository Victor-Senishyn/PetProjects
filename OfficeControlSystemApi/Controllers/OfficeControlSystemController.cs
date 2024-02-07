using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Services;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeControlSystemController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAccessCardService _accessCardService;
        private readonly IVisitHistoryService _visitHistoryService;

        public OfficeControlSystemController(AppDbContext context)
        {
            _employeeService = new EmployeeService(context);
            _accessCardService = new AccessCardService(context);
            _visitHistoryService = new VisitHistoryService(context);
        }

        [HttpPost("employee")]
        public IActionResult AddEmployee([FromBody] Employee employeeInput)
        {
            try
            {
                var newEmployee = _employeeService.AddEmployee(employeeInput);
                return Ok(newEmployee);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("exit/{visitHistoryId}")]
        public IActionResult UpdateExitDateTime(long visitHistoryId)
        {
            try
            {
                var newVisitHistory = _visitHistoryService.UpdateExitDateTime(visitHistoryId);
                return Ok(newVisitHistory);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("visit/{employeeId}")]
        public IActionResult AddAccessHistory(long employeeId)
        {
            try
            {
                var newVisitHistory = _visitHistoryService.AddVisitHistory(employeeId);
                return Ok(newVisitHistory);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
