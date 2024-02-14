using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models;
using OfficeControlSystemApi.Models.DTOs;
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
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeInput)
        {
            try
            {
                var newEmployee = await _employeeService.CreateEmployeeAsync(employeeInput);
                var newAccessCard = await _accessCardService.CreateAccessCardAsync(employeeInput);
                var newVisitHistory = await _visitHistoryService.CreateVisitHistoryAsync(newAccessCard);

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

        [HttpPost("visit/{accessCardId}")]
        public async Task<IActionResult> AddVisitHistory(long accessCardId)
        {
            try
            {
                var newAccessCard = await _accessCardService.GetAccessCardByIdAsync(accessCardId);
                var newVisitHistory = await _visitHistoryService.CreateVisitHistoryAsync(newAccessCard);

                return Ok(newVisitHistory);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
