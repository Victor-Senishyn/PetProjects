using Microsoft.AspNetCore.Mvc;
using OfficeControlSystemApi.Data;
using OfficeControlSystemApi.Services;
using OfficeControlSystemApi.Services.Interaces;

namespace OfficeControlSystemApi.Controllers
{
    public class VisitHistoryController : Controller
    {
        private readonly IAccessCardService _accessCardService;
        private readonly IVisitHistoryService _visitHistoryService;
        private readonly AppDbContext _dbContext;

        public VisitHistoryController(
            IAccessCardService accessCardService,
            IVisitHistoryService visitHistoryService,
            AppDbContext dbContext)
        {
            _accessCardService = accessCardService;
            _visitHistoryService = visitHistoryService;
            _dbContext = dbContext;
        }

        [HttpPut("exit/{visitHistoryId}")]
        public async Task<IActionResult> UpdateExitDateTimeAsync(long visitHistoryId, CancellationToken cancellationToken)
        {
            try
            {
                var visitHistory = await _visitHistoryService.UpdateExitDateTime(visitHistoryId, cancellationToken);
                await _dbContext.SaveChangesAsync();

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
        public async Task<IActionResult> AddVisitHistory(long accessCardId, CancellationToken cancellationToken)
        {
            try
            {
                var accessCard = await _accessCardService.GetAccessCardByIdAsync(accessCardId, cancellationToken);
                var visitHistory = await _visitHistoryService.CreateVisitHistoryAsync(accessCard, cancellationToken);
                await _dbContext.SaveChangesAsync();

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
    }
}
