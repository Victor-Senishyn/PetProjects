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
        private readonly EmployeeService _employeeService;
        private readonly AccessCardService _accessCardService;
        private readonly VisitHistoryService _visitHistoryService;

        public OfficeControlSystemController(AppDbContext context)
        {
            _employeeService = new EmployeeService(context);
            _accessCardService = new AccessCardService(context);
            _visitHistoryService = new VisitHistoryService(context);
        }

        [HttpPost("employee")]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employeeInput)
        {
            try
            {
                var newEmployee = await _employeeService.AddEmployeeAsync(employeeInput);
                var newAccessCard = await _accessCardService.CreateAccessCardAsync(newEmployee);
                var newVisitHistory = await _visitHistoryService.CreateVisitHistoryAsync(newAccessCard);
                _accessCardService.AddVisitHistory(newAccessCard, newVisitHistory);
                return Ok(newEmployee);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("exit/{visitHistoryId}")]
        public async Task<IActionResult> UpdateExitDateTimeAsync(long visitHistoryId)
        {
            try
            {
                var newVisitHistory = await _visitHistoryService.UpdateExitDateTime(visitHistoryId);
                return Ok(newVisitHistory);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("visit/{employeeId}")]
        public async Task<IActionResult> AddVisitHistory(long employeeId)
        {
            try///don't work now
            {
                var newAccessCard = await _accessCardService.GetAccessCardById(employeeId);//
                var newVisitHistory = await _visitHistoryService.CreateVisitHistoryAsync(newAccessCard);//
                newAccessCard.VisitHistories.Add(newVisitHistory);

                return Ok(newVisitHistory);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
