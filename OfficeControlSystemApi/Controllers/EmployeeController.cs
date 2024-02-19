using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Models.DTOs;
using OfficeControlSystemApi.Services;
using OfficeControlSystemApi.Services.Commands;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly IEmployeeService _employeeService;
        //private readonly IAccessCardService _accessCardService;
        //private readonly IVisitHistoryService _visitHistoryService;
        //private readonly AppDbContext _dbContext;

        private readonly CreateEmployeeCommand _createEmployeeCommand;

        public EmployeeController(
            //IEmployeeService employeeService, 
            //IAccessCardService accessCardService, 
            //IVisitHistoryService visitHistoryService,
            //AppDbContext dbContext,
            CreateEmployeeCommand createEmployeeCommand
            )
        {
            //_employeeService = employeeService;
            //_accessCardService = accessCardService;
            //_visitHistoryService = visitHistoryService;
            //_dbContext = dbContext;
            _createEmployeeCommand = createEmployeeCommand;
        }

        [HttpPost("employee")]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employeeInput, CancellationToken cancellationToken)
        {
            return await _createEmployeeCommand.ExecuteAsync(employeeInput, cancellationToken);
        }
    }
}
