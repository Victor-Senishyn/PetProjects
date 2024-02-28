using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Services;
using OfficeControlSystemApi.Services.Commands;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Controllers
{
    public class VisitHistoryController : Controller
    {
        private readonly IVisitHistoryService _visitHistoryService;
        private readonly ICreateVisitHistoryCommand _createVisitHistoryCommand;

        public VisitHistoryController(
            IVisitHistoryService visitHistoryService,
            ICreateVisitHistoryCommand createVisitHistoryCommand)
        {
            _visitHistoryService = visitHistoryService;
            _createVisitHistoryCommand = createVisitHistoryCommand;
        }

        [HttpPatch("exit/{visitHistoryId}")]
        [Authorize(Policy = "RequireAdministratorOrUserRole")]
        public async Task<IActionResult> UpdateExitDateTimeAsync(
            long visitHistoryId, 
            CancellationToken cancellationToken)
        {
            try
            {
                var visitHistory = await _visitHistoryService.UpdateExitDateTime(visitHistoryId, cancellationToken);
                return Ok(visitHistory);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) when (ex is OperationCanceledException or TaskCanceledException)
            {
                return BadRequest("Request canceled due to user action or timeout.");
            }
        }

        [HttpPost("visit/{accessCardId}")]
        [Authorize(Policy = "RequireAdministratorOrUserRole")]
        public async Task<IActionResult> AddVisitHistory(
            long accessCardId, 
            CancellationToken cancellationToken)
        {
            try
            {
                return new OkObjectResult(await _createVisitHistoryCommand.ExecuteAsync(accessCardId, cancellationToken));
            }
            catch (ArgumentException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
